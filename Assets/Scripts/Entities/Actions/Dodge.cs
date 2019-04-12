using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    [CreateAssetMenu(menuName = "Action/Dodge")]
    public class Dodge : Action
    {
        public override void Execute(GameObject collector)
        {
            Entity entity = collector.GetComponent<Entity>();
            if (entity)
            {
                entity.Dodge = true;
                entity.animator.SetTrigger("Dodge");
            }
        }
    }
}
