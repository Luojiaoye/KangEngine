namespace KangEngine.Core
{
    public class KangSingleTon<T> where T:new()
    {
        private static T _inst;
        protected KangSingleTon(){ }

        public static T Inst()
        {
            if (KangSingleTon<T>._inst == null)
                KangSingleTon<T>._inst = new T();

            return KangSingleTon<T>._inst;
        }
    }
}
