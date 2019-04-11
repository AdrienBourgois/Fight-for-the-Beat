using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    [CreateAssetMenu(menuName = "Action/Attack")]
    public class Attack : Action
    {
        public string TrigerName = "Attack";
        public List<int> SpaceEffect;
        public bool Dodgeable;

        public override void Execute(GameObject collector)
        {
            Entity entity = collector.GetComponent<Entity>();
            if (entity)
            {
                foreach(int space in SpaceEffect)
                {
                    if(space > 0)
                    {
                        Entity target = entity.GetNextRelativeSpaceEntity(space);
                        if(target)
                        {
                            if (!Dodgeable || (!target.Dodge && Dodgeable))
                                target.Hit(1);
                        }
                    }
                    else if (space < 0)
                    {
                        Entity target = entity.GetPreviousRelativeSpaceEntity(space * -1);
                        if (target)
                        {
                            if (!Dodgeable || (!target.Dodge && Dodgeable))
                                target.Hit(1);
                        }
                    }
                }
            }

            Animator animator = collector.GetComponent<Animator>();
            if (animator)
            {
                animator.SetTrigger(TrigerName);
            }
        }
    }
}
