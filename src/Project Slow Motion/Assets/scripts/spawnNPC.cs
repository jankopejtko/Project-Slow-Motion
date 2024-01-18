using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnNPC : MonoBehaviour
{
    public List<GameObject> spawns = new List<GameObject>();
    List<GameObject> NPCs = new List<GameObject>();
    public GameObject NPC_prefab;
    private void Start()
    {
    }
    void Update()
    {
        NPCs.AddRange(GameObject.FindGameObjectsWithTag("NPC"));
        if (NPCs.Count <= 2)
        {
            GameObject randomSpawn = spawns[Random.Range(0, spawns.Count)];
            Instantiate(NPC_prefab, randomSpawn.transform.position, randomSpawn.transform.rotation);
        }
        NPCs.Clear();
    }
}
