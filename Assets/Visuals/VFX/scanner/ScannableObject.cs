using System;
using System.Collections;
using UnityEngine;

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

    private void Update() {
        if(currentVfxInstance != null)
            currentVfxInstance.transform.position = transform.position;
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
            currentVfxInstance = Instantiate(vfxPrefab, spawnPosition, Quaternion.identity);
            GameObject lightgameObject = currentVfxInstance.transform.GetChild(0).gameObject;
            Light light = lightgameObject.GetComponent<Light>();
            GameObject beam = currentVfxInstance.transform.GetChild(1).gameObject;
            GameObject beamVisual = beam.transform.GetChild(0).gameObject;
            Renderer beamRenderer = beamVisual.GetComponent<Renderer>();
            Material materialClone = new Material(beamRenderer.material);
            beamRenderer.material = materialClone;
            
            LeanTween.value(lightgameObject, 0f, 19f, 19f).setOnUpdate((float val) => {
                if(val < 4f)
                    light.intensity = val/4f;
                else if (val > 15)
                    light.intensity = 1 - (val - 15)/4f;
                else {
                    light.intensity = 1 - (0.5f - (float)Math.Cos((val - 4) * 2 * Math.PI * 2 / 11f) / 2f) / 2f;
                }
            });
            
            LeanTween.value(beam, 0f, 19, 19f).setOnUpdate((float vale) => {
                //Debug.Log(val);
                if(vale < 4f)
                    materialClone.SetFloat("_Opacity", vale/4f);
                else if (vale > 15)
                    materialClone.SetFloat("_Opacity", 1 - (vale - 15)/4f);
                else {
                    materialClone.SetFloat("_Opacity", 1 - (0.5f - (float)Math.Cos((vale - 4) * 2 * Math.PI * 2 / 11f) / 2f)/1.5f);
                }
            });
            
            
            
            Destroy(currentVfxInstance, 20f); 
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

