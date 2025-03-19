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
    [SerializeField] private int amountCannonballs;
    
    private void Start()
    {
        cannonballAmountText.text = "" + amountCannonballs;
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // Rotate cannonBarrel to mousePos
        Vector2 direction = (cannonBarrel.position - mousePos).normalized;   
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        cannonBarrel.rotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetMouseButtonDown(0) && amountCannonballs >= 1)
        {
            GameObject cannonball = Instantiate(cannonballPrefab, spawnPos.position, Quaternion.identity, cannonballHolder.transform);
            
            Rigidbody2D rb = cannonball.GetComponent<Rigidbody2D>();
            Vector2 shootDirection = (mousePos - spawnPos.position).normalized;

            rb.AddForce(-shootDirection * shootForce, ForceMode2D.Impulse);

            amountCannonballs -= 1;
            cannonballAmountText.text = "" + amountCannonballs;
            if (amountCannonballs == 0 && gm.piratesAlive >= 1)
            {
                StartCoroutine(WaitForFinalCannonball());
            }
        }
    }

    // Wait for final cannonball to potentially hit a pirate
    IEnumerator WaitForFinalCannonball()
    {
        yield return new WaitForSeconds(5f);
        if (amountCannonballs == 0 && gm.piratesAlive >= 1)
        {
            gm.GameOver();
        }
    }
}