using System;
using TMPro;
using UnityEngine;

public class CarrotManager : MonoBehaviour
{
    private const string totalCarrotsKey = "TotalCarrots";
    public static CarrotManager instance;
    [Header("Data")]
    [SerializeField] private int baseCarrotMultipler;
    [SerializeField] private int frenzyModeMultipler;

    private int carrotIncrement;
    public int CarrotIncrement => carrotIncrement;
    private double totalCarrotsCount;

    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI carrotsText;
    [SerializeField] private TextMeshProUGUI carrotsPerSecondText;

    [Header("References")]
    [SerializeField] private UpgradeManager upgradeManager;

    private void Awake()
    {
        #region Singleton
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        #endregion

        Load();
        carrotIncrement = baseCarrotMultipler;

        // ACTIONS
        InputManager.OnCarrotClicked += OnCarrotClickedCallBack;
        Carrot.OnFrenzzyModeStarted += OnFrenzzyModeStartedCallBack;
        Carrot.OnFrenzzyModeStopped += OnFrenzzyModeStoppedCallBack;

        ShopManager.OnUpgradeButtonPurchased += OnUpgradeButtonPurchasedCallBack;
    }
    private void OnDestroy()
    {

        // ACTIONS
        InputManager.OnCarrotClicked -= OnCarrotClickedCallBack;
        Carrot.OnFrenzzyModeStarted -= OnFrenzzyModeStartedCallBack;
        Carrot.OnFrenzzyModeStopped -= OnFrenzzyModeStoppedCallBack;

        ShopManager.OnUpgradeButtonPurchased -= OnUpgradeButtonPurchasedCallBack;

    }



    #region ACTIONS
    private void OnFrenzzyModeStartedCallBack()
    {
        carrotIncrement = frenzyModeMultipler;

    }

    private void OnFrenzzyModeStoppedCallBack()
    {
        carrotIncrement = baseCarrotMultipler;
    }


    private void OnCarrotClickedCallBack()
    {
        AddCarrots(carrotIncrement);

    }

    private void OnUpgradeButtonPurchasedCallBack(int obj)
    {
        UpdateCarrotsPerSecondText();
    }

    #endregion

    [NaughtyAttributes.Button]
    public void Add500Carrots() => AddCarrots(500);

    public void AddCarrots(double value)
    {
        totalCarrotsCount += value;
        UpdateCarrotsText();
        Save();
    }

    public bool TryPurchase(double value)
    {
        if (value <= totalCarrotsCount)
        {
            AddCarrots(-value);
            return true;
        }
        else
            return false;
    }
    private void UpdateCarrotsText() => carrotsText.text = totalCarrotsCount.ToString("F0") + " Carrots!";
    private void UpdateCarrotsPerSecondText()
    {
        carrotsPerSecondText.text = " carrots per seconds: " + upgradeManager.GetCarrotsPerSecond().ToString("F0");
    }


    #region SAVE and LOAD
    private void Save() => PlayerPrefs.SetString(totalCarrotsKey, totalCarrotsCount.ToString());
    private void Load()
    {
        double.TryParse(PlayerPrefs.GetString(totalCarrotsKey), out totalCarrotsCount);
        UpdateCarrotsText();
        UpdateCarrotsPerSecondText();
    }
    #endregion

}
