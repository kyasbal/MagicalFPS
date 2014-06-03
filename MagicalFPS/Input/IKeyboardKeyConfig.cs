using SlimDX.DirectInput;

namespace MagicalFPS.Input
{
    public interface IKeyboardKeyConfig
    {
        Key MoveRight { get; }

        Key MoveLeft { get; }

        Key MoveTop { get; }

        Key MoveBottom { get; }

        Key Jump { get; }

        Key Sit { get; }

        Key Cancel { get; }
        
        Key Accept { get; }
    }
}