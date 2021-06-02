using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour, IEndGameObserver
{
    [Header("Speed")]
    public float currentSpeed;
    public float walkSpeed;
    public float runSpeed;
    public float SuperSpeed;

    [Header("Rotate")]
    public float rotateDuration;
    public Vector3 InitPosition;
    private Rigidbody rb;

    [Header("CharaInfo")]
    public static int health;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = walkSpeed;
        InitPosition = transform.position;
        transform.localEulerAngles = new Vector3(0, -90, 0);
        health = 3;
        GameManager.Instance.AddObserver(this);
    }
    
    void OnDisable()
    {
        if (!GameManager.IsInitialized) return;
        GameManager.Instance.RemoveObserver(this);
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

    public void EndNotify()
    {
        health--;
        if (health <= 0)
        {
            SceneManager.LoadScene(0);
        }
        transform.position = InitPosition;
    }
}
