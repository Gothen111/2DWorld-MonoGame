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
using GameLibrary.Enums;
using GameLibrary.Map.Chunk.Decorator;
using GameLibrary.Factory;
#endregion

#region Using Statements Class Specific
#endregion

namespace GameLibrary.Map.Region
{
    [Serializable()]
    public class Region : Box
    {
        private static int lastId = 0;

        private int getId()
        {
            return lastId++;
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

        public Region(String _Name, int _PosX, int _PosY, Vector3 _Size, RegionEnum _RegionEnum, Dimension.Dimension _ParentDimension)
        {
            this.Id = this.getId();
            this.Name = _Name;
            this.Position = new Vector3(_PosX, _PosY, 0);
            this.Size = new Vector3(_Size.X, _Size.Y, _Size.Z);
            
            chunks = new Chunk.Chunk[(int)(this.Size.X * this.Size.Y)];

            this.regionEnum = _RegionEnum;

            this.Parent = _ParentDimension;

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
            this.regionEnum = (RegionEnum)info.GetValue("regionEnum", typeof(int));

            this.chunks = new Chunk.Chunk[regionSizeX * regionSizeY];
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
            info.AddValue("regionEnum", this.regionEnum, typeof(int));
        }

        public virtual void createRegion()
        {
            //if (this.Position.X == 0 && this.Position.Y == 0)
            //{
                double[,] var_HeigthMap = this.getParent().getHeightMap(this.Position, new Vector3(regionSizeX * Chunk.Chunk.chunkSizeX, regionSizeY * Chunk.Chunk.chunkSizeY, 0));

                double test = 0;
                for (int x = 0; x < var_HeigthMap.GetLength(0); x++)
                {
                    for (int y = 0; y < var_HeigthMap.GetLength(1); y++)
                    {
                        Block.Block var_Block = this.getBlockAtCoordinate(new Vector3(this.Position.X + x * Block.Block.BlockSize, this.Position.Y + y * Block.Block.BlockSize, 0));
                        if (var_Block != null)
                        {
                            if (var_HeigthMap[x, y] < 20)
                            {
                                var_Block.setFirstLayer(BlockEnum.Water);
                            }
                            else if (var_HeigthMap[x, y] > 40)
                            {
                                var_Block.setFirstLayer(BlockEnum.Wall);
                            }
                        }

                        test += var_HeigthMap[x, y];
                    }
                }
                Console.WriteLine(test / 10000);
            //}

            foreach(Chunk.Chunk var_Chunk in this.chunks)
            {
                if (var_Chunk != null)
                {
                    ChunkFactory.chunkFactory.generateChunk(var_Chunk, RegionDependency.regionDependency.getLayer(this.RegionEnum));
                    Decorator.decorator.decorateChunk(var_Chunk);
                    this.loadAllObjectsFromChunkToQuadTree(var_Chunk);
                }
            }
        }

        public Dimension.Dimension getParent()
        {
            return (Dimension.Dimension)this.Parent;
        }

        public bool setChunkAtPosition(Vector3 _Position, Chunk.Chunk _Chunk)
        {
            return this.setChunkAtPosition(_Position, _Chunk, false);
        }

        public bool setChunkAtPosition(Vector3 _Position, Chunk.Chunk _Chunk, bool _Loaded)
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
                        _Chunk.setNeighbours();

                        /*if (Configuration.Configuration.isHost || Configuration.Configuration.isSinglePlayer)
                        {
                            this.loadAllObjectsFromChunkToQuadTree(_Chunk);
                        }*/

                        if (_Loaded)
                        {
                            this.loadAllObjectsFromChunkToQuadTree(_Chunk);
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


        public void loadAllObjectsFromChunkToQuadTree(Chunk.Chunk _Chunk)
        {
            List<Object.Object> var_ObjectsList = _Chunk.getAllObjectsInChunk();

            for (int i = 0; i < var_ObjectsList.Count; i++)
            {
                this.getParent().QuadTreeObject.Insert(var_ObjectsList[i]);
            }
        }

        public void setAllNeighboursOfChunks()
        {
            for (int i = 0; i < this.chunks.Length; i++)
            {
                if (this.chunks[i] != null)
                {
                    this.chunks[i].setNeighbours();
                }
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
                var_Chunk = GameLibrary.Factory.RegionFactory.createChunkInRegion(this, (int)_Position.X, (int)_Position.Y);
            }
            else
            {
                this.setChunkAtPosition(_Position, var_Chunk, true);
            }
            return var_Chunk;
        }

        public Chunk.Chunk loadChunk(float _PosX, float _PosY)
        {
            String var_Path = "Save/" + this.getParent().Id + "/" + this.Position.X + "_" + this.Position.Y + "/Chunks/" + _PosX + "_" + _PosY + ".sav";
            if (System.IO.File.Exists(var_Path))
            {
                Chunk.Chunk var_Chunk = (Chunk.Chunk)Utility.IO.IOManager.LoadISerializeAbleObjectFromFile(var_Path);//Utility.Serializer.DeSerializeObject(var_Path);
                if (var_Chunk != null)
                {
                    var_Chunk.Parent = this;
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
            String var_Path = "Save/" + this.getParent().Id + "/" + this.Position.X + "_" + this.Position.Y + "/Chunks/" + _Chunk.Position.X + "_" + _Chunk.Position.Y + ".sav";
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
                Configuration.Configuration.networkManager.addEvent(new RequestRegionMessage(this), GameMessageImportance.VeryImportant);
            }
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

        public Block.Block getBlockAtCoordinate(Vector3 _Position)
        {
            Chunk.Chunk var_Chunk = this.getChunkAtPosition(_Position);
            if (var_Chunk != null)
            {
                return var_Chunk.getBlockAtCoordinate(_Position);
            }
            return null;
        }

        protected override void boundsChanged()
        {
            this.Bounds = new Cube(this.Position, new Vector3((regionSizeX * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize - 1), (regionSizeX * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize - 1), 0));
        }

        protected override Box getNeighbourBox(Vector3 _Position)
        {
            return this.getParent().getRegionAtPosition(_Position);
        }

        public override void setNeighbours()
        {
            base.setNeighbours();
            this.setAllNeighboursOfChunks();
        }
    }
}
