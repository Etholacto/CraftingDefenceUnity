using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    GameObject castle;

    [SerializeField]
    GameObject Healthbar;

    [SerializeField]
    float enemiesNumber;

    [SerializeField]
    float moveSpeed;

    List<GameObject> enemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < enemiesNumber; i++)
        {
            float x = Random.Range(-20f, 20f);
            float z = Random.Range(-20f, 20f);
            Vector3 position = new Vector3(x, 1f, z);
            GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            enemies.Add(enemy);

            GameObject healthBar = Instantiate(Healthbar, enemy.transform);
            // Add colliders to enemy prefabs
            enemy.AddComponent<CapsuleCollider>();
            enemy.GetComponent<CapsuleCollider>().radius = 0.65f;
            enemy.GetComponent<CapsuleCollider>().isTrigger = false;
            enemy.AddComponent<Rigidbody>();
            enemy.GetComponent<Rigidbody>().mass = 1f;
            enemy.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            enemy.GetComponent<Rigidbody>().useGravity = true;
            enemy.GetComponent<Rigidbody>().isKinematic = false;
            enemy.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
            enemy.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;

            // Attach EnemyCollisionHandler script to each enemy
            enemy.AddComponent<IndividualEnemyController>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = castle.transform.position;

        foreach (GameObject enemy in enemies)
        {
            Vector3 newPosition = Vector3.MoveTowards(enemy.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            enemy.transform.position = newPosition;
        }
    }

}