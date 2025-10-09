using System;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class BonusParticleManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private CarrotManager carrotManager;
    [SerializeField] private BonusParticle bonusParticlePrefab;

    private ObjectPool<BonusParticle> bonusParticlePool;

    private void Awake()
    {
        InputManager.OnCarrotClickedPosition += OnCarrotClickedPositionCallBack;
    }


    private void OnDestroy()
    {

        InputManager.OnCarrotClickedPosition -= OnCarrotClickedPositionCallBack;

    }

    private void Start()
    {
        bonusParticlePool = new ObjectPool<BonusParticle>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy);
    }

    private void ActionOnDestroy(BonusParticle bonusParticle)
    {
        Destroy(bonusParticle.gameObject);
    }

    private void ActionOnRelease(BonusParticle bonusParticle)
    {
        bonusParticle.gameObject.SetActive(false);
    }

    private void ActionOnGet(BonusParticle bonusParticle)
    {
        bonusParticle.gameObject.SetActive(true);
    }

    private BonusParticle CreateFunc()
    {
        return Instantiate(bonusParticlePrefab, transform);
    }

    private void OnCarrotClickedPositionCallBack(Vector2 position)
    {
        BonusParticle bonusParticleInstance = bonusParticlePool.Get();
        bonusParticleInstance.transform.position = position;

        bonusParticleInstance.Configure(carrotManager.CarrotIncrement);

        LeanTween.delayedCall(1, () => bonusParticlePool.Release(bonusParticleInstance));
    }
}
