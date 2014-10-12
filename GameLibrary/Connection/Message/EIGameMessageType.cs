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

namespace GameLibrary.Connection.Message
{
    public enum EIGameMessageType
    {
        CreatureEquipmentToInventoryMessage,
        CreatureInventoryItemPositionChangeMessage,
        CreatureInventoryToEquipmentMessage,
        PlayerCommandMessage,
        RemoveObjectMessage,
        RequestBlockMessage,
        RequestChunkMessage,
        RequestLivingObjectMessage,
        RequestPlayerMessage,
        RequestRegionMessage,
        RequestWorldMessage,
        UpdateAnimatedObjectBodyMessage,
        UpdateBlockMessage,
        UpdateChunkMessage,
        UpdateCreatureInventoryMessage,
        UpdateFactionsMessage,
        UpdateObjectMessage,
        UpdateObjectMovementMessage,
        UpdateObjectHealthMessage,
        UpdateObjectPositionMessage,
        UpdatePlayerMessage,
        UpdateRacesMessage,
        UpdateRegionMessage,
        UpdateWorldMessage,
    }
}
