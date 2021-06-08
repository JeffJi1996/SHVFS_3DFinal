using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeathPanel : Singleton<DeathPanel>
{
    [SerializeField] private float existTime;
    public Text LifeText;
    public Text KillText;
    public bool haveShown;
    public bool startFadeOut;
    public CanvasGroup CanvasGroup;

    void Start()
    {
        haveShown = false;
        startFadeOut = false;
        CanvasGroup = GetComponent<CanvasGroup>();
    }
    void Update()
    {
        //I want to Reset all the things when this black panel has fully shown.
        //So when the alpha is 1, means it has fully revealed.
        if (CanvasGroup.alpha == 1)
        {
            haveShown = true;
        }
        else
        {
            haveShown = false;
        }
    }
    public void ShowDeath()
    {
        LifeText.text = "<color=white>" + "You Have " + "</color>" + "<color=red>" + PlayerDeath.Instance.health+"</color>"+" lives.";
        StartCoroutine(ShowDeathPanel());
    }
    IEnumerator ShowDeathPanel()
    {
        GetComponent<UIFadeInOut>().FadeIn();
        yield return new WaitForSeconds(existTime);
        startFadeOut = true;
        GetComponent<UIFadeInOut>().FadeOut();
    }

    

}
