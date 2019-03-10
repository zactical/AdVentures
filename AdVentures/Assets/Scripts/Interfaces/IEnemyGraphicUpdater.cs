using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyGraphicUpdater
{
    int GraphicLevels();
    void UpdateGraphics(int health);
    void UpdateGraphics();
}
