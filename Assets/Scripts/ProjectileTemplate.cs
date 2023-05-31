using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTemplate : MonoBehaviour
{
    private void Start()
    {
        GameObject tower = GameObject.FindGameObjectWithTag("Tower");
        Physics.IgnoreCollision(tower.GetComponent<BoxCollider>(), GetComponent<SphereCollider>());
    }

    private void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
    }
}
