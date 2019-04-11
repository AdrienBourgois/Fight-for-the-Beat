using UnityEngine;

namespace Graphic
{
    public class CameraFollow : MonoBehaviour
    {
        #pragma warning disable CS0649
        [SerializeField]
        private GameObject target;
        #pragma warning restore CS0649

        private float delta;

        private void Start()
        {
            delta = transform.position.x;
        }

        private void Update()
        {
            Transform object_transform = transform;
            Vector3 position = object_transform.position;
            position.x = target.transform.position.x + delta;
            object_transform.position = position;
        }
    }
}
