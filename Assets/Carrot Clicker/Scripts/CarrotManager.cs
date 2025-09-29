using System;
using TMPro;
using UnityEngine;

public class CarrotManager : MonoBehaviour
{
    private const string totalCarrotsKey = "TotalCarrots";
    [Header("Data")]
    [SerializeField] private int baseCarrotMultipler;
    [SerializeField] private int frenzyModeMultipler;
    private int carrotIncrement;
    public int CarrotIncrement => carrotIncrement;
    private double totalCarrotsCount;

    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI carrotsText;

    private void Awake()
    {
        carrotIncrement = baseCarrotMultipler;
        InputManager.OnObjectClicked += OnObjectClickedCallBack;
        Carrot.OnFrenzzyModeStarted += OnFrenzzyModeStartedCallBack;
        Carrot.OnFrenzzyModeStopped += OnFrenzzyModeStoppedCallBack;
        Load();
    }
    private void OnDestroy()
    {
        InputManager.OnObjectClicked -= OnObjectClickedCallBack;

        Carrot.OnFrenzzyModeStarted -= OnFrenzzyModeStartedCallBack;
        Carrot.OnFrenzzyModeStopped -= OnFrenzzyModeStoppedCallBack;
    }


    private void OnFrenzzyModeStartedCallBack()
    {
        carrotIncrement = frenzyModeMultipler;

    }

    private void OnFrenzzyModeStoppedCallBack()
    {
        carrotIncrement = baseCarrotMultipler;
    }


    private void OnObjectClickedCallBack()
    {
        totalCarrotsCount += carrotIncrement;

        UpdateCarrotsText();
        Save();
    }


    private void UpdateCarrotsText()
    {
        carrotsText.text = totalCarrotsCount.ToString() + " Carrots!";

    }

    private void Save()
    {
        PlayerPrefs.SetString(totalCarrotsKey, totalCarrotsCount.ToString());
    }

    private void Load()
    {
        double.TryParse(PlayerPrefs.GetString(totalCarrotsKey), out totalCarrotsCount);

        UpdateCarrotsText();
    }

}
