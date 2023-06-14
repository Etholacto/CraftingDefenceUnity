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

    [SerializeField] private GameOver GameOver;

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision occurred with: " + collision.gameObject.name);
        if (collision.gameObject.name == "Black Widow(Clone)" && Castlehealth > 0)
        {
            Castlehealth = Castlehealth - 0.1f;
            castleImage.fillAmount = castleImage.fillAmount - 0.001f;
            HealthBarText.text = Castlehealth.ToString("0")+"%";
            
        }
        else if( Castlehealth > 0) { }
        else
        {
            if (GameOver.isPanelActive())
            {
                GameOver.Continue();
            }
            else
            {
                GameOver.Pause();
            }
        }
    }
}
