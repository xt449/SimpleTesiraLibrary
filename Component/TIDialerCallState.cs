namespace SimpleTesiraLibrary.Component
{
	public enum TIDialerCallState
	{
		Unset = 0,
		/// <summary>
		/// The analog line is on hook and ready to make a call
		/// </summary>
		Idle = 1,
		/// <summary>
		/// A number has been entered in the STC card and it is currently dialing.
		/// </summary>
		Dialing = 2,
		/// <summary>
		/// The far end is ringing
		/// </summary>
		Ringback = 3,
		/// <summary>
		/// The far end has presented a busy indication
		/// </summary>
		BusyTone = 4,
		/// <summary>
		/// The STC card has received an error tone on the line
		/// </summary>
		ErrorTone = 5,
		/// <summary>
		/// The call to the far end has been connected
		/// </summary>
		Connected = 6,
		/// <summary>
		/// A STC card has detected an incoming call
		/// </summary>
		Ringing = 7,
		/// <summary>
		/// The far end has hung up the call
		/// </summary>
		Dropped = 8,
		/// <summary>
		/// The card is booting
		/// </summary>
		Init = 12,
		/// <summary>
		/// A fault has been detected on the phone line (reference the prompt field for more information)
		/// </summary>
		StateFault = 13,
		/// <summary>
		/// A call has been connected but the SVC receive block mute has been engaged
		/// </summary>
		ConnectedMuted = 14,
	}
}
