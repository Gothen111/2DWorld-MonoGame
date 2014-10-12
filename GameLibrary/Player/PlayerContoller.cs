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

namespace GameLibrary.Player
{
    public class PlayerContoller
    {
        public static PlayerContoller playerContoller = new PlayerContoller();

        private List<InputAction> inputActions;

        public PlayerContoller()
        {
            this.inputActions = new List<InputAction>();
        }

        public void update()
        {
            foreach (InputAction var_InputAction in this.inputActions)
            {
                if (var_InputAction.wantsToPeformAction())
                {
                    var_InputAction.performAction();
                }
                else
                {
                    var_InputAction.stopAction();
                }
            }
        }

        public void addInputAction(InputAction _InputAction)
        {
            this.inputActions.Add(_InputAction);
        }

        public void clearInputActions()
        {
            this.inputActions.Clear();
        }
    }
}
