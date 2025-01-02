using System;

namespace SimpleTesiraLibrary.Component
{
	public interface IMuteComponent
	{
		bool IsMuted { get; }

		event EventHandler<bool> MutedStateChanged;

		void SetMuted(bool state);
	}
}
