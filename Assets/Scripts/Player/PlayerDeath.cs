using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : Singleton<PlayerDeath>,IEndGameObserver
{
    public int health;
    public bool doOnce;
    public bool doOnce1;

    public Vector3 InitPosition;

    private void Start()
    {
        InitPosition = transform.position;
        

        GameManager.Instance.AddObserver(this);
        doOnce = true;
        doOnce1 = true;
    }

    private void Update()
    {
        //�����ǽ���תͷ�󣬲���Ц����ͬʱ��ʼ�򿪺�ɫ��������
        if (LookAway.Instance.endRotate)
        { 
            if (doOnce)
            {
                SoundManager.instance.PlaySound("sfx_collect");
                DeathPanel.Instance.ShowDeath();
                doOnce = false;
            }
        }
        //����ɫ����������ʾ��Ϻ󣬿�ʼReset:����λ�ú;�ͷ��powerUp����
        if (DeathPanel.Instance.haveShown)
        {
            if (doOnce1)
            {
                Reset();
                LookAway.Instance.endRotate = false;
                doOnce = true;

                doOnce1 = false;
            }
        }

        if (DeathPanel.Instance.startFadeOut)
        {
            if (DeathPanel.Instance.CanvasGroup.alpha == 0)
            {
                PlayerMovement.Instance.enabled = true;
                MouseLook.Instance.enabled = true;
                LookAway.Instance.startRotate = false;
                doOnce1 = true;
                DeathPanel.Instance.startFadeOut = false;
            }
        }
    }
    void OnDisable()
    {
        if (!GameManager.IsInitialized) return;
        GameManager.Instance.RemoveObserver(this);
    }
    public void EndNotify()
    { 
        health--;
        PlayerMovement.Instance.enabled = false;
        MouseLook.Instance.enabled = false;

        LookAway.Instance.startRotate = true;

        if (health <= 0)
        {
            SceneManager.LoadScene(0);
        }
        
    }

    private void Reset()
    {
        transform.position = InitPosition;
        transform.eulerAngles = new Vector3(0, 0, 0);
        PowerUpManager.Instance.Reset();
    }

}
