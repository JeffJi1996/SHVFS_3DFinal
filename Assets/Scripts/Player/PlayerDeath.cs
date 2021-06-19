using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : Singleton<PlayerDeath>, IEndGameObserver
{
    public int health;
    public bool doOnce;
    public bool doOnce1;
    public bool isDeath;

    public KillBy killBy;
    public enum KillBy
    {
        NormalEnemy,
        Boss,
        Spike,
    }

    private Vector3 InitPosition;
    public Vector3 InitRotation;

    private void Start()
    {
        InitPosition = transform.position;


        GameManager.Instance.AddObserver(this);
        doOnce = true;
        doOnce1 = true;
        isDeath = false;
    }

    private void Update()
    {
        //当主角结束转头后，播放笑声，同时开始打开黑色死亡界面
        if (LookAway.Instance.endRotate)
        {
            if (doOnce)
            {
                DeathPanel.Instance.ShowDeath();
                doOnce = false;
            }
        }
        //当黑色死亡界面显示完毕后，开始Reset:人物位置和镜头，powerUp重置
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
                if (health <= 0)
                {
                    SceneManager.LoadScene(0);
                }
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
        isDeath = true;
        health--;
        PlayerMovement.Instance.enabled = false;
        MouseLook.Instance.enabled = false;
        SoundManager.instance.PlaySound("sfx_playerDie");
        switch (killBy)
        {
            case KillBy.NormalEnemy:
                LookAway.Instance.startRotate = true;
                break;
            case KillBy.Boss:
                LookAway.Instance.startRotate = true;
                break;
            case KillBy.Spike:
                DeathPanel.Instance.ShowDeath();
                break;
        }

 

    }

    private void Reset()
    {
        transform.position = InitPosition;
        transform.eulerAngles = InitRotation;
        PowerUpManager.Instance.Reset();
        isDeath = false;
        PlayerAbilityControl.instance.RecoverToHuman();
        PlankManager.Instance.Reset();
    }

}
