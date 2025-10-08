using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    [Header("Settings")]
    [Tooltip("Value in Hertz")]
    [SerializeField] private int addCarrotsFrequency;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        InvokeRepeating("AddCarrots", 1, 1f/ addCarrotsFrequency);
        
    }

    private void AddCarrots()
    {
        double totalCarrots = GetCarrotsPerSecond();

        CarrotManager.instance.AddCarrots(totalCarrots/ addCarrotsFrequency);

        Debug.Log($"Carrots per second: {GetCarrotsPerSecond()}");
    }

    public double GetCarrotsPerSecond()
    {
        UpgradeButtonData[] upgradeButtonDatas = ShopManager.Instance.GetUpgradeButtonsData();

        if (upgradeButtonDatas.Length <= 0) // Don't have any Upgrades in This array "Remember 0 is AutoClick"
            return 0;

        double totalCarrots = 0;

        for (int i = 0; i < upgradeButtonDatas.Length; i++)
        {
            double carrotsPerUpgradeButton = upgradeButtonDatas[i].cpsPerLevel * ShopManager.Instance.GetUpgradeLevel(i);
            totalCarrots += carrotsPerUpgradeButton;
        }

        return totalCarrots;
    }
}
