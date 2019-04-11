using UnityEngine;

namespace Graphic
{
    public class FlowersAnimations : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Animator>().Play("Fleur", -1, Random.value);
        }
    }
}
