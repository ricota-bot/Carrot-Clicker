using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Carrot : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform carrotRendererTransform;
    [SerializeField] private Image carrotFillImage;
    [SerializeField] private GameObject particlesParent;

    [Header("Settings")]
    [SerializeField] private float fillRate;

    [Header("Actions")]
    public static Action OnFrenzzyModeStarted;
    public static Action OnFrenzzyModeStopped;

    private bool isFrenzyModeActive;
    private Vector3 initialCarrotScale;

    private void Awake()
    {
        initialCarrotScale = carrotRendererTransform.localScale;

        InputManager.OnObjectClicked += OnObjectClickedCallBack;
    }

    private void OnDestroy()
    {
        InputManager.OnObjectClicked -= OnObjectClickedCallBack;

    }

    private void OnObjectClickedCallBack()
    {
        // Animate Carrot
        AnimateCarrotsOnClick();

        if (!isFrenzyModeActive)
            // Fill Method
            Fill();
    }

    private void AnimateCarrotsOnClick()
    {
        carrotRendererTransform.localScale = initialCarrotScale;
        LeanTween.cancel(carrotRendererTransform.gameObject);
        LeanTween.scale(carrotRendererTransform.gameObject, Vector3.one * 0.7f, 0.15f).setLoopPingPong(1);
    }

    private void Fill()
    {
        carrotFillImage.fillAmount += fillRate;

        if (carrotFillImage.fillAmount >= 1)
        {
            StartFrenzyMode();
        }
    }

    private void StartFrenzyMode()
    {
        isFrenzyModeActive = true;
        particlesParent.SetActive(true);

        LeanTween.value(1, 0, 10f).setOnUpdate((value) => carrotFillImage.fillAmount = value).setOnComplete(StopFrenzyMode);
        OnFrenzzyModeStarted?.Invoke();
    }

    private void StopFrenzyMode()
    {
        isFrenzyModeActive = false;
        particlesParent.SetActive(false);
        OnFrenzzyModeStopped?.Invoke();
    }
}
