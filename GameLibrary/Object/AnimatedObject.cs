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
using GameLibrary.Object.ObjectEnums;
using GameLibrary.Map.Block;
using GameLibrary.Map.World;
#endregion

namespace GameLibrary.Object
{
    [Serializable()]
    public class AnimatedObject: Object
    {
        private Body.Body body;

        public Body.Body Body
        {
            get { return body; }
            set { body = value; }
        }

        private float scale;

        public float Scale
        {
            get { return scale; }
            set { this.Size /= scale; scale = value; this.Size *= scale; }
        }

        private DirectionEnum directionEnum = DirectionEnum.Down;

        public DirectionEnum DirectionEnum
        {
            get { return directionEnum; }
            set { directionEnum = value; }
        }

        private float movementSpeed;

        public float MovementSpeed
        {
            get { return movementSpeed; }
            set { movementSpeed = value; }
        }

        private bool moveUp;

        public bool MoveUp
        {
            get { return moveUp; }
            set { moveUp = value; }
        }
        private bool moveLeft;

        public bool MoveLeft
        {
            get { return moveLeft; }
            set { moveLeft = value; }
        }
        private bool moveRight;

        public bool MoveRight
        {
            get { return moveRight; }
            set { moveRight = value; }
        }
        private bool moveDown;

        public bool MoveDown
        {
            get { return moveDown; }
            set { moveDown = value; }
        }

        private int updatePositionTimer;
        private int updatePositionTimerMax;

        public AnimatedObject() : base()
        {
            this.body = new Body.Body();
            this.scale = 1f;
            this.Size = new Vector3(32, 32, 0);

            this.movementSpeed = 1.0f;

            this.updatePositionTimerMax = 5;
            this.updatePositionTimer = this.updatePositionTimerMax;
        }

        public AnimatedObject(SerializationInfo info, StreamingContext ctxt) : base(info, ctxt)
        {
            this.scale = (float)info.GetValue("scale", typeof(float));
            this.movementSpeed = (float)info.GetValue("movementSpeed", typeof(float));

            this.directionEnum = (DirectionEnum)info.GetValue("directionEnum", typeof(DirectionEnum));

            this.body = (Body.Body)info.GetValue("body", typeof(Body.Body));

            this.moveUp = (bool)info.GetValue("moveUp", typeof(bool));
            this.moveLeft = (bool)info.GetValue("moveLeft", typeof(bool));
            this.moveRight = (bool)info.GetValue("moveRight", typeof(bool));
            this.moveDown = (bool)info.GetValue("moveDown", typeof(bool));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("scale", this.scale, typeof(float));
            info.AddValue("movementSpeed", this.movementSpeed, typeof(float));

            info.AddValue("directionEnum", this.directionEnum, typeof(DirectionEnum));

            info.AddValue("body", this.body, typeof(Body.Body));

            info.AddValue("moveUp", this.moveUp, typeof(bool));
            info.AddValue("moveLeft", this.moveLeft, typeof(bool));
            info.AddValue("moveRight", this.moveRight, typeof(bool));
            info.AddValue("moveDown", this.moveDown, typeof(bool));

            base.GetObjectData(info, ctxt);
        }

        public override void update(GameTime _GameTime)
        {
            base.update(_GameTime);
            this.body.update(_GameTime);
            this.smoothPosition();
            this.move(_GameTime);
        }

        private void smoothPosition()
        {
            float var_DiffX = (this.NextPosition.X - this.Position.X);
            float var_X;

            float var_DiffY = (this.NextPosition.Y - this.Position.Y);
            float var_Y;
            if (Math.Abs(var_DiffX) <= this.movementSpeed)
            {
                var_X = this.NextPosition.X;
            }
            else
            {
                var_X = this.Position.X + var_DiffX / 2;
            }

            if (Math.Abs(var_DiffY) <= this.movementSpeed)
            {
                var_Y = this.NextPosition.Y;
            }
            else
            {
                var_Y = this.Position.Y + var_DiffY / 2;
            }
            this.Position = new Vector3(var_X, var_Y, this.Position.Z);
        }

        private void move(GameTime _GameTime)
        {
            if (this.CurrentBlock == null)
            {
                this.CurrentBlock = this.getDimensionIsIn().getBlockAtCoordinate(this.Position);
                this.CurrentBlock.addObject(this);
            }

            float var_X = (-Convert.ToInt32(this.moveLeft) + Convert.ToInt32(this.moveRight)) * this.movementSpeed;
            float var_Y = (-Convert.ToInt32(this.moveUp) + Convert.ToInt32(this.moveDown)) * this.movementSpeed;

            if ((Configuration.Configuration.isHost && !(this is PlayerObject)) || Configuration.Configuration.isSinglePlayer || this == Configuration.Configuration.networkManager.client.PlayerObject)
            {              
                Vector2 var_PositionBlockSizeOld = new Vector2((this.Position.X) / Map.Block.Block.BlockSize, (this.Position.Y) / Map.Block.Block.BlockSize);
                Vector2 var_PositionBlockSizeNew = new Vector2((this.Position.X + var_X) / Map.Block.Block.BlockSize, (this.Position.Y + var_Y) / Map.Block.Block.BlockSize);

                Map.Block.Block var_Block = this.CurrentBlock;

                if ((int)var_PositionBlockSizeOld.X < (int)var_PositionBlockSizeNew.X)
                {
                    var_Block = (Map.Block.Block)this.CurrentBlock.RightNeighbour;
                    if (var_Block == null || !var_Block.IsWalkAble)
                    {
                        var_X = 0;
                    }
                }
                else if ((int)var_PositionBlockSizeOld.X > (int)var_PositionBlockSizeNew.X)
                {
                    var_Block = (Map.Block.Block)this.CurrentBlock.LeftNeighbour;
                    if (var_Block == null || !var_Block.IsWalkAble)
                    {
                        var_X = 0;
                    }
                }
                if ((int)var_PositionBlockSizeOld.Y < (int)var_PositionBlockSizeNew.Y)
                {
                    var_Block = (Map.Block.Block)this.CurrentBlock.BottomNeighbour;
                    if (var_Block == null || !var_Block.IsWalkAble)
                    {
                        var_Y = 0;
                    }
                }
                else if ((int)var_PositionBlockSizeOld.Y > (int)var_PositionBlockSizeNew.Y)
                {
                    var_Block = (Map.Block.Block)this.CurrentBlock.TopNeighbour;
                    if (var_Block == null || !var_Block.IsWalkAble)
                    {
                        var_Y = 0;
                    }
                }
            }

            this.Velocity = new Vector3(var_X, var_Y, 0) * (20 / _GameTime.ElapsedGameTime.Milliseconds);

            if ((Configuration.Configuration.isHost && !(this is PlayerObject)) || Configuration.Configuration.isSinglePlayer || this == Configuration.Configuration.networkManager.client.PlayerObject)
            {
                if (this.Velocity.X != 0 || this.Velocity.Y != 0)
                {
                    Rectangle nextBounds = new Rectangle((int)(this.Bounds.Left + this.Velocity.X), (int)(this.Bounds.Top + this.Velocity.Y), (int)this.Bounds.Width, (int)this.Bounds.Height);
                    List<Object> objectsColliding = this.getDimensionIsIn().getObjectsColliding(nextBounds);
                    objectsColliding.Remove(this as LivingObject);
                    if (objectsColliding.Count < 1)
                    {
                        this.NextPosition = this.Position + this.Velocity;
                        this.Position = this.NextPosition;
                        Configuration.Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.UpdateObjectPositionMessage((LivingObject)this), GameLibrary.Connection.GameMessageImportance.UnImportant);
                    }
                }
            }

            if (Configuration.Configuration.isHost && this is PlayerObject)
            {
                Configuration.Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.UpdateObjectPositionMessage((LivingObject)this), GameLibrary.Connection.GameMessageImportance.UnImportant);
            }
            
            if (this.Velocity.X != 0 || this.Velocity.Y != 0)
            {
                checkChangedBlock(); 
                this.body.walk(this.Velocity);
                this.updateMovementDirection();
            }
            else
            {
                this.body.stopWalk();
            }
        }

        private void updateMovementDirection()
        {
            if (OldPosition == this.Position)
            {

            }
            else if (OldPosition.X == this.Position.X)
            {
                if (OldPosition.Y > this.Position.Y)
                {
                    this.DirectionEnum = ObjectEnums.DirectionEnum.Top;
                }
                else if (OldPosition.Y < this.Position.Y)
                {
                    this.DirectionEnum = ObjectEnums.DirectionEnum.Down;
                }
            }
            else if (OldPosition.X > this.Position.X)
            {
                this.DirectionEnum = ObjectEnums.DirectionEnum.Left;
                if (Math.Abs(this.Velocity.X) < Math.Abs(this.Velocity.Y))
                {
                    if (OldPosition.Y > this.Position.Y)
                    {
                        this.DirectionEnum = ObjectEnums.DirectionEnum.Top;
                    }
                    else if (OldPosition.Y < this.Position.Y)
                    {
                        this.DirectionEnum = ObjectEnums.DirectionEnum.Down;
                    }
                }
            }
            else if (OldPosition.X < this.Position.X)
            {
                this.DirectionEnum = ObjectEnums.DirectionEnum.Right;
                if (Math.Abs(this.Velocity.X) < Math.Abs(this.Velocity.Y))
                {
                    if (OldPosition.Y > this.Position.Y)
                    {
                        this.DirectionEnum = ObjectEnums.DirectionEnum.Top;
                    }
                    else if (OldPosition.Y < this.Position.Y)
                    {
                        this.DirectionEnum = ObjectEnums.DirectionEnum.Down;
                    }
                }
            }
            this.body.setDirection(this.directionEnum);
        }

        public virtual void onCollide(AnimatedObject _CollideWith)
        {
        }

        public virtual void draw(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch, Vector3 _DrawPositionExtra, Color _Color)
        {
            //TODO: An das Attribut Scale anpassen
            //Vector2 var_Position = new Vector2(this.Position.X + _DrawPositionExtra.X - this.Size.X/2, this.Position.Y + _DrawPositionExtra.Y - this.Size.Y);
            Vector2 var_Position = new Vector2(this.Bounds.X, this.Bounds.Y) + new Vector2(_DrawPositionExtra.X, _DrawPositionExtra.Y);

            /*if (this.animation.drawColor() != Color.White)
            {
                var_DrawColor = Color.Lerp(this.objectDrawColor, this.animation.drawColor(), 0.1f);
            }*/

            this.body.draw(_GraphicsDevice, _SpriteBatch, var_Position);
        }

        public virtual void onChangedBlock()
        {
        }

        public virtual void onChangedChunk()
        {
        }

        public void checkChangedBlock()
        {
            //TODO: Methode vebessern, über world get block usw ... vll ;) vll ist das ja auch nicht schneller :D
            if (this.CurrentBlock != null)
            {
                bool var_BlockChanged = false;
                Block var_OldBlock = this.CurrentBlock;

                int var_BlockPosX = (int)this.CurrentBlock.Position.X / Map.Block.Block.BlockSize;
                int var_BlockPosY = (int)this.CurrentBlock.Position.Y / Map.Block.Block.BlockSize;

                Vector3 var_Position = this.Position;

                if (var_Position.X < var_BlockPosX * Map.Block.Block.BlockSize)
                {
                    this.CurrentBlock.removeObject(this);
                    if (this.CurrentBlock.LeftNeighbour != null)
                    {
                        ((Map.Block.Block)this.CurrentBlock.LeftNeighbour).addObject(this);
                        var_BlockChanged = true;
                    }
                }
                else if (var_Position.X > (var_BlockPosX + 1) * Map.Block.Block.BlockSize)
                {
                    this.CurrentBlock.removeObject(this);
                    if (this.CurrentBlock.RightNeighbour != null)
                    {
                        ((Map.Block.Block)this.CurrentBlock.RightNeighbour).addObject(this);
                        var_BlockChanged = true;
                    }
                }
                else if (var_Position.Y < var_BlockPosY * Map.Block.Block.BlockSize)
                {
                    this.CurrentBlock.removeObject(this);
                    if (this.CurrentBlock.TopNeighbour != null)
                    {
                        ((Map.Block.Block)this.CurrentBlock.TopNeighbour).addObject(this);
                        var_BlockChanged = true;
                    }
                }
                else if (var_Position.Y > (var_BlockPosY + 1) * Map.Block.Block.BlockSize)
                {
                    this.CurrentBlock.removeObject(this);
                    if (this.CurrentBlock.BottomNeighbour != null)
                    {
                        ((Map.Block.Block)this.CurrentBlock.BottomNeighbour).addObject(this);
                        var_BlockChanged = true;
                    }
                }

                if (var_BlockChanged)
                {
                    this.onChangedBlock();

                    this.CurrentBlock.onObjectEntersBlock(this);

                    if (var_OldBlock.Parent != this.CurrentBlock.Parent)
                    {
                        this.onChangedChunk();
                    }
                }
            }
            else
            {
                Logger.Logger.LogErr("AnimatedObject->checkChangedBlock() : this.CurrentBlock = null");
            }
        }
    }
}
