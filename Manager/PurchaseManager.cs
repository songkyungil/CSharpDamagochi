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
    public class PurchaseManager : Singleton<PurchaseManager>
    {
        public Dictionary<int, IPurchase> purchase = new();

        public void Init()
        {
            CreatePurchase();
        }

        private void CreatePurchase()
        {
            // 인터페이스 타입
            Type interfaceType = typeof(IPurchase);

            // 현재 실행 중인 어셈블리에서 인터페이스를 구현하는 모든 클래스 타입 찾기
            List<Type> types = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => interfaceType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToList();

            for (int i = 0; i < types.Count; i++)
            {
                IPurchase instance = (IPurchase)Activator.CreateInstance(types[i]);
                purchase.Add(i + 1, instance);
            }
        }
    }
}
