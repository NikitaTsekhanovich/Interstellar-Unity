using System.Collections.Generic;
using MainMenu.Store.Items;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.Store
{
    public class StoreController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currentCoinsText;
        [SerializeField] private List<Item> _items = new();
        [SerializeField] private TMP_Text _priceScissors;
        [SerializeField] private TMP_Text _pricePencil;
        [SerializeField] private TMP_Text _priceKey;
        [SerializeField] private Button _scissorsButton;
        [SerializeField] private Button _pencilButton;
        [SerializeField] private Button _keyButton;

        private int _currentCoins;

        public void StartStore()
        {
            UpdateCoinsText();
            LoadStoreItems();
        }

        private void UpdateCoinsText()
        {
            _currentCoins = PlayerPrefs.GetInt(PlayerDataKeys.CoinsDataKey);
            _currentCoinsText.text = $"{_currentCoins}";
        }

        private void LoadStoreItems()
        {
            foreach (var item in _items)
            {
                switch (item.ItemType)
                {
                    case ItemType.Scissors:
                        _priceScissors.text = $"{item.Price}";
                        CheckPurchaseOpportunity(item.Price, _scissorsButton);
                        break;
                    case ItemType.Pencil:
                        _pricePencil.text = $"{item.Price}";
                        CheckPurchaseOpportunity(item.Price, _pencilButton);
                        break;
                    case ItemType.Key:
                        _priceKey.text = $"{item.Price}";
                        CheckPurchaseOpportunity(item.Price, _keyButton);
                        break;
                }
            }
        }

        private void CheckPurchaseOpportunity(int itemPrice, Button button)
        {
            if (itemPrice > _currentCoins)
                button.interactable = false;
            else 
                button.interactable = true;
        }

        public void BuyScissors()
        {
            BuyItem(ItemType.Scissors);
        }

        public void BuyPencil()
        {
            BuyItem(ItemType.Pencil);
        }

        public void BuyKey()
        {
            BuyItem(ItemType.Key);
        }

        private void BuyItem(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Scissors:
                    SetBuy(_items[0].Price, PlayerDataKeys.ScissorsDataKey);
                    break;
                case ItemType.Pencil:
                    SetBuy(_items[1].Price, PlayerDataKeys.PencilsDataKey);
                    break;
                case ItemType.Key:
                    SetBuy(_items[2].Price, PlayerDataKeys.KeysDataKey);
                    break;
            }
            CheckPurchaseOpportunity(_items[0].Price, _scissorsButton);
            CheckPurchaseOpportunity(_items[1].Price, _pencilButton);
            CheckPurchaseOpportunity(_items[2].Price, _keyButton);
            UpdateCoinsText();
        }

        private void SetBuy(int itemPrice, string keyData)
        {
            _currentCoins -= itemPrice;
            PlayerPrefs.SetInt(PlayerDataKeys.CoinsDataKey, _currentCoins);
            var countItem = PlayerPrefs.GetInt(keyData);
            PlayerPrefs.SetInt(keyData, countItem + 1);
        }
    }
}

