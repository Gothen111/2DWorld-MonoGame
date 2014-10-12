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
using Lidgren.Network;
using GameLibrary.Behaviour.Member;
using GameLibrary.Behaviour;
#endregion

namespace GameLibrary.Connection.Message
{
    public class UpdateRacesMessage : IGameMessage
    {
        #region Constructors and Destructors

        public UpdateRacesMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public UpdateRacesMessage(List<Race> items)
        {
            this.MessageTime = NetTime.Now;
            this.races = items;

            int var_CountRaces = this.races.Count;
            this.value = new int[var_CountRaces, var_CountRaces];
            for (int y = 0; y < var_CountRaces; y++)
            {
                for (int x = 0; x < var_CountRaces; x++)
                {
                    this.value[y, x] = this.races[y].BehaviourMember[x].Value;
                }
            }
        }

        #endregion

        #region Properties

        public double MessageTime { get; set; }

        public List<Race> races { get; set; }

        public int[,] value { get; set; }

        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.UpdateRacesMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.MessageTime = im.ReadDouble();
            int var_CountRaces = im.ReadInt32();

            this.races = new List<Race>();
            for (int i = 0; i < var_CountRaces; i++)
            {
                this.races.Add(Utility.Serialization.Serializer.DeserializeObjectFromString<Race>(im.ReadString()));
            }
            this.value = new int[var_CountRaces, var_CountRaces];
            for (int y = 0; y < var_CountRaces; y++)
            {
                for (int x = 0; x < var_CountRaces; x++)
                {
                    this.value[y, x] = im.ReadInt32();
                    this.races[y].BehaviourMember.Add(new BehaviourItem<Race>(this.races[x], this.value[y, x]));
                }
            }
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.MessageTime);
            int var_CountRaces = this.races.Count;
            om.Write(var_CountRaces);
            for (int i = 0; i < var_CountRaces; i++)
            {
                om.Write(Utility.Serialization.Serializer.SerializeObjectToString(this.races[i]));
            }
            for (int y = 0; y < var_CountRaces; y++)
            {
                for (int x = 0; x < var_CountRaces; x++)
                {
                    om.Write(this.value[y,x]);
                }
            }
        }

        #endregion
    }
}
