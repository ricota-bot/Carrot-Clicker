using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OfflineEarningsPopup : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI offlineEarningsText;
    [SerializeField] private Button claimButton;


    public void Configure(string earningsText)
    {
        offlineEarningsText.text = DoubleUtilities.ToIdleNotation(Double.Parse(earningsText)); // Posso formatar quando chamar o metodo Configure "F0"
    }

    public Button GetClaimButton() => claimButton;

}
