using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    public int currentLife;
    public int maxLife;
    public int minLife;
    public int enemyScorePoint;
    public float speed;
    public float attackRange;
    public float followRange;
    public bool alwaysFollow;
    private NavMeshAgent agent;
    private PlayerController target;
    [Header("Patrol")]
    public Transform[] points;
    private int desPoint = 0;
    private WeaponController weapon;

public void DamageEnemy(int quantity)
    {
        currentLife -= quantity;
        if (currentLife <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        weapon = GetComponent<WeaponController>();
        target = FindObjectOfType<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        GotoNextPoint();
        //agent.SetDestination(target.transform.position);
    }

    private void Update()
    {
        SearchEnemy();
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
        
        //agent.SetDestination(target.transform.position);
    }
    private void GotoNextPoint()
    {
        if (points.Length == 0)
        
            return;
        agent.destination = points[desPoint].position;
        desPoint = (desPoint + 1) % points.Length;
    }
    private void SearchEnemy()
    {
        RaycastHit hit;
        Vector3 direction = target.transform.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            if(hit.collider.CompareTag("Player") && hit.distance <= 10f)
            {
                agent.SetDestination(target.transform.position);
                agent.stoppingDistance = 3f;
                transform.LookAt(target.transform.position);
                if(hit.distance <= 9f)
                {
                    if(weapon.CanShoot())
                        weapon.Shoot();
                }
            }
            else
            {
                agent.stoppingDistance = 1f;
                
            } 
                
        }
            
            
    }
}
