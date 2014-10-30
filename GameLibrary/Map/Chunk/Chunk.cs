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
using Utility.Corpus;
using GameLibrary.Connection.Message;
using GameLibrary.Connection;
#endregion

namespace GameLibrary.Map.Chunk
{
    [Serializable()]
    public class Chunk : Box
    {
        public static int chunkSizeX = 10;
        public static int chunkSizeY = 10;

        private Block.Block[] blocks;

        public Block.Block[] Blocks
        {
            get { return blocks; }
            set { blocks = value; }
        }

        private List<Vector3> pathEntries;

        public List<Vector3> PathEntries
        {
            get { return pathEntries; }
            set { pathEntries = value; }
        }

        private ChunkEnum chunkEnum;

        public ChunkEnum ChunkEnum
        {
            get { return chunkEnum; }
            set { chunkEnum = value; }
        }

        public Chunk(String _Name, int _PosX, int _PosY, Region.Region _ParentRegion)
            :base()
        {
            this.Name = _Name;
            this.Position = new Vector3(_PosX, _PosY, 0);
            this.Size = new Vector3(chunkSizeX, chunkSizeY, 0);
            
            this.blocks = new Block.Block[chunkSizeX * chunkSizeY];

            this.pathEntries = new List<Vector3>();

            this.Parent = _ParentRegion;

            if (Configuration.Configuration.isHost || Configuration.Configuration.isSinglePlayer)
            {
            }
            else
            {
                this.requestFromServer();
            }
        }

        public Chunk(SerializationInfo info, StreamingContext ctxt) 
            :base(info, ctxt)
        {         
            this.blocks = (Block.Block[])info.GetValue("blocks", typeof(Block.Block[]));
            //this.blocks = new Block.Block[(int)this.Size.X * (int)this.Size.Y];
            //this.setAllNeighboursOfBlocks();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
            info.AddValue("blocks", this.blocks, typeof(Block.Block[]));
        }
        
        public bool setBlockAtCoordinate(Vector3 _Position, Block.Block _Block)
        {
            int var_X = (int)Math.Abs(_Position.X - this.Position.X) / Block.Block.BlockSize;
            int var_Y = (int)Math.Abs(_Position.Y - this.Position.Y) / Block.Block.BlockSize;
            return this.setBlockAtPosition(var_X, var_Y, _Block);
        }

        public bool setBlockAtPosition(int _PosX, int _PosY, Block.Block _Block)
        {
            if (_PosX >= 0 && _PosX < this.Size.X)
            {
                if (_PosY >= 0 && _PosY < this.Size.Y)
                {
                    int var_Position = (int)(_PosX + _PosY * chunkSizeX);
                    this.blocks[var_Position] = _Block;
                    //this.setAllNeighboursOfBlocks();// HIER!!!
                    _Block.setNeighbours();
                    return true;
                }
                else
                {
                    Logger.Logger.LogErr("Chunk->setBlockAtPosition(...) : Platzierung nicht möglich: PosX " + _PosX + " PosY " + _PosY);
                    return false;
                }
            }
            else
            {
                Logger.Logger.LogErr("Chunk->setBlockAtPosition(...) : Platzierung nicht möglich: PosX " + _PosX + " PosY " + _PosY);
                return false;
            }
        }

        public void setAllNeighboursOfBlocks()
        {
            for (int x = 0; x < this.Size.X; x++)
            {
                for (int y = 0; y < this.Size.Y; y++)
                {
                    Block.Block var_Block = this.getBlockAtPosition(x, y);
                    if (var_Block != null)
                    {
                        var_Block.Parent = this;
                        var_Block.setNeighbours();
                    }
                }
            }
        }

        public Block.Block getBlockAtCoordinate(Vector3 _Position)
        {
            int var_X = (int)Math.Abs(_Position.X - this.Position.X) / Block.Block.BlockSize;//(int)((_PosX % (Region.Region.regionSizeX * Chunk.chunkSizeX * Block.Block.BlockSize)) % (Chunk.chunkSizeX * Block.Block.BlockSize) / Block.Block.BlockSize);
            int var_Y = (int)Math.Abs(_Position.Y - this.Position.Y) / Block.Block.BlockSize;//(int)((_PosY % (Region.Region.regionSizeY * Chunk.chunkSizeY * Block.Block.BlockSize)) % (Chunk.chunkSizeY * Block.Block.BlockSize) / Block.Block.BlockSize);
            
            return this.getBlockAtPosition(var_X, var_Y);
        }

        public Block.Block getBlockAtPosition(float _PosX, float _PosY)
        {
            if (_PosX >= 0 && _PosX < Chunk.chunkSizeX && _PosY >= 0 && _PosY < Chunk.chunkSizeY)
            {
                int var_Position = (int)(_PosX + _PosY * chunkSizeX);
                return blocks[var_Position];//blocks[(int)(_PosX), ((int)_PosY)];
            }
            return null;
        }

        public override void update(GameTime _GameTime)
        {
            base.update(_GameTime);
            /*for (int x = 0; x < this.Size.X; x++)
            {
                for (int y = 0; y < this.Size.Y; y++)
                {
                    this.getBlockAtPosition(x, y).update();
                }
            }*/
        }

        public List<Object.Object> getAllObjectsInChunk()
        {
            List<Object.Object> result = new List<Object.Object>();
            for (int x = 0; x < this.Size.X; x++)
            {
                for (int y = 0; y < this.Size.Y; y++)
                {
                    if (this.getBlockAtPosition(x, y) != null)
                    {
                        foreach (Object.Object var_Object in this.getBlockAtPosition(x, y).Objects)
                        {
                            result.Add(var_Object);
                        }
                    }
                }
            }
            return result;
        }

        public List<Object.Object> getAllEnvironmentObjectsInChunk()
        {
            List<Object.Object> result = new List<Object.Object>();
            for (int x = 0; x < this.Size.X; x++)
            {
                for (int y = 0; y < this.Size.Y; y++)
                {
                    foreach (Object.Object var_Object in this.getBlockAtPosition(x, y).ObjectsPreEnviorment)
                    {
                        result.Add(var_Object);
                    }
                }
            }
            return result;
        }

        public static Vector3 parsePosition(Vector3 _Position)
        {
            int _PosX = (int)_Position.X;
            int _PosY = (int)_Position.Y;

            int var_SizeX = (Chunk.chunkSizeX * Block.Block.BlockSize);
            int var_SizeY = (Chunk.chunkSizeY * Block.Block.BlockSize);

            int var_RestX = _PosX % var_SizeX;
            int var_RestY = _PosY % var_SizeY;

            if (var_RestX != 0)
            {
                if (_PosX < 0)
                {
                    _PosX = _PosX - (var_SizeX + var_RestX);
                }
                else
                {
                    _PosX = _PosX - var_RestX;
                }
            }
            if (var_RestY != 0)
            {
                if (_PosY < 0)
                {
                    _PosY = _PosY - (var_SizeY + var_RestY);
                }
                else
                {
                    _PosY = _PosY - var_RestY;
                }
            }

            return new Vector3(_PosX, _PosY, _Position.Z);
        }

        public override void requestFromServer()
        {
            base.requestFromServer();
            if (this.Parent == null)
            {
            }
            else
            {
                Configuration.Configuration.networkManager.addEvent(new RequestChunkMessage(this), GameMessageImportance.VeryImportant);
            }
        }

        protected override void boundsChanged()
        {
            this.Bounds = new Cube(this.Position, new Vector3((chunkSizeX * Block.Block.BlockSize - 1), (int)(chunkSizeY * Block.Block.BlockSize - 1), 0));
        }

        protected override Box getNeighbourBox(Vector3 _Position)
        {
            return ((Region.Region)this.Parent).getParent().getChunkAtPosition(_Position);
        }

        public override void setNeighbours()
        {
            base.setNeighbours();
            this.setAllNeighboursOfBlocks();
        }
    }
}
