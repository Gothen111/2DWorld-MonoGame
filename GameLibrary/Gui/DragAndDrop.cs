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
#endregion

namespace GameLibrary.Gui
{
    public class DragAndDrop : Component
    {
        private bool isDragAndDropAble;

        public bool IsDragAndDropAble
        {
            get { return isDragAndDropAble; }
            set { isDragAndDropAble = value; }
        }

        private bool isDraged;

        public bool IsDraged
        {
            get { return isDraged; }
        }

        public DragAndDrop()
            : base()
        {
            this.isDragAndDropAble = false;
            this.isDraged = false;
        }

        public DragAndDrop(Rectangle _Bounds)
            : base(_Bounds)
        {
            this.isDragAndDropAble = false;
            this.isDraged = false;
        }

        public virtual void onDrag(Vector2 _Position)
        {
        }

        public virtual bool onDrop(Vector2 _Position)
        {
            if(MenuManager.menuManager.ActiveContainer!=null)
            {
                Component var_Menu = MenuManager.menuManager.ActiveContainer;
                Component var_TopComponent = var_Menu.getTopComponent(_Position);
                return var_TopComponent.componentIsDropedIn(this);
            }
            return false;
        }

        public override void onClick(MouseEnum mouseButton, Vector2 _MousePosition)
        {
            base.onClick(mouseButton, _MousePosition);
            if (this.isDragAndDropAble && mouseButton.Equals(Input.Mouse.MouseEnum.MouseEnum.Left))
            {
                if (!this.isDraged)
                {
                    this.onDrag(_MousePosition);
                    this.isDraged = true;                   
                }
                else
                {
                    if (this.onDrop(_MousePosition))
                    {
                        this.isDraged = false;
                    }
                }
            }
        }

        /*
        public override bool mouseClicked(UserInterface.MouseEnum.MouseEnum mouseButtonClicked, Microsoft.Xna.Framework.Vector2 position)
        {
            if (base.mouseClicked(mouseButtonClicked, position))
            {
                if (this.isDragAndDropAble)
                {
                    if (!this.isDraged)
                    {
                        this.isDraged = true;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool mouseReleased(UserInterface.MouseEnum.MouseEnum mouseButtonReleased, Microsoft.Xna.Framework.Vector2 position)
        {
            if(base.mouseReleased(mouseButtonReleased, position))
            {
                if (this.isDragAndDropAble)
                {
                    if (this.isDraged)
                    {
                        this.isDraged = false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }*/

        public override void mouseMoved(Vector2 position)
        {
            base.mouseMoved(position);
            if (this.isDragAndDropAble)
            {
                if (this.isDraged)
                {
                    this.Bounds = new Rectangle((int)position.X, (int)position.Y, this.Bounds.Width, this.Bounds.Height);
                }
            }
        }
    }
}
