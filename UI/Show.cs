using CSharpDamagochi.DesignPattern;
using CSharpDamagochi.Interface;
using CSharpDamagochi.Manager;
using CSharpDamagochi.Poketmon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDamagochi.UI
{
    public class Show : Singleton<Show>
    {
        public void Title()
        {
            Console.WriteLine("포켓몬 키우기");
        }

        public void SelectPoketmon()
        {
            Console.WriteLine("포켓몬을 선택하세요.");
            Console.WriteLine("1.피카츄, 2.파이리, 3.꼬부기");
        }

        public void InputPoketmonName(string species)
        {
            Console.WriteLine($"{species}의 이름을 입력하세요");
        }

        public void PoketmonStatus(Poketmon.Poketmon poketmon)
        {
            Line();
            Console.Write(
                $"포켓몬: {poketmon.species}\n" +
                $"포켓몬 이름: {poketmon.name}\n" +
                $"체력: {poketmon.hp}/{poketmon.maxHp}\n" +
                $"레벨: {poketmon.level}\n" +
                $"경험치: {poketmon.curExp}/{poketmon.level * 100}\n");
            Line();
        }

        public void Line()
        {
            Console.WriteLine("=====================================");
        }

        public void ShowBattleMenu(Dictionary<int, IBattleAction> battles)// 배틀 시작때 보이는 메뉴
        {
            Console.WriteLine("무엇을 하시겠습니까?");

            for (int i = 1; i <= battles.Count; i++)
            {
                Console.WriteLine($"{i}.{battles[i].Name}");
            }
        }


        public void ShowBattleStatus(Poketmon.Poketmon poketmon, Poketmon.Poketmon enemy, string userName)
        {

            Console.WriteLine("\n===== 전투 상황 =====");
            Console.WriteLine($"{userName}의{poketmon.name}: HP {poketmon.hp}/{poketmon.maxHp}");
            Console.WriteLine($"{enemy.name}: HP {enemy.hp}/{enemy.maxHp}");
            // 포획율 계산 및 표시
            double captureRate = BattleManager.Instance.CalculateCaptureRate(enemy);
            Console.WriteLine($"현재 포획 확률: {captureRate:P1}");
            // 스킬 데미지 출력 (타입 상성 정보 포함)
            Console.WriteLine("\n[스킬 예상 데미지]");
            if (poketmon.skills != null && poketmon.skills.Count > 0)
            {
                int skillIndex = 1;
                foreach (var skillEntry in poketmon.skills)
                {
                    var skill = skillEntry.Value;
                    int damage = skill.CalculateDamage(poketmon, enemy.type);
                    double effectiveness = skill.GetEffectiveness(enemy.type);

                    string effectivenessText = "보통";
                    if (effectiveness == 2.0) effectivenessText = "매우 효과적 (2배)";
                    else if (effectiveness == 0.5) effectivenessText = "효과가 별로 (0.5배)";
                    else if (effectiveness == 0.0) effectivenessText = "효과 없음 (0배)";

                    Console.WriteLine($"{skillIndex}. {skill.Name}: {damage} 데미지 [{effectivenessText}]");
                    skillIndex++;
                }
            }
            else
            {
                Console.WriteLine("사용 가능한 스킬이 없습니다.");
            }

            Console.WriteLine("====================");
        }
    }
}
