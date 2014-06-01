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
        private JoyStickStateChecker _joyStickStateChecker;

        public JoystickHandOperationChecker(GameContext context,int index)
        {
            _joyStickStateChecker = context.DirectInput.getJoyStickChecker(index);
        }

        public Vector2 getMovementVector()
        {
            if (_joyStickStateChecker != null)
            {
                if (_joyStickStateChecker.getJoystick(0).Length() != 0)
                {
                    Tracer.i("input");
                }
            }
            return _joyStickStateChecker.getJoystick(0);
        }

        public bool isAcceptButtonPressed()
        {
            return _joyStickStateChecker.getButtonState(3);
        }
    }
}
