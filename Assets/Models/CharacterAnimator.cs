using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    NavMover mover;
    Animator animator;
    AI ai;

    void Awake()
    {
        mover = GetComponentInParent<NavMover>();
        ai = GetComponentInParent<AI>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        animator.SetFloat("Speed", 1f);
    }

    void Update()
    {
        animator.SetFloat("Speed", mover.Speed); 
        animator.SetBool("Attack", ai.ActiveTask is AttackTarget && mover.Speed <= 0.1f); 
    }
}
