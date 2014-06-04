using System;
using System.Diagnostics;
using MMF.Model;
using SlimDX;

namespace MagicalFPS.MagicEffect
{
    abstract class EffectBase:DefaultDrawableImpl
    {
        private readonly GameContext _gameContext;
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

        protected EffectBase(GameContext gameContext) : base()
        {
            _gameContext = gameContext;
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

        /// <summary>
        /// 深度バッファを無効にして指定したでりげーとを実行します
        /// </summary>
        /// <param name="act"></param>
        protected void InvokeInDisableDepth(Action act)
        {
            var immediateContext = _gameContext.RenderContext.DeviceManager.Device.ImmediateContext;
            var depth = immediateContext.OutputMerger.GetDepthStencilView();
            immediateContext.OutputMerger.SetTargets(immediateContext.OutputMerger.GetRenderTargets(1));
            act();
            immediateContext.OutputMerger.SetTargets(depth, immediateContext.OutputMerger.GetRenderTargets(1));
        }

        public virtual void ReloadEffectFile()
        {
            
        }
    }
}
