namespace TankGame
{
    public interface IData<T>
    {
        public void Save(T data, string path = null);
        public T Load(string path = null);
    }
}
