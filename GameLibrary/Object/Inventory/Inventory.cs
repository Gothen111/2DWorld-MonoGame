#region Using Statements Standard
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Runtime.Serialization;
using GameLibrary.Map.World;
using GameLibrary.Connection;
#endregion

#region Using Statements Class Specific
#endregion

namespace GameLibrary.Object.Inventory
{
    [Serializable()]
    public class Inventory : ISerializable
    {
        private int maxItems;

        public int MaxItems
        {
            get { return maxItems; }
            set { maxItems = value; }
        }

        private List<ItemObject> items;

        public List<ItemObject> Items
        {
            get { return items; }
            set { items = value; }
        }

        private bool inventoryChanged;

        public bool InventoryChanged
        {
            get { return inventoryChanged; }
            set { inventoryChanged = value; }
        }

        public Inventory()
        {
            this.maxItems = 12;
            this.items = new List<ItemObject>();
            this.inventoryChanged = true;
        }

        public Inventory(SerializationInfo info, StreamingContext ctxt)
            :base()
        {
            this.maxItems = (int)info.GetValue("maxItems", typeof(int));
            this.items = (List<ItemObject>)info.GetValue("items", typeof(List<ItemObject>));
            this.inventoryChanged = (bool)info.GetValue("inventoryChanged", typeof(bool));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("maxItems", maxItems, typeof(int));
            info.AddValue("items", items, typeof(List<ItemObject>));
            info.AddValue("inventoryChanged", inventoryChanged, typeof(bool));
        }

        private int getFreePlace()
        {
            if(!this.isInventoryFull())
            {
                List<int> var_FreeSpace = new List<int>();
                for (int i = 0; i < this.maxItems; i++)
                {
                    var_FreeSpace.Add(i);
                }
                foreach (ItemObject var_ItemObject in this.items)
                {
                    var_FreeSpace.Remove(var_ItemObject.PositionInInventory);
                }
                return var_FreeSpace[0]; //First
            }
            return -1;
        }

        public bool addItemObjectToInventory(ItemObject _ItemObject)
        {
            ItemObject var_ItemObject = getItemObjectEqual(_ItemObject);
            if(var_ItemObject!=null)
            {
                if (addItemObjectToItemStack(var_ItemObject, _ItemObject))
                {
                    if (World.world.getObject(_ItemObject.Id) != null)
                    {
                        World.world.removeObjectFromWorld(_ItemObject);
                    }
                    this.inventoryChanged = true;
                    return true;
                }
                else
                {
                    //Nehme Item nicht auf usw .... 
                    return false;
                }
            }
            else
            {
                if (this.isInventoryFull())
                {
                    return false;
                }
                else
                {
                    this.addItemObjectToInventoryAt(_ItemObject, this.getFreePlace());
                    this.inventoryChanged = true;
                    return true;
                }
            }
        }

        public bool addItemObjectToInventoryAt(ItemObject _ItemObject, int _Position)
        {
            if (!this.isInventoryFull())
            {
                _ItemObject.PositionInInventory = _Position;
                this.items.Add(_ItemObject);
                if (World.world.getObject(_ItemObject.Id) != null)
                {
                    World.world.removeObjectFromWorld(_ItemObject);
                }
                return true;
            }
            return false;
        }

        public ItemObject getItemObjectEqual(ItemObject _ItemObject)
        {
            foreach (ItemObject var_ItemObject in this.items)
            {
                if (var_ItemObject.ItemEnum == _ItemObject.ItemEnum)
                {
                    if (var_ItemObject.OnStack + _ItemObject.OnStack < var_ItemObject.StackMax)
                    {
                        return var_ItemObject;
                    }
                }
            }
            return null;
        }

        public bool isInventoryFull()
        {
            if (this.items.Count >= this.maxItems)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool addItemObjectToItemStack(ItemObject _ItemObject, ItemObject _AddItemObject)
        {
            if (_ItemObject.OnStack < _ItemObject.StackMax + _AddItemObject.OnStack)
            {
                _ItemObject.OnStack += _AddItemObject.OnStack;
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool removeItemObjectFromItemStack(ItemObject _ItemObject)
        {
            _ItemObject.OnStack -= 1;
            if (_ItemObject.OnStack <= 0)
            {
                this.items.Remove(_ItemObject);
            }
            return true;
        }

        public void itemDropedInInventory(CreatureObject _InventoryOwner, ItemObject _ItemObject, int _NewPosition)
        {
            if (this.items.Contains(_ItemObject))
            {
                this.changeItemPosition(_InventoryOwner, _ItemObject, _NewPosition);
            }
            else
            {
                //TODO: Kommt wohl von wo anders... anderes Inventar usw..
                //this.addItemObjectToInventory(_ItemObject);
                /*if (_InventoryOwner.Equipment.Contains(_ItemObject))
                {
                    //Sende jeweilige Änderung
                    if (Configuration.Configuration.isHost)
                    {

                    }
                    else
                    {
                        Event.EventList.Add(new Event(new GameLibrary.Connection.Message.CreatureEquipmentToInventoryMessage(_InventoryOwner.Id, _ItemObject.PositionInInventory, _NewPosition), GameMessageImportance.VeryImportant));
                    }
                    //_InventoryOwner.Equipment.Remove((EquipmentObject)_ItemObject);
                    //this.addItemObjectToInventory(_ItemObject);
                }*/
                if (_ItemObject is EquipmentObject)
                {
                    if (_InventoryOwner.Body.containsEquipmentObject((EquipmentObject)_ItemObject))
                    {
                        Configuration.Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.CreatureEquipmentToInventoryMessage(_InventoryOwner.Id, _ItemObject.PositionInInventory, _NewPosition), GameMessageImportance.VeryImportant);
                    }
                    else
                    {
                        //_InventoryOwner enthält das Equip nicht im Body. Kommt woanders her!
                    }
                }
            }
        }

        //Sendet Änderung zum clienten bzw. server.
        public void changeItemPosition(CreatureObject _InventoryOwner, ItemObject _ItemObject, int _NewPosition)
        {
            int var_OldPosition = _ItemObject.PositionInInventory;
            _ItemObject.PositionInInventory = _NewPosition;
            if (var_OldPosition == _NewPosition)
            {

            }
            else
            {
                //Sende jeweilige Änderung
                if (Configuration.Configuration.isHost)
                {

                }
                else
                {
                    Configuration.Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.CreatureInventoryItemPositionChangeMessage(_InventoryOwner.Id, var_OldPosition, _NewPosition), GameMessageImportance.VeryImportant);
                }
            }
        }

        //Dient nur zum wechseln! Ohne Senden! EHER für host
        public void changeItemPosition(CreatureObject _InventoryOwner, int _OldPosition, int _NewPosition)
        {
            ItemObject var_ItemToChange = null;

            foreach (ItemObject var_ItemObject in this.items)
            {
                if (var_ItemObject.PositionInInventory == _OldPosition)
                {
                    var_ItemToChange = var_ItemObject;
                    break;
                }
            }

            if (var_ItemToChange != null)
            {
                if (_NewPosition == -1)
                {
                    this.items.Remove(var_ItemToChange);
                    var_ItemToChange.PositionInInventory = _NewPosition;
                    var_ItemToChange.Position = new Microsoft.Xna.Framework.Vector3(40, 40, 0) + _InventoryOwner.Position;
                    World.world.addObject(var_ItemToChange);
                }
                else
                {
                    //TODO: Gucke ob an __NewPosition kein objekt!
                    var_ItemToChange.PositionInInventory = _NewPosition;
                }
            }
        }

        public void dropItem(CreatureObject _InventoryOwner, ItemObject _ItemObject)
        {
            this.items.Remove(_ItemObject);
            this.InventoryChanged = true;
            if (Configuration.Configuration.isHost)
            {
            }
            else
            {
                Configuration.Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.CreatureInventoryItemPositionChangeMessage(_InventoryOwner.Id, _ItemObject.PositionInInventory, -1), GameMessageImportance.VeryImportant);
            }
        }
    }
}
