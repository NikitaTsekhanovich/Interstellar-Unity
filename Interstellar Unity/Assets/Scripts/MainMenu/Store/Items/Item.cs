using UnityEngine;

namespace MainMenu.Store.Items
{
    [CreateAssetMenu(fileName ="Item", menuName ="StoreItem/ Item")]
    public class Item : ScriptableObject
    {
        [SerializeField] private int _price;
        [SerializeField] private ItemType _itemType;

        public int Price => _price;
        public ItemType ItemType => _itemType;
    }
}

