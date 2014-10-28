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
#endregion

namespace GameLibrary.Gui.Menu
{
    public class GameSurface : Container
    {
        Component interfaceComponent;
        Component healthComponent;
        Component manaComponent;

        Button inventoryButton;
        InventoryMenu inventoryMenu;


        //ParticleComponent particleComponent;


        public GameSurface()
            :base()
        {
            this.Bounds = new Rectangle(0, (int)(Setting.Setting.resolutionY * 0.7), Setting.Setting.resolutionX, (int)(Setting.Setting.resolutionY * 0.3));

            this.AllowMultipleFocus = true;

            this.AllowsDropIn = true;

            this.healthComponent = new Healthbar(new Rectangle((int)(this.Bounds.X + this.Bounds.Width * 0.106), (int)(this.Bounds.Y + this.Bounds.Height * 0.18), 187, 188));
            this.healthComponent.BackgroundGraphicPath = "Gui/Menu/GameSurface/Health";
            this.add(this.healthComponent);

            this.manaComponent = new Component(new Rectangle((int)(this.Bounds.X + this.Bounds.Width * 0.662), (int)(this.Bounds.Y + this.Bounds.Height * 0.18), 187, 188));
            this.manaComponent.BackgroundGraphicPath = "Gui/Menu/GameSurface/Mana";
            this.add(this.manaComponent);

            this.interfaceComponent = new Component(new Rectangle((int)(this.Bounds.X + this.Bounds.Width * 0.1), (int)(this.Bounds.Y + this.Bounds.Height * 0.15), 767, 201));
            this.interfaceComponent.BackgroundGraphicPath = "Gui/Menu/GameSurface/Interface";
            this.add(this.interfaceComponent);

            this.inventoryButton = new Button(new Rectangle((int)(this.Bounds.X + this.Bounds.Width * 0.5), (int)(this.Bounds.Y + this.Bounds.Height * 0.3), 25, 25));
            this.inventoryButton.BackgroundGraphicPath = "Gui/Menu/Inventory/InventoryButton";
            this.inventoryButton.Action = inventoryButton_Click;
            this.inventoryButton.IsTextEditAble = false;
            this.add(this.inventoryButton);

            this.inventoryMenu = new InventoryMenu(Configuration.Configuration.networkManager.client.PlayerObject);
            this.inventoryMenu.setIsActive(false);
            this.add(this.inventoryMenu);
        }

        private void inventoryButton_Click()
        {
            if (this.inventoryMenu.IsActive)
            {
                this.inventoryMenu.setIsActive(false);
            }
            else
            {
                this.inventoryMenu.setIsActive(true);
            }
        }

        public override bool componentIsDropedIn(Component _Component)
        {
            base.componentIsDropedIn(_Component);
            if (_Component is InventoryItem)
            {             
                //Iteriere durch alle inventory menus in surface oä....
                /*this.inventoryMenu.InventoryOwner.Inventory.dropItem(this.inventoryMenu.InventoryOwner, ((InventoryItem)_Component).ItemObject);
                ((InventoryItem)_Component).release();
                ((InventoryItem)_Component).ItemObject.PositionInInventory = -1;
                return true;*/
            }
            return false;
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch)
        {
            _SpriteBatch.Begin(SpriteSortMode.Deferred,
                    BlendState.AlphaBlend, null, null, null, null,
                    Camera.Camera.camera.getMatrix());

            if (Camera.Camera.camera.Target != null)
            {
                Map.World.World.world.draw(_GraphicsDevice, _SpriteBatch, Camera.Camera.camera.Target);
            }
            else
            {
                _SpriteBatch.DrawString(GameLibrary.Ressourcen.RessourcenManager.ressourcenManager.Fonts["Arial"], "Dein Charakter ist leider gestorben :(", new Vector2(50, 50), Color.White);
            }

            _SpriteBatch.End();

            _SpriteBatch.Begin();
            base.draw(_GraphicsDevice, _SpriteBatch);
            _SpriteBatch.End();
        }
    }
}
