namespace SimpleTesiraLibrary.Component;

public interface IVoIPControlStatusComponent
{
	string LastNumber { get; }

	/// <param name="line">[1-2]</param>
	/// <param name="callAppearance">[1-6]</param>
	void Dial(uint line, uint callAppearance, string number);

	/// <param name="line">[1-2]</param>
	/// <param name="callAppearance">[1-6]</param>
	void Answer(uint line, uint callAppearance);

	/// <param name="line">[1-2]</param>
	/// <param name="callAppearance">[1-6]</param>
	void End(uint line, uint callAppearance);

	/// <param name="callAppearance">[1-6]</param>
	/// <param name="digit">[0-9*#]</param>
	void DTMF(uint line, char digit);
}
