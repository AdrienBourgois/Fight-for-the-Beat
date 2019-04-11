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
                if(enemy.GetPreviousRelativeSpaceEntity(3) || enemy.GetPreviousRelativeSpaceEntity(2))
                {
                    enemy.LaunchSequence(enemy.Attack[1]);
                }
                if (enemy.GetPreviousSpaceEntity())
                {
                    enemy.LaunchSequence(enemy.Attack[0]);
                }
                else
                    enemy.LaunchSequence(enemy.MoveLeft);

                enemy.ExecuteSequence();
            }
        }
    }
}
