using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class GameUtils
{
    //public static T[] GetAllInstances<T>() where T : UnityEngine.Object
    //{
    //    string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);  //FindAssets uses tags check documentation for more info
    //    T[] a = new T[guids.Length];
    //    for (int i = 0; i < guids.Length; i++)         //probably could get optimized 
    //    {
    //        string path = AssetDatabase.GUIDToAssetPath(guids[i]);
    //        a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
    //    }
    //
    //    return a;
    //}

   // public static T[] GetAllInstances<T>() where T : UnityEngine.Object
   // {        
   //     return Resources.Load<T>();
   // }

    public static Enemy GetEnemyPrefabByType(EnemyTypeEnum enemyType)
    {
        var enemyPrefabs = Resources.LoadAll<Enemy>("").ToList();
        var enemy = enemyPrefabs.FirstOrDefault(x => x.EnemyType == enemyType);

        if (enemy == null)
            Debug.LogError($"No enemy of type: {enemyType} was found in the enemy manager.");

        return enemy;
    }

    public static string GetRandomTrainingMessage()
    {
        return trainingMessages[Random.Range(0, trainingMessages.Length)];
    }

    private static readonly string[] trainingMessages = new string[] {
        "trained you on Ticklers!",
        "trained you on Web Templates!",
        "trained you on ETRAN!",
        "trained you on Custom Reporting!",
        "trained you on Lead Tracking!",
        "trained you on Payment Tracking!",
        "trained you on 1502 Reporting!",
        "trained you on the CSA Balance Import!",
        "trained you on the SBPS Scores Import!",
        "trained you on the Deliquent Property Taxes Import!",
        "trained you on the Loan Import/Export!",
        "trained you on the BMI integration!",
        "trained you on the Veritax integration!",
        "trained you on the T-4506 integration!",
        "trained you on the Mirador integration!",
        "trained you on the FlashSpread integration!",
        "trained you on the QuikTrak integration!",
        "trained you on the LaserPro integration!",
        "trained you on the BCR integration!",
        "gave a Servicing Overview!",
        "gave a Loan Origination Overview!",
        "gave a Home Page Overview!",
        "gave a Loan Record Overview!",
        "gave a Financial Statements Overview!",
        "gave a Admin Overview!",
        "gave a Credit Memo Overview!",
        "gave a Notes and Tasks Overview!",
        "gave a 2-Step Verification Overview!",
        "helped you install the Loan Authorization Wizard!",
        "helped you through Live Chat!",
        "answered your Help Desk ticket!",
        "met you at the NADCO conference!",
        "met you at the NAGGL conference!"
    };
}
