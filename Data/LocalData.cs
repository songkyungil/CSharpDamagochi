using CSharpDamagochi.DesignPattern;
using CSharpDamagochi.Poketmon;
using System.Collections.Generic;

public class LocalData : Singleton<LocalData>
{
    public string userName;
    public Poketmon selectPoketmon;
    
    // 재화 시스템 추가
    public int money = 1000; // 초기 자본금 1000원
    public int monsterBallCount = 3; // 몬스터볼 보유 개수

    // 회복 아이템 데이터 추가
    public int redPotionCount = 2;      // 체력 50 회복
    public int hyperPotionCount = 2;    // 체력 100 회복
    public int fullRestoreCount = 1;    // 전체 체력 회복

    // 포획한 포켓몬 목록
    public List<Poketmon> capturedPokemons = new List<Poketmon>();
}
