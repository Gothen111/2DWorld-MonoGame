#region Using Statements Standard
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Runtime.Serialization;
#endregion

#region Using Statements Class Specific
using Input.Keyboard;
using Input.Mouse;
using Input.Mouse.MouseEnum;
using GameLibrary.Object;
using GameLibrary.Factory.FactoryEnums;
#endregion

namespace GameLibrary.Gui
{
    public class EquipmentField : Container
    {
        private CreatureObject inventoryOwner;

        private int fieldId;

        public int FieldId
        {
            get { return fieldId; }
            set { fieldId = value; }
        }

        private List<ItemEnum> acceptedItemTypes;

        public List<ItemEnum> AcceptedItemTypes
        {
            get { return acceptedItemTypes; }
            set { acceptedItemTypes = value; }
        }

        //Component itemSpace;

        InventoryItem item;

        public EquipmentField(CreatureObject _InventoryOwner, int _FieldId, List<ItemEnum> _AcceptedItemTypes, Rectangle _Bounds)
            :base(_Bounds)
        {
            this.inventoryOwner = _InventoryOwner;

            this.fieldId = _FieldId;

            this.acceptedItemTypes = _AcceptedItemTypes;

            /*this.itemSpace = new Component(new Rectangle(this.Bounds.X, this.Bounds.Y, this.Bounds.Width, this.Bounds.Height));
            this.itemSpace.BackgroundGraphicPath = "Gui/Menu/Inventory/InventoryItemSpace";
            this.add(this.itemSpace);*/

            //this.BackgroundGraphicPath = "Gui/Menu/Inventory/InventoryItemSpace";

            this.item = null;
        }

        public void removeItem()
        {
            this.remove(this.item);
            this.item = null;
            this.clear();
        }

        public void setItem(ItemObject _ItemObject)
        {
            this.item = new InventoryItem(new Rectangle(this.Bounds.X + (int)(this.Bounds.Width - _ItemObject.Size.X) / 2, this.Bounds.Y + (int)(this.Bounds.Height - _ItemObject.Size.Y) / 2, (int)_ItemObject.Size.X, (int)_ItemObject.Size.Y), this, inventoryOwner);
            this.item.BackgroundGraphicPath = _ItemObject.ItemIconGraphicPath;
            this.item.IsTextEditAble = false;
            this.item.Text = _ItemObject.OnStack.ToString();
            this.item.IsDragAndDropAble = true;
            this.item.ItemObject = _ItemObject;
            this.item.ItemObject.PositionInInventory = this.fieldId;

            this.add(this.item);
            //this.inventoryOwner.Inventory.InventoryChanged = true;
            //Event.EventList.Add(new Event(new GameLibrary.Connection.Message.UpdateCreatureInventoryMessage(this.inventoryOwner.Id, this.inventoryOwner.Inventory), GameMessageImportance.VeryImportant));
        }

        private void itemDropedIn(ItemObject _ItemObject)
        {
            this.inventoryOwner.guiSetItemToEquipment(_ItemObject, this.fieldId);
        }

        public override bool componentIsDropedIn(Component _Component)
        {
            base.componentIsDropedIn(_Component);
            if(_Component is InventoryItem)
            {
                //TODO: Überürufe welcher typ :D ob wawffe oder ücstung usw
                if (this.item == null || this.Components.Contains(this.item))
                {
                    if (this.acceptedItemTypes.Contains(((InventoryItem)_Component).ItemObject.ItemEnum))
                    {
                        this.itemDropedIn(((InventoryItem)_Component).ItemObject);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
