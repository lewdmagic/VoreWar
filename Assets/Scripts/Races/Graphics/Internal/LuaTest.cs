using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using Races.Graphics.Implementations.MainRaces;
using UnityEngine;


// public class Loader : ScriptLoaderBase
// {
//     public object LoadFile(string file, Table globalContext)
//     {
//         throw new NotImplementedException();
//     }
//
//     public string ResolveFileName(string filename, Table globalContext)
//     {
//         throw new NotImplementedException();
//     }
//
//     public string ResolveModuleName(string modname, Table globalContext)
//     {
//         throw new NotImplementedException();
//     }
// }


public static class Colors
{
    public static readonly Color white = Color.white;
}


public static class LuaTest
{

    internal static void MoonSharpTest()
    {
        string scriptCode = @"    
--builder.Setup(function (input, output)
    --output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.MermenSkin);
--end);

builder.RenderSingle(SpriteType.Body, 1, function (input, output)
    output.Sprite(input.Sprites.Whisp[1]);
end
);

--[[
builder.RenderSingle2(""Body"", 1, function (input, output)
	aValue = SpriteType.Body;
    --output.Sprite(input.Sprites.Whisp[1]);
end
);
]]--

--builder.RenderSingle(SpriteType.Body, 1, function (input, output)
    --output.Sprite(input.Sprites.Whisp[1]);
--end
--);

--builder.RunBefore(Defaults.Finalize);
--builder.RandomCustom(Defaults.RandomCustom);

		";

        //DynValue res = Script.RunString(scriptCode);
        
        ScriptHelper.RegisterSimpleAction();  
        
    }
}


internal class RaceScriptUsable
{
    internal Action<MiscRaceData> SetupFunc;
    internal Action<IRunInput, IRaceRenderAllOutput> Generator;
    internal Action<IRandomCustomInput> Value;

    internal RaceScriptUsable(Action<MiscRaceData> setupFunc, Action<IRunInput, IRaceRenderAllOutput> generator, Action<IRandomCustomInput> value)
    {
        SetupFunc = setupFunc;
        Generator = generator;
        Value = value;
    }
}

internal class ClothingScriptUsable
{
    internal Action<IClothingSetupInput, IClothingSetupOutput> SetMisc;
    internal Action<IClothingRenderInput, IClothingRenderOutput, Table> CompleteGen;

    public ClothingScriptUsable(Action<IClothingSetupInput, IClothingSetupOutput> setMisc, Action<IClothingRenderInput, IClothingRenderOutput, Table> completeGen)
    {
        SetMisc = setMisc;
        CompleteGen = completeGen;
    }
}

public static class ScriptHelper
{
    
    public static bool initted = false;
    
    static ScriptHelper()
    {
        // Register Types
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
        UserData.RegisterType<IRenderInput>();
        UserData.RegisterType<Gender>();
        UserData.RegisterType<IOverSizeParameters>();
        UserData.RegisterType<PredatorComponent>();
        UserData.RegisterType<Debug>();
        UserData.RegisterType<Vector2>();
        UserData.RegisterType<Vector3>();
        UserData.RegisterType<Traits>();
        UserData.RegisterType<Stat>();
        UserData.RegisterType<RaceTraits>();
        
        UserData.RegisterType<IClothing>();
        
        UserData.RegisterType<ButtonType>();
        UserData.RegisterType<CustomizerButton>();
        UserData.RegisterType<EnumIndexedArray<ButtonType, CustomizerButton>>();
        
        UserData.RegisterType<ButtonCustomizer>();
        
        UserData.RegisterType<TextsBasic>();
        UserData.RegisterType<FlavorText>();
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
        UserData.RegisterType<ClothingRenderInput>();
        UserData.RegisterType<ClothingRenderInput>();
        
        
        UserData.RegisterType<BindableClothing<IOverSizeParameters>>();
        UserData.RegisterType<BindableClothing<OverSizeParameters>>();
        UserData.RegisterType<MiscRaceData>();
        
        UserData.RegisterType<IList>();
        UserData.RegisterType<IList<IClothing>>();
        UserData.RegisterType<List<IClothing>>();
        UserData.RegisterType<IRaceRenderAllOutput>();
        
        ScriptHelper.RegisterSimpleAction<IRandomCustomInput>();
        ScriptHelper.RegisterSimpleAction<RaceTraits>();
        
        ScriptHelper.RegisterSimpleAction<Unit, EnumIndexedArray<ButtonType, CustomizerButton>>();
        ScriptHelper.RegisterSimpleAction<Unit, ButtonCustomizer>();
        ScriptHelper.RegisterSimpleAction<IClothingSetupInput, IClothingSetupOutput>();
        ScriptHelper.RegisterSimpleAction<IClothingSetupInput, ClothingMiscData>();
        
        ScriptHelper.RegisterSimpleAction<IClothingRenderInput, IClothingRenderOutput>();
        ScriptHelper.RegisterSimpleAction<IClothingBuilder<OverSizeParameters>>();
        
        ScriptHelper.RegisterSimpleFunc<int>();
        ScriptHelper.RegisterSimpleFunc<string, IClothing>();
        ScriptHelper.RegisterSimpleFunc<string, Func<IClothingRenderInput, Table>, IClothing>();
        ScriptHelper.RegisterSimpleFunc<IClothingRenderInput, Table>();
        
        ScriptHelper.RegisterSimpleFunc<float, float, Vector2>();
        ScriptHelper.RegisterSimpleFunc<float, float, float, Vector3>();
        
        ScriptHelper.RegisterSimpleAction<string>();

        initted = true;
    }
    
    internal static void ScriptPrep(string path, IRaceBuilder builder)
    {
        string scriptCode = File.ReadAllText(path);
        
        Script script = new Script();
        
        
        script.Globals["Log"] = (Action<string>) Debug.Log;

        #region Enums
        
        // Traits should be later renamed to Train to follow naming conventions
        // Set to Trait in script scrope to avoid breaking changes to scripts
        script.Globals["Trait"] = UserData.CreateStatic<Traits>();
        script.Globals["ButtonType"] = UserData.CreateStatic<ButtonType>();
        script.Globals["Gender"] = UserData.CreateStatic<Gender>();
        script.Globals["Stat"] = UserData.CreateStatic<Stat>();
        script.Globals["SpriteType"] = UserData.CreateStatic<SpriteType>();
        script.Globals["Gender"] = UserData.CreateStatic<Gender>();
        script.Globals["SwapType"] = UserData.CreateStatic<SwapType>();

        #endregion
        
        script.Globals["GetPaletteCount"] = (Func<SwapType, int>) ColorPaletteMap.GetPaletteCount;
        script.Globals["GetPalette"] = (Func<SwapType, int, ColorSwapPalette>) ColorPaletteMap.GetPalette;
        Func<float, float, float, Vector3> newVector3 = (x, y, z) => new Vector3(x, y, z);
        script.Globals["newVector3"] = newVector3;
        
        Func<float, float, Vector2> newVector2 = (x, y) => new Vector2(x, y);
        script.Globals["newVector2"] = newVector2;
        
        Func<TextsBasic> newTextsBasic = () => new TextsBasic();
        script.Globals["newTextsBasic"] = newTextsBasic;
        
        Func<TextsBasic, TextsBasic, TextsBasic, Dictionary<string, string>, FlavorText> newFlavorText = (preyDescriptions, predDescriptions, raceSingleDescriptions, weaponNames) => new FlavorText(preyDescriptions, predDescriptions, raceSingleDescriptions, weaponNames);
        script.Globals["newFlavorText"] = newFlavorText;

        RegisterStatic(script, "Config", typeof(Config));
        RegisterStatic(script, "Defaults", typeof(Defaults));
        RegisterStatic(script, "CommonRaceCode", typeof(CommonRaceCode));
        RegisterStaticFields(script, "HorseClothing", typeof(EquinesLua.HorseClothing));
        

        Dictionary<string, dynamic> defaults = new Dictionary<string, dynamic>
        {
            ["Finalize"] = Defaults.Finalize,
            ["RandomCustom"] = Defaults.RandomCustom,
            ["BasicBellyRunAfter"] = Defaults.BasicBellyRunAfter
        };
        
        script.Globals["Defaults"] = defaults;
        script.Globals["Finalize"] = Defaults.Finalize;

        Func<int, int> RandomInt = (max) => State.Rand.Next(max);
        script.Globals["RandomInt"] = RandomInt;
        
        script.Globals["builder"] = builder;
        
        script.DoString(@"
function ternary ( cond , T , F )
    if cond then return T else return F end
end");
		
        script.DoString(scriptCode);
    }
    
    internal static void ScriptPrep2(string path, string raceId)
    {
        string scriptCode = File.ReadAllText(path);
        ScriptPrep2FromCode(scriptCode, raceId);
    }
    
    internal static RaceScriptUsable ScriptPrep2FromCode(string scriptCode, string raceId)
    {
        
        Script script = new Script();
        
        
        script.Globals["Log"] = (Action<string>) Debug.Log;


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
                    Debug.Log("Doh! An error occured! " + ex.Message);
                    Debug.Log("Doh! An error occured! " + ex.DecoratedMessage);
                    throw;
                }
            };
            
            return GameManager.customManager.GetRaceClothing(raceId, id, realCalcFunc);
        };

        //Table table = Table();
        
        //DynValue

        script.Globals["GetClothing2"] = getClothing2;
        
        

        #region Enums
        
        // Traits should be later renamed to Train to follow naming conventions
        // Set to Trait in script scrope to avoid breaking changes to scripts
        script.Globals["Trait"] = UserData.CreateStatic<Traits>();
        script.Globals["ButtonType"] = UserData.CreateStatic<ButtonType>();
        script.Globals["Gender"] = UserData.CreateStatic<Gender>();
        script.Globals["Stat"] = UserData.CreateStatic<Stat>();
        script.Globals["SpriteType"] = UserData.CreateStatic<SpriteType>();
        script.Globals["Gender"] = UserData.CreateStatic<Gender>();
        script.Globals["SwapType"] = UserData.CreateStatic<SwapType>();

        #endregion
        
        script.Globals["GetPaletteCount"] = (Func<SwapType, int>) ColorPaletteMap.GetPaletteCount;
        script.Globals["GetPalette"] = (Func<SwapType, int, ColorSwapPalette>) ColorPaletteMap.GetPalette;
        Func<float, float, float, Vector3> newVector3 = (x, y, z) => new Vector3(x, y, z);
        script.Globals["newVector3"] = newVector3;
        
        Func<float, float, Vector2> newVector2 = (x, y) => new Vector2(x, y);
        script.Globals["newVector2"] = newVector2;
        
        Func<TextsBasic> newTextsBasic = () => new TextsBasic();
        script.Globals["newTextsBasic"] = newTextsBasic;
        
        Func<TextsBasic, TextsBasic, TextsBasic, Dictionary<string, string>, FlavorText> newFlavorText = (preyDescriptions, predDescriptions, raceSingleDescriptions, weaponNames) => new FlavorText(preyDescriptions, predDescriptions, raceSingleDescriptions, weaponNames);
        script.Globals["newFlavorText"] = newFlavorText;

        RegisterStatic(script, "Config", typeof(Config));
        RegisterStatic(script, "Defaults", typeof(Defaults));
        RegisterStatic(script, "CommonRaceCode", typeof(CommonRaceCode));
        RegisterStaticFields(script, "HorseClothing", typeof(EquinesLua.HorseClothing));
        

        Dictionary<string, dynamic> defaults = new Dictionary<string, dynamic>
        {
            ["Finalize"] = Defaults.Finalize,
            ["RandomCustom"] = Defaults.RandomCustom,
            ["BasicBellyRunAfter"] = Defaults.BasicBellyRunAfter
        };
        
        script.Globals["Defaults"] = defaults;
        script.Globals["Finalize"] = Defaults.Finalize;

        Func<int, int> RandomInt = (max) => State.Rand.Next(max);
        script.Globals["RandomInt"] = RandomInt;
        
        script.DoString(@"
function ternary ( cond , T , F )
    if cond then return T else return F end
end");
		
        script.DoString(scriptCode, null, raceId + " - race.lua");

        object render = script.Globals["render"];
        object setup = script.Globals["setup"];
        object randomCustom = script.Globals["randomCustom"];
        
        RaceScriptUsable scriptUsable = new RaceScriptUsable(
            (output) =>
            {
                try
                {
                    script.Call(setup, output);
                }
                catch (ScriptRuntimeException ex)
                {
                    Debug.Log("Doh! An error occured! " + ex.DecoratedMessage);
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
                    Debug.Log("Doh! An error occured! " + ex.Message);
                    Debug.Log("Doh! An error occured! " + ex.DecoratedMessage);
                }
            },
            (output) =>
            {
                script.Call(randomCustom, output);
            }
        );

        return scriptUsable;
    }
	
	
    internal static ClothingScriptUsable ScriptPrepClothingFromCode(string scriptCode, string clothingId)
    {
        Script script = new Script();
        
        script.Globals["Log"] = (Action<string>) Debug.Log;

        #region Enums
        
        // TODO Traits should be later renamed to Trait to follow naming conventions
        // Set to Trait in script scrope to avoid breaking changes to scripts
        script.Globals["Trait"] = UserData.CreateStatic<Traits>();
        script.Globals["ButtonType"] = UserData.CreateStatic<ButtonType>();
        script.Globals["Gender"] = UserData.CreateStatic<Gender>();
        script.Globals["Stat"] = UserData.CreateStatic<Stat>();
        script.Globals["SpriteType"] = UserData.CreateStatic<SpriteType>();
        script.Globals["Gender"] = UserData.CreateStatic<Gender>();
        script.Globals["SwapType"] = UserData.CreateStatic<SwapType>();

        #endregion
        
        script.Globals["GetPaletteCount"] = (Func<SwapType, int>) ColorPaletteMap.GetPaletteCount;
        script.Globals["GetPalette"] = (Func<SwapType, int, ColorSwapPalette>) ColorPaletteMap.GetPalette;
        Func<float, float, float, Vector3> newVector3 = (x, y, z) => new Vector3(x, y, z);
        script.Globals["newVector3"] = newVector3;
        
        Func<float, float, Vector2> newVector2 = (x, y) => new Vector2(x, y);
        script.Globals["newVector2"] = newVector2;
        
        Func<TextsBasic> newTextsBasic = () => new TextsBasic();
        script.Globals["newTextsBasic"] = newTextsBasic;
        
        Func<TextsBasic, TextsBasic, TextsBasic, Dictionary<string, string>, FlavorText> newFlavorText = (preyDescriptions, predDescriptions, raceSingleDescriptions, weaponNames) => new FlavorText(preyDescriptions, predDescriptions, raceSingleDescriptions, weaponNames);
        script.Globals["newFlavorText"] = newFlavorText;

        
        RegisterStatic(script, "Config", typeof(Config));
        RegisterStatic(script, "Color", typeof(Colors));
        RegisterStatic(script, "Defaults", typeof(Defaults));
        RegisterStatic(script, "CommonRaceCode", typeof(CommonRaceCode));
        RegisterStaticFields(script, "HorseClothing", typeof(EquinesLua.HorseClothing));
        

        Dictionary<string, dynamic> defaults = new Dictionary<string, dynamic>
        {
            ["Finalize"] = Defaults.Finalize,
            ["RandomCustom"] = Defaults.RandomCustom,
            ["BasicBellyRunAfter"] = Defaults.BasicBellyRunAfter
        };
        
        script.Globals["Defaults"] = defaults;
        script.Globals["Finalize"] = Defaults.Finalize;

        Func<int, int> RandomInt = (max) => State.Rand.Next(max);
        script.Globals["RandomInt"] = RandomInt;
        
        script.DoString(@"
function ternary ( cond , T , F )
    if cond then return T else return F end
end");
        
        script.DoString(scriptCode, null, clothingId + " - clothing.lua");
        
        object render = script.Globals["render"];
        object setup = script.Globals["setup"];
        
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
    
	
    public static void RegisterStatic(Script script, string staticName, Type type)
    {
        // Get global methods (must be public) and add them to the script.Globals
        MethodInfo[] globalMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Static);
        var methodTable = new Table(script);
        
        foreach (var method in globalMethods)
        {
            // Get name, parameters and return type so we can build a delegate
            string name = method.Name;
            Type[] parameters = method.GetParameters().Select(p => p.ParameterType).ToArray();
            Type returnType = method.ReturnType;

            // Build a delegate and add to globals with the name of the method, use the correct delegate type based on the return type
            if(returnType == typeof(void))
            {
                Delegate del = Delegate.CreateDelegate(Expression.GetActionType(parameters), method);
                //script.Globals[name] = del;
                methodTable.Set(name, DynValue.FromObject(script, del));
            }
            else
            {
                Delegate del = Delegate.CreateDelegate(Expression.GetFuncType(parameters.Concat(new[] { returnType }).ToArray()), method);
                //script.Globals[name] = del;
                methodTable.Set(name, DynValue.FromObject(script, del));
            }
        }

        script.Globals[staticName] = methodTable;
    }


    public static void RegisterStaticFields(Script script, string staticName, Type type)
    {
        // Get global methods (must be public) and add them to the script.Globals
        MethodInfo[] globalMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Static);
        FieldInfo[] globalFields = type.GetFields(BindingFlags.Public | BindingFlags.Static);
        
        
        var methodTable = new Table(script);
        
        foreach (var field in globalFields)
        {
            // Get name, parameters and return type so we can build a delegate
            string name = field.Name;

            //script.Globals[name] = del;
            methodTable.Set(name, DynValue.FromObject(script, field.GetValue(null)));
        }
        
        foreach (var method in globalMethods)
        {
            // Get name, parameters and return type so we can build a delegate
            string name = method.Name;
            Type[] parameters = method.GetParameters().Select(p => p.ParameterType).ToArray();
            Type returnType = method.ReturnType;

            // Build a delegate and add to globals with the name of the method, use the correct delegate type based on the return type
            if(returnType == typeof(void))
            {
                Delegate del = Delegate.CreateDelegate(Expression.GetActionType(parameters), method);
                //script.Globals[name] = del;
                methodTable.Set(name, DynValue.FromObject(script, del));
            }
            else
            {
                Delegate del = Delegate.CreateDelegate(Expression.GetFuncType(parameters.Concat(new[] { returnType }).ToArray()), method);
                //script.Globals[name] = del;
                methodTable.Set(name, DynValue.FromObject(script, del));
            }
        }

        script.Globals[staticName] = methodTable;
    }
	
    public static void RegisterSimpleFunc<T>()
    {
        Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function, typeof(Func<T>),
            v =>
            {
                var function = v.Function;
                return (Func<T>) (() => function.Call().ToObject<T>());
            }
        );
    }

    public static void RegisterSimpleFunc<T1, TResult>()
    {
        Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function, typeof(Func<T1, TResult>),
            v =>
            {
                var function = v.Function;
                return (Func<T1, TResult>) ((T1 p1) => function.Call(p1).ToObject<TResult>());
            }
        );
    }

    public static void RegisterSimpleFunc<T1, T2, TResult>()
    {
        Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function, typeof(Func<T1, T2, TResult>),
            v =>
            {
                var function = v.Function;
                return (Func<T1, T2, TResult>) ((T1 p1, T2 p2) => function.Call(p1, p2).ToObject<TResult>());
            }
        );
    }

    public static void RegisterSimpleFunc<T1, T2, T3, TResult>()
    {
        Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function, typeof(Func<T1, T2, T3, TResult>),
            v =>
            {
                var function = v.Function;
                return (Func<T1, T2, T3, TResult>) ((T1 p1, T2 p2, T3 p3) => function.Call(p1, p2, p3).ToObject<TResult>());
            }
        );
    }

    public static void RegisterSimpleAction<T>()
    {
        Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function, typeof(Action<T>),
            v =>
            {
                var function = v.Function;
                return (Action<T>) (p => function.Call(p));
            }
        );
    }

    public static void RegisterSimpleAction<T, TU>()
    {
        Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function, typeof(Action<T, TU>),
            v =>
            {
                var function = v.Function;
                return (Action<T, TU>) ((p, p2) => function.Call(p, p2));
            }
        );
    }

    public static void RegisterSimpleAction()
    {
        Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function, typeof(Action),
            v =>
            {
                var function = v.Function;

                return (Action) (() => function.Call());
            }
        );
    }
}