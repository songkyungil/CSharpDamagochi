using CSharpDamagochi.Interface;
using CSharpDamagochi.Inventory;
using CSharpDamagochi.UI;

namespace CSharpDamagochi.Action
{
    public class InventoryAction : IAction
    {
        private readonly string _actionName = "인벤토리";
        public string ActionName => _actionName;

        public void Exectue()
        {
            Inventory inventory = new Inventory();
            inventory.ShowInventory();
            
        }

        public void Show()
        {
            
        }
        public class Inventory
        {
            public void ShowInventory()
            {
                
                Console.Clear();
                Console.WriteLine("========== 인벤토리 ==========");
                Console.WriteLine($"소지금: {LocalData.Instance.inventory.mygold}원");
                Console.WriteLine("=============================");

                // 아이템 목록 표시
                Console.WriteLine("\n[아이템 목록]");
                Console.WriteLine($"몬스터볼: {LocalData.Instance.inventory.Items[3].itemcount}개");
                Console.WriteLine($"빨간포션: {LocalData.Instance.inventory.Items[0].itemcount}개 (전투 중 체력 50 회복)");
                Console.WriteLine($"고급빨간포션: {LocalData.Instance.inventory.Items[1].itemcount}개 (전투 중 체력 100 회복)");
                Console.WriteLine($"풀회복약: {LocalData.Instance.inventory.Items[2].itemcount}개 (전투 중 전체 체력 회복)");

                Console.WriteLine("\n=============================");
                Console.WriteLine("아이템 사용은 전투 중에만 가능합니다.");

            }
        }
    }
}