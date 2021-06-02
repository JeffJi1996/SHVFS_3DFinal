using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollectComponent : MonoBehaviour
{
    public int collectionAmount;
    public int allCollectionAmount;

    public Text collectionText;


    
    void Start()
    {
        collectionAmount = 0;
        allCollectionAmount = FindObjectsOfType<Collection>().Length;
    }

    void Update()
    {
        collectionText.text = collectionAmount+"/"+allCollectionAmount;
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Collection>() != null)
        {
            SoundManager.instance.PlaySound("sfx_collect");
            Destroy(col.gameObject);
            collectionAmount++;
        }
    }

}
