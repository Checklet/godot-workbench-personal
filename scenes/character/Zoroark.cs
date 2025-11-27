using Godot;

public partial class Zoroark : AnimatedSprite2D
{
    public string CharacterName = "Zoroark";
    public byte Direction = 0;

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

        if (currentInput != _lastInput)
        {
            switch (currentInput)
            {
                case 0b0000:
                    // idle
                    PlayIdle(false);
                    break;

                case 0b0001:
                case 0b1011:
                    // walk up
                    Direction = 0b0001;
                    PlayWalk();
                    break;

                case 0b0010:
                case 0b0111:
                    // walk right
                    Direction = 0b0010;
                    PlayWalk();
                    break;

                case 0b0100:
                case 0b1110:
                    // walk down
                    Direction = 0b0100;
                    PlayWalk();
                    break;

                case 0b1000:
                case 0b1101:
                    // walk left
                    Direction = 0b1000;
                    PlayWalk();
                    break;

                case 0b0101:
                case 0b1010:
                case 0b1111:
                    // no valid input; spin
                    PlayIdle(true);
                    break;

                case 0b0011:
                case 0b0110:
                case 0b1100:
                case 0b1001:
                    // no valid input; calculate due to last given input
                    switch (Direction)
                    {
                        case 0b0001:
                        case 0b0010:
                        case 0b0100:
                        case 0b1000:
                            Direction ^= currentInput;
                            PlayWalk();
                            break;

                        default:
                            PlayIdle(true);
                            break;
                    }
                    break;

                default:
                    currentInput = 0;
                    break;
            }

            _lastInput = currentInput;
        }

        if (_isWalking) Position += GetDirectionVector() * 3;
    }

    private void PlayIdle(bool isSpinning)
    {
        _isWalking = false;

        if (isSpinning)
        {
            Play("spin");
            return;
        }

        if (Direction == 0b0001) Play("idle-up");
        if (Direction == 0b0010) Play("idle-right");
        if (Direction == 0b0100) Play("idle-down");
        if (Direction == 0b1000) Play("idle-left");
    }

    private void PlayWalk()
    {
        _isWalking = true;

        if (Direction == 0b0001) Play("walk-up");
        if (Direction == 0b0010) Play("walk-right");
        if (Direction == 0b0100) Play("walk-down");
        if (Direction == 0b1000) Play("walk-left");
    }

    private Vector2 GetDirectionVector()
    {
        if (Direction == 0b0001) return Vector2.Up;
        if (Direction == 0b0010) return Vector2.Right;
        if (Direction == 0b0100) return Vector2.Down;
        if (Direction == 0b1000) return Vector2.Left;
        return Vector2.Zero;
    }
}