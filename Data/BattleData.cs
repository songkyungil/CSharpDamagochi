using CSharpDamagochi.Action;
using CSharpDamagochi.Interface;
using CSharpDamagochi.Manager;
using CSharpDamagochi.Poketmon;
using CSharpDamagochi.Table;
using CSharpDamagochi.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CSharpDamagochi.Data
{
   
    public class BattleData : IBattleActionData
    {
        public Poketmon.Poketmon Enemy;
        public Poketmon.Poketmon Poketmon;

        public BattleData(Poketmon.Poketmon enemy, Poketmon.Poketmon poketmon)
        {
            Enemy = enemy;
            Poketmon = poketmon;
        }
    }

    public class PlayerAttack : IBattleAction, IBattleActionFactory<IBattleActionData>
    {
        private Poketmon.Poketmon _poketmon;
        private Poketmon.Poketmon _enemy;

        public string Name => "공격하기";

        public void Action()
        {
            Console.WriteLine($"\n{_poketmon.name}의 공격!");
            Console.WriteLine($"{_enemy.name}에게 {_poketmon.atk}의 피해!");
            BattleManager.Instance.Attack(_poketmon.atk, _enemy.hp);
             
            if (_enemy.hp < 0) _enemy.hp = 0; // 적 체력 0이하되면 0으로 고정시켜주기 , 음수방지
        }

        public IBattleAction Create(IBattleActionData data)
        {
            var actionData = data as BattleData;
            _poketmon = actionData.Poketmon;
            _enemy = actionData.Enemy;

            return this;
        }
    }

    public class PlayerUseSkill : IBattleAction, IBattleActionFactory<IBattleActionData>
    {
        private Poketmon.Poketmon _enemy;
        private Poketmon.Poketmon _poketmon;

        public string Name => "스킬 사용";

        public void Action()
        {         
            // 스킬 선택 메뉴 표시
            Console.WriteLine("\n사용할 스킬을 선택하세요:");
            int index = 1;
            foreach (var skill in _poketmon.skills.Values) // 가지고 있는 스킬들에서 하나씩 가져옴
            {
                Console.WriteLine($"{index}. {skill.Name}");
                index++;
            }


            var skillChoice = Input.SelectNumber(); // 스킬 선택할 수 있도록 입력받기  }

            //이전 코드에서는 정상적동되는 코드를 바로 return 처리 시켜서 정상작동되지 않은 코드만 return 시켜줌
            if (skillChoice <= 0 || skillChoice > _poketmon.skills.Count)
            {
                return;
            }

            var skillKey = _poketmon.skills.Keys.ElementAt(skillChoice - 1); // ElementAt 함수는 매개변수에 있는 인덱스 순서대로 값 반환 
            var selectSkill = _poketmon.skills[skillKey];

            Console.WriteLine($"\n{_poketmon.name}의 {selectSkill.Name}!");
            selectSkill.Use(_poketmon, _enemy); // 해당 포켓몬이 가지고 있는 스킬 사용하기
        }

        public IBattleAction Create(IBattleActionData data)
        {
            var actionData = data as BattleData;
            _poketmon = actionData.Poketmon;
            _enemy = actionData.Enemy;

            return this;
        }
    }

    public class ItemUse : IBattleAction, IBattleActionFactory<IBattleActionData>
    {
        private Poketmon.Poketmon _poketmon;
        private Poketmon.Poketmon _enemy;

        public string Name => "아이템 사용";

        public void Action()
        {
            Console.WriteLine("\n===== 아이템 사용 =====");
            Console.WriteLine("어떤 아이템을 사용하시겠습니까?");
            foreach (var i in ItemManager.Instance.item)
            {
                var itemValue = i.Value;
                Console.WriteLine($"{i.Key}. {itemValue.Name}x{itemValue.Amount}: {itemValue.Description}");
            }
            Console.WriteLine($"{ItemManager.Instance.item.Count + 1}. 취소");

            // 아이템 선택
            int itemChoice = Input.SelectNumber();

            if (itemChoice == ItemManager.Instance.item.Count + 1)
            {
                return;
            }

            // 미리 생성된 아이템 가져오기
            var item = ItemManager.Instance.item[itemChoice] as IUseableItem;

            // 아이템 데이터 주입
            IItemData itemData = CreateItemData(item.Type);
            var factory = item as IItemFactory<IItemData>;
            factory.Create(itemData);

            // 아이템 사용
            if (item.Condition())
            {
                item.Use();
            }
        }

        public IBattleAction Create(IBattleActionData data)
        {
            var actionData = data as BattleData;
            _poketmon = actionData.Poketmon;
            _enemy = actionData.Enemy;

            return this;
        }

        private IItemData CreateItemData(EItemType type)
        {
            return type switch
            {
                EItemType.Potion => new PotionData(_poketmon),
                EItemType.MonsterBall => new MonsterBallData(_enemy, _poketmon)
            };
        }
    }
    public class TryEscape : IBattleAction
    {
        public string Name => "도망치기";

        public void Action()
        {
            Random random = new Random();
            var escapeChance = random.Next(1, 101);
            if (escapeChance <= 70)
            {
                Console.WriteLine("\n무사히 도망쳤다!");
                BattleManager.Instance.IsBattleActive = false;
            }
            else
            {
                Console.WriteLine("\n도망칠 수 없었다!");
            }
        }
    }
}