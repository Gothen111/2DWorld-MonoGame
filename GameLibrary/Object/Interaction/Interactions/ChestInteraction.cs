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

namespace GameLibrary.Object.Interaction.Interactions
{
    public class ChestInteraction : LivingObjectInteraction
    {
        private bool isOpen;

        public ChestInteraction(LivingObject _InteractionOwner)
            : base(_InteractionOwner)
        {
            this.isOpen = false;
        }

        public override void doInteraction(LivingObject _Interactor)
        {
            base.doInteraction(_Interactor);
            if (this.isOpen)
            {
                this.closeChest();
            }
            else
            {
                this.openChest();
            }
        }

        private void closeChest()
        {
            //this.InteractionOwner.Animation = new GameLibrary.Object.Animation.Animations.OpenChestAnimation(this.InteractionOwner);
            //this.isOpen = false;
        }

        private void openChest()
        {
            //this.InteractionOwner.Animation = new GameLibrary.Object.Animation.Animations.OpenChestAnimation(this.InteractionOwner);
            this.isOpen = true;
        }
    }
}
