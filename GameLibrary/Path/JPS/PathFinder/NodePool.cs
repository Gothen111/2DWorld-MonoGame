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
#endregion

namespace GameLibrary.Path.JPS.EpPathFinding
{
    public class NodePool
    {
        protected Dictionary<GridPos, Node> m_nodes;

        public NodePool()
        {
            m_nodes = new Dictionary<GridPos, Node>();
        }

        public Dictionary<GridPos, Node> Nodes
        {
            get { return m_nodes; }
        }
        public Node GetNode(int iX, int iY)
        {
            GridPos pos = new GridPos(iX, iY);
            return GetNode(pos);
        }

        public Node GetNode(GridPos iPos)
        {
            if (m_nodes.ContainsKey(iPos))
                return m_nodes[iPos];
           return null;
        }

        public Node SetNode(int iX, int iY, bool? iWalkable = null)
        {
            GridPos pos = new GridPos(iX, iY);
            return SetNode(pos,iWalkable);
        }

        public Node SetNode(GridPos iPos, bool? iWalkable = null)
        {
            if (iWalkable.HasValue)
            {
                if (iWalkable.Value == true)
                {
                    if (m_nodes.ContainsKey(iPos))
                        return m_nodes[iPos];
                    Node newNode = new Node(iPos.x, iPos.y, iWalkable);
                    m_nodes.Add(iPos, newNode);
                    return newNode;
                }
                else
                {
                    removeNode(iPos);
                }
                
            }
            else
            {
                Node newNode = new Node(iPos.x, iPos.y, true);
                m_nodes.Add(iPos, newNode);
                return newNode;
            }
            return null;
        }
        protected void removeNode(int iX, int iY)
        {
            GridPos pos = new GridPos(iX, iY);
            removeNode(pos);
        }
        protected void removeNode(GridPos iPos)
        {
            if (m_nodes.ContainsKey(iPos))
                m_nodes.Remove(iPos);
        }
    }
}