using CSharpDamagochi.Manager;
using CSharpDamagochi.Table;
using CSharpDamagochi.UI;

namespace CSharpDamagochi.Program
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("CI/CD TEST");
            GameManager.Instance.Init();
            PurchaseManager.Instance.Init();
            ItemManager.Instance.Init();
            BattleManager.Instance.Init();

            Show.Instance.Title();
            GameManager.Instance.SelectPoketmon();

            while(true)
            {
                Show.Instance.PoketmonStatus(LocalData.Instance.selectPoketmon);
                GameManager.Instance.SelectAction();
            }
        }
    }
}
