using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class MinionWave_SO : ScriptableObject
{
    [SerializeField] List<GameObject> minions;

    public List<GameObject> Minions
    {
        get => new List<GameObject>(minions);
    }
}
