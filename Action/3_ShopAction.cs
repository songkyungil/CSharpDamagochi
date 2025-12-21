using CSharpDamagochi.Interface;
using CSharpDamagochi.Manager;
using CSharpDamagochi.Table;
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
       
        private readonly LocalData _localData = LocalData.Instance; // 로컬데이터에서 필요한 데이터들 인스턴스로 생성함.

        public void VisitShop()
        {
            

            Console.Clear();
            Console.WriteLine("===== 포켓몬 상점에 오신 것을 환영합니다! =====");

            bool isShopping = true;

            while (isShopping)
            {
                Console.Clear();
                Console.WriteLine($"\n현재 소지금: {_localData.inventory.mygold}원");
                Console.WriteLine($"몬스터볼 보유: {_localData.inventory.Items[3].itemcount}개"); //인벤토리 3번째에 몬스터볼 정보 들어가있음

                // 회복 아이템 보유량 표시
                Console.WriteLine($"\n[회복 아이템]");
                Console.WriteLine($"빨간포션: {_localData.inventory.Items[1].itemcount}개 (체력 50 회복)");
                Console.WriteLine($"고급빨간포션: {_localData.inventory.Items[2].itemcount}개 (체력 100 회복)");
                Console.WriteLine($"풀회복약: {_localData.inventory.Items[3].itemcount}개 (전체 체력 회복)");

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

                // 7번을 누르니 key 값이 존재하지 않다고 해서 해당 구간 탈출처리해줌
                if (choice == PurchaseManager.Instance.purchase.Count + 1)
                {
                    isShopping = false;
                    return;
                }
                // 딕셔너리배열 순서에 맞게 클래스들이 생성된다. -> 숫자입력 받아 주면 해당 클래스가 생성되는 구조임
                var purchase = PurchaseManager.Instance.purchase[choice];

                if (purchase.Condition())
                {

                    purchase.Buy();
                }
                else
                {
                    Console.WriteLine("\n돈이 부족합니다!");
                    Console.WriteLine("\n엔터를 누르면 이전메뉴로 돌아갑니다!");
                    Console.ReadKey(); // 반복문에서 계속 실행되는  Console.Clear(); 를 처리해주기 위해서 프로그램 일시정지인 ReadKey() 기능 사용
                    return; // 해당 반복문 탈출
                }
            }
        }
    }
}