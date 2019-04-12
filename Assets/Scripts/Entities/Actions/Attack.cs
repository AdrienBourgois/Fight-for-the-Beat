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
                entity.Dodge = false;

                foreach (int Space in SpaceEffect)
                {
                    if (Space > 0)
                    {
                        Entity target = entity.GetNextRelativeSpaceEntity(Space);
                        if (target)
                        {
                            if (!Dodgeable || (!target.Dodge && Dodgeable))
                                target.Hit(1);
                        }
                    }
                    else if (Space < 0)
                    {
                        Entity target = entity.GetPreviousRelativeSpaceEntity(Space * -1);
                        if (target)
                        {
                            if (!Dodgeable || (!target.Dodge && Dodgeable))
                                target.Hit(1);
                        }
                    }
                }
                
                entity.animator.SetTrigger(TrigerName);
            }
        }
    }
}
