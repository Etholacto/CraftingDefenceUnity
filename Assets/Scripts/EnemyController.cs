using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    GameObject castle;

    [SerializeField]
    GameObject Healthbar;

    float enemiesNumber;

    [SerializeField]
    float moveSpeed;

    List<GameObject> enemies = new List<GameObject>();

    // Start is called before the first frame update
    public void SpawnEnemies(float amount)
    {
        enemiesNumber = amount;
        for (int i = 0; i < amount; i++)
        {
            float x = Random.Range(-20f, 20f);
            float z = Random.Range(-20f, 20f);
            Vector3 position = new Vector3(x, 1f, z);
            GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            enemies.Add(enemy);

            // Add colliders to enemy prefabs
            enemy.AddComponent<BoxCollider>();
            enemy.GetComponent<BoxCollider>().size = new Vector3(4f, 1f, 4f);
            enemy.GetComponent<BoxCollider>().isTrigger = false;
            enemy.AddComponent<Rigidbody>();
            enemy.GetComponent<Rigidbody>().mass = 1f;
            enemy.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            enemy.GetComponent<Rigidbody>().useGravity = true;
            enemy.GetComponent<Rigidbody>().isKinematic = false;
            enemy.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
            enemy.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;

            // Attach EnemyCollisionHandler script to each enemy
            enemy.AddComponent<IndividualEnemyController>();
            enemy.tag = "Enemy";
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = castle.transform.position;

        if (enemies != null)
        {
            foreach (GameObject enemy in enemies)
            {
                if (enemy != null)
                {
                    Vector3 newPosition = Vector3.MoveTowards(enemy.transform.position, targetPosition, moveSpeed * Time.deltaTime);
                    enemy.transform.position = newPosition;
                }
            }
        }
    }

    public int enemyAlive()
    {
        string objectName = "Black Widow(Clone)";
        GameObject[] enemies = GameObject.FindObjectsOfType<GameObject>().Where(obj => obj.name == objectName).ToArray();
        int enemyCount = enemies.Length;
        return enemyCount;
    }
}
