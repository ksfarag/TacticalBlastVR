using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamagePhysics : MonoBehaviour, ITakeDamage
{
    private Rigidbody RigidBody;

    void Start()
    {
        RigidBody = GetComponent<Rigidbody>();
    }

    public void TakeDamage(Bullet bullet, Vector3 pointOfImpact)
    {
        RigidBody.AddForce(bullet.transform.forward * bullet.GetHittingForce(), ForceMode.Impulse);
    }

    void Update()
    {
        
    }
}
