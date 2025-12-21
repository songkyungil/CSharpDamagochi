using CSharpDamagochi.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDamagochi.Poketmon
{
    public enum EPoketmonType
    {
        Normal,
        Fire,
        Water,
        Electric,
        Grass,
        Ice,
        Fighting,
        Poison,
        Ground,
        Flying,
        Psychic,
        Bug,
        Rock,
        Ghost,
        Dragon,
        Dark,
        Steel,
        Fairy
    }
    public enum ESKillKey
    {
        Thunderbolt,
        Flamethrower,
        IcePunch,
        HydroPump,
        RazorLeaf,
    }
    public class Poketmon
    {
        public int hp;
        public int maxHp;
        public int atk;
        public int level;
        public int curExp;
        public Dictionary<ESKillKey, Skill> skills;
        public string species;
        public string name;
        public EPoketmonType type;

        public Poketmon(int hp, int maxHp, int atk, int level, int curExp, Dictionary<ESKillKey, Skill> skills, string species, string name, EPoketmonType type)
        {
            this.hp = hp;
            this.maxHp = maxHp;
            this.atk = atk;
            this.level = level;
            this.curExp = curExp; 
            this.skills = skills;
            this.species = species;
            this.name = name;
            this.type = type;
        }

        public void TakeDamage(int damage)
        {
            hp -= damage;
            Console.WriteLine($"{name}의 체력이 {damage}만큼 피해를 입었습니다!\n");
        }
    }

    public abstract class Skill
    {
        // 공통적으로 처리해준 것.
        public string Name { get; protected set; } // 스킬이름은 필수적으로 들어감, set을 protected 해준 이유는 상속 받은 유도클래스에서만 변경처리 가능하기 때문이다.
        public int Power { get; protected set; } // 스킬의 공격력도 필수적으로 들어감. set을 protected 해준 이유는 상속 받은 유도클래스에서만 변경처리 가능하기 때문이다.
        public EPoketmonType Type { get; protected set; } // 스킬의 타입도 필수적으로 들어감. set을 protected 해준 이유는 상속 받은 유도클래스에서만 변경처리 가능하기 때문이다.

        // 생성자
        protected Skill(string name, int power, EPoketmonType type)
        {
            Name = name;
            Power = power;
            Type = type;
        }
     
        public int CalculateDamage(Poketmon user, EPoketmonType targetType)   // 데미지 계산 함수
        {
            // 기본 데미지 계산
            int power = Power;
            if (user.type == Type) power = (int)(power * 1.2); // 동일 타입 보너스
            power = (int)(power * (1 + user.level * 0.01)); // 레벨 보정

            int damage = (power * user.atk) / 50;

            // 타입 상성 배율 적용
            double effectiveness = GetTypeEffectiveness(targetType);
            damage = (int)(damage * effectiveness);

            return damage;
        }

        
        public double GetEffectiveness(EPoketmonType targetType)// 타입 상성 배율을 반환하는 메서드 (public으로 추가) -> 스킬 데미지 출력처리 위해서 만듬, 그래서 public으로 만들어줌
        {
            return GetTypeEffectiveness(targetType);
        }
        protected virtual double GetTypeEffectiveness(EPoketmonType targetType)// 타입 상성 배율을 반환하는 가상 함수. 기본은 1.0
        {
            return 1.0;
        }



        // 추상 클래스 - 파생 클래스에서 반드시 구현해야 함 -> 데미지 로직 구현함.
        public abstract void Use(Poketmon user, Poketmon target);

    }

}
