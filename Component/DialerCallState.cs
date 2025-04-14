namespace SimpleTesiraLibrary.Component;

public enum DialerCallState
{
	Initializing = 0,
	Idle,
	/// <summary>Outgoing call</summary>
	Dialing,
	/// <summary>Incoming call</summary>
	Ringing,
	/// <summary>Active call</summary>
	Active,
}
