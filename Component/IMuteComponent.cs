namespace SimpleTesiraLibrary.Component;

using System;

public interface IMuteComponent
{
	bool IsMuted { get; }

	event EventHandler<bool> MutedStateChanged;

	void SetMuted(bool state);
}
