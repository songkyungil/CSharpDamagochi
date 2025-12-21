
using CSharpDamagochi.Poketmon;

namespace CSharpDamagochi.Table
{
    //정적(static)으로 처리해줘서 생성자 없이 그냥 값만 넘겨주기로함.
    public static class SkillTable
    // 1. public Dictionary<int, Poketmon.Skill> tables = new() 가 안되는 이유 -> 타입명시를 정확하게 해줘야 컴파일러가 알아먹음.
    {   // 스킬이 추상클래스라서 객체 생성 불가하지만 Dictionary 객체 생성은 가능하다 -> 그 딕셔너리 객체에 추상클래스를 상속받은 파생클래스 값을 넣어줄거임.
        // 딕셔너리의 값은 상속받은 skill 의 파생클래스의 객체를 써줘야된다.
        public static Dictionary<int, Poketmon.Skill> tables = new Dictionary<int, Poketmon.Skill>();


        static SkillTable() // 스킬테이블 생성자 만들어주기. -> 번호명, 스킬 형식임.
        {
            // 파생 클래스 인스턴스를 생성하여 추가
            tables.Add(1, new Thunderbolt()); // 파생클래스의 객체로 생성
            tables.Add(2, new Flamethrower());
            tables.Add(3, new IcePunch());
            tables.Add(4, new HydroPump());
            tables.Add(5, new RazorLeaf());
        }

        public class Thunderbolt : Poketmon.Skill
        {
            public Thunderbolt()
           : base("10만볼트", 90, Poketmon.EPoketmonType.Electric) // 생성자로 만들어 줘서 값 대입 초기화 시켜줌
            {
               
            }

            protected override double GetTypeEffectiveness(Poketmon.EPoketmonType targetType)
            {
                if (targetType == Poketmon.EPoketmonType.Water || targetType == Poketmon.EPoketmonType.Flying)
                    return 2.0;
                else if (targetType == Poketmon.EPoketmonType.Ground)
                    return 0.0;
                else if (targetType == Poketmon.EPoketmonType.Grass ||
                         targetType == Poketmon.EPoketmonType.Electric ||
                         targetType == Poketmon.EPoketmonType.Dragon)
                    return 0.5;
                else
                    return 1.0;
            }

            public override void Use(Poketmon.Poketmon user, Poketmon.Poketmon target)
            {
                string playerName = LocalData.Instance.userName;
                Console.WriteLine($"{playerName}의 {user.name}가 {Name}을 사용!");

                int damage = CalculateDamage(user, target.type); // 생성해준 객체의 멤버변수를 이용해 계산해주는 함수

                double effectiveness = GetTypeEffectiveness(target.type);
                if (effectiveness == 2.0)
                    Console.WriteLine("효과가 굉장했다!");
                else if (effectiveness == 0.0)
                    Console.WriteLine("아무런 피해를 입지않았다!");
                else if (effectiveness == 0.5)
                    Console.WriteLine("효과가 별로인 것 같다...");

                Console.WriteLine($"{target.name}에게 {damage}의 피해!");
                target.TakeDamage(damage);
            }
        }

        // Flamethrower (불꽃 타입)
        public class Flamethrower : Poketmon.Skill
        {
            public Flamethrower()
                : base("화염방사", 95, Poketmon.EPoketmonType.Fire)
            {
             
            }

            protected override double GetTypeEffectiveness(Poketmon.EPoketmonType targetType)
            {
                if (targetType == Poketmon.EPoketmonType.Grass ||
                    targetType == Poketmon.EPoketmonType.Ice ||
                    targetType == Poketmon.EPoketmonType.Bug ||
                    targetType == Poketmon.EPoketmonType.Steel)
                    return 2.0;
                else if (targetType == Poketmon.EPoketmonType.Fire ||
                         targetType == Poketmon.EPoketmonType.Water ||
                         targetType == Poketmon.EPoketmonType.Rock ||
                         targetType == Poketmon.EPoketmonType.Dragon)
                    return 0.5;
                else
                    return 1.0;
            }

            public override void Use(Poketmon.Poketmon user, Poketmon.Poketmon target)
            {
                string playerName = LocalData.Instance.userName;
                Console.WriteLine($"{playerName}의 {user.name}가 {Name}을 사용!");

                int damage = CalculateDamage(user, target.type);

                double effectiveness = GetTypeEffectiveness(target.type);
                if (effectiveness == 2.0)
                    Console.WriteLine("효과가 굉장했다!");
                else if (effectiveness == 0.5)
                    Console.WriteLine("효과가 별로인 것 같다...");

                Console.WriteLine($"{target.name}에게 {damage}의 피해!");
                target.TakeDamage(damage);
            }
        }
        // IcePunch (얼음 타입)
        public class IcePunch : Poketmon.Skill
        {
            public IcePunch()
                : base("냉동펀치", 75, Poketmon.EPoketmonType.Ice)
            {

            }

            protected override double GetTypeEffectiveness(Poketmon.EPoketmonType targetType)
            {
                if (targetType == Poketmon.EPoketmonType.Grass ||
                    targetType == Poketmon.EPoketmonType.Ground ||
                    targetType == Poketmon.EPoketmonType.Flying ||
                    targetType == Poketmon.EPoketmonType.Dragon)
                    return 2.0;
                else if (targetType == Poketmon.EPoketmonType.Fire ||
                         targetType == Poketmon.EPoketmonType.Water ||
                         targetType == Poketmon.EPoketmonType.Ice ||
                         targetType == Poketmon.EPoketmonType.Steel)
                    return 0.5;
                else
                    return 1.0;
            }

            public override void Use(Poketmon.Poketmon user, Poketmon.Poketmon target)
            {
                string playerName = LocalData.Instance.userName;
                Console.WriteLine($"{playerName}의 {user.name}가 {Name}을 사용!");

                int damage = CalculateDamage(user, target.type);

                double effectiveness = GetTypeEffectiveness(target.type);
                if (effectiveness == 2.0)
                    Console.WriteLine("효과가 굉장했다!");
                else if (effectiveness == 0.5)
                    Console.WriteLine("효과가 별로인 것 같다...");

                Console.WriteLine($"{target.name}에게 {damage}의 피해!");
                target.TakeDamage(damage);
            }
        }


        // HydroPump (물 타입)
        public class HydroPump : Poketmon.Skill
        {
            public HydroPump()
                : base("하이드로펌프", 110, Poketmon.EPoketmonType.Water)
            {

            }

            protected override double GetTypeEffectiveness(Poketmon.EPoketmonType targetType)
            {
                if (targetType == Poketmon.EPoketmonType.Fire ||
                    targetType == Poketmon.EPoketmonType.Ground ||
                    targetType == Poketmon.EPoketmonType.Rock)
                    return 2.0;
                else if (targetType == Poketmon.EPoketmonType.Water ||
                         targetType == Poketmon.EPoketmonType.Grass ||
                         targetType == Poketmon.EPoketmonType.Dragon)
                    return 0.5;
                else
                    return 1.0;
            }

            public override void Use(Poketmon.Poketmon user, Poketmon.Poketmon target)
            {
                string playerName = LocalData.Instance.userName;
                Console.WriteLine($"{playerName}의 {user.name}가 {Name}을 사용!");

                int damage = CalculateDamage(user, target.type);

                double effectiveness = GetTypeEffectiveness(target.type);
                if (effectiveness == 2.0)
                    Console.WriteLine("효과가 굉장했다!");
                else if (effectiveness == 0.5)
                    Console.WriteLine("효과가 별로인 것 같다...");

                Console.WriteLine($"{target.name}에게 {damage}의 피해!");
                target.TakeDamage(damage);
            }
        }

        // RazorLeaf (풀 타입)
        public class RazorLeaf : Poketmon.Skill
        {
            public RazorLeaf()
                : base("잎날가르기", 55, Poketmon.EPoketmonType.Grass)
            {

            }

            protected override double GetTypeEffectiveness(Poketmon.EPoketmonType targetType)
            {
                if (targetType == Poketmon.EPoketmonType.Water ||
                    targetType == Poketmon.EPoketmonType.Ground ||
                    targetType == Poketmon.EPoketmonType.Rock)
                    return 2.0;
                else if (targetType == Poketmon.EPoketmonType.Fire ||
                         targetType == Poketmon.EPoketmonType.Grass ||
                         targetType == Poketmon.EPoketmonType.Poison ||
                         targetType == Poketmon.EPoketmonType.Flying ||
                         targetType == Poketmon.EPoketmonType.Bug ||
                         targetType == Poketmon.EPoketmonType.Dragon ||
                         targetType == Poketmon.EPoketmonType.Steel)
                    return 0.5;
                else
                    return 1.0;
            }

            public override void Use(Poketmon.Poketmon user, Poketmon.Poketmon target)
            {
                string playerName = LocalData.Instance.userName;
                Console.WriteLine($"{playerName}의 {user.name}가 {Name}을 사용!");

                int damage = CalculateDamage(user, target.type);

                double effectiveness = GetTypeEffectiveness(target.type);
                if (effectiveness == 2.0)
                    Console.WriteLine("효과가 굉장했다!");
                else if (effectiveness == 0.5)
                    Console.WriteLine("효과가 별로인 것 같다...");

                Console.WriteLine($"{target.name}에게 {damage}의 피해!");
                target.TakeDamage(damage);
            }
        }
    }
    
}
