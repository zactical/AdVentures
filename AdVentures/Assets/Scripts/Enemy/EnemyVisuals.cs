using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisuals : MonoBehaviour, IEnemyGraphicUpdater
{
    [SerializeField]
    private SpriteRenderer mainEnemyRenderer;

    [SerializeField]
    public List<EnemyHealthData> visualData;
    

    public void UpdateGraphics(int health)
    {
        if (health < 0)
            return;

        mainEnemyRenderer.sprite = visualData[health - 1].sprite;
        mainEnemyRenderer.material = visualData[health - 1].material;
    }

    public void UpdateGraphics()
    {
        UpdateGraphics(visualData.Count);
    }

    public int GraphicLevels()
    {
        return visualData.Count; 
    }
}
