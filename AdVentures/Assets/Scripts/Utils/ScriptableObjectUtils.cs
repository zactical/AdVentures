using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ScriptableObjectUtils
{
    public static T Copy<T>(T source) where T : ScriptableObject
    {
        return (T)ScriptableObject.CreateInstance(typeof(T));
        //if (resetSource != null)
        //{
        //    var output = JsonUtility.ToJson(resetSource);
        //    JsonUtility.FromJsonOverwrite(output, this);
        //}
    }

    public static void Reset<T>(T objectToReset) where T : ScriptableObject
    {
        var resetSource = Copy<T>(objectToReset);
        var output = JsonUtility.ToJson(resetSource);
        JsonUtility.FromJsonOverwrite(output, objectToReset);
    }

    public static T[] GetAllInstances<T>() where T : ScriptableObject
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);  //FindAssets uses tags check documentation for more info
        T[] a = new T[guids.Length];
        for (int i = 0; i < guids.Length; i++)         //probably could get optimized 
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
        }

        return a;

    }

    public static T[] GetAllInstances<T>(T instance) where T : ScriptableObject
    {
        return GetAllInstances<T>();

    }
}
