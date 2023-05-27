using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public float Speed = 5.0f;

    Rigidbody rb;

    float horizontalInput;
    float verticalInput;

    private Vector3 _moveDir = Vector3.zero;
    private bool canMove;

    public float CollectDelay = 1f;
    float timer;

    float WoodAmount = 0;
    float StoneAmount = 0;

    public TMPro.TMP_Text WoodAmountText;
    public TMPro.TMP_Text StoneAmountText;

    private PopUpSystem pop;
    private TowerBuy buy;
    public GameObject GO_Tower1, GO_Tower2, GO_Tower3;
    public GameObject currentObj;
    [SerializeField] private Transform TowerTarget;
    [SerializeField] private LayerMask TowerMask;
    [SerializeField] private Transform PlayerCameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pop = GameObject.FindGameObjectWithTag("GameUI").GetComponent<PopUpSystem>();
        pop.PopDown();

        buy = GameObject.FindGameObjectWithTag("GameUI").GetComponent<TowerBuy>();
        buy.CloseBuy();
    }

    // Update is called once per frame
    void Update()
    {
        //Get Input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        SpeedControl();

        ChangeResourceText();


        if (Input.GetKeyDown(KeyCode.F))
        {
            if (buy.isActive() == false)
            {
                buy.OpenBuy();
            }
            else
            {
                buy.CloseBuy();
            }
        }

        float RayDistance = 10f;
        if (Physics.Raycast(PlayerCameraTransform.position, PlayerCameraTransform.forward, out RaycastHit HitInfo, RayDistance, TowerMask))
        {
            if (HitInfo.transform.TryGetComponent(out TowerBP towerBP))
            {
                currentObj = HitInfo.collider.gameObject;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Destroy(currentObj);
                }
                towerBP.MoveObject(TowerTarget);
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (currentObj.name.Contains("1"))
            {
                Instantiate(GO_Tower1, TowerTarget.position, TowerTarget.rotation);
            }
            else if (currentObj.name.Contains("2"))
            {
                Instantiate(GO_Tower2, TowerTarget.position, TowerTarget.rotation);
            }
            else
            {
                Instantiate(GO_Tower3, TowerTarget.position, TowerTarget.rotation);
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
        WoodAmountText.text = string.Format("{0}", WoodAmount);
        StoneAmountText.text = string.Format("{0}", StoneAmount);
    }

    private void OnTriggerStay(Collider col)
    {
        timer += Time.deltaTime;
        if (col.gameObject.name == "WoodObj")
        {
            if (Input.GetKey(KeyCode.E) && timer > CollectDelay)
            {
                WoodAmount += 1;
                timer -= CollectDelay;
            }
            pop.PopUp("Press E to collect Wood");
        }
        else if (col.gameObject.name == "StoneObj")
        {
            if (Input.GetKey(KeyCode.E) && timer > CollectDelay)
            {
                StoneAmount += 1;
                timer -= CollectDelay;
            }
            pop.PopUp("Press E to collect Stone");
        }
    }

    private void OnTriggerExit(Collider col)
    {
        pop.PopDown();
    }
}
