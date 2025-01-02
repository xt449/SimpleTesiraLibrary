namespace SimpleTesiraLibrary.Component
{
	public enum VoIPDialerCallState
	{
		Unset = 0,
		/// <summary>
		/// The call appearance is initializing indicating general setup is in place; DHCP in progress, registration is taking place, etc.
		/// This can also indicate that the line has not been configured.
		/// The SVC-2 card will not be able to dial when this state is displayed.
		/// </summary>
		Init = 1,
		/// <summary>
		/// General Fault condition; Network link is down, IP address conflict in place.
		/// The SVC-2 card will not be able to dial when this state is displayed.
		/// </summary>
		Fault = 2,
		/// <summary>
		/// Call Appearance is part of a registered connection to a Proxy Server and is ready to make or receive a call. 
		/// </summary>
		Idle = 3,
		/// <summary>
		/// Call appearance is off hook and dial tone is present.
		/// </summary>
		Dialtone = 4,
		/// <summary>
		/// User has started dialing numbers but has yet to hit send
		/// </summary>
		Silent = 5,
		/// <summary>
		/// User has hit send on the call appearance and the card has sent an INVITE to the proxy or the called party.
		/// No response has been received at this point. 
		/// </summary>
		Dialing = 6,
		/// <summary>
		/// The far end is ringing
		/// </summary>
		Ringback = 7,
		/// <summary>
		/// The call appearance has an incoming call
		/// </summary>
		Ringing = 8,
		/// <summary>
		/// The call has been answered but the call isn't active yet
		/// </summary>
		AnswerCall = 9,
		/// <summary>
		/// The far end is busy
		/// </summary>
		Busy = 10,
		/// <summary>
		/// User has rejected the incoming call
		/// </summary>
		Reject = 11,
		/// <summary>
		/// The user has dialed an invalid number on this call appearance
		/// </summary>
		InvalidNumber = 12,
		/// <summary>
		/// A call has been connected to the call appearance
		/// </summary>
		Active = 13,
		/// <summary>
		/// A call is established but audio is muted in the VoIP Receive block
		/// </summary>
		ActiveMuted = 14,
		/// <summary>
		/// The near end has placed the call appearance on hold
		/// </summary>
		OnHold = 15,
		/// <summary>
		/// The call appearance has received a call waiting indication
		/// </summary>
		WaitingRing = 16,
		/// <summary>
		/// The call appearance has been placed in a local conference
		/// </summary>
		ConfActive = 17,
		/// <summary>
		/// The call appearance is part of a local conference that has been placed on hold
		/// </summary>
		ConfHold = 18,
		/// <summary>
		/// The call appearance is initializing
		/// </summary>
		TransferInit = 19,
		/// <summary>
		/// The call appearance is silent
		/// </summary>
		TransferSilence = 20,
		/// <summary>
		/// The call appearance is awaiting number to be dialed
		/// </summary>
		TransferReqDialing = 21,
		/// <summary>
		/// The call appearance is in a process of transferring
		/// </summary>
		TransferProcess = 22,
		/// <summary>
		/// The call appearance is updating the transfer process
		/// </summary>
		TransferReplacesProcess = 23,
		/// <summary>
		/// The call appearance transfer is active
		/// </summary>
		TransferActive = 24,
		/// <summary>
		/// The call appearance is seeing DTMF tones from the proxy server
		/// </summary>
		TransferRingback = 25,
		/// <summary>
		/// The call appearance is on hold
		/// </summary>
		TransferOnHold = 26,
		/// <summary>
		/// The call appearance is awaiting confirmation to transfer
		/// </summary>
		TransferDecision = 27,
		/// <summary>
		/// The call appearance has experienced an error initializing the transfer process
		/// </summary>
		TransferInitError = 28,
		/// <summary>
		/// The call appearance is waiting
		/// </summary>
		TransferWait = 29,
	}
}
