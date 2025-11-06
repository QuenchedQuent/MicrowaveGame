using Godot;
using System;

public partial class PassiveUpgrade : Button
{
	[Export] private Label _upgradeLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Disabled = true;

		Pressed += OnPressed;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
    {
		ShouldButtonBeActive();
    }
	
	private void ShouldButtonBeActive()
	{
		if (ScoreManager.Score >= 10)
		{
			Disabled = false;
		}
		else
        {
			Disabled = true;
        }
    }

	private void OnPressed()
	{
		SignalManager.EmitOnScoreUpdated(-10);
		SignalManager.EmitOnUpgradeUpdated();

		_upgradeLabel.Text = ScoreManager.Upgrades.ToString("D4");
	}
}
