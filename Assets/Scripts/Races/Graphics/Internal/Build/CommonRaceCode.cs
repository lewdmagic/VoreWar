#region

using System;

#endregion

public static class CommonRaceCode
{
    public static Action<IRunInput, IRaceRenderAllOutput<OverSizeParameters>> MakeBreastOversize2(int highestBreastSprite)
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

    public static bool AreBreastsOverside(IRunInput input, int highestBreastSprite)
    {
        if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
        {
            int leftSize =
                (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize +
                               input.Actor.GetLeftBreastSize(highestBreastSprite));
            if (leftSize > input.Actor.Unit.DefaultBreastSize)
            {
                return true;
            }
        }

        if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
        {
            int rightSize =
                (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize +
                               input.Actor.GetRightBreastSize(highestBreastSprite));
            if (rightSize > input.Actor.Unit.DefaultBreastSize)
            {
                return true;
            }
        }

        return false;
    }
    
    public static Action<IRunInput, IRunOutput<OverSizeParameters>> MakeBreastOversize(int highestBreastSprite)
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
    
    public static void MakeBreastOversize2(IRunInput input, IRunOutput<OverSizeParameters> output, int highestBreastSprite)
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
    }
}