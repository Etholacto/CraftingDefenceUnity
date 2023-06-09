using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    [SerializeField] private bool player1;
    [Header("PlayerLocation")]
    [SerializeField] private Transform PlayerTransform1;
    [SerializeField] private Transform PlayerTransform2;

    private Vector3 velocity = Vector3.zero;
    private float SmoothFactor = 0.1f;

    private void Start()
    {
        if (player1)
        {
            transform.position = PlayerTransform1.position;
        }
        else
        {
            transform.position = midpoint();
        }
    }

    private void FixedUpdate()
    {
        Vector3 newPos = new Vector3(0, 0, 0);
        if (player1)
        {
            newPos = PlayerTransform1.position;
        }
        else
        {
            newPos = midpoint();
        }

        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, SmoothFactor);
    }


    private Vector3 midpoint()
    {
        return new Vector3((PlayerTransform1.position.x + PlayerTransform2.position.x) / 2, (PlayerTransform1.position.y + PlayerTransform2.position.y) / 2, (PlayerTransform1.position.z + PlayerTransform2.position.z) / 2);
    }

    public void SetBool(bool IsSingle)
    {
        player1 = IsSingle;
    }

}
