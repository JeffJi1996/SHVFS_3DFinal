using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpecialCollection : MonoBehaviour
{
    [SerializeField] private float rebornTime;
    public bool hasBeenCollected;
    

    void Start()
    {
        hasBeenCollected = false;
    }

    public IEnumerator BeCollected()
    {
        Destroy();
        yield return new WaitForSeconds(rebornTime);
        Recover();
    }
    public void Destroy()
    {
        hasBeenCollected = true;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void Recover()
    {
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
        hasBeenCollected = false;
    }

}
