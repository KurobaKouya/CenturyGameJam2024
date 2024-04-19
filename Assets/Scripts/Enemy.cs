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
    [SerializeField]private bool inLight;

    [Header("Variables")]
    public Transform mesh;
    public float health = Globals.monsterHealth;
    public int inkAmount = 10;
    public bool isDead = false;
    [SerializeField]private float deathTimer = 2f;

    [Header("SFX")]
    [SerializeField] AudioClipInstance hurtSFX, deathSFX;

    public void Init()
    {
        isDead = false;
        inLight = false;
        deathTimer = 2f;
        StopAllCoroutines();
        mesh.localPosition = Vector3.zero;
        health = Globals.monsterHealth;
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        agent.speed = 3;
        walkPointSet = false;
        // StartCoroutine(CheckIfInSafeZone());
    }

    public void UpdateLoop()
    {
        if (health <= 0) StartCoroutine(Die());
        if (deathTimer <= 0f) StartCoroutine(DieFromLight());

        // Check for sight range
        playerInSightRange = Physics.CheckSphere(transform.position, Globals.monsterSightRange, playerLayerMask);

        if (!playerInSightRange || inLight) Patrol();
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
            walkPoint = transform.position - agent.destination * 5f;
            if (agent.isOnNavMesh) agent.SetDestination(walkPoint);
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
            StartCoroutine(LoseAggro());
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SafeZone"))
        {
            deathTimer -= Time.deltaTime;
            inLight = true;
        }
        else
        {
            deathTimer += 0.25f * Time.deltaTime;
            deathTimer = Mathf.Clamp(deathTimer, -1, 2f);
        }
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
        agent.speed = -3;
        AudioManager.instance.PlaySourceAudio(hurtSFX);
        yield return new WaitForSeconds(1);

        // Revert speed to normal
        agent.speed = 3;
    }


    // IEnumerator CheckIfInSafeZone()
    // {
    //     yield return new WaitForSeconds(0.5f);
    //     isDead = inLight;
    // }


    IEnumerator Die()
    {
        isDead = true;
        GameManager.Instance.gameData.inkAmount += inkAmount;
        mesh.localPosition = new(0, mesh.localPosition.y - 5 * Time.deltaTime, 0);
        AudioManager.instance.PlaySourceAudio(deathSFX);
        yield return new WaitForSeconds(0.5f);
    }


    IEnumerator DieFromLight()
    {
        mesh.localPosition = new(0, mesh.localPosition.y - 5 * Time.deltaTime, 0);
        yield return new WaitForSeconds(0.5f);
        isDead = true;
    }
}
