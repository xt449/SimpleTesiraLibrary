using System;

namespace SimpleTesiraLibrary;

/// <summary>
/// https://support.biamp.com/Tesira/Control/Tesira_command_string_calculator
/// </summary>
public static class TesiraCommands
{
	public static string GetVersion() => $"DEVICE get version";

	public static class Preset
	{
		public static string RecallPresetByName(string presetName) => $"DEVICE recallPresetByName \"{presetName}\"";
	}

	public static class Mute
	{
		public static string GetMute(string instanceTag, uint channel) => $"\"{instanceTag}\" get mute {channel}";
		public static string SetMute(string instanceTag, uint channel, bool value) => $"\"{instanceTag}\" set mute {channel} {(value ? "true" : "false")}";
		public static string ToggleMute(string instanceTag, uint channel) => $"\"{instanceTag}\" toggle mute {channel}";
		public static string SubscribeMute(string instanceTag, uint channel, string subscriptionToken) => $"\"{instanceTag}\" subscribe mute {channel} \"{subscriptionToken}\"";
	}

	public static class Level
	{
		public static string GetLevel(string instanceTag, uint channel) => $"\"{instanceTag}\" get level {channel}";
		public static string SetLevel(string instanceTag, uint channel, float value) => $"\"{instanceTag}\" set level {channel} {value}";
		public static string SubscribeLevel(string instanceTag, uint channel, string subscriptionToken) => $"\"{instanceTag}\" subscribe level {channel} \"{subscriptionToken}\"";

		public static string GetMinLevel(string instanceTag, uint channel) => $"\"{instanceTag}\" get minLevel {channel}";

		public static string GetMaxLevel(string instanceTag, uint channel) => $"\"{instanceTag}\" get maxLevel {channel}";
	}

	[Obsolete("Unclear documentation")]
	public static class Dialer
	{
		public static string Dial(string instanceTag, uint line, uint callAppearance, string number) => $"\"{instanceTag}\" dial {line} {callAppearance} {number}";
		public static string Answer(string instanceTag, uint line, uint callAppearance) => $"\"{instanceTag}\" answer {line} {callAppearance}";
		public static string End(string instanceTag, uint line, uint callAppearance) => $"\"{instanceTag}\" end {line} {callAppearance}";
		public static string DTMF(string instanceTag, uint line, string digit) => $"\"{instanceTag} dtmf {line} {digit}\"";
		[Obsolete("Incomplete API")]
		public static string SubscribeCallState(string instanceTag, string subscriptionToken) => $"\"{instanceTag}\" subscribe callState \"{subscriptionToken}\"";
	}

	public static class TIControlStatus
	{
		public static string Dial(string instanceTag, string number) => $"\"{instanceTag}\" dial {number}";
		public static string Answer(string instanceTag) => $"\"{instanceTag}\" answer";
		public static string End(string instanceTag) => $"\"{instanceTag}\" end";
		/// <param name="digit">[0-9*#]</param>
		public static string DTMF(string instanceTag, char digit) => $"\"{instanceTag} dtmf {digit}\"";

		public static string SubscribeRinging(string instanceTag, string subscriptionToken) => $"\"{instanceTag}\" subscribe ringing \"{subscriptionToken}\"";
		public static string SubscribeDialing(string instanceTag, string subscriptionToken) => $"\"{instanceTag}\" subscribe dialing \"{subscriptionToken}\"";
		public static string SubscribeCallState(string instanceTag, string subscriptionToken) => $"\"{instanceTag}\" subscribe callState \"{subscriptionToken}\"";
	}

	public static class VoIPControlStatus
	{
		/// <param name="line">[1-2]</param>
		/// <param name="callAppearance">[1-6]</param>
		public static string Dial(string instanceTag, uint line, uint callAppearance, string number) => $"\"{instanceTag}\" dial {line} {callAppearance} {number}";
		/// <param name="line">[1-2]</param>
		/// <param name="callAppearance">[1-6]</param>
		public static string Answer(string instanceTag, uint line, uint callAppearance) => $"\"{instanceTag}\" answer {line} {callAppearance}";
		/// <param name="line">[1-2]</param>
		/// <param name="callAppearance">[1-6]</param>
		public static string End(string instanceTag, uint line, uint callAppearance) => $"\"{instanceTag}\" end {line} {callAppearance}";
		/// <param name="line">[1-2]</param>
		/// <param name="digit">[0-9*#]</param>
		public static string DTMF(string instanceTag, uint line, char digit) => $"\"{instanceTag} dtmf {line} {digit}\"";

		/// <param name="line">[1-2]</param>
		/// <param name="callAppearance">[1-6]</param>
		public static string SubscribeRinging(string instanceTag, uint line, uint callAppearance, string subscriptionToken) => $"\"{instanceTag}\" subscribe ringing {line} {callAppearance} \"{subscriptionToken}\"";
		public static string SubscribeCallState(string instanceTag, string subscriptionToken) => $"\"{instanceTag}\" subscribe callState \"{subscriptionToken}\"";
	}
}
