using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpSystem : MonoBehaviour
{
    [SerializeField] private GameObject popUpBox;
    [SerializeField] private TMP_Text popUpText;

    public void PopUp(string text)
    {
        if (text != null && popUpBox != null)
        {
            popUpBox.SetActive(true);
            popUpText.text = text;
        }
    }

    public void PopUpTimed(string text, float timer)
    {
        PopUp(text);
        Invoke("PopDown", timer);
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
