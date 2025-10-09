using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardContainer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI rewardAmountText;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI dayText;


    public void Configure(string rewardAmount, Sprite icon, string day)
    {
        rewardAmountText.text = rewardAmount;
        this.icon.sprite = icon;
        dayText.text = day;
    }
}
