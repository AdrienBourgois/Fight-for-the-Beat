using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    [CreateAssetMenu(menuName = "Action/Attack")]
    public class Attack : Action
    {
        public override void Execute(GameObject collector)
        {
            Animator animator = collector.GetComponent<Animator>();
            if (animator)
            {
                animator.SetTrigger("Attack");
            }
        }
    }
}
