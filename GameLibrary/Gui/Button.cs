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
    public class Button : TextField
    {
        private Action action;

        public Action Action
        {
            get { return action; }
            set { action = value; }
        }

        public Button()
            : base()
        {
            this.TextAlign = TextAlign.Center;
			this.BackgroundGraphicPath = "Gui/Button";
            this.IsTextEditAble = false;
        }

        public Button(Rectangle _Bounds)
            : base(_Bounds)
        {
            this.TextAlign = TextAlign.Center;
			this.BackgroundGraphicPath = "Gui/Button";
            this.IsTextEditAble = false;
        }

        public void StartAction()
        {
            if (this.action != null)
            {
                this.action();
            }
        }

        public override void onClick(MouseEnum mouseButton, Vector2 _MousePosition)
        {
            base.onClick(mouseButton, _MousePosition);
            this.StartAction();
        }
    }
}
