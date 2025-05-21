using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ScannableObject : MonoBehaviour
{
    //public VisualEffect ScanVfx;
    private GameObject currentVfxInstance;
    public GameObject vfxPrefab;
    public Renderer objectRenderer;
    public Color highlightColor = Color.cyan;

    private Color originalColor;

    void Start()
    {
        if (objectRenderer != null)
            originalColor = objectRenderer.material.color;
    }

    public void OnScanned()
    {
        // change color
        /*if (objectRenderer != null)
        {
            objectRenderer.material.color = highlightColor;
        } */
            

        // active particles
        /*if (ScanVfx != null)
        {
            ScanVfx.SendEvent("OnPlay");
            Debug.Log("the object was scanned");
        }
        else
        {
            Debug.Log("no vfx found");
        } */
        
        #region
        if (vfxPrefab != null)
        {
            Vector3 spawnPosition = transform.position + Vector3.down * 0.5f; 
            GameObject vfxInstance = Instantiate(vfxPrefab, spawnPosition, Quaternion.identity);
            Destroy(vfxInstance, 20f); 
        }
        else
        {
            Debug.LogWarning("No VFX prefab assigned.");
        }
        
        #endregion
        
        #region Mode2
        
        /*if (currentVfxInstance != null)
        {
            Destroy(currentVfxInstance);
        }
        if (vfxPrefab != null)
        {
            Vector3 spawnPosition = transform.position + Vector3.down * 0.5f; 
            currentVfxInstance = Instantiate(vfxPrefab, spawnPosition, Quaternion.identity);
            Destroy(currentVfxInstance, 20f); 
        }
        else
        {
            Debug.LogWarning("No VFX prefab assigned.");
        }
         */   
        #endregion

        // restore color after selected seconds
        StartCoroutine(ResetColor());
    }

    IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(10f);
        if (objectRenderer != null)
            objectRenderer.material.color = originalColor;
    }
}

