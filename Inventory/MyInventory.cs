
using CSharpDamagochi.Table;



namespace CSharpDamagochi.Inventory
{
    public class MyInventory 
    {
        // 인벤토리에 존재하는 나의 골드
        public int mygold;

        //리스트로 받아야 여러 아이템을 받아 처리할 수 있다.
        public List<Item.Item> Items; // 다른 클래스에 있는 item 멤버변수로 선언 -> has-a 관계 이용


        // 기본 생성자로 만들어줌
        public MyInventory()
        {
            mygold = 1000; // 기본골드
            Items = new List<Item.Item>(); // 아이템 정보를 리스트로 객체 생성해줌 -> 테이블에서 집어 넣어줄 리스트임
            
            var itemTable = new ItemTable(); // ItemTable에서 정보 가져오기

            // ItemTable의 모든 아이템을 기반으로 빈 인벤토리 생성
            foreach (var element in itemTable.tables) // 아이템 테이블에서 하나하나 식 빼옴 -> 이렇게 넣어줘야 아이템 테이블에 아이템 추가해도 전체적인 코드 변경없다.
            {
                Item.Item originalItem = element.Value; //요소 하나하나에 itme

                // ItemTable의 아이템을 복사하되, 개수는 0으로 설정
                Item.Item inventoryItem = new Item.Item( // 데이터 타입 맞춰주기 위해서 새로 객체 생성하고 데이터 값 넣어주기 -> inventory에 들어갈 정보임
                    itemnumber: originalItem.itemnumber,
                    itemname: originalItem.itemname,
                    itemcount: 0,  // 인벤토리에서는 0개부터 시작
                    itemprice: originalItem.itemprice
                );

                Items.Add(inventoryItem); //마지막에 List로 만들어준 인벤토리 데이터 다 집어넣어주기 
            }
        }

   
    }
}
