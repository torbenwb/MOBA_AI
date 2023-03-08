using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    public bool Request{set;}
    public void Execute();
    public IEnumerator Cooldown(float cooldownTime);
}