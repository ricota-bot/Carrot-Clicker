using System;
using UnityEngine;

[RequireComponent(typeof(DailyRewardsUI))]
public class DailyRewardsManager : MonoBehaviour
{
    private const string DAILYREWARDS_INDEX_KEY = "DailyRewardKey";
    private const string LAST_CLAIM_DATE_KEY = "LastClaimDateKey";

    [Header("Actions")]
    public static Action<bool> OnClaimIsPossible;

    [Header("References")]
    private DailyRewardsUI dailyRewardsUI;

    [Header("Elements")]
    [SerializeField] private DailyRewardContainer[] dailyRewardContainers;

    [Header("Data")]
    [SerializeField] private DailyRewardsData[] dailyRewardsData;

    private DateTime lastClaimDateTime;

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
                rewardAmount = dailyRewardsData[i].amount.ToString("F0") + " Level";

            Sprite icon = dailyRewardsData[i].icon;
            string day = $"Day 0{i + 1}";

            bool claimed = false;
            if (dailyRewardIndex > i)
                claimed = true;
            dailyRewardContainers[i].Configure(rewardAmount, icon, day, claimed);
        }
    }

    public void ClaimRewards()
    {
        DailyRewardsData dailyReward = dailyRewardsData[dailyRewardIndex];

        RewardPlayer(dailyReward);

        dailyRewardIndex++;
        SaveData();

        UpdateRewardContainers();
    }

    public void CheckIfCanClaim()
    {
        TimeSpan timeAway = DateTime.Now.Subtract(lastClaimDateTime);

        double elapsedHours = timeAway.TotalHours;
        if (elapsedHours < 24)// Menor que 24 horas ou seja um dia
        {
            int hoursInSeconds = 60 * 60 * 24;
            int secondsAway = hoursInSeconds - (int)timeAway.TotalSeconds;

            dailyRewardsUI.InitializeTimer(secondsAway);
            Debug.Log("TIMER");
        }
        else if (elapsedHours <= 0)
        {
            Debug.Log("CLAIM!");
        }
    }

    public bool AllRewardsHaveBeenClaimed()
    {
        if (dailyRewardIndex > 6)
            return true;
        else return false;
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

    private void LoadData()
    {
        dailyRewardsUI = GetComponent<DailyRewardsUI>();
        dailyRewardIndex = PlayerPrefs.GetInt(DAILYREWARDS_INDEX_KEY);

        if (LoadLastClaimDateTime())
            CheckIfCanClaim();
        //else
        //    Debug.LogError("[Load Data] Not possible to Load your Data!");
    }
    private void SaveData()
    {
        PlayerPrefs.SetInt(DAILYREWARDS_INDEX_KEY, dailyRewardIndex);

        SaveLastDateTime();
    }


    private bool LoadLastClaimDateTime()
    {
        string savedDate = PlayerPrefs.GetString(LAST_CLAIM_DATE_KEY);
        bool validDateTime = DateTime.TryParse(savedDate, null, System.Globalization.DateTimeStyles.RoundtripKind, out lastClaimDateTime);

        return validDateTime;
    }

    private void SaveLastDateTime()
    {
        string now = DateTime.Now.ToString("o");

        PlayerPrefs.SetString(LAST_CLAIM_DATE_KEY, now);
        PlayerPrefs.Save();
        Debug.LogWarning($"[Daily Rewards] Salvou data: {now}");
    }
}


