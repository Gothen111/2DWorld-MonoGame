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
using GameLibrary.Object.Task;
using GameLibrary.Object.Interaction;
using GameLibrary.Enums;
using GameLibrary.Connection;
#endregion

namespace GameLibrary.Object
{
    [Serializable()]
    public class LivingObject : AnimatedObject
    {
        private float healthPoints;

        public float HealthPoints
        {
            get { return healthPoints; }
            set { healthPoints = value; }
        }

        private float maxHealthPoints;

        public float MaxHealthPoints
        {
            get { return maxHealthPoints; }
            set { maxHealthPoints = value; }
        }

        private bool isDead;

        public bool IsDead
        {
            get { return isDead; }
            set { isDead = value; }
        }

        protected GenderEnum gender;

        public GenderEnum Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        private float aggroRange;

        public float AggroRange
        {
            get { return aggroRange; }
            set { aggroRange = value; }
        }

        private Task.Aggro.AggroSystem<LivingObject> aggroSystem;

        public Task.Aggro.AggroSystem<LivingObject> AggroSystem
        {
            get { return aggroSystem; }
            set { aggroSystem = value; }
        }

        private List<LivingObjectInteraction> interactions;

        internal List<LivingObjectInteraction> Interactions
        {
            get { return interactions; }
            set { interactions = value; }
        }

        private List<LivingObjectTask> tasks;

        public List<LivingObjectTask> Tasks
        {
            get { return tasks; }
            set { tasks = value; }
        }

        private LivingObjectTask currentTask;

        private bool canBeEffected; // Wie vergiften oder knockback usw.....

        public bool CanBeEffected
        {
            get { return canBeEffected; }
            set { canBeEffected = value; }
        }

        private Path.Path path;

        public Path.Path Path
        {
            get { return path; }
            set { path = value; }
        }

        public LivingObject()
            : base()
        {
            this.healthPoints = 20;
            this.maxHealthPoints = 20;
            this.aggroRange = 300;
            this.isDead = false;
            this.tasks = new List<LivingObjectTask>();
            this.aggroSystem = new Task.Aggro.AggroSystem<LivingObject>();
            this.currentTask = null;
            this.canBeEffected = true;
            this.interactions = new List<LivingObjectInteraction>();
            this.path = null;
        }

        public LivingObject(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            this.healthPoints = (float)info.GetValue("healthPoints", typeof(float));
            this.maxHealthPoints = (float)info.GetValue("maxHealthPoints", typeof(float));
            this.aggroRange = (float)info.GetValue("aggroRange", typeof(float));

            this.isDead = (bool)info.GetValue("isDead", typeof(bool));
            this.canBeEffected = (bool)info.GetValue("canBeEffected", typeof(bool));

            this.gender = (GenderEnum)info.GetValue("gender", typeof(GenderEnum));
            aggroSystem = new Task.Aggro.AggroSystem<LivingObject>();
            //tasks = new List<LivingObjectTask>();
            this.interactions = new List<LivingObjectInteraction>();

            //this.aggroSystem = (Task.Aggro.AggroSystem<LivingObject>)info.GetValue("aggroSystem", typeof(Task.Aggro.AggroSystem<LivingObject>));

            //TODO: Verändere Methoden, sodass Interaction und Tasks nicht mehr den Owner speichern, sonst gibt es eine Kettenspeicherung bei Serialisierung
            //this.interactions = (List<LivingObjectInteraction>)info.GetValue("interactions", typeof(List<LivingObjectInteraction>));

            this.tasks = (List<LivingObjectTask>)info.GetValue("tasks", typeof(List<LivingObjectTask>));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);

            info.AddValue("healthPoints", this.healthPoints, typeof(float));
            info.AddValue("maxHealthPoints", this.maxHealthPoints, typeof(float));
            info.AddValue("aggroRange", this.aggroRange, typeof(float));

            info.AddValue("isDead", this.isDead, typeof(bool));
            info.AddValue("canBeEffected", this.canBeEffected, typeof(bool));

            info.AddValue("gender", this.gender, typeof(GenderEnum));

            //info.AddValue("aggroSystem", this.aggroSystem, typeof(Task.Aggro.AggroSystem<LivingObject>));

            //info.AddValue("interactions", this.interactions, typeof(List<LivingObjectInteraction>));

            info.AddValue("tasks", this.tasks, typeof(List<LivingObjectTask>));
        }

        public override void update(GameTime _GameTime)
        {
            base.update(_GameTime);
            if (Configuration.Configuration.isHost || Configuration.Configuration.isSinglePlayer)
            {
                this.updateAggroSystem();
                this.doTasks();
                if (this.path != null)
                {
                    this.path.moveOnPath(this);
                }
            }
        }

        private void updateAggroSystem()
        {
            if (this.aggroSystem != null)
            {
                List<LivingObject> objectsToRemove = new List<LivingObject>();
                foreach (LivingObject var_LivingObject in this.aggroSystem.AggroItems.Keys.ToList())
                {
                    if (var_LivingObject.isDead)
                    {
                        objectsToRemove.Add(var_LivingObject);
                    }
                    else if (Vector3.Distance(this.Position, var_LivingObject.Position) > this.aggroRange)
                    {
                        this.aggroSystem.modifyAggro(var_LivingObject, 0.9f);
                        this.aggroSystem.addAggro(var_LivingObject, -2f);
                        if (this.aggroSystem.AggroItems[var_LivingObject] <= 0)
                            objectsToRemove.Add(var_LivingObject);
                    }
                }
                foreach (LivingObject var_LivingObject in objectsToRemove)
                {
                    this.aggroSystem.removeUnit(var_LivingObject);
                }
            }
        }

        private void doTasks()
        {
            if (currentTask != null)
            {
                if (currentTask.wantToDoTask())
                {
                }
                else
                {
                    currentTask = null;
                }
            }

            if (this.tasks != null)
            {
                LivingObjectTask var_TaskWantToDo = null;
                foreach (LivingObjectTask var_Task in this.tasks)
                {
                    if (var_Task.wantToDoTask())
                    {
                        if (currentTask == null)
                        {
                            if (var_TaskWantToDo == null)
                            {
                                var_TaskWantToDo = var_Task;
                            }
                            else if (var_TaskWantToDo.Priority < var_Task.Priority)
                            {
                                var_TaskWantToDo = var_Task;
                            }
                        }
                        else
                        {
                            if (var_TaskWantToDo == null)
                            {
                                if (this.currentTask.Priority < var_Task.Priority)
                                {
                                    var_TaskWantToDo = var_Task;
                                }
                            }
                            else if (var_TaskWantToDo.Priority < var_Task.Priority)
                            {
                                var_TaskWantToDo = var_Task;
                            }
                        }
                    }
                }
                if (var_TaskWantToDo != null)
                {
                    this.currentTask = var_TaskWantToDo;
                }
                if (this.currentTask != null)
                {
                    this.currentTask.update();
                }
            }
        }

        public void getInteracted(LivingObject _Interactor)
        {
            foreach (LivingObjectInteraction var_Interaction in this.interactions)
            {
                var_Interaction.doInteraction(_Interactor);
            }
        }

        public void interact()
        {
            List<Object> var_Objects = GameLibrary.Map.World.World.world.getObjectsInRange(this.Position, this.Size.X + 5);
            var_Objects.Remove(this);
            foreach (Object var_Object in var_Objects)
            {
                if (var_Object is LivingObject)
                {
                    ((LivingObject)var_Object).getInteracted(this);
                }
            }
        }

        public virtual void attack()
        {
        }

        public void attackLivingObject(LivingObject _Target, int _Damage)
        {
            if(Configuration.Configuration.isHost || Configuration.Configuration.isSinglePlayer)
                _Target.onAttacked(this, _Damage);
        }

        public void MoveWithoutDirectionChange(Vector3 _TargetPosition)
        {
            this.Position = _TargetPosition;
        }

        public Boolean isInFight()
        {
            return aggroSystem.AggroItems.Count > 0;
        }

        public virtual void onAttacked(LivingObject _Attacker, int _DamageAmount)
        {
            this.damage(_DamageAmount);
            this.aggroSystem.addAggro(_Attacker, _DamageAmount * 100f);
            Vector3 knockBackVector = this.Position - _Attacker.Position;
            knockBackVector.X = knockBackVector.X / knockBackVector.Length() * 20;
            knockBackVector.Y = knockBackVector.Y / knockBackVector.Length() * 20;
            knockBackVector.Z = knockBackVector.Z / knockBackVector.Length() * 20;
            //this.knockBack(knockBackVector);

            Configuration.Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.UpdateObjectPositionMessage(this), GameMessageImportance.VeryImportant);
            Configuration.Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.UpdateObjectHealthMessage(this), GameMessageImportance.VeryImportant);

            this.checkChangedBlock();
        }

        public virtual float calculateDamage(float _DamageAmount)
        {
            return _DamageAmount;
        }

        public void damage(float _DamageAmount)
        {
            this.healthPoints -= this.calculateDamage(_DamageAmount);
            if (this.healthPoints <= 0 && !this.isDead)
            {
                this.isDead = true;
                try
                {
                    //Texture2D texture = Ressourcen.RessourcenManager.ressourcenManager.Texture[this.GraphicPath + "_Dead"];
                    //this.GraphicPath = this.GraphicPath + "_Dead";
                    GameLibrary.Map.World.World.world.removeObjectFromWorld(this);
                    //TODO: PreEnviornment
                    if (this.CurrentBlock != null)
                    {
                        this.CurrentBlock.ObjectsPreEnviorment.Add(this);
                        this.CurrentBlock.removeObject(this);
                    }
                }
                catch (Exception e)
                {
                    GameLibrary.Map.World.World.world.removeObjectFromWorld(this);
                }
            }
            else if (this.healthPoints <= 0)
            {
                GameLibrary.Map.World.World.world.removeObjectFromWorld(this);
            }
            else
            {
                /*if (this.canBeEffected)
                {
                    this.Animation = new GameLibrary.Object.Animation.Animations.TakeDamageAnimation(this);
                }*/
            }
        }

        public void knockBack(Vector3 _KnockBackAmount)
        {
            if (this.canBeEffected)
            {
                this.Position += _KnockBackAmount;
            }
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch, Vector3 _DrawPositionExtra, Color _Color)
        {
            if (this.healthPoints < this.maxHealthPoints)
            {
                Texture2D lifebar = Ressourcen.RessourcenManager.ressourcenManager.Texture["Character/Lifebar"];
                float lifePercentage = this.healthPoints / this.maxHealthPoints;
                float lifebarWidth = lifebar.Bounds.Width * lifePercentage;

                try
                {
                    //Rectangle lifebarBounds = new Rectangle((int)(this.Position.X + Ressourcen.RessourcenManager.ressourcenManager.Texture[this.GraphicPath].Bounds.Width / 2 - lifebarWidth / 2 - this.Size.X / 2), (int)(this.Position.Y - 5 - this.Size.Y), (int)lifebarWidth / 2, lifebar.Bounds.Height / 2);
                    Rectangle lifebarBounds = new Rectangle((int)(this.Position.X - this.Size.X / 2), (int)(this.Position.Y - 5 - this.Size.Y), (int)lifebarWidth / 2, lifebar.Bounds.Height / 2);

                    _SpriteBatch.Draw(lifebar, lifebarBounds, Color.White * this.LightLevel);
                }
                catch (Exception e)
                {
                    //Logger.Logger.LogErr(this.GraphicPath + " nicht gefunden!");
                }
            }

            base.draw(_GraphicsDevice, _SpriteBatch, _DrawPositionExtra, _Color);
        }
    }
}
