using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Navigation Variables")]
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask lightLayerMask;

    private NavMeshAgent agent;
    private Vector3 walkPoint;
    private bool walkPointSet;
    private bool playerInSightRange;
    private bool inLight;

    [Header("Variables")]
    public int health = Globals.monsterHealth;
    public bool isDead = false;

    public void Init()
    {
        isDead = false;
        if (agent == null) agent = GetComponent<NavMeshAgent>();
    }

    public void UpdateLoop()
    {
        if (health <= 0) ObjectPoolManager.ReturnObjectToPool(gameObject);


        // Check for sight range
        playerInSightRange = Physics.CheckSphere(transform.position, Globals.monsterSightRange, playerLayerMask);

        if (!playerInSightRange) Patrol();
        else if (!inLight) Chase();
    }


    private void Patrol()
    {
        if (!walkPointSet) SearchWalkPoint();
        else if (agent.isOnNavMesh) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Reached
        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
    }


    private void Chase()
    {
        if (agent.isOnNavMesh) agent.SetDestination(GameManager.Instance.player.transform.position);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SafeZone")) 
        {
            inLight = true;
            walkPoint = transform.position - agent.destination;
            if (agent.isOnNavMesh) agent.SetDestination(transform.position - agent.destination);
        }
        else if (other.CompareTag("Player")) 
        {
            GameEvents.Instance.PlayerHit();
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SafeZone")) StartCoroutine(LoseAggro());
    }


    private void SearchWalkPoint()
    {
        float randomX = Random.Range(-Globals.monsterPatrolRange, Globals.monsterPatrolRange);
        float randomZ = Random.Range(-Globals.monsterPatrolRange, Globals.monsterPatrolRange);

        walkPoint = new(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayerMask)) walkPointSet = true;
    }


    IEnumerator LoseAggro()
    {
        yield return new WaitForSeconds(1f);
        inLight = false;
    }
}
