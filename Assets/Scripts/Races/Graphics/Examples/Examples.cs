using UnityEngine;

internal static class Examples
{
    
    // Making it an interface will imropove reusability, but it's not necessary
    private interface IFacingFrontParameters : IParameters
    {
        bool FacingFront { get; }
    }

    // Definition of parameters we can use within a render cycle. 
    // NOTE: these only exist within a single cycle. They are non-persistent. 
    private class FacingFrontParameters : IFacingFrontParameters
    {
        public bool FacingFront { get; set; }
    }
    
    // Begin creating an instance of a race
    
    
    // We pass FacingFrontParameters as a generic type to allow access to an instance of it within the render functions. 
    // An new instance of FacingFrontParameters is created at every render cycle. 
    // 
    // The usage of Parameters is as follows: 
    // In RunBefore, the values are calculated and assigned to output.Params.PROPERTY
    // In RenderSingle of RaceBuilder, the value can be accessed by input.Params.PROPERTY
    // In RenderFull of ClothingBuilder, the value can be accessed by input.Params.PROPERTY
    
    
    // We pass "Blank" defaults as a base of our new instance. Blank is the equivalent of the old BlankSlate superclass.
    // The second parameter is of type Action<IRaceBuilder<FacingFrontParameters>>, we create an anonymous instance of it with the 
    // builder => {} labmda syntax.
    // Inside the builder block, we define the behavior of the race renderer by using 
    // builder.Setup, builder.RunBefore, builder.RenderSingle, builder.RandomCustom
    internal static IRaceData MyRace = RaceBuilder.CreateV2(Defaults.Blank, builder =>
    {
        // Setup is only ran once. Think of it as a constructor.
        // It's not required, but it's almost always needed
        builder.Setup(output =>
        {
            // These two are functions because they often change based on Config 
            output.BreastSizes = () => 3;
            output.DickSizes = () => 3;
            output.GentleAnimation = true;
            output.AllowedMainClothingTypes.Set(
                Rags.RagsInstance
            );
        });
        
        // RunBefore is ran before every render cycle.
        // It allows us to perform actions that don't logically belong to any body part.
        // It's also the only valid place to set values in our Paramaters instance, which can be accessed as output.Params
        builder.RunBefore((input, output) =>
        {
            if (input.Actor.IsAnalVoring || input.Actor.IsUnbirthing || input.Actor.IsCockVoring)
            {
                //output.Params.FacingFront = false;
            }
            else
            {
                //output.Params.FacingFront = true;
            }
        });
        
        // Set the logic for rendering a single body part, and the default layer. 
        // output.Sprite() sets the sprite to be rendered. Beware that since there is no return statement,
        // the code will continue executing after you've called output.Sprite().
        // If another output.Sprite() is called, the previous value is overwritten.
        // This isn't strictly a mistake, all method calls to output are practically free regarding performance,
        // however it might make the code less clear.
        // You will need to write this for each body part you want to render (SpriteType.Belly, SpriteType.Head etc).
        builder.RenderSingle(SpriteType.Body, 3, (input, output) =>
        {
            // You can provide either a Palette or a solid Color. 
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.Kobold, input.Actor.Unit.AccessoryColor));
            
            // Access FacingFront we set inside RunBefore
            if (true) //input.Params.FacingFront)
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Kobolds[1]);
                }
                else
                {
                    output.Sprite(input.Sprites.Kobolds[0]);
                }
            }
            else
            {
                output.Sprite(input.Sprites.Kobolds[2]);
            }
            
            // other methods such as Layer(), AddOffset(), Color(), Palette are also availble. 
            // They can be chained in any order for convinience and readability.  
            output.Layer(5).AddOffset(0.5f, 0).Coloring(Defaults.WhiteColored);
            
            // Example #1
            // other methods such as Layer(), AddOffset(), Color(), Palette are also availble. 
            // They can be chained in any order for convinience and readability.  
            output.Sprite(input.Sprites.Kobolds[2]).Layer(5).AddOffset(0.5f, 0).Coloring(Defaults.WhiteColored);
            // equivalent to 
            output.Layer(5).Coloring(Defaults.WhiteColored).Sprite(input.Sprites.Kobolds[2]).AddOffset(0.5f, 0);
            
            // Example #2
            // Values applied to output higher up in the code persist unless overwritten
            output.Layer(5);
            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Kobolds[111]);
            }
            else
            {
                output.Sprite(input.Sprites.Kobolds[222]);
            }
            // is equivalent to 
            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Kobolds[111]).Layer(5);
            }
            else
            {
                output.Sprite(input.Sprites.Kobolds[222]).Layer(5);
            }
            
            // Values can be overwritten. This can be handy when one sprite is different from the rest. 
            output.Sprite(input.Sprites.Kobolds[111]).Layer(5);
            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Kobolds[111]); // This will have Layer = 5
            }
            else
            {
                output.Sprite(input.Sprites.Kobolds[222]).Layer(8); // This will have Layer = 8
            }
        });
        
        // Required function. usually it's based on Defaults.RandomCustom, which is just the old 
        // Random method from DefaultRaceData. 
        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            unit.SkinColor = State.Rand.Next(data.MiscRaceData.SkinColors);
            unit.EyeType = State.Rand.Next(data.MiscRaceData.EyeTypes);
            unit.HairStyle = State.Rand.Next(data.MiscRaceData.HairStyles);
            unit.HairColor = State.Rand.Next(data.MiscRaceData.HairColors);
            unit.BodyAccentType1 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes1);
            unit.BodyAccentType2 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes2);
            unit.BodyAccentType3 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes3);
            unit.BodyAccentType4 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes4);
            unit.ClothingColor = State.Rand.Next(data.MiscRaceData.ClothingColors);
            unit.ClothingColor2 = State.Rand.Next(data.MiscRaceData.ClothingColors);
        });
    });
    
    // --------- CLOTHING -----------
    
    // By making setting the generic type parameter to IFacingFrontParameters, it creates a requirement for the Race's 
    // generic type to implement it. You won't be able to add this clothing item inside RaceBuilder.Create that isn't 
    // of generic type IFacingFrontParameters (or a subtype of it) 
    // This allows the clothing item's RenderFull function to have access to an instance implementing IFacingFrontParameters
    // Which has been updated in the RunBefore function of the Race that's wearing it.
    // TL;DR: It's a way to pass values from Race's rander cycle to clothing items.  
    private static BindableClothing<IFacingFrontParameters> RagsInstance = ClothingBuilder.CreateV2<IFacingFrontParameters>(builder =>
    {
        // Similar to setup in Race
        // Setup is only ran once. Think of it as a constructor.
        // It's not required, but it's almost always needed
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.RevealsDick = true;
        });

        // Unlike the Race renderer, this one does all the sprites in one go. 
        // The String ID (or name) of the sprite is only used within this function.
        // The names cannot conflict with names of other clothing pieces, and there are no naming rules.
        // "Clothing1" "Clothing2" are legacy names from old code. I recommend using more descriptive naming (e.g. Torso, Belly, Bra, Sleeves) 
        // There is no limitation on the number of sprites. 
        builder.RenderAll((input, output, extra) =>
        {
            output["Clothing2"].Layer(11);
            output["Clothing1"].Layer(10);

            if (extra.FacingFront)
            {
                output["Clothing1"].Sprite(input.Sprites.Kobolds[63]);
                output["Clothing2"].Sprite(input.Sprites.Kobolds[66]);
            }
            else
            {
                output["Clothing1"].Sprite(input.Sprites.Kobolds[65]);
                output["Clothing2"].Sprite(null); // This is optional. 
                // Some properties set in Setup can overwritten
                // Note that this only affects this render cycle, and will not persist
                output.RevealsDick = false;
            }

            // Clothing items can modify the rendering of the owner.
            // Currently, if multiple items modify the same race rendering property,
            // the behavior is underfined. Avoid it because the results can change with updates
            // or with code reorganization. This might be addressed with overwrite priority down the line.
            output.ChangeSprite(SpriteType.Balls).Sprite(input.Sprites.Kobolds[111]);
            output.ChangeSprite(SpriteType.Balls).Layer(15);
        });
    });
}