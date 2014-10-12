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
using GameLibrary.Commands.CommandTypes;
#endregion

namespace GameLibrary.Player
{
    public class InputAction
    {
        //private MouseState mouseState;
      
        private List<Keys> keysToWatchOn;
        private Command command;
        public InputAction(List<Keys> _KeysToWatchOn, Command _Command)
        {
            this.keysToWatchOn = _KeysToWatchOn;
            this.command = _Command;
        }
        public bool wantsToPeformAction()
        {
            if(this.keysToWatchOn!=null)
            {
                foreach(Keys key in this.keysToWatchOn)
                {
                    if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyUp(key))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        public void performAction()
        {
            if (this.command != null)
            {
                this.command.doCommand();
            }
        }
        public void stopAction()
        {
            if (this.command != null)
            {
                this.command.stopCommand();
            }
        }
    }
}
