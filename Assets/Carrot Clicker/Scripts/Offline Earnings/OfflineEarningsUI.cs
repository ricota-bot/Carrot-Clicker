using System;
using UnityEngine;

public class OfflineEarningsUI : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private OfflineEarningsPopup offlineEarningsPopup;


    private void Start()
    {
        //offlineEarningsPopup.gameObject.SetActive(false);
    }

    public void DisplayPopup(double earnings)
    {
        offlineEarningsPopup.Configure(earnings.ToString("F0"));

        offlineEarningsPopup.gameObject.SetActive(true);

        offlineEarningsPopup.GetClaimButton().onClick.AddListener(() => ClaimButtonClickedCallBack(earnings));
    }

    private void ClaimButtonClickedCallBack(double earnings)
    {
        offlineEarningsPopup.gameObject.SetActive(false);
        CarrotManager.instance.AddCarrots(earnings);


    }
}
