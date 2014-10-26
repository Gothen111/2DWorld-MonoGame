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
using System.IO;
using System.Reflection;
#endregion

namespace GameLibrary.Gui.Menu
{
	public class CharacterMenu : Container
    {
		ListView charactersListView;
		Button createNewCharacterButton;

        Button singlePlayerButton;
        Button multiPlayerButton;
		Button connectToServerButton;

        List<PlayerObject> charactersList;

		public CharacterMenu()
            :base()
        {
            this.Bounds = new Rectangle(0,0,1000,1000); // TODO: Größe an Bildschirm anpassen!
            this.BackgroundGraphicPath = "Gui/Menu/CharacterCreation/Background";

            this.AllowMultipleFocus = true;

			this.charactersListView = new ListView (new Rectangle (500, 0, 289, 600));
			this.add(this.charactersListView);
			this.fillCharactersListView();

			this.createNewCharacterButton = new Button(new Rectangle(200, 200, 289, 85));
			this.createNewCharacterButton.Text = "Create Character";
			this.add(this.createNewCharacterButton);
			this.createNewCharacterButton.Action = openCreateCharacterMenu;

            this.singlePlayerButton = new Button(new Rectangle(10, 300, 289, 85));
            this.singlePlayerButton.Text = "Singleplayer";
            this.add(this.singlePlayerButton);
            this.singlePlayerButton.Action = startSinglePlayer;

            this.multiPlayerButton = new Button(new Rectangle(300, 300, 289, 85));
            this.multiPlayerButton.Text = "Host Game";
            this.add(this.multiPlayerButton);
            this.multiPlayerButton.Action = startMulitPlayer;

			this.connectToServerButton = new Button(new Rectangle(590, 300, 289, 85));
			this.connectToServerButton.Text = "Connect";
			this.add(this.connectToServerButton);
			this.connectToServerButton.Action = openConnectToServerMenu;
		}

        //TODO: Lade Charactere aus Datei oä. und füge sie der Liste hinzu.
        private void loadCharactersFromFile(List<PlayerObject> _CharactersList)
        {
            String var_Path = "Save/Characters/";

            if (!Directory.Exists(var_Path))
            {
                Directory.CreateDirectory(var_Path);
            }

            String[] var_Names = Directory.GetFiles(var_Path);

            for (int i = 0; i < var_Names.Length; i++)
            {
                try
                {
                    PlayerObject var_PlayerObject = (PlayerObject)Utility.IO.IOManager.LoadISerializeAbleObjectFromFile(var_Names[i]);//Utility.Serializer.DeSerializeObject(var_Names[i]);
                    _CharactersList.Add(var_PlayerObject);
                }
                catch (Exception e)
                {
                    //TODO: Soll veraltete Player-Datei gelöscht werden oder konvertiert, o.ä.?!
                    File.Delete(var_Names[i]);
                }
            }
        }

        private void createTextFieldFromCharacter(ListView _CharactersListView, PlayerObject _PlayerObject)
        {
            TextField var_TextField1 = new TextField(new Rectangle(0, 0, 289, 85));
            var_TextField1.IsTextEditAble = false;
            var_TextField1.Text = _PlayerObject.Name;
            _CharactersListView.addAtBottom(var_TextField1);
        }

        private void createTextFields(ListView _CharactersListView, List<PlayerObject> _CharactersList)
        {
            foreach (PlayerObject var_PlayerObject in _CharactersList)
            {
                this.createTextFieldFromCharacter(_CharactersListView, var_PlayerObject);
            }
        }
	
		private void fillCharactersListView()
		{
            this.charactersList = new List<PlayerObject>();

            this.loadCharactersFromFile(this.charactersList);

            this.createTextFields(this.charactersListView, this.charactersList);
		}

		private void openCreateCharacterMenu()
		{
            MenuManager.menuManager.setMenu(new CharacterCreationMenu());
		}

		private bool characterHasBeenChoosen()
		{
			return this.charactersListView.getSelectedComponent() != null ? true : false;
		}

        private PlayerObject getPlayerObjectFromCharactersListView()
        {
            foreach (PlayerObject var_PlayerObject in this.charactersList)
            {
                if (((TextField)this.charactersListView.getSelectedComponent()).Text.Equals(var_PlayerObject.Name))
                {
                    return var_PlayerObject;
                }
            }
            return null;
        }

        private void startSinglePlayer()
        {
            if (this.characterHasBeenChoosen())
            {
                Configuration.Configuration.gameManager.startSinglePlayerGame();
                PlayerObject var_PlayerObject = this.getPlayerObjectFromCharactersListView();
                Configuration.Configuration.networkManager.client.PlayerObject = var_PlayerObject;

                GameLibrary.Map.World.World.world.addPlayerObject(var_PlayerObject);
                GameLibrary.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Player.InputAction(new List<Keys>() { Keys.W }, new GameLibrary.Commands.CommandTypes.WalkUpCommand(var_PlayerObject)));
                GameLibrary.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Player.InputAction(new List<Keys>() { Keys.S }, new GameLibrary.Commands.CommandTypes.WalkDownCommand(var_PlayerObject)));
                GameLibrary.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Player.InputAction(new List<Keys>() { Keys.A }, new GameLibrary.Commands.CommandTypes.WalkLeftCommand(var_PlayerObject)));
                GameLibrary.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Player.InputAction(new List<Keys>() { Keys.D }, new GameLibrary.Commands.CommandTypes.WalkRightCommand(var_PlayerObject)));
                GameLibrary.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Player.InputAction(new List<Keys>() { Keys.Space }, new GameLibrary.Commands.CommandTypes.AttackCommand(var_PlayerObject)));

                GameLibrary.Camera.Camera.camera.setTarget(var_PlayerObject);     

                MenuManager.menuManager.setMenu(new GameSurface());
            }
        }

        private void startMulitPlayer()
        {
            /*if (this.characterHasBeenChoosen())
            {
                Configuration.Configuration.gameManager.startMultiPlayerGame();
                PlayerObject var_PlayerObject = this.getPlayerObjectFromCharactersListView();
                Configuration.Configuration.networkManager.client.PlayerObject = var_PlayerObject;

                GameLibrary.Map.World.World.world.addPlayerObject(var_PlayerObject);
                GameLibrary.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Player.InputAction(new List<Keys>() { Keys.W }, new GameLibrary.Commands.CommandTypes.WalkUpCommand(var_PlayerObject)));
                GameLibrary.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Player.InputAction(new List<Keys>() { Keys.S }, new GameLibrary.Commands.CommandTypes.WalkDownCommand(var_PlayerObject)));
                GameLibrary.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Player.InputAction(new List<Keys>() { Keys.A }, new GameLibrary.Commands.CommandTypes.WalkLeftCommand(var_PlayerObject)));
                GameLibrary.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Player.InputAction(new List<Keys>() { Keys.D }, new GameLibrary.Commands.CommandTypes.WalkRightCommand(var_PlayerObject)));
                GameLibrary.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Player.InputAction(new List<Keys>() { Keys.Space }, new GameLibrary.Commands.CommandTypes.AttackCommand(var_PlayerObject)));

                GameLibrary.Camera.Camera.camera.setTarget(var_PlayerObject);

                MenuManager.menuManager.setMenu(new GameSurface());
            }*/
        }

		private void openConnectToServerMenu()
		{
			if (this.characterHasBeenChoosen()) 
			{
                Configuration.Configuration.gameManager.connectToServer();
                Configuration.Configuration.networkManager.client.PlayerObject = this.getPlayerObjectFromCharactersListView();
                MenuManager.menuManager.setMenu(new ConnectToServerMenu());
			}
		}

        public override void draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch)
        {
            _SpriteBatch.Begin();
            base.draw(_GraphicsDevice, _SpriteBatch);
            _SpriteBatch.End();
        }
    }
}
