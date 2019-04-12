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
            Entity entity = collector.GetComponent<Entity>();
            if (entity)
            {
                entity.Dodge = false;
                entity.Move(SpaceLenght);
                entity.animator.SetTrigger("Move");
            }
        }
    }
}
