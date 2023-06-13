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
    private bool shooting;

    public ParticleSystem ps;

    private void Awake()
    {
        delay = ShotDelay;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            shooting = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            shooting = false;
        }
        if (other.gameObject.tag == "Projectile")
        {
            Destroy(other);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (shooting)
        {
            if (col.gameObject.tag == "Enemy")
            {
                if (delay <= 0f)
                {
                    delay = ShotDelay;


                    Vector3 DirectionOfProjectile = col.transform.position - SpawnPoint.position;

                    //Make Projectile
                    GameObject currentProjectile = Instantiate(Projectile, SpawnPoint.position, Quaternion.identity);

                    //Add forces to Projectile
                    currentProjectile.GetComponent<Rigidbody>().AddForce(DirectionOfProjectile.normalized * ForwardForce, ForceMode.Impulse);

                    if (ps)
                    {
                        float step = 5f * Time.deltaTime;
                        Vector3 newDirection = Vector3.RotateTowards(transform.forward, DirectionOfProjectile, step, 0.0f);
                        ps.transform.rotation = Quaternion.LookRotation(newDirection);
                        ps.Play();
                    }

                    Destroy(currentProjectile, 2.5f);
                }

                delay--;
            }
        }
    }
}
