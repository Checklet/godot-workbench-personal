using Godot;
using System;

public partial class Zoroark : AnimatedSprite2D
{
    public string CharacterName { get; init; } = "Zoroark";

    public override void _Process(double delta)
    {
        base._Process(delta);

        byte currentInput = 0;

        if (Input.IsKeyPressed(Key.W)) currentInput += 0b0001;
        if (Input.IsKeyPressed(Key.D)) currentInput += 0b0010;
        if (Input.IsKeyPressed(Key.S)) currentInput += 0b0100;
        if (Input.IsKeyPressed(Key.A)) currentInput += 0b1000;

        // TODO - Develop algorithm for translating inputs into valid directions for walking.
    }
}