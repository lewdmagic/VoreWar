using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MoonSharp.Interpreter;
using UnityEngine;

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
        
        IRaceData Instance = RaceBuilder.CreateV2(Defaults.Blank, builder =>
        {

            UserData.RegisterType<Action>();
            UserData.RegisterType<IParameters>();
            UserData.RegisterType<IRaceRenderInput>();
            UserData.RegisterType<IRaceRenderOutput>();
            UserData.RegisterType<Sprite>();
            UserData.RegisterType<SpriteType>();
            UserData.RegisterType<IRaceBuilder<IParameters>>();
		    
            ScriptHelper.RegisterSimpleAction<string, string>();
            ScriptHelper.RegisterSimpleAction<IRaceRenderInput<IParameters>, IRaceRenderOutput>();
		    
            Script script = new Script();
            script.Globals["builder"] = builder;
            script.Globals["SpriteType"] = UserData.CreateStatic<SpriteType>();
            script.DoString(scriptCode);
		    
            builder.RunBefore(Defaults.Finalize);
            builder.RandomCustom(Defaults.RandomCustom);
        });
    }
}


public static class ScriptHelper
{
    static ScriptHelper()
    {
        // Register Types
        UserData.RegisterType<OverSizeParameters>();
        UserData.RegisterType<MiscRaceDataWritableReadable<OverSizeParameters>>();
        
        UserData.RegisterType<Action>();
        UserData.RegisterType<IParameters>();
        UserData.RegisterType<IRaceRenderInput>();
        UserData.RegisterType<IRaceRenderOutput>();
        UserData.RegisterType<Sprite>();
        UserData.RegisterType<SpriteType>();
        UserData.RegisterType<SpriteDictionary>();
        UserData.RegisterType<IRaceBuilder<IParameters>>();
        UserData.RegisterType<IRaceBuilder<IOverSizeParameters>>();
        UserData.RegisterType<IRaceBuilder<OverSizeParameters>>();
        UserData.RegisterType<SwapType>();
        UserData.RegisterType<Actor_Unit>();
        UserData.RegisterType<Unit>();
        UserData.RegisterType<ColorSwapPalette>();
        UserData.RegisterType<IRandomCustomInput>();
        UserData.RegisterType<IRunInput>();
        UserData.RegisterType<IRunOutput>();
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
        UserData.RegisterType<IWriteOnlyList<IClothing<IParameters>>>();
        UserData.RegisterType<IWriteOnlyList<IClothing<IOverSizeParameters>>>();
        UserData.RegisterType<IWriteOnlyList<IClothing<OverSizeParameters>>>();
        UserData.RegisterType<WrappedList<IClothing<OverSizeParameters>>>();
        UserData.RegisterType<WrappedList<IClothing<IOverSizeParameters>>>();
        UserData.RegisterType<MiscRaceDataWritableReadable<IParameters>>();
        UserData.RegisterType<MiscRaceDataWritableReadable<IOverSizeParameters>>();
        
        UserData.RegisterType<IClothing>();
        UserData.RegisterType<IClothing<IOverSizeParameters>>();
        UserData.RegisterType<IClothing<OverSizeParameters>>();
        
        UserData.RegisterType<Clothing<IOverSizeParameters>>();
        UserData.RegisterType<Clothing<OverSizeParameters>>();
        
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
        
        UserData.RegisterType<IRaceRenderAllOutput<IParameters>>();
        UserData.RegisterType<IRaceRenderAllOutput<OverSizeParameters>>();
        UserData.RegisterType<RaceRenderAllOutput<OverSizeParameters>>();
        UserData.RegisterType<RaceRenderAllOutput<OverSizeParameters>>();
        
        ScriptHelper.RegisterSimpleAction<IRaceRenderInput<IParameters>, IRaceRenderOutput>();
        ScriptHelper.RegisterSimpleAction<IRaceRenderInput<OverSizeParameters>, IRaceRenderOutput>();
        ScriptHelper.RegisterSimpleAction<IRaceRenderInput<IOverSizeParameters>, IRaceRenderOutput>();
        ScriptHelper.RegisterSimpleAction<IRunInput,IRunOutput>();
        
        ScriptHelper.RegisterSimpleAction<IRandomCustomInput>();
        ScriptHelper.RegisterSimpleAction<RaceTraits>();
        
        ScriptHelper.RegisterSimpleAction<MiscRaceDataWritableReadable<IParameters>>();
        ScriptHelper.RegisterSimpleAction<MiscRaceDataWritableReadable<IOverSizeParameters>>();
        ScriptHelper.RegisterSimpleAction<MiscRaceDataWritableReadable<OverSizeParameters>>();
        
        ScriptHelper.RegisterSimpleAction<IRunInput,IRunOutput>();
        
        ScriptHelper.RegisterSimpleAction<Unit, EnumIndexedArray<ButtonType, CustomizerButton>>();
        ScriptHelper.RegisterSimpleAction<Unit, ButtonCustomizer>();
        ScriptHelper.RegisterSimpleAction<IRunInput,IRunOutput<OverSizeParameters>>();
        ScriptHelper.RegisterSimpleAction<IRunInput, IRaceRenderAllOutput<IParameters>>();
        ScriptHelper.RegisterSimpleAction<IRunInput, IRaceRenderAllOutput<OverSizeParameters>>();
        
        ScriptHelper.RegisterSimpleFunc<int>();
        
        ScriptHelper.RegisterSimpleFunc<float, float, Vector2>();
        ScriptHelper.RegisterSimpleFunc<float, float, float, Vector3>();
        
        ScriptHelper.RegisterSimpleAction<string>();
    }
    
    internal static void ScriptPrep(string path, IRaceBuilder<OverSizeParameters> builder)
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
    
    internal static void ScriptPrep2(string path, IRaceBuilder<OverSizeParameters> builder)
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

        object render = script.Globals["render"];
        
        builder.RenderAll((input, output) =>
        {
            script.Call(render, input, output);
        });
        
        
        
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