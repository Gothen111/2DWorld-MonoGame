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
using GameLibrary.Connection;
#endregion

namespace GameLibrary.Map.World
{
    public partial class World
    {
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

        public Object.Object addObject(Object.Object _Object, Boolean insertInQuadTree)
        {
            Region.Region region = this.getRegionObjectIsIn(_Object);
            return addObject(_Object, insertInQuadTree, region);
        }

        public Object.Object addObject(Object.Object _Object, Boolean insertInQuadTree, Region.Region _Region)
        {
            if (insertInQuadTree)
            {
                this.quadTreeObject.Insert(_Object);
            }
            if (_Region != null)
            {
                Chunk.Chunk chunk = _Region.getChunkObjectIsIn(_Object);
                if (chunk != null)
                {
                    if(this.getObject(_Object.Id)==null)
                    {
                        Block.Block var_Block = chunk.getBlockAtCoordinate(_Object.Position);
                        if (var_Block != null)
                        {
                            var_Block.addObject(_Object);
                            /*if (insertInQuadTree)
                            {
                                this.quadTreeObject.Insert(_Object);
                            }*/
                            if (Configuration.Configuration.isHost)
                            {
                                Configuration.Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.UpdateObjectMessage(_Object), GameMessageImportance.VeryImportant);
                            }
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
            return getObjectsInRange(_Position, this.quadTreeObject.Root, _Range, new List<SearchFlags.Searchflag>());
        }

        public List<Object.Object> getObjectsInRange(Vector3 _Position, float _Range, List<SearchFlags.Searchflag> _SearchFlags)
        {
            return getObjectsInRange(_Position, this.quadTreeObject.Root, _Range, _SearchFlags);
        }

        public List<Object.Object> getObjectsInRange(Vector3 _Position, QuadTree<Object.Object>.QuadNode currentNode, float _Range)
        {
            return getObjectsInRange(_Position, currentNode, _Range, new List<SearchFlags.Searchflag>());
        }

        public List<Object.Object> getObjectsInRange(Vector3 _Position, QuadTree<Object.Object>.QuadNode currentNode, float _Range, List<SearchFlags.Searchflag> _SearchFlags)
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


        private void getObjectsInRange(Rectangle bounds, QuadTree<Object.Object>.QuadNode currentNode, List<Object.Object> result, List<SearchFlags.Searchflag> _SearchFlags)
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
            else//if (currentNode.Equals(this.quadTreeObject.Root))
            {
                addAllObjectsInRange(currentNode, bounds, result, _SearchFlags);
            }
        }

        public void addAllObjectsInRange(QuadTree<Object.Object>.QuadNode currentNode, Rectangle bounds, List<Object.Object> result, List<SearchFlags.Searchflag> _SearchFlags)
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
                        foreach (SearchFlags.Searchflag searchFlag in _SearchFlags)
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
