using System;
using System.Collections.Generic;
using KangEngine.Timer.Base;

namespace KangEngine.Timer
{
    internal class TimerWheel
    {
        public uint lastTimerKey = 0;
        private Dictionary<uint, Dictionary<Delegate, TimerUnitBase>> _wheelDic;

        public TimerWheel()
        {
            this._wheelDic = new Dictionary<uint, Dictionary<Delegate, TimerUnitBase>>(0x186a0);
        }

        public void Add(TimerUnitBase unit)
        {
            Dictionary<Delegate, TimerUnitBase> dictionary = null;
            uint timerKey = unit.timeOutTime() / TimerManager.BUBBLE_CNT;
            if (!this._wheelDic.ContainsKey(timerKey))
            {
                dictionary = new Dictionary<Delegate, TimerUnitBase>();
                this._wheelDic.Add(timerKey, dictionary);
            }
            else
            {
                dictionary = this._wheelDic[timerKey];
            }

            if (!dictionary.ContainsKey(unit.listener))
                dictionary.Add(unit.listener, unit);
        }

        public TimerUnitBase GetTimerUnit(Delegate listener, uint startTime)
        {
            uint key = startTime / TimerManager.BUBBLE_CNT;
            if (!this._wheelDic.ContainsKey(key))
                return null;
            if (!this._wheelDic[key].ContainsKey(listener))
                return null;

            return this._wheelDic[key][listener];
        }

        public void Remove(TimerUnitBase unit)
        {
            uint key = unit.timeOutTime() / TimerManager.BUBBLE_CNT;
            if (this._wheelDic.ContainsKey(key) && this._wheelDic[key].ContainsKey(unit.listener))
                this._wheelDic[key].Remove(unit.listener);
        }

        public void ExecAction(uint timerKey)
        {
            if (!this._wheelDic.ContainsKey(timerKey))
                return;
            Dictionary<Delegate, TimerUnitBase> dictionary = this._wheelDic[timerKey];
            foreach (KeyValuePair<Delegate, TimerUnitBase> pair in dictionary)
            {
                if (pair.Key == null)
                    continue;

                pair.Value.ExecAction();
                pair.Value.repeatCnt++;
            }

            this._wheelDic.Remove(timerKey);
            foreach (KeyValuePair<Delegate, TimerUnitBase> pair in dictionary)
            {
                if (pair.Key != null && pair.Value.repeatCnt < pair.Value.maxRepeatCnt)
                    this.Add(pair.Value);
            }
        }
    }
}
