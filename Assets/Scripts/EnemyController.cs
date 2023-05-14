using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{ 

    [SerializeField]
    GameObject enemyCommunityHolder;

    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    float moveSpeed = 5f;

    List<GameObject> enemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 10; i++)
    {   
        float x = Random.Range(-20f, 20f);
        float z = Random.Range(-20f, 20f);
        Vector3 position = new Vector3(x, 0, z);
        GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        enemies.Add(enemy);

        // Add colliders to enemy prefabs
        enemy.AddComponent<CapsuleCollider>();
        enemy.GetComponent<CapsuleCollider>().radius = 0.5f;
        enemy.GetComponent<CapsuleCollider>().isTrigger = false;
        enemy.AddComponent<Rigidbody>();
        enemy.GetComponent<Rigidbody>().useGravity = false;
        enemy.GetComponent<Rigidbody>().isKinematic = true;
    }
    }

    // Update is called once per frame
    void Update()
    {   
        Vector3 targetPosition = Vector3.zero;
        
        foreach(GameObject enemy in enemies)
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
            // Reverse direction of collided enemies
            Vector3 direction = collision.gameObject.transform.position - enemies[Random.Range(0, enemies.Count)].transform.position;
            direction.Normalize();
            collision.gameObject.transform.position += direction * moveSpeed * Time.deltaTime;
            enemies[Random.Range(0, enemies.Count)].transform.position -= direction * moveSpeed * Time.deltaTime;
        }
    }
}