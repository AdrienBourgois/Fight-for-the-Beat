using System.Collections.Generic;
using UnityEngine;

namespace Level.Background
{
    [ExecuteInEditMode]
    public class ObjectCycle : MonoBehaviour
    {
        public List<GameObject> objects = new List<GameObject>();

        [Range(0, 1)]
        public float offset;

        [SerializeField]
        private float width = 2f;

        private float totalWidth = 1f;
        private float mPosition;

        public float Position
        {
            get => mPosition;
            set
            {
                float scale_x = transform.localScale.x;

                mPosition = value;

                if (scale_x > 0f)
                    mPosition /= scale_x;

                Vector3 l_position = Vector3.zero;

                totalWidth = 0f;

                foreach (GameObject sr in objects)
                {
                    if (sr)
                    {
                        sr.transform.localPosition = l_position;
                        l_position.x += width;
                        totalWidth += width;
                    }
                }

                float dx = mPosition % totalWidth;

                foreach (GameObject sr in objects)
                {
                    if (sr)
                    {
                        Vector3 local_pos = sr.transform.localPosition + Vector3.right * dx;

                        if (local_pos.x <= -width)
                            local_pos.x += totalWidth;
                        else if (local_pos.x > totalWidth)
                            local_pos.x -= totalWidth;

                        local_pos.x -= offset * totalWidth;

                        sr.transform.localPosition = local_pos;
                    }
                }
            }
        }

        private void Awake()
        {
            Position = 0f;
        }

        private void OnValidate()
        {
            Position = 0f;
        }
    }
}