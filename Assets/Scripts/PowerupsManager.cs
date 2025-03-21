using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsManager : MonoBehaviour
{

    [SerializeField] private GameObject cannonballPrefab;
    [SerializeField] private GameObject cannonballHolder;
    [SerializeField] private Sprite explosiveCannonballSprite;
    
    [SerializeField] private int randomUpgrade;
    [SerializeField] private bool receivedUpgade;
    [SerializeField] private int splitShootForce;

    private void Start()
    {
        SpawnCircle();
    }

    private void OnTriggerEnter2D(Collider2D cannonball)
    {
        if (cannonball.gameObject.CompareTag("Cannonball") && !receivedUpgade)
        {
            Debug.Log("hit circle");
            //randomUpgrade = Random.Range(1, 7);
            randomUpgrade = 3;

            Rigidbody2D rb = cannonball.GetComponent<Rigidbody2D>();
            Vector2 currentDirection = rb.velocity.normalized;

            switch (randomUpgrade)
            {
                case 1:
                    // Size increase (Cannonball gets bigger)
                    cannonball.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
                    break;
                case 2:
                    // Speed increase (Cannonball gets faster)
                    rb.AddForce(currentDirection * 5, ForceMode2D.Impulse);
                    break;
                case 3:
                    // Split projectile (Cannonball duplicates into 2 cannonballs)
                    Transform cannonballPos = cannonball.GetComponent<Transform>();
                    Rigidbody2D originalRb = cannonball.GetComponent<Rigidbody2D>();
                    Vector2 originalDirection = originalRb.velocity.normalized;

                    // Spawn new cannonballs at slightly different positions
                    Vector3 split1Pos = cannonballPos.position + new Vector3(0.5f, 0.5f, 0f);
                    Vector3 split2Pos = cannonballPos.position + new Vector3(-0.5f, -0.5f, 0f);

                    GameObject splitCannonball1 = Instantiate(cannonballPrefab, split1Pos, Quaternion.identity, cannonballHolder.transform);
                    Rigidbody2D rb1 = splitCannonball1.GetComponent<Rigidbody2D>();
                    rb1.AddForce(originalDirection * splitShootForce, ForceMode2D.Impulse);

                    GameObject splitCannonball2 = Instantiate(cannonballPrefab, split2Pos, Quaternion.identity, cannonballHolder.transform);
                    Rigidbody2D rb2 = splitCannonball2.GetComponent<Rigidbody2D>();
                    rb2.AddForce(originalDirection * splitShootForce, ForceMode2D.Impulse);

                    Destroy(cannonball.gameObject);
                    break;
                case 4:
                    // Heavy cannonball (Increase speed and mass of cannonball)
                    cannonball.GetComponent<SpriteRenderer>().color = new Color32(200, 255, 225, 255);
                    cannonball.GetComponent<Rigidbody2D>().mass = 2;
                    rb.AddForce(currentDirection * 5, ForceMode2D.Impulse);
                    break;
                case 5:
                    // Explosive Cannonball (Cannonball explodes on impact)
                    cannonball.GetComponent<SpriteRenderer>().sprite = explosiveCannonballSprite;
                    break;
                case 6:
                    // Ghosting through walls, so you can only hit enemies (Cannonball can go through everything except the enemies and ground)
                    break;
                default:
                    Debug.Log("Upgrade didnt work");
                    break;
            }
            receivedUpgade = true;
            this.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(WaitForSpawnCircle());
        }
    }

    IEnumerator WaitForSpawnCircle()
    {
        yield return new WaitForSeconds(3f);
        SpawnCircle();
    }

    private void SpawnCircle()
    {
        float randomX = Random.Range(-0.75f, 7.85f);
        float randomY = Random.Range(-3.75f, 3.75f);

        this.GetComponent<Transform>().position = new Vector3(randomX, randomY, 1f);
        receivedUpgade = false;
        this.GetComponent<SpriteRenderer>().enabled = true;
    }
}