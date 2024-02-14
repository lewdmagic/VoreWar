using System.Collections.Generic;

internal class WeightedList<T1>
{
    private List<T1> _action = new List<T1>();
    private List<int> _weight = new List<int>();

    public void Add(T1 action, int weight)
    {
        if (weight <= 0) return;
        this._action.Add(action);
        this._weight.Add(weight);
    }

    public T1 GetResult()
    {
        int total = 0;
        int count = _weight.Count;
        if (count < 1) return default(T1);
        if (count == 1) return _action[0];
        foreach (int weight in _weight)
        {
            total += weight;
        }

        int roll = State.Rand.Next(total);
        int accumulator = 0;
        for (int x = 0; x < count; x++)
        {
            accumulator += _weight[x];
            if (roll < accumulator) return _action[x];
        }

        return default(T1);
    }

    public T1 GetAndRemoveResult()
    {
        T1 act;
        int total = 0;
        int count = _weight.Count;
        if (count < 1) return default(T1);
        if (count == 1)
        {
            act = _action[0];
            _action.Clear();
            _weight.Clear();
            return act;
        }

        foreach (int weight in _weight)
        {
            total += weight;
        }

        int roll = State.Rand.Next(total);
        int accumulator = 0;
        for (int x = 0; x < count; x++)
        {
            accumulator += _weight[x];
            if (roll < accumulator)
            {
                act = _action[x];
                _action.RemoveAt(x);
                _weight.RemoveAt(x);
                return act;
            }
        }

        return default(T1);
    }
}