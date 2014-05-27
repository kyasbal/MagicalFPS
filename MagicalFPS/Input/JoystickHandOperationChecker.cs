using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMF.Input;
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
            return _joyStickStateChecker.getJoystick(0);
        }
    }
}
