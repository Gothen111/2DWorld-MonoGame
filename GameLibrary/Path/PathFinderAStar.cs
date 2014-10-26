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
using GameLibrary.Map.World;
using GameLibrary.Map.Region;
using GameLibrary.Map.Chunk;
using GameLibrary.Map.Block;
using GameLibrary.Path.AStar;
using GameLibrary.Map.Dimension;
#endregion

namespace GameLibrary.Path
{
    public class MySolver<TPathNode, TUserContext> : SpatialAStar<TPathNode, TUserContext> where TPathNode : IPathNode<TUserContext>
    {
        protected override Double Heuristic(PathNode inStart, PathNode inEnd)
        {
            return Math.Abs(inStart.X - inEnd.X) + Math.Abs(inStart.Y - inEnd.Y);
        }

        protected override Double NeighborDistance(PathNode inStart, PathNode inEnd)
        {
            return Heuristic(inStart, inEnd);
        }

        public MySolver(TPathNode[,] inGrid)
            : base(inGrid)
        {
        }
    }

    public class PathFinderAStar
    {
        public static Path generatePath(Dimension _Dimension, Vector2 _StartPosition, Vector2 _EndPosition)
        {
            try
            {
                int var_SizeX = 20;//(int)Math.Abs(_StartPosition.X - _EndPosition.X)/16 + 2;//20;
                int var_SizeY = 20;//(int)Math.Abs(_StartPosition.Y - _EndPosition.Y)/16 + 2;//20;

                int var_StartX = (int)((_StartPosition.X % (Region.regionSizeX * Chunk.chunkSizeX * Block.BlockSize)) % (Chunk.chunkSizeX * Block.BlockSize) / Block.BlockSize);
                int var_StartY = (int)((_StartPosition.Y % (Region.regionSizeY * Chunk.chunkSizeY * Block.BlockSize)) % (Chunk.chunkSizeY * Block.BlockSize) / Block.BlockSize);          

                int var_TargetX = (int)((_EndPosition.X % (Region.regionSizeX * Chunk.chunkSizeX * Block.BlockSize)) % (Chunk.chunkSizeX * Block.BlockSize) / Block.BlockSize);
                int var_TargetY = (int)((_EndPosition.Y % (Region.regionSizeY * Chunk.chunkSizeY * Block.BlockSize)) % (Chunk.chunkSizeY * Block.BlockSize) / Block.BlockSize);

                var_TargetX = var_TargetX - var_StartX + var_SizeX/2;
                var_TargetY = var_TargetY - var_StartY + var_SizeY/2;

                PathNode[,] grid = new PathNode[var_SizeX, var_SizeY];

                for (int x = 0; x < var_SizeX; x++)
                {
                    for (int y = 0; y < var_SizeY; y++)
                    {
                        int var_X = (int)_StartPosition.X + (-var_SizeX / 2 + x) * Block.BlockSize;
                        int var_Y = (int)_StartPosition.Y + (-var_SizeX / 2 + y) * Block.BlockSize;

                        Block var_Block = _Dimension.getBlockAtCoordinate(new Vector3(var_X, var_Y, 0));
                        if (var_Block != null)
                        {
                            // TODO: Das Problem noch fixxen, falls object in/auf mauer/Wall! WEil dann ommt n komischer pfad ;)
                            bool var_IsWall = false;
                            if (!var_Block.IsWalkAble)
                            {
                                var_IsWall = true;
                            }
                            else if (var_Block.Objects.Count > 0)
                            {
                                var_IsWall = true;
                                if (x == var_SizeX / 2 && y == var_SizeY / 2)
                                {
                                    var_IsWall = false;
                                }
                                if (x == var_TargetX && y == var_TargetY)
                                {
                                    var_IsWall = false;
                                }
                            }
                            grid[x, y] = new PathNode()
                            {
                                // (PROBLEM, ziel ist auch auf not walkable also als mauer markiert!;)) meh oder weniger gefixxt .. 
                                IsWall = var_IsWall,//!var_Block.IsWalkAble || (var_Block.Objects.Count > 0 && x != var_SizeX / 2 && y != var_SizeY / 2 && x != var_TargetX && y != var_TargetY),
                                X = x,
                                Y = y,
                                block = var_Block,
                            };
                        }
                        else
                        {
                            grid[x, y] = new PathNode()
                            {
                                IsWall = true,
                                X = x,
                                Y = y,
                                block = null,
                            };
                        }
                    }
                }

                MySolver<PathNode, System.Object> aStar = new MySolver<PathNode, System.Object>(grid);

                Path var_Result = new Path(aStar.Search(new System.Drawing.Point(var_SizeX / 2, var_SizeY / 2), new System.Drawing.Point(var_TargetX, var_TargetY), null));

                /*for (int y = 0; y < var_SizeY; y++)
                {
                    for (int x = 0; x < var_SizeY; x++)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        if (x == 10 && y == 10)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        foreach(PathNode var_PathNode in var_Result.PathNodes)
                        {
                            if (var_PathNode.X == x && var_PathNode.Y == y)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                            }
                        }
                        if (grid[x, y].IsWall)
                        {
                            Console.Write("x");
                        }
                        else
                        {
                            Console.Write("-");
                        }
                    }
                    Console.WriteLine();
                }*/

                return var_Result;
            }
            catch (Exception ex)
            {
                Logger.Logger.LogErr(ex.ToString());
            }

            return null;
        }
    }
}
