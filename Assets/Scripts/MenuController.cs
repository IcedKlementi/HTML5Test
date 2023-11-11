using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject menuItemPrefab;
    [SerializeField] private Transform menuParent;

    [FormerlySerializedAs("scrollView")] [SerializeField]
    private GameObject menuView;

    [SerializeField] private GameObject receiptItemPrefab;
    [SerializeField] private Transform receiptParent;
    [SerializeField] private GameObject receiptView;

    [SerializeField] private List<MenuItem> _menuItems;
    private List<GameObject> _menuObjects = new();
    private List<GameObject> _receiptObjects = new();
    private Dictionary<int, Dictionary<int, int>> _tableData = new();

    [Serializable]
    private struct MenuItem
    {
        public int itemId;
        public string itemName;
    }

    private async void Start()
    {
        //_menuItems = await ReadData();
        SpawnMenuObjects();
        ActivateMenu(false);
    }

    private static async Task<List<MenuItem>> ReadData()
    {
        var filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "MenuItems.json");

        string json;
        if (filePath.Contains("://") || filePath.Contains(":///"))
        {
            UnityWebRequest www = new(filePath);
            json = www.result.ToString();
            Debug.Log(json);
        }
        else
        {
            json = await File.ReadAllTextAsync(filePath);
        }

        var menuItems = JsonUtility.FromJson<JsonHelper.ArrayOf<MenuItem>>(json).Array.ToList();
        return menuItems;
    }

    private void SpawnMenuObjects()
    {
        foreach (var item in _menuItems)
        {
            var menuObject = Instantiate(menuItemPrefab, menuParent);
            _menuObjects.Add(menuObject);
            menuObject.GetComponent<MenuItemController>().Setup(item.itemId, item.itemName);
        }
    }

    private void ActivateMenu(bool toActivate = true)
    {
        menuView.SetActive(toActivate);
    }

    private void ActivateMenu([CanBeNull] out Dictionary<int, int> itemQuantities, bool toActivate = false)
    {
        menuView.SetActive(toActivate);
        itemQuantities = null;
        if (toActivate) return;
        itemQuantities = new();
        foreach (var itemController in _menuObjects.Select(item => item.GetComponent<MenuItemController>()))
        {
            itemQuantities.Add(itemController.ItemQuantity().Key, itemController.ItemQuantity().Value);
            itemController.ResetQuantities();
        }
    }

    public void OpenTableOrdering(int tableNumber)
    {
        if (_tableData.ContainsKey(tableNumber))
        {
            ShowReceipt(tableNumber);
            return;
        }

        _tableData.Add(tableNumber, null);
        ActivateMenu();
    }

    public void CloseTableOrdering(bool orderConfirmed)
    {
        ActivateMenu(out var itemQuantities);
        var lastEntry = _tableData.Last();
        if (!orderConfirmed)
        {
            _tableData.Remove(_tableData.Last().Key);
            return;
        }

        _tableData.Remove(_tableData.Last().Key);
        _tableData.Add(lastEntry.Key, itemQuantities);
    }

    private void ShowReceipt(int tableNumber)
    {
        receiptView.SetActive(true);
        _tableData.TryGetValue(tableNumber, out var itemQuantities);
        Debug.Log(itemQuantities);
        if (itemQuantities == null) return;
        foreach (var itemQuantity in itemQuantities)
        {
            if (itemQuantity.Value == 0) continue;
            var receiptObject = Instantiate(receiptItemPrefab, receiptParent);
            _receiptObjects.Add(receiptObject);
            receiptObject.GetComponent<ReceiptItemController>()
                .Setup(_menuItems.Find(x => x.itemId == itemQuantity.Key).itemName, itemQuantity.Value);
        }
    }

    public void CloseReceipt()
    {
        receiptView.SetActive(false);
        foreach (var receiptObject in _receiptObjects)
        {
            Destroy(receiptObject);
        }
    }
}