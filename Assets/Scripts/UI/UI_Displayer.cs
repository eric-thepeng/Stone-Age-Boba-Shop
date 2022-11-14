using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class UI_Displayer : MonoBehaviour
{
    public delegate void AfterShow();
    public delegate void AfterHide();
    public AfterShow afterShow;
    public AfterHide afterHide;
    
    CanvasGroup cg;
    public AnimationCurve showCurver, hideCurve;
    public float animationSpeed;
    
    void Awake()
    {
        cg = GetComponent<CanvasGroup>();
        afterShow = Nothing;
        afterHide = Nothing;

    }

    public void Blink()
    {
        StopAllCoroutines();
        StartCoroutine(ShowPanel());
    }

    public IEnumerator ShowPanel()
    {
        float timer = 0;
        while (cg.alpha<1)
        {
            cg.alpha = showCurver.Evaluate(timer);
            timer += Time.deltaTime * animationSpeed;
            yield return null;
        }
        cg.blocksRaycasts = true;
        cg.interactable = true;
        afterShow();
    }

    public IEnumerator HidePanel()
    {
        float timer = 0;
        while (cg.alpha > 0)
        {
            cg.alpha = hideCurve.Evaluate(timer);
            timer += Time.deltaTime * animationSpeed;
            yield return null;
        }
        cg.blocksRaycasts = false;
        cg.interactable = false;
        afterHide();
    }

    void Nothing() { }
}
