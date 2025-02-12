# VoreWar
This game is a continuation of darkevilme's game Vore War. It is a turn based tactical game with rpg elements in a vore related world.

Check out [The Eka's thread](https://aryion.com/forum/viewtopic.php?f=79&t=65398) by @Flame_Valxsarion for more information! 

# VoreWar Experimental

This branch contains a lot of modifications to the code base.

Major changes at a glance:

* The code relating to Races has been rewritten almost entirely.
* Custom races feature that allows adding new races coded with Lua without recompiling the game.
* Wide encompassing code cleanup and normalization.
* **Upgraded Unity version to 2022.3.5f**
* Many bugs fixed.
* Many bugs added.

## Custom Races

Custom Races is an experimental feature that allows users to add or modify races. Adding a folder with appropriate files is all it takes to add a race.

Custom races are coded with Lua scripting language 5.2. API.lua file located in `Tools/Lua/API.lua` contains the definitions for the custom races lua API, adding it to your editor will provide autocompletion and type checking. It's not required but *highly* recommended.

### Set up your editor

1. Download Visual Studio Code https://code.visualstudio.com/.
2. Install extension called `Lua` by sumneko.
3. Open extension settings, search `@ext:sumneko.lua workspace library`, click `Add Item` and paste the path to API.lua


### Creating custom races

The name of the folder serves as the ID of the race. To keep IDs from colliding, the race id is made up of 2 parts separated by a period. First part is the creator's name and the second is the race name such as: `jackson.human`

The fastest way to create a new race is to copy an existing one! For example, you can copy the folder `greenslime.equine` and name it `jamie.equine`. You just created a new custom race. Next time you start the game, it will show up as a separate option. Of course it's a carbon copy, but you can immediately begin to modify and experiment with it.

Your `jamie.equine` folder will contain the following items:

* `sprites` folder should contain png files.
* `clothing` folder should contain a folder for each piece of clothing (more on this later)
* `race.lua` primary race script.
* `banner.png` the banner sprite.

### Folder Layout

```
CustomRaces
├───jamie.human 📁
│   ├───race.lua
│   ├───banner.png
│   ├───sprites 📁
│   │   ├───body_001.png
│   │   ├───body_002.png
│   │   └───body_003.png
│   ├───palettes 📁
│   │   ├───skin.png
│   │   ├───hair.png
│   └───clothing 📁
│       ├───pants1 📁
│       │   ├───clothing.lua
│       │   └───sprites 📁
│       │       ├───top_part.png 
│       │       └───bottom_part.png
│       └───pants2 📁
│           ├───clothing.lua
│           └───sprites 📁
│               ├───top_part.png
│               └───bottom_part.png
├───jamie.cat 📁
│   ... (same as above)

```

## race.lua

race.lua is the script file that defines a race.

It must contain 4 items:

1. The API version at the top of the file. The latest version is 0.1.0.
```
API_VERSION = "0.1.0"
```
2. A setup function. It's called once. You can use the output variable to customize the race.
```lua 
---@param input ISetupInput
---@param output ISetupOutput
function Setup(input, output)
    
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
---@param output IRandomCustomOutput
function Randomize(input, output)
    
end 
```


## Render in depth

The render function is called with two parameters: input and output.

The input variable gives you access to various data needed to make decisions. The output variable allows you to provide rendering instructions.

the cornerstone is the NewSprite method belonging to output

```lua
local headSprite = output.NewSprite(SpriteType.Head, 5)
```

This creates a sprite handle for us, with the specified SpriteType and layer.
Now, we need to tell it what image to render. The very basic one is just Sprite() which takes a string.
```lua
headSprite.Sprite("head");
```

That's all you need to render head.png from the sprites folder.

The sprite method has a few variations for convenience.

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
headSprite.Palette("skin", 6);;
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

Various race attributes can be in the Setup function.

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

## Clothing

Clothing works in a very similar way to custom races. Each race folder must contain the following:

* `clothing.lua` primary race script.
* `sprites` folder should contain png files.

### clothing.lua

clothing.lua must contain 3 items:

1. The API version at the top of the file. The only version right now is 0.0.1, so don't worry about this.
```
API_VERSION = "0.0.1"
```
2. A setup function. It's called once. You can use the output variable to customize the clothing.
```lua 
---@param input IClothingSetupInput
---@param output IClothingSetupOutput
function Setup(input, output)
    
end
```
3. A render function. This function is called every update. It tells the game what sprites to render.
```lua 
---@param input IClothingRenderInput
---@param output IClothingRenderOutput
function Render(input, output)
    
end
```

### Example

```lua 
API_VERSION = "0.0.1"

---@param input IClothingSetupInput
---@param output IClothingSetupOutput
function Setup(input, output)
    output.DiscardUsesPalettes = true;
end

---@param input IClothingRenderInput
---@param output IClothingRenderOutput
function Render(input, output)
    output.DisableDick();

    output.NewSprite("main", 15)
          .Sprite(input.Sex, ternary(input.A.HasBelly, "hasbelly", "nobelly"))
          .Palette("clothing", input.U.ClothingColor);
end
```

## Palettes

Custom races support palettes to add differently colored variations of sprites. To add a palette, add a PNG file to the `palettes` folder of a custom race. 

The first row of pixels of the palette define the template colors that will be replaced. 

This example palette has top row pixels with red values 50, 100, 150, 200, 250. Only the red value of the pixel matters, you may also use shades of white like RGB(50, 50, 50), RGB(100, 100, 100) etc. 

The rows after first one define the replacement colors. Each row is a separate set.

![Palette example](Docs/Img/palette_example.png)

### Example

This is a sprite that's made up of pixels with red values of 50, 100, 150, 200, 250.  

![Palette example](Docs/Img/arm_template.png)

If the example palette from above is applied to it with index 5, the reds will be replaced with corresponding colors from the bottom (7th) row.

![Palette example](Docs/Img/arm_white.png)


If it's applied with index 3, the reds will be replaced with corresponding colors from the 5th row.

![Palette example](Docs/Img/arm_redish.png)

Apply a palette to a sprite with the `.Palette(string name, int index)` method. The first argument `name` is the name of the PNG file defining a palette.
The following snippet will replace the red template sprite with colors from the 7th row (index 5). 

```lua
output.NewSprite(SpriteType.Head, 5)
      .Sprite("arm")
      .Palette("example_palette", 5);
```

### Global Palettes

Palettes can be registered to be available to all custom races by placing them in `GameData/Palettes`. You can access them the same way you access palettes placed within custom races. If a global palette and a palette in a custom race share a name, the race specific palette takes priority. 