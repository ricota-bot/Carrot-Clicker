using System;
using UnityEngine;

[RequireComponent(typeof(OfflineEarningsUI))]
public class OfflineEarningsManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int maxOfflineTimeInSeconds;
    private const string LastDateKey = "LastDateTime";

    private OfflineEarningsUI offlineEarningsUI;

    private void Start()
    {
        offlineEarningsUI = GetComponent<OfflineEarningsUI>();
        LoadLastDateTime();
    }

    private void OnApplicationPause(bool pause)
    {
        if (!pause)
            return;

        SaveCurrentDateTime();
    }

    private void LoadLastDateTime()
    {
        // Verifica se já existe uma data salva
        if (!PlayerPrefs.HasKey(LastDateKey))
        {
            Debug.Log("[OfflineEarnings] Nenhum registro anterior — primeira vez jogando.");
            return;
        }

        string savedDate = PlayerPrefs.GetString(LastDateKey);

        if (DateTime.TryParse(savedDate, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime lastDateTime))
        {
            DateTime now = DateTime.Now; // Agora Atual
            TimeSpan timeAway = now - lastDateTime; // Calcula o tempo de agora com a ultima vez que entrou

            int secondsAway = (int)timeAway.TotalSeconds; // Transforma as horas dias.. em segundos

            Debug.Log($"You were off for: {secondsAway} seconds");
            secondsAway = Mathf.Min(secondsAway, maxOfflineTimeInSeconds);

            CalculateOfflineEarnings(secondsAway);

        }

    }

    private void SaveCurrentDateTime()
    {
        string now = DateTime.Now.ToString("o");

        PlayerPrefs.SetString(LastDateKey, now);
        PlayerPrefs.Save();
        Debug.Log($"[OfflineEarnings] Salvou data: {now}");
    }

    private void CalculateOfflineEarnings(int value)
    {
        double offlineEarnings = value * UpgradeManager.Instance.GetCarrotsPerSecond();
        Debug.Log($"Offline Carrots: {offlineEarnings}");

        if (offlineEarnings <= 100)
            return;

        offlineEarningsUI.DisplayPopup(offlineEarnings);

    }
}
