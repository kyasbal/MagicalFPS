using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicalFPS.Input
{
    interface IJoystickKeyConfig
    {
        int MovementJoystickVector { get; }

        int AcceptButtonIndex { get; }

        int CancelButtonIndex { get; }

        int JumpButtonIndex { get; }

        int SitButtonIndex { get; }
    }
}
