using System.Collections;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public Collider2D movementArea; // ������ �� ���������, � ������� ����� ������������ ������
    public SpriteRenderer[] spritesToFlipX; // ������ �������� ��� ��������� Flip �� X
    [SerializeField] private float MoveMax;
    [SerializeField] private float PauseMax;
    private bool isMovingLeft = false; // ���� ��� ����������� ����������� ��������

    private void Start()
    {
        StartCoroutine(MoveObject());
    }

    private IEnumerator MoveObject()
    {
        Bounds movementBounds = movementArea.bounds;

        while (true)
        {
            // ���������� ��������� �������� ��� moveSpeed � pauseTime � ��������� ��������
            float moveSpeed = Random.Range(.7f, MoveMax);
            float pauseTime = Random.Range(.5f, PauseMax);

            // ���������� ��������� ���������� � �������� ������ ����������
            float randomX = Random.Range(movementBounds.min.x, movementBounds.max.x);
            float randomY = Random.Range(movementBounds.min.y, movementBounds.max.y);

            // �������� ����� ������� ������ ���������� ��� ����������� �������
            Vector3 newPosition = new Vector3(randomX, randomY, transform.position.z);

            // ����������� ������� � ������ �����������
            if (transform.position.x > randomX)
            {
                FlipSprites(true); // ��������� ������� �����
                isMovingLeft = true;
            }
            else
            {
                FlipSprites(false); // ��������� ������� ������
                isMovingLeft = false;
            }

            // ������ ���������� ������ � ����� �����
            yield return StartCoroutine(MoveToPosition(newPosition, moveSpeed));

            // ������� ��������� ����� ����� ��������� ������������
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

            // ������������� ������ ������� ������� ������� � ����� �����
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
