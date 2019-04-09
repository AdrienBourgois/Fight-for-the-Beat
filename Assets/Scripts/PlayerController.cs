using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController2D Controller;
    public Animator Animator;

    private void Update()
    {

        { // Can execute
            if (Input.GetButtonDown("Left"))
            {
                Controller.MoveTo(new Vector3(-1, 0));
            }
            else if (Input.GetButtonDown("Right"))
            {
                Controller.MoveTo(new Vector3(1, 0));
            }
            else if (Input.GetButtonDown("Up"))
            {
                Controller.RoundTripTo(new Vector3(0, 2));
            }
        }
    }

}
