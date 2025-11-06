using Godot;
using System;

public partial class SignalManager : Node
{
	[Signal]
	public delegate void OnScoreUpdatedEventHandler(int points);

	[Signal]
	public delegate void OnScoreUpdateHUDEventHandler();

	[Signal]
	public delegate void OnUpgradeUpdatedEventHandler();

	[Signal]
	public delegate void OnSaveScoreEventHandler();

	public static SignalManager Instance { get; private set; }

	public override void _Ready()
	{
		Instance = this;
	}

	public static void EmitOnScoreUpdated(int points)
	{
		Instance.EmitSignal(SignalName.OnScoreUpdated, points);
	}

	public static void EmitOnScoreUpdateHUD()
    {
		Instance.EmitSignal(SignalName.OnScoreUpdateHUD);
    }

	public static void EmitOnUpgradeUpdated()
	{
		Instance.EmitSignal(SignalName.OnUpgradeUpdated);
	}
	
	public static void EmitOnSaveScore()
    {
		Instance.EmitSignal(SignalName.OnSaveScore);
    }
}
