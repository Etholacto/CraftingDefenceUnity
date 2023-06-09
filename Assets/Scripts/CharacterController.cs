using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    [Header("Player 1")]
    [SerializeField] private bool player1;

    [Header("Character Movement")]
    public float Speed = 5.0f;

    Rigidbody rb;

    float horizontalInput;
    float verticalInput;

    private Vector3 _moveDir = Vector3.zero;
    private bool canMove;

    public float CollectDelay = 1f;
    float timer;

    [Header("Resources")]
    [SerializeField] private CharacterDB db;

    [SerializeField] private TMPro.TMP_Text WoodAmountText;
    [SerializeField] private TMPro.TMP_Text StoneAmountText;

    [Header("UI")]
    [SerializeField] private PopUpSystem pop;
    [SerializeField] private PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        db.resetValues();
    }

    // Update is called once per frame
    void Update()
    {

        if (player1)
        {
            //Get Input
            horizontalInput = Input.GetAxis("Horizontal.P1");
            verticalInput = Input.GetAxis("Vertical.P1");
        }
        else
        {
            horizontalInput = Input.GetAxis("Horizontal.P2");
            verticalInput = Input.GetAxis("Vertical.P2");
        }

        SpeedControl();

        ChangeResourceText();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu != null)
            {
                if (pauseMenu.isPanelActive())
                {
                    pauseMenu.Continue();
                }
                else
                {
                    pauseMenu.Pause();
                }
            }
        }
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
        if (WoodAmountText != null || StoneAmountText != null)
        {
            WoodAmountText.text = string.Format("{0}", db.GetResource("wood"));
            StoneAmountText.text = string.Format("{0}", db.GetResource("stone"));
        }
    }

    private void OnTriggerStay(Collider col)
    {
        timer += Time.deltaTime;
        if (col.gameObject.name == "WoodObj")
        {
            if ((Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.Minus)) && timer > CollectDelay)
            {
                db.SetResource("wood", 1f);
                timer -= CollectDelay;
            }
            pop.PopUp("Press F to collect Wood");
        }
        else if (col.gameObject.name == "StoneObj")
        {
            if ((Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.Minus)) && timer > CollectDelay)
            {
                db.SetResource("stone", 1f);
                timer -= CollectDelay;
            }
            pop.PopUp("Press F to collect Stone");
        }
    }

    private void OnTriggerExit(Collider col)
    {
        pop.PopDown();
    }
}
