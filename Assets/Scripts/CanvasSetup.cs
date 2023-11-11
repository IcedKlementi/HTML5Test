using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSetup : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
