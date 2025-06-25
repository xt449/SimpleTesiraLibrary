using System;

namespace SimpleTesiraLibrary.Component;

public interface ILevelComponent : IMuteComponent
{
	float Level { get; }

	float LevelMin { get; }
	float LevelMax { get; }

	event EventHandler<float> LevelChanged;

	void SetLevel(float level);

	virtual void SetLevelRatio(float levelRatio)
	{
		SetLevel((levelRatio * (LevelMax - LevelMin)) + LevelMin);
	}
}
