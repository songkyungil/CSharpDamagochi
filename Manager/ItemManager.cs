using CSharpDamagochi.Data;
using CSharpDamagochi.DesignPattern;
using CSharpDamagochi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDamagochi.Manager
{
    public class ItemManager : Singleton<ItemManager>
    {
        public Dictionary<int, IItem> item = new();

        public void Init()
        {
            CreateItem();
        }

        private void CreateItem()
        {
            // 인터페이스 타입
            Type interfaceType = typeof(IItem);

            // 현재 실행 중인 어셈블리에서 인터페이스를 구현하는 모든 클래스 타입 찾기
            List<Type> types = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => interfaceType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToList();

            for (int i = 0; i < types.Count; i++)
            {
                IItem instance = (IItem)Activator.CreateInstance(types[i]);
                item.Add(i + 1, instance);
            }
        }
    }
}
