using System.Collections.Generic;
namespace Damon.Tool.Pool {
    public interface IPool<T> {

        T Allocate ();
        bool Recycle (T obj);
    }

    public abstract class Pool<T> : IPool<T> {
        protected Stack<T> mCacheStack = new Stack<T> ();

        protected IObjectFactory<T> mFactory;

        public int count { get { return mCacheStack.Count; } }

        protected int mMaxCount = 5;

        public virtual T Allocate () {
            return count == 0 ? mFactory.Create () : mCacheStack.Pop ();
        }

        public abstract bool Recycle (T obj); //抽象方法

    }

    public interface IObjectFactory<T> {
        T Create ();
    }
}