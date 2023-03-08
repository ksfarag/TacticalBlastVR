using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour, ITakeDamage
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float minTimeUnderCover;
    [SerializeField] private float maxTimeUnderCover;
    [SerializeField] private int minShotsToTake;
    [SerializeField] private int maxShotsToTake;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float damage;
    [Range(0, 100)] [SerializeField] private int shootingAccuracy;
    [SerializeField] private ParticleSystem bloodSplatterPFX;
    [SerializeField] private Transform shootingPos;

    private bool isShooting;
    private int currentShotsTaken;
    private int currentMaxtShotsToTake;
    private NavMeshAgent agent;
    private Player player;
    private Transform coverSpot;
    private Animator animator;
    private float m_health;



    public float health 
    {
        set
        {
            m_health = Mathf.Clamp(value, 0, startingHealth);
        }
        get
        {
            return m_health;
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        animator.SetTrigger("Run");
        m_health = startingHealth;
    }

    private void Update()
    {
        if (agent.isStopped == false && (transform.position - coverSpot.position).sqrMagnitude <= 0.1f)
        {
            agent.isStopped= true;
            StartCoroutine(ShootingCo());
        }
        if (isShooting)
        {
            RotateTowardsPlayer();
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = player.GetHeadPosition() - transform.position;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction);
        rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        transform.rotation = rotation;
    }

    private IEnumerator ShootingCo()
    {
        HideBehindCover();
        yield return new WaitForSeconds(UnityEngine.Random.Range(minTimeUnderCover, maxTimeUnderCover));
        StartShooting();
    }

    private void StartShooting()
    {
        isShooting= true;
        currentMaxtShotsToTake = UnityEngine.Random.Range(minShotsToTake, maxShotsToTake);
        currentShotsTaken = 0;
        animator.SetTrigger("Shoot");
    }

    public void Shoot()
    {
        bool hitChance = UnityEngine.Random.Range(0, 100) < shootingAccuracy;
        if (hitChance)
        {
            RaycastHit hit;
            Vector3 direction = player.transform.position;
            if (Physics.Raycast(shootingPos.position, direction, out hit))
            {
                if (hit.collider.GetComponentInParent<Player>())
                {
                    player.TakeDamage(damage);
                }
            }
        }
        currentShotsTaken++;
        if (currentShotsTaken >= currentMaxtShotsToTake)
        {
            StartCoroutine(ShootingCo());
        }
    }
    private void HideBehindCover()
    {
        animator.SetTrigger("Crouch");
    }

    public void Init(Player player, Transform coverSpot)
    {
        this.player = player;
        this.coverSpot = coverSpot;
        GetCover();
    }

    private void GetCover()
    {
        agent.isStopped= false;
        agent.SetDestination(coverSpot.position);
    }

    public void TakeDamage(Bullet bullet, Vector3 pointOfImpact)
    {
        health -= bullet.GetDamage();
        if (health <= 0) { Destroy(gameObject); }
        ParticleSystem effect = Instantiate(bloodSplatterPFX, pointOfImpact, Quaternion.LookRotation(player.transform.position - pointOfImpact));
        effect.Stop();
        effect.Play();
    }


}
