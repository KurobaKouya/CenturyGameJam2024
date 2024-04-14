using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Navigation Variables")]
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private LayerMask groundLayerMask;

    private NavMeshAgent agent;
    private Vector3 walkPoint;
    private bool walkPointSet;
    private bool playerInSightRange;

    [Header("Variables")]
    public bool isDead = false;

    public void Init()
    {
        isDead = false;
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        StartCoroutine(DeathAfterSeconds());
    }

    public void UpdateLoop()
    {
        // Check for sight range
        playerInSightRange = Physics.CheckSphere(transform.position, Globals.monsterSightRange, playerLayerMask);

        if (!playerInSightRange) Patrol();
        else Chase();
    }


    private void Patrol()
    {
        if (!walkPointSet) SearchWalkPoint();
        else agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Reached
        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
    }


    private void Chase()
    {
        agent.SetDestination(GameManager.Instance.player.transform.position);
    }


    private void OnTriggerEnter(Collider other)
    {
        // if (other.CompareTag("Player")) 
    }

    private void SearchWalkPoint()
    {
        float randomX = Random.Range(-Globals.monsterPatrolRange, Globals.monsterPatrolRange);
        float randomZ = Random.Range(-Globals.monsterPatrolRange, Globals.monsterPatrolRange);

        walkPoint = new(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayerMask)) walkPointSet = true;
    }


    IEnumerator DeathAfterSeconds()
    {
        yield return new WaitForSeconds(5);
        isDead = true;
    }
}
