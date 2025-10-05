using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Actions")]
    public static Action OnCarrotClicked;
    public static Action<Vector2> OnCarrotClickedPosition;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
                ThrowRayCast(touch.position);
        }
    }

    private void ThrowRayCast(Vector2 position)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
        worldPosition.z = 0; // força no plano do 2D
        Collider2D hit = Physics2D.OverlapPoint(worldPosition);
        if (hit == null) // Caso não colidiu com nada que tenha collider
            return;

        Debug.Log($"Colidiu com {hit.name}");

        OnCarrotClicked?.Invoke(); // Caso esse Objeto receba a colisão, chamamos um Evento
        OnCarrotClickedPosition?.Invoke(worldPosition);
    }
}
