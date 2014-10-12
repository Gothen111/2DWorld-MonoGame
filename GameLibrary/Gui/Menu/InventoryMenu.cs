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
using GameLibrary.Object;
using GameLibrary.Object.Body;
#endregion

namespace GameLibrary.Gui.Menu
{
    public class InventoryMenu : Container
    {
        private CreatureObject inventoryOwner;

        public CreatureObject InventoryOwner
        {
            get { return inventoryOwner; }
            set { inventoryOwner = value; }
        }

        Container equipmentContainer;

        Container itemContainer;

        public InventoryMenu(CreatureObject _InventoryOwner)
            :base()
        {
            this.inventoryOwner = _InventoryOwner;

            this.Bounds = new Rectangle(475, 0, 700, 1000); // TODO: Größe an Bildschirm anpassen!

            this.AllowMultipleFocus = true;

            this.BackgroundGraphicPath = "Gui/Menu/Inventory/InventoryMenu";

            this.equipmentContainer = new Container(this.Bounds);

            int var_Count = this.inventoryOwner.Body.BodyParts.Count;
            for (int y = 0; y < var_Count; y++)
            {
                Component var_InventoryItemSpace = new Component(new Rectangle(this.Bounds.X, this.Bounds.Y + y * 36, 36, 36));
                var_InventoryItemSpace.BackgroundGraphicPath = "Gui/Menu/Inventory/InventoryItemSpace";
                this.add(var_InventoryItemSpace);
            }


            for (int y = 0; y < var_Count; y++)
            {
                EquipmentField var_EquipmentField = new EquipmentField(this.inventoryOwner, this.inventoryOwner.Body.BodyParts[y].Id, this.inventoryOwner.Body.BodyParts[y].AcceptedItemTypes, new Rectangle(this.Bounds.X, this.Bounds.Y + y * 36, 36, 36));
                this.equipmentContainer.add(var_EquipmentField);
            }


            this.itemContainer = new Container(new Rectangle(this.Bounds.X, this.Bounds.Y + 300, this.Bounds.Width, this.Bounds.Height));

            int var_BackbackSize = this.inventoryOwner.Inventory.MaxItems;

            int var_SizeY = var_BackbackSize / 4 + var_BackbackSize % 4;

            for (int y = 0; y < var_SizeY; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    int var_ItemId = y * 4 + x;
                    if (var_BackbackSize > 0)
                    {
                        InventoryField var_InventoryField = new InventoryField(this.inventoryOwner, var_ItemId, new Rectangle(this.Bounds.X + 92 + 36 * x, this.Bounds.Y + 306 + y * 36, 36, 36));
                        this.itemContainer.add(var_InventoryField);

                        var_BackbackSize -= 1;
                    }
                }
            }
           
            this.checkItems();
            this.add(this.equipmentContainer);
            this.add(this.itemContainer);         
        }

        public void checkItems()
        {
            this.checkInventoryItems();
            this.checkEquipmentItems();
            //TODO: Das gefällt mir ganz und gar nicht mit dem InventoryChanged = false; hier. es muss noch ne exta varialbe hier geben und das chang ein creatue geupdated ...
            this.inventoryOwner.Inventory.InventoryChanged = false;         
        }

        private void checkInventoryItems()
        {
            if (this.inventoryOwner.Inventory.InventoryChanged)
            {
                foreach (InventoryField var_InventoryField in this.itemContainer.Components)
                {
                    var_InventoryField.removeItem();
                }

                foreach(ItemObject var_ItemObject in this.inventoryOwner.Inventory.Items)
                {
                    foreach (InventoryField var_InventoryField in this.itemContainer.Components)
                    {
                        if (var_ItemObject.PositionInInventory == var_InventoryField.FieldId)
                        {
                            var_InventoryField.setItem(var_ItemObject);
                            break;
                        }
                    }
                }
                foreach (InventoryField var_InventoryField in this.itemContainer.Components)
                {
                    //Console.WriteLine(var_InventoryField.FieldId + " : " + var_InventoryField.Components.Count);
                }

            }
        }

        private void checkEquipmentItems()
        {
            // Nur für n spieler :D und gehen wir aus das erst mal spieler nur human sein kann
            if (this.inventoryOwner.Inventory.InventoryChanged)
            {
                foreach (EquipmentField var_EquipmentField in this.equipmentContainer.Components)
                {
                    foreach (BodyPart var_BodyPart in this.inventoryOwner.Body.BodyParts)
                    {
                        if (var_EquipmentField.FieldId == var_BodyPart.Id)
                        {
                            var_EquipmentField.removeItem();
                            if (var_BodyPart.Equipment != null)
                            {
                                var_EquipmentField.setItem(var_BodyPart.Equipment);
                            }
                        }
                    }
                }
            }
        }
    }
}
