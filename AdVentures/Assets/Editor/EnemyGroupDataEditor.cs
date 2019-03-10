using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(EnemyGroup))]
public class EnemyGroupDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EnemyGroup group = (EnemyGroup)target;


         var rows = group.GetComponentsInChildren<EnemyGroupRow>();
        //
        // //foreach (var item in rows)
        // //{
        // //    Object.DestroyImmediate(item);
        // //}
        // foreach (var row in rows)
        // {
        //     Instantiate(test, row.transform.position, Quaternion.identity, row.transform);
        // }

        if (GUILayout.Button("Test Formation"))
        {
            foreach (var row in rows)
            {
                var allChildren = row.transform.Cast<Transform>().ToList();
                foreach (Transform child in allChildren)
                {
                    DestroyImmediate(child.gameObject);
                }

                row.Initialize(null, true);
            }
        }

        if (GUILayout.Button("Delete Formation"))
        {
            foreach (var row in rows)
            {
                var allChildren = row.transform.Cast<Transform>().ToList();
                foreach (Transform child in allChildren)
                {
                    DestroyImmediate(child.gameObject);
                }
            }
            SavePrefab();
        }
    }

    private void SavePrefab()
    {
        var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
        if (prefabStage != null)
        {
            EditorSceneManager.MarkSceneDirty(prefabStage.scene);
        }
    }
}
