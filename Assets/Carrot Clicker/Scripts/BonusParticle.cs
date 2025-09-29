using TMPro;
using UnityEngine;

public class BonusParticle : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshPro bonusText;

    public void Configure(int carrotMultipler)
    {
        bonusText.text = "+" + carrotMultipler.ToString();
    }

}
