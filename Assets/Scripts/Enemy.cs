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
    public Transform mesh;
    public int health = Globals.monsterHealth;
    public int inkAmount = 10;
    public bool isDead = false;

    public void Init()
    {
        isDead = false;
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        // StartCoroutine(CheckIfInSafeZone());
    }

    public void UpdateLoop()
    {
        if (health <= 0) StartCoroutine(Die());


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
        else if (other.CompareTag("Player") && health > 0) 
        {
            GameEvents.Instance.PlayerHit();
            isDead = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SafeZone")) 
        {
            if (inLight) return;
            StartCoroutine(LoseAggro());
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SafeZone"))
            inLight = true;
        else
            inLight = false;
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


    public void TakeDamage() => StartCoroutine(TakeDamageCoroutine());
    IEnumerator TakeDamageCoroutine()
    {
        agent.speed = 1;
        yield return new WaitForSeconds(1);

        // Revert speed to normal
        agent.speed = 2;
    }


    // IEnumerator CheckIfInSafeZone()
    // {
    //     yield return new WaitForSeconds(0.5f);
    //     isDead = inLight;
    // }


    IEnumerator Die()
    {
        GameManager.Instance.gameData.inkAmount += inkAmount;
        mesh.position = new(mesh.position.x, mesh.position.y - 5 * Time.deltaTime, mesh.position.z);
        yield return new WaitForSeconds(0.5f);
        isDead = true;
    }
}
