using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX;

namespace MagicalFPS.Input
{
    public interface IHandOperationChecker
    {
        Vector2 getMovementVector();
    }
}
