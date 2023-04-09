using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAtacking : MonoBehaviour
{
    private ObjectDetector objectDetector;
    public string enemyTag;

    public int damage = 5;

    int lastAttackInt = -1, randomAttackInt;


    Animator animator;
    AnimationEventHandler eventHandler;

    bool hitSomeone = false;

    private void Awake()
    {
        objectDetector = GetComponentInChildren<ObjectDetector>();
        animator = GetComponentInChildren<Animator>();
        eventHandler = GetComponentInChildren<AnimationEventHandler>();
        eventHandler.onFinish += Attack;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            while (randomAttackInt == lastAttackInt)
            {
                randomAttackInt = UnityEngine.Random.Range(0, 4);
            }

            if (randomAttackInt != lastAttackInt)
                animator.SetInteger("attackInt", randomAttackInt);

            animator.SetTrigger("attack");

            hitSomeone = false;
            foreach (Collider colider in objectDetector.colliders)
            {
                Health health = colider.GetComponent<Health>();
                if (health != null && colider.CompareTag(enemyTag))
                {
                    health.TakeDamage(damage);
                    hitSomeone = true;
                }
            }
            lastAttackInt = randomAttackInt;
        }
    }

    public void Attack()
    {

        if (hitSomeone)
        {
            CinemachineShaker.instance.ShakeCamera(2, 0.3f);
        }

    }

}
