using OdinSerializer;
using System.Collections.Generic;

namespace Assets.Scripts.Utility.Stored
{
    internal struct EmpireData
    {
        public bool AIPlayer;
        public string VillageCount;
        public int StrategicAI;
        public int TacticalAI;
        public bool CanVore;
        public string Team;
        public int PrimaryColor;
        public int SecondaryColor;
        public string TurnOrder;
        public float MaxArmySize;
        public float MaxGarrisonSize;
    }

    internal class CreateStrategicStored
    {
        [OdinSerialize]
        internal Dictionary<string, string> InputFields;

        [OdinSerialize]
        internal Dictionary<string, bool> Toggles;

        [OdinSerialize]
        internal Dictionary<string, int> Dropdowns;

        [OdinSerialize]
        internal Dictionary<string, float> Sliders;

        [OdinSerialize]
        internal Dictionary<Race, EmpireData> Empires;

        public CreateStrategicStored()
        {
            InputFields = new Dictionary<string, string>();
            Toggles = new Dictionary<string, bool>();
            Dropdowns = new Dictionary<string, int>();
            Sliders = new Dictionary<string, float>();
            Empires = new Dictionary<Race, EmpireData>();
        }
    }
}