using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEntity : MonoBehaviour
{
    public Entities.Entity PrefabEntity;
    
    public int EnemySpaceSize;
    public int EnemySpacePosition;
    
    public void Spawn()
    {
        Entities.Entity newEntity = Instantiate<Entities.Entity>(PrefabEntity, transform.position, Quaternion.identity);
        newEntity.SetSpaceIndex(EnemySpacePosition, EnemySpaceSize);
        
        gameObject.SetActive(false);
    }
}
