using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    [CreateAssetMenu(menuName = "Action/Prepare")]
    public class Prepare : Action
    {
        public string TrigerName = "Prepare";

        public override void Execute(GameObject collector)
        {
            Entity entity = collector.GetComponent<Entity>();
            if (entity)
            {
                entity.Dodge = false;
                entity.animator.SetTrigger(TrigerName);
            }
        }
    }
}
