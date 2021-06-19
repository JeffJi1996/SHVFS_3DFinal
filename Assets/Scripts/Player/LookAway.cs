using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAway : Singleton<LookAway>
{
    public Transform target;
    public float rotateSpeed;
    public bool startRotate;
    public float lerpFloat;
    public bool endRotate;

    void Start()
    {
        startRotate = false;
        endRotate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startRotate)
        {
            Vector3 lookDirection = target.transform.position - transform.position;
            Quaternion lookOnLook = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, lerpFloat);
            lerpFloat = rotateSpeed * Time.deltaTime;

            if (Vector3.Angle(transform.forward.normalized,lookDirection.normalized)<1)
            {
                endRotate = true;
                startRotate = false;
            }
        }
    }
}
