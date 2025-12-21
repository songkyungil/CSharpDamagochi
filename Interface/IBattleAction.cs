using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
