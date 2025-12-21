
namespace CSharpDamagochi.Table
{
    public class PoketmonTable
    {
        public Dictionary<int, Poketmon.Poketmon> tables = new()
        {

            // 피카츄
            {1, new(
                hp: 200,
                maxHp: 300,
                atk: 10,
                level: 1,
                curExp :0,
                skills:new Dictionary<Poketmon.ESKillKey, Poketmon.Skill> {
                    { Poketmon.ESKillKey.Thunderbolt, SkillTable.tables[1] }// 바로 인덱스에 접근함, SkillTable은 스태틱으로 정의해줘서 객체생성없이 바로접근 
                }
                ,
                species: "쥐포켓몬",
                name: "피카츄",
                type: Poketmon.EPoketmonType.Electric)
            },
            // 파이리
            {2, new(
                hp: 200,
                maxHp: 300,
                atk: 10,
                level: 1,
                curExp :0,
                skills: new Dictionary<Poketmon.ESKillKey, Poketmon.Skill> {
                    { Poketmon.ESKillKey.Flamethrower, SkillTable.tables[2] }
                },
                species: "도룡뇽포켓몬",
                name: "파이리",
                type: Poketmon.EPoketmonType.Fire)
            },
            // 꼬부기
            {3, new(
                hp:  200,
                maxHp: 300,
                atk: 10,
                level: 1,
                curExp :0,
                skills: new Dictionary<Poketmon.ESKillKey, Poketmon.Skill> {
                    { Poketmon.ESKillKey.HydroPump, SkillTable.tables[4] }
                },
                species: "꼬마거북포켓몬",
                name: "꼬부기",
                type: Poketmon.EPoketmonType.Water)
            },
            // 이상해씨
            {4, new(
                hp:  200,
                maxHp: 300,
                atk: 10,
                level: 1,
                curExp :0,
                skills: new Dictionary<Poketmon.ESKillKey, Poketmon.Skill> {
                    { Poketmon.ESKillKey.RazorLeaf, SkillTable.tables[5] }
                },
                species: "씨앗포켓몬",
                name: "이상해씨",
                type: Poketmon.EPoketmonType.Grass)
            },
            // 구구
            {5, new(
                hp:  200,
                maxHp: 300,
                atk: 10,
                level: 1,
                curExp :0,
                skills: null,
                species: "아기새포켓몬",
                name: "구구",
                type: Poketmon.EPoketmonType.Flying)
            }
        };
    }
}
