using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMF.Input;
using MMF.Utility;
using SlimDX;
using SlimDX.DirectInput;

namespace MagicalFPS.Input
{
    public class KeyboardHandOperationChecker:IHandOperationChecker
    {
        private IKeyboardKeyConfig _config;
        private KeyboardStateChecker keyboardChecker;

        public class DefaultKeyConfig:IKeyboardKeyConfig
        {
            public Key MoveRight
            {
                get { return Key.RightArrow; }
            }

            public Key MoveLeft
            {
                get { return Key.LeftArrow; }
            }

            public Key MoveTop {get { return Key.UpArrow; }}

            public Key MoveBottom
            {
                get { return Key.DownArrow; }
            }
        }

        public KeyboardHandOperationChecker(DirectInputManager directInputManager,int index,IKeyboardKeyConfig config=null)
        {
            if (config == null)
            {
                config=new DefaultKeyConfig();
            }
            _config = config;
            keyboardChecker = directInputManager.getKeyboardChecker(index);
        }

        public Vector2 getMovementVector()
        {
            Vector2 retValue = Vector2.Zero;
            if(keyboardChecker.IsPressed(_config.MoveRight))retValue+=new Vector2(1000,0);
            if (keyboardChecker.IsPressed(_config.MoveLeft)) retValue += new Vector2(-1000, 0);
            if (keyboardChecker.IsPressed(_config.MoveTop)) retValue += new Vector2(0,-1000);
            if (keyboardChecker.IsPressed(_config.MoveBottom)) retValue += new Vector2(0, 1000);
            return retValue;
        }
    }

    public interface IKeyboardKeyConfig
    {
        Key MoveRight { get; }

        Key MoveLeft { get; }

        Key MoveTop { get; }

        Key MoveBottom { get; }
    }
}
