using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Rigidbody _rb;
    public GameObject explosionPrefab;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Fire(float speed, Vector3 direction)
    {
        _rb.velocity = direction * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Explode();
    }

    private void Explode()
    {
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}