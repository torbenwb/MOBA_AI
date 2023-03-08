using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType{Minion, Tower, Nexus, Hero}

public class Unit : MonoBehaviour, IDamageable
{
    public IntWrapper Health;
    public UnitType unitType;

    public void Damage(int amount) {
        Health -= amount; 
        if (Health <= 0) Destroy(gameObject);
    }   
}
