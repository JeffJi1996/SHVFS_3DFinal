using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDirection : MonoBehaviour
{
    private GameObject Player;
    public GameObject Enemy;

    [SerializeField] private Image fill1;
    [SerializeField] private Image fill2;
    [SerializeField] private GameObject sprites;
    [SerializeField] private float detectRange;

    private Vector3 enemyVector3;
    private float RotateAngle;
    private float PlayerForwardAngle;


    private RectTransform transform;

    private void Start()
    {
        Player = FindObjectOfType<PlayerMovement>().gameObject;
        transform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        enemyVector3 = CalculateAngle();
        RotateAngle = Angle_360(new Vector3(0, 0, 1), enemyVector3);
        transform.rotation = Quaternion.Euler(0, 0, -RotateAngle);

        var distance = Vector3.Distance(Enemy.transform.position, Player.transform.position);

        if (Enemy.activeSelf && distance<detectRange)
        {
            sprites.SetActive(true);
            fill1.fillAmount = distance / detectRange;
            fill2.fillAmount = fill1.fillAmount;
        }
        else
        {
            sprites.SetActive(false);
        }


    }

    Vector3 CalculateAngle()
    {
        PlayerForwardAngle = Angle_360(new Vector3(0, 0, 1), Player.transform.forward.normalized);

        Vector3 playPosition = new Vector3(Player.transform.position.x, 0, Player.transform.position.z);
        Vector3 EnemyPosition = new Vector3(Enemy.transform.position.x, 0, Enemy.transform.position.z);
        Vector3 PlayerEnemyDirection = (EnemyPosition - playPosition).normalized;
        Quaternion PianYi;
        if (PlayerForwardAngle < 180)
        {
            PianYi = Quaternion.Euler(0, -PlayerForwardAngle, 0);
        }
        else
        {
            PianYi = Quaternion.Euler(0, 360 - PlayerForwardAngle, 0);
        }

        Vector3 newRotateDirection = PianYi * PlayerEnemyDirection;

        return newRotateDirection;
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
