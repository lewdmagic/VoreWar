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

    public static bool AreBreastsOverside(Actor_Unit actor, int highestBreastSprite)
    {
        if (actor.PredatorComponent?.LeftBreastFullness > 0)
        {
            int leftSize =
                (int)Math.Sqrt(actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize +
                               actor.GetLeftBreastSize(highestBreastSprite));
            if (leftSize > actor.Unit.DefaultBreastSize)
            {
                return true;
            }
        }

        if (actor.PredatorComponent?.RightBreastFullness > 0)
        {
            int rightSize =
                (int)Math.Sqrt(actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize +
                               actor.GetRightBreastSize(highestBreastSprite));
            if (rightSize > actor.Unit.DefaultBreastSize)
            {
                return true;
            }
        }

        return false;
    }


    public static Func<IClothingRenderInput, IOverSizeParameters> MakeOversizeFunc(int size)
    {
        return input =>
        {
            return new OverSizeParameters()
            {
                Oversize = CommonRaceCode.AreBreastsOverside(input.A, size)
            };
        };
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