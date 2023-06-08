using UnityEngine;

public class IndividualEnemyController : MonoBehaviour
{
	public float moveDistance = 2f;

	private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Castle" || collision.gameObject.name == "Bean enemy(Clone)") {

			Vector3 moveDirection = transform.position - collision.transform.position;
			moveDirection.Normalize();

			// Move the enemy back by the specified distance
			transform.position += moveDirection * moveDistance;

		}
    }
}