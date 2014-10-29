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
using GameLibrary.Collison;
using GameLibrary.Object;
using GameLibrary.Map.World.SearchFlags;
using GameLibrary.Connection;
using GameLibrary.Map.Region;
using GameLibrary.Enums;
#endregion

namespace GameLibrary.Map.Dimension
{
    public class Dimension :Box
    {
        #region Attribute

        private static int lastId = 0;

        private int getId()
        {
            return lastId++;
        }

        private List<Region.Region> regions;

        private QuadTree<Object.Object> quadTreeObject;

        public QuadTree<Object.Object> QuadTreeObject
        {
            get { return quadTreeObject; }
            set { quadTreeObject = value; }
        }
        private List<Object.Object> objectsToUpdate;

        private int objectsToUpdateCounter;

        private int objectsToUpdateCounterMax;

        private List<Chunk.Chunk> chunksOutOfRange;

        private List<PlayerObject> currentPlayerObjects;

        #endregion

        #region Constructors

        public Dimension(World.World _ParentWorld)
        {
            this.Id = this.getId();

            this.Parent = _ParentWorld;

            regions = new List<Region.Region>();

            this.quadTreeObject = new QuadTree<Object.Object>(new Vector3(32, 32, 0), 20);

            this.objectsToUpdate = new List<Object.Object>();

            this.objectsToUpdateCounter = 0;

            this.objectsToUpdateCounterMax = 50;

            this.chunksOutOfRange = new List<Chunk.Chunk>();
        }

        public Dimension(int _Id, World.World _ParentWorld)
        {
            this.Id = _Id;

            this.Parent = _ParentWorld;

            regions = new List<Region.Region>();

            this.quadTreeObject = new QuadTree<Object.Object>(new Vector3(32, 32, 0), 20);

            this.objectsToUpdate = new List<Object.Object>();

            this.objectsToUpdateCounter = 0;

            this.objectsToUpdateCounterMax = 50;

            this.chunksOutOfRange = new List<Chunk.Chunk>();
        }

        public Dimension(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            this.regions = new List<Region.Region>();

            this.quadTreeObject = new QuadTree<Object.Object>(new Vector3(32, 32, 0), 20);

            this.objectsToUpdate = new List<Object.Object>();

            this.objectsToUpdateCounter = 0;

            this.objectsToUpdateCounterMax = 50;

            this.chunksOutOfRange = new List<Chunk.Chunk>();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }

        #endregion

        #region Update

        public void setCurrentPlayerObjects(List<PlayerObject> _PlayerObjects)
        {
            this.currentPlayerObjects = _PlayerObjects;
        }

        public override void update(GameTime _GameTime)
        {
            base.update(_GameTime);

            this.chunksOutOfRange = new List<Chunk.Chunk>();

            /*bool var_NewUpdateObjectsList = false;

            if (this.objectsToUpdateCounter <= 0)
            {
                this.objectsToUpdate = new List<Object.Object>();
                var_NewUpdateObjectsList = true;
                this.objectsToUpdateCounter = this.objectsToUpdateCounterMax;
            }
            else
            {
                this.objectsToUpdateCounter -= 1;
            }*/

            this.objectsToUpdate = new List<Object.Object>();

            this.chunksOutOfRange = new List<Chunk.Chunk>();
            foreach (Region.Region var_Region in this.regions)
            {
                foreach (Chunk.Chunk var_Chunk in var_Region.Chunks)
                {
                    if (var_Chunk != null)
                    {
                        var_Chunk.update(_GameTime);
                        this.chunksOutOfRange.Add(var_Chunk);
                    }
                }
            }

            this.updatePlayerObjectsNeighbourhood(_GameTime);

            foreach (Region.Region var_Region in this.regions)
            {
                var_Region.update(_GameTime);
            }

            foreach (Chunk.Chunk var_Chunk in this.chunksOutOfRange)
            {
                foreach (Object.Object var_Object in var_Chunk.getAllObjectsInChunk())
                {
                    this.quadTreeObject.Remove(var_Object);
                }
                this.removeChunk(var_Chunk);
            }

            try
            {
                foreach (Object.Object var_Object in this.objectsToUpdate)
                {
                    var_Object.update(_GameTime);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void updatePlayerObjectsNeighbourhood(GameTime _GameTime)
        {
            List<PlayerObject> var_Copy = new List<PlayerObject>(this.currentPlayerObjects);
            foreach (Object.PlayerObject var_PlayerObject in var_Copy)
            {
                this.updatePlayerObjectNeighborhood(_GameTime, var_PlayerObject);
            }
        }

        public void updatePlayerObjectNeighborhood(GameTime _GameTime, Object.PlayerObject _PlayerObject)
        {
            List<Chunk.Chunk> var_ChunksInRange = new List<Chunk.Chunk>();
            foreach (Chunk.Chunk var_Chunk in this.chunksOutOfRange)
            {
                if (Vector3.Distance(var_Chunk.Position, new Vector3(_PlayerObject.Position.X, _PlayerObject.Position.Y, 0)) <= (Setting.Setting.blockDrawRange * Block.Block.BlockSize))
                {
                    var_ChunksInRange.Add(var_Chunk);
                }
            }

            foreach (Chunk.Chunk var_Chunk in var_ChunksInRange)
            {
                this.chunksOutOfRange.Remove(var_Chunk);
            }

            if (_PlayerObject != null)
            {
                Vector3 var_PlayerPos = _PlayerObject.Position;

                if (_PlayerObject.CurrentBlock != null)
                {
                    var_PlayerPos = _PlayerObject.CurrentBlock.Position;
                }
                int var_DrawSizeX = Setting.Setting.blockDrawRange;
                int var_DrawSizeY = Setting.Setting.blockDrawRange;

                for (int x = 0; x < var_DrawSizeX; x++)
                {
                    for (int y = 0; y < var_DrawSizeY; y++)
                    {
                        Vector3 var_Position = new Vector3(var_PlayerPos.X + (-var_DrawSizeX / 2 + x) * Block.Block.BlockSize, var_PlayerPos.Y + (-var_DrawSizeY / 2 + y) * Block.Block.BlockSize, 0);
                        Block.Block var_Block = this.getBlockAtCoordinate(var_Position);
                        if (var_Block != null)
                        {
                            var_Block.update(_GameTime);
                        }
                        else
                        {
                            Region.Region var_Region = this.getRegionAtPosition(var_Position);
                            if (var_Region == null)
                            {
                                var_Region = this.createRegionAt(var_Position);
                            }
                            else
                            {
                                Chunk.Chunk var_Chunk = this.getChunkAtPosition(var_Position);
                                if (var_Chunk == null)
                                {
                                    var_Chunk = this.createChunkAt(var_Position);
                                }
                            }
                        }
                    }
                }
            }

            List<Object.Object> var_Objects = this.getObjectsInRange(_PlayerObject.Position, 400);
            foreach (Object.Object var_Object in var_Objects)
            {
                if (!this.objectsToUpdate.Contains(var_Object))
                {
                    this.objectsToUpdate.Add(var_Object);
                }
            }
        }

        #endregion

        #region Region

        public Region.Region getRegion(int _Id)
        {
            foreach (Region.Region var_Region in this.regions)
            {
                if (var_Region.Id == _Id)
                {
                    return var_Region;
                }
            }
            return null;
        }

        public bool addRegion(Region.Region _Region)
        {
            if (!containsRegion(_Region.Id))
            {
                this.regions.Add(_Region);

                if (GameLibrary.Configuration.Configuration.isHost)
                {
                    //this.saveRegion(_Region);
                }
                else
                {

                }

                return false;
            }
            else
            {
                Logger.Logger.LogErr("World->addRegion(...) : Region mit Id: " + _Region.Id + " schon vorhanden!");
                return false;
            }
        }

        public bool containsRegion(int _Id)
        {
            if (this.getRegion(_Id) != null)
            {
                //return true;
            }
            return false;
        }

        public bool containsRegion(Region.Region _Region)
        {
            return containsRegion(_Region.Id);
        }

        public Region.Region getRegionAtPosition(Vector3 _Position)
        {
            foreach (Region.Region var_Region in this.regions)
            {
                if (var_Region.Bounds.Left <= _Position.X && var_Region.Bounds.Right >= _Position.X)
                {
                    if (var_Region.Bounds.Top <= _Position.Y && var_Region.Bounds.Bottom >= _Position.Y)
                    {
                        return var_Region;
                    }
                }
            }

            return null;
        }

        private Region.Region loadRegion(Vector3 _Position)
        {
            String var_Path = "Save/" + this.Id + "/" + _Position.X + "_" + _Position.Y + "/RegionInfo.sav";
            if (System.IO.File.Exists(var_Path))
            {
                Region.Region var_Region = (Region.Region)Utility.IO.IOManager.LoadISerializeAbleObjectFromFile(var_Path);
                var_Region.Parent = this;
                return var_Region;
            }
            return null;
        }

        private void saveRegion(Region.Region _Region)
        {
            String var_Path = "Save/" + _Region.Position.X + "_" + _Region.Position.Y + "/RegionInfo.sav";
            Utility.IO.IOManager.SaveISerializeAbleObjectToFile(var_Path, _Region);
        }

        public Region.Region createRegionAt(Vector3 _Position)
        {
            _Position = Region.Region.parsePosition(_Position);

            Region.Region var_Region = this.loadRegion(_Position);
            if (var_Region == null)
            {
                int var_RegionType = Utility.Random.Random.GenerateGoodRandomNumber(0, Enum.GetValues(typeof(RegionEnum)).Length - 1);
                var_Region = GameLibrary.Factory.RegionFactory.regionFactory.generateRegion("Region", (int)_Position.X, (int)_Position.Y, (RegionEnum)var_RegionType, this);
            }
            this.addRegion(var_Region);

            return var_Region;
        }

        #endregion

        #region Chunk

        public Chunk.Chunk getChunkAtPosition(Vector3 _Position)
        {
            Region.Region var_Region = this.getRegionAtPosition(_Position);
            if (var_Region != null)
            {
                return var_Region.getChunkAtPosition(_Position);
            }
            return null;
        }

        public Chunk.Chunk createChunkAt(Vector3 _Position)
        {
            Region.Region var_Region = this.getRegionAtPosition(_Position);
            if (var_Region != null)
            {
                return var_Region.createChunkAt(_Position);
            }
            return null;
        }

        public bool removeChunk(Chunk.Chunk _Chunk)
        {
            Region.Region var_Region = this.getRegionAtPosition(_Chunk.Position);
            if (var_Region != null)
            {
                return var_Region.removeChunk(_Chunk);
            }
            return false;
        }

        #endregion

        #region Block

        public Block.Block getBlockAtCoordinate(Vector3 _Position)
        {
            Region.Region var_Region = this.getRegionAtPosition(_Position);
            if (var_Region != null)
            {
                return var_Region.getBlockAtCoordinate(_Position);
            }
            return null;
        }

        public bool setBlockAtCoordinate(Vector3 _Position, Block.Block _Block)
        {
            Region.Region var_Region = this.getRegionAtPosition(_Position);
            if (var_Region != null)
            {
                return var_Region.setBlockAtCoordinate(_Position, _Block);
            }
            return false;
        }

        #endregion

        #region Collision

        public List<Object.Object> getObjectsColliding(Rectangle bounds)
        {
            return getObjectsColliding(bounds, new List<Searchflag>());
        }

        public List<Object.Object> getObjectsColliding(Rectangle bounds, List<Searchflag> _SearchFlags)
        {
            List<Object.Object> result = new List<Object.Object>();
            getObjectsColliding(bounds, this.quadTreeObject.Root, result, _SearchFlags);
            return result;
        }

        private void getObjectsColliding(Rectangle bounds, QuadTree<Object.Object>.QuadNode currentNode, List<Object.Object> result, List<Searchflag> _SearchFlags)
        {
            if (Utility.Collision.Intersection.RectangleIsInRectangle(bounds, currentNode.Bounds))
            {
                //Circle fits in node, so search in subnodes
                Boolean circleFitsInSubnode = false;
                foreach (QuadTree<Object.Object>.QuadNode node in currentNode.Nodes)
                {
                    if (node != null)
                    {
                        if (Utility.Collision.Intersection.RectangleIsInRectangle(bounds, node.Bounds))
                        {
                            circleFitsInSubnode = true;
                            getObjectsInRange(bounds, node, result, _SearchFlags);
                        }
                    }
                }

                //Aggrocircle fit into a subnode? then
                if (!circleFitsInSubnode)
                {
                    addAllObjectsInRange(currentNode, bounds, result, _SearchFlags);
                }
                return;
            }
            if (currentNode.Equals(this.quadTreeObject.Root))
            {
                addAllObjectsInRange(currentNode, bounds, result, _SearchFlags);
            }
        }

        #endregion

        #region quadTreeObject

        public Object.Object getObject(int _Id)
        {
            if (this.quadTreeObject != null)
            {
                List<QuadTree<Object.Object>.QuadNode> var_Copy = new List<QuadTree<Object.Object>.QuadNode>(this.quadTreeObject.GetAllNodes());
                foreach (QuadTree<Object.Object>.QuadNode var_QuadNode in var_Copy)
                {
                    System.Collections.ObjectModel.ReadOnlyCollection<Object.Object> var_Copy2 = new System.Collections.ObjectModel.ReadOnlyCollection<Object.Object>(var_QuadNode.Objects);
                    foreach (Object.Object var_Object in var_Copy2)
                    {
                        if (var_Object.Id == _Id)
                        {
                            return var_Object;
                        }
                    }
                }
            }
            return null;
        }

        public Region.Region getRegionObjectIsIn(GameLibrary.Object.Object _Object)
        {
            foreach (Region.Region var_Region in this.regions)
            {
                if (_Object.Position.X >= var_Region.Position.X)
                {
                    if (_Object.Position.X <= var_Region.Position.X + var_Region.Bounds.Width)
                    {
                        if (_Object.Position.Y >= var_Region.Position.Y)
                        {
                            if (_Object.Position.Y <= var_Region.Position.Y + var_Region.Bounds.Height)
                            {
                                return var_Region;
                            }
                        }
                    }
                }
            }
            return null;
        }

        public Object.Object addObject(Object.Object _Object)
        {
            return addObject(_Object, true);
        }

        public Object.Object addObject(Object.Object _Object, Boolean _InsertInQuadTree)
        {
            Region.Region var_Region = this.getRegionObjectIsIn(_Object);
            return addObject(_Object, _InsertInQuadTree, var_Region);
        }

        public Object.Object addObject(Object.Object _Object, Boolean _InsertInQuadTree, Region.Region _Region)
        {
            if (_InsertInQuadTree)
            {
                this.quadTreeObject.Insert(_Object);
            }
            if (_Region != null)
            {
                Chunk.Chunk chunk = _Region.getChunkObjectIsIn(_Object);
                if (chunk != null)
                {
                    Block.Block var_Block = chunk.getBlockAtCoordinate(_Object.Position);
                    if (var_Block != null)
                    {
                        var_Block.addObject(_Object);
                        if (Configuration.Configuration.isHost)
                        {
                            Configuration.Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.UpdateObjectMessage(_Object), GameMessageImportance.VeryImportant);
                        }
                    }
                }
            }
            else
            {
                Logger.Logger.LogInfo("World.addObject: Object konnte der Region nicht hinzugefügt werden, da diese null war");
            }
            return _Object;
        }

        public Object.Object addPreEnvironmentObject(Object.Object _Object, Region.Region _Region)
        {
            if (_Region != null)
            {
                Chunk.Chunk chunk = _Region.getChunkObjectIsIn(_Object);
                if (chunk != null)
                {
                    Block.Block var_Block = chunk.getBlockAtCoordinate(_Object.Position);
                    if (var_Block != null)
                    {
                        var_Block.addPreEnvironmentObject(_Object);
                        if (Configuration.Configuration.isHost)
                        {
                            Configuration.Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.UpdatePreEnvironmentObjectMessage(_Object), GameMessageImportance.VeryImportant);
                        }
                    }
                }
            }
            else
            {
                Logger.Logger.LogInfo("World.addObject: Object konnte der Region nicht hinzugefügt werden, da diese null war");
            }
            return _Object;
        }

        public void removeObjectFromWorld(Object.Object _Object)
        {
            //TODO: Gucke ob element auch vorhanden ;)
            this.objectsToUpdate.Remove(_Object);
            this.quadTreeObject.Remove(_Object);
            if (_Object.CurrentBlock != null)
            {
                _Object.CurrentBlock.removeObject(_Object);
                _Object.CurrentBlock = null;
            }

            if (Configuration.Configuration.isHost)
            {
                Configuration.Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.RemoveObjectMessage(_Object), GameMessageImportance.VeryImportant);
            }
        }

        #endregion

        #region getObjectInRange
        public List<Object.Object> getObjectsInRange(Vector3 _Position, float _Range)
        {
            return getObjectsInRange(_Position, this.quadTreeObject.Root, _Range, new List<Searchflag>());
        }

        public List<Object.Object> getObjectsInRange(Vector3 _Position, float _Range, List<Searchflag> _SearchFlags)
        {
            return getObjectsInRange(_Position, this.quadTreeObject.Root, _Range, _SearchFlags);
        }

        public List<Object.Object> getObjectsInRange(Vector3 _Position, QuadTree<Object.Object>.QuadNode currentNode, float _Range)
        {
            return getObjectsInRange(_Position, currentNode, _Range, new List<Searchflag>());
        }

        public List<Object.Object> getObjectsInRange(Vector3 _Position, QuadTree<Object.Object>.QuadNode currentNode, float _Range, List<Searchflag> _SearchFlags)
        {
            Utility.Corpus.Circle circle = new Utility.Corpus.Circle(_Position, _Range);
            List<Object.Object> result = new List<Object.Object>();
            if (currentNode != null)
            {
                Rectangle surroundingRectangle = new Rectangle((int)(circle.Position.X - circle.Radius), (int)(circle.Position.Y - circle.Radius), (int)(circle.Radius * 2), (int)(circle.Radius * 2));

                getObjectsInRange(surroundingRectangle, currentNode/*this.quadTreeObject.Root*/, result, _SearchFlags);
                List<Object.Object> toRemove = new List<Object.Object>();
                foreach (Object.Object var_Object in result)
                {
                    if (Vector3.Distance(var_Object.Position, _Position) > _Range) //TODO: Mit CollisionBounds berechnen, ob Object im Kreis liegt
                    {
                        toRemove.Add(var_Object);
                    }
                }
                foreach (Object.Object var_Object in toRemove)
                {
                    result.Remove(var_Object);
                }
            }
            else
            {
                Logger.Logger.LogErr("getObjectsInRage(currentNode ist null, wahrscheinlich Root eines Quadtrees");
            }
            return result;
        }


        private void getObjectsInRange(Rectangle bounds, QuadTree<Object.Object>.QuadNode currentNode, List<Object.Object> result, List<Searchflag> _SearchFlags)
        {
            if (Utility.Collision.Intersection.RectangleIsInRectangle(bounds, currentNode.Bounds))
            {
                System.Collections.ObjectModel.ReadOnlyCollection<QuadTree<Object.Object>.QuadNode> var_Copy = new System.Collections.ObjectModel.ReadOnlyCollection<QuadTree<Object.Object>.QuadNode>(currentNode.Nodes);

                foreach (QuadTree<Object.Object>.QuadNode node in var_Copy)
                {
                    if (node != null)
                    {
                        if (Utility.Collision.Intersection.RectangleIsInRectangle(bounds, node.Bounds))
                        {
                            getObjectsInRange(bounds, node, result, _SearchFlags);
                        }
                    }
                }

                //Aggrocircle fit into a subnode? then
                addAllObjectsInRange(currentNode, bounds, result, _SearchFlags);
                return;
            }
            if (currentNode.Equals(this.quadTreeObject.Root))
            {
                addAllObjectsInRange(currentNode, bounds, result, _SearchFlags);
            }
        }

        public void addAllObjectsInRange(QuadTree<Object.Object>.QuadNode currentNode, Rectangle bounds, List<Object.Object> result, List<Searchflag> _SearchFlags)
        {
            System.Collections.ObjectModel.ReadOnlyCollection<Object.Object> var_Copy = new System.Collections.ObjectModel.ReadOnlyCollection<Object.Object>(currentNode.Objects);
            //TODO: Zusätzliche Informationen: Die Auflistung wurde geändert. Der Enumerationsvorgang kann möglicherweise nicht ausgeführt werden.
            try
            {
                foreach (Object.Object var_Object in var_Copy)
                {
                    if (!result.Contains(var_Object))// && !var_Object.IsDead)
                    {
                        Boolean containsAllFlags = true;
                        foreach (Searchflag searchFlag in _SearchFlags)
                        {
                            if (!searchFlag.hasFlag(var_Object))
                                containsAllFlags = false;

                        }
                        if (!containsAllFlags)
                            continue;
                        if (var_Object is AnimatedObject)
                        {
                            if (Utility.Collision.Intersection.RectangleIntersectsRectangle(bounds, ((AnimatedObject)var_Object).Bounds.Rectangle)) ///DrawBounds ???
                            {
                                if (var_Object.CollisionBounds != null && var_Object.CollisionBounds.Count > 0)
                                {
                                    foreach (Rectangle collisionBound in var_Object.CollisionBounds)
                                    {
                                        if (Utility.Collision.Intersection.RectangleIntersectsRectangle(bounds, new Rectangle(collisionBound.X + (int)var_Object.Bounds.X, collisionBound.Y + (int)var_Object.Bounds.Y, (int)collisionBound.Width, (int)collisionBound.Height))) // collisionBound ???
                                        {
                                            result.Add(var_Object);
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    result.Add(var_Object);
                                }
                            }
                        }
                    }
                }
                System.Collections.ObjectModel.ReadOnlyCollection<QuadTree<Object.Object>.QuadNode> var_Copy2 = new System.Collections.ObjectModel.ReadOnlyCollection<QuadTree<Object.Object>.QuadNode>(currentNode.Nodes);
                foreach (QuadTree<Object.Object>.QuadNode node in var_Copy2)
                {
                    if (node != null)
                        addAllObjectsInRange(node, bounds, result, _SearchFlags);
                }
            }
            catch (Exception e)
            {
                Logger.Logger.LogErr(e.ToString());
            }
        }
        #endregion
    }
}
