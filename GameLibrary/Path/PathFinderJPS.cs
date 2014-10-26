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
using GameLibrary.Path.JPS.EpPathFinding;
using GameLibrary.Path.JPS.General;
using GameLibrary.Map.Dimension;
#endregion

namespace GameLibrary.Path
{
    public class PathFinderJPS
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

                BaseGrid searchGrid = new DynamicGridWPool(SingletonHolder<NodePool>.Instance);
                JumpPointParam jumpParam = new JumpPointParam(searchGrid, true, true, false, HeuristicMode.EUCLIDEAN);

                GridPos startPos = new GridPos(var_SizeX / 2, var_SizeX / 2);
                GridPos endPos = new GridPos(var_TargetX, var_TargetY);
                for (int x = 0; x < var_SizeX; x++)
                {
                    for (int y = 0; y < var_SizeY; y++)
                    {
                        int var_X = (int)_StartPosition.X + (-var_SizeX / 2 + x) * Block.BlockSize;
                        int var_Y = (int)_StartPosition.Y + (-var_SizeX / 2 + y) * Block.BlockSize;

                        Block var_Block = _Dimension.getBlockAtCoordinate(new Vector3(var_X, var_Y, 0));
                        bool var_IsWalkAble = false;
                        if (var_Block != null)
                        {
                            if (var_Block.IsWalkAble)
                            {
                                var_IsWalkAble = true;
                            }
                            else if (var_Block.Objects.Count > 0)
                            {
                                if (x == var_SizeX / 2 && y == var_SizeY / 2)
                                {
                                    var_IsWalkAble = true;
                                }
                                if (x == var_TargetX && y == var_TargetY)
                                {
                                    var_IsWalkAble = true;
                                }
                            }
                        }

                        searchGrid.SetWalkableAt(new GridPos(x, y), var_IsWalkAble);
                    }
                }
                jumpParam.CrossCorner = true;
                jumpParam.CrossAdjacentPoint = false;
                jumpParam.UseRecursive = false; // KP ;D
                jumpParam.Reset(startPos, endPos);
                List<GridPos> resultList = JumpPointFinder.FindPath(jumpParam);


                LinkedList<PathNode> var_PathNodes = new LinkedList<PathNode>();

                foreach (GridPos var_GridPos in resultList)
                {
                    PathNode var_PathNode = new PathNode();
                    var_PathNode.X = var_GridPos.x;
                    var_PathNode.Y = var_GridPos.y;

                    int var_X = (int)_StartPosition.X + (-var_SizeX / 2 + var_PathNode.X) * Block.BlockSize;
                    int var_Y = (int)_StartPosition.Y + (-var_SizeX / 2 + var_PathNode.Y) * Block.BlockSize;

                    Block var_Block = _Dimension.getBlockAtCoordinate(new Vector3(var_X, var_Y, 0));

                    var_PathNode.block = var_Block;

                    var_PathNodes.AddLast(var_PathNode);
                }


                /*for (int y = 0; y < var_SizeY; y++)
                {
                    for (int x = 0; x < var_SizeY; x++)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        if (x == 10 && y == 10)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        foreach (PathNode var_PathNode in var_PathNodes)
                        {
                            if (var_PathNode.X == x && var_PathNode.Y == y)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                            }
                        }
                        if (!searchGrid.IsWalkableAt(x,y))
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

                Path var_Result = new Path(var_PathNodes);
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
