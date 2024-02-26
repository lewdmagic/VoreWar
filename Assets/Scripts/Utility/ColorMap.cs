using UnityEngine;

public static class ColorMap
{
    private enum ColorEnum
    {
        White,
        Tone1,
        Tone2,
        Tone3,
        Tone4,
        Tone5,
        Brown,
        Red,
        Pink,
        Yellow,
        Golden,
        Orange,
        DarkGray,
        OffWhite,
        DarkBlue,
        DarkerBlue,
        DarkGreen,
        DarkerGreen,
        DarkPurple,
        DarkerPurple,
        Blue,
        LightBlue,
        LightGreen,
        LightPurple,
        RedOrange,
    }

    private static Color[] _colors;

    private static Color[] _hairColors;
    private static Color[] _skinColors;
    private static Color[] _bodyAccesoryColors;
    private static Color[] _lizardColors;
    private static Color[] _slimeColors;

    private static Color[] _earthyColors;

    private static Color[] _eyeColors;
    private static Color[] _clothingColors;

    private static Color[] _impSkinColors;
    private static Color[] _impSecondaryColors;
    private static Color[] _impHairColors;
    private static Color[] _impScleraColors;

    private static Color[] _vagrantColors;

    private static Color[] _sharkColors;
    private static Color[] _sharkBellyColors;
    private static Color[] _pastelColors;

    private static Color[] _wyvernColors;
    private static Color[] _wyvernBellyColors;

    private static Color[] _schiwardezColors;

    private static Color[] _exoticColors;

    private static Color[] _dratopyrMainColors;
    private static Color[] _dratopyrWingColors;
    private static Color[] _dratopyrFleshColors;
    private static Color[] _dratopyrEyeColors;

    static ColorMap()
    {
        _colors = new Color[25];
        _colors[(int)ColorEnum.White] = new Color(1.0f, 1.0f, 1.0f);
        _colors[(int)ColorEnum.Tone1] = new Color(1f, .8f, .643f);
        _colors[(int)ColorEnum.Tone2] = new Color(1f, .757f, .565f);
        _colors[(int)ColorEnum.Tone3] = new Color(.914f, .725f, .584f);
        _colors[(int)ColorEnum.Tone4] = new Color(.875f, .694f, .459f);
        _colors[(int)ColorEnum.Tone5] = new Color(.804f, .631f, .518f);
        _colors[(int)ColorEnum.Brown] = new Color(.5f, .4f, .25f);
        _colors[(int)ColorEnum.Red] = new Color(.7f, .1f, 0);
        _colors[(int)ColorEnum.Pink] = new Color(.9f, .4f, .4f);
        _colors[(int)ColorEnum.Yellow] = new Color(.9f, .9f, .4f);
        _colors[(int)ColorEnum.Golden] = new Color(.5f, .5f, .1f);
        _colors[(int)ColorEnum.Orange] = new Color(.9f, .6f, .0f);
        _colors[(int)ColorEnum.DarkGray] = new Color(.2f, .2f, .2f);
        _colors[(int)ColorEnum.OffWhite] = new Color(.9f, .9f, .9f);
        _colors[(int)ColorEnum.DarkBlue] = new Color(.3f, .3f, .85f);
        _colors[(int)ColorEnum.DarkerBlue] = new Color(.15f, .15f, .75f);
        _colors[(int)ColorEnum.DarkGreen] = new Color(0, .45f, 0);
        _colors[(int)ColorEnum.DarkerGreen] = new Color(0, .3f, 0);
        _colors[(int)ColorEnum.DarkPurple] = new Color(.4f, 0.1f, .8f);
        _colors[(int)ColorEnum.DarkerPurple] = new Color(.325f, 0.5f, .65f);
        _colors[(int)ColorEnum.Blue] = new Color(.3f, .3f, .8f);
        _colors[(int)ColorEnum.LightBlue] = new Color(.6f, .6f, .9f);
        _colors[(int)ColorEnum.LightGreen] = new Color(.6f, .9f, .6f);
        _colors[(int)ColorEnum.LightPurple] = new Color(.7f, .4f, .9f);
        _colors[(int)ColorEnum.RedOrange] = new Color(.92f, .45f, .03f);

        _hairColors = new Color[]
        {
            _colors[(int)ColorEnum.Brown],
            _colors[(int)ColorEnum.Pink],
            _colors[(int)ColorEnum.Red],
            _colors[(int)ColorEnum.Yellow],
            _colors[(int)ColorEnum.Golden],
            _colors[(int)ColorEnum.Orange],
            _colors[(int)ColorEnum.DarkGray],
            _colors[(int)ColorEnum.OffWhite],
            _colors[(int)ColorEnum.RedOrange],
        };

        _bodyAccesoryColors = new Color[]
        {
            _colors[(int)ColorEnum.Brown],
            _colors[(int)ColorEnum.Pink],
            _colors[(int)ColorEnum.Red],
            _colors[(int)ColorEnum.Yellow],
            _colors[(int)ColorEnum.Golden],
            _colors[(int)ColorEnum.Orange],
            _colors[(int)ColorEnum.DarkGray],
            _colors[(int)ColorEnum.OffWhite],
            _colors[(int)ColorEnum.RedOrange],
        };

        _skinColors = new Color[]
        {
            _colors[(int)ColorEnum.Tone1],
            _colors[(int)ColorEnum.Tone2],
            _colors[(int)ColorEnum.Tone3],
            _colors[(int)ColorEnum.Tone4],
            _colors[(int)ColorEnum.Tone5],
        };

        _lizardColors = new Color[]
        {
            _colors[(int)ColorEnum.DarkBlue],
            _colors[(int)ColorEnum.DarkerBlue],
            _colors[(int)ColorEnum.DarkGreen],
            _colors[(int)ColorEnum.DarkerGreen],
            _colors[(int)ColorEnum.DarkPurple],
            _colors[(int)ColorEnum.DarkerPurple],
            _colors[(int)ColorEnum.Brown],
            _colors[(int)ColorEnum.Golden],
        };

        _eyeColors = new Color[]
        {
            _colors[(int)ColorEnum.Brown],
            _colors[(int)ColorEnum.Pink],
            _colors[(int)ColorEnum.Red],
            _colors[(int)ColorEnum.Yellow],
            _colors[(int)ColorEnum.Golden],
            _colors[(int)ColorEnum.Orange],
            _colors[(int)ColorEnum.Blue],
            _colors[(int)ColorEnum.LightBlue],
            _colors[(int)ColorEnum.LightGreen],
            _colors[(int)ColorEnum.DarkGreen],
            _colors[(int)ColorEnum.LightPurple],
            new Color(.22f, .95f, 1),
        };


        _slimeColors = new Color[]
        {
            _colors[(int)ColorEnum.Blue],
            _colors[(int)ColorEnum.LightBlue],
            _colors[(int)ColorEnum.DarkGreen],
            _colors[(int)ColorEnum.LightGreen],
            _colors[(int)ColorEnum.LightPurple],
            _colors[(int)ColorEnum.Pink],
            _colors[(int)ColorEnum.Red],
            _colors[(int)ColorEnum.Orange],
            _colors[(int)ColorEnum.DarkGray],
            new Color(.96f, .94f, .92f),
        };


        _earthyColors = new Color[]
        {
            new Color(.29f, .21f, .15f),
            new Color(.50f, .42f, .34f),
            new Color(.66f, .63f, .53f),
            new Color(.73f, .61f, .38f),
            new Color(.73f, .70f, .47f),
        };

        _clothingColors = new Color[]
        {
            _colors[(int)ColorEnum.Blue],
            _colors[(int)ColorEnum.LightGreen],
            _colors[(int)ColorEnum.DarkGreen],
            _colors[(int)ColorEnum.LightBlue],
            _colors[(int)ColorEnum.DarkBlue],
            _colors[(int)ColorEnum.LightPurple],
            _colors[(int)ColorEnum.DarkPurple],
            _colors[(int)ColorEnum.Brown],
            _colors[(int)ColorEnum.Pink],
            _colors[(int)ColorEnum.Red],
            _colors[(int)ColorEnum.Yellow],
            _colors[(int)ColorEnum.Golden],
            _colors[(int)ColorEnum.Orange],
            _colors[(int)ColorEnum.DarkGray],
            _colors[(int)ColorEnum.OffWhite],
        };

        _impSkinColors = new Color[]
        {
            new Color(.75f, .85f, .85f),
            new Color(.85f, .77f, .75f),
            new Color(.86f, .92f, .75f),
            new Color(1f, .91f, 1f),
            _colors[(int)ColorEnum.OffWhite],
            new Color(.86f, .96f, 1f),
            new Color(1f, 1f, .79f),
        };

        _impSecondaryColors = new Color[]
        {
            new Color(.22f, .14f, .26f),
            new Color(.26f, .14f, .38f),
            new Color(.29f, 0, .04f),
            new Color(.09f, .17f, .2f),
            new Color(.43f, 0, .22f),
            new Color(.19f, .19f, .19f),
        };

        _impHairColors = new Color[]
        {
            new Color(.32f, .32f, .32f),
            new Color(.91f, .39f, .1f),
            new Color(1f, .98f, .61f),
            new Color(.89f, 0, .48f),
            new Color(1f, .98f, .61f),
            _colors[(int)ColorEnum.White],
            new Color(.61f, .25f, .06f),
            new Color(0, .69f, .2f),
        };

        _impScleraColors = new Color[]
        {
            Color.white,
            new Color(1f, .98f, .61f),
            new Color(.9f, .9f, .5f),
            new Color(.9f, .8f, .6f),
            new Color(1f, .66f, .61f),
            _colors[(int)ColorEnum.Red],
        };


        _vagrantColors = new Color[]
        {
            new Color(1f, .66f, .66f),
            new Color(.85f, .66f, 1f),
            new Color(.66f, .73f, 1f),
            new Color(.66f, 1f, .85f),
            new Color(.85f, 1f, .66f),
            new Color(1, .80f, .66f),
        };

        _sharkColors = new Color[]
        {
            new Color(.3f, .8f, 1f),
            new Color(.7f, .9f, 1f),
            new Color(.5f, .8f, 1f),
            new Color(.8f, .7f, .3f),
            new Color(.8f, .3f, .3f),
            new Color(.1f, .3f, .5f),
            new Color(.4f, .4f, .4f),
            new Color(.2f, .7f, .5f),
            new Color(1f, 1f, 1f)
        };

        _sharkBellyColors = new Color[]
        {
            new Color(1f, 1f, 1f),
            new Color(.9f, .9f, .9f),
            new Color(.8f, .8f, .8f),
            new Color(.6f, .6f, .6f)
        };

        _pastelColors = new Color[]
        {
            new Color(.93f, 1f, .965f),
            new Color(.92f, 1f, 1f),
            new Color(.92f, .965f, 1f),
            new Color(.84f, .84f, .975f),
            new Color(1f, .88f, .93f),
            new Color(1f, .84f, .8f),
            new Color(1f, .975f, .75f),
            new Color(.88f, 1f, .92f),
            new Color(1f, .95f, .92f),
            new Color(.95f, .55f, .95f),
        };


        _wyvernColors = new Color[]
        {
            new Color(.8f, .4f, .1f), // Flame
            new Color(.7f, .05f, 0f), // Crimson
            new Color(.2f, .2f, .8f), // Blue
            new Color(.5f, .6f, 1f), // Sky
            new Color(.25f, .25f, .3f), // Black
            new Color(.1f, .4f, .1f), // Deep Green
            new Color(.5f, .2f, .9f), // Purple
            new Color(1f, 1f, 0f), // Yellow
            new Color(.5f, .8f, .5f), // Pale Green
            new Color(.8f, .5f, .5f), // Rose Red
            new Color(.92f, .95f, .96f), // Dust
        };

        _wyvernBellyColors = new Color[] // Lighter versions of the main wyvern colours.
        {
            new Color(1f, .6f, .2f), // Flame
            new Color(1f, .15f, .05f), // Crimson
            new Color(.3f, .3f, 1f), // Blue
            new Color(.6f, .75f, 1f), // Sky
            new Color(.6f, .6f, .7f), // Black
            new Color(.2f, .6f, .2f), // Deep Green
            new Color(.6f, .3f, 1f), // Purple
            new Color(.6f, 1f, .6f), // Pale Green
            new Color(1f, .6f, .6f), // Rose Red
            new Color(.94f, .96f, .98f), // Dust
        };

        _exoticColors = new Color[]
        {
            new Color(.6f, 0f, 1f),
            new Color(.5f, .9f, 0f),
            new Color(1f, .9f, 0f),
            new Color(1f, 0f, 0f),
            new Color(0f, .8f, .7f),
            new Color(.5f, .9f, 0f),
            new Color(.2f, 0f, .8f),
            new Color(1f, .5f, 0f)
        };

        _schiwardezColors = new Color[]
        {
            new Color(.8f, .5f, 0f),
            new Color(.5f, .5f, .5f),
            new Color(.2f, .2f, .25f),
            new Color(0f, 0f, .8f),
            new Color(0f, .7f, .7f),
            new Color(.8f, 0f, 0f)
        };

        _dratopyrMainColors = new Color[]
        {
            new Color(.4f, .4f, 1f),
            new Color(.4f, 1f, .4f),
            new Color(1f, .4f, .4f),
            new Color(1f, 1f, 1f),
            new Color(.8f, .8f, .8f),
            new Color(.2f, .2f, .9f),
            new Color(.2f, .9f, .2f),
            new Color(.9f, .2f, .2f),
            new Color(.6f, 0f, 1f),
            new Color(.4f, 0f, .9f)
        };

        _dratopyrWingColors = new Color[]
        {
            new Color(.2f, .2f, .8f),
            new Color(.2f, .8f, .2f),
            new Color(.8f, .2f, .2f),
            new Color(.1f, .1f, .7f),
            new Color(.1f, .7f, .1f),
            new Color(.7f, .1f, .1f),
            new Color(.4f, 0f, .8f),
            new Color(.2f, 0f, .7f)
        };

        _dratopyrFleshColors = new Color[]
        {
            new Color(.3f, .3f, 1f),
            new Color(1f, .3f, .3f),
            new Color(.6f, 0f, 1f),
            new Color(1f, .9f, 0f)
        };

        _dratopyrEyeColors = new Color[]
        {
            new Color(1f, 1f, 1f),
            new Color(.6f, 0f, 0f),
            new Color(0f, 0f, 0f)
        };
    }

    public static Color GetColor(Color[] type, int index)
    {
        if (index < type.Length) return type[index];
        return type[0];
    }

    public static Color GetHairColor(int i) => GetColor(_hairColors, i);
    public static Color GetEyeColor(int i) => GetColor(_eyeColors, i);
    public static Color GetSkinColor(int i) => GetColor(_skinColors, i);
    public static Color GetBodyAccesoryColor(int i) => GetColor(_bodyAccesoryColors, i);
    public static Color GetLizardColor(int i) => GetColor(_lizardColors, i);
    public static Color GetSlimeColor(int i) => GetColor(_slimeColors, i);
    public static Color GetEarthyColor(int i) => GetColor(_earthyColors, i);
    public static Color GetImpSkinColor(int i) => GetColor(_impSkinColors, i);
    public static Color GetImpSecondaryColor(int i) => GetColor(_impSecondaryColors, i);
    public static Color GetImpHairColor(int i) => GetColor(_impHairColors, i);
    public static Color GetImpScleraColor(int i) => GetColor(_impScleraColors, i);
    public static Color GetClothingColor(int i) => GetColor(_clothingColors, i);
    public static Color GetVagrantColor(int i) => GetColor(_vagrantColors, i);
    public static Color GetSharkColor(int i) => GetColor(_sharkColors, i);
    public static Color GetSharkBellyColor(int i) => GetColor(_sharkBellyColors, i);
    public static Color GetPastelColor(int i) => GetColor(_pastelColors, i);
    public static Color GetWyvernColor(int i) => GetColor(_wyvernColors, i);
    public static Color GetWyvernBellyColor(int i) => GetColor(_wyvernBellyColors, i);
    public static Color GetExoticColor(int i) => GetColor(_exoticColors, i);
    public static Color GetSchiwardezColor(int i) => GetColor(_schiwardezColors, i);

    public static Color GetDratopyrMainColor(int i) => GetColor(_dratopyrMainColors, i);
    public static Color GetDratopyrWingColor(int i) => GetColor(_dratopyrWingColors, i);
    public static Color GetDratopyrFleshColor(int i) => GetColor(_dratopyrFleshColors, i);
    public static Color GetDratopyrEyeColor(int i) => GetColor(_dratopyrEyeColors, i);


    public static int HairColorCount => _hairColors.Length;
    public static int EyeColorCount => _eyeColors.Length;
    public static int SkinColorCount => _skinColors.Length;
    public static int BodyAccesoryColorCount => _bodyAccesoryColors.Length;
    public static int LizardColorCount => _lizardColors.Length;
    public static int SlimeColorCount => _slimeColors.Length;
    public static int EarthyColorCount => _earthyColors.Length;
    public static int ImpSkinColorCount => _impSkinColors.Length;
    public static int ImpSecondaryColorCount => _impSecondaryColors.Length;
    public static int ImpHairColorCount => _impHairColors.Length;
    public static int ImpScleraColorCount => _impScleraColors.Length;
    public static int ClothingColorCount => _clothingColors.Length;
    public static int VagrantColorCount => _vagrantColors.Length;
    public static int SharkColorCount => _sharkColors.Length;
    public static int SharkBellyColorCount => _sharkBellyColors.Length;
    public static int PastelColorCount => _pastelColors.Length;
    public static int WyvernColorCount => _wyvernColors.Length;
    public static int WyvernBellyColorCount => _wyvernBellyColors.Length;
    public static int ExoticColorCount => _exoticColors.Length;
    public static int SchiwardezColorCount => _schiwardezColors.Length;

    public static int DratopyrMainColorCount => _dratopyrMainColors.Length;
    public static int DratopyrWingColorCount => _dratopyrWingColors.Length;
    public static int DratopyrFleshColorCount => _dratopyrFleshColors.Length;
    public static int DratopyrEyeColorCount => _dratopyrEyeColors.Length;

    internal static Color Redify(Color color, float fraction)
    {
        color.r = 1 - (1 - color.r) * fraction;
        color.g = color.g * (1 - fraction);
        color.b = color.b * (1 - fraction);
        return color;
    }

    internal static Color Bluify(Color color, float fraction)
    {
        color.r = color.r * (1 - fraction);
        color.g = color.g * (1 - fraction);
        color.b = 1 - (1 - color.b) * fraction;
        return color;
    }

    internal static Color Darken(Color color, float fraction)
    {
        color.r = color.r * (1 - fraction);
        color.g = color.g * (1 - fraction);
        color.b = color.b * (1 - fraction);
        return color;
    }

    internal static Color Lighten(Color color, float fraction)
    {
        color.r = 1 - (1 - color.r) * (1 - fraction);
        color.g = 1 - (1 - color.g) * (1 - fraction);
        color.b = 1 - (1 - color.b) * (1 - fraction);
        return color;
    }
}