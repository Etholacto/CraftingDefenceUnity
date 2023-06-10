using UnityEngine;

public class IndividualEnemyController : MonoBehaviour
{
	public float moveDistance = 2f;

    private float Health = 25f;

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
