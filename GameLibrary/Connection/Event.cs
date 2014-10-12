using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Connection.Message;

namespace GameLibrary.Connection
{
    /*public class Event
    {
        public static List<Event> EventList = new List<Event>();

        private IGameMessage iGameMessage;
        private GameMessageImportance Importance;
        public IGameMessage getIGameMessage()
        {
            return iGameMessage;
        }
        public GameMessageImportance getImportance()
        {
            return Importance;
        }
        public Event(IGameMessage _iGameMessage)
        {
            iGameMessage = _iGameMessage;
            Importance = 0;
        }
        public Event(IGameMessage _iGameMessage, GameMessageImportance _Importance)
        {
            iGameMessage = _iGameMessage;
            Importance = _Importance;
        }
    }*/

    public struct Event
    {
        public IGameMessage IGameMessage;
        public GameMessageImportance Importance;
        public Event(IGameMessage _IGameMessage, GameMessageImportance _GameMessageImportance)
        {
            this.IGameMessage = _IGameMessage;
            this.Importance = _GameMessageImportance;
        }
    }

    public enum GameMessageImportance
    {
        UnImportant, // Unreliable, // Message can be send more than once or lost //UDP like
        VeryImportant // ReliableOrdered // This delivery method guarantees that messages will always be received in the exact order they were sent //TCP like
    }
}
