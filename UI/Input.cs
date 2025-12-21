using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDamagochi.UI
{
    public static class Input
    {
        public static int SelectNumber()
        {
            Console.Write("선택:");
            return Console.ReadLine().ToInt32();
        }

        public static string SelectText()
        {
            Console.Write("입력:");
            return Console.ReadLine();
        }
    }
}
