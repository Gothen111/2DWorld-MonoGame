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
using GameLibrary.Connection.Message;
using GameLibrary.Connection;
#endregion

#region Using Statements Class Specific
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
            get { return isWalkAble; }
            set { isWalkAble = value; }
        }

        private int height;

        public int Height
        {
            get { return height; }
            set { height = value; }
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
        }

        public void drawBlock(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch)
        {
            Vector2 var_DrawPosition = new Vector2(this.Position.X, this.Position.Y);

            Color var_Color = Color.White;

            if (Setting.Setting.debugMode)
            {
                if (this.objects.Count > 0)
                {
                    var_Color = Color.Green;
                }
            }

            String var_RegionType = ((Region.Region)this.Parent.Parent).RegionEnum.ToString();

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
                    _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Region/" + var_RegionType + "/" + var_RegionType], var_DrawPosition, new Rectangle((int)(var_Enum-1) * BlockSize, (int)(var_Layer) * BlockSize, BlockSize, BlockSize), var_Color);
                }
                var_Layer += 1;
            }
        }

        public static Vector2 parsePosition(Vector2 _Position)
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

            return new Vector2(_PosX, _PosY);
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
