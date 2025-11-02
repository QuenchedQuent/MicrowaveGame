using Godot;
using System;

public partial class Player : CharacterBody2D
{

	[Export] private Sprite2D _playerSprite;

	private float _moveSpeed = 200f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		ProcessInput();
		MoveAndSlide();
	}

	private void ProcessInput()
	{
		Vector2 direction = Vector2.Zero;

		// Input
		if (Input.IsActionPressed("right"))
		{
			direction.X += 1;
			FlipSpriteH(false);
		}
		if (Input.IsActionPressed("left"))
		{
			direction.X -= 1;
			FlipSpriteH(true);
		}
		if (Input.IsActionPressed("down"))
			direction.Y += 1;
		if (Input.IsActionPressed("up"))
			direction.Y -= 1;

		// Normalize so diagonal movement isn’t faster
		if (direction != Vector2.Zero)
			direction = direction.Normalized();

		// Move
		Velocity = direction * _moveSpeed;
	}

	private void FlipSpriteH(bool flip)
	{
		_playerSprite.FlipH = flip;
	}
}
