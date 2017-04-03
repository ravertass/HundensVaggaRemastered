﻿using Microsoft.Xna.Framework.Content;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    internal class Items {
        private readonly Dictionary<string, Item> itemsMap = new Dictionary<string, Item>();

        // It's a little stupid that this class contains the inventory, but it makes things easier
        private readonly Inventory inventory;
        public Inventory Inventory {
            get { return inventory; }
        }

        public Items(string path, ContentManager content, Inventory inventory) {
            List<ItemJson> itemsJson = DeserializeItemsJson(path);
            AddRoomsFromJsonToMap(content, itemsJson);
            this.inventory = inventory;
        }

        private static List<ItemJson> DeserializeItemsJson(string path) {
            string jsonString = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<ItemJson>>(jsonString);
        }

        private void AddRoomsFromJsonToMap(ContentManager content, List<ItemJson> itemsJson) {
            StateOfTheWorld worldState = new StateOfTheWorld();
            foreach (ItemJson itemJson in itemsJson) {
                itemsMap.Add(itemJson.Name, itemJson.GetItemInstance(content));
            }
        }

        public Item GetItem(string itemName) {
            return itemsMap[itemName];
        }

    }
}