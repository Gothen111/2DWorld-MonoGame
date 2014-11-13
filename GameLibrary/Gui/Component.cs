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
    public class Component : MouseListener, KeyboardListener
    {
        private Rectangle bounds;

        public Rectangle Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }

        public bool IsInBounds(Vector2 _Position)
        {
            if (_Position.X >= bounds.Left && _Position.X <= bounds.Right && _Position.Y >= bounds.Top && _Position.Y <= bounds.Bottom)
            {
                return true;
            }
            return false;
        }

        private bool isFocusAble;

        public bool IsFocusAble
        {
            get { return isFocusAble; }
            set { isFocusAble = value; this.IsFocused = this.IsFocused; }
        }

        private bool isFocused;

        public bool IsFocused
        {
            get { return isFocused; }
            set { isFocused = value && IsFocusAble && IsVisible; }
        }

        private String isFocusedTexture = "";

        public String IsFocusedTexture
        {
            get { return isFocusedTexture; }
            set { isFocusedTexture = value; }
        }

        private bool isHovered;

        public bool IsHovered
        {
            get { return isHovered; }
            set { isHovered = value; }
        }

        private String isHoveredTexture;

        public String IsHoveredTexture
        {
            get { return isHoveredTexture; }
            set { isHoveredTexture = value; }
        }

        private bool isPressed;

        public bool IsPressed
        {
            get { return isPressed; }
            set { isPressed = value; }
        }

        private String isPressedTexture;

        public String IsPressedTexture
        {
            get { return isPressedTexture; }
            set { isPressedTexture = value; }
        }

        private bool isVisible;

        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        private bool allowMultipleFocus;

        public bool AllowMultipleFocus
        {
            get { return allowMultipleFocus; }
            set { allowMultipleFocus = value; }
        }

        private String backgroundGraphicPath;

        public String BackgroundGraphicPath
        {
            get { return backgroundGraphicPath; }
            set { backgroundGraphicPath = value; updateGraphics(); }
        }

        private Rectangle sourceRectangle;

        public Rectangle SourceRectangle
        {
            get { return sourceRectangle; }
            set { sourceRectangle = value; }
        }

        private float scale;

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        private bool isActive;

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }

        private String isSelectedTexture;

        public String IsSelectedTexture
        {
            get { return isSelectedTexture; }
            set { isSelectedTexture = value; }
        }

        private bool allowsDropIn;

        public bool AllowsDropIn
        {
            get { return allowsDropIn; }
            set { allowsDropIn = value; }
        }

        //protected Component parent;

        private Color componentColor;

        public Color ComponentColor
        {
            get { return componentColor; }
            set { componentColor = value; }
        }

        private int zIndex;

        public int ZIndex
        {
            get { return zIndex; }
            set { zIndex = value; }
        }


        public void setBounds(Vector3 _Pos, Vector3 _Size)
        {
            this.bounds = new Rectangle((int)_Pos.X, (int)_Pos.Y, (int)_Size.X, (int)_Size.Y);
        }

        public Component()
        {
            Input.Mouse.MouseManager.mouseFocus.Add(this);
            IsFocusAble = true;
            isVisible = true;
            this.scale = 1.0f;
            this.isActive = true;
            this.allowsDropIn = false;
            //this.parent = null;
            this.componentColor = Color.White;
        }

        public Component(Rectangle _Bounds) //, Component _Parent
            : this()
        {
            this.bounds = _Bounds;
            this.sourceRectangle = new Rectangle(0, 0, this.bounds.Width, this.bounds.Height);
            //this.parent = _Parent;
        }

        public virtual bool mouseClicked(MouseEnum mouseButtonClicked, Vector2 position)
        {
            if (position.X >= bounds.Left && position.X <= bounds.Right && position.Y >= bounds.Top && position.Y <= bounds.Bottom)
            {
                this.isPressed = true;
                return true;
            }
            else
            {
                this.IsFocused = false;
                this.isPressed = false;
                Input.Keyboard.KeyboardManager.keyboardFocus.Remove(this);
                return false;
            }
        }

        public virtual bool mouseReleased(MouseEnum mouseButtonReleased, Vector2 position)
        {
            if (position.X >= bounds.Left && position.X <= bounds.Right && position.Y >= bounds.Top && position.Y <= bounds.Bottom)
            {
                if (this.isPressed)
                {
                    onClick(mouseButtonReleased, position);
                    return true;
                }
            }
            else
            {
                this.IsFocused = false;
                this.isPressed = false;
                Input.Keyboard.KeyboardManager.keyboardFocus.Remove(this);
            }
            return false;
        }

        public virtual void mouseMoved(Vector2 position)
        {
            if (position.X >= bounds.Left && position.X <= bounds.Right && position.Y >= bounds.Top && position.Y <= bounds.Bottom)
            {
                this.IsHovered = true;
                //Console.WriteLine(position.X + " : " + position.Y);
            }
            else
            {
                this.IsHovered = false;
                this.isPressed = false;
            }
        }

        public virtual void onClick(MouseEnum mouseButton, Vector2 _MousePosition)
        {
            if (this.isActive)
            {
                this.IsFocused = true;
                if (!Input.Keyboard.KeyboardManager.keyboardFocus.Contains(this))
                {
                    if (!allowMultipleFocus)
                        Input.Keyboard.KeyboardManager.keyboardFocus.Clear();
                    Input.Keyboard.KeyboardManager.keyboardFocus.Add(this);
                }
            }
        }

        public virtual void keyboardButtonClicked(Keys buttonPressed)
        {

        }

        public virtual void keyboardButtonReleased(Keys buttonReleased)
        {

        }

        public virtual void release()
        {
            Input.Mouse.MouseManager.mouseFocus.Remove(this);
            Input.Keyboard.KeyboardManager.keyboardFocus.Remove(this);
        }

        public virtual Component getTopComponent(Vector2 _Position)
        {
            return this;
        }

        public virtual bool componentIsDropedIn(Component _Component)
        {
            return true;
        }

        public virtual void update()
        {
        }

        private void updateGraphics()
        {
            try
            {
                Texture2D texture = Ressourcen.RessourcenManager.ressourcenManager.Texture[this.backgroundGraphicPath + "_Selected"];
                IsSelectedTexture = this.backgroundGraphicPath + "_Selected";
            }
            catch (Exception e)
            {
                IsSelectedTexture = this.backgroundGraphicPath;
            }

            try
            {
                Texture2D texture = Ressourcen.RessourcenManager.ressourcenManager.Texture[this.backgroundGraphicPath + "_Hover"];
                IsHoveredTexture = this.backgroundGraphicPath + "_Hover";
            }
            catch (Exception e)
            {
                IsHoveredTexture = this.backgroundGraphicPath;
            }

            try
            {
                Texture2D texture = Ressourcen.RessourcenManager.ressourcenManager.Texture[this.backgroundGraphicPath + "_Pressed"];
                IsPressedTexture = this.backgroundGraphicPath + "_Pressed";
            }
            catch (Exception e)
            {
                IsPressedTexture = this.backgroundGraphicPath;
            }
        }

        public virtual void draw(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch)
        {
            if (this.backgroundGraphicPath != null && !this.backgroundGraphicPath.Equals(""))
            {
                if(this.isActive && this.isVisible)
                {
                    Vector2 pos = new Vector2(this.Bounds.X, this.Bounds.Y); 
                        
                    if (!this.IsHovered)
                    {
                        if (!this.isSelected)
                        {
                            _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.backgroundGraphicPath], pos, null, this.componentColor, 0.0f,Vector2.Zero, this.scale, SpriteEffects.None, 0.0f);
                        }
                        else
                        {
                            _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.IsPressedTexture], pos, null, this.componentColor, 0.0f, Vector2.Zero, this.scale, SpriteEffects.None, 0.0f);
                        }
                            
                    }
                    else
                    {
                        if (!this.isPressed)
                        {
                            if(!this.IsHoveredTexture.Equals(this.backgroundGraphicPath))
                            {
                                _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.IsHoveredTexture], pos, null, this.componentColor, 0.0f, Vector2.Zero, this.scale, SpriteEffects.None, 0.0f);
                            }
                            else
                            {
                                if (!this.isSelected)
                                {
                                    _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.backgroundGraphicPath], pos, null, this.componentColor, 0.0f, Vector2.Zero, this.scale, SpriteEffects.None, 0.0f);
                                }
                                else
                                {
                                    _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.IsSelectedTexture], pos, null, this.componentColor, 0.0f, Vector2.Zero, this.scale, SpriteEffects.None, 0.0f);
                                }
                            }
                        }
                        else
                        {
                            if(!this.IsPressedTexture.Equals(this.backgroundGraphicPath))
                            {
                                _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.IsPressedTexture], pos, null, this.componentColor, 0.0f, Vector2.Zero, this.scale, SpriteEffects.None, 0.0f);
                            }
                            else
                            {
                                if(!this.IsHoveredTexture.Equals(this.backgroundGraphicPath))
                                {
                                    _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.IsHoveredTexture], pos, null, this.componentColor, 0.0f, Vector2.Zero, this.scale, SpriteEffects.None, 0.0f);
                                }
                                else
                                {
                                    if (!this.isSelected)
                                    {
                                        _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.backgroundGraphicPath], pos, null, this.componentColor, 0.0f, Vector2.Zero, this.scale, SpriteEffects.None, 0.0f);
                                    }
                                    else
                                    {
                                        _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.IsSelectedTexture], pos, null, this.componentColor, 0.0f, Vector2.Zero, this.scale, SpriteEffects.None, 0.0f);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
