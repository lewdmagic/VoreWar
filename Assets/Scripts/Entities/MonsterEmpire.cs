public class MonsterEmpire : Empire
{
    //[OdinSerialize]
    //public List<Army> Armies;

    //[OdinSerialize]
    //public IStrategicAI StrategicAI;
    //[OdinSerialize]
    //public TacticalAIType TacticalAIType;


    public MonsterEmpire(ConstructionArgs args) : base(args)
    {
        if (args.StrategicAI == StrategyAIType.Monster)
            StrategicAI = new MonsterStrategicAI(this);
        else if (args.StrategicAI == StrategyAIType.Goblin) StrategicAI = new GoblinAI(this);
    }

    public new void Regenerate()
    {
        for (int i = 0; i < Armies.Count; i++)
        {
            Armies[i].Refresh();
        }
    }


    public new int Income => 0;
    public new int StartingXP => 0;

    public new void LoadFix()
    {
    }

    public new void CalcIncome(Village[] villages, bool AddToStats = false)
    {
    }
}