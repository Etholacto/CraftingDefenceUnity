using UnityEngine;
using UnityEngine.UI;

public class CastleController : MonoBehaviour
{
    [SerializeField] 
    private TMPro.TMP_Text HealthBarText;

    [SerializeField]
    private Image castleImage;

    [SerializeField]
    float Castlehealth;

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision occurred with: " + collision.gameObject.name);
        if (collision.gameObject.name == "Bean enemy(Clone)" && Castlehealth > 0)
        {
            Castlehealth = Castlehealth - 0.1f;
            castleImage.fillAmount = castleImage.fillAmount - 0.001f;
            HealthBarText.text = Castlehealth.ToString("0")+"%";
            
        }
    }
}
