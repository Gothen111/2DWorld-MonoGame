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
using GameLibrary.Map.World;
using GameLibrary.Map.Region;
using GameLibrary.Map.Chunk;
using GameLibrary.Map.Block;
using GameLibrary.Enums;
#endregion

namespace GameLibrary.Factory
{
    public class FarmFactory
    {
        public static FarmFactory farmFactory = new FarmFactory();

        public void generateFarms(Region _Region, int _MaxCount, int _MinDistance)
        {
            int var_StartPositionX = (int)_Region.Position.X;
            int var_StartPositionY = (int)_Region.Position.Y;

            int var_EndPositionX = var_StartPositionX + (int)_Region.Size.X * Chunk.chunkSizeX * Block.BlockSize;
            int var_EndPositionY = var_StartPositionY + (int)_Region.Size.Y * Chunk.chunkSizeY * Block.BlockSize;

            int var_Count = Utility.Random.Random.GenerateGoodRandomNumber(0, _MaxCount);
            var_Count = _MaxCount;
            for (int i = 0; i < var_Count; i++)
            {
                EnvironmentObject var_EnvironmentObject = EnvironmentFactory.environmentFactory.createEnvironmentObject(_Region.RegionEnum, EnvironmentEnum.FarmHouse1);
                var_EnvironmentObject.Position = new Microsoft.Xna.Framework.Vector3(500, 500, 0);
                //var_EnvironmentObject.CollisionBounds.Add(new Microsoft.Xna.Framework.Rectangle(var_EnvironmentObject.DrawBounds.Left + 40, var_EnvironmentObject.DrawBounds.Bottom - 105, 280, 65)); 
                ((World)_Region.Parent).addObject(var_EnvironmentObject,true, _Region); // Region wird erst world zugewiesen. dannach könne erst objetek hin :(
            }
        }
    }
}
