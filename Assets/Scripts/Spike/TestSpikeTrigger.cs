using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpikeTrigger : MonoBehaviour
{
    public Vector3 triggerDirection;
    public Spike spike;

    void Awake()
    {
        spike = transform.parent.GetComponentInChildren<Spike>();
        var direction = transform.parent.position - transform.position;
        triggerDirection = new Vector3(direction.x, 0, direction.z).normalized;
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<PlayerMovement>() != null && spike.isActive == false)
        {
            var PlayerMoveDirection = col.GetComponent<VelocityComponent>().direction;
            if (Angle_360(PlayerMoveDirection, triggerDirection) < 90)
            {
                spike.isActive = true;
            }
        }
    }

    public float Angle_360(Vector3 from, Vector3 to)
    {
        float angle = Vector3.Angle(from, to);

        Vector3 v3 = Vector3.Cross(from, to);

        float dot = Vector3.Dot(v3, Vector3.up);

        if (dot < 0)
        {
            angle *= -1;
            angle += 360;
        }

        return angle;
    }


}
