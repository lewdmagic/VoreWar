internal class StrategicMoveUndo
{
    internal Army Army;
    internal int Mp;
    internal Vec2I PreviousPosition;

    public StrategicMoveUndo(Army army, int mP, Vec2I previousPosition)
    {
        Army = army;
        Mp = mP;
        PreviousPosition = previousPosition;
    }

    internal void Undo()
    {
        State.GameManager.StrategyMode.Translator.ClearTranslator();
        State.GameManager.StrategyMode.QueuedPath = null;
        Army.SetPosition(PreviousPosition);
        Army.RemainingMp = Mp;
        State.GameManager.StrategyMode.Regenerate();
    }
}