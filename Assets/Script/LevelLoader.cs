using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen; // Loading screen prefab

    [SerializeField] private string sceneToLoadName; // Scene name in build settings


    /// <summary>
    /// Launched when there's a click on the button in the scene, therefore the public
    /// </summary>
    public void LoadScene()
    {
        StartCoroutine(LoadSceneCoroutine());
    }

    /// <summary>
    /// Loads a scene asynchronously and display loading screen
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadSceneCoroutine()
    {
        // Making the loading screen appear
        var loadingScreenInstance = Instantiate(loadingScreen);
        // Making the loading screen persistent after we unloaded the scene
        DontDestroyOnLoad(loadingScreenInstance);
        // Getting the loading screen animator
        var loadingAnimator = loadingScreenInstance.GetComponent<Animator>();
        // Getting current animator state to retrieve the length in seconds of the actual animation
        var currentAnimTime = loadingAnimator.GetCurrentAnimatorStateInfo(0).length;
        // Start loading the scene in the background
        var loading = SceneManager.LoadSceneAsync(sceneToLoadName);
        // Disable auto-load 
        loading.allowSceneActivation = false;
        // While the scene is still loading
        while (!loading.isDone)
        {
            // If the scene loaded at 90% (which means 100% in Unity)
            if (loading.progress >= 0.9f)
            {
                // Launch the disappear animation
                loadingAnimator.SetTrigger("Disappear");
                // Make the new scene visible
                loading.allowSceneActivation = true;
            }
            // Wait for the end of the appearing animation before switching scenes
            yield return new WaitForSeconds(currentAnimTime);
        }
    }
}
