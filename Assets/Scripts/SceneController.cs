using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public async Task InitialLoad(string sceneName)
    {
        var activeScene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (activeScene.progress < 1f) await Task.Yield();
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }

    public async Task ChangeSceneTo(string sceneName)
    {
        var activeScene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        activeScene.allowSceneActivation = false;
        while (activeScene.progress < 0.9f) await Task.Yield();
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        activeScene.allowSceneActivation = true;
        while (activeScene.progress < 1f) await Task.Yield();
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }
}