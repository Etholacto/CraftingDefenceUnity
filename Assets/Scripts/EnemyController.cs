using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    GameObject mainCharacter;

    [SerializeField]
    GameObject Healthbar;

    [SerializeField]
    float moveSpeed = 5f;

    List<GameObject> enemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            float x = Random.Range(-20f, 20f);
            float z = Random.Range(-20f, 20f);
            Vector3 position = new Vector3(x, 0, z);
            GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            enemies.Add(enemy);

            GameObject healthBar = Instantiate(Healthbar, enemy.transform);
            // Add colliders to enemy prefabs
            enemy.AddComponent<CapsuleCollider>();
            enemy.GetComponent<CapsuleCollider>().radius = 0.5f;
            enemy.GetComponent<CapsuleCollider>().isTrigger = false;
            enemy.AddComponent<Rigidbody>();
            enemy.GetComponent<Rigidbody>().mass = 1f;
            enemy.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            enemy.GetComponent<Rigidbody>().useGravity = true;
            enemy.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = mainCharacter.transform.position;

        foreach (GameObject enemy in enemies)
        {
            Vector3 newPosition = Vector3.MoveTowards(enemy.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            enemy.transform.position = newPosition;
        }
    }

    // Handle collisions between enemy objects
    private void OnCollisionEnter(Collision collision)
    {
        if (enemies.Contains(collision.gameObject))
        {
            Debug.Log("It's a enemy");
        }
        else
        {
            Debug.Log("It's a player");
        }
    }
}