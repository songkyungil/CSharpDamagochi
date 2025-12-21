using CSharpDamagochi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDamagochi.Interface
{
    public interface IItem
    {
        public EItemType Type { get; }
        public int Amount { get; }
        public string Name { get; }
        public string Description { get; }
    }

    public interface IUseableItem : IItem
    {
        public bool Condition();
        public void Use();
    }

    public interface IItemFactory<T> where T: IItemData
    {
        public IUseableItem Create(T data);
    }

    public interface IItemData
    {

    }
}
