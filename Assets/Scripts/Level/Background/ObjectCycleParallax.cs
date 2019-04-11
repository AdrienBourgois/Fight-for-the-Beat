using UnityEngine;

namespace Level.Background
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(ObjectCycle))]
    public class ObjectCycleParallax : MonoBehaviour
    {
        public Transform target;
        public Vector2 factor;

        private ObjectCycle spriteCicle;

        private void Awake()
        {
            spriteCicle = GetComponent<ObjectCycle>();
        }

        private void Start()
        {
            if (!target)
                if (Camera.main != null)
                    target = Camera.main.transform;
        }

        private void Update()
        {
            if (target && spriteCicle)
            {
                Transform object_transform = transform;
                Vector3 target_position = target.position;

                spriteCicle.Position = target_position.x * factor.x;

                Vector3 local_position = object_transform.localPosition;
                local_position.y = target_position.y * factor.y;
                object_transform.localPosition = local_position;
            }
        }
    }
}