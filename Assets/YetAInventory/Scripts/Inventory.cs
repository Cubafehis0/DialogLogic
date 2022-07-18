using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace YetAInventory
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField]
        private ItemPackage[] packages;
        public void AddItem(string package,Item item)
        {
            
            foreach (var p in packages)
            {
                if (p.name.Equals(package))
                {
                    p.AddItem(item);
                    return;
                }
            }
        }

        public void AddItem(string package,IEnumerable<Item> items)
        {
            foreach(Item item in items)
            {
                AddItem(package,item);
            }
        }
    }
}

