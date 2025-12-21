using static System.Net.WebRequestMethods;

namespace CSharpDamagochi.DesignPattern
{
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {
        private static T _instance;

        public Singleton() { }

        public static T Instance 
        {
            get 
            {
                _instance ??= new T();
                return _instance;
            }
        }
    }
}
