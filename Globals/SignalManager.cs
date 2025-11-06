using Godot;
using System;

public partial class SignalManager : Node
{
	[Signal]
	public delegate void OnScoreUpdatedEventHandler(int points);

	[Signal]
	public delegate void OnUpgradeUpdatedEventHandler();

	public static SignalManager Instance { get; private set; }

	public override void _Ready()
	{
		Instance = this;
	}

	public static void EmitOnScoreUpdated(int points)
	{
		Instance.EmitSignal(SignalName.OnScoreUpdated, points);
	}
	
	public static void EmitOnUpgradeUpdated()
    {
		Instance.EmitSignal(SignalName.OnUpgradeUpdated);
    }
}
