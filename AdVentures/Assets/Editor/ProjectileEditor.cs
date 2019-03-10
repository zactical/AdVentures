using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(Projectile))]
public class ProjectileEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var projectile = (Projectile)target;

        if (GUILayout.Button("Set Collider"))
        {
            var sr = projectile.GetComponentInChildren<SpriteRenderer>();
            var collider = projectile.GetComponentInChildren<CircleCollider2D>();

            var width = sr.sprite.bounds.size.x * sr.transform.localScale.x;

            collider.radius = width / 2;

        }

        if (GUILayout.Button("Force Save"))
        {
            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage != null)
            {
                EditorSceneManager.MarkSceneDirty(prefabStage.scene);
            }
        }
    }
}
