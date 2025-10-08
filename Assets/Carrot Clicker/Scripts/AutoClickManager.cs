using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AutoClickManager : MonoBehaviour
{
    private const string LevelKey = "levelKey";
    [Header("Data")]
    [SerializeField] private int level;

    [Header("Rotator")]
    [SerializeField] private Transform rotator;
    [SerializeField] private float rotatorSpeed;
    [SerializeField] private GameObject bunnyPrefab;
    [SerializeField] private float rotatorRadius;
    [SerializeField] private int maxBunnies = 72;


    private List<GameObject> bunnyPool = new List<GameObject>();

    private void Awake()
    {
        ShopManager.OnUpgradeButtonPurchased += OnUpgradeButtonPurchasedCallBack;
    }

    private void OnDestroy()
    {
        ShopManager.OnUpgradeButtonPurchased -= OnUpgradeButtonPurchasedCallBack;

    }

    private void OnUpgradeButtonPurchasedCallBack(int index)
    {
        CheckIfCanUpgrade(index);
    }

    private void Start()
    {
        LoadData();
        //SpawnBunny();

        CreatePool();
        InitBunniesFromLevel();
    }

    private void Update()
    {
        rotator.transform.Rotate(Vector3.forward * Time.deltaTime * rotatorSpeed);
    }

    private void CheckIfCanUpgrade(int index)
    {
        if (index == 0) // Caso seja o primeiro Upgrade -> Esse é o AutoClicker
            UpgradeCarrotsLevelButton();

    }

    public void UpgradeCarrotsLevelButton() // Upgrade apenas no Carrot Clicker
    {
        level++;
        if (level <= maxBunnies)
            ActivateBunny(level - 1); // ativa o próximo
    }

    private void InitBunniesFromLevel()
    {
        for (int i = 0; i < level && i < bunnyPool.Count; i++)
        {
            bunnyPool[i].SetActive(true);
        }
    }

    private void CreatePool()
    {
        float angleStep = 10;


        float radius1 = rotatorRadius;      // primeiro círculo
        float radius2 = 3.2f; // segundo círculo (maior)

        for (int i = 0; i < maxBunnies; i++)
        {
            float radius = (i < 36) ? radius1 : radius2;
            float angle = i * angleStep;

            Vector2 position = new Vector2(
                radius * Mathf.Cos(angle * Mathf.Deg2Rad),
                radius * Mathf.Sin(angle * Mathf.Deg2Rad)
            );

            var bunny = Instantiate(bunnyPrefab, position, Quaternion.identity, rotator);
            bunny.transform.up = position.normalized;
            bunny.SetActive(false); // começa desativado
            bunnyPool.Add(bunny);
        }
    }

    private void ActivateBunny(int index)
    {
        if (index >= 0 && index < bunnyPool.Count)
            bunnyPool[index].SetActive(true);
    }

    private void LoadData()
    {
        level = ShopManager.Instance.GetUpgradeLevel(0);
    }
}
