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
        public Sequence AttackDash = null;

        private Sequence CurentSequence = null;
        private int CurentActionIndex;



        protected override void Start()
        {
            base.Start();

            Life = 2;

            GameManager.Instance.OnPlayerPlayed += Controll;
        }

        void Controll()
        {
            if(GetPreviousSpaceEntity())
            {
                float rand = Random.Range(1, 2);
                if(rand == 2)
                    LaunchSequence(Attack);
                else
                    LaunchSequence(AttackLow);
            }
            else
                LaunchSequence(MoveLeft);

            ExecuteSequence();
        }


        void LaunchSequence(Sequence sequence)
        {
            if (CurentSequence) return;

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
        }

        override protected void OnDie()
        {
            gameObject.SetActive(false);
        }
    }
}
