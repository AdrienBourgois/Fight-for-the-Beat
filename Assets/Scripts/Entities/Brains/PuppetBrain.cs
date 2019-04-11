using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    [CreateAssetMenu(menuName = "AI/PuppetBrain")]
    public class PuppetBrain : Brain
    {
        override public void Controll(GameObject collector)
        {
            Enemy enemy = collector.GetComponent<Enemy>();
            if (enemy)
            {
                Entity target = enemy.GetPreviousSpaceEntity();

                if (target is Player)
                {
                    int rand = Random.Range(0, enemy.Attack.Count);
                    enemy.LaunchSequence(enemy.Attack[rand]);
                }
                else if (target)
                {
                    enemy.LaunchSequence(enemy.Idle);
                }
                else
                {
                    enemy.LaunchSequence(enemy.MoveLeft);
                }

                enemy.ExecuteSequence();
            }
        }
    }
}
