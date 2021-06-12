using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : Singleton<PlayerMovement>
{
    [Header("Speed")]
    public float currentSpeed;
    public float walkSpeed;
    public float runSpeed;
    public float SuperSpeed;

    [Header("Rotate")]
    public float rotateDuration;
    private Rigidbody rb;

    private RaycastHit hitInfo;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = walkSpeed;
        transform.localEulerAngles = new Vector3(0, -90, 0);
    }
    
 
    
    // Update is called once per frame
    void Update()
    {
        #region Run
        if (Input.GetKey(KeyCode.LeftShift) && PlayerAbilityControl.instance.PowerUpState == false)
        {
            currentSpeed = runSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && PlayerAbilityControl.instance.PowerUpState==false)
        {
            currentSpeed = walkSpeed;
        }
        #endregion
        #region TurnAround
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            Quaternion Turn180Degree = Quaternion.Euler(transform.localEulerAngles+new Vector3(0,180,0));
            transform.DOLocalRotateQuaternion(Turn180Degree, rotateDuration);
        }
        

        #endregion
    }

    void FixedUpdate()
    {
        #region Move
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveH + transform.forward * moveV;
        rb.velocity = move * currentSpeed;
        #endregion

    }

    public string LookAtThing()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.GetComponent<ChongLuan>() != null)
                PlayerAbilityControl.instance.AttackAbility.isAtEgg = true;
            else
                PlayerAbilityControl.instance.AttackAbility.isAtEgg = false;
            return hitInfo.collider.name;
        }
        return null;
    }
   
}
