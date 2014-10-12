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

namespace Input.Mouse
{
    public class MouseManager
    {
        public static MouseManager mouseManager = new MouseManager();

        public static List<MouseListener> mouseFocus = new List<MouseListener>();

        List<MouseEnum.MouseEnum> mouseKeysPressed;

        private Vector2 oldMousePosition = new Vector2(Microsoft.Xna.Framework.Input.Mouse.GetState().X, Microsoft.Xna.Framework.Input.Mouse.GetState().Y);

        private MouseManager()
        {
            mouseKeysPressed = new List<MouseEnum.MouseEnum>();
        }

        public void update()
        {
            MouseState mouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (!mouseKeysPressed.Contains(MouseEnum.MouseEnum.Left))
                {
                    mouseKeysPressed.Add(MouseEnum.MouseEnum.Left);
                    notifyMouseFocusAboutClickEvent(MouseEnum.MouseEnum.Left, new Vector2(mouseState.X, mouseState.Y));
                }
            }
            else
            {
                if (mouseKeysPressed.Contains(MouseEnum.MouseEnum.Left))
                {
                    mouseKeysPressed.Remove(MouseEnum.MouseEnum.Left);
                    notifyMouseFocusAboutReleaseEvent(MouseEnum.MouseEnum.Left, new Vector2(mouseState.X, mouseState.Y));
                }
            }

            if (mouseState.MiddleButton == ButtonState.Pressed)
            {
                if (!mouseKeysPressed.Contains(MouseEnum.MouseEnum.Middle))
                {
                    mouseKeysPressed.Add(MouseEnum.MouseEnum.Middle);
                    notifyMouseFocusAboutClickEvent(MouseEnum.MouseEnum.Middle, new Vector2(mouseState.X, mouseState.Y));
                }
            }
            else
            {
                if (mouseKeysPressed.Contains(MouseEnum.MouseEnum.Middle))
                {
                    mouseKeysPressed.Remove(MouseEnum.MouseEnum.Middle);
                    notifyMouseFocusAboutReleaseEvent(MouseEnum.MouseEnum.Middle, new Vector2(mouseState.X, mouseState.Y));
                }
            }

            if (mouseState.RightButton == ButtonState.Pressed)
            {
                if (!mouseKeysPressed.Contains(MouseEnum.MouseEnum.Right))
                {
                    mouseKeysPressed.Add(MouseEnum.MouseEnum.Right);
                    notifyMouseFocusAboutClickEvent(MouseEnum.MouseEnum.Right, new Vector2(mouseState.X, mouseState.Y));
                }
            }
            else
            {
                if (mouseKeysPressed.Contains(MouseEnum.MouseEnum.Right))
                {
                    mouseKeysPressed.Remove(MouseEnum.MouseEnum.Right);
                    notifyMouseFocusAboutReleaseEvent(MouseEnum.MouseEnum.Right, new Vector2(mouseState.X, mouseState.Y));
                }
            }

            if (mouseState.X != oldMousePosition.X || mouseState.Y != oldMousePosition.Y)
            {
                oldMousePosition.X = mouseState.X;
                oldMousePosition.Y = mouseState.Y;
                notifyMouseFocusAboutMoveEvent(oldMousePosition);
            }


            
        }

        private void notifyMouseFocusAboutClickEvent(MouseEnum.MouseEnum mouseButton, Vector2 position)
        {
            List<MouseListener> copy = new List<MouseListener>();
            foreach (MouseListener listener in mouseFocus)
            {
                copy.Add(listener);
            }
            foreach (MouseListener listener in copy)
            {
                listener.mouseClicked(mouseButton, position);
            }
        }

        private void notifyMouseFocusAboutReleaseEvent(MouseEnum.MouseEnum mouseButton, Vector2 position)
        {
            List<MouseListener> copy = new List<MouseListener>();
            foreach (MouseListener listener in mouseFocus)
            {
                copy.Add(listener);
            }
            foreach (MouseListener listener in copy)
            {
                listener.mouseReleased(mouseButton, position);
            }
        }

        private void notifyMouseFocusAboutMoveEvent(Vector2 position)
        {
            List<MouseListener> copy = new List<MouseListener>();
            foreach (MouseListener listener in mouseFocus)
            {
                copy.Add(listener);
            }
            foreach (MouseListener listener in copy)
            {
                listener.mouseMoved(position);
            }
        }

    }
}
