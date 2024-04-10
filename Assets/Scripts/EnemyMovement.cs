using System.Collections;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public Collider2D movementArea; // Ссылка на коллайдер, в котором будет перемещаться объект
    public SpriteRenderer[] spritesToFlipX; // Массив спрайтов для изменения Flip по X
    [SerializeField] private float MoveMax;
    [SerializeField] private float PauseMax;
    private bool isMovingLeft = false; // Флаг для определения направления движения

    private void Start()
    {
        StartCoroutine(MoveObject());
    }

    private IEnumerator MoveObject()
    {
        Bounds movementBounds = movementArea.bounds;

        while (true)
        {
            // Генерируем случайные значения для moveSpeed и pauseTime в указанных пределах
            float moveSpeed = Random.Range(.7f, MoveMax);
            float pauseTime = Random.Range(.5f, PauseMax);

            // Генерируем случайные координаты в пределах границ коллайдера
            float randomX = Random.Range(movementBounds.min.x, movementBounds.max.x);
            float randomY = Random.Range(movementBounds.min.y, movementBounds.max.y);

            // Получаем новую позицию внутри коллайдера для перемещения объекта
            Vector3 newPosition = new Vector3(randomX, randomY, transform.position.z);

            // Переключаем спрайты в нужное направление
            if (transform.position.x > randomX)
            {
                FlipSprites(true); // Повернуть спрайты влево
                isMovingLeft = true;
            }
            else
            {
                FlipSprites(false); // Повернуть спрайты вправо
                isMovingLeft = false;
            }

            // Плавно перемещаем объект к новой точке
            yield return StartCoroutine(MoveToPosition(newPosition, moveSpeed));

            // Ожидаем случайное время перед следующим перемещением
            yield return new WaitForSeconds(pauseTime);
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition, float moveSpeed)
    {
        float elapsedTime = 0;
        Vector3 startingPosition = transform.position;

        while (elapsedTime < 1)
        {
            elapsedTime += Time.deltaTime * moveSpeed;

            // Интерполируем плавно текущую позицию объекта к новой точке
            transform.position = Vector3.Lerp(startingPosition, targetPosition, Mathf.SmoothStep(0f, 1f, elapsedTime));

            yield return null;
        }
    }

    private void FlipSprites(bool flipX)
    {
        foreach (SpriteRenderer sprite in spritesToFlipX)
        {
            sprite.flipX = flipX;
        }
    }
}
