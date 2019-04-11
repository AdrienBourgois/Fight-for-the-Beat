using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEntity : MonoBehaviour
{
    public Entities.Entity PrefabEntity;

    public int EnemySpaceSize;
    public int EnemySpacePosition;

    public int SpaceActivator;

    public void Spawn()
    {
        Entities.Entity newEntity = Instantiate<Entities.Entity>(PrefabEntity, transform.position, Quaternion.identity);
        newEntity.SetSpaceIndex(EnemySpacePosition, EnemySpaceSize);

        gameObject.SetActive(false);
    }

    private void Start()
    {
        GameManager.Instance.OnPlayerPlayed += DetectPlayer;
    }

    private void DetectPlayer()
    {
        Entities.Entity entity = Level.SpaceManager.Instance.GetEntityOnSpace(SpaceActivator);
        if (entity)
        {
            if (entity as Entities.Player)
                ActiveTarget();
        }
    }

    private void ActiveTarget()
    {
        Spawn();

        GameManager.Instance.OnPlayerPlayed -= DetectPlayer;
        gameObject.SetActive(false);
    }
}
