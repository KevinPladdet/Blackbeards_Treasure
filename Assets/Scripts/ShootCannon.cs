using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShootCannon : MonoBehaviour
{

    [SerializeField] private GameManager gm;

    [Header("Cannon")]
    [SerializeField] private GameObject cannonballPrefab;
    [SerializeField] private GameObject cannonballHolder;
    [SerializeField] private Transform cannonBarrel;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float shootForce;

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI cannonballAmountText;
    
    private void Start()
    {
        cannonballAmountText.text = "" + gm.amountCannonballs;
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // Rotate cannonBarrel to mousePos
        Vector2 direction = (cannonBarrel.position - mousePos).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (!gm.gameIsPaused)
        {
            cannonBarrel.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (Input.GetMouseButtonDown(0) && gm.amountCannonballs >= 1 || Input.GetKeyDown(KeyCode.Space) && gm.amountCannonballs >= 1)
        {
            if (!gm.gameIsPaused)
            {
                GameObject cannonball = Instantiate(cannonballPrefab, spawnPos.position, Quaternion.identity, cannonballHolder.transform);

                Rigidbody2D rb = cannonball.GetComponent<Rigidbody2D>();
                Vector2 shootDirection = (mousePos - spawnPos.position).normalized;

                rb.AddForce(-shootDirection * shootForce, ForceMode2D.Impulse);

                gm.amountCannonballs -= 1;
                cannonballAmountText.text = "" + gm.amountCannonballs;
                if (gm.amountCannonballs == 0 && gm.piratesAlive >= 1)
                {
                    StartCoroutine(WaitForFinalCannonball());
                }
            }
        }
    }

    // Wait for final cannonball to potentially hit a pirate
    IEnumerator WaitForFinalCannonball()
    {
        yield return new WaitForSeconds(5f);
        if (gm.amountCannonballs == 0 && gm.piratesKilled <= 2)
        {
            gm.GameOver();
        }
    }
}