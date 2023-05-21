using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuy : MonoBehaviour
{
    public GameObject TowerSelect;

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
}
