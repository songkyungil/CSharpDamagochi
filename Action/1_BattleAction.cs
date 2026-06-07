using CSharpDamagochi.Data;

using CSharpDamagochi.Interface;
using CSharpDamagochi.Manager;
using CSharpDamagochi.Table;
using CSharpDamagochi.UI;


namespace CSharpDamagochi.Action
{
    public class BattleAction : IAction
    {
        private readonly string actionName = "전투하기";
        public string ActionName => actionName;

        public void Exectue()
        {
            new Battle(LocalData.Instance.selectPoketmon).BattleStart();
        }

        public void Show()
        {
            Console.WriteLine("전투완료");
        }
    }

    public class Battle
    {
        private readonly Poketmon.Poketmon _poketmon;
        private readonly Poketmon.Poketmon _enemy;

        // 생성 될때 내가 선택한 포켓몬의 객체 생성 및 적은 내 포켓몬 테이블에서 랜덤으로 선택되고 생성됨.
        public Battle(Poketmon.Poketmon poketmon)
        {
            _poketmon = poketmon;

            Random random = new Random();
            var poketmonTable = new PoketmonTable();

            // 1부터 5까지 랜덤 인덱스 생성
            int randomIndex = random.Next(1, 6);

            // 포켓몬 테이블에서 랜덤으로 선택한 포켓몬을 적으로 설정 -> 테이블에서 데이터 가져오도록 처리
            var originalEnemy = poketmonTable.tables[randomIndex];

            // 기존 생성자를 사용하여 새로운 적 포켓몬 생성 -> 테이블에서 가져온 데이터로 정확하게 명시해서 만들어줌.
            _enemy = new Poketmon.Poketmon(
                originalEnemy.hp,
                originalEnemy.maxHp,
                originalEnemy.atk,
                originalEnemy.level,
                originalEnemy.curExp,
                originalEnemy.skills,
                originalEnemy.species,
                $"야생 {originalEnemy.name}",
                originalEnemy.type
            );
            BattleManager.Instance.IsBattleActive = true; // 적 생성됐으면 배틀활성화
        }

        public void BattleStart()
        {
            Show.Instance.Line(); // show 싱글턴 사용, 라인표시해주기
            Console.WriteLine($"\n{_enemy.name}가 나타났다!");
            Show.Instance.ShowBattleStatus(_poketmon, _enemy, LocalData.Instance.userName);

            // 턴제 전투 루프 -> 도망치거나 체력 0되거나 적이 죽으면 와일문 탈출.
            while (BattleManager.Instance.IsBattleActive && !IsDead() && !IsKill())
            {
                // 플레이어 턴
                PlayerTurn();
                // 플레이어가 도망쳤는지 확인
                if (!BattleManager.Instance.IsBattleActive) break;

                // 플레이어 턴 후 적이 죽었는지 확인 -> 이렇게 안하면 적을 죽였어도 적턴이 시작되고 와일문 탈출한다.
                if (IsKill()) break;
                // 적 턴
                EnemyTurn();
                // 적 턴 후 플레이어가 죽었는지 확인
                if (IsDead()) break;
                // 전투 상황 표시 -> 둘다 살아 있을때
                Show.Instance.ShowBattleStatus(_poketmon, _enemy, LocalData.Instance.userName);
            }
            // 전투 결과 처리
            HandleBattleResult();
        }

        private void PlayerTurn()
        {
            Console.WriteLine("\n===== 당신의 턴! =====");
            Show.Instance.PoketmonStatus(_poketmon); // Show 타입 싱글턴에서 만든거 가지고 상태보여줌
            Show.Instance.ShowBattleMenu(BattleManager.Instance.battle);

            int choice = Input.SelectNumber(); // 입력 UI 받아옴
            var battle = BattleManager.Instance.battle[choice];
            var data = new BattleData(_enemy, _poketmon);
            var factory = battle as IBattleActionFactory<IBattleActionData>;
            factory?.Create(data);
            
            battle.Action();
        }

        private void EnemyTurn()
        {
            Console.WriteLine($"\n===== {_enemy.name}의 턴! =====");

            // 적도 스킬을 가졌다면 랜덤으로 스킬 사용
            Random random = new Random();
            bool useSkill = false;

            int skillChance = random.Next(1, 101);

            if (_enemy.skills != null && skillChance > 70) // 스킬이 비어있지 않고 스킬을 사용하면 
            {
                useSkill = true;
                var skillKey = _enemy.skills.Keys.First();
                var skill = _enemy.skills[skillKey];

                Console.WriteLine($"{_enemy.name}의 {skill.Name}!");
                skill.Use(_enemy, _poketmon); // 적 스킬을 나한테 플레이어한테 사용
            }

            // 스킬을 사용하지 않으면 일반 공격
            else
            {
                Console.WriteLine($"\n{_enemy.name}의 공격!");
                int damage = _enemy.atk;
                Console.WriteLine($"{_poketmon.name}에게 {damage}의 피해!");
                _poketmon.hp -= damage;

                if (_poketmon.hp < 0) _poketmon.hp = 0; // 내 체력이 마이너스 상태된다면 0으로 값 재설정처리해주기
            }
        }

        private void HandleBattleResult()// 적 죽이고 보상출력 해주는 함수
        {
            if (IsDead())
            {
                Console.WriteLine($"\n{_poketmon.name}은(는) 쓰러졌다...");
            }
            else if (IsKill())
            {
                Console.WriteLine($"\n{_enemy.name}를 쓰러뜨렸다!");
                BattleManager.Instance.AwardExperience(_poketmon, _enemy);
            }
        }

        private bool IsDead() => _poketmon.hp <= 0;

        private bool IsKill() => _enemy.hp <= 0;
    }
}
