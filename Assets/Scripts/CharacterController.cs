using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    [Header("Player 1")]
    [SerializeField] private bool player1;

    [Header("Character Movement")]
    [SerializeField] private float Speed = 10.0f;
    [SerializeField] private float RotSpeed = 240.0f;
    [SerializeField] private float Gravity = 20.0f;

    private Rigidbody rb;

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

    private Camera MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        db.resetValues();
        rb = this.GetComponent<Rigidbody>();
        if (PlayerPrefs.GetString("IsCoop").Contains("yes"))
        {
            GameObject MainCamera = this.transform.GetChild(3).gameObject;
            Camera Cam = MainCamera.GetComponent<Camera>();
            if (player1)
            {
                Cam.rect = new Rect(0f, 0f, 0.5f, 1f);
            }
            else
            {
                Cam.rect = new Rect(0.5f, 0f, 0.5f, 1f);
            }
        }
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
        Quaternion Rotation = Quaternion.Euler(new Vector3(0, horizontalInput * RotSpeed, 0) * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation.normalized * Rotation);

        _moveDir = Vector3.forward * verticalInput;
        _moveDir = transform.TransformDirection(_moveDir) * Speed;
        _moveDir.y -= Gravity * Time.deltaTime;

        rb.AddForce(_moveDir.normalized * 10 * Speed, ForceMode.Force);
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
