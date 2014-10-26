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
    public class Container : DragAndDrop
    {
        private List<Component> components;

        public List<Component> Components
        {
            get { return components; }
            set { components = value; }
        }

        private ContainerStrategy.Strategy strategy;

		public ContainerStrategy.Strategy Strategy
        {
            get { return strategy; }
            set { strategy = value; }
        }

        public Container() : base()
        {
            components = new List<Component>();
        }

        public Container(Rectangle _Bounds) : base(_Bounds)
        {
            components = new List<Component>();
        }

		public virtual void add(Component _Component)
        {
            if (this.strategy != null && !this.strategy.checkComponent(_Component))
            {
                Logger.Logger.LogInfo("Strategy " + this.strategy.ToString() + " verhindert das Hinzufügen von " + _Component.ToString());
                return;
            }
            this.components.Add(_Component);
        }

		public virtual void remove(Component _Component)
        {
            if(this.components.Contains(_Component))
                this.components.Remove(_Component);
        }

        public override void release()
        {
            base.release();
            foreach (Component var_Component in this.components)
            {
                var_Component.release();
            }
        }

        public void clear()
        {
            this.components = new List<Component>();
        }

        public void setIsActive(bool _IsActive)
        {
            this.IsActive = _IsActive;
            foreach (Component var_Component in this.components)
            {
                var_Component.IsActive = _IsActive;
            }
        }

        //Basiert darauf das Objekte die zuletzt drauf kommen weiter oben sind. In der Ansicht!
        //TODO: Iteriere vll rückwärts ;) Also top down statt bottom up
        public override Component getTopComponent(Vector2 _Position)
        {
            Component var_Result = null;
            foreach (Component var_Component in this.components)
            {
                if (var_Component is DragAndDrop)
                {
                    if (!((DragAndDrop)var_Component).IsDraged)
                    {
                        if (var_Component.IsActive)
                        {
                            if (var_Component.IsInBounds(_Position))
                            {
                                var_Result = var_Component;
                            }
                        }
                    }
                }
                else
                {
                    if (var_Component.IsActive)
                    {
                        if (var_Component.IsInBounds(_Position))
                        {
                            var_Result = var_Component;
                        }
                    }
                }
            }
            if (var_Result != null)
            {
                return var_Result.getTopComponent(_Position);
            }
            return this;
        }

        public override void update()
        {
            base.update();
            foreach (Component var_Component in this.components)
            {
                var_Component.update();
            }
        }

        public override void draw(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch)
        {
            base.draw(_GraphicsDevice, _SpriteBatch);

            if (this.IsActive && this.IsVisible)
            {
                //if (this.BackgroundGraphicPath != null && !this.BackgroundGraphicPath.Equals(""))
                //{
                    //_SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.BackgroundGraphicPath], new Vector2(this.Bounds.X, this.Bounds.Y), this.ComponentColor);
                //}
                foreach (Component var_Component in this.components.OrderBy(i => i.ZIndex))
                {
                    var_Component.draw(_GraphicsDevice, _SpriteBatch);
                }
            }
        }
    }
}
