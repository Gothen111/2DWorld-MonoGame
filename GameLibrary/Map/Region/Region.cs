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
using Utility.Corpus;
using GameLibrary.Connection.Message;
using GameLibrary.Connection;
#endregion

#region Using Statements Class Specific
#endregion

namespace GameLibrary.Map.Region
{
    [Serializable()]
    public class Region : Box
    {
        public static int _id = 0;
        private int id = _id++;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public static int regionSizeX = 10;
        public static int regionSizeY = 10;

        private Chunk.Chunk[] chunks;

        public Chunk.Chunk[] Chunks
        {
            get { return chunks; }
            set { chunks = value; }
        }

        private RegionEnum regionEnum;

        public RegionEnum RegionEnum
        {
            get { return regionEnum; }
            set { regionEnum = value; }
        }

        private List<DungeonGeneration.Dungeon> dungeons;

        public List<DungeonGeneration.Dungeon> Dungeons
        {
            get { return dungeons; }
            set { dungeons = value; }
        }

        public Region(String _Name, int _PosX, int _PosY, Vector3 _Size, RegionEnum _RegionEnum, World.World _ParentWorld)
        {
            this.Name = _Name;
            this.Position = new Vector3(_PosX, _PosY, 0);
            this.Size = new Vector3(_Size.X, _Size.Y, _Size.Z);
            this.Bounds = new Cube(this.Position, new Vector3((regionSizeX * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize - 1), (regionSizeX * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize - 1), 0));

            chunks = new Chunk.Chunk[(int)(this.Size.X * this.Size.Y)];

            this.regionEnum = _RegionEnum;

            this.Parent = _ParentWorld;

            this.dungeons = new List<DungeonGeneration.Dungeon>();

            if (Configuration.Configuration.isHost || Configuration.Configuration.isSinglePlayer)
            {
            }
            else
            {
                this.requestFromServer();
            }
        }

        public Region(SerializationInfo info, StreamingContext ctxt) 
            : base(info, ctxt)
        {
            this.id = (int)info.GetValue("id", typeof(int));
            this.regionEnum = (RegionEnum)info.GetValue("regionEnum", typeof(int));

            this.chunks = new Chunk.Chunk[regionSizeX * regionSizeY];
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
            info.AddValue("id", this.id, typeof(int));
            info.AddValue("regionEnum", this.regionEnum, typeof(int));
        }

        public bool setChunkAtPosition(Vector3 _Position, Chunk.Chunk _Chunk)
        {
            Chunk.Chunk var_Chunk = this.getChunkAtPosition(_Chunk.Position);
            if (var_Chunk == null)
            {
                /*if (_PosX >= Bounds.Left && _PosX <= Bounds.Right)
                {
                    if (_PosY >= Bounds.Top && _PosY <= Bounds.Bottom)
                    {*/
                        int var_X = (int)Math.Abs(_Position.X - this.Position.X) / (Block.Block.BlockSize * Chunk.Chunk.chunkSizeX);
                        int var_Y = (int)Math.Abs(_Position.Y - this.Position.Y) / (Block.Block.BlockSize * Chunk.Chunk.chunkSizeY);

                        int var_Position = (int)(var_X + var_Y * regionSizeX);
                        this.chunks[var_Position] = _Chunk;
                        //this.setAllNeighboursOfChunk(_Chunk);
                        this.setAllNeighboursOfChunks();
                        //World.World.world.setAllNeighboursOfRegion((Region)_Chunk.Parent);
                        if (GameLibrary.Configuration.Configuration.isHost)
                        {
                            //GameLibrary.Commands.Executer.Executer.executer.addCommand(new Commands.CommandTypes.UpdateChunkCommand(_Chunk));
                            //this.saveChunk(_Chunk);
                        }
                        else
                        {

                        }

                        return true;
                /*    }
                }*/
                        Logger.Logger.LogErr("Region->setChunkAtPosition(...) : Platzierung nicht möglich: PosX " + _Position.X + " PosY " + _Position.Y);
                return false;
            }
            else
            {
                Logger.Logger.LogErr("Region->setChunkAtPosition(...) : Chunk mit Id: " + _Chunk.Id + " schon vorhanden!");
                return false;
            }
        }

        public bool containsChunk(int _Id)
        {
            foreach (Chunk.Chunk var_Chunk in this.chunks)
            {
                if (var_Chunk.Id == _Id)
                {
                    return true;
                }
            }
            return false;
        }

        public bool removeChunk(Chunk.Chunk _Chunk)
        {
            int var_X = (int)Math.Abs(_Chunk.Position.X - this.Position.X) / (Block.Block.BlockSize * Chunk.Chunk.chunkSizeX);
            int var_Y = (int)Math.Abs(_Chunk.Position.Y - this.Position.Y) / (Block.Block.BlockSize * Chunk.Chunk.chunkSizeY);

            int var_Position = (int)(var_X + var_Y * regionSizeX);
            if (var_Position < this.chunks.Length)
            {
                this.chunks[var_Position] = null;
                return true;
            }

            return false;
        }

        public void setAllNeighboursOfChunks()
        {
            foreach (Chunk.Chunk var_Chunk in this.chunks)
            {
                if (var_Chunk != null)
                {
                    var_Chunk.Parent = this;
                    setAllNeighboursOfChunk(var_Chunk);
                }
            }
        }

        public void setAllNeighboursOfChunk(Chunk.Chunk _Chunk)
        {
            Chunk.Chunk var_ChunkNeighbourLeft = null;
            if(this is DungeonGeneration.Dungeon)
            {
                var_ChunkNeighbourLeft = this.getChunkAtPosition(new Vector3(_Chunk.Position.X - Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, _Chunk.Position.Y, 0));
            
            }
            else
            {
                var_ChunkNeighbourLeft = World.World.world.getChunkAtPosition(new Vector3(_Chunk.Position.X - Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, _Chunk.Position.Y, 0));
            }

            if (var_ChunkNeighbourLeft != null)
            {
                _Chunk.LeftNeighbour = var_ChunkNeighbourLeft;
                var_ChunkNeighbourLeft.RightNeighbour = _Chunk;
                for (int blockY = 0; blockY < Chunk.Chunk.chunkSizeY; blockY++)
                {
                    Block.Block var_BlockRight = _Chunk.getBlockAtPosition(0, blockY);
                    Block.Block var_BlockLeft = var_ChunkNeighbourLeft.getBlockAtPosition(Chunk.Chunk.chunkSizeX - 1, blockY);

                    if (var_BlockLeft != null && var_BlockRight != null)
                    {
                        var_BlockRight.LeftNeighbour = var_BlockLeft;
                        var_BlockLeft.RightNeighbour = var_BlockRight;
                    }
                }
            }

            Chunk.Chunk var_ChunkNeighbourRight = null;
            if (this is DungeonGeneration.Dungeon)
            {
                var_ChunkNeighbourRight = this.getChunkAtPosition(new Vector3(_Chunk.Position.X + Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, _Chunk.Position.Y, 0));

            }
            else
            {
                var_ChunkNeighbourRight = World.World.world.getChunkAtPosition(new Vector3(_Chunk.Position.X + Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, _Chunk.Position.Y, 0));
            }
            if (var_ChunkNeighbourRight != null)
            {
                _Chunk.RightNeighbour = var_ChunkNeighbourRight;
                var_ChunkNeighbourRight.LeftNeighbour = _Chunk;
                for (int blockY = 0; blockY < Chunk.Chunk.chunkSizeY; blockY++)
                {
                    Block.Block var_BlockRight = var_ChunkNeighbourRight.getBlockAtPosition(0, blockY);
                    Block.Block var_BlockLeft = _Chunk.getBlockAtPosition(Chunk.Chunk.chunkSizeX - 1, blockY);

                    if (var_BlockLeft != null && var_BlockRight != null)
                    {
                        var_BlockLeft.RightNeighbour = var_BlockRight;
                        var_BlockRight.LeftNeighbour = var_BlockLeft;
                    }
                }
            }

            Chunk.Chunk var_ChunkNeighbourTop = null;
            if(this is DungeonGeneration.Dungeon)
            {
                var_ChunkNeighbourTop = this.getChunkAtPosition(new Vector3(_Chunk.Position.X, _Chunk.Position.Y - Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, 0));
            
            }
            else
            {
                var_ChunkNeighbourTop = World.World.world.getChunkAtPosition(new Vector3(_Chunk.Position.X, _Chunk.Position.Y - Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, 0));
            }

            if (var_ChunkNeighbourTop != null)
            {
                _Chunk.TopNeighbour = var_ChunkNeighbourTop;
                var_ChunkNeighbourTop.BottomNeighbour = _Chunk;
                for (int blockX = 0; blockX < Chunk.Chunk.chunkSizeX; blockX++)
                {
                    Block.Block var_BlockTop = var_ChunkNeighbourTop.getBlockAtPosition(blockX, Chunk.Chunk.chunkSizeY - 1);
                    Block.Block var_BlockBottom = _Chunk.getBlockAtPosition(blockX, 0);

                    if (var_BlockTop != null && var_BlockBottom != null)
                    {
                        var_BlockBottom.TopNeighbour = var_BlockTop;
                        var_BlockTop.BottomNeighbour = var_BlockBottom;
                    }
                }
            }

            Chunk.Chunk var_ChunkNeighbourBottom = null;
            if(this is DungeonGeneration.Dungeon)
            {
                var_ChunkNeighbourBottom = this.getChunkAtPosition(new Vector3(_Chunk.Position.X, _Chunk.Position.Y + Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, 0));
            }
            else
            {
                var_ChunkNeighbourBottom = World.World.world.getChunkAtPosition(new Vector3(_Chunk.Position.X, _Chunk.Position.Y + Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, 0));
            }

            if (var_ChunkNeighbourBottom != null)
            {
                _Chunk.BottomNeighbour = var_ChunkNeighbourBottom;
                var_ChunkNeighbourBottom.TopNeighbour = _Chunk;
                for (int blockX = 0; blockX < Chunk.Chunk.chunkSizeX; blockX++)
                {
                    Block.Block var_BlockTop = _Chunk.getBlockAtPosition(blockX, Chunk.Chunk.chunkSizeY - 1);
                    Block.Block var_BlockBottom = var_ChunkNeighbourBottom.getBlockAtPosition(blockX, 0);

                    if (var_BlockTop != null && var_BlockBottom != null)
                    {
                        var_BlockTop.BottomNeighbour = var_BlockBottom;
                        var_BlockBottom.TopNeighbour = var_BlockTop;
                    }
                }
            }
        }

        public Chunk.Chunk getChunkAtPosition(Vector3 _Position)
        {
            int var_X = (int)Math.Abs(_Position.X - this.Position.X) / (Block.Block.BlockSize * Chunk.Chunk.chunkSizeX);
            int var_Y = (int)Math.Abs(_Position.Y - this.Position.Y) / (Block.Block.BlockSize * Chunk.Chunk.chunkSizeY);

            int var_Position = (int)(var_X + var_Y * regionSizeX);
            if (var_Position < this.chunks.Length)
            {
                return this.chunks[var_Position];
            }
            return null;
        }

        public Chunk.Chunk createChunkAt(Vector3 _Position)
        {
            _Position = Chunk.Chunk.parsePosition(_Position);
            Chunk.Chunk var_Chunk = this.loadChunk(_Position.X, _Position.Y);
            if(var_Chunk == null)
            {
                var_Chunk = GameLibrary.Factory.RegionFactory.regionFactory.createChunkInRegion(this, (int)_Position.X, (int)_Position.Y);
            }
            else
            {
                this.setChunkAtPosition(_Position, var_Chunk);
            }
            return var_Chunk;
        }

        public Chunk.Chunk loadChunk(float _PosX, float _PosY)
        {
            String var_Path = "Save/" + this.Position.X + "_" + this.Position.Y + "/Chunks/" + _PosX + "_" + _PosY + ".sav";
            if (System.IO.File.Exists(var_Path))
            {
                Chunk.Chunk var_Chunk = (Chunk.Chunk)Utility.IO.IOManager.LoadISerializeAbleObjectFromFile(var_Path);//Utility.Serializer.DeSerializeObject(var_Path);
                if (var_Chunk != null)
                {
                    var_Chunk.Parent = GameLibrary.Map.World.World.world.getRegion(this.id);
                    var_Chunk.setAllNeighboursOfBlocks();
                    return var_Chunk;
                }
                else
                {
                    System.IO.File.Delete(var_Path);
                    Logger.Logger.LogErr("Chunk konnte nicht geladen werden, obwohl vorhanden -> Datei wird gelöscht");
                }
            }
            return null;
        }

        public void saveChunk(Chunk.Chunk _Chunk)
        {
            //Speichere erst mal nur blöcke
            String var_Path = "Save/" + this.Position.X + "_" + this.Position.Y + "/Chunks/" + _Chunk.Position.X + "_" + _Chunk.Position.Y + ".sav";
            Utility.IO.IOManager.SaveISerializeAbleObjectToFile(var_Path, _Chunk);
        }

        public Chunk.Chunk getChunkObjectIsIn(GameLibrary.Object.Object _Object)
        {
            //TODO: Fehlerbehandlungen, falls LivingObject nicht in der Region ist --> Nullpointer da var_X oder var_Y zu klein/groß
            int var_X = (int)(_Object.Position.X / ((this.Position.X + 1) * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize));
            int var_Y = (int)(_Object.Position.Y / ((this.Position.Y + 1) * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize));
            if (var_X >= Region.regionSizeX || var_Y >= Region.regionSizeY)
            {
                Logger.Logger.LogErr("LivingObject befindet sich nicht in Region " + this.Id);
                return null;
            }
            else
            {
                return this.getChunkAtPosition(_Object.Position);
            }
        }

        public override void update(GameTime _GameTime)
        {
            base.update(_GameTime);
        }

        public Chunk.Chunk getChunk(int _Id)
        {
            foreach (Chunk.Chunk var_Chunk in chunks)
            {
                if (var_Chunk != null)
                {
                    if (var_Chunk.Id == _Id)
                    {
                        return var_Chunk;
                    }
                }
            }
            return null;
        }

        public static Vector3 parsePosition(Vector3 _Position)
        {
            int _PosX = (int)_Position.X;
            int _PosY = (int)_Position.Y;

            int var_SizeX = (Region.regionSizeX * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize);
            int var_SizeY = (Region.regionSizeY * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize);

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
                Configuration.Configuration.networkManager.addEvent(new RequestRegionMessage(this.Position), GameMessageImportance.VeryImportant);
            }
        }

        public Block.Block getBlockAtCoordinate(Vector3 _Position)
        {
            Chunk.Chunk var_Chunk = this.getChunkAtPosition(_Position);
            if (var_Chunk != null)
            {
                return var_Chunk.getBlockAtCoordinate(_Position);
            }
            return null;
        }

        public bool setBlockAtCoordinate(Vector3 _Position, Block.Block _Block)
        {
            Chunk.Chunk var_Chunk = this.getChunkAtPosition(_Position);
            if (var_Chunk != null)
            {
                return var_Chunk.setBlockAtCoordinate(_Position, _Block);
            }
            return false;
        }
    }
}
