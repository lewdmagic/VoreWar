Custom Races is a new experimental feature that allows users to add or modify races. Adding a folder with appropriate files is all that's required to add a custom race.

Custom races are programmed with Lua scripting language 5.2. API.lua file contains the definitions for the custom race lua API, adding it to your editor will provide autocompletion and type checking. It's not required but *highly* recommended.

## Set up your editor

1. Download visual code https://code.visualstudio.com/.
2. Install extension called `Lua` by sumneko. 
3. Open extension settings, search `@ext:sumneko.lua workspace library`, click `Add Item` and paste the path to API.lua 


## Creating custom races

The name of the folder serves as the ID of the race. To keep IDs from colliding, the race id is made up of 2 parts separated by a period. First part is the creator's name and the second is the race name such as: `jackson.human`

The fastest way to create a new race is to simply copy an existing one! For example, you can copy the folder `magic.equines` and name it `jamie.equines`. You just created a new custom race. Next time you start the game, it will show up as a separate option. Of course it's a carbon copy, but you can immidiately begin to modify and experiment with it.

Your `jamie.equine` folder will containe the following items:

* `sprites` folder should contain png files.
* `clothing` folder should containt a folder for each piece of clothing (more on this later)
* `race.lua` primary race script.
* `banner.png` the banner sprite.  


# race.lua

race.lua is the pri

It must contain 4 items: 

1. The API version at the top of the file. The only version right now is 0.0.1, so don't worry about this.
```
API_VERSION = "0.0.1"
```
2. A setup function. It's called once. You can use the output variable to customize the race. 
```lua 
---@param output ISetupOutput
function Setup(output)
    
end 
```
3. A render function. This function is called every update. It tells the game what sprites to render. 
```lua 
---@param input IRaceRenderInput
---@param output IRaceRenderAllOutput
function Render(input, output)
    
end 
```
4. A randomizer function. It's called to randomize a unit. 
```lua 
---@param input IRandomCustomInput
function RandomCustom(input)
    
end 
```


## Render in depth

The render function is called with two parameters: input and output. 

The input variable gives you access to various data needed to make decisions.
The output variable allows you to provide rendering instructions. 

the cornerstone is the NewSprite method belonging to output

```lua
local headSprite = NewSprite(SpriteType.Head, 5)
```

This creates a sprite handle for us, with the specified SpriteType and layer. 
Now, we need to tell it what image to render. The very basic one is just Sprite() which takes a string.
```lua
headSprite.Sprite("head");
```

That's all you need to render head.png from the sprites folder. 

The sprite method has a few variations for conviniece. 

calling it with multiple arguments will join them with "_"
```lua
headSprite.Sprite("head", "female"); --renders head_female.png from /sprites
```

if the last argument is an integer, it will get converted to a 3 character string e.g. 5 -> 005, 44 -> 044. 
```lua
headSprite.Sprite("head", "female", 3); --renders head_female_003.png from /sprites
```

The special Sprite0 method offsets the number by +1. This comes in handy quite often since a lot of values are 0-indexed while the sprites are designed to start from 001. 
```lua
headSprite.Sprite0("head", "female", 3); --renders head_female_004.png from /sprites
```

Configure the coloring of the sprite. 
```lua
headSprite.Coloring(SwapType.HorseSkin, 5);
```

## Setup

Assign a singular and plural name for the race. 

#### Names
```lua
output.Names("Human", "Humans");
```

#### RaceTraits

Assign race traits for the race.

```lua
output.SetRaceTraits(function (traits)
    traits.BodySize = 10;
    traits.StomachSize = 16;
    traits.HasTail = true;
    traits.FavoredStat = Stat.Agility;
    traits.RacialTraits = {
        TraitType.Charge,
        TraitType.StrongMelee
    };
    traits.RaceDescription = "";
end)
```
#### FlavorText

Assign flavor texts for various categories. If more than 1 is provided, a random one will be picked every time it's needed.

```lua
output.SetFlavorText(FlavorType.RaceSingleDescription,
        NewFlavorEntry("equine"),
        NewFlavorEntry("bronco"),
        NewFlavorEntryGendered("mare", Gender.Female),
        NewFlavorEntryGendered("stallion", Gender.Male)
    );

output.SetFlavorText(FlavorType.WeaponMelee1, NewFlavorEntry("Push Dagger"));
```

#### CustomizeButtons

Adjust the displayed names for the character customizer buttons. Can also enable/disable them.

```lua
output.CustomizeButtons(function (unit, buttons)
    buttons.SetText(ButtonType.ClothingExtraType1, "Overtop");
    buttons.SetText(ButtonType.ClothingExtraType2, "Overbottom");
    buttons.SetText(ButtonType.BodyAccentTypes3,  "Skin Pattern");
    buttons.SetText(ButtonType.BodyAccentTypes4, "Head Pattern");
    buttons.SetText(ButtonType.BodyAccentTypes5, "Torso Color");
end);
```

#### TownNames

Set a list of town names for the race. 

```lua
    output.TownNames({
    "Cataphracta",
    "Equus",
    "The Ranch",
    "Haciendo",
    "Alfarsan"
});
```

#### Clothing

Create and add clothing items. 

```lua
output.AllowedMainClothingTypes.AddRange({-- undertops
    GetClothing("under_top_male_1"),
    GetClothing("under_top_male_2"),
    GetClothing("under_top_male_3"),
    GetClothing2("under_top_female_1", oversizeParamCalc),
    GetClothing2("under_top_female_2", oversizeParamCalc),
    GetClothing2("under_top_female_3", oversizeParamCalc),
    GetClothing2("under_top_female_4", oversizeParamCalc)
});

output.AllowedWaistTypes.AddRange({ -- underbottoms
    GetClothing("under_bottom_1"),
    GetClothing("under_bottom_2"),
    GetClothing("under_bottom_3"),
    GetClothing("under_bottom_4"),
    GetClothing("under_bottom_5")
});
```



#### Other Race Data

Various race attributes can be adjusted here. 

```lua
output.SpecialAccessoryCount = 0;
output.ClothingShift = NewVector3(0, 0, 0);
output.AvoidedEyeTypes = 0;
output.AvoidedMouthTypes = 0;

output.HairColors = GetPaletteCount(SwapType.UniversalHair);
output.HairStyles = 15;
output.SkinColors = GetPaletteCount(SwapType.HorseSkin);
output.AccessoryColors = GetPaletteCount(SwapType.HorseSkin);
output.EyeTypes = 4;
output.EyeColors = GetPaletteCount(SwapType.EyeColor);
output.SecondaryEyeColors = 1;
output.BodySizes = 0;

output.MouthTypes = 0;
output.AvoidedMainClothingTypes = 0;
output.TailTypes = 6;
output.BodyAccentTypes3 = 5;
output.BodyAccentTypes4 = 5;
output.BodyAccentTypes5 = 2;

output.ClothingColors = GetPaletteCount(SwapType.Clothing50Spaced);
output.ExtendedBreastSprites = true;
output.WholeBodyOffset = NewVector2(0, 16 * 0.625);
```









```
CustomRaces
- jackson.human
- - race.lua
- - banner.png
- - sprites
- - - sprite_1.png
- - - sprite_2.png
- - - sprite_3.png
- - clothing
- - - pants1
- - - - 


└───magic.equines
    ├───clothing
    │   ├───over_bottom_1
    │   │   └───sprites
    │   ├───over_bottom_2
    │   │   └───sprites
    │   ├───over_bottom_3
    │   │   └───sprites
    │   ├───over_top_1
    │   │   └───sprites
    │   ├───over_top_2
    │   │   └───sprites
    │   ├───under_bottom_1
    │   │   └───sprites
    │   ├───under_bottom_2
    │   │   └───sprites
    │   ├───under_bottom_3
    │   │   └───sprites
    │   ├───under_bottom_4
    │   │   └───sprites
    │   ├───under_bottom_5
    │   │   └───sprites
    │   ├───under_top_female_1
    │   │   └───sprites
    │   ├───under_top_female_2
    │   │   └───sprites
    │   ├───under_top_female_3
    │   │   └───sprites
    │   ├───under_top_female_4
    │   │   └───sprites
    │   ├───under_top_male_1
    │   │   └───sprites
    │   ├───under_top_male_2
    │   │   └───sprites
    │   └───under_top_male_3
    │       └───sprites
    └───sprites
```




