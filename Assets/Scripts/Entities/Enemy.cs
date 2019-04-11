using UnityEngine;
using System.Collections.Generic;

namespace Entities
{
    public class Enemy : Entity
    {
        public Sequence Idle = null;
        public Sequence MoveLeft = null;
        public Sequence MoveRight = null;
        public List<Sequence> Attack;

        private Sequence CurentSequence = null;
        private int CurentActionIndex;

        public Brain AI;

        protected override void Start()
        {
            base.Start();
            GameManager.Instance.OnPlayerPlayed += Controll;
        }

        void Controll()
        {
            AI.Controll(this.gameObject);
        }
        
        public void LaunchSequence(Sequence sequence)
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
                Debug.Log(CurentSequence.name + " Execute " + CurentSequence.Actions[CurentActionIndex].name);

                if (CurentSequence.Actions[CurentActionIndex] != null)
                    CurentSequence.Actions[CurentActionIndex].Execute(this.gameObject);

                if (CurentSequence.Actions.Count <= ++CurentActionIndex)
                {
                    CurentSequence = null;
                    CurentActionIndex = 0;
                }
            }
        }

        override protected void OnHit(int damage)
        {
            GameManager.Instance.ComboIncreased();
        }

        override protected void OnDie()
        {
            CurentSequence = null;
            GameManager.Instance.ComboIncreased();

            GameManager.Instance.OnPlayerPlayed -= Controll;

            gameObject.SetActive(false);
        }
    }
}
