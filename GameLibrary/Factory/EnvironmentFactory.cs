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
using GameLibrary.Factory.FactoryEnums;
using GameLibrary.Object.Interaction.Interactions;
#endregion

namespace GameLibrary.Factory
{
    public class EnvironmentFactory
    {
        public static EnvironmentFactory environmentFactory = new EnvironmentFactory();

        private EnvironmentFactory()
        {
        }

        public EnvironmentObject createEnvironmentObject(RegionEnum _RegionEnum, EnvironmentEnum objectType)
        {
            EnvironmentObject environmentObject = new EnvironmentObject();
            environmentObject.EnvironmentEnum = objectType;

            switch(objectType)
            {
                case EnvironmentEnum.Tree_Normal_1:
                    {
                        //_SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Region/" + var_RegionType + "/" + var_RegionType], var_DrawPosition, new Rectangle((int)(var_Enum-1) * BlockSize, (int)(var_Layer) * BlockSize, BlockSize, BlockSize), var_Color);
                        environmentObject.Body.MainBody.TexturePath = "Region/Environment/Tree_Normal";
                        environmentObject.Size = new Microsoft.Xna.Framework.Vector3(64, 64, 0);
                        environmentObject.Body.setSize(new Microsoft.Xna.Framework.Vector3(64, 64, 0));

                        int var_ShiftX = 0;
                        int var_ShiftY = (int)(((int)_RegionEnum) * environmentObject.Size.Y);

                        environmentObject.Body.MainBody.StandartTextureShift = new Vector2(var_ShiftX, var_ShiftY);

                        environmentObject.CollisionBounds.Add(new Rectangle(22, 45, 12, 2));
                        break;
                    }
                case EnvironmentEnum.Flower_1:
                    {
                        environmentObject.Body.MainBody.TexturePath = "Region/" + _RegionEnum.ToString() + "/Block/Environment/Flower/Flower1";
                        environmentObject.Size = new Microsoft.Xna.Framework.Vector3(32, 32, 0);
                        environmentObject.Body.MainBody.StandartTextureShift = new Microsoft.Xna.Framework.Vector2(Utility.Random.Random.GenerateGoodRandomNumber(0, 9) * 32, 0);
                        break;
                    }
                case EnvironmentEnum.Plant:
                    {
                        break;
                    }
                case EnvironmentEnum.Tree_Brown:
                    {
                        break;
                    }
                case EnvironmentEnum.Tree_Grey:
                    {
                        break;
                    }
                case EnvironmentEnum.Chest:
                    {
                        environmentObject.Body.MainBody.TexturePath = "Region/" + _RegionEnum.ToString() + "/Block/Environment/Chest/Chest";
                        environmentObject.Size = new Microsoft.Xna.Framework.Vector3(32, 48, 0);
                        environmentObject.Body.MainBody.StandartTextureShift = new Microsoft.Xna.Framework.Vector2(1*32, 0);
                        environmentObject.Interactions.Add(new ChestInteraction(environmentObject));
                        break;
                    }
                case EnvironmentEnum.FarmHouse1:
                    {
                        environmentObject.Body.MainBody.TexturePath = "Region/" + _RegionEnum.ToString() + "/Block/Environment/Farm/FarmHouse1";
                        environmentObject.Size = new Microsoft.Xna.Framework.Vector3(370, 355, 0);
                        break;
                    }
            }
            return environmentObject;
        }
    }
}
