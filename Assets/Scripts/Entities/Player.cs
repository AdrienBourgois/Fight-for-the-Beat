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

        private bool m_FacingRight = true;
        private int NexLayer = 0;

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
                    if (GetNextSpaceEntity())
                        LaunchSequence(Attack);
                    else
                    {
                        if (!m_FacingRight)
                            Flip();
                        LaunchSequence(MoveRight);
                    }
                    break;
                case (GameInputs.InputManager.Keys.Left):
                    if (m_FacingRight)
                        Flip();
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
        
        void ChangeLayer()
        {
            switch (NexLayer)
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

        void ChangeAnimatorLayer(int layer)
        {
            if (NexLayer < 3)
            {
                animator.SetTrigger("Morph");
                NexLayer = layer;
            }
        }

        private void Flip()
        {
            m_FacingRight = !m_FacingRight;
            
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
