using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSamlpe : MonoBehaviour
{
    [SerializeField] MoskitoHouse moskitoHouse;
    [SerializeField] UIParameters uIParameters;
    [SerializeField] private float HealPoint;
    [SerializeField] private float Damage;
    [SerializeField] private float EatCount;
    [SerializeField] private float LocalTimer, MainTimer;
    [SerializeField][Range(0f, 1f)] private float Speed;
    [SerializeField] private GameObject CollectPoint;
    [SerializeField] private Collider2D ColliderPoint;
    public Collider2D EnemyCollider;
    private Vector2 RandomPositionInIdle;
    private Vector2 RandomPositionInAttack;
    public float MaxDistanceFromPoint;

    public enum UnitState
    {
        FlyToCollectPoint,  // Режим полета к точке сбора
        AttackEnemy,        // Режим атаки врага
    }
    public UnitState currentState = UnitState.FlyToCollectPoint;

    public void Awake()
    {
        FindPoint();
        LocalTimer = MainTimer;
        RandomPositionInIdle = (Vector2)CollectPoint.transform.position;
    }

    private void Update()
    {
        switch (currentState)
        {
            case UnitState.FlyToCollectPoint:
                GoToPoint();  // Вызываем метод для полета к точке сбора
                break;
            case UnitState.AttackEnemy:
                Attack(EnemyCollider); // Вызываем метод для атаки врага
                break;
        }
        if (Vector2.Distance(RandomPositionInIdle, (Vector2)CollectPoint.transform.position) > MaxDistanceFromPoint)
        {
            RandomPositionInIdle = GetRandomPointInPointCollider();
        }
    }

    private void Feeding()
    {
        if (LocalTimer <= 0f)
        {
            LocalTimer = MainTimer;
            uIParameters.BloodCollect(EatCount);
        }
        else if (LocalTimer > 0f)
        {
            LocalTimer -= Time.fixedDeltaTime;
        }
    }

    public void Death()
    {
        Destroy(this);
        //Debug.Log(Name + " is Death");
    }

    public void Attack(Collider2D EnemyCollider)
    {
        RandomPositionInAttack = GetRandomPointInEnemyCollider();
        transform.position = Vector2.MoveTowards(transform.position, RandomPositionInAttack, Time.fixedDeltaTime * Speed);
        Feeding();
    }

    private Vector2 GetRandomPointInEnemyCollider()
    {
        Vector2 EnemyPointPosition = EnemyCollider.transform.position;

        float randomRadius = Random.Range(0f, MaxDistanceFromPoint);
        float randomAngle = Random.Range(0f, Mathf.PI * 2f);

        float randomX = EnemyPointPosition.x + randomRadius * Mathf.Cos(randomAngle);
        float randomY = EnemyPointPosition.y + randomRadius * Mathf.Sin(randomAngle);

        return new Vector2(randomX, randomY);
    }

    private Vector2 GetRandomPointInPointCollider()
    {
        Vector2 collectPointPosition = CollectPoint.transform.position;

        float randomRadius = Random.Range(0f, MaxDistanceFromPoint);
        float randomAngle = Random.Range(0f, Mathf.PI * 2f);

        float randomX = collectPointPosition.x + randomRadius * Mathf.Cos(randomAngle);
        float randomY = collectPointPosition.y + randomRadius * Mathf.Sin(randomAngle);

        return new Vector2(randomX, randomY);
    }

    private void FindPoint()
    {
        CollectPoint = GameObject.FindGameObjectWithTag("collection point");
        ColliderPoint = CollectPoint.GetComponent<Collider2D>();
        moskitoHouse = GameObject.FindObjectOfType<MoskitoHouse>();
        uIParameters = GameObject.FindObjectOfType<UIParameters>();
    }

    public void GoToPoint()
    {
        float distance = Vector2.Distance(transform.position, CollectPoint.transform.position);
        if (distance > MaxDistanceFromPoint)
        {
            transform.position = Vector2.MoveTowards(transform.position, CollectPoint.transform.position, Time.fixedDeltaTime * Speed);
        }
        else
        {
            // Если достигли точки, запустить Idle
            if (Vector2.Distance(transform.position, RandomPositionInIdle) < 0.02f)
            {
                Idle();
            }
            // Иначе продолжить движение
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, RandomPositionInIdle, Speed * Time.fixedDeltaTime);
            }
        }
    }

    public void Idle()
    {
        if (Vector2.Distance((Vector2)transform.position, RandomPositionInIdle) < 0.02f)
        {
            RandomPositionInIdle = GetRandomPointInPointCollider();
            //Debug.Log("Random point "+ RandomPositionInIdle);
        }
        else
        {

        }
    }

}
