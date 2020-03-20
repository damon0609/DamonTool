using Damon.Tool.Pool;
using UnityEngine;
public class ZTestPool : MonoBehaviour
{
    SimpleObjectPool<Fish> pool;
    private int num = 5;
    void Start()
    {
        pool = new SimpleObjectPool<Fish>(Create, (Fish f)=> {
            f.id = 0;
        }, num);

        for (int i = 0; i < pool.count; i++)
        {
            Fish f = pool.Allocate();
            Debug.Log(f.id);
        }
    }

    Fish Create()
    {
        Fish fish = null;
        for (int i = 0; i < num; i++)
        {
            fish = new Fish(i);
        }
        return fish;
    }

    public class Fish
    {
        public int id;
        public Fish(int num)
        {
            this.id = num;
        }
    }
}
