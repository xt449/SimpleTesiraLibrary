using System;

namespace SimpleTesiraLibrary.Component
{
	internal class MuteComponent : IMuteComponent
	{
		private const int CHANNEL = 1;

		private readonly BiampTesiraDSP tesira;
		private readonly string instanceTag;

		private readonly string mutePublishToken;

		public bool IsMuted { get; private set; }

		public event EventHandler<bool>? MutedStateChanged;

		public MuteComponent(BiampTesiraDSP tesira, string instanceTag)
		{
			this.tesira = tesira;
			this.instanceTag = instanceTag;

			mutePublishToken = $"{instanceTag}#MUTE";

			tesira.SubscribeToPublishToken(mutePublishToken, HandleMutePublishToken);

			tesira.Connected += Initialize;
		}

		public void SetMuted(bool state)
		{
			tesira.QueueCommand(TesiraCommands.Mute.SetMute(instanceTag, CHANNEL, state), null);
		}

		// private

		private void Initialize(object? _, EventArgs e)
		{
			tesira.QueueCommand(TesiraCommands.Mute.SubscribeMute(instanceTag, CHANNEL, mutePublishToken), null);
		}

		private void HandleMutePublishToken(string value)
		{
			IsMuted = bool.Parse(value);

			// Trigger event
			MutedStateChanged?.Invoke(this, IsMuted);
		}
	}
}
