using System;
using UnityEngine;

public class OfflineEarningsUI : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private OfflineEarningsPopup offlineEarningsPopup;

    public void DisplayPopup(double earnings)
    {
        offlineEarningsPopup.Configure(earnings.ToString("F0")); // Configura

        offlineEarningsPopup.gameObject.SetActive(true); // Torna visivel

        offlineEarningsPopup.GetClaimButton().onClick.AddListener(() => ClaimButtonClickedCallBack(earnings));
    }

    private void ClaimButtonClickedCallBack(double earnings)
    {
        offlineEarningsPopup.gameObject.SetActive(false);
        CarrotManager.instance.AddCarrots(earnings);

    }
}
