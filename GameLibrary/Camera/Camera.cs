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
using GameLibrary.Setting;
#endregion

namespace GameLibrary.Camera
{
    public class Camera : KeyboardListener
    {
        public static Camera camera;
        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        private float zoom;

        public float Zoom
        {
            get { return zoom; }
            set { zoom = value; }
        }

        private GameLibrary.Object.LivingObject target;

        public GameLibrary.Object.LivingObject Target
        {
            get { return target; }
            set { target = value; }
        }

        private Viewport viewPort;

        public Camera(Viewport _ViewPort)
        {
            this.target = null;
            this.position = new Vector3(0, 0, 0);
            this.zoom = 0.5f;
            this.viewPort = _ViewPort;
            KeyboardManager.keyboardFocus.Add(this);
        }

        public void setTarget(GameLibrary.Object.LivingObject _Target)
        {
            this.target = _Target;
        }

        public void setPosition(Vector3 _Position)
        {
            this.target = null;
            this.position = _Position;
        }

        public void update(GameTime gameTime)
        {
            if(target != null)
            {
                this.position = this.target.Position;// -new Vector3(viewPort.Width / 2, viewPort.Height / 2, 0);
            }
        }

        public Matrix getMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(-this.position.X, -this.position.Y, 0))*
                Matrix.CreateScale(new Vector3(this.zoom, this.zoom, 1))*
                Matrix.CreateScale(new Vector3(viewPort.Width / Setting.Setting.resolutionX, viewPort.Height / Setting.Setting.resolutionY, 1)) *
                Matrix.CreateTranslation(new Vector3(viewPort.Width * 0.5f, viewPort.Height * 0.5f, 0));
        }

        public void keyboardButtonClicked(Microsoft.Xna.Framework.Input.Keys buttonPressed)
        {
            if (buttonPressed.Equals(Microsoft.Xna.Framework.Input.Keys.OemPlus))
            {
                this.zoom += 0.1f;
                if (this.zoom > 1.5f)
                    this.zoom = 1.5f;
            }
            if (buttonPressed.Equals(Microsoft.Xna.Framework.Input.Keys.OemMinus))
            {
                this.zoom -= 0.1f;
                if (this.zoom < 0.5f)
                    this.zoom = 0.5f;
            }
            if (buttonPressed.Equals(Microsoft.Xna.Framework.Input.Keys.Z))
            {
                if(this.zoom == 0.1f)
                {
                    this.zoom = 1.0f;
                }
                else
                {
                    this.zoom = 0.1f;
                }
            }
        }

        public void keyboardButtonReleased(Microsoft.Xna.Framework.Input.Keys buttonReleased)
        {
            
        }
    } 
}
