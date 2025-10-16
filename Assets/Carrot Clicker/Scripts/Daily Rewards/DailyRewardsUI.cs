using System;
using TMPro;
using UnityEngine;

public class DailyRewardsUI : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private DailyRewardsManager dailyRewardsManager;
    [SerializeField] private GameObject dailyRewardPanel;
    [SerializeField] private GameObject isPossibleToClaimIcon;

    [Header("Timer Elements")]
    [SerializeField] private GameObject claimButton;
    [SerializeField] private GameObject timerContainer;
    [SerializeField] private TextMeshProUGUI timerText;

    private int timerInSeconds;

    private void Awake()
    {
        DailyRewardsManager.OnClaimIsPossible += OnClaimIsPossibleCallBack;
    }

    private void OnDestroy()
    {
        DailyRewardsManager.OnClaimIsPossible -= OnClaimIsPossibleCallBack;

    }

    private void Start()
    {
        AllRewardsHaveBeenClaimed();
    }

    private void OnClaimIsPossibleCallBack(bool canClaim)
    {
        // Mostra/esconde o painel principal (opcional)
        dailyRewardPanel.SetActive(canClaim);

        // Mostra o ícone no botão se PODE reivindicar
        isPossibleToClaimIcon.SetActive(canClaim);

        Debug.Log($"[DailyRewardsUI] Pode reivindicar: {canClaim}");

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timerInSeconds /= 2;
        }

    }

    public void OpenButtonCallBack()
    {
        dailyRewardPanel.SetActive(true);

    }

    public void CloseButtonCallBack()
    {
        dailyRewardPanel.SetActive(false);
    }

    public void ClaimButtonCallBack()
    {
        dailyRewardsManager.ClaimRewards();
        
        ResetTimer();

        CloseButtonCallBack();
        AllRewardsHaveBeenClaimed();
    }

    public void ResetTimer()
    {
        int timer = 60 * 60 * 24 - 1;

        InitializeTimer(timer);

    }

    public void InitializeTimer(int seconds)
    {
        claimButton.SetActive(false);
        timerContainer.SetActive(true);

        timerInSeconds = seconds;

        UpdateTimerText();

        InvokeRepeating(nameof(UpdateTimer), 0, 1);
    }


    private void UpdateTimer()
    {
        timerInSeconds--;
        UpdateTimerText();

        if (timerInSeconds <= 0)
            StopTimer();
    }

    private void StopTimer()
    {
        CancelInvoke(nameof(UpdateTimer));

        timerContainer.SetActive(false);
        claimButton.SetActive(true);

    }

    private void AllRewardsHaveBeenClaimed()
    {
        if (dailyRewardsManager.AllRewardsHaveBeenClaimed())
        {
            claimButton.SetActive(false);
            timerContainer.SetActive(false);
        }
    }

    private void UpdateTimerText()
    {
        timerText.text = TimeSpan.FromSeconds(timerInSeconds).ToString();

    }
}
