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
    public bool enableSmoothBrainNav = true;
    public GameObject enemy;
    public GameObject hitbox;

    private Transform playerTransform;

    [SerializeField] AudioSource audioShoot;
    [SerializeField] AudioSource audioWalk;


    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("NPCshootTarget").transform;
        lastShootTime = -shootingCooldown;
        navMeshAgent.angularSpeed = 360f;
        navMeshAgent.updateRotation = true;

        //modify values
        navMeshAgent.speed *= Random.Range(0.75f, 1.25f);
        shootingDistance = shootingDistance + Random.Range(-2, 4);
    }

    void Update()
    {
        if (enemyStats.Health <= 0)
        {
            this.GetComponent<Rigidbody>().useGravity = false;
            this.GetComponent<Rigidbody>().isKinematic = true;
            anim.enabled = false;
            Destroy(hitbox);
            Destroy(navMeshAgent);
            Destroy(healthBar);
            Destroy(enemy, 10f);
            return;
        }
        else 
        {
            Vector3 directionToPlayer = playerTransform.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;
            //show ray in scene for debug
            Debug.DrawRay(transform.position, directionToPlayer * distanceToPlayer, Color.red);
            if (Physics.Raycast(transform.position, directionToPlayer, out _, distanceToPlayer, obstacleMask))
            {
                SetAnimatorState(true, false, false);
                navMeshAgent.isStopped = true;
                return;
            }
            else 
            {
                if (distanceToPlayer > ignoringDistance)
                {
                    SetAnimatorState(true, false, false);
                    navMeshAgent.isStopped = true;
                    Debug.Log("player is ignored");
                }
                else if (distanceToPlayer > shootingDistance)
                {
                    SetAnimatorState(false, false, true);
                    navMeshAgent.isStopped = false;
                    if(NavMesh.SamplePosition(playerTransform.position, out _, 1.0f, NavMesh.AllAreas)) 
                    {
                        navMeshAgent.SetDestination(playerTransform.position);
                        Debug.Log("nav agent in use");
                    }
                    else if(enableSmoothBrainNav)
                    {
                        lookAtPlayer();
                        transform.Translate(directionToPlayer.normalized * navMeshAgent.speed * Time.deltaTime, Space.World);
                        Debug.Log("switching to smooth brain");
                    }
                    if (audioWalk.isPlaying) 
                    {
                    }
                    else 
                    {
                        audioWalk.Play();
                    }
                }
                else
                {
                    audioWalk.Stop();
                    lookAtPlayer();
                    if (Time.time > lastShootTime + shootingCooldown)
                    {
                        SetAnimatorState(false, true, false);
                        navMeshAgent.isStopped = true;
                        Shoot();
                        lastShootTime = Time.time;
                        audioShoot.Play();
                        Debug.Log("enemy is shooting at player");
                    }
                }
            }

        }

    }

    private void lookAtPlayer()
    {
        //npc look at player
        Vector3 lookAtPosition = playerTransform.position;
        lookAtPosition.y = transform.position.y;
        transform.LookAt(lookAtPosition);
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