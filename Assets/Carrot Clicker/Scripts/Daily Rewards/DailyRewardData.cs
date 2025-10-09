using UnityEngine;

[System.Serializable]
public struct DailyRewardsData
{
    public EDailyRewardType rewardType;
    public double amount;
    public Sprite icon;
    public int upgradeIndex;
}

