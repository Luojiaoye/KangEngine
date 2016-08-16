using System;
using UnityEngine;
using KangEngine.Core;
using KangEngine.Timer.Base;

namespace KangEngine.Timer
{
    public class TimerManager : KangSingleTon<TimerManager>
    {
        internal static uint BUBBLE_CNT = 0x20;
        private TimerWheel _scaledTimerWheel;
        private TimerWheel _timerWheel;

        public TimerManager()
        {
            this._scaledTimerWheel = new TimerWheel();
            this._timerWheel = new TimerWheel();
        }

        public object AddListener(Action listener, uint delay, uint repeat, bool scaled = false)
        {
            TimerUnit unit = new TimerUnit(listener, delay, repeat);
            unit.startTime = GetCurTime() + delay;
            unit.scaled = scaled;

            if (scaled)
                this._scaledTimerWheel.Add(unit);
            else
                this._timerWheel.Add(unit);

            return unit;
        }

        public object AddListener<T>(Action<T> listener, uint delay, uint repeat, T arg, bool scaled = false)
        {
            TimerUnit<T> unit = new TimerUnit<T>(listener, delay, repeat, arg);
            unit.startTime = GetCurTime() + delay;
            unit.scaled = scaled;

            if (scaled)
                this._scaledTimerWheel.Add(unit);
            else
                this._timerWheel.Add(unit);

            return unit;
        }

        public object AddListener<T1, T2>(Action<T1, T2> listener, uint delay, uint repeat, T1 arg1, T2 arg2, bool scaled = false)
        {
            TimerUnit<T1, T2> unit = new TimerUnit<T1, T2>(listener, delay, repeat, arg1, arg2);
            unit.startTime = GetCurTime() + delay;
            unit.scaled = scaled;

            if (scaled)
                this._scaledTimerWheel.Add(unit);
            else
                this._timerWheel.Add(unit);

            return unit;
        }

        public object AddListener<T1, T2, T3>(Action<T1, T2, T3> listener, uint delay, uint repeat, T1 arg1, T2 arg2, T3 arg3, bool scaled = false)
        {
            TimerUnit<T1, T2, T3> unit = new TimerUnit<T1, T2, T3>(listener, delay, repeat, arg1, arg2, arg3);
            unit.startTime = GetCurTime() + delay;
            unit.scaled = scaled;

            if (scaled)
                this._scaledTimerWheel.Add(unit);
            else
                this._timerWheel.Add(unit);

            return unit;
        }

        public void RemoveListener(object unit)
        {
            TimerUnitBase timer = unit as TimerUnitBase;
            if (timer.scaled)
                this._scaledTimerWheel.Remove(timer);
            else
                this._timerWheel.Remove(timer);
        }

        private uint GetCurTime(bool scaled = false)
        {
            if (scaled)
                return (uint)(Time.time * 1000f);
            else
                return (uint)(Time.unscaledTime * 1000f); 
        }

        public void Update()
        {
            uint idx;
            uint curCnt = this.GetCurTime() / TimerManager.BUBBLE_CNT;
            for (idx = this._timerWheel.lastTimerKey; idx < curCnt; ++idx)
            {
                this._timerWheel.ExecAction(idx);
                this._timerWheel.lastTimerKey = idx;
            }

            curCnt = this.GetCurTime(true)/TimerManager.BUBBLE_CNT;
            for (idx=this._scaledTimerWheel.lastTimerKey; idx < curCnt; ++idx)
            {
                this._scaledTimerWheel.ExecAction(idx);
                this._scaledTimerWheel.lastTimerKey = idx;
            }
        }
    }
}
