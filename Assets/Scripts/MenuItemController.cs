using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuItemController : MonoBehaviour
{
    private Action _onValueChanged;

    private int _menuItemId;

    [SerializeField] private TextMeshProUGUI itemNameText;

    [SerializeField] private TextMeshProUGUI quantityText;
    private int _quantity = 0;


    private void Start()
    {
        _onValueChanged += () => { quantityText.text = _quantity.ToString(); };
        _onValueChanged?.Invoke();
    }

    public void Setup(int itemId, string itemName)
    {
        this._menuItemId = itemId;
        itemNameText.text = itemName;
    }

    public void Decrement()
    {
        _quantity = Mathf.Clamp(--_quantity, 0, 10);
        _onValueChanged?.Invoke();
    }

    public void Increment()
    {
        _quantity = Mathf.Clamp(++_quantity, 0, 10);
        _onValueChanged?.Invoke();
    }

    public void ResetQuantities()
    {
        _quantity = 0;
        _onValueChanged?.Invoke();
    }

    public KeyValuePair<int, int> ItemQuantity()
    {
        return new KeyValuePair<int, int>(_menuItemId, _quantity);
    }
}