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
    public interface MouseListener
    {
        bool mouseClicked(MouseEnum.MouseEnum mouseButtonClicked, Vector2 position);
        bool mouseReleased(MouseEnum.MouseEnum mouseButtonReleased, Vector2 position);
        void mouseMoved(Vector2 position);
        void onClick(MouseEnum.MouseEnum mouseButton, Vector2 _MousePosition);
    }
}
