using System;

namespace KangEngine.Timer.Base
{
    internal class TimerUnit : TimerUnitBase
    {
        public TimerUnit(Action callback, uint time, uint repeatCnt)
        {
            base.listener = callback;
            base.delay = time;
            base.maxRepeatCnt = repeatCnt == 0 ? uint.MaxValue : repeatCnt;
        }

        public override void ExecAction()
        {
            Action listener = (Action)base.listener;
            listener();
        }
    }

    internal class TimerUnit<T> : TimerUnitBase
    {
        private T _arg;
        public TimerUnit(Action<T> callback, uint time, uint repeatCnt, T arg)
        {
            base.listener = callback;
            base.delay = time;
            base.maxRepeatCnt = repeatCnt == 0 ? uint.MaxValue : repeatCnt;
            this._arg = arg;
        }

        public override void ExecAction()
        {
            Action<T> listener = (Action<T>)base.listener;
            listener(this._arg);
        }
    }

    internal class TimerUnit<T1, T2> : TimerUnitBase
    {
        private T1 _arg1;
        private T2 _arg2;
        public TimerUnit(Action<T1, T2> callback, uint time, uint repeatCnt, T1 arg1, T2 arg2)
        {
            base.listener = callback;
            base.delay = time;
            base.maxRepeatCnt = repeatCnt == 0 ? uint.MaxValue : repeatCnt;
            this._arg1 = arg1;
            this._arg2 = arg2;
        }

        public override void ExecAction()
        {
            Action<T1, T2> listener = (Action<T1, T2>)base.listener;
            listener(this._arg1, this._arg2);
        }
    }

    internal class TimerUnit<T1, T2, T3> : TimerUnitBase
    {
        private T1 _arg1;
        private T2 _arg2;
        private T3 _arg3;
        public TimerUnit(Action<T1, T2, T3> callback, uint time, uint repeatCnt, T1 arg1, T2 arg2, T3 arg3)
        {
            base.listener = callback;
            base.delay = time;
            base.maxRepeatCnt = repeatCnt == 0 ? uint.MaxValue : repeatCnt;
            this._arg1 = arg1;
            this._arg2 = arg2;
            this._arg3 = arg3;
        }

        public override void ExecAction()
        {
            Action<T1, T2, T3> listener = (Action<T1, T2, T3>)base.listener;
            listener(this._arg1, this._arg2, this._arg3);
        }
    }
}
