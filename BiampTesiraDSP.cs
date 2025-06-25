using EasyConnections;
using SimpleTesiraLibrary.Component;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimpleTesiraLibrary;

public class BiampTesiraDSP
{
	private static readonly TimeSpan commandResponseTimeout = TimeSpan.FromMilliseconds(5_000);

	private readonly IConnection connection;

	private readonly string username;
	private readonly string password;

	private readonly StringBuilder inputBuffer;

	private readonly Dictionary<string, Action<string>> tokenSubscriptions;

	private readonly AsyncQueue<(string command, Action<string>? okResponseHandler)> commandQueue;

	private TaskCompletionSource? lastCommandResponseCompletionSource;
	private Action<string>? lastCommandResponseHandler;

	public event EventHandler? Connected;

	public BiampTesiraDSP(IConnection connection, string username = "default", string password = "")
	{
		connection.StatusConnected += Connection_StatusConnected;
		connection.DataReceivedAsString += Connection_DataReceivedAsString;
		this.connection = connection;

		this.username = username;
		this.password = password;

		inputBuffer = new StringBuilder();

		tokenSubscriptions = new Dictionary<string, Action<string>>();

		commandQueue = new AsyncQueue<(string, Action<string>?)>(ProcessCommand);
	}

	public IMuteComponent CreateMuteComponent(string instanceTag)
	{
		return new MuteComponent(this, instanceTag);
	}

	public ILevelComponent CreateLevelComponent(string instanceTag)
	{
		return new LevelComponent(this, instanceTag);
	}

	public ITIControlStatusComponent CreateTIControlStatusComponent(string instanceTag)
	{
		return new TIControlStatusComponent(this, instanceTag);
	}

	public IVoIPControlStatusComponent CreateVoIPControlStatusComponent(string instanceTag)
	{
		return new VoIPControlStatusComponent(this, instanceTag);
	}

	public void RecallPreset(string presetName)
	{
		QueueCommand(TesiraCommands.Preset.RecallPresetByName(presetName), null);
	}

	public bool SubscribeToPublishToken(string token, Action<string> subscriptionHandler)
	{
		return tokenSubscriptions.TryAdd(token, subscriptionHandler);
	}

	// internal

	internal void QueueCommand(string command, Action<string>? okResponseHandler)
	{
		commandQueue.Add((command, okResponseHandler));
	}

	// private

	private async void Connection_StatusConnected(object? _, EventArgs e)
	{
		// Show authentication prompt on raw connections
		await connection.SendStringAsync("\n");
	}

	private void HeartbeatHandler_HeartbeatTimedOut(object? sender, EventArgs e)
	{
		// Trigger disconnect
		connection.Disconnect();
	}

	private void Connection_DataReceivedAsString(object? _, string data)
	{
		// Append to buffer
		inputBuffer.Append(data);

		// Convert to string
		string inputString = inputBuffer.ToString();

		// Handle welcome message
		if (inputString.Contains(WELCOME_MESSAGE))
		{
			// Clear input buffer
			inputBuffer.Clear();

			// Trigger event
			Connected?.Invoke(this, EventArgs.Empty);
			return;
		}

		// Handle password prompt
		if (inputString.Contains(PASSWORD_PROMPT))
		{
			// Send password
			connection.SendStringAsync($"{password}\n").AsTask().Wait();
			return;
		}

		// Handle username prompt
		if (inputString.Contains(USERNAME_PROMPT))
		{
			// Send username
			connection.SendStringAsync($"{username}\n").AsTask().Wait();
			return;
		}

		// Handle normal responses
		Match match;
		while ((match = Regex.Match(inputBuffer.ToString(), "^(-ERR|\\+OK|!) ?(.*)\\n", RegexOptions.Multiline)).Success)
		{
			// Remove up to end of match
			inputBuffer.Remove(0, match.Index + match.Length);

			switch (match.Groups[1].Value)
			{
				case "-ERR":
				{
					HandleErrorResponse();
					break;
				}
				case "+OK":
				{
					HandleOkayResponse(match.Groups[2].Value);
					break;
				}
				case "!":
				{
					HandlePublishTokenResponse(match.Groups[2].Value);
					break;
				}
			}
		}
	}

	private void HandleErrorResponse()
	{
		// Completed
		lastCommandResponseCompletionSource?.SetResult();
	}

	private void HandleOkayResponse(string response)
	{
		// Call handler
		lastCommandResponseHandler?.Invoke(response);

		// Completed
		lastCommandResponseCompletionSource?.SetResult();
	}

	private void HandlePublishTokenResponse(string response)
	{
		Match match = Regex.Match(response, "^\"publishToken\":\"(.+?)\" \"value\":\"?(.+?)\"?$");
		if (match.Success)
		{
			string publishToken = match.Groups[1].Value;
			string value = match.Groups[2].Value;

			// Get subscription
			if (tokenSubscriptions.TryGetValue(publishToken, out Action<string>? action))
			{
				// Trigger action
				action(value);
			}
		}
	}

	private async Task ProcessCommand((string command, Action<string>? okResponseHandler) element)
	{
		// Store completion source and handler
		lastCommandResponseCompletionSource = new TaskCompletionSource();
		lastCommandResponseHandler = element.okResponseHandler;

		// Send command with command delimiter
		await connection.SendStringAsync($"{element.command}\n");

		// Wait for completetion source or timeout
		await lastCommandResponseCompletionSource.Task.WaitAsync(commandResponseTimeout);

		// Set completion source and handler to null
		lastCommandResponseCompletionSource = null;
		lastCommandResponseHandler = null;
	}

	// const

	private const string WELCOME_MESSAGE = "Welcome to the Tesira Text Protocol Server...";

	private const string USERNAME_PROMPT = "\nlogin: ";
	private const string PASSWORD_PROMPT = "Password: ";
}
