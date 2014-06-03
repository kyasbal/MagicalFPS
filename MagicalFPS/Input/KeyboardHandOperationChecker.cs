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
            public virtual Key MoveRight{get { return Key.RightArrow; }}
            public virtual Key MoveLeft{get { return Key.LeftArrow; }}
            public virtual Key MoveTop {get { return Key.UpArrow; }}
            public virtual Key MoveBottom{get{ return Key.DownArrow;}}
            public virtual Key Jump { get{return Key.LeftShift;} }
            public virtual Key Sit { get{return Key.LeftControl;} }
            public virtual Key Cancel { get{return Key.Backspace;} }
            public virtual Key Accept { get{return Key.Return;} }
        }

        public KeyboardHandOperationChecker(DirectInputManager directInputManager,int index,IKeyboardKeyConfig config=null)
        {
            config = config ?? new DefaultKeyConfig();
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


        public bool isAcceptButtonPressed()
        {
            return keyboardChecker.IsPressed(_config.Accept);
        }

        public bool isCancelButtonPressed()
        {
            return keyboardChecker.IsPressed(_config.Cancel);
        }

        public bool isJumpButtonPressed()
        {
            return keyboardChecker.IsPressed(_config.Jump);
        }

        public bool isSitButtonPressed()
        {
            return keyboardChecker.IsPressed(_config.Sit);
        }
    }
}
