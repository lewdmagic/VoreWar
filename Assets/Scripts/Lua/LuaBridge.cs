using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MoonSharp.Interpreter;
using UnityEngine;



public static class FlavorEntryMaker
{
    public static FlavorEntry New(string text, double weight, Gender? gender)
    {
        return new FlavorEntry(text, weight, gender);
    }

    public static FlavorEntry New(string text)
    {
        return New(text, 1, null);
    }
    public static FlavorEntry NewGendered(string text, Gender gender)
    {
        return New(text, 1, gender);
    }
}


public static class LuaBridge
{
    static LuaBridge()
    {
        UserData.RegisterType<OverSizeParameters>();
        
        UserData.RegisterType<Action>();
        UserData.RegisterType<IParameters>();
        UserData.RegisterType<IRaceRenderInput>();
        UserData.RegisterType<IRaceRenderOutput>();
        UserData.RegisterType<Sprite>();
        UserData.RegisterType<SpriteType>();
        UserData.RegisterType<SpriteDictionary>();
        UserData.RegisterType<IRaceBuilder>();
        UserData.RegisterType<SwapType>();
        UserData.RegisterType<Actor_Unit>();
        UserData.RegisterType<Unit>();
        UserData.RegisterType<ColorSwapPalette>();
        UserData.RegisterType<IRandomCustomInput>();
        UserData.RegisterType<IRunInput>();
        UserData.RegisterType<Gender>();
        UserData.RegisterType<IOverSizeParameters>();
        UserData.RegisterType<PredatorComponent>();
        UserData.RegisterType<Vector2>();
        UserData.RegisterType<Vector3>();
        UserData.RegisterType<TraitType>();
        UserData.RegisterType<Stat>();
        UserData.RegisterType<RaceTraits>();
        UserData.RegisterType<IRaceTraits>();
        
        UserData.RegisterType<IClothing>();
        
        UserData.RegisterType<ButtonType>();
        UserData.RegisterType<CustomizerButton>();
        UserData.RegisterType<EnumIndexedArray<ButtonType, CustomizerButton>>();
        
        UserData.RegisterType<ButtonCustomizer>();
        UserData.RegisterType<IButtonCustomizer>();
        
        UserData.RegisterType<Texts>();
        UserData.RegisterType<FlavorText>();
        UserData.RegisterType<FlavorType>();
        UserData.RegisterType<FlavorEntry>();
        UserData.RegisterType<Item>();
        UserData.RegisterType<Weapon>();
        UserData.RegisterType<Accessory>();
        UserData.RegisterType<SpellBook>();
            
        UserData.RegisterType<IClothingSetupInput>();
        UserData.RegisterType<IClothingSetupOutput>();
        UserData.RegisterType<ClothingMiscData>();
        
        
        UserData.RegisterType<IClothingRenderInput>();
        UserData.RegisterType<IClothingRenderOutput>();
        UserData.RegisterType<ClothingRenderOutput>();
        UserData.RegisterType<ClothingRenderInput>();
        
        
        UserData.RegisterType<BindableClothing<IOverSizeParameters>>();
        UserData.RegisterType<BindableClothing<OverSizeParameters>>();
        UserData.RegisterType<SetupOutput>();
        
        UserData.RegisterType<IList>();
        UserData.RegisterType<IList<IClothing>>();
        UserData.RegisterType<List<IClothing>>();
        UserData.RegisterType<IRaceRenderAllOutput>();
        
        LuaUtil.RegisterSimpleAction<IRandomCustomInput>();
        LuaUtil.RegisterSimpleAction<RaceTraits>();
        LuaUtil.RegisterSimpleAction<IRaceTraits>();
        
        LuaUtil.RegisterSimpleAction<Unit, EnumIndexedArray<ButtonType, CustomizerButton>>();
        LuaUtil.RegisterSimpleAction<Unit, ButtonCustomizer>();
        LuaUtil.RegisterSimpleAction<Unit, IButtonCustomizer>();
        LuaUtil.RegisterSimpleAction<IUnitRead, IButtonCustomizer>();
        LuaUtil.RegisterSimpleAction<IClothingSetupInput, IClothingSetupOutput>();
        LuaUtil.RegisterSimpleAction<IClothingSetupInput, ClothingMiscData>();
        
        LuaUtil.RegisterSimpleAction<IClothingRenderInput, IClothingRenderOutput>();
        LuaUtil.RegisterSimpleAction<IClothingBuilder<OverSizeParameters>>();
        
        LuaUtil.RegisterSimpleFunc<int>();
        LuaUtil.RegisterSimpleFunc<string, IClothing>();
        LuaUtil.RegisterSimpleFunc<string, Func<IClothingRenderInput, Table>, IClothing>();
        LuaUtil.RegisterSimpleFunc<IClothingRenderInput, Table>();
        
        LuaUtil.RegisterSimpleFunc<float, float, Vector2>();
        LuaUtil.RegisterSimpleFunc<float, float, float, Vector3>();
        
        LuaUtil.RegisterSimpleAction<string>();
    }


    private static void RegisterShared(Script script)
    {
        script.Globals["Log"] = (Action<string>) Debug.Log;
        
        script.Globals["Gender"] = UserData.CreateStatic<Gender>();
        script.Globals["SpriteType"] = UserData.CreateStatic<SpriteType>();
        script.Globals["SwapType"] = UserData.CreateStatic<SwapType>();

        script.Globals["GetPaletteCount"] = (Func<SwapType, int>) ColorPaletteMap.GetPaletteCount;
        script.Globals["GetPalette"] = (Func<SwapType, int, ColorSwapPalette>) ColorPaletteMap.GetPalette;
        Func<float, float, float, Vector3> newVector3 = (x, y, z) => new Vector3(x, y, z);
        script.Globals["NewVector3"] = newVector3;
        
        Func<float, float, Vector2> newVector2 = (x, y) => new Vector2(x, y);
        script.Globals["NewVector2"] = newVector2;


        LuaUtil.RegisterStatic(script, "Config", typeof(Config));

        Dictionary<string, dynamic> defaults = new Dictionary<string, dynamic>
        {
            ["Finalize"] = Defaults.Finalize,
            ["RandomCustom"] = Defaults.RandomCustom,
            ["BasicBellyRunAfter"] = Defaults.BasicBellyRunAfter
        };
        
        script.Globals["Defaults"] = defaults;

        Func<int, int> RandomInt = (max) => State.Rand.Next(max);
        script.Globals["RandomInt"] = RandomInt;
        
        script.DoString(@"
function ternary ( cond , T , F )
    if cond then return T else return F end
end");
        
        script.DoString(@"
function Ternary ( cond , T , F )
    if cond then return T else return F end
end");
    }


    private static void RegisterRace(Script script, string raceId)
    {
        script.Globals["TraitType"] = UserData.CreateStatic<TraitType>();
        script.Globals["ButtonType"] = UserData.CreateStatic<ButtonType>();
        script.Globals["Stat"] = UserData.CreateStatic<Stat>();
        script.Globals["FlavorType"] = UserData.CreateStatic<FlavorType>();


        LuaUtil.RegisterStatic(script, "FlavorEntryMaker", typeof(FlavorEntryMaker));
        
        Func<Texts> newTextsBasic = () => new Texts();
        script.Globals["NewTextsBasic"] = newTextsBasic;
        
        Func<string, FlavorEntry> newFlavorEntry = (text) => new FlavorEntry(text);
        script.Globals["NewFlavorEntry"] = newFlavorEntry;
        
        Func<string, Gender, FlavorEntry> newFlavorEntryGendered = (text, gender) => new FlavorEntry(text, gender);
        script.Globals["NewFlavorEntryGendered"] = newFlavorEntryGendered;
        
        Func<Texts, Texts, Texts, Dictionary<string, string>, FlavorText> newFlavorText = (preyDescriptions, predDescriptions, raceSingleDescriptions, weaponNames) => new FlavorText(preyDescriptions, predDescriptions, raceSingleDescriptions, weaponNames);
        script.Globals["NewFlavorText"] = newFlavorText;
        
        Func<string, IClothing> getClothing = (id) =>
        {
            return GameManager.customManager.GetRaceClothing(raceId, id);
        };

        script.Globals["GetClothing"] = getClothing;
        
        Func<string, Func<IClothingRenderInput, Table>, IClothing> getClothing2 = (id, calcFunc) =>
        {
            Func<IClothingRenderInput, Table> realCalcFunc = (input) =>
            {
                try
                {
                    Table calculatedTable = calcFunc.Invoke(input);
                    // Passing null as script parameter makes this table usable in any script
                    Table detachedFromScriptTable = new Table(null);

                    // TODO likely possible to optimize.
                    // This is done to detach the result table from race script
                    // so it can be passed to clothing
                    foreach (var pair in calculatedTable.Pairs)
                    {
                        detachedFromScriptTable.Set(pair.Key, pair.Value);
                    }
                    return detachedFromScriptTable;
                }
                catch (ScriptRuntimeException ex)
                {
                    Debug.LogError("Doh! An error occured! " + ex.Message);
                    Debug.LogError("Doh! An error occured! " + ex.DecoratedMessage);
                    throw;
                }
            };
            
            return GameManager.customManager.GetRaceClothing(raceId, id, realCalcFunc);
        };

        script.Globals["GetClothing2"] = getClothing2;
    }

    // Can be used later
    private static void RegisterClothing(Script script)
    {

    }
    
    
    internal static RaceScriptUsable RacePrep(string scriptCode, string raceId)
    {
        Script script = new Script();
        RegisterShared(script);
        RegisterRace(script, raceId);
		
        script.DoString(scriptCode, null, raceId + " - race.lua");

        object render = script.Globals["Render"];
        object setup = script.Globals["Setup"];
        object randomCustom = script.Globals["RandomCustom"];
        
        RaceScriptUsable scriptUsable = new RaceScriptUsable(
            (output) =>
            {
                try
                {
                    script.Call(setup, output);
                }
                catch (ScriptRuntimeException ex)
                {
                    Debug.LogError("Doh! An error occured! " + ex.DecoratedMessage);
                }
            },
            (input, output) =>
            {
                try
                {
                    script.Call(render, input, output);
                }
                catch (ScriptRuntimeException ex)
                {
                    Debug.LogError("Doh! An error occured! " + ex.Message);
                    Debug.LogError("Doh! An error occured! " + ex.DecoratedMessage);
                }
            },
            (output) =>
            {
                try
                {
                    script.Call(randomCustom, output);
                }
                catch (ScriptRuntimeException ex)
                {
                    Debug.LogError("Doh! An error occured! " + ex.Message);
                    Debug.LogError("Doh! An error occured! " + ex.DecoratedMessage);
                }
            }
        );

        return scriptUsable;
    }
	
	
    internal static ClothingScriptUsable ScriptPrepClothingFromCode(string scriptCode, string clothingId)
    {
        Script script = new Script();
        
        RegisterShared(script);
        RegisterClothing(script);
        
        script.DoString(scriptCode, null, clothingId + " - clothing.lua");
        
        object render = script.Globals["Render"];
        object setup = script.Globals["Setup"];
        
        ClothingScriptUsable clothingScriptUsable = new ClothingScriptUsable(
            (input, output) =>
            {
                script.Call(setup, input, output);
            },
            (input, output, extra) =>
            {
                try
                {
                    script.Call(render, input, output, extra);
                }
                catch (ScriptRuntimeException ex)
                {
                    Debug.Log("Doh! An error occured! " + ex.Message);
                    Debug.Log("Doh! An error occured! " + ex.DecoratedMessage);
                }
            }
        );

        return clothingScriptUsable;
    }
}

// Workaround for statics in stucts
public static class Colors
{
    public static readonly Color white = Color.white;
}