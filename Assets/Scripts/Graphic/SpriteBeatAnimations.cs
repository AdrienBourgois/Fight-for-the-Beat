using System.Collections.Generic;
using Audio;
using UnityEngine;

namespace Graphic
{
    public class SpriteBeatAnimations : MonoBehaviour
    {
        [SerializeField]
        private List<Sprite> sprites = new List<Sprite>();

        private SpriteRenderer spriteRenderer;

        private int index;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            AudioManager.Instance.OnBeat += OnBeat;
        }

        private void OnDestroy()
        {
            AudioManager.Instance.OnBeat -= OnBeat;
        }

        private void OnBeat(int _beat)
        {
            ++index;
            if (index >= sprites.Count)
                index = 0;

            spriteRenderer.sprite = sprites[index];
        }
    }
}
