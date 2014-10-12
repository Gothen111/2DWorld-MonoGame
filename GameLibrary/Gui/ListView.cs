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
	public class ListView : Container
    {
		private Component lastSelected;

		public ListView()
            : base()
        {

        }

		public ListView(Rectangle _Bounds)
            : base(_Bounds)
        {

        }

		//TODO: Lässt sich bestimmt per Strategie machen ;)
		public void addAtTop(Component _Component)
		{
            _Component.Bounds = new Rectangle(this.Bounds.X, this.Bounds.Y, _Component.Bounds.Width, _Component.Bounds.Height);

			foreach (Component var_Component in this.Components)
			{
                var_Component.Bounds = new Rectangle(var_Component.Bounds.X, var_Component.Bounds.Y + _Component.Bounds.Height, var_Component.Bounds.Width, var_Component.Bounds.Height);
			}

			this.add(_Component);
		}

		public void addAtBottom(Component _Component)
		{
			float var_PositionY = this.Bounds.Y;

			foreach (Component var_Component in this.Components)
			{
				if (var_Component.Bounds.Y + var_Component.Bounds.Height >= var_PositionY) 
				{
					var_PositionY = var_Component.Bounds.Y + var_Component.Bounds.Height;
				}
			}

            _Component.Bounds = new Rectangle(this.Bounds.X, (int)var_PositionY, _Component.Bounds.Width, _Component.Bounds.Height);

			this.add(_Component);
		}
			
		public Component getSelectedComponent()
		{
			return this.lastSelected;
		}

		public override void onClick(MouseEnum mouseButton, Vector2 _MousePosition)
		{
			base.onClick(mouseButton, _MousePosition);
            if (this.lastSelected != null)
            {
                this.lastSelected.IsSelected = false;
                this.lastSelected = null;
            }

			//TODO: Texture / Background anpassen!
			foreach (Component var_Component in this.Components)
			{
				// VLL auch mit isPressed ;) da mouse event öfters als einmal abegefeuert wird usw...
                if (_MousePosition.X >= var_Component.Bounds.Left && _MousePosition.X <= var_Component.Bounds.Right && _MousePosition.Y >= var_Component.Bounds.Top && _MousePosition.Y <= var_Component.Bounds.Bottom)
				{
					this.lastSelected = var_Component;
				}
			}
            if (this.lastSelected != null)
                this.lastSelected.IsSelected = true;
		}
    }
}
