using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardContainer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI rewardAmountText;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private GameObject claimElements;


    public void Configure(string rewardAmount, Sprite icon, string day, bool claimed)
    {
        rewardAmountText.text = rewardAmount;
        this.icon.sprite = icon;
        dayText.text = day;

        if (claimed)
            Claim();

    }

    public void Claim()
    {
        claimElements.SetActive(true);
        // Play a sound ? mayb effects ?.. i do no you decide this :)
    }
}
