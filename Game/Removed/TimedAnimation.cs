using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    [Serializable]
    public class TimedAnimation : Animation
    {
        public TimedAnimation(Animation animation, Int32 time) : base(animation)
        {
            RunTime = time;
            _animationTimer = new System.Threading.Timer(new System.Threading.TimerCallback((state) =>
            {
                this.Animating = false;
            }), null, 0, time);
        }

        [System.Runtime.Serialization.OnDeserializedAttribute()]
        private void RunThisMethod(System.Runtime.Serialization.StreamingContext context)
        {
            _animationTimer = new System.Threading.Timer(new System.Threading.TimerCallback((state) =>
            {
                this.Animating = false;
            }), null, 0, RunTime);
        }

        public static TimedAnimation Load(String TimedAnimationFilePath)
        {
            System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.Stream stream = new System.IO.FileStream(TimedAnimationFilePath,
                                      System.IO.FileMode.Open,
                                      System.IO.FileAccess.Read,
                                      System.IO.FileShare.Read);
            TimedAnimation obj = (TimedAnimation)formatter.Deserialize(stream);
            stream.Close();
            return new TimedAnimation(obj, obj.RunTime);
        }

        public Int32 RunTime
        {
            get
            {
                return _runTime;
            }
            set
            {
                _runTime = value;
                _animationTimer = new System.Threading.Timer(new System.Threading.TimerCallback((state) =>
                {
                    this.Animating = false;
                }), null, 0, _runTime);
            }
        }

        private Int32 _runTime = 100;

        [NonSerialized]
        private System.Threading.Timer _animationTimer;
    }
}
