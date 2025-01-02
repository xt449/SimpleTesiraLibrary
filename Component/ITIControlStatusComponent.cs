namespace SimpleTesiraLibrary.Component
{
	public interface ITIControlStatusComponent
	{
		string LastNumber { get; }

		void Dial(string number);

		void Answer();

		void End();

		/// <param name="digit">[0-9*#]</param>
		void DTMF(char digit);
	}
}
