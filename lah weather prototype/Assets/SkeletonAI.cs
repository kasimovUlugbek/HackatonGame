using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonAI : Health
{
    public static int instances=0;
    NavMeshAgent navMesh;

    bool sawHuman;
    Transform human;
    public string humanTag;

    public float distToStop = 5f;
    public float distToAway = 2f;
    public float distForRecovery = 1.5f;
    public float distToBeIdle = 0.2f;

    public float breakAfterAttack = 2f;
    float breakPassed = 0;
    bool startedAttack = false, finishedAtack = false;

    public enum State
    {
        Idle,
        chasing,
        atacking,
        backingAway
    }
    public State state;

    Animator animator;
    AnimationEventHandler eventHandler;

    private void Awake()
    {
        instances++;
        navMesh = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        eventHandler = GetComponentInChildren<AnimationEventHandler>();
        eventHandler.onFinish += FinishAtacking;
        onDamaged += Recover;
    }

    void Update()
    {
        switch (state)
        {
            case State.Idle:
                if (sawHuman)
                    state = State.chasing;
                break;
            case State.chasing:
                float distance = Vector3.Distance(transform.position, human.position);
                if (distance > distToStop)//keep chasing
                    navMesh.SetDestination(human.position);
                else if (distance < distToAway)//back away
                {
                    breakPassed = breakAfterAttack;
                    state = State.backingAway;
                }
                else
                {
                    navMesh.SetDestination(transform.position);
                    state = State.atacking;
                }
                break;
            case State.atacking:
                if (startedAttack == false)
                {
                    animator.SetTrigger("attack");
                    startedAttack = true;
                }
                else if (finishedAtack)
                {
                    finishedAtack = false;
                    startedAttack = false;

                    breakPassed = breakAfterAttack;
                    state = State.backingAway;
                }

                break;
            case State.backingAway:
                breakPassed -= Time.deltaTime;
                Vector3 dirAwayFromHuman = transform.position - human.position;
                if (breakPassed > 0)
                    navMesh.SetDestination((dirAwayFromHuman.normalized * distForRecovery) + transform.position);
                else if (breakPassed < 0)
                    state = State.chasing;
                break;
            default:
                break;
        }

        animator.SetBool("moving", Vector3.Distance(transform.position, navMesh.destination) > distToBeIdle);

        if (currentHealth <= 0)
        {
            instances--;
            Destroy(gameObject);
        }
    }
    public void FinishAtacking()
    {
        finishedAtack = true;
        CinemachineShaker.instance.ShakeCamera(2, 0.3f);
    }
    public void Recover()
    {
        state = State.backingAway;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(humanTag))
        {
            sawHuman = true;
            human = other.transform;
        }
    }
    private void OnDrawGizmos()
    {
        if (navMesh != null)
            Gizmos.DrawSphere(navMesh.destination, 1);
    }
}