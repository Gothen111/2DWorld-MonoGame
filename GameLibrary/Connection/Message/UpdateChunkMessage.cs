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
using GameLibrary.Map.Chunk;
using GameLibrary.Map.Region;
#endregion

namespace GameLibrary.Connection.Message
{
    public class UpdateChunkMessage : IGameMessage
    {
        #region Constructors and Destructors

        public UpdateChunkMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public UpdateChunkMessage(Chunk _Chunk)
        {
            this.Id = _Chunk.Id;
            this.MessageTime = NetTime.Now;
            this.RegionId = ((Region)_Chunk.Parent).Id;
            this.Chunk = _Chunk;
            this.Position = _Chunk.Position;
        }

        #endregion

        #region Properties

        public int Id { get; set; }

        public double MessageTime { get; set; }

        public int RegionId { get; set; }

        public Map.Chunk.Chunk Chunk { get; set; }

        public Vector3 Position { get; set; }

        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.UpdateChunkMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.Id = im.ReadInt32();
            this.MessageTime = im.ReadDouble();
            this.RegionId = im.ReadInt32();

            //this.Chunk = Utility.Serializer.DeserializeObjectFromString<Model.Map.Chunk.Chunk>(im.ReadString());
            //this.Chunk.Parent = Model.Map.World.World.world.getRegion(this.RegionId);
            //this.Chunk.setAllNeighboursOfBlocks();

            this.Position = Lidgren.MonoGame.ReadVector3(im);

            Map.Chunk.Chunk var_Chunk =  Map.World.World.world.getChunkAtPosition(Position);

            if (var_Chunk != null)
            {
                if (var_Chunk.IsRequested)
                {
                    var_Chunk.IsRequested = false;
                }
            }
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.Id);
            om.Write(this.MessageTime);
            om.Write(this.RegionId);

            //om.Write(Utility.Serializer.SerializeObjectToString(this.Chunk));

            Lidgren.MonoGame.WriteVector3(this.Position, om);

            /*int var_Size = Enum.GetValues(typeof(Map.Block.BlockLayerEnum)).Length;

            if (this.Chunk != null)
            {
                for (int x = 0; x < Map.Chunk.Chunk.chunkSizeX; x++)
                {
                    for (int y = 0; y < Map.Chunk.Chunk.chunkSizeY; y++)
                    {
                        for (int i = 0; i < var_Size; i++)
                        {
                            om.Write((int)this.Chunk.Blocks[x, y].Layer[i]);
                        }
                    }
                }
            }*/
        }

        #endregion
    }
}
