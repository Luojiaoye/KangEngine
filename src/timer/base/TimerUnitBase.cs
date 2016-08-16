using System;

namespace KangEngine.Timer.Base
{
    internal abstract class TimerUnitBase
    {
        public uint delay;
        public uint repeatCnt;
        public uint maxRepeatCnt;
        public uint startTime;
        public Delegate listener;
        public bool scaled;

        public abstract void ExecAction();

        public uint timeOutTime()
        {
            return this.startTime + this.delay * this.repeatCnt;
        }
    }
}
