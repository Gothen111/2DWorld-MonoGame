using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Commands
{
    public enum ECommandType
    {
        WalkLeftCommand,
        WalkRightCommand,
        WalkTopCommand,
        WalkDownCommand,
        StopWalkLeftCommand,
        StopWalkRightCommand,
        StopWalkTopCommand,
        StopWalkDownCommand,
        AttackCommand,
    }
}
