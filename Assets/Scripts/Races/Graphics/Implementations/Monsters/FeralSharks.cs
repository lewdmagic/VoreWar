#region

using System.Collections.Generic;

#endregion

/// <summary>
/// Is a skyshark.
/// </summary>
internal static class FeralSharks
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.None };
            output.SkinColors = ColorMap.SharkColorCount;
            output.AccessoryColors = ColorMap.SharkBellyColorCount;
            output.ExtraColors1 = ColorMap.SlimeColorCount;
            output.EyeTypes = 3;
            output.SpecialAccessoryCount = 2;
        });


        builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
        {
            output.Coloring(ColorMap.GetSlimeColor(input.Actor.Unit.ExtraColor1));
            if (input.Actor.Unit.Level > 15)
            {
                output.Sprite(input.Sprites.Shark[29]);
            }
        });

        builder.RenderSingle(SpriteType.Eyes, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Shark[9 + input.Actor.Unit.EyeType]);
        });
        builder.RenderSingle(SpriteType.Mouth, 5, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsEating || input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Shark[8]);
            }
        });
        builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
        {
            output.Coloring(ColorMap.GetSharkColor(input.Actor.Unit.SkinColor));
            if (input.Actor.IsEating || input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Shark[2]);
                return;
            }

            output.Sprite(input.Sprites.Shark[0]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 4, (input, output) =>
        {
            output.Coloring(ColorMap.GetSharkBellyColor(input.Actor.Unit.AccessoryColor));
            if (input.Actor.IsEating || input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Shark[3]);
                return;
            }

            output.Sprite(input.Sprites.Shark[1]);
        });

        builder.RenderSingle(SpriteType.BodyAccent2, 0, (input, output) =>
        {
            output.Coloring(ColorMap.GetSharkColor(input.Actor.Unit.SkinColor));
            if (!input.Actor.Targetable)
            {
                output.Sprite(input.Sprites.Shark[4 + input.Actor.Unit.SpecialAccessoryType]);
                return;
            }

            if (State.Rand.Next(200) == 0)
            {
                input.Actor.SetAnimationMode(1, 1.5f);
            }

            int specialMode = input.Actor.CheckAnimationFrame();
            if (specialMode == 1)
            {
                output.Sprite(input.Sprites.Shark[6 + input.Actor.Unit.SpecialAccessoryType]);
                return;
            }

            output.Sprite(input.Sprites.Shark[4 + input.Actor.Unit.SpecialAccessoryType]);
        });

        builder.RenderSingle(SpriteType.Belly, 1, (input, output) =>
        {
            output.Coloring(ColorMap.GetSharkBellyColor(input.Actor.Unit.AccessoryColor));
            if (input.Actor.HasBelly == false)
            {
                output.Sprite(input.Sprites.Shark[12]);
                return;
            }

            int size = input.Actor.GetStomachSize(22);

            if (size >= 21 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
            {
                output.Sprite(input.Sprites.Shark[30]);
                return;
            }

            if (size >= 19 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
            {
                output.Sprite(input.Sprites.Shark[31]);
                return;
            }

            if (size >= 17 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
            {
                output.Sprite(input.Sprites.Shark[32]);
                return;
            }

            if (size > 16)
            {
                size = 16;
            }

            output.Sprite(input.Sprites.Shark[12 + size]);
        });

        builder.RunBefore(Defaults.Finalize);
        builder.RandomCustom(Defaults.RandomCustom);
    });
}