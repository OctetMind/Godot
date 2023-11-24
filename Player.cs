using Godot;
using System;

public partial class Player : Area2D
{
	/// <summary>
	/// Speed in pixels/sec
	/// </summary>
	[Export]
	public int Speed { get; set; } = 400;

	/// <summary>
	/// Signal for hitting an eniemy
	/// </summary>
	[Signal]
	public delegate void HitEventHandler();

	/// <summary>
	/// Screen size of the game window
	/// </summary>
	public Vector2 ScreenSize;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// charater movement on screen
		var velocity = Vector2.Zero;

		if (Input.IsActionPressed(CONSTANTS.MOVEMENT_KEYS.MOVE_RIGHT))
		{
			velocity.X += 1;
		}
		if (Input.IsActionPressed(CONSTANTS.MOVEMENT_KEYS.MOVE_LEFT))
		{
			velocity.X += -1;
		}
		if (Input.IsActionPressed(CONSTANTS.MOVEMENT_KEYS.MOVE_UP))
		{
			velocity.Y += -1;
		}
		if (Input.IsActionPressed(CONSTANTS.MOVEMENT_KEYS.MOVE_DOWN))
		{
			velocity.Y += 1;
		}

		var animatedSprite2D = GetNode<AnimatedSprite2D>(CONSTANTS.PLAYER_NODES.ANIMATEDSPRITE2D);

		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			animatedSprite2D.Play();
		}
		else
		{
			animatedSprite2D.Stop();
		}

		Position += velocity * (float)delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		);

		if (velocity.X != 0) {
			animatedSprite2D.Animation = CONSTANTS.ANIMATION.WALK;
			animatedSprite2D.FlipV = false;
			animatedSprite2D.FlipH = velocity.X < 0;
		}
		else if (velocity.Y != 0)
		{
			animatedSprite2D.Animation = CONSTANTS.ANIMATION.UP;
			animatedSprite2D.FlipV = velocity.Y > 0;
		}
	}
}
