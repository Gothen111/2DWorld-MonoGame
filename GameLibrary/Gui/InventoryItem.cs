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
#endregion

namespace GameLibrary.Gui
{
    public class InventoryItem : TextField
    {
        private ItemObject itemObject;

        public ItemObject ItemObject
        {
            get { return itemObject; }
            set { itemObject = value; }
        }

        private Container parent;

        private CreatureObject inventoryOwner;

        public InventoryItem(Rectangle _Bounds, Container _Parent, CreatureObject _InventoryOwner)
            : base(_Bounds)
        {
            this.itemObject = null;
            this.parent = _Parent;
            this.inventoryOwner = _InventoryOwner;
        }

        public override void onDrag(Vector2 _Position)
        {
            this.parent.ZIndex = 100;
            base.onDrag(_Position);
        }

        public override bool onDrop(Vector2 _Position)
        {
            return base.onDrop(_Position);
        }

        public override void release()
        {
            this.parent.Components.Remove(this);
            base.release();
        }
    }
}
