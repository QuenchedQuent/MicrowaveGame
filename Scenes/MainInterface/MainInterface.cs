using Godot;
using System;

public partial class MainInterface : Control
{
	private const float PASSIVE_INTERVAL = 1f; // 1 second

	[Export]
	private Label _scoreLabel;

	[Export]
	private Label _upgradesLabel;

	private float _passiveTimer = 0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetScoreAndUpgrades();
		SignalManager.Instance.OnScoreUpdateHUD += OnScoreUpdateHUD;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
    {
		if (Input.IsActionJustPressed("quit"))
		{
			SignalManager.EmitOnSaveScore();
			CallDeferred(MethodName.DeferredQuit);
		}
		else if (Input.IsActionJustPressed("reset"))
		{
			ScoreManager.ResetScoreAndUpgrades();
		}

		CalculatePassiveIncome(delta);		
    }

	private void OnScoreUpdateHUD()
	{
		_scoreLabel.Text = ScoreManager.Score.ToString("D4");
		_upgradesLabel.Text = ScoreManager.Upgrades.ToString("D4");
	}

	private void SetScoreAndUpgrades()
	{
		_scoreLabel.Text = ScoreManager.Instance.SavedScore.Score.ToString("D4");
		_upgradesLabel.Text = ScoreManager.Instance.SavedScore.Upgrades.ToString("D4");
	}

	private void DeferredQuit()
	{
		GetTree().Quit();
	}

	private void CalculatePassiveIncome(double delta)
    {
		_passiveTimer += (float)delta;
		if (_passiveTimer >= PASSIVE_INTERVAL)
        {
			_passiveTimer = 0f;
			SignalManager.EmitOnScoreUpdated((int)(ScoreManager.BaseIncome * Math.Pow(1.1, ScoreManager.Upgrades)));
        }
    }
	
}
