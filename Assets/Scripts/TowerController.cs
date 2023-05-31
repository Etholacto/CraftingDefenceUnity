using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerController : MonoBehaviour
{
    [SerializeField] private GameObject Projectile;
    [SerializeField] private Transform SpawnPoint;

    //Projectile Force
    [SerializeField] private float ForwardForce;

    //Tower Stats
    [SerializeField] private float ShotDelay;
    private float delay;

    //Bools
    private bool ready;

    private void Awake()
    {
        delay = ShotDelay;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ready = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ready = false;
        }
        if (other.gameObject.tag == "Projectile")
        {
            Destroy(other);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (ready)
        {
            if (col.gameObject.tag == "Player")
            {
                if (delay <= 0f)
                {
                    delay = ShotDelay;


                    Vector3 DirectionOfProjectile = col.transform.position - SpawnPoint.position;

                    //Make Projectile
                    GameObject currentProjectile = Instantiate(Projectile, SpawnPoint.position, Quaternion.identity);

                    //Add forces to Projectile
                    currentProjectile.GetComponent<Rigidbody>().AddForce(DirectionOfProjectile.normalized * ForwardForce, ForceMode.Impulse);

                    Destroy(currentProjectile, 2.5f);
                }

                delay--;
            }
        }
    }
}
