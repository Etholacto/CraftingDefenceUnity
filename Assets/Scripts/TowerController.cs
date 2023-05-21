using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerController : MonoBehaviour
{
    [SerializeField] private LayerMask TowerMask;
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private Transform TowerTarget;
    [Space]
    [SerializeField] private float TowerRange;
    private Rigidbody CurrentObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (CurrentObject)
            {
                CurrentObject.useGravity = true;
                CurrentObject = null;
                return;
            }

            Ray CameraRay = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(CameraRay, out RaycastHit HitInfo, TowerRange, TowerMask))
            {
                CurrentObject = HitInfo.rigidbody;
                CurrentObject.useGravity = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (CurrentObject)
        {
            Vector3 DirectionToPoint = TowerTarget.position + CurrentObject.position;
            float DistanceToPoint = DirectionToPoint.magnitude;

            CurrentObject.velocity = DirectionToPoint * 10f * DistanceToPoint;
        }
    }
}
