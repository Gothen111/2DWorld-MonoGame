#region Using Statements Standard
using System;
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

namespace GameLibrary.Object.Task.Tasks
{
    public enum TaskPriority
    {
        Stand,
        Walk_Random,
        Attack_Random,
        Order_Follow,
        Order_Move,
        Order_MoveAttack,
        Order_Attack,
        Order_UseItem,
        Order_CastSpell,
    }
}
