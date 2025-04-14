namespace SimpleTesiraLibrary.Component;

using System;
using System.Text.RegularExpressions;

internal class LevelComponent : ILevelComponent
{
	private const int CHANNEL = 1;

	private readonly BiampTesiraDSP tesira;
	private readonly string instanceTag;

	private readonly string mutePublishToken;
	private readonly string levelPublishToken;

	public bool IsMuted { get; private set; }

	public float Level { get; private set; }

	public float LevelMin { get; private set; }

	public float LevelMax { get; private set; }

	public event EventHandler<float>? LevelChanged;
	public event EventHandler<bool>? MutedStateChanged;

	public LevelComponent(BiampTesiraDSP tesira, string instanceTag)
	{
		this.tesira = tesira;
		this.instanceTag = instanceTag;

		mutePublishToken = $"{instanceTag}#MUTE";
		levelPublishToken = $"{instanceTag}#LEVEL";

		tesira.SubscribeToPublishToken(mutePublishToken, HandleMutePublishToken);
		tesira.SubscribeToPublishToken(levelPublishToken, HandleLevelPublishToken);

		tesira.Connected += Initialize;
	}

	public void SetMuted(bool state)
	{
		tesira.QueueCommand(TesiraCommands.Mute.SetMute(instanceTag, CHANNEL, state), null);
	}

	public void SetLevel(float level)
	{
		tesira.QueueCommand(TesiraCommands.Level.SetLevel(instanceTag, CHANNEL, level), null);
	}

	// private

	private void Initialize(object? _, EventArgs e)
	{
		tesira.QueueCommand(TesiraCommands.Mute.SubscribeMute(instanceTag, CHANNEL, mutePublishToken), null);
		tesira.QueueCommand(TesiraCommands.Level.SubscribeLevel(instanceTag, CHANNEL, levelPublishToken), null);

		tesira.QueueCommand(TesiraCommands.Level.GetMinLevel(instanceTag, CHANNEL), HandleGetMinLevel);
		tesira.QueueCommand(TesiraCommands.Level.GetMaxLevel(instanceTag, CHANNEL), HandleGetMaxLevel);
	}

	private void HandleMutePublishToken(string value)
	{
		IsMuted = bool.Parse(value);

		// Trigger event
		MutedStateChanged?.Invoke(this, IsMuted);
	}

	private void HandleLevelPublishToken(string value)
	{
		Level = float.Parse(value);

		// Trigger event
		LevelChanged?.Invoke(this, Level);
	}

	private void HandleGetMinLevel(string response)
	{
		Match match = Regex.Match(response, "^\"value\":(.+?)$");
		if (match.Success)
		{
			LevelMin = float.Parse(match.Groups[1].Value);
		}
	}

	private void HandleGetMaxLevel(string response)
	{
		Match match = Regex.Match(response, "^\"value\":(.+?)$");
		if (match.Success)
		{
			LevelMax = float.Parse(match.Groups[1].Value);
		}
	}
}
