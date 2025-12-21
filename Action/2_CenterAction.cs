using CSharpDamagochi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDamagochi.Action
{
    public class CenterAction : IAction
    {
        private readonly string _actionName = "포켓몬 센터";
        public string ActionName => _actionName;

        public void Exectue()
        {
            new Center().Healing();
        }

        public void Show()
        {
            Console.WriteLine("포켓몬 센터 방문 완료");
        }
    }

    public class Center
    {
        private readonly LocalData _localData = LocalData.Instance; // 로컬데이터에 내가 선택한 포켓몬과 포획한 포켓몬 데이터 전부 들어있음.

        public void Healing()
        {
            Console.Clear();
            Console.WriteLine("포켓몬 센터에 오신 것을 환영합니다!");

            // 현재 선택된 포켓몬 회복
            if (_localData.selectPoketmon != null)
            {
                _localData.selectPoketmon.hp = _localData.selectPoketmon.maxHp;
                Console.WriteLine($"{_localData.selectPoketmon.name}의 체력이 모두 회복되었습니다.");
            }

            // 포획한 모든 포켓몬 회복
            if (_localData.capturedPokemons != null)
            {
                for (int i = 0; i < _localData.capturedPokemons.Count; i++) //포획한 포켓몬 배열 모두 순회
                {
                    // 현재 선택된 포켓몬과 같지 않은 경우에만 회복 -> 포획한 포켓몬만 회복
                    if (_localData.capturedPokemons[i] != _localData.selectPoketmon)
                    {
                        _localData.capturedPokemons[i].hp = _localData.capturedPokemons[i].maxHp;
                        Console.WriteLine($"{_localData.capturedPokemons[i].name}의 체력이 회복되었습니다.");
                    }
                }
                Console.WriteLine($"총 {_localData.capturedPokemons.Count+1}마리의 포켓몬이 치료되었습니다."); // 잡은 포켓몬 + 1 은 무조건 전체 포켓몬수임
            }

            Console.WriteLine("모든 포켓몬이 건강해졌습니다!");
        }
    }
}
