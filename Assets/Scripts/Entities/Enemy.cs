using UnityEngine;

namespace Entities
{
    public class Enemy : Entity
    {
        public Sequence Idle = null;
        public Sequence MoveLeft = null;
        public Sequence MoveRight = null;
        public Sequence Attack = null;
        public Sequence AttackLow = null;
        public Sequence PrepareAttack = null;
        public Sequence PrepareAttackAttackLow = null;

        private Sequence CurentSequence = null;
        private int CurentActionIndex;


        private void Update()
        {
            if (CurentSequence == null)
            {



            }
            if (Input.GetButtonDown("Jump"))
            {
                ExecuteSequence();
            }
        }

        void LaunchSequence(Sequence sequence)
        {
            CurentSequence = sequence;
            CurentActionIndex = 0;

            if (sequence?.Actions.Count == 0)
            {
                CurentSequence = null;
            }
        }

        public void ExecuteSequence()
        {
            if (CurentSequence != null)
            {

                if (CurentSequence.Actions[CurentActionIndex] != null)
                    CurentSequence.Actions[CurentActionIndex].Execute(this.gameObject);

                if (CurentSequence.Actions.Count <= ++CurentActionIndex)
                {
                    CurentSequence = null;
                    CurentActionIndex = 0;
                }
            }
            else
            {
                LaunchSequence(Idle);
                ExecuteSequence();
            }
        }
    }
}
