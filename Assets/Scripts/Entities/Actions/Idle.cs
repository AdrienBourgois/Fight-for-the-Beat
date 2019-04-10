using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    [CreateAssetMenu(menuName = "Action/Idle")]
    public class Idle : Action
    {
        public override void Execute(GameObject entitie)
        {
            Animator animator = entitie.GetComponent<Animator>();
            if (animator)
            {
                animator.SetTrigger("Idle");
            }
        }
    }
}
