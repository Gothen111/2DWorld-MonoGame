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

namespace GameLibrary.Object.Interaction
{
    public class LivingObjectInteraction
    {
        private LivingObject interactionOwner;

        internal LivingObject InteractionOwner
        {
            get { return interactionOwner; }
            set { interactionOwner = value; }
        }

        public LivingObjectInteraction()
        {

        }

        public LivingObjectInteraction(LivingObject _InteractionOwner)
        {
            this.interactionOwner = _InteractionOwner;
        }

        public virtual void doInteraction(LivingObject _Interactor)
        {
        }
    }
}
