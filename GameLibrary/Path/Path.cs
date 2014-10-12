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
using GameLibrary.Object;
#endregion

namespace GameLibrary.Path
{
    public class Path
    {
        private LinkedList<PathNode> pathNodes;

        public LinkedList<PathNode> PathNodes
        {
            get { return pathNodes; }
            set { pathNodes = value; }
        }
        private bool finished;

        public Path()
        {
            this.finished = false;
        }

        public Path(LinkedList<PathNode> _PathNodes)
            :this()
        {
            this.pathNodes = _PathNodes;
        }

        private void removeFirst()
        {
            this.pathNodes.RemoveFirst();
        }

        private PathNode getNextNode()
        {
            if (this.pathNodes.Count > 0)
            {
                return this.pathNodes.First();
            }
            return null;
        }

        private PathNode extractFirst()
        {
            PathNode var_PathNode = null;
            if(this.pathNodes.Count > 0)
            {
                var_PathNode = this.getNextNode();
                this.removeFirst();

            }
            return var_PathNode;
        }

        private bool isInRange(Vector2 _Position, Vector2 _TargetPosition, int _Range)
        {
            bool var_Result = false;

            if (Math.Sqrt(Math.Pow(_Position.X - _TargetPosition.X, 2) + Math.Pow(_Position.Y - _TargetPosition.Y, 2)) <= _Range)
            {
                var_Result = true;
            }

            return var_Result;
        }

        public void moveOnPath(LivingObject _LivingObject)
        {
            if (!this.finished && this.pathNodes != null)
            {
                if (_LivingObject.CurrentBlock != null)
                {
                    PathNode var_NextNode = this.getNextNode();
                    if (var_NextNode != null)
                    {
                        if (this.isInRange(new Vector2(_LivingObject.Position.X, _LivingObject.Position.Y), new Vector2(var_NextNode.block.Position.X + 16, var_NextNode.block.Position.Y + 16), (int)_LivingObject.MovementSpeed*15))
                        {
                            var_NextNode = this.extractFirst();
                        }

                        if (var_NextNode != null)
                        {
                            _LivingObject.MoveRight = false;
                            _LivingObject.MoveLeft = false;
                            _LivingObject.MoveDown = false;
                            _LivingObject.MoveUp = false;
                            if (var_NextNode.block.Position.X + 16  > _LivingObject.Position.X)
                            {
                                _LivingObject.MoveRight = true;
                            }
                            else if (var_NextNode.block.Position.X + 16 < _LivingObject.Position.X)
                            {
                                _LivingObject.MoveLeft = true;
                            }
                            if (var_NextNode.block.Position.Y + 16 > _LivingObject.Position.Y)
                            {
                                _LivingObject.MoveDown = true;
                            }
                            else if (var_NextNode.block.Position.Y + 16 < _LivingObject.Position.Y)
                            {
                                _LivingObject.MoveUp = true;
                            }
                        }
                        else
                        {
                            this.finished = true;
                        }
                    }
                    else
                    {
                        this.finished = true;
                    }
                }
            }
        }
    }
}
