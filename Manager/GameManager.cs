using CSharpDamagochi.DesignPattern;
using CSharpDamagochi.Interface;
using CSharpDamagochi.Table;
using CSharpDamagochi.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDamagochi.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        public string actionMenu = "";
        public List<IAction> actions = new List<IAction>();


        public void Init()
        {
            CreateAction();
        }

        public void SelectPoketmon()
        {
            Show.Instance.SelectPoketmon();

            var selectPoketmon = Input.SelectNumber();

            var poketmonDB = new PoketmonTable();
            LocalData.Instance.selectPoketmon = poketmonDB.tables[selectPoketmon];

            Show.Instance.InputPoketmonName(LocalData.Instance.selectPoketmon.name);
            LocalData.Instance.selectPoketmon.name = Input.SelectText();

            Console.WriteLine("유저의 이름을 입력하세요.");
            LocalData.Instance.userName = Input.SelectText();
        }

        public void SelectAction()
        {
            Console.WriteLine(actionMenu);
            var selectAction = Input.SelectNumber();
            actions[selectAction - 1].Exectue();
            actions[selectAction - 1].Show();
        }

        private void CreateAction()
        {
            // 인터페이스 타입
            Type interfaceType = typeof(IAction);

            // 현재 실행 중인 어셈블리에서 인터페이스를 구현하는 모든 클래스 타입 찾기
            List<Type> types = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => interfaceType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToList();

            // 각 타입에 대해 인스턴스 생성
            foreach (Type type in types)
            {
                // Activator.CreateInstance를 사용하여 인스턴스 생성
                IAction instance = (IAction)Activator.CreateInstance(type);
                actions.Add(instance);
            }

            // 생성된 인스턴스 사용
            for(int i = 0; i< actions.Count; i++)
            {
                actionMenu += $"{i+1}.{actions[i].ActionName} ";
            }
        }
    }
}
