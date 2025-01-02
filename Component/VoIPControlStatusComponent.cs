using System;

namespace SimpleTesiraLibrary.Component
{
	internal class VoIPControlStatusComponent : IVoIPControlStatusComponent
	{
		private readonly BiampTesiraDSP tesira;
		private readonly string instanceTag;

		private readonly string callStatePublishToken;

		public string LastNumber { get; private set; }

		public VoIPControlStatusComponent(BiampTesiraDSP tesira, string instanceTag)
		{
			this.tesira = tesira;
			this.instanceTag = instanceTag;

			callStatePublishToken = $"{instanceTag}#CALLSTATE";

			tesira.SubscribeToPublishToken(callStatePublishToken, HandleCallStatePublishToken);

			tesira.Connected += Initialize;

			LastNumber = "";
		}

		public void Dial(uint line, uint callAppearance, string number)
		{
			tesira.QueueCommand(TesiraCommands.VoIPControlStatus.Dial(instanceTag, line, callAppearance, number), null);
		}

		public void Answer(uint line, uint callAppearance)
		{
			tesira.QueueCommand(TesiraCommands.VoIPControlStatus.Answer(instanceTag, line, callAppearance), null);
		}

		public void End(uint line, uint callAppearance)
		{
			tesira.QueueCommand(TesiraCommands.VoIPControlStatus.End(instanceTag, line, callAppearance), null);
		}

		public void DTMF(uint line, char digit)
		{
			tesira.QueueCommand(TesiraCommands.VoIPControlStatus.DTMF(instanceTag, line, digit), null);
		}

		// private

		private void Initialize(object? _, EventArgs e)
		{
			tesira.QueueCommand(TesiraCommands.VoIPControlStatus.SubscribeCallState(instanceTag, callStatePublishToken), null);
		}

		private void HandleCallStatePublishToken(string obj)
		{
			throw new NotImplementedException();
		}
	}
}
