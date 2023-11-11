using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    private void Start()
    {
        _ = FindObjectOfType<SceneController>().InitialLoad("Menu");
    }
}
