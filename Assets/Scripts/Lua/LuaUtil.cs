using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MoonSharp.Interpreter;

public static class LuaUtil
{
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
            if (returnType == typeof(void))
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
                return (Func<T>)(() => function.Call().ToObject<T>());
            }
        );
    }

    public static void RegisterSimpleFunc<T1, TResult>()
    {
        Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function, typeof(Func<T1, TResult>),
            v =>
            {
                var function = v.Function;
                return (Func<T1, TResult>)((T1 p1) => function.Call(p1).ToObject<TResult>());
            }
        );
    }

    public static void RegisterSimpleFunc<T1, T2, TResult>()
    {
        Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function, typeof(Func<T1, T2, TResult>),
            v =>
            {
                var function = v.Function;
                return (Func<T1, T2, TResult>)((T1 p1, T2 p2) => function.Call(p1, p2).ToObject<TResult>());
            }
        );
    }

    public static void RegisterSimpleFunc<T1, T2, T3, TResult>()
    {
        Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function, typeof(Func<T1, T2, T3, TResult>),
            v =>
            {
                var function = v.Function;
                return (Func<T1, T2, T3, TResult>)((T1 p1, T2 p2, T3 p3) => function.Call(p1, p2, p3).ToObject<TResult>());
            }
        );
    }

    public static void RegisterSimpleAction<T>()
    {
        Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function, typeof(Action<T>),
            v =>
            {
                var function = v.Function;
                return (Action<T>)(p => function.Call(p));
            }
        );
    }

    public static void RegisterSimpleAction<T, TU>()
    {
        Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function, typeof(Action<T, TU>),
            v =>
            {
                var function = v.Function;
                return (Action<T, TU>)((p, p2) => function.Call(p, p2));
            }
        );
    }

    public static void RegisterSimpleAction()
    {
        Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function, typeof(Action),
            v =>
            {
                var function = v.Function;

                return (Action)(() => function.Call());
            }
        );
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
            if (returnType == typeof(void))
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
}