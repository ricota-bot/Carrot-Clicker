using UnityEngine;

public class DailyRewardsManager : MonoBehaviour
{
    private const string DAILYREWARDS_INDEX_KEY = "dailyRewardKey";

    [Header("Elements")]
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
            dailyRewardContainers[i].Configure(rewardAmount, icon, day);
        }
    }

    public void ClaimButtonCallBack()
    {
        DailyRewardsData dailyReward = dailyRewardsData[dailyRewardIndex];

        RewardPlayer(dailyReward);

        dailyRewardIndex++;
        SaveData();
    }

    private void RewardPlayer(DailyRewardsData dailyReward)
    {
        switch (dailyReward.rewardType)
        {
            case EDailyRewardType.Carrots:

                break;
            case EDailyRewardType.Upgrade:

                break;
                default:
                Debug.LogWarning("ERROR.. NOT REWARD PLAYER");
                break;
        }
    }

    private void LoadData()
    {
        dailyRewardIndex = PlayerPrefs.GetInt("DAILYREWARDS_KEY");
    }
    private void SaveData()
    {
        PlayerPrefs.SetInt("DAILYREWARDS_KEY", dailyRewardIndex);
    }
}


