internal class RaceSpriteMod
{
}


public static class SeliciaMod
{
    private static void ModEquineBelly()
    {
        /*
        var race = Races.GetRace(Race.Equines);


        var raceTyped = race as RaceDataBuildImpl<IParameters>;


        raceTyped.RaceSpriteSet[SpriteType.Belly].GeneratorMod = (input) =>
        {
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(29, 1.2f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    //return Out.OnlySprite(State.GameManager.SpriteDictionary.Horse[89]);
                }
                else if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    //return Out.OnlySprite(State.GameManager.SpriteDictionary.Horse[88]);
                }
                else if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size >= 27)
                {
                    //return Out.OnlySprite(State.GameManager.SpriteDictionary.Horse[87]);
                }
            }

            return null;
        };

    }



    private static void Belly()
    {
        Func<IGenInput<IParameters>, IGenOutput> generator = (input) =>
        {
            if (input.Actor.HasBelly)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize(9) == 9)
                {
                    //return Out.OnlySprite(State.GameManager.SpriteDictionary.Puca[50]);
                }
                else if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize(9) == 9)
                {
                    //return Out.OnlySprite(State.GameManager.SpriteDictionary.Puca[49]);
                }
            }

            return null;
        };





         */
    }


    private static void Balls()
    {
        // Func<IGenInputWithState<BasicState>, UpdateOut> generator = (input) =>
        // {
        //     if (input.Actor.Unit.HasDick == false)
        //     {
        //         return null;
        //     }
        //
        //     update.AddOffset(0, -21 * .625f);
        //     if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ??
        //          false) && input.Actor.GetBallSize(21, .8f) == 21)
        //     {
        //         return update.Sprite(State.GameManager.SpriteDictionary.Balls[24]).AddOffset(0, -18 * .625f);
        //     }
        //     else if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ??
        //               false) && input.Actor.GetBallSize(21, .8f) == 21)
        //     {
        //         return update.Sprite(State.GameManager.SpriteDictionary.Balls[23]).AddOffset(0, -18 * .625f);
        //     }
        //     else if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ??
        //               false) && input.Actor.GetBallSize(21, .8f) == 20)
        //     {
        //         return update.Sprite(State.GameManager.SpriteDictionary.Balls[22]).AddOffset(0, -15 * .625f);
        //     }
        //     else if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ??
        //               false) && input.Actor.GetBallSize(21, .8f) == 19)
        //     {
        //         return update.Sprite(State.GameManager.SpriteDictionary.Balls[21]).AddOffset(0, -14 * .625f);
        //     }
        //
        //     return null;
        // };
    }
}