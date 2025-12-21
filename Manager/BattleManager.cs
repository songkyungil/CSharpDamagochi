using CSharpDamagochi.DesignPattern;
using CSharpDamagochi.Interface;
using CSharpDamagochi.Poketmon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDamagochi.Manager
{
    public class BattleManager : Singleton<BattleManager>
    {
        public bool IsBattleActive { get; set; }


        public Dictionary<int, IBattleAction> battle = new();

        public void Init()
        {
            CreateBattle();
        }

        private void CreateBattle()
        {
            // 인터페이스 타입
            Type interfaceType = typeof(IBattleAction);

            // 현재 실행 중인 어셈블리에서 인터페이스를 구현하는 모든 클래스 타입 찾기
            List<Type> types = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => interfaceType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToList();

            for (int i = 0; i < types.Count; i++)
            {
                IBattleAction instance = (IBattleAction)Activator.CreateInstance(types[i]);
                battle.Add(i + 1, instance);
            }
        }

        public void CheckLevelUp(Poketmon.Poketmon poketmon) // 경험치 일정치 초과하면 레벨업 해주는 함수
        {
            int requiredExp = poketmon.level * 100;

            if (poketmon.curExp >= requiredExp)
            {
                poketmon.level++;
                poketmon.curExp -= requiredExp;
                poketmon.atk += 10;
                poketmon.maxHp += 50;
                poketmon.hp = poketmon.maxHp;

                Console.WriteLine($"\n축하합니다! {poketmon.name}이(가) 레벨 {poketmon.level}로 올랐습니다!");
                Console.WriteLine($"공격력: +10, 최대체력: 50");
            }
        }

        public double CalculateCaptureRate(Poketmon.Poketmon enemy)
        {
            // 기본 포획률
            double baseCaptureRate = 0.3; // 30% 기본 확률

            // 체력이 낮을수록 포획률 증가
            double hpFactor = 1.0 - ((double)enemy.hp / enemy.maxHp);

            // 최종 포획률 계산 (기본 30% + 체력 비례 최대 70% = 최대 100%)
            double captureRate = baseCaptureRate + (hpFactor * 0.7);

            // 확률은 0~1 사이로 제한 -> 계산식 처리위함(정규화와 비슷하다.)
            if (captureRate < 0) captureRate = 0;
            if (captureRate > 1) captureRate = 1;

            return captureRate;
        }

        public Poketmon.Poketmon CreateCapturedPokemon(Poketmon.Poketmon original)
        {
            // 스킬 딕셔너리 복사
            Dictionary<Poketmon.ESKillKey, Poketmon.Skill> copiedSkills = null; // 처음에 값 초기화 해줘야 가비지값 안들어감.
            if (original.skills != null) // 해당 포켓몬 스킬이 없다면 
            {
                copiedSkills = new Dictionary<Poketmon.ESKillKey, Poketmon.Skill>(); // 딕셔너리 스킬 값 대입.
                foreach (var a in original.skills) // 넣어준 스킬에서 하나씩 
                {
                    copiedSkills.Add(a.Key, a.Value); // 스킬 넣어주기
                }
            }

            // 포획한 포켓몬 생성
            return new Poketmon.Poketmon(
                original.hp,
                original.maxHp,
                original.atk,
                original.level,
                original.curExp,
                copiedSkills,
                original.species,
                original.name, // 이름은 종족명으로 설정
                original.type
            );
        }

        public void Attack(int damage, int hp)
        {
            hp -= damage;
        }

        public void AwardExperience(Poketmon.Poketmon poketmon, Poketmon.Poketmon enemy) // 경험치 보상출력 함수
        {
            int expGained = enemy.level * 50;
            poketmon.curExp += expGained;
            Console.WriteLine($"{poketmon.name}은(는) {expGained} 경험치를 얻었다!");

            BattleManager.Instance.CheckLevelUp(poketmon);
        }
    }
}
