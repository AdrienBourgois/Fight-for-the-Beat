using UnityEngine;
using System.Collections.Generic;
using Audio;
using UnityEngine.UI;

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
        private int CurrentLayer = 0;

        public Image HealBar;
        public List<Sprite> SpritesHealBar;

        protected override void Start()
        {
            base.Start();
            animator = GetComponent<Animator>();

            GameInputs.InputManager.Instance.OnKeyPressed += Controll;

            GameInputs.InputManager.Instance.OnNoKeyPressed += OnNoKeyPressed;
            GameManager.Instance.OnComboIncreased += ChangeAnimatorLayer;
            GameManager.Instance.OnCombotReseted += OnCombotReseted;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            GameInputs.InputManager.Instance.OnKeyPressed -= Controll;

            GameInputs.InputManager.Instance.OnNoKeyPressed -= OnNoKeyPressed;
            GameManager.Instance.OnComboIncreased -= ChangeAnimatorLayer;
            GameManager.Instance.OnCombotReseted -= OnCombotReseted;
        }

        private void OnNoKeyPressed()
        {
            LaunchSequence(Idle);
            ExecuteSequence();
            GameManager.Instance.CombotReseted();
        }

        private void OnCombotReseted()
        {
            ChangeAnimatorLayer(0);
        }

        void Controll(GameInputs.InputManager.Keys key)
        {
            switch (key)
            {
                case (GameInputs.InputManager.Keys.Right):
                    if (!m_FacingRight)
                        Flip();
                    if (GetNextSpaceEntity())
                        LaunchSequence(Attack);
                    else
                        LaunchSequence(MoveRight);
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
            AudioManager.Instance.PlayHitEvent();
            HealBar.sprite = SpritesHealBar[life];
        }

        override protected void OnDie()
        {
            gameObject.SetActive(false);

            HealBar.sprite = SpritesHealBar[life];

            GameManager.Instance.GameOver();
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
            CurrentLayer = NexLayer;
        }

        void ChangeAnimatorLayer(int layer)
        {
            if (layer < 3 && NexLayer != layer)
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
