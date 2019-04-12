using Audio;
using UnityEngine;

namespace GameInputs
{
    public class InputManager : MonoBehaviour
    {
        public enum Keys
        {
            Up,
            Left,
            Down,
            Right
        }

        public delegate void KeyPressedEvent(Keys _key);
        public delegate void WrongInputEvent();

        public static InputManager Instance { get; private set; }

        public event KeyPressedEvent OnKeyPressed;
        public event WrongInputEvent OnNoKeyPressed;
        public event WrongInputEvent OnMultipleKeyPressed;

        private bool canInteract;
        private bool hadInteract;

        private bool hadBeginPlayed;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            AudioManager.Instance.OnOffbeat += OnOffbeat;
            GameManager.Instance.OnMenu += () => hadBeginPlayed = false;
        }

        private void Update()
        {
            if (!GameManager.Instance.OnGame)
                return;

            if (Input.GetButtonDown("Left"))
                ProcessInput(Keys.Left);
            if (Input.GetButtonDown("Right"))
                ProcessInput(Keys.Right);
            if (Input.GetButtonDown("Up"))
                ProcessInput(Keys.Up);
            if (Input.GetButtonDown("Down"))
                ProcessInput(Keys.Down);

            if (Input.GetKeyDown(KeyCode.Escape))
                GameManager.Instance.Pause(true);
        }

        private void OnOffbeat(int _beat)
        {
            if(!hadInteract && hadBeginPlayed)
                OnNoKeyPressed?.Invoke();

            canInteract = true;
            hadInteract = false;
        }

        private void ProcessInput(Keys _key)
        {
            hadBeginPlayed = true;

            if (hadInteract)
            {
                OnMultipleKeyPressed?.Invoke();
            }
            else if (canInteract)
            {
                canInteract = false;
                hadInteract = true;
                OnKeyPressed?.Invoke(_key);
            }
        }
    }
}
