using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Entities
{
    [CreateAssetMenu(menuName = "Action/Move")]
    public class Move : Action
    {
        public int SpaceLenght;
        public Vector2 Mouvement;

        public override void Execute(GameObject collector)
        {
            CharacterController2D cc2D = collector.GetComponent<CharacterController2D>();
            if (cc2D)
            {
                //cc2D.MoveTo(Mouvement);
            }

            Entity entity = collector.GetComponent<Entity>();
            if (entity)
            {
                entity.Move(SpaceLenght);
            }

            Animator animator = collector.GetComponent<Animator>();
            if (animator)
            {
                animator.SetTrigger("Move");
            }
        }
    }
}
