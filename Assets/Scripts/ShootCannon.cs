using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCannon : MonoBehaviour
{

    [SerializeField] private GameObject cannonballPrefab;
    [SerializeField] private GameObject cannonballHolder;
    [SerializeField] private Vector3 spawnPos;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(cannonballPrefab, spawnPos, Quaternion.identity, cannonballHolder.transform);

            Vector3 mousePos = Input.mousePosition;

            Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);

        }
    }
}
