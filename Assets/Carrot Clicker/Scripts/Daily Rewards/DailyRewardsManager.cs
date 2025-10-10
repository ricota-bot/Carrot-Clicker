using UnityEngine;

public class DailyRewardsManager : MonoBehaviour
{
    private const string DAILYREWARDS_INDEX_KEY = "dailyRewardKey";

    [Header("Elements")]
    [SerializeField] private GameObject dailyRewardPanel;
    [SerializeField] private DailyRewardContainer[] dailyRewardContainers;

    [Header("Data")]
    [SerializeField] private DailyRewardsData[] dailyRewardsData;

    private int dailyRewardIndex;

    private void Awake()
    {
        LoadData();
    }

    private void Start()
    {
        InitializeDailyRewardsContainers();
    }

    private void InitializeDailyRewardsContainers()
    {
        for (int i = 0; i < dailyRewardContainers.Length; i++) // Loop por todos os container
        {
            string rewardAmount = DoubleUtilities.ToIdleNotation(dailyRewardsData[i].amount);

            if (dailyRewardsData[i].rewardType == EDailyRewardType.Upgrade)
                rewardAmount = dailyRewardsData[i].amount.ToString("F0");

            Sprite icon = dailyRewardsData[i].icon;
            string day = $"Day 0{i + 1}";

            bool claimed = false;
            if (dailyRewardIndex > i)
                claimed = true;
            dailyRewardContainers[i].Configure(rewardAmount, icon, day, claimed);
        }
    }

    public void ClaimButtonCallBack()
    {
        DailyRewardsData dailyReward = dailyRewardsData[dailyRewardIndex];

        RewardPlayer(dailyReward);

        dailyRewardIndex++;
        SaveData();

        UpdateRewardContainers();
    }

    private void UpdateRewardContainers()
    {
        for (int i = 0; i < dailyRewardContainers.Length; i++) // Loop por todos os container
        {
            if (dailyRewardIndex > i)
                dailyRewardContainers[i].Claim();
        }
    }

    private void RewardPlayer(DailyRewardsData dailyReward)
    {
        switch (dailyReward.rewardType)
        {
            case EDailyRewardType.Carrots:
                RewardCarrots(dailyReward);
                break;
            case EDailyRewardType.Upgrade:
                RewardUpgrade(dailyReward);
                break;
            default:
                Debug.LogWarning("ERROR.. NOT REWARD PLAYER");
                break;
        }
    }

    private void RewardCarrots(DailyRewardsData dailyRewardData)
    {
        CarrotManager.instance.AddCarrots(dailyRewardData.amount);
    }

    private void RewardUpgrade(DailyRewardsData dailyRewardData)
    {
        ShopManager.Instance.RewardUpgrade(dailyRewardData.upgradeIndex, (int)dailyRewardData.amount);
    }

    public void OpenButtonCallBack()
    {
        dailyRewardPanel.SetActive(true);

    }

    public void CloseButtonCallBack()
    {
        dailyRewardPanel.SetActive(false);
    }

    private void LoadData()
    {
        dailyRewardIndex = PlayerPrefs.GetInt(DAILYREWARDS_INDEX_KEY);
    }
    private void SaveData()
    {
        PlayerPrefs.SetInt(DAILYREWARDS_INDEX_KEY, dailyRewardIndex);
    }
}


