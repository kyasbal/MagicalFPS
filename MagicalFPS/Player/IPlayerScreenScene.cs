using MMF.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicalFPS.Player
{
    interface IPlayerScreenScene
    {
        void OnLoad(D2DSpriteBatch batch);

        void RenderSprite(D2DSpriteBatch batch);

        void CheckKeyState();

        bool IsInitialized { get; }
    }
}
