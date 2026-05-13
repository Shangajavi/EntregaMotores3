using System;
using UnityEngine;
[RequireComponent(typeof(CapsuleCollider))]
public class EnemyOne : MonoBehaviour, IDamagable
{
    [SerializeField]private int health = 10;

    private MeshRenderer mR;
    

    private void Awake()
    {
        mR = GetComponent<MeshRenderer>();
        
    }

    private void Update()
    {
        if (health <= 0)
        {
            mR.enabled = false;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
