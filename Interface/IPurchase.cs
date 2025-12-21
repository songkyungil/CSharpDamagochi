using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDamagochi.Interface
{
    public interface IPurchase
    {
        public string Name { get; }
        public string Description { get; }
        public int Cost { get; }
        public bool Condition();
        public void Buy();
    }
}
