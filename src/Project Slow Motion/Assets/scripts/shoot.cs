using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class shoot : MonoBehaviour
{
    public string ObjName;
    private bool shootEnabled = true;
    [SerializeField] private GameObject bullet_prefab;
    [SerializeField] private GameObject bulletSpawnPoint;
    [SerializeField] private GameObject[] smoke;
    [SerializeField] private float bulletSpeed = 10;
    [SerializeField] private float reloadDeley;
    [SerializeField] private int bulletNumberMAX;
    public int BulletNumberMAX { get => bulletNumberMAX; }
    [SerializeField] private int numOfBulletsShotAtOnce;
    private int bulletNumber;
    public int BulletNumber { get => bulletNumber; }

    [SerializeField] private float range;

    private void Start()
    {
        bulletNumber = bulletNumberMAX;
    }
    public void shootBullet()
    {
        if (bulletNumber > 0 && shootEnabled)
        {
            bulletNumber--;
            if (numOfBulletsShotAtOnce > 1)
            {
                for (int i = 0; i < numOfBulletsShotAtOnce; i++)
                {
                    float randomSpread = Random.Range(-range, range);
                    Quaternion spreadRotation = Quaternion.Euler(randomSpread, randomSpread, randomSpread);
                    GameObject bullet = Instantiate(bullet_prefab, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation * spreadRotation);
                    bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletSpeed);
                }
            }
            else
            {
                GameObject bullet = Instantiate(bullet_prefab, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
                bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletSpeed);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.forward * 1);
    }
    private void Update()
    {
        if (bulletNumber <= 0)
        {
            if (bulletNumber < 0) 
            {
                Debug.LogWarning("bullet count less than zero");
            }
            StartCoroutine(reload());
            bulletNumber = bulletNumberMAX;
        }
    }
    IEnumerator reload()
    {
        Debug.Log("reloading " + ObjName);
        foreach (GameObject s in smoke)
        {
            s.SetActive(true);
            s.GetComponent<ParticleSystem>().Play();
        }
        shootEnabled = false;
        yield return new WaitForSeconds(reloadDeley);
        shootEnabled = true;
        bulletNumber = bulletNumberMAX;
        foreach (GameObject s in smoke)
        {
            s.GetComponent<ParticleSystem>().Stop();
        }
        Debug.Log(ObjName + " reloaded");
    }
}
