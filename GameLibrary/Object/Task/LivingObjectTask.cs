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

namespace GameLibrary.Object.Task
{
    [Serializable()]
    public class LivingObjectTask : ISerializable
    {
        private LivingObject taskOwner;

        public LivingObject TaskOwner
        {
            get { return taskOwner; }
            set { taskOwner = value; }
        }

        private Tasks.TaskPriority priority;

        public Tasks.TaskPriority Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        public LivingObjectTask()
        {

        }

        public LivingObjectTask(LivingObject _TaskOwner, Tasks.TaskPriority _Priority)
        {
            this.taskOwner = _TaskOwner;
            this.priority = _Priority;
        }

        public LivingObjectTask(SerializationInfo info, StreamingContext ctxt)
        {
            //this.taskOwner = World.world.getObject((int)info.GetValue("taskOwnerId", typeof(int))) as LivingObject;
            this.priority = (Task.Tasks.TaskPriority)info.GetValue("priority", typeof(Task.Tasks.TaskPriority));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            //info.AddValue("taskOwnerId", this.taskOwner.Id, typeof(int));
            info.AddValue("priority", this.priority, typeof(Task.Tasks.TaskPriority));
        }

        public virtual bool wantToDoTask()
        {
            return false;
        }

        public virtual void update()
        {
        }
    }
}
