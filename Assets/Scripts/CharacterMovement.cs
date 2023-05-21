using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    public float Speed = 5.0f;

    public float drag;

    Rigidbody rb;

    float horizontalInput;
    float verticalInput;

    private Vector3 _moveDir = Vector3.zero;

    float WoodAmount = 0;
    float StoneAmount = 0;

    public TMPro.TMP_Text WoodAmountText;
    public TMPro.TMP_Text StoneAmountText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get Input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        SpeedControl();

        ChangeResourceText();

    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        //calculate movement direction
        _moveDir = transform.forward * verticalInput + transform.right * horizontalInput;

        rb.AddForce(_moveDir.normalized * Speed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity if over Speed
        if (flatVel.magnitude > Speed)
        {
            Vector3 limitedVel = flatVel.normalized * Speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void ChangeResourceText()
    {
        WoodAmountText.text = string.Format("{0}", WoodAmount);
        StoneAmountText.text = string.Format("{0}", StoneAmount);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "WoodObj")
        {
            WoodAmount += 1;
        }
    }
}
