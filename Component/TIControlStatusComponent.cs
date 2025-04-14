namespace SimpleTesiraLibrary.Component;

using System;

internal class TIControlStatusComponent : ITIControlStatusComponent
{
	private readonly BiampTesiraDSP tesira;
	private readonly string instanceTag;

	private readonly string callStatePublishToken;

	public string LastNumber { get; private set; }

	public TIControlStatusComponent(BiampTesiraDSP tesira, string instanceTag)
	{
		this.tesira = tesira;
		this.instanceTag = instanceTag;

		callStatePublishToken = $"{instanceTag}#CALLSTATE";

		tesira.SubscribeToPublishToken(callStatePublishToken, HandleCallStatePublishToken);

		tesira.Connected += Initialize;

		LastNumber = "";
	}

	public void Dial(string number)
	{
		tesira.QueueCommand(TesiraCommands.TIControlStatus.Dial(instanceTag, number), null);
	}

	public void Answer()
	{
		tesira.QueueCommand(TesiraCommands.TIControlStatus.Answer(instanceTag), null);
	}

	public void End()
	{
		tesira.QueueCommand(TesiraCommands.TIControlStatus.End(instanceTag), null);
	}

	public void DTMF(char digit)
	{
		tesira.QueueCommand(TesiraCommands.TIControlStatus.DTMF(instanceTag, digit), null);
	}

	// private

	private void Initialize(object? _, EventArgs e)
	{
		tesira.QueueCommand(TesiraCommands.TIControlStatus.SubscribeCallState(instanceTag, callStatePublishToken), null);
	}

	private void HandleCallStatePublishToken(string obj)
	{
		throw new NotImplementedException();
	}
}
