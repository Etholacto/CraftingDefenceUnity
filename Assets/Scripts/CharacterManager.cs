using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private CharacterDB characterdb;
    [SerializeField] private Transform SpawnPoint;

    [SerializeField] private Transform PlayerCameraTransform;
    private float RayDistance = 5f;
    [SerializeField] private LayerMask Mask;

    private GameObject currentObj;

    private int selectedOption = 0;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(characterdb.GetCharacter(selectedOption).Character, SpawnPoint.transform.position, Quaternion.identity);
    }

    private GameObject GetWhatsInfront()
    {
        if (Physics.Raycast(PlayerCameraTransform.position, PlayerCameraTransform.forward, out RaycastHit HitInfo, RayDistance, Mask))
        {
            currentObj = HitInfo.collider.gameObject;
        }
        return currentObj;
    }

    public void NextOption()
    {
        GameObject temp = GetWhatsInfront();
        if (temp != null)
        {
            Destroy(temp);
        }

        selectedOption++;

        if (selectedOption >= characterdb.CharacterCount)
        {
            selectedOption = 0;
        }

        Instantiate(characterdb.GetCharacter(selectedOption).Character, SpawnPoint.transform.position, Quaternion.identity);
    }

    public void PrevOption()
    {
        GameObject temp = GetWhatsInfront();
        if (temp != null)
        {
            Destroy(temp);
        }

        selectedOption--;

        if (selectedOption < 0)
        {
            selectedOption = characterdb.CharacterCount - 1;
        }

        Instantiate(characterdb.GetCharacter(selectedOption).Character, SpawnPoint.transform.position, Quaternion.identity);
    }

    public void BackButton()
    {

    }

    public void PlayButton()
    {

    }
}
