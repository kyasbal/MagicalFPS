using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMF.Input;
using MMF.Utility;
using SlimDX;

namespace MagicalFPS.Input
{
    class JoystickHandOperationChecker:IHandOperationChecker
    {
        private readonly IJoystickKeyConfig _keyConfig;

        public class DefaultJoystickConfig:IJoystickKeyConfig
        {//TODO デフォルトのキーの位置の決定
            public int MovementJoystickVector { get { return 0; }}
            public int AcceptButtonIndex { get { return 3; } }
            public int CancelButtonIndex { get; private set; }
            public int JumpButtonIndex { get; private set; }
            public int SitButtonIndex { get; private set; }
        }

        private JoyStickStateChecker _joyStickStateChecker;

        public JoystickHandOperationChecker(GameContext context,int index,IJoystickKeyConfig keyConfig=null)
        {
            _keyConfig = keyConfig;
            _joyStickStateChecker = context.DirectInput.getJoyStickChecker(index);
        }

        public Vector2 getMovementVector()
        {
            return _joyStickStateChecker.getJoystick(_keyConfig.MovementJoystickVector);
        }

        public bool isAcceptButtonPressed()
        {
            return _joyStickStateChecker.getButtonState(_keyConfig.AcceptButtonIndex);
        }

        public bool isCancelButtonPressed()
        {
            return _joyStickStateChecker.getButtonState(_keyConfig.CancelButtonIndex);
        }

        public bool isJumpButtonPressed()
        {
            return _joyStickStateChecker.getButtonState(_keyConfig.JumpButtonIndex);
        }

        public bool isSitButtonPressed()
        {
            return _joyStickStateChecker.getButtonState(_keyConfig.SitButtonIndex);
        }
    }
}
