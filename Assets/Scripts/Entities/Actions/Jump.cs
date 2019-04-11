using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    [CreateAssetMenu(menuName = "Action/Jump")]
    public class Jump : Action
    {
        public override void Execute(GameObject collector)
        {
            Entity entity = collector.GetComponent<Entity>();
            if (entity)
            {
                entity.Dodge = true;
            }

            Animator animator = collector.GetComponent<Animator>();
            if (animator)
            {
                animator.SetTrigger("Jump");
            }
        }
    }
}
