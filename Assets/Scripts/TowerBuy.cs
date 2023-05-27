using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuy : MonoBehaviour
{
    public GameObject TowerSelect, Tower1_bp, Tower2_bp, Tower3_bp;
    [SerializeField] private Transform TowerTarget;

    public void OpenBuy()
    {
        TowerSelect.SetActive(true);
    }

    public void CloseBuy()
    {
        TowerSelect.SetActive(false);
    }

    public bool isActive()
    {
        return TowerSelect.activeSelf;
    }

    public void spawn_Tower1_bp()
    {
        TowerSelect.SetActive(false);
        Instantiate(Tower1_bp, TowerTarget.position, TowerTarget.rotation);
    }

    public void spawn_Tower2_bp()
    {
        TowerSelect.SetActive(false);
        Instantiate(Tower2_bp, TowerTarget.position, TowerTarget.rotation);
    }

    public void spawn_Tower3_bp()
    {
        TowerSelect.SetActive(false);
        Instantiate(Tower3_bp, TowerTarget.position, TowerTarget.rotation);
    }
}
