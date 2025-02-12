﻿using System.IO;
using UnityEngine;

public class ExceptionHandler : MonoBehaviour
{
    private int _exceptionCount;
    private string _path;

    private void Awake()
    {
        if (Application.isEditor) return;

        Application.logMessageReceived += HandleException;
        if (Application.platform == RuntimePlatform.OSXPlayer)
            _path = Path.Combine(Application.persistentDataPath, "recentexceptions.txt");
        else
            _path = Path.Combine(Application.dataPath, "recentexceptions.txt");
    }

    private void HandleException(string condition, string stackTrace, LogType type)
    {
        try
        {
            if (type == LogType.Exception)
            {
                using (StreamWriter writer = new StreamWriter(_path))
                {
                    if (_exceptionCount > 50) //To avoid too much clutter
                        return;
                    if (_exceptionCount == 0)
                    {
                        if (Application.platform == RuntimePlatform.OSXPlayer)
                            State.GameManager.CreateFullScreenMessageBox($"The first Exception of this session was just logged to recentexceptions.txt, you'll probably want to notify a dev on the VoreWar Discord or GitHub with the contents of that file so it can be fixed.  As this is a mac and mac exception logs don't seem to be writing correctly at the moment, you can take a screenshot of this screen and send it instead.  \nFull Details: {type}: {condition}\nVersion :{State.Version}\n{stackTrace}");
                        else
                            State.GameManager.CreateMessageBox("The first Exception of this session was just logged to recentexceptions.txt, you'll probably want to notify a dev on the VoreWar Discord or GitHub with the contents of that file so it can be fixed");
                    }

                    writer.WriteLine($"{type}: {condition}\nVersion :{State.Version}\n{stackTrace}");
                    writer.Flush();
                    _exceptionCount++;
                }
            }
        }
        catch
        {
            Debug.Log("Failed to write exception... just eating it rather than throw another exception");
        }
    }
}