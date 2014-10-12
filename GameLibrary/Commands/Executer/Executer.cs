using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Commands.CommandTypes;

namespace GameLibrary.Commands.Executer
{
    public class Executer
    {
        public static Executer executer = new Executer();

        private List<Command> scheduledCommands;

        protected List<Command> ScheduledCommands
        {
            get { return scheduledCommands; }
            set { scheduledCommands = value; }
        }

        private Executer()
        {
            scheduledCommands = new List<Command>();
        }

        public void update(float delta)
        {
            while (scheduledCommands.Count > 0)
            {
                Command command = scheduledCommands.First();
                command.doCommand();
                scheduledCommands.RemoveAt(0);
            }
        }

        public void addCommand(Command command)
        {
            scheduledCommands.Add(command);
        }

        public void removeCommand(Command command)
        {
            removeCommand(getCommandIndex(command));
        }

        public void removeCommand(int index)
        {
            if (index != null && index >= 0 && index < scheduledCommands.Count)
                scheduledCommands.RemoveAt(index);
        }

        public int getCommandIndex(Command command)
        {
            return scheduledCommands.IndexOf(command);
        }
    }
}
