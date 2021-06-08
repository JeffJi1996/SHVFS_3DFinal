using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : Singleton<PowerUpManager>
{
    private SpecialCollection[] powerUps;
    void Start()
    {
        powerUps = FindObjectsOfType<SpecialCollection>();
    }

    public void Reset()
    {
        foreach (var specialCollection in powerUps)
        {
            if (specialCollection.hasBeenCollected)
            {
                StopCoroutine(specialCollection.BeCollected());
                specialCollection.Recover();
            }
        }
    }



}
