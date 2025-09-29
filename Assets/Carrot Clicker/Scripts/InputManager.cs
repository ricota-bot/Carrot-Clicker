using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Actions")]
    public static Action OnObjectClicked;
    public static Action<Vector2> OnObjectClickedPosition;

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

        OnObjectClicked?.Invoke(); // Caso esse Objeto receba a colisão, chamamos um Evento
        OnObjectClickedPosition?.Invoke(worldPosition);
    }
}
