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
#endregion

namespace GameLibrary.Map.World
{
    public partial class World
    {
        #region Collision

        public List<Object.Object> getObjectsColliding(Rectangle bounds)
        {
            return getObjectsColliding(bounds, new List<SearchFlags.Searchflag>());
        }

        public List<Object.Object> getObjectsColliding(Rectangle bounds, List<SearchFlags.Searchflag> _SearchFlags)
        {
            List<Object.Object> result = new List<Object.Object>();
            getObjectsColliding(bounds, this.quadTreeObject.Root, result, _SearchFlags);
            return result;
        }

        private void getObjectsColliding(Rectangle bounds, QuadTree<Object.Object>.QuadNode currentNode, List<Object.Object> result, List<SearchFlags.Searchflag> _SearchFlags)
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
    }
}
