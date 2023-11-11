using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReceiptItemController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemQuantity;

    public void Setup(string itemName, int itemQuantity)
    {
        this.itemName.text = itemName;
        this.itemQuantity.text = itemQuantity.ToString();
    }
}
