using UnityEngine;

namespace Audio
{
    public class EnemiesSounds : MonoBehaviour
    {
        public void PlayFlowerAttackEvent() => AudioManager.Instance.PlayFlowerAttackEvent();
        public void PlayFlowerFootstepEvent() => AudioManager.Instance.PlayFlowerFootstepEvent();
        public void PlayFlowerPrepaEvent() => AudioManager.Instance.PlayFlowerPrepaEvent();
        public void PlayFouetAttack() => AudioManager.Instance.PlayFouetAttack();
        public void PlayFouetJump() => AudioManager.Instance.PlayFouetJump();
        public void PlayHatAttackEvent() => AudioManager.Instance.PlayHatAttackEvent();
        public void PlayHatPrepaEvent() => AudioManager.Instance.PlayHatPrepaEvent();
        public void PlayPuppetAttackEvent() => AudioManager.Instance.PlayPuppetAttackEvent();
        public void PlayPuppetPrepaEvent() => AudioManager.Instance.PlayPuppetPrepaEvent();
    }
}
