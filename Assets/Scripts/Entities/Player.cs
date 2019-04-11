using UnityEngine;

namespace Entities
{
    public class Player : Entity
    {
        public Sequence Idle = null;
        public Sequence MoveLeft = null;
        public Sequence MoveRight = null;
        public Sequence Attack = null;
        public Sequence Jump = null;

        private Sequence CurentSequence = null;
        private int CurentActionIndex;

        private Animator animator;

        protected override void Start()
        {
            base.Start();
            animator = GetComponent<Animator>();

            GameInputs.InputManager.Instance.OnKeyPressed += Controll;

            //GameInputs.InputManager.Instance.OnNoKeyPressed += () => { LaunchSequence(Idle); ExecuteSequence(); GameManager.Instance.CombotReseted();; };
            GameManager.Instance.OnComboIncreased += ChangeAnimatorLayer;
            GameManager.Instance.OnCombotReseted += () => { ChangeAnimatorLayer(0); };
        }

        void Controll(GameInputs.InputManager.Keys key)
        {
            switch (key)
            {
                case (GameInputs.InputManager.Keys.Right):
                    if(GetNextSpaceEntity())
                        LaunchSequence(Attack);
                    else
                        LaunchSequence(MoveRight);
                    break;
                case (GameInputs.InputManager.Keys.Left):
                        LaunchSequence(MoveLeft);
                    break;
                case (GameInputs.InputManager.Keys.Up):
                    LaunchSequence(Jump);
                    break;
                case (GameInputs.InputManager.Keys.Down):
                    LaunchSequence(Idle);
                    break;
                default:
                    break;
            }

            ExecuteSequence();
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
            GameManager.Instance.PlayerPlayed();
        }


        override protected void OnHit(int damage)
        {
            GameManager.Instance.CombotReseted();
        }

        override protected void OnDie()
        {
            gameObject.SetActive(false);
        }

        void ChangeAnimatorLayer(int layer)
        {
            switch(layer)
            {
                case (0):
                    animator.SetLayerWeight(animator.GetLayerIndex("Combo1"), 0);
                    animator.SetLayerWeight(animator.GetLayerIndex("Combo2"), 0);
                    break;
                case (1):
                    animator.SetLayerWeight(animator.GetLayerIndex("Combo1"), 1);
                    animator.SetLayerWeight(animator.GetLayerIndex("Combo2"), 0);
                    break;
                case (2):
                    animator.SetLayerWeight(animator.GetLayerIndex("Combo1"), 0);
                    animator.SetLayerWeight(animator.GetLayerIndex("Combo2"), 1);
                    break;
                default:
                    break;
            }
        }
    }
}
