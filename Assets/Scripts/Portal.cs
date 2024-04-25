using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomObjectSpawner : MonoBehaviour
{
    public GameObject portalImage; // The object to spawn
    public float spawnInterval; // Time interval between spawns
    public float spawnDelay; // Time interval between spawns
    public float OriginalSpawnDelay; // Time interval between spawns
    public float OriginaSpawnInterval; // Time interval between spawns
    public float objectLifetime = 15f; // How long the object lasts
    private GameObject puckScript;

    public float Minx, Miny;
    bool canTeleport = false;

    private GameObject spawnedObject; // Reference to the spawned object
    private void Start()
    {
        // Start spawning objects
        puckScript = GameObject.FindGameObjectWithTag("Puck");
    }
    private void Update()
    {
        if (canTeleport == true)
        {
            spawnInterval = spawnInterval - Time.deltaTime;
        }

        if (spawnInterval <= 0)
        {
            print("tst");
            Vector3 spawnPosition = new Vector3(Minx, Miny, 0); // Generate the position where the Puck will be telportd to
            spawnedObject = Instantiate(portalImage, spawnPosition, Quaternion.identity);
            spawnDelay = spawnDelay - Time.deltaTime;
            if (spawnDelay <= 0)
            {
                puckScript.transform.position = spawnPosition;
                puckScript.gameObject.SetActive(true);
                spawnInterval = OriginaSpawnInterval;
                canTeleport = false;
                spawnDelay = OriginalSpawnDelay;
            }
        }
    }
  


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Puck"))
        {
            puckScript.gameObject.SetActive(false);//make the puck disappear when it touches the portal
            canTeleport = true;
        }
        else
        {
            return;
        }
    }
}
      
