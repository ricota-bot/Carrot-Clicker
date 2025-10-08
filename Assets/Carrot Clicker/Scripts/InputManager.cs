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
            ManageTouches();
        
    }

    private void ManageTouches()
    {
        int touchCount = Input.touchCount; // fixa o valor antes do loop

        for (int i = 0; i < touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began)
                ThrowRayCast(touch.position);
        }
    }


    private void ThrowRayCast(Vector2 position)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
        worldPosition.z = 0; // força no plano do 2D
        Collider2D hit = Physics2D.OverlapPoint(worldPosition);
        if (hit == null) // Caso não colidiu com nada que tenha collider apenas retornamos
            return;

        // Chamamos os Actions quando colidimos com a Cenoura no caso
        OnCarrotClicked?.Invoke(); // Caso esse Objeto receba a colisão, chamamos um Evento
        OnCarrotClickedPosition?.Invoke(worldPosition); // Posição para as particulas
    }
}
