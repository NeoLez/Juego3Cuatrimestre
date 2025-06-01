using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class TerrainScanner : MonoBehaviour
{
    public GameObject terrainScanPrefab;
    public float duration = 10;
    public float size = 500;
    
    void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            ActivateSpawnScanner();
        }
    }

    void ActivateSpawnScanner()
    {
        GameObject terrainScan = Instantiate(terrainScanPrefab, gameObject.transform.position, Quaternion.identity) as GameObject;
        ParticleSystem terrainScannerPS = terrainScan.GetComponentInChildren<ParticleSystem>();

        if (terrainScannerPS != null)
        {
            var main = terrainScannerPS.main;
            main.startLifetime = duration;
            main.startSize = size;
        }
        else
        {
            Debug.Log("the first child doesnt have a particle system");
        }
        Destroy(terrainScan, duration + 1);
    }
}
