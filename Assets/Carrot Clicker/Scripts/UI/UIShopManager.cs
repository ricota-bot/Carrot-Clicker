using System;
using UnityEngine;

public class UIShopManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private RectTransform shopPanel;
    [SerializeField] private RectTransform upgradePanel;
    [SerializeField] private RectTransform settingsPanel;

    [Header("Settings")]
    private Vector2 openedPosition;
    private Vector2 closedPositionShop;
    private Vector2 closedPositionUpgrade;
    private Vector2 closedPositionSettings;

    [Header("Actions")]
    public static Action OnPanelMoved;

    private void Start()
    {
        openedPosition = Vector2.zero;

        closedPositionShop = new Vector2(-shopPanel.rect.width, 0);
        closedPositionUpgrade = new Vector2(0, -upgradePanel.rect.height);
        closedPositionSettings = new Vector2(settingsPanel.rect.width, 0);

        // Inicializa já fechados
        shopPanel.anchoredPosition = closedPositionShop;
        upgradePanel.anchoredPosition = closedPositionUpgrade;
        settingsPanel.anchoredPosition = closedPositionSettings;
    }

    private void MovePanel(RectTransform panel, Vector2 targetPos)
    {
        LeanTween.cancel(panel);
        LeanTween.move(panel, targetPos, .3f)
            .setEase(LeanTweenType.easeOutSine)
            .setIgnoreTimeScale(true);

        OnPanelMoved?.Invoke();
    }

    #region SHOP
    public void OpenShopButton() => MovePanel(shopPanel, openedPosition);
    public void CloseShopButton() => MovePanel(shopPanel, closedPositionShop);
    #endregion

    #region UPGRADE
    public void OpenUpgradeButton() => MovePanel(upgradePanel, openedPosition);
    public void CloseUpgradeButton() => MovePanel(upgradePanel, closedPositionUpgrade);
    #endregion

    #region SETTINGS
    public void OpenSettingsButton() => MovePanel(settingsPanel, openedPosition);
    public void CloseSettingsButton() => MovePanel(settingsPanel, closedPositionSettings);
    #endregion
}
