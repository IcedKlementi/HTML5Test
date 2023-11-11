using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    private int _counter;
    public Action OnValueChanged;

    [SerializeField] private TextMeshProUGUI _counterText;

    private void Start()
    {
        OnValueChanged += () => { _counterText.text = _counter.ToString(); };
        _counter = PlayerPrefs.GetInt("counter");
        OnValueChanged?.Invoke();
    }
    
    public void Increment()
    {
        PlayerPrefs.SetInt("counter", ++_counter);
        OnValueChanged?.Invoke();
    }

    public void Reset()
    {
        _counter = 0;
        PlayerPrefs.SetInt("counter", _counter);
        OnValueChanged?.Invoke();
    }
}