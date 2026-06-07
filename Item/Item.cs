

namespace CSharpDamagochi.Item
{
    public class Item
    {
        public int itemnumber;
        public string itemname;
        public int itemcount;
        public int itemprice;

     
        public Item(int itemnumber, string itemname,int itemcount, int itemprice) // 아이템 테이블에 들어갈 수있도록 파라메터가 있는 생성자로 만듬.
        {

            this.itemnumber = itemnumber;
            this.itemname = itemname;
            this.itemcount = itemcount;
            this.itemprice = itemprice;
           
        }
    }
}
