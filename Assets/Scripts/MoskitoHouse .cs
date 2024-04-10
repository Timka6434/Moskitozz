using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoskitoHouse : MonoBehaviour
{
    [SerializeField] private GameObject GameObjectHouse;
    [SerializeField] public GameObject ObjectToSpawn;
    [SerializeField] private float HealPoint;
    [SerializeField] public GameObject[] UnitContainer;
    [SerializeField] private GameObject SpawnParent;
    List<GameObject> Unitlist = new List<GameObject>();   

    public void InitializateSpawn()
    {
        //Spawn(GameObjectHouse.transform, ObjectToSpawn[CountObject]);
       // Debug.Log(Defend);
    }

    public void Spawn()
    {
        GameObject newSpawnedObject = Instantiate(ObjectToSpawn, SpawnParent.transform);
        AddToUnitContainer(newSpawnedObject);
    }
    public void AddToUnitContainer(GameObject gameObject)
    {
        Unitlist.Add(gameObject.gameObject);   
        UnitContainer = Unitlist.ToArray();
    }

}
