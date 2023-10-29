#region

using System;

#endregion

internal static class CommonRaceCode
{
    internal static Action<IRunInput, IRunOutput<OverSizeParameters>> MakeBreastOversize(int highestBreastSprite)
    {
        return (input, output) =>
        {
            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize =
                    (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize +
                                   input.Actor.GetLeftBreastSize(highestBreastSprite));
                if (leftSize > input.Actor.Unit.DefaultBreastSize)
                {
                    output.Params.Oversize = true;
                }
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize =
                    (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize +
                                   input.Actor.GetRightBreastSize(highestBreastSprite));
                if (rightSize > input.Actor.Unit.DefaultBreastSize)
                {
                    output.Params.Oversize = true;
                }
            }
        };
    }
}