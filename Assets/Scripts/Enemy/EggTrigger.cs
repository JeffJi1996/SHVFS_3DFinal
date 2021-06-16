using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>()!= null && PlayerAbilityControl.instance.PowerUpState)
        {
            GetComponentInParent<ChongLuan>().canAttack = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovement>()!= null)
        {
            GetComponentInParent<ChongLuan>().canAttack = false;
        }
    }
}
