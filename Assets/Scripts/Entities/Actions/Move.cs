using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Entities
{
    [CreateAssetMenu(menuName = "Action/Move")]
    public class Move : Action
    {
        public Vector2 Mouvement;
        public float Speed;

        public override void Execute(GameObject entitie)
        {
            CharacterController2D cc2D = entitie.GetComponent<CharacterController2D>();
            if (cc2D)
            {
                cc2D.MoveTo(Mouvement);
            }

            Animator animator = entitie.GetComponent<Animator>();
            if (animator)
            {
                animator.SetTrigger("Move");
            }
        }
    }
}
