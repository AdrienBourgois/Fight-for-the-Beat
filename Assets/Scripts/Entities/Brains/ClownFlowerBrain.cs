using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    [CreateAssetMenu(menuName = "AI/ClownFlowerBrain")]
    public class ClownFlowerBrain : Brain
    {
        override public void Controll(GameObject collector)
        {
            Enemy enemy = collector.GetComponent<Enemy>();
            if (enemy)
            {
                Entity target1 = enemy.GetPreviousSpaceEntity();
                Entity target2 = enemy.GetPreviousRelativeSpaceEntity(2);
                Entity target3 = enemy.GetPreviousRelativeSpaceEntity(3);

                if ((target3 is Player || target2 is Player))
                {
                    enemy.LaunchSequence(enemy.Attack[1]);
                }
                if (target1 is Player)
                {
                    enemy.LaunchSequence(enemy.Attack[0]);
                }
                else if (target1)
                    enemy.LaunchSequence(enemy.Idle);
                else
                    enemy.LaunchSequence(enemy.MoveLeft);

                enemy.ExecuteSequence();
            }
        }
    }
}