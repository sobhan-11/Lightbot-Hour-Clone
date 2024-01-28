namespace Utility
{
    public interface IPooledObject
    {
        void SetPool(ObjectPool _pool);
        void BackToPool();
    }
}