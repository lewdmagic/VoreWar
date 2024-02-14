using OdinSerializer;

internal class Relationship
{
    [OdinSerialize]
    private float _attitude = 0;

    internal float Attitude { get => _attitude; set => _attitude = value; }

    [OdinSerialize]
    private int _turnsSinceAsked = -1;

    internal int TurnsSinceAsked { get => _turnsSinceAsked; set => _turnsSinceAsked = value; }

    [OdinSerialize]
    private RelationState _type = RelationState.Neutral;

    internal RelationState Type { get => _type; set => _type = value; }

    public Relationship(int firstTeam, int secondTeam)
    {
        if (firstTeam == -1 || secondTeam == -1)
        {
            Type = RelationState.Enemies;
            Attitude = -.75f;
            switch (Config.DiplomacyScale)
            {
                case DiplomacyScale.Default:
                    Attitude = -.75f;
                    break;
                case DiplomacyScale.Suspicious:
                    Attitude = -1.25f;
                    break;
                case DiplomacyScale.Distrustful:
                    Attitude = -5;
                    break;
                case DiplomacyScale.Friendly:
                    Attitude = 0;
                    break;
                case DiplomacyScale.Unforgetting:
                    Attitude = -.75f;
                    break;
            }

            return;
        }

        if (firstTeam == -200 || secondTeam == -200)
        {
            Type = RelationState.Neutral;
            Attitude = 0;
            return;
        }

        if (firstTeam == secondTeam)
        {
            Type = RelationState.Allied;
            Attitude = 3;
            switch (Config.DiplomacyScale)
            {
                case DiplomacyScale.Default:
                    Attitude = 3;
                    break;
                case DiplomacyScale.Suspicious:
                    Attitude = 2;
                    break;
                case DiplomacyScale.Distrustful:
                    Attitude = 1;
                    break;
                case DiplomacyScale.Friendly:
                    Attitude = 5;
                    break;
                case DiplomacyScale.Unforgetting:
                    Attitude = 3f;
                    break;
            }

            return;
        }
        else
        {
            Type = RelationState.Enemies;
            Attitude = -.75f;
            switch (Config.DiplomacyScale)
            {
                case DiplomacyScale.Default:
                    Attitude = -.75f;
                    break;
                case DiplomacyScale.Suspicious:
                    Attitude = -1.25f;
                    break;
                case DiplomacyScale.Distrustful:
                    Attitude = -5;
                    break;
                case DiplomacyScale.Friendly:
                    Attitude = 0;
                    break;
                case DiplomacyScale.Unforgetting:
                    Attitude = -.75f;
                    break;
            }

            return;
        }
    }
}