using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VelocityComponent : MonoBehaviour
{
    private Vector3 AverageVelocity;
    public float Multiplier;
    public int Dampening;
    private List<Vector3> velocityVectors = new List<Vector3>();
    private Vector3 previousPosition;
    public Vector3 direction;

    void Awake()
    {
        previousPosition = transform.position;
    }

    private void Update()
    {
        if (velocityVectors.Count >= Dampening)
        {
            velocityVectors.RemoveAt(0);
        }

        var velocityVector = transform.position - previousPosition;

        velocityVectors.Add(velocityVector);

        var averageVelocity = Vector3.zero;

        foreach(var v in velocityVectors)
        {
            averageVelocity += v;
        }

        averageVelocity = averageVelocity / velocityVectors.Count;

        previousPosition = transform.position;
        AverageVelocity = averageVelocity * Multiplier;

        GetDirection();

    }

    void GetDirection()
    {
        if (Mathf.Abs(AverageVelocity.x)>Mathf.Abs(AverageVelocity.z))
        {
            direction = new Vector3(AverageVelocity.x,0,0).normalized;
        }
        else if(Mathf.Abs(AverageVelocity.x)< Mathf.Abs(AverageVelocity.z))
        {
            direction = new Vector3(0, 0, AverageVelocity.z).normalized;
        }
        else if (Mathf.Abs(AverageVelocity.x) == Mathf.Abs(AverageVelocity.z))
        {
            if (AverageVelocity.x == 0)
            {
                direction = new Vector3(0, 0, 0);
            }

            if (AverageVelocity.x != 0)
            {
                direction = new Vector3(AverageVelocity.x, 0, AverageVelocity.z).normalized;
            }
        }
    }


}
