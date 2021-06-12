using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
    
    void StartToSpike()
    {
        transform.parent.GetComponentInChildren<SpikeManager>().ActiveSpike();
    }
}
