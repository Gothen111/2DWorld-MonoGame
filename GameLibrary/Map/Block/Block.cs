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
using GameLibrary.Connection.Message;
using GameLibrary.Connection;
using GameLibrary.Enums;
#endregion

namespace GameLibrary.Map.Block
{
    [Serializable()]
    public class Block : Box
    {
        public static int BlockSize = 32;

        private BlockEnum[] layer;

        public BlockEnum[] Layer
        {
            get { return layer; }
            set { layer = value; }
        }

        private List<Object.Object> objects;

        public List<Object.Object> Objects
        {
            get { return objects; }
            set { objects = value; }
        }

        private List<Object.Object> objectsPreEnviorment;

        public List<Object.Object> ObjectsPreEnviorment
        {
            get { return objectsPreEnviorment; }
            set { objectsPreEnviorment = value; }
        }

        private bool isWalkAble;

        public bool IsWalkAble
        {
            get { return this.layer[0] == BlockEnum.Ground2 ? false : true; }
            set { isWalkAble = value; }
        }

        private int height;

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        private float nextLightLevel;

        public float NextLightLevel
        {
            get { return nextLightLevel; }
            set { nextLightLevel = value; }
        }

        private float lightAdmit = 0;

        public float LightAdmit
        {
            get { return lightAdmit; }
            set { lightAdmit = value; }
        }

        private float lightAbsorb;

        public float LightAbsorb
        {
            get { return lightAbsorb; }
            set { lightAbsorb = value; }
        }

        private float lightShadow;

        public float LightShadow
        {
            get { return lightShadow; }
            set { lightShadow = value; }
        }


        public Block(int _PosX, int _PosY, BlockEnum _BlockEnum, Chunk.Chunk _ParentChunk)
            :base()
        {
            this.layer = new BlockEnum[Enum.GetValues(typeof(BlockLayerEnum)).Length];
            this.layer[0] = _BlockEnum;
            this.objects = new List<Object.Object>();
            this.Position = new Vector3(_PosX, _PosY, 0);
            this.Parent = _ParentChunk;

            objectsPreEnviorment = new List<Object.Object>();

            this.isWalkAble = true;
            this.height = 0;
            this.Size = new Vector3(Block.BlockSize, Block.BlockSize, 0);
        }

        public Block(SerializationInfo info, StreamingContext ctxt) 
            :base(info, ctxt)
        {
            this.layer = (BlockEnum[])info.GetValue("layer", typeof(BlockEnum[]));
            this.objects = (List<Object.Object>)info.GetValue("objects", typeof(List<Object.Object>));
            foreach (Object.Object var_Object in this.objects)
            {
                GameLibrary.Map.World.World.world.QuadTreeObject.Insert(var_Object);
                var_Object.CurrentBlock = this;
            }
            this.objectsPreEnviorment = (List<Object.Object>)info.GetValue("objectsPreEnviorment", typeof(List<Object.Object>));
            foreach (Object.Object var_Object in this.objectsPreEnviorment)
            {
                var_Object.CurrentBlock = this;
            }
            this.height = (int)info.GetValue("height", typeof(int));
            this.isWalkAble = (bool)info.GetValue("isWalkAble", typeof(bool));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
            info.AddValue("layer", this.layer, typeof(BlockEnum[]));
            info.AddValue("objects", this.objects, this.objects.GetType());
            info.AddValue("objectsPreEnviorment", this.objectsPreEnviorment, this.objectsPreEnviorment.GetType());
            info.AddValue("height", this.height, typeof(int));
            info.AddValue("isWalkAble", this.isWalkAble, typeof(bool));
        }

        public void setLayerAt(Enum _Enum, BlockLayerEnum _Id)
        {
            int x = (int)_Id;
            this.layer[(int)_Id] = (BlockEnum)_Enum;
        }

        public void setFirstLayer(Enum _Enum)
        {
            this.layer[0] = (BlockEnum)_Enum;
            if (_Enum is BlockEnum)
            {
                if ((BlockEnum)_Enum == BlockEnum.Wall)
                {
                    this.isWalkAble = false;
                }
            }
        }

        public void addObject(Object.Object _Object)
        {
            if (!this.objects.Contains(_Object))
            {
                this.objects.Add(_Object);
            }
            _Object.CurrentBlock = this;
        }

        public void removeObject(Object.Object _Object)
        {
            this.objects.Remove(_Object);
            //_Object.CurrentBlock = null;
        }

        public void addPreEnvironmentObject(Object.Object _Object)
        {
            if (!this.objectsPreEnviorment.Contains(_Object))
            {
                this.objectsPreEnviorment.Add(_Object);
            }
            _Object.CurrentBlock = this;
        }

        public void removePreEnvironmentObject(Object.Object _Object)
        {
            this.objectsPreEnviorment.Remove(_Object);
            //_Object.CurrentBlock = null;
        }

        public override void update(GameTime _GameTime)
        {
            base.update(_GameTime);
            /*foreach (Object.LivingObject var_LivingObject in objects.Reverse<Object.Object>())
            {
                if (var_LivingObject.IsDead)
                {
                    //this.objects.Remove(var_LivingObject);
                }
                else
                {
                    var_LivingObject.update();
                }
            }*/
            //this.calculateLightLevel();
            //this.LightLevel = this.nextLightLevel;
            if (Setting.Setting.lightTwo)
            {
                float var_SmoothFactor = 30; // 100 ist Fackel

                if (Math.Abs(this.LightLevel - this.nextLightLevel) <= 1 / var_SmoothFactor)
                {
                    this.LightLevel = this.nextLightLevel;
                }

                if (this.LightLevel == this.nextLightLevel)
                {
                }
                else
                {
                    float var_Diff = this.LightLevel - this.nextLightLevel;
                    this.LightLevel -= var_Diff / (var_SmoothFactor);
                }
            }

            /*for (int i = 0; i < this.objects.Count; i++)
            {
                this.objects[i].LightLevel = this.LightLevel;
            }
            for (int i = 0; i < this.objectsPreEnviorment.Count; i++)
            {
                this.objectsPreEnviorment[i].LightLevel = this.LightLevel;
            }*/
        }

        public void calculateLightLevelLeft()
        {
            if (this.LeftNeighbour != null)
            {
                this.LightLevel = Math.Max(this.LightLevel, ((Block)this.LeftNeighbour).LightLevel - this.lightAbsorb);
                //this.lightColor = Color.Lerp(((Block)this.LeftNeighbour).lightColor * ((Block)this.LeftNeighbour).lightLevel, this.lightColor * this.lightLevel, 0.5f);
            }
        }

        public void calculateLightLevelRight()
        {
            if (this.RightNeighbour != null)
            {
                this.LightLevel = Math.Max(this.LightLevel, ((Block)this.RightNeighbour).LightLevel - this.lightAbsorb);
                //this.lightColor = Color.Lerp(((Block)this.RightNeighbour).lightColor * ((Block)this.RightNeighbour).lightLevel, this.lightColor * this.lightLevel, 0.5f);
            }
        }

        public void calculateLightLevelTop()
        {
            if (this.TopNeighbour != null)
            {
                this.LightLevel = Math.Max(this.LightLevel, ((Block)this.TopNeighbour).LightLevel - this.lightAbsorb);
                //this.lightColor = Color.Lerp(((Block)this.TopNeighbour).lightColor * ((Block)this.TopNeighbour).lightLevel, this.lightColor * this.lightLevel, 0.5f);
            }
        }

        public void calculateLightLevelBottom()
        {
            if (this.BottomNeighbour != null)
            {
                this.LightLevel = Math.Max(this.LightLevel, ((Block)this.BottomNeighbour).LightLevel - this.lightAbsorb);
                //this.lightColor = Color.Lerp(((Block)this.BottomNeighbour).lightColor * ((Block)this.BottomNeighbour).lightLevel, this.lightColor * this.lightLevel, 0.5f);
            }
        }

        public void calculateLightLevel()
        {
            //Color var_Color = this.lightColor;

            if (this.LeftNeighbour != null)
            {
                this.LightLevel = (this.LightLevel + ((Block)this.LeftNeighbour).LightLevel) / 2;
                //var_Color = Color.Lerp(var_Color, ((Block)this.LeftNeighbour).lightColor, 0.9f);
            }
            if (this.RightNeighbour != null)
            {
                this.LightLevel = (this.LightLevel + ((Block)this.RightNeighbour).LightLevel) / 2;
                //var_Color = Color.Lerp(var_Color, ((Block)this.RightNeighbour).lightColor, 0.9f);
            }
            if (this.TopNeighbour != null)
            {
                this.LightLevel = (this.LightLevel + ((Block)this.TopNeighbour).LightLevel) / 2;
                //var_Color = Color.Lerp(var_Color, ((Block)this.TopNeighbour).lightColor, 0.9f);
            }
            if (this.BottomNeighbour != null)
            {
                this.LightLevel = (this.LightLevel + ((Block)this.BottomNeighbour).LightLevel) / 2;
                //var_Color = Color.Lerp(var_Color, ((Block)this.BottomNeighbour).lightColor, 0.9f);
            }

            //this.lightColor = var_Color;
            //this.lightLevel = Math.Min(1,Math.Max(this.lightLevel, var_LightLevelNeighbours / 4));
        }

        public virtual void onObjectEntersBlock(Object.Object var_Object)
        {
        }

        public void drawBlock(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch)
        {
            Vector2 var_DrawPosition = new Vector2(this.Position.X, this.Position.Y);

            Color var_Color = Color.White;

            Color var_Lerp = Color.Lerp(this.DrawColor, this.LightColor, 0.8f);

            if (Setting.Setting.debugMode)
            {
                /*if (this.objects.Count > 0)
                {
                    var_Color = Color.Green;
                }
                else
                {*/
                    var_Color = this.DrawColor;
                    //if (var_Color == Color.White)
                    //{
                        /*if (this.lightLevel <= 0.1f)
                        {
                            this.lightLevel = 0.0f;
                        }
                        else if (this.lightLevel <= 0.4f)
                        {
                            this.lightLevel = 0.4f;
                        }
                        else if (this.lightLevel <= 0.6f)
                        {
                            this.lightLevel = 0.6f;
                        }
                        else if (this.lightLevel <= 1.0f)
                        {
                            this.lightLevel = 1.0f;
                        }*/
                    int var_AmountGrey = (int)(255 * this.LightLevel);
                        //Color var_Lerp = Color.Lerp(this.drawColor, this.lightColor, 0.8f); // this.lightColor
                    //int var_AmountRed = (int)(var_Lerp.R * this.LightLevel);
                    //int var_AmountGreen = (int)(var_Lerp.G * this.LightLevel);
                    //int var_AmountBlue = (int)(var_Lerp.B * this.LightLevel);

                        float correctionFactor = 0.1f;
                        int var_AmountRed = (int)(((255 - var_Lerp.R) * correctionFactor + var_Lerp.R) * this.LightLevel);
                        int var_AmountGreen = (int)(((255 - var_Lerp.G) * correctionFactor + var_Lerp.G) * this.LightLevel);
                        int var_AmountBlue = (int)(((255 - var_Lerp.B) * correctionFactor + var_Lerp.B) * this.LightLevel);

                        var_Color = new Color(var_AmountRed, var_AmountGreen, var_AmountBlue);
                    //}
                //}
            }

            String var_RegionType = ((Region.Region)this.Parent.Parent).RegionEnum.ToString();

            String var_TexturePath = "Region/" + var_RegionType + "/" + var_RegionType;

            /*for(int i = 0; i < this.height; i++)
            {
                _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Region/" + var_RegionType + "/Block/" + "Wall"], var_DrawPosition - new Vector2(0, BlockSize * i), var_Color);
            }*/

            BlockLayerEnum var_Layer = BlockLayerEnum.Layer1;
            while ((int)var_Layer < this.layer.Length)
            {
                BlockEnum var_Enum = this.layer[(int)var_Layer];
                if (var_Enum != BlockEnum.Nothing)
                {
                    _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[var_TexturePath], var_DrawPosition, new Rectangle((int)(var_Enum - 1) * BlockSize, (int)(var_Layer) * BlockSize, BlockSize, BlockSize), var_Color);
                    //_SpriteBatch.DrawString(GameLibrary.Ressourcen.RessourcenManager.ressourcenManager.Fonts["Arial"], ((int)(this.LightLevel*10)).ToString(), new Vector2(this.Position.X, this.Position.Y), Color.White,0,Vector2.Zero,0.5f,SpriteEffects.None,0.0f);
                    //_SpriteBatch.DrawString(GameLibrary.Ressourcen.RessourcenManager.ressourcenManager.Fonts["Arial"], ("R:" + var_Lerp.R + "\n" + "G:" + var_Lerp.G + "\n" + "B:" + var_Lerp.B + "\n"), new Vector2(this.Position.X, this.Position.Y), Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0.0f);
                
                }
                var_Layer += 1;
            }
        }

        public static Vector3 parsePosition(Vector3 _Position)
        {
            int _PosX = (int)_Position.X;
            int _PosY = (int)_Position.Y;

            int var_SizeX = (Block.BlockSize);
            int var_SizeY = (Block.BlockSize);

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

            return new Vector3(_PosX, _PosY, 0);
        }

        public override void requestFromServer()
        {
            base.requestFromServer();
            if (this.Parent == null)
            {           
            }
            else
            {
                Configuration.Configuration.networkManager.addEvent(new RequestBlockMessage(this.Position), GameMessageImportance.VeryImportant);                   
            }
        }
    }
}
