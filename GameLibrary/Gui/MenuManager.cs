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
using GameLibrary.Gui.Menu;
#endregion

namespace GameLibrary.Gui
{
    public class MenuManager
    {
        public static MenuManager menuManager = new MenuManager();

        private Container activeContainer;

        public Container ActiveContainer
        {
            get { return activeContainer; }
            set { activeContainer = value; }
        }

        private MenuManager()
        {
            this.setMenu(new StartMenu());
        }

        public void setMenu(Container _Menu)
        {
            if(this.activeContainer!=null)
            {
                this.activeContainer.release();
            }
            this.activeContainer = _Menu;
        }
    }
}
