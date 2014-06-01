using MMF.Sprite;
using MMF.Sprite.D2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MagicalFPS.Player
{
    public class PlayerDescription
    {
        public PlayerDescription(D2DSpriteBatch batch,string charaName,string s1,string s2,string s3,string img)
        {
            charactorName = charaName;
            skillName_1 = s1;
            skillName_2 = s2;
            skillName_3 = s3;
            image =  batch.CreateBitmap(@img);
        }

        public string charactorName;
        public string skillName_1;
        public string skillName_3;
        public string skillName_2;
        public SlimDX.Direct2D.Bitmap image;
    }
}
