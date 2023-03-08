using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float hittingForce;
    private float damage;

    public void SetForce(float force) 
    {
        hittingForce = force;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public float GetHittingForce()
    {
        return hittingForce;
    }

    public float GetDamage()
    {
        return damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        ITakeDamage[] targets = other.GetComponentsInParent<ITakeDamage>();
        foreach (var target in targets)
        {
            target.TakeDamage(this, transform.position);
        }
        Destroy(gameObject);
    }
}
