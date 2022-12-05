using BaseTemplate.Behaviours;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public MenuCanvas MenuCanvas;
    public GameCanvas GameCanvas;
    public EndCanvas EndCanvas;

    [SerializeField] CanvasGroup MenuCanvasGroup, GameCanvasGroup, EndCanvasGroup;

    CanvasGroup actualCanvasGroup;

    public void Init()
    {
        actualCanvasGroup = MenuCanvasGroup;

        MenuCanvas.Init();
    }

    public void StartGame()
    {
        SwitchToCanvas(GameCanvasGroup);
    }

    public void EndGame()
    {
        SwitchToCanvas(EndCanvasGroup);
    }




    public void SwitchToCanvas(CanvasGroup toCanvas)
    {
        if (actualCanvasGroup == toCanvas) return;

        if (toCanvas == MenuCanvasGroup) MenuCanvas.Init();
        if (toCanvas == GameCanvasGroup) GameCanvas.Init();

        actualCanvasGroup.interactable = false;
        actualCanvasGroup.blocksRaycasts = false;
        actualCanvasGroup.alpha = 0;

        toCanvas.interactable = true;
        toCanvas.blocksRaycasts = true;
        toCanvas.DOFade(1, .3f);



        actualCanvasGroup = toCanvas;
    }
}
