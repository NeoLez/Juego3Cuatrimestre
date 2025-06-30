using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class MainMenu : MonoBehaviour {
    [SerializeField] private GameObject controles;
    [SerializeField] private string sceneKey;
    private AsyncOperationHandle<SceneInstance> loadHandle;
    private bool sceneLoadedToMemory;
    private bool shouldLoadScene;
    private Scene mainMenuScene;

    private void Start() {
        mainMenuScene = SceneManager.GetActiveScene();
        
        loadHandle = Addressables.LoadSceneAsync(
            sceneKey, 
            LoadSceneMode.Single, 
            activateOnLoad: false
        );

        loadHandle.Completed += handle => {
            if (handle.Status == AsyncOperationStatus.Succeeded)
                sceneLoadedToMemory = true;
        };
    }

    private void Update() {
        if (shouldLoadScene && sceneLoadedToMemory) { 
            loadHandle.Result.ActivateAsync();
            
            shouldLoadScene = false;
        }
    }

    public void ShowControles() {
        controles.SetActive(true);
    }
    public void HideControles() {
        controles.SetActive(false);
    }
    public void Play() {
        shouldLoadScene = true;
        Debug.Log(shouldLoadScene);
        Debug.Log(sceneLoadedToMemory);
        
    }
    
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Cerraste el juego");
    }
}
