using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<PlayerMovement>()!= null && PlayerDeath.Instance.isDeath == false)
        {
            Debug.Log(PlayerDeath.Instance.health);
            PlayerDeath.Instance.killBy = PlayerDeath.KillBy.Spike;
            SoundManager.instance.PlaySound("sfx_playerDie");
            GameManager.Instance.NotifyObservers();
        }
    }
}
