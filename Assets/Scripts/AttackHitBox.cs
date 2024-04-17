using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    [HideInInspector] public int damage = 0;
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Enemy":
                Enemy en = other.GetComponent<Enemy>();
                en.health -= damage;
                en.TakeDamage();
                break;
        }
    }
}
