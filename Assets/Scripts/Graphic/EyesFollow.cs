using Entities;
using UnityEngine;

namespace Graphic
{
    public class EyesFollow : MonoBehaviour
    {
        [SerializeField]
        private float radius = 1f;

        private Vector3 initialPosition;
        private Transform playerTransform;

        private void Start()
        {
            playerTransform = FindObjectOfType<Player>().transform;
            initialPosition = transform.localPosition;
        }

        private void Update()
        {
            Transform eye_transform = transform;
            Vector3 direction = playerTransform.position - eye_transform.position;
            eye_transform.localPosition = initialPosition + direction * radius;
        }
    }
}
