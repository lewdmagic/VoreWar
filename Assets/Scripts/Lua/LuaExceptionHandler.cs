using System;
using System.IO;
using MoonSharp.Interpreter;
using UnityEngine;

public static class LuaExceptionHandler
{
    private static readonly string DirPath = "UserData/LuaExceptions";
    private static readonly string Path = $"{DirPath}/Log_{DateTime.Now.ToString().Replace(":", "-").Replace(" ", "_")}.txt";
    private static bool _directoryExists = false;
    private static bool _exceptionHappened = false;

    private static void EnsurePathExists()
    {
        if (_directoryExists)
        {
            return;
        }

        try
        {
            // Determine whether the directory exists.
            if (Directory.Exists(DirPath))
            {
                _directoryExists = true;
                return;
            }
        
            Directory.CreateDirectory(DirPath);
            _directoryExists = true;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
    
    
    public static void LogToFile(ScriptRuntimeException exception)
    {
        EnsurePathExists();
        try
        {
            // TODO Optimize
            using StreamWriter sw = File.AppendText(Path);
            sw.WriteLine(exception.DecoratedMessage);

            if (!_exceptionHappened)
            {
                State.GameManager.CreateMessageBox($"A Lua script exception occured. Further exceptions in this session will not create a pop-up but will be logged to {Path}. The exception message: \n\n{exception.DecoratedMessage}");
                _exceptionHappened = true;
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
    
}