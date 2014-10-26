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
using GameLibrary.Object;
using GameLibrary.Map.World;
using GameLibrary.Map.Region;
using GameLibrary.Map.Chunk;
using GameLibrary.Map.Block;
using GameLibrary.Factory.FactoryEnums;
using GameLibrary.Behaviour.Member;
using GameLibrary.Behaviour;
#endregion

namespace GameLibrary.Factory
{
    public class BehaviourFactory
    {
        public static BehaviourFactory behaviourFactory = new BehaviourFactory();

        public BehaviourFactory()
        {
            createFactions();
            createRaces();

        }

        protected List<Faction> factions;
        public List<Faction> Factions
        {
            set { this.factions = value; }
            get { return factions; }
        }

        protected List<Race> races;
        public List<Race> Races
        {
            set { this.races = value; }
            get { return races; }
        }

        private void createFactions()
        {
            factions = new List<Faction>();
            Random random = new Random();

            //Erstelle alle Factions zuerst
            foreach (FactionEnum item in Enum.GetValues(typeof(FactionEnum)))
            {
                Faction tmpFaction = new Faction(item);
                factions.Add(tmpFaction);
            }

            //Bilde in jeder Faction eine Referenz auf jede andere Faction mit einem Zufallswert zwischen 0 und 100
            foreach (Faction faction in factions)
            {
                foreach (Faction faction2 in factions)
                {
                    if (faction == faction2)
                    {
                        faction.addItem(new BehaviourItem<Faction>(faction2, 100));
                    }
                    else
                    {
                        faction.addItem(new BehaviourItem<Faction>(faction2, random.Next(0, 100)));
                    }
                }
            }
        }

        private void createRaces()
        {
            races = new List<Race>();
            Random random = new Random();

            //Erstelle alle Races zuerst
            foreach (RaceEnum item in Enum.GetValues(typeof(RaceEnum)))
            {
                Race tmpRace = new Race(item);
                races.Add(tmpRace);
            }

            //Bilde in jeder Race eine Referenz auf jede andere Race mit einem Zufallswert zwischen 0 und 100
            foreach (Race race in races)
            {
                foreach (Race race2 in races)
                {
                    if (race == race2)
                    {
                        race.addItem(new BehaviourItem<Race>(race2, 100));
                    }
                    else
                    {
                        race.addItem(new BehaviourItem<Race>(race2, random.Next(0, 100)));
                    }
                }
            }
        }

        public Race getRace(RaceEnum type)
        {
            foreach(Race race in races)
            {
                if (race.Type == type)
                    return race;
            }
            return null;
        }

        public Faction getFaction(FactionEnum type)
        {
            foreach (Faction faction in factions)
            {
                if (faction.Type == type)
                    return faction;
            }
            return null;
        }

        
    }
}
