using System;
namespace Damon.Tool.Pool
{
    public class GameObjectFactory<T> : IObjectFactory<T>
    {
        private Func<T> mFactory;
        public GameObjectFactory(Func<T> fun)
        {
            this.mFactory = fun;
        }

        public T Create()
        {
            return mFactory();
        }
    }


    public class SimpleObjectPool<T> : Pool<T>
    {
        private Action<T> mReset;

        public SimpleObjectPool(Func<T> fun, Action<T> rest = null, int count = 0)
        {
            mFactory = new GameObjectFactory<T>(fun);
            for (int i = 0; i < count; i++)
            {
                if (mCacheStack != null && mFactory != null)
                {
                    mCacheStack.Push(mFactory.Create());
                }
            }
        }

        public override bool Recycle(T obj)
        {
            mReset.GetInvocationList();
            mCacheStack.Push(obj);
            return true;
        }
    }
}
