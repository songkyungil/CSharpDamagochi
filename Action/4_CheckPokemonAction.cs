using CSharpDamagochi.Interface;
using CSharpDamagochi.Poketmon;
using CSharpDamagochi.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CSharpDamagochi.Action.CheckPokemonAction;

namespace CSharpDamagochi.Action
{
    public class CheckPokemonAction : IAction
    {
        private readonly string _actionName = "포켓몬 확인/교체";
        public string ActionName => _actionName;

        public void Exectue()
        {
            CheckPokemon checkpokemon = new CheckPokemon();
            checkpokemon.CheckPoketmon();
            checkpokemon.TotalSwap();
        }



        public void Show()
        {
            Console.WriteLine("포켓몬 목록 확인 완료");
        }

        public class CheckPokemon
        {
            public void CheckPoketmon() // 파트너 포켓몬과 포획한 포켓몬 정보 출력해줌
            {
                var currentpokemon = LocalData.Instance.selectPoketmon; // 로컬에서 싱글톤으로 현재 파트너 포켓몬 정보 가져오기
                Console.WriteLine("===== 파트너 포켓몬 =====");
                Console.WriteLine($"{currentpokemon.name}: HP {currentpokemon.hp}/{currentpokemon.maxHp}");
                Console.WriteLine("===== 포획한 포켓몬 목록 =====");

                if (LocalData.Instance.capturedPokemons.Count == 0)
                {
                    Console.WriteLine("포획한 포켓몬이 없습니다.");
                }
                else
                {
                    for (int i = 0; i < LocalData.Instance.capturedPokemons.Count; i++)
                    {
                        var pokemon = LocalData.Instance.capturedPokemons[i];
                        Console.WriteLine($"{i + 1}. {pokemon.name} (HP: {pokemon.hp}/{pokemon.maxHp}, 공격력: {pokemon.atk})");
                    }
                }

                Console.WriteLine($"총 {LocalData.Instance.capturedPokemons.Count}마리 포획");
            }

            public void SwapPokemon(int index)
            {
                var newPartner = LocalData.Instance.capturedPokemons[index]; //잡은 포켓몬 배열에서 가져오기

                // 현재 파트너를 캡처 목록에 추가
                LocalData.Instance.capturedPokemons.Add(LocalData.Instance.selectPoketmon);

                // 선택한 포켓몬을 캡처 목록에서 제거 -> 이미 로컬에 잇는 선택 포켓몬으로 넘어감 
                LocalData.Instance.capturedPokemons.RemoveAt(index); 

                // 파트너 변경
                LocalData.Instance.selectPoketmon = newPartner;

                Console.WriteLine($"파트너가 {newPartner.name}(으)로 변경되었습니다!");
            }

            public void TotalSwap()
            {
                // 기본 정보 표시
                var current = LocalData.Instance.selectPoketmon;
                var capturedPoketmons = LocalData.Instance.capturedPokemons;
                var capturedCount = LocalData.Instance.capturedPokemons.Count;
                Console.WriteLine($"현재 파트너: {current.name} ({current.species})");
                Console.WriteLine($"HP: {current.hp}/{current.maxHp} | 공격력: {current.atk}");

                // 포획한 포켓몬 목록
                Console.WriteLine($"\n포획한 포켓몬 ({capturedCount}마리):");

                if (capturedCount > 0)
                {
                    for (int i = 0; i < capturedCount; i++)
                    {
                        var pokemon = capturedPoketmons[i];
                        Console.WriteLine($"{i + 1}. {pokemon.name} (HP: {pokemon.hp}/{pokemon.maxHp})");
                    }

                    // 교체 옵션
                    Console.Write("\n교체할 포켓몬 번호 (0: 취소): ");
                    int choice = Input.SelectNumber();

                    if (choice > 0 && choice <= capturedCount) // 선택 입력 및 배열의 길이 확인 -> 배열조건 맞쳐줄려면 길이 생각해야됨.
                    {
                        SwapPokemon(choice - 1);
                    }
                }
                else
                {
                    Console.WriteLine("포획한 포켓몬이 없습니다.");
                }
            }

        }
    }
}
