using System;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    [Header("Elements")]
    [SerializeField] private UpgradeButton upgradeButtonPrefab;
    [SerializeField] private Transform upgradeButtonParent;

    [Header("DATA")]
    [SerializeField] private UpgradeButtonData[] upgradeButtonDatas;

    [Header("Actions")]
    public static Action<int> OnUpgradeButtonPurchased;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        InitializeUpgradeButtons();
    }

    private void InitializeUpgradeButtons()
    {

        for (int i = 0; i < upgradeButtonDatas.Length; i++)
        {
            SpawnButton(i);
        }

    }

    private void SpawnButton(int index)
    {

        UpgradeButton upgradeButtonInstance = Instantiate(upgradeButtonPrefab, upgradeButtonParent);

        UpgradeButtonData upgradeButtonData = upgradeButtonDatas[index];

        int upgradeLevel = GetUpgradeLevel(index);

        Sprite icon = upgradeButtonData.icon;
        string title = upgradeButtonData.title;
        string subTitle = string.Format("lvl {0} (+{1} cps)", upgradeLevel, upgradeButtonData.cpsPerLevel);
        double price = upgradeButtonData.GetPrice(upgradeLevel);

        upgradeButtonInstance.Configure(icon, title, subTitle, price, () => { UpgradeButtonClickedCallBack(index); });


    }

    // QUANDO CLICA NO BOTÃO
    private void UpgradeButtonClickedCallBack(int index)
    {
        if (CarrotManager.instance.TryPurchase(GetUpgradeButtonPrice(index)))
            IncreaseUpgradeLevel(index);
        Debug.Log("Dont have money for this !!");

        // Posso Spawnar uma Particula escrito que não tem dinheiro, igual spawnei o Click do FeedBack da cenoura

    }

    // QUANDO CLICAMOS NO BOTÃO É PRA CHAMAR ESSE METODO TAMBEM
    private void IncreaseUpgradeLevel(int index)
    {
        int currentLevel = GetUpgradeLevel(index);
        currentLevel++;

        SaveUpgradeLevel(index, currentLevel);

        UpdateButtonsVisuals(index);
        OnUpgradeButtonPurchased?.Invoke(index);

    }

    // ATUALIZAMOS A SUBTITULO E A PREÇO
    private void UpdateButtonsVisuals(int index)
    {
        UpgradeButton upgradeButton = upgradeButtonParent.GetChild(index).GetComponent<UpgradeButton>();

        UpgradeButtonData upgradeButtonData = upgradeButtonDatas[index];

        int upgradeLevel = GetUpgradeLevel(index);

        string subTitle = string.Format("lvl {0} (+{1} cps)", upgradeLevel, upgradeButtonData.cpsPerLevel);
        double price = upgradeButtonData.GetPrice(upgradeLevel);

        upgradeButton.UpdateVisuals(subTitle, price);
    }

    /// <summary>
    /// Pegamos o Preço do Item no Index Especificado
    /// </summary>
    private double GetUpgradeButtonPrice(int index)
    {
        return upgradeButtonDatas[index].GetPrice(GetUpgradeLevel(index));
    }

    // LOAD AND SAVE DATA
    /// <summary>
    /// From ShopManager -> Get The level of this current Index "From UpgradesContainers in ShopPanelUI"
    /// </summary>
    public int GetUpgradeLevel(int index)
    {
        return PlayerPrefs.GetInt("Upgrade" + index);
    }

    private void SaveUpgradeLevel(int index, int upgradeLevel)
    {
        PlayerPrefs.SetInt("Upgrade" + index, upgradeLevel);
    }

    public UpgradeButtonData[] GetUpgradeButtonsData() => upgradeButtonDatas;

}
