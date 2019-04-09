using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    bool EndMove;
    float Speed = 12;

    private void Start()
    {
        EndMove = true;
    }

    public void MoveTo(Vector2 position)
    {
        if (EndMove)
        {
            EndMove = false;
            StartCoroutine(Move(transform.position, new Vector3(position.x, position.y, 0) + transform.position));
        }
    }

    IEnumerator Move(Vector3 startposition, Vector3 endPosition)
    {
        for (float f = 0f; f <= 1; f += Speed * Time.deltaTime)
        {
            transform.position = Vector3.Lerp(startposition, endPosition, f);

            yield return null;
        }

        transform.position = endPosition;
        EndMove = true;
    }

    public void RoundTripTo(Vector2 position)
    {
        if (EndMove)
        {
            EndMove = false;
            StartCoroutine(RoundTrip(transform.position, new Vector3(position.x, position.y, 0) + transform.position));
        }
    }

    IEnumerator RoundTrip(Vector3 startposition, Vector3 maxPosition)
    {
        for (float f = 0f; f <= 1; f += Speed * Time.deltaTime)
        {
            transform.position = Vector3.Lerp(startposition, maxPosition, f);

            yield return null;
        }
        for (float f = 0f; f <= 1; f += Speed * Time.deltaTime)
        {
            transform.position = Vector3.Lerp(maxPosition, startposition, f);

            yield return null;
        }
        transform.position = startposition;
        EndMove = true;
    }
}
