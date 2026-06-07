

namespace CSharpDamagochi.Interface
{
    public interface IBattleAction
    {
        public string Name { get; }
        public void Action();
    }


    public interface IBattleActionFactory<T>
    {
        public IBattleAction Create(T data);
    }

    public interface IBattleActionData
    {

    }
}
