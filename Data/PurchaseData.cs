using CSharpDamagochi.DesignPattern;
using CSharpDamagochi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDamagochi.Data
{
    public class PurchaseMaxHp : IPurchase
    {
        public string Name => "최대체력";

        public string Description => "최대 체력 +50";

        public int Cost => 300;

        public void Buy()
        {
          
            LocalData.Instance.inventory.mygold -= Cost;
            LocalData.Instance.selectPoketmon.maxHp += 50;
            LocalData.Instance.selectPoketmon.hp += 50; // 현재 체력도 증가

            Console.WriteLine($"최대 체력을 50 증가시켰습니다!");
            Console.WriteLine($"{LocalData.Instance.selectPoketmon.name}의 최대 체력: {LocalData.Instance.selectPoketmon.maxHp}");
            Console.WriteLine($"남은 소지금: {LocalData.Instance.inventory.mygold}원");
        }

        public bool Condition()
        {
            if (LocalData.Instance.selectPoketmon == null) return false;
            if (LocalData.Instance.inventory.mygold < Cost) return false;

            return true;
        }

       
    }

    public class PurchaseAttack : IPurchase
    {
        public string Name => "공격력";

        public string Description => "공격력 +5";

        public int Cost => 400;

        public void Buy()
        {
            
            LocalData.Instance.inventory.mygold -= Cost;
            LocalData.Instance.selectPoketmon.atk += 5;

            Console.WriteLine($"공격력을 5 증가시켰습니다!");
            Console.WriteLine($"{LocalData.Instance.selectPoketmon.name}의 공격력: {LocalData.Instance.selectPoketmon.atk}");
            Console.WriteLine($"남은 소지금: {LocalData.Instance.inventory.mygold}원");
        }

        public bool Condition()
        {
            if (LocalData.Instance.selectPoketmon == null) return false;
            if (LocalData.Instance.inventory.mygold < Cost) return false;

            return true;
        }

   
    }

    public class PurchaseMonsterBall : IPurchase
    {
        public string Name => "몬스터 볼";

        public string Description => "포켓몬을 포획할 수 있다";

        public int Cost => 300;

        public void Buy()
        {
            
            LocalData.Instance.inventory.mygold -= Cost;
            LocalData.Instance.inventory.Items[3].itemcount++;

            Console.WriteLine($"몬스터볼을 구매했습니다!");
            Console.WriteLine($"현재 몬스터볼 보유 개수: {LocalData.Instance.inventory.Items[3].itemcount}");
            Console.WriteLine($"남은 소지금: {LocalData.Instance.inventory.mygold}원");
        }

        public bool Condition()
        {
            if (LocalData.Instance.inventory.mygold < Cost) return false;
            return true;
        }

  
    }

    public class PurchaseRedPotion : IPurchase
    {
        public string Name => "빨간 포션";

        public string Description => "체력을 50 회복한다.";

        public int Cost => 150;

        public void Buy()
        {
         
            LocalData.Instance.inventory.mygold -= Cost;
            LocalData.Instance.inventory.Items[0].itemcount++;

            Console.WriteLine("빨간포션을 구매했습니다!");
            Console.WriteLine($"현재 빨간포션: {LocalData.Instance.inventory.Items[0].itemcount}개");
            Console.WriteLine($"남은 소지금: {LocalData.Instance.inventory.mygold}원");
        }

        public bool Condition()
        {
            if (LocalData.Instance.inventory.mygold < Cost) return false;
            return true;
        }

   
    }

    public class PurchaseHyperPotion : IPurchase
    {
        public string Name => "하이퍼 포션";

        public string Description => "체력을 100 회복한다";

        public int Cost => 300;

        public void Buy()
        {
          
            LocalData.Instance.inventory.mygold -= Cost;
            LocalData.Instance.inventory.Items[1].itemcount++;

            Console.WriteLine("고급빨간포션을 구매했습니다!");
            Console.WriteLine($"현재 고급빨간포션: {LocalData.Instance.inventory.Items[1].itemcount}개");
            Console.WriteLine($"남은 소지금: {LocalData.Instance.inventory.mygold}원");
        }

        public bool Condition()
        {
            if (LocalData.Instance.inventory.mygold < Cost) return false;
            return true;
        }

  
    }

    public class PurchaseFullRestore : IPurchase
    {
        public string Name => "풀 회복약";

        public string Description => "체력을 전부 회복한다.";

        public int Cost => 600;

        public void Buy()
        {
            
            LocalData.Instance.inventory.mygold -= Cost;
            LocalData.Instance.inventory.Items[2].itemcount++;

            Console.WriteLine("풀회복약을 구매했습니다!");
            Console.WriteLine($"현재 풀회복약: {LocalData.Instance.inventory.Items[2].itemcount}개");
            Console.WriteLine($"남은 소지금: {LocalData.Instance.inventory.mygold}원");
        }

        public bool Condition()
        {
            if (LocalData.Instance.inventory.mygold < Cost) return false;
            return true;
        }

        public void NotBuy()
        {
            Console.WriteLine($"돈이 없어 구매가 불가합니다!");
        }
    }
}