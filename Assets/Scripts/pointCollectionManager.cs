using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointCollectionManager : MonoBehaviour
{
    private MoskitoHouse moskitoHouse;

    private void Start()
    {
        moskitoHouse = FindObjectOfType<MoskitoHouse>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            foreach (GameObject unitSamlpe in moskitoHouse.UnitContainer)
            {
                Collider2D enemyCollider = collision.gameObject.GetComponent<Collider2D>();
                unitSamlpe.GetComponent<UnitSamlpe>().EnemyCollider = enemyCollider;
                unitSamlpe.GetComponent<UnitSamlpe>().currentState = UnitSamlpe.UnitState.AttackEnemy;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (GameObject unitSamlpe in moskitoHouse.UnitContainer)
        {
            Collider2D enemyCollider = collision.gameObject.GetComponent<Collider2D>();
            unitSamlpe.GetComponent<UnitSamlpe>().EnemyCollider = null;
            unitSamlpe.GetComponent<UnitSamlpe>().currentState = UnitSamlpe.UnitState.FlyToCollectPoint;
        }
    }
}
