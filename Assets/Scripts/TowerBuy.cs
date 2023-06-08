using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuy : MonoBehaviour
{
    [Header("TowerPanel")]
    [SerializeField] private GameObject TowerSelect;
    [Header("Towers")]
    [SerializeField] private GameObject Tower1;
    [SerializeField] private GameObject Tower2;
    [SerializeField] private GameObject Tower3;
    [Header("Tower Blueprints")]
    [SerializeField] private GameObject Tower1_bp;
    [SerializeField] private GameObject Tower2_bp;
    [SerializeField] private GameObject Tower3_bp;
    [Header("Buttons")]
    [SerializeField] private Button b1;
    [SerializeField] private Button b2;
    [SerializeField] private Button b3;
    private GameObject currentObj;
    [Header("Raycast")]
    [SerializeField] private Transform TowerTarget;
    [SerializeField] private LayerMask TowerMask;
    [SerializeField] private Transform PlayerCameraTransform;
    [Header("other")]
    [SerializeField] private PopUpSystem pop;
    [SerializeField] private CharacterDB db;

    private void spawn_Tower1_bp()
    {
        Destroy(currentObj);
        Instantiate(Tower1_bp, TowerTarget.position, TowerTarget.rotation);
        pop.PopUp("1 wood, 1 stone");
    }

    private void spawn_Tower2_bp()
    {
        Destroy(currentObj);
        Instantiate(Tower2_bp, TowerTarget.position, TowerTarget.rotation);
        pop.PopUp("2 wood, 2 stone");
    }

    private void spawn_Tower3_bp()
    {
        Destroy(currentObj);
        Instantiate(Tower3_bp, TowerTarget.position, TowerTarget.rotation);
        pop.PopUp("3 wood, 3 stone");
    }

    private void spawnTower(GameObject currentObj)
    {
        if (currentObj.name.Contains("1"))
        {
            if (db.GetResource("wood") > 0 || db.GetResource("stone") > 0)
            {
                Instantiate(Tower1, TowerTarget.position, TowerTarget.rotation);
                db.SetResource("wood", -1f);
                db.SetResource("stone", -1f);
            }
            else
            {
                pop.PopUp("Not enough Resources");
            }
        }
        else if (currentObj.name.Contains("2"))
        {
            if (db.GetResource("wood") > 1 || db.GetResource("stone") > 1)

            {
                Instantiate(Tower2, TowerTarget.position, TowerTarget.rotation);
                db.SetResource("wood", -2f);
                db.SetResource("stone", -2f);
            }
            else
            {
                pop.PopUp("Not enough Resources");
            }
        }
        else
        {
            if (db.GetResource("wood") > 2 || db.GetResource("stone") > 2)
            {
                Instantiate(Tower3, TowerTarget.position, TowerTarget.rotation);
                db.SetResource("wood", -3f);
                db.SetResource("stone", -3f);
            }
            else
            {
                pop.PopUp("Not enough Resources");
            }
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
            pop.PopDown();
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
