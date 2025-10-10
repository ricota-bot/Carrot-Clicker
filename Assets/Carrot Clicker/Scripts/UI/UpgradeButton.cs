using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI subTitle;
    [SerializeField] private TextMeshProUGUI price;
    [Space(15)]
    [SerializeField] private Button button;


    public void Configure(Sprite icon, string title, string subTitle, double price, Action onClickAction)
    {
        this.icon.sprite = icon;
        this.title.text = title;
        UpdateVisuals(subTitle, price);

        // Remove listeners antigos pra evitar acúmulo
        button.onClick.RemoveAllListeners();

        // Adiciona a nova ação
        button.onClick.AddListener(() => onClickAction());
    }

    public void UpdateVisuals(string subTitle, double price)
    {
        this.subTitle.text = subTitle;
        this.price.text = DoubleUtilities.ToIdleNotation(price);

    }
}
