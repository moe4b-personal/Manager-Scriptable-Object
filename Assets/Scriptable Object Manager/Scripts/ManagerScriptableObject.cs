using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

[CreateAssetMenu]
public class ManagerScriptableObject : ScriptableObject
{
    //variable will hold the instance value so we don't look for it everytime we need to access it
    //access the Instance only via the Current Property
    static ManagerScriptableObject _current;
    public static ManagerScriptableObject Current
    {
        get
        {
            if (_current == null || !Application.isPlaying)
                _current = GetCurrent();

            return _current;
        }
    }
    static ManagerScriptableObject GetCurrent()
    {
        var list = Resources.LoadAll<ManagerScriptableObject>("");

        if (list.Length == 0) return null;

        for (int i = 0; i < list.Length; i++)
        {
            if (list[i].name.ToLower().Contains("override"))
                return list[i];
        }

        return list.First();
    }

    //Is there a Current instance ?
    public static bool AssetAvaliable { get { return Current != null; } }

    //gets called when the game loads using the argument gets it called before the startup scene is loaded
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnGameLoad()
    {
        if (Current)
            Current.Configure();
        else
            throw new NullReferenceException("No Asset Instance Found, Please Make Sure An Asset Instance Exists Within A Resources Folder");
    }

    //gets called on the local instance from the OnGameLoad, will only be called once obviously
    void Configure()
    {
        Debug.Log("Manager Configured, Called Once The Game Loads, Right Before The Scene Load");

        SceneManager.sceneLoaded += OnSceneChanged;
    }

    //listener to scene change event, calls the Init method
    private void OnSceneChanged(Scene scene, LoadSceneMode loadMode)
    {
        Init();
    }

    //gets called on scene change, should act as a replacement for a monobehaviour's Awake, Start, .... methods
    void Init()
    {
        Debug.Log("Manager Initilized, Called On Every Scene Load Including The Startup Scene");
    }
}