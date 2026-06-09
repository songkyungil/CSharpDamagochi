using CSharpDamagochi.Manager;
using CSharpDamagochi.Table;
using CSharpDamagochi.UI;

namespace CSharpDamagochi.Program
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("CI/CD TEST!!!!! 집공유기로 DDNS설정이랑 포트 포워딩 작업함!");
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
