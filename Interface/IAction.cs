using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDamagochi.Interface
{
    public interface IAction
    {
        public string ActionName { get; }
        public void Exectue();
        public void Show();
    }
}
