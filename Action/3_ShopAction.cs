using CSharpDamagochi.Interface;
using CSharpDamagochi.Manager;
using CSharpDamagochi.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDamagochi.Action
{
    public class ShopAction : IAction
    {
        private readonly string _actionName = "상점";
        public string ActionName => _actionName;

        public void Exectue()
        {
            Shop shop = new Shop();
            shop.VisitShop();
        }

        public void Show()
        {
            Console.WriteLine("상점을 나왔습니다.");
        }
    }

    public class Shop
    {
        private readonly LocalData _localData = LocalData.Instance;

        public void VisitShop()
        {
            Console.Clear();
            Console.WriteLine("===== 포켓몬 상점에 오신 것을 환영합니다! =====");

            bool isShopping = true;

            while (isShopping)
            {
                Console.Clear();
                Console.WriteLine($"\n현재 소지금: {_localData.money}원");
                Console.WriteLine($"몬스터볼 보유: {_localData.monsterBallCount}개");

                // 회복 아이템 보유량 표시
                Console.WriteLine($"\n[회복 아이템]");
                Console.WriteLine($"빨간포션: {_localData.redPotionCount}개 (체력 50 회복)");
                Console.WriteLine($"고급빨간포션: {_localData.hyperPotionCount}개 (체력 100 회복)");
                Console.WriteLine($"풀회복약: {_localData.fullRestoreCount}개 (전체 체력 회복)");

                if (_localData.selectPoketmon != null)
                {
                    Console.WriteLine($"\n현재 포켓몬: {_localData.selectPoketmon.name}");
                    Console.WriteLine($"체력: {_localData.selectPoketmon.hp}/{_localData.selectPoketmon.maxHp}");
                }

                Console.WriteLine("\n무엇을 구매하시겠습니까?");
                for(int i = 1; i <= PurchaseManager.Instance.purchase.Count; i++)
                {
                    var purchaseItem = PurchaseManager.Instance.purchase[i];
                    Console.WriteLine($"{i}. {purchaseItem.Name}: {purchaseItem.Cost}원 ({purchaseItem.Description})");
                }

                Console.WriteLine($"{PurchaseManager.Instance.purchase.Count+1}. 상점 나가기");

                var choice = Input.SelectNumber();

                var purchase = PurchaseManager.Instance.purchase[choice];
                
                if(purchase.Condition())
                {
                    purchase.Buy();
                }
            }
        }
    }
}