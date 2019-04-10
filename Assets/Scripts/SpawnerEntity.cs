using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEntity : MonoBehaviour
{
    public Entities.Entity PrefabEntity;
    
    public int EnemySpaceSize;
    public int EnemySpacePosition;

    void Start()
    {
        Entities.Entity newEntity = Instantiate<Entities.Entity>(PrefabEntity,transform);
        newEntity.SetSpaceIndex(EnemySpacePosition, EnemySpaceSize);
    }
}
