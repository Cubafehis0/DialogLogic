using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YetAInventory
{
    public class ItemPackage :MonoBehaviour, IEnumerable<Item>
    {
        private List<Item> items=new List<Item>();


        public void AddItem(Item item)
        {

        }
        public IEnumerator<Item> GetEnumerator()
        {
            return items.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}

