using Mono.Cecil;
using UnityEngine;
using UnityEngine.UI;

public class IndividualEnemyController : MonoBehaviour
{
	public float moveDistance = 2f;

    private float Health = 50f;

    private Image foregroundImage;

    void Start()
    {
        GameObject healthBar = GameObject.Find("HealthBar");
        GameObject instantiatedHealthBar = Instantiate(healthBar, transform);
        Transform foreground = instantiatedHealthBar.transform.Find("Background/Foreground");

        foregroundImage = foreground.GetComponent<Image>();
    }

	private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Castle" ) {

			Vector3 moveDirection = transform.position - collision.transform.position;
			moveDirection.Normalize();

			// Move the enemy back by the specified distance
			transform.position += moveDirection * moveDistance;

		}

        if (collision.gameObject.CompareTag("Projectile"))
        {
            Health -= 10;
            foregroundImage.fillAmount -= 0.2f;
        }
    }

    void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
