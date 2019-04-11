using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    [CreateAssetMenu(menuName = "AI/HatBrain")]
    public class HatBrain : Brain
    {
        override public void Controll(GameObject collector)
        {
            Enemy enemy = collector.GetComponent<Enemy>();
            if (enemy)
            {
                if (enemy.GetPreviousSpaceEntity())
                {
                    int rand = Random.Range(0, enemy.Attack.Count);
                    enemy.LaunchSequence(enemy.Attack[rand]);
                }
                else
                    enemy.LaunchSequence(enemy.MoveLeft);

                enemy.ExecuteSequence();
            }
        }
    }
}
