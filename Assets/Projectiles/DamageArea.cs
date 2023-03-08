using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public bool destroyOnHit = true;
    public int damage = 10;
    public GameObject destroyFXPrefab;
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<IDamageable>().Damage(damage);
        if (destroyOnHit) {
            if (destroyFXPrefab) Instantiate(destroyFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
