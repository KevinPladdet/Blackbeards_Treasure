using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCannon : MonoBehaviour
{

    [SerializeField] private GameObject cannonballPrefab;
    [SerializeField] private GameObject cannonballHolder;
    
    [SerializeField] private Transform cannonBarrel;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float shootForce;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // Rotate cannonBarrel to mousePos
        Vector2 direction = (mousePos - cannonBarrel.position).normalized;   
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        cannonBarrel.rotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject cannonball = Instantiate(cannonballPrefab, spawnPos.position, Quaternion.identity, cannonballHolder.transform);
            
            Rigidbody2D rb = cannonball.GetComponent<Rigidbody2D>();
            Vector2 shootDirection = (mousePos - spawnPos.position).normalized;
            
            rb.AddForce(shootDirection * shootForce, ForceMode2D.Impulse);
        }
    }
}
