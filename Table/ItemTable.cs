

namespace CSharpDamagochi.Table
{
    public class ItemTable
    {
        public Dictionary<int, Item.Item> tables;

        public ItemTable() // 생성자로 만들기 
        {
            tables = new Dictionary<int, Item.Item>
        {
            {1, new Item.Item(1, "빨간 포션", 1, 150)},// 아이템번호, 아이템이름, 아이템개수, 아이템가격
            {2, new Item.Item(2, "하이퍼 포션", 1, 300)},
            {3, new Item.Item(3, "풀 회복약", 1, 600)},
            {4, new Item.Item(4, "몬스터 볼", 1, 300)}
        };
        }
    }
}
