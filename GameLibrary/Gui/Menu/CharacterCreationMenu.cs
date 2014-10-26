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
using Input.Mouse.MouseEnum;
using System.IO;
#endregion

namespace GameLibrary.Gui.Menu
{
    public class CharacterCreationMenu : Container
    {
		TextField playerNameTextField;
		Button createCharacterButton;
        Component plattformComponent;
        Component characterComponent;

        Container bodyColorPicker;

        Button maleButton;
        Button femaleButton;

        PlayerObject playerObject;

        public CharacterCreationMenu()
            :base()
        {
            this.Bounds = new Rectangle(0,0,1000,1000); // TODO: Größe an Bildschirm anpassen!
            this.BackgroundGraphicPath = "Gui/Menu/CharacterCreation/Background";

            this.AllowMultipleFocus = true;

            this.plattformComponent = new Component(new Rectangle(290, 200, 230, 70));
            this.plattformComponent.BackgroundGraphicPath = "Gui/Menu/CharacterCreation/Plattform";
            this.add(this.plattformComponent);

            this.createCharacter(Factory.FactoryEnums.GenderEnum.Male);

            /*this.characterComponent = new Component(new Rectangle(350, 100, 96, 128));//new Component(new Rectangle(320, 50, 170, 190));
            this.characterComponent.BackgroundGraphicPath = this.playerObject.Body.MainBody.TexturePath;//"Character/BodyMale";//"Character/Char1_Big";
            int var_SizeX = (int)this.playerObject.Body.MainBody.Size.X;
            int var_SizeY = (int)this.playerObject.Body.MainBody.Size.Y;
            this.characterComponent.SourceRectangle = new Rectangle(var_SizeX, 0, var_SizeX, var_SizeY);
            this.add(this.characterComponent);*/

            this.bodyColorPicker = new Container(new Rectangle(530, 50, 300, 300));
            this.createColors();
            this.add(this.bodyColorPicker);

            this.maleButton = new Button(new Rectangle(0, 50, 60, 60));
            //this.maleButton.Text = "Male";
            this.maleButton.BackgroundGraphicPath = "Gui/Menu/CharacterCreation/MaleSymbol";
            this.maleButton.Scale = 0.5f;
            this.maleButton.Action = this.selectedMale;
            this.add(this.maleButton);

            this.femaleButton = new Button(new Rectangle(60, 50, 60, 60));
            //this.femaleButton.Text = "Female";
            this.femaleButton.BackgroundGraphicPath = "Gui/Menu/CharacterCreation/FemaleSymbol";
            this.femaleButton.Scale = 0.4f;
            this.femaleButton.Action = this.selectedFemale;
            this.add(this.femaleButton);

			this.playerNameTextField = new TextField(new Rectangle(260, 280, 289, 85));
			this.playerNameTextField.Text = "Name";
			this.add(this.playerNameTextField);

			this.createCharacterButton = new Button(new Rectangle(260, 380, 289, 85));
			this.createCharacterButton.Text = "Accept";
			this.add(this.createCharacterButton);
            this.createCharacterButton.Action = acceptCharacter;
        }

        private void createColors()
        {
            /*int y = 0;
            for (int x = 0; x < 10; x++)
            {
                for (; y < 5; y++)
                {
                    Component var_ColorComponent = new Component(new Rectangle(this.bodyColorPicker.Bounds.X + x * 16 + (y%2)*8, this.bodyColorPicker.Bounds.Y + y * 13, 16, 17));
                    var_ColorComponent.BackgroundGraphicPath = "Gui/Menu/CharacterCreation/ColorField";
                    var_ColorComponent.ComponentColor = new Color(y * 25, x * 25, x * 25);
                    this.bodyColorPicker.add(var_ColorComponent);
                }
                for (; y >=5 && y < 10; y++)
                {
                    Component var_ColorComponent = new Component(new Rectangle(this.bodyColorPicker.Bounds.X + x * 16 + (y % 2) * 8, this.bodyColorPicker.Bounds.Y + y * 13, 16, 17));
                    var_ColorComponent.BackgroundGraphicPath = "Gui/Menu/CharacterCreation/ColorField";
                    var_ColorComponent.ComponentColor = new Color(255 - y * 10, 255 - x * 25, x * 25);
                    this.bodyColorPicker.add(var_ColorComponent);
                }
                y = 0;
            }*/
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < y; x++)
                {
                    Component var_ColorComponent = new Component(new Rectangle(this.bodyColorPicker.Bounds.X + x * 16 + (y % 2) * 8, this.bodyColorPicker.Bounds.Y + y * 13, 16, 17));
                    var_ColorComponent.BackgroundGraphicPath = "Gui/Menu/CharacterCreation/ColorField";
                    var_ColorComponent.ComponentColor = new Color(255 - (y * 25 + x * 25) / 2, y * 25 - x * 25, (y * 25 + x * 25) / 2);
                    //var_ColorComponent.ComponentColor = new Color((125-x*12, y * 25 - x * 25, (y * 25 + x * 25) / 2);
                    //Console.Write(var_ColorComponent.ComponentColor.B + " | ");
                    this.bodyColorPicker.add(var_ColorComponent);
                }
                Console.WriteLine();
            }
        }

        public override void mouseMoved(Vector2 position)
        {
            base.mouseMoved(position);
            if (this.bodyColorPicker.IsInBounds(position))
            {
                Component var_Top = this.bodyColorPicker.getTopComponent(position);
                if (var_Top != this.bodyColorPicker)
                {
                    //this.characterComponent.ComponentColor = var_Top.ComponentColor;
                }
            }
            else
            {
                //this.characterComponent.ComponentColor = this.playerObject.ObjectDrawColor;
            }
        }

        private void createCharacter(Factory.FactoryEnums.GenderEnum _GenderEnum)
        {
            this.playerObject = Factory.CreatureFactory.creatureFactory.createPlayerObject(Factory.FactoryEnums.RaceEnum.Human, Factory.FactoryEnums.FactionEnum.Beerdrinker, Factory.FactoryEnums.CreatureEnum.Commandant, _GenderEnum);
            this.playerObject.Body.stopWalk();
            this.playerObject.Position = new Vector3(400, 220, 0);
        }

        public override void onClick(MouseEnum mouseButton, Vector2 _MousePosition)
        {
            base.onClick(mouseButton, _MousePosition);
            if (mouseButton == MouseEnum.Left)
            {
                if (this.bodyColorPicker.IsInBounds(_MousePosition))
                {
                    Component var_Top = this.bodyColorPicker.getTopComponent(_MousePosition);
                    //this.characterComponent.ComponentColor = var_Top.ComponentColor;
                    this.playerObject.Body.setColor(var_Top.ComponentColor);
                }
            }
        }

        private void selectedMale()
        {
            this.createCharacter(Factory.FactoryEnums.GenderEnum.Male);
            //this.characterComponent.BackgroundGraphicPath = this.playerObject.GraphicPath;
        }
        private void selectedFemale()
        {
            this.createCharacter(Factory.FactoryEnums.GenderEnum.Female);
            //this.characterComponent.BackgroundGraphicPath = this.playerObject.GraphicPath;
        }

        private void openCharacterMenu()
        {
            MenuManager.menuManager.setMenu(new CharacterMenu());
        }

        private void acceptCharacter()
        {
            bool var_CreationProblem = false;

            String var_Path = "Save/Characters/" + this.playerNameTextField.Text + ".sav";

            if (File.Exists(var_Path))
            {
                var_CreationProblem = true;
            }
            if(!var_CreationProblem)
            {
                this.playerObject.Name = this.playerNameTextField.Text;
                Utility.IO.IOManager.SaveISerializeAbleObjectToFile(var_Path, this.playerObject);
                this.openCharacterMenu();
            }
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch)
        {
            _SpriteBatch.Begin();
            base.draw(_GraphicsDevice, _SpriteBatch);
            this.playerObject.draw(_GraphicsDevice, _SpriteBatch, Vector3.Zero, Color.White);
            _SpriteBatch.End();
        }
    }
}
