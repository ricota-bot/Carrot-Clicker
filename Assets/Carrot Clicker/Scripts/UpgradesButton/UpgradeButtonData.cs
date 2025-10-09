using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeButtonData", menuName = "ScriptableObject/UpgradeButtonData", order = 0)]
public class UpgradeButtonData : ScriptableObject
{
    [Header("Elements")]
    public Sprite icon;
    public string title;
    public string subTitle;

    [Header("Settings")]
    public double cpsPerLevel;
    public double basePrice;
    public float coefficient;


    public double GetPrice(int level)
    {
        return basePrice * Mathf.Pow(coefficient, level);
    }

}

