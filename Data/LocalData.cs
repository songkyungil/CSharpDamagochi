using CSharpDamagochi.DesignPattern;
using CSharpDamagochi.Inventory;

using CSharpDamagochi.Poketmon;


public class LocalData : Singleton<LocalData>
{
    public string userName;
    public Poketmon selectPoketmon;
    public MyInventory inventory = new MyInventory(); // 내 인벤토리 객체 생성
    


    // 포획한 포켓몬 목록
    public List<Poketmon> capturedPokemons = new List<Poketmon>();


}
