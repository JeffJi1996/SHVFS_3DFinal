using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public static MiniMap instance;

    void Awake()
    {
        instance = this;
    }
    public void ShowEnemyIcon(int floor)
    {
        var Icons = FindObjectsOfType<MiniIcon_Enemy>();
        foreach (var miniIconEnemy in Icons)
        {
            if (miniIconEnemy.floor == floor)
            {
                miniIconEnemy.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    public void HideEnemyIcon()
    {
        var Icons = FindObjectsOfType<MiniIcon_Enemy>();
        foreach (var miniIconEnemy in Icons)
        {
            if (miniIconEnemy.gameObject.GetComponent<SpriteRenderer>().enabled)
            {
                miniIconEnemy.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

}
