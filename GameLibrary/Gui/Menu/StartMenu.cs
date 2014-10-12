using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using GameLibrary.Gui;

namespace GameLibrary.Gui.Menu
{
    public class StartMenu : Container
    {
		Button characterMenuButton;

        public StartMenu()
            :base()
        {
            this.Bounds = new Rectangle(0,0,1000,1000); // TODO: Größe an Bildschirm anpassen!
            this.BackgroundGraphicPath = "Gui/Background";

            this.AllowMultipleFocus = true;

			this.characterMenuButton = new Button(new Rectangle(200, 300, 289, 85));
			this.characterMenuButton.Text = "Character Menu";
			this.add(this.characterMenuButton);
			this.characterMenuButton.Action = openCharacterMenu;
        }

		public void openCharacterMenu()
        {
            MenuManager.menuManager.setMenu(new CharacterMenu());
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch)
        {
            _SpriteBatch.Begin();
            base.draw(_GraphicsDevice, _SpriteBatch);
            _SpriteBatch.End();
        }
    }
}
