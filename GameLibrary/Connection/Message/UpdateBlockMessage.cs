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
using Lidgren.Network;
using GameLibrary.Map.Block;
using GameLibrary.Map.Chunk;
using GameLibrary.Map.World;
using GameLibrary.Map.Region;
using GameLibrary.Enums;
#endregion

namespace GameLibrary.Connection.Message
{
    public class UpdateBlockMessage : IGameMessage
    {
        #region Constructors and Destructors

        public UpdateBlockMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public UpdateBlockMessage(Block _Block)
        {
            this.DimensionId = ((Region)_Block.Parent.Parent).getParent().Id;
            this.MessageTime = NetTime.Now;
            this.Block = _Block;
        }

        #endregion

        #region Properties

        public int DimensionId { get; set; }

        public double MessageTime { get; set; }

        public Block Block { get; set; }

        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.UpdateBlockMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.DimensionId = im.ReadInt32();
            this.MessageTime = im.ReadDouble();

            Microsoft.Xna.Framework.Vector3 var_Position = Lidgren.MonoGame.ReadVector3(im);

            this.Block = Map.World.World.world.getDimensionById(this.DimensionId).getBlockAtCoordinate(var_Position);

            int var_Size = Enum.GetValues(typeof(BlockLayerEnum)).Length;

            if (this.Block != null)
            {
                if (this.Block.IsRequested)
                {
                    for (int i = 0; i < var_Size; i++)
                    {
                        this.Block.Layer[i] = (BlockEnum)im.ReadInt32();
                    }
                }
            }
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.DimensionId);
            om.Write(this.MessageTime);    

            Lidgren.MonoGame.WriteVector3(this.Block.Position, om);

            int var_Size = Enum.GetValues(typeof(BlockLayerEnum)).Length;

            if (this.Block != null)
            {
                for (int i = 0; i < var_Size; i++)
                {
                    om.Write((int)this.Block.Layer[i]);
                }
            }
        }

        #endregion
    }
}
