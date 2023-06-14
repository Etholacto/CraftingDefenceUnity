using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuyP2 : MonoBehaviour
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
    private GameObject[] towersBP = new GameObject[3];
    private int index = 0;

    private void Awake()
    {
        towersBP[0] = Tower1_bp;
        towersBP[1] = Tower2_bp;
        towersBP[2] = Tower3_bp;
    }

    private void spawnTower(GameObject currentObj)
    {
        if (currentObj.name.Contains("1"))
        {
            if (db.GetResource("wood") > 1 && db.GetResource("stone") > 1)
            {
                Instantiate(Tower1, TowerTarget.position, TowerTarget.rotation);
                db.SetResource("wood", -2f);
                db.SetResource("stone", -2f);
            }
        }
        else if (currentObj.name.Contains("2"))
        {
            if (db.GetResource("wood") > 3 && db.GetResource("stone") > 3)

            {
                Instantiate(Tower2, TowerTarget.position, TowerTarget.rotation);
                db.SetResource("wood", -4f);
                db.SetResource("stone", -4f);
            }
        }
        else if (currentObj.name.Contains("3"))
        {
            if (db.GetResource("wood") > 5 && db.GetResource("stone") > 5)
            {
                Instantiate(Tower3, TowerTarget.position, TowerTarget.rotation);
                db.SetResource("wood", -6f);
                db.SetResource("stone", -6f);
            }
        }

        if (pop != null)
        {
            pop.PopUpTimed("Not enough Resources", 1.5f);
        }
    }

    private void FixedUpdate()
    {
        float RayDistance = 12f;
        if (Physics.Raycast(PlayerCameraTransform.position, PlayerCameraTransform.forward, out RaycastHit HitInfo, RayDistance, TowerMask))
        {
            if (HitInfo.transform.TryGetComponent(out TowerBP towerBP))
            {
                currentObj = HitInfo.collider.gameObject;
                towerBP.MoveObject(TowerTarget);
            }
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Place.P2") || Input.GetButtonDown("Reset.P2"))
        {
            if (pop != null)
            {
                if (pop.IsPopUp())
                {
                    pop.PopDown();
                }
            }
            if (Input.GetButtonDown("Place.P2"))
            {
                Destroy(currentObj);
                spawnTower(currentObj);
            }
            if (b1 != null || b2 != null || b3 != null)
            {
                b1.GetComponent<Image>().color = Color.white;
                b2.GetComponent<Image>().color = Color.white;
                b3.GetComponent<Image>().color = Color.white;
            }
        }

        ChooseTowerBp();
    }

    private void ChooseTowerBp()
    {
        if (Input.GetButtonDown("Next.P2"))
        {
            SpawnBP(towersBP[index]);
            index++;
        }
        else if (Input.GetButtonDown("Prev.P2"))
        {
            SpawnBP(towersBP[index]);
            index--;
        }
        if (index == towersBP.Length)
        {
            index = 0;
        }
        else if (index < 0)
        {
            index = towersBP.Length - 1;
        }
    }

    private void SpawnBP(GameObject towerBP)
    {
        Destroy(currentObj);
        if (towerBP.name.Contains("1"))
        {
            if (b1 != null || b2 != null || b3 != null)
            {
                b1.GetComponent<Image>().color = Color.grey;
                b2.GetComponent<Image>().color = Color.white;
                b3.GetComponent<Image>().color = Color.white;
            }
            Instantiate(Tower1_bp, TowerTarget.position, TowerTarget.rotation);
            ResourcePop(1, 1);
        }
        else if (towerBP.name.Contains("2"))
        {
            if (b1 != null || b2 != null || b3 != null)
            {
                b1.GetComponent<Image>().color = Color.white;
                b2.GetComponent<Image>().color = Color.grey;
                b3.GetComponent<Image>().color = Color.white;
            }
            Instantiate(Tower2_bp, TowerTarget.position, TowerTarget.rotation);
            ResourcePop(2, 2);
        }
        else
        {
            if (b1 != null || b2 != null || b3 != null)
            {
                b1.GetComponent<Image>().color = Color.white;
                b2.GetComponent<Image>().color = Color.white;
                b3.GetComponent<Image>().color = Color.grey;
            }
            Instantiate(Tower3_bp, TowerTarget.position, TowerTarget.rotation);
            ResourcePop(3, 3);
        }
    }

    private void ResourcePop(float woodcost, float stonecost)
    {
        if (pop != null)
        {
            string text = $"{woodcost} wood, {stonecost} stone";
            pop.PopUp(text);
        }
    }
}
