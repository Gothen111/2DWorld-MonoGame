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
    public class Checkbox : Component
    {
        private bool isChecked;

        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; }
        }

        public Checkbox()
            : base()
        {
            this.isChecked = false;
        }

        public Checkbox(Rectangle _Bounds)
            : base(_Bounds)
        {
            this.isChecked = false;
        }

        public override void onClick(MouseEnum mouseButton, Vector2 _MousePosition)
        {
            base.onClick(mouseButton, _MousePosition);
            if (this.isChecked)
            {
                this.isChecked = false;
            }
            else
            {
                this.isChecked = true;
            }
        }
    }
}
