using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMF.Model;
using SlimDX;

namespace MagicalFPS.Effect
{
    abstract class EffectBase:DefaultDrawableImpl
    {
        private static Stopwatch stopWatch;

        static EffectBase()
        {
            stopWatch=new Stopwatch();
            stopWatch.Start();
        }

        protected long startTime;

        protected bool isPlaying;
        protected Vector3 _startDirection;
        protected Vector3 _startPosition;

        public void Start(Vector3 startPosition,Vector3 startDirection)
        {
            _startPosition = startPosition;
            _startDirection = startDirection;
            startTime = stopWatch.ElapsedMilliseconds;
            isPlaying = true;
        }

        protected EffectBase() : base()
        {
        }


        public override int SubsetCount
        {
            get { return 0; }
        }

        public override int VertexCount
        {
            get { return 0; }
        }

        public override void Draw()
        {
            if(isPlaying)
            Draw(stopWatch.ElapsedMilliseconds-startTime);
        }

        protected abstract void Draw(long time);

        public override void Update()
        {
            if(isPlaying)
            Update(stopWatch.ElapsedMilliseconds-startTime);
        }

        protected abstract void Update(long time);

        public override void Dispose()
        {
            
        }
    }
}
