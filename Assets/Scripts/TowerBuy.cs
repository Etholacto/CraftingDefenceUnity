using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuy : MonoBehaviour
{
    [SerializeField] private GameObject TowerSelect, Tower1, Tower1_bp, Tower2, Tower2_bp, Tower3, Tower3_bp;
    [SerializeField] private Button b1, b2, b3;
    private GameObject currentObj;
    [SerializeField] private Transform TowerTarget;
    [SerializeField] private LayerMask TowerMask;
    [SerializeField] private Transform PlayerCameraTransform;

    private void spawn_Tower1_bp()
    {
        Destroy(currentObj);
        Instantiate(Tower1_bp, TowerTarget.position, TowerTarget.rotation);
    }

    private void spawn_Tower2_bp()
    {
        Destroy(currentObj);
        Instantiate(Tower2_bp, TowerTarget.position, TowerTarget.rotation);
    }

    private void spawn_Tower3_bp()
    {
        Destroy(currentObj);
        Instantiate(Tower3_bp, TowerTarget.position, TowerTarget.rotation);
    }

    private void spawnTower(GameObject currentObj)
    {
        if (currentObj.name.Contains("1"))
        {
            Instantiate(Tower1, TowerTarget.position, TowerTarget.rotation);
        }
        else if (currentObj.name.Contains("2"))
        {
            Instantiate(Tower2, TowerTarget.position, TowerTarget.rotation);
        }
        else
        {
            Instantiate(Tower3, TowerTarget.position, TowerTarget.rotation);
        }
    }

    private void Update()
    {
        float RayDistance = 10f;
        if (Physics.Raycast(PlayerCameraTransform.position, PlayerCameraTransform.forward, out RaycastHit HitInfo, RayDistance, TowerMask))
        {
            if (HitInfo.transform.TryGetComponent(out TowerBP towerBP))
            {
                currentObj = HitInfo.collider.gameObject;
                towerBP.MoveObject(TowerTarget);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Tab))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                spawnTower(currentObj);
            }
            b1.GetComponent<Image>().color = Color.white;
            b2.GetComponent<Image>().color = Color.white;
            b3.GetComponent<Image>().color = Color.white;
            Destroy(currentObj);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            b1.GetComponent<Image>().color = Color.grey;
            b2.GetComponent<Image>().color = Color.white;
            b3.GetComponent<Image>().color = Color.white;
            spawn_Tower1_bp();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            b1.GetComponent<Image>().color = Color.white;
            b2.GetComponent<Image>().color = Color.grey;
            b3.GetComponent<Image>().color = Color.white;
            spawn_Tower2_bp();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            b1.GetComponent<Image>().color = Color.white;
            b2.GetComponent<Image>().color = Color.white;
            b3.GetComponent<Image>().color = Color.grey;
            spawn_Tower3_bp();
        }

    }
}
