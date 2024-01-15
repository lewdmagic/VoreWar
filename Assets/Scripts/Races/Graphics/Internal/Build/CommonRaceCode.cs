#region

using System;

#endregion

public static class CommonRaceCode
{
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
}