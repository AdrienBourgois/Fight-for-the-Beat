using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerActivator : MonoBehaviour
{
    public SpawnerEntity target;
    public int SpaceActivator;

    private void Start()
    {
        GameManager.Instance.OnPlayerPlayed += () => 
        {
            Entities.Entity entity = Level.SpaceManager.Instance.GetEntityOnSpace(SpaceActivator);
            if (entity)
            {
                if (entity as Entities.Player)
                    ActiveTarget();
            }
        };
    }

    public void ActiveTarget()
    {
        target?.Spawn();
    }
}
