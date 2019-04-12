using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    [CreateAssetMenu(menuName = "Action/Idle")]
    public class Idle : Action
    {
        public override void Execute(GameObject collector)
        {
            Entity entity = collector.GetComponentInChildren<Entity>();
            if (entity)
            {
                entity.Dodge = false;
            }
        }
    }
}
