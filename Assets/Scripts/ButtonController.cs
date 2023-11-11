using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public void ChangeSceneTo(string sceneName)
    {
        _ = FindObjectOfType<SceneController>().ChangeSceneTo(sceneName);
    }
}
