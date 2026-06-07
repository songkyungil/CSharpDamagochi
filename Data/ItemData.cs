
using CSharpDamagochi.Interface;
using CSharpDamagochi.Manager;

namespace CSharpDamagochi.Data
{
    public enum EItemType
    {
        Potion,
        MonsterBall
    }
    public class MonsterBallData : IItemData
    {
        public Poketmon.Poketmon Enemy;
        public Poketmon.Poketmon Poketmon;

        public MonsterBallData(Poketmon.Poketmon enemy, Poketmon.Poketmon poketmon)
        {
            Enemy = enemy;
            Poketmon = poketmon;
        }
    }

    public class ItemMonsterBall : IUseableItem, IItemFactory<IItemData>
    {
        private Poketmon.Poketmon _enemy;
        private Poketmon.Poketmon _poketmon;

        public string Name => "몬스터 볼";
        public string Description => "몬스터를 포획할 수 있습니다";

        public int Amount => LocalData.Instance.inventory.Items[3].itemcount;

        public EItemType Type => EItemType.MonsterBall;

        public bool Condition()
        {
            if (LocalData.Instance.inventory.Items[3].itemcount <= 0)
            {
                Console.WriteLine("몬스터볼이 없습니다!");
                return false;
            }

            return true;
        }

        public IUseableItem Create(IItemData data)
        {
            var monsterBallData = data as MonsterBallData;
            _enemy = monsterBallData.Enemy;
            _poketmon = monsterBallData.Poketmon;

            return this;
        }

        public void Use()
        {

            LocalData.Instance.inventory.Items[3].itemcount--;
            Console.WriteLine($"몬스터볼을 사용했습니다! (남은 개수: {LocalData.Instance.inventory.Items[3].itemcount})");

            // 포획 확률 계산
            var captureRate = BattleManager.Instance.CalculateCaptureRate(_enemy);

            Random random = new Random();
            bool isCaptured = random.NextDouble() < captureRate;

            if (isCaptured)
            {
                Console.WriteLine($"축하합니다! {_enemy.name}를 포획했습니다!");

                // 포획한 포켓몬 저장
                Poketmon.Poketmon capturedPokemon = BattleManager.Instance.CreateCapturedPokemon(_enemy);
                LocalData.Instance.capturedPokemons.Add(capturedPokemon);

                Console.WriteLine($"현재 포획한 포켓몬 수: {LocalData.Instance.capturedPokemons.Count}");

                BattleManager.Instance.IsBattleActive = false;

                // 포획 보상
                int expGained = _enemy.level * 25;
                _poketmon.curExp += expGained;
                Console.WriteLine($"{_poketmon.name}은(는) {expGained} 경험치를 얻었다!");

                BattleManager.Instance.CheckLevelUp(_poketmon);
            }
            else
            {
                Console.WriteLine($"아쉽게도 {_enemy.name}는 몬스터볼에서 탈출했습니다!");

                // 포획 실패 시 적의 턴
                Console.WriteLine("\n적이 분노했습니다!");
                //EnemyTurn();
            }
        }
    }

    public class PotionData : IItemData
    {
        public Poketmon.Poketmon Poketmon;

        public PotionData(Poketmon.Poketmon poketmon)
        {
            Poketmon = poketmon;
        }
    }
    public class ItemRedPotion : IUseableItem, IItemFactory<IItemData>
    {
        private Poketmon.Poketmon _poketmon;
        public string Name => "빨간 포션";
        public string Description => "체력 50 회복";

        public int Amount => LocalData.Instance.inventory.Items[0].itemcount;


        public EItemType Type => EItemType.Potion;

        public bool Condition()
        {
            if (LocalData.Instance.inventory.Items[0].itemcount <= 0)
            {
                Console.WriteLine("빨간포션이 없습니다!");
                return false;
            }

            return true;
        }

        public void Use()
        {
            LocalData.Instance.inventory.Items[0].itemcount--;

            int healAmount = 50;
            int originalHp = _poketmon.hp;

            _poketmon.hp += healAmount;
            if (_poketmon.hp >= _poketmon.maxHp) // 최대체력 초과하게되면
                _poketmon.hp = _poketmon.maxHp; // 최대체력에 맞춰주기

            int actualHeal = _poketmon.hp - originalHp; //현재체력에서 뺀만큼 실제 회복량


            Console.WriteLine($"빨간포션을 사용했습니다! (남은 개수: {LocalData.Instance.inventory.Items[0].itemcount})");
            Console.WriteLine($"{_poketmon.name}의 체력이 {actualHeal}만큼 회복되었습니다.");
            Console.WriteLine($"현재 체력: {_poketmon.hp}/{_poketmon.maxHp}");
        }

        public IUseableItem Create(IItemData data)
        {
            var potionData = data as PotionData;
            _poketmon = potionData.Poketmon;
            return this;
        }
    }

    public class ItemHyperPotion : IUseableItem, IItemFactory<IItemData>
    {
        private Poketmon.Poketmon _poketmon;

        public string Name => "하이퍼 포션";

        public string Description => "체력 100 회복";

        public int Amount => LocalData.Instance.inventory.Items[1].itemcount;

        public EItemType Type => EItemType.Potion;

        public bool Condition()
        {
            if (LocalData.Instance.inventory.Items[1].itemcount <= 0)
            {
                Console.WriteLine("고급빨간포션이 없습니다!");
                return false;
            }

            return true;
        }

        public void Use()
        {
            LocalData.Instance.inventory.Items[1].itemcount--;

            // 액션
            int healAmount = 100;
            int originalHp = _poketmon.hp;

            _poketmon.hp += healAmount;
            if (_poketmon.hp >= _poketmon.maxHp)
                _poketmon.hp = _poketmon.maxHp;

            int actualHeal = _poketmon.hp - originalHp;

            Console.WriteLine($"고급빨간포션을 사용했습니다! (남은 개수: {LocalData.Instance.inventory.Items[1].itemcount})");
            Console.WriteLine($"{_poketmon.name}의 체력이 {actualHeal}만큼 회복되었습니다.");
            Console.WriteLine($"현재 체력: {_poketmon.hp}/{_poketmon.maxHp}");
        }

        public IUseableItem Create(IItemData data)
        {
            var potionData = data as PotionData;
            _poketmon = potionData.Poketmon;
            return this;
        }
    }

    public class ItemFullRecovery : IUseableItem, IItemFactory<IItemData>
    {
        private Poketmon.Poketmon _poketmon;

        public string Name => "풀 회복약";

        public string Description => "체력을 모두 회복";

        public int Amount => LocalData.Instance.inventory.Items[2].itemcount;

        public EItemType Type => EItemType.Potion;

        public bool Condition()
        {
            if (LocalData.Instance.inventory.Items[2].itemcount <= 0)
            {
                Console.WriteLine("풀회복약이 없습니다!");
                return false;
            }

            return true;
        }

        public IUseableItem Create(IItemData data)
        {
            var potionData = data as PotionData;
            _poketmon = potionData.Poketmon;
            return this;
        }

        public void Use()
        {
            LocalData.Instance.inventory.Items[2].itemcount--;

            // 액션
            int originalHp = _poketmon.hp;

            _poketmon.hp = _poketmon.maxHp;
            int actualHeal = _poketmon.hp - originalHp;

            Console.WriteLine($"풀회복약을 사용했습니다! (남은 개수: {LocalData.Instance.inventory.Items[2].itemcount})");
            Console.WriteLine($"{_poketmon.name}의 체력이 완전히 회복되었습니다. (+{actualHeal})");
            Console.WriteLine($"현재 체력: {_poketmon.hp}/{_poketmon.maxHp}");
        }
    }

}
