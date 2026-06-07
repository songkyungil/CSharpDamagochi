

namespace CSharpDamagochi.Interface
{
    public interface IAction
    {
        public string ActionName { get; }
        public void Exectue();
        public void Show();
    }
}
