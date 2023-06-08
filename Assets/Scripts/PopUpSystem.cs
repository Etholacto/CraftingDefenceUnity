using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpSystem : MonoBehaviour
{
    [SerializeField] private GameObject popUpBox;
    [SerializeField] private TMP_Text popUpText;

    public void PopUp(string text)
    {
        popUpBox.SetActive(true);
        popUpText.text = text;
    }

    public void PopDown()
    {
        popUpBox.SetActive(false);
    }

    public bool IsPopUp()
    {
        return popUpBox.activeSelf;
    }
}
