using UnityEngine;
using UnityEngine.AI;

public class NPCBehavior : MonoBehaviour
{
    public float shootingDistance = 5f;
    public float ignoringDistance = 15f;
    public float viewDistance = 20f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float shootingCooldown = 1f;
    private float lastShootTime;
    [SerializeField] private Animator anim;
    public LayerMask obstacleMask;
    [SerializeField] private entityStats enemyStats;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private NavMeshAgent navMeshAgent;
    public GameObject enemy;

    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("player").transform;
        lastShootTime = -shootingCooldown;
        navMeshAgent.angularSpeed = 360f;
        navMeshAgent.updateRotation = true;
    }

    void Update()
    {
        if (enemyStats.Health <= 0)
        {
            this.GetComponent<Rigidbody>().isKinematic = true;
            anim.enabled = false;
            Destroy(healthBar);
            Destroy(enemy, 10f);
            return;
        }

        Vector3 directionToPlayer = playerTransform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (Physics.Raycast(transform.position, directionToPlayer, out _, viewDistance, obstacleMask))
        {
            SetAnimatorState(true, false, false);
            return;
        }

        if (distanceToPlayer > ignoringDistance)
        {
            SetAnimatorState(true, false, false);
        }
        else if (distanceToPlayer > shootingDistance)
        {
            SetAnimatorState(false, false, true);
            navMeshAgent.SetDestination(playerTransform.position);
        }
        else if (Time.time > lastShootTime + shootingCooldown)
        {
            navMeshAgent.isStopped = true;
            SetAnimatorState(false, true, false);
            Shoot();
            lastShootTime = Time.time;
        }
    }

    void SetAnimatorState(bool isIdle, bool isShooting, bool isWalking)
    {
        anim.SetBool("isIdle", isIdle);
        anim.SetBool("isShooting", isShooting);
        anim.SetBool("isWalking", isWalking);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Vector3 bulletDirection = playerTransform.position - bulletSpawnPoint.position;
        bullet.GetComponent<Rigidbody>().velocity = bulletDirection.normalized * 10f;
    }
}