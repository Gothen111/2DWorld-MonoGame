using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using GameLibrary.Gui;

namespace GameLibrary.Gui.Menu
{
	public class ConnectToServerMenu : Container
    {
        TextField serverIPTextField;
        TextField serverPortTextField;
        Button connectServerButton;

		public ConnectToServerMenu()
            :base()
        {
            this.Bounds = new Rectangle(0,0,1000,1000); // TODO: Größe an Bildschirm anpassen!
            this.BackgroundGraphicPath = "Gui/Menu/CharacterCreation/Background";

            this.AllowMultipleFocus = true;

            this.serverIPTextField = new TextField(new Rectangle(200, 100, 289, 85));
            this.serverIPTextField.Text = "127.0.0.1";
            this.add(this.serverIPTextField);
            this.serverPortTextField = new TextField(new Rectangle(200, 200, 289, 85));
            this.serverPortTextField.Text = "14242";
            this.add(this.serverPortTextField);
            this.connectServerButton = new Button(new Rectangle(200, 300, 289, 85));
            this.connectServerButton.Text = "Connect";
            this.add(this.connectServerButton);
            this.connectServerButton.Action = connectToServer;
        }

        public void connectToServer()
        {
            Configuration.Configuration.networkManager.Start(this.serverIPTextField.Text, this.serverPortTextField.Text);
            MenuManager.menuManager.setMenu(new LoadingMenu());
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch)
        {
            _SpriteBatch.Begin();
            base.draw(_GraphicsDevice, _SpriteBatch);
            _SpriteBatch.End();
        }
    }
}
