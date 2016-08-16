using System;
using System.Collections.Generic;
using KangEngine.Core;

namespace KangEngine.Event
{
    public class EventManager : KangSingleTon<EventManager>
    {
        private Dictionary<object, Delegate> _evtDic;

        public EventManager()
        {
            _evtDic = new Dictionary<object, Delegate>();
        }

        public void DispatchEvent(object type)
        {
            Delegate handler = null;
            uint key = Convert.ToUInt32(type);
            _evtDic.TryGetValue(key, out handler);
            Action action = handler as Action;
            if (handler != null)
                action();
        }

        public void DispatchEvent<T>(object type, T arg)
        {
            Delegate handler = null;
            uint key = Convert.ToUInt32(type);
            _evtDic.TryGetValue(key, out handler);
            Action<T> action = handler as Action<T>;
            if (action != null)
                action(arg);
        }

        public void DispatchEvent<T1, T2>(object type, T1 arg1, T2 arg2)
        {
            Delegate handler = null;
            uint key = Convert.ToUInt32(type);
            _evtDic.TryGetValue(key, out handler);
            Action<T1, T2> action = handler as Action<T1, T2>;
            if (action != null)
                action(arg1, arg2);
        }

        public void DispatchEvent<T1, T2, T3>(object type, T1 arg1, T2 arg2, T3 arg3)
        {
            Delegate handler = null;
            uint key = Convert.ToUInt32(type);
            _evtDic.TryGetValue(key, out handler);
            Action<T1, T2, T3> action = handler as Action<T1, T2, T3>;
            if (action != null)
                action(arg1, arg2, arg3);
        }

        public void DispatchEvent<T1, T2, T3, T4>(object type, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            Delegate handler = null;
            uint key = Convert.ToUInt32(type);
            _evtDic.TryGetValue(key, out handler);
            Action<T1, T2, T3, T4> action = handler as Action<T1, T2, T3, T4>;
            if (action != null)
                action(arg1, arg2, arg3, arg4);
        }

        public void AddEventListener(object type, Action handler)
        {
            uint key = Convert.ToUInt32(type);
            if (!this._evtDic.ContainsKey(key))
                this._evtDic.Add(key, null);
            this._evtDic[key] = (Action)Delegate.Combine((Action)this._evtDic[key], handler);
        }

        public void AddEventListener<T>(object type, Action<T> handler)
        {
            uint key = Convert.ToUInt32(type);
            if (!this._evtDic.ContainsKey(key))
                this._evtDic.Add(key, null);
            this._evtDic[key] = (Action<T>)Delegate.Combine((Action<T>)this._evtDic[key], handler);
        }

        public void AddEventListener<T1, T2>(object type, Action<T1, T2> handler)
        {
            uint key = Convert.ToUInt32(type);
            if (!this._evtDic.ContainsKey(key))
                this._evtDic.Add(key, null);
            this._evtDic[key] = (Action<T1, T2>)Delegate.Combine((Action<T1, T2>)this._evtDic[key], handler);
        }

        public void AddEventListener<T1, T2, T3>(object type, Action<T1, T2, T3> handler)
        {
            uint key = Convert.ToUInt32(type);
            if (!this._evtDic.ContainsKey(key))
                this._evtDic.Add(key, null);
            this._evtDic[key] = (Action<T1, T2, T3>)Delegate.Combine((Action<T1, T2, T3>)this._evtDic[key], handler);
        }

        public void AddEventListener<T1, T2, T3, T4>(object type, Action<T1, T2, T3, T4> handler)
        {
            uint key = Convert.ToUInt32(type);
            if (!this._evtDic.ContainsKey(key))
                this._evtDic.Add(key, null);
            this._evtDic[key] = (Action<T1, T2, T3, T4>)Delegate.Combine((Action<T1, T2, T3, T4>)this._evtDic[key], handler);
        }

        public void RemoveEventListener(object type, Action handler)
        {
            uint key = Convert.ToUInt32(type);
            if (!this._evtDic.ContainsKey(key))
                return;

            this._evtDic[key] = (Action)Delegate.Remove((Action)this._evtDic[key], handler);
        }

        public void RemoveEventListener<T>(object type, Action<T> handler)
        {
            uint key = Convert.ToUInt32(type);
            if (!this._evtDic.ContainsKey(key))
                return;

            this._evtDic[key] = (Action<T>)Delegate.Remove((Action<T>)this._evtDic[key], handler);
        }

        public void RemoveEventListener<T1, T2>(object type, Action<T1, T2> handler)
        {
            uint key = Convert.ToUInt32(type);
            if (!this._evtDic.ContainsKey(key))
                return;

            this._evtDic[key] = (Action<T1, T2>)Delegate.Remove((Action<T1, T2>)this._evtDic[key], handler);
        }

        public void RemoveEventListener<T1, T2, T3>(object type, Action<T1, T2, T3> handler)
        {
            uint key = Convert.ToUInt32(type);
            if (!this._evtDic.ContainsKey(key))
                return;

            this._evtDic[key] = (Action<T1, T2, T3>)Delegate.Remove((Action<T1, T2, T3>)this._evtDic[key], handler);
        }

        public void RemoveEventListener<T1, T2, T3, T4>(object type, Action handler)
        {
            uint key = Convert.ToUInt32(type);
            if (!this._evtDic.ContainsKey(key))
                return;

            this._evtDic[key] = (Action<T1, T2, T3, T4>)Delegate.Remove((Action<T1, T2, T3, T4>)this._evtDic[key], handler);
        }
    }
}
