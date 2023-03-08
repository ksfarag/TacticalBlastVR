using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] Transform head; //Main camera obj

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Player Health: " + health);
    }
    public Vector3 GetHeadPosition()
    {
        return head.position;
    }
}
