using Godot;

public abstract partial class Creature : AnimatedSprite2D
{
    private byte _direction;
    private byte _lastInput;
    private bool _isWalking;

    public override void _Process(double delta)
    {
        base._Process(delta);

        byte currentInput = 0;

        if (Input.IsKeyPressed(Key.W)) currentInput += 0b0001;
        if (Input.IsKeyPressed(Key.D)) currentInput += 0b0010;
        if (Input.IsKeyPressed(Key.S)) currentInput += 0b0100;
        if (Input.IsKeyPressed(Key.A)) currentInput += 0b1000;

        if (currentInput != _lastInput) UpdateDirection(_lastInput = currentInput);
        if (_isWalking) Position += GetDirectionVector() * 3;

    }

    private void UpdateDirection(byte input)
    {
        switch (input)
        {
            case 0b0000:
                // idle
                PlayIdle();
                break;

            case 0b0001:
            case 0b1011:
                // walk up
                _direction = 0b0001;
                PlayWalk();
                break;

            case 0b0010:
            case 0b0111:
                // walk right
                _direction = 0b0010;
                PlayWalk();
                break;

            case 0b0100:
            case 0b1110:
                // walk down
                _direction = 0b0100;
                PlayWalk();
                break;

            case 0b1000:
            case 0b1101:
                // walk left
                _direction = 0b1000;
                PlayWalk();
                break;

            case 0b0101:
            case 0b1010:
            case 0b1111:
                // no valid input; spin
                PlayIdle();
                break;

            case 0b0011:
            case 0b0110:
            case 0b1100:
            case 0b1001:
                // no valid input; calculate due to last given input
                switch (_direction)
                {
                    case 0b0001:
                    case 0b0010:
                    case 0b0100:
                    case 0b1000:
                        _direction ^= input;
                        PlayWalk();
                        break;
                }
                break;
        }
    }

    private void PlayWalk()
    {
        _isWalking = true;

        SpeedScale = 1;
        if (_direction == 0b0001) Play("up");
        if (_direction == 0b0010) Play("right");
        if (_direction == 0b0100) Play("down");
        if (_direction == 0b1000) Play("left");
    }

    private void PlayIdle()
    {
        _isWalking = false;
        SpeedScale = 0.5f;
    }

    private Vector2 GetDirectionVector()
    {
        if (_direction == 0b0001) return Vector2.Up;
        if (_direction == 0b0010) return Vector2.Right;
        if (_direction == 0b0100) return Vector2.Down;
        if (_direction == 0b1000) return Vector2.Left;
        return Vector2.Zero;
    }

}
