using Godot;
using System;

public partial class MicrowaveButton : TextureButton
{
	[Export] private Label _scoreLabel;

	private int _score = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
		Pressed += OnButtonPressed;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

    private void OnButtonPressed()
    {
		GD.Print("Clicked!");

		_score++;

		_scoreLabel.Text = _score.ToString();
    }
}
