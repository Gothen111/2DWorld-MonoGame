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
using GameLibrary.Enums;
using GameLibrary.Object.Body;
using GameLibrary.Object.Task.Tasks;
#endregion

namespace GameLibrary.Factory
{
    public class CreatureFactory
    {
        public static CreatureFactory creatureFactory = new CreatureFactory();

        private CreatureFactory()
        {
        }

        private void createBodySettings(RaceObject _RaceObject, RaceEnum _ObjectRace, GenderEnum _ObjectGender)
        {
            switch (_ObjectRace)
            {
                case RaceEnum.Ogre:
                    {
                        _RaceObject.Body = new BodyHuman();
                        ((BodyHuman)_RaceObject.Body).MainBody.TexturePath = "Character/Ogre1";
                        _RaceObject.Size = new Vector3(80, 96, 0);
                        _RaceObject.Body.setSize(new Vector3(80, 96, 0));
                        //npcObject.CollisionBounds.Add(new Rectangle(17 + (int)npcObject.Size.X / 2, 69 + (int)npcObject.Size.Y / 2, 49, 25));
                        break;
                    }
                case RaceEnum.Human:
                    {
                        _RaceObject.Body = new BodyHuman();

                        switch (_ObjectGender)
                        {
                            case GenderEnum.Male:
                                ((BodyHuman)_RaceObject.Body).MainBody.TexturePath = "Character/BodyMale";
                                break;
                            case GenderEnum.Female:
                                ((BodyHuman)_RaceObject.Body).MainBody.TexturePath = "Character/BodyFemale";
                                break;
                        }

                        _RaceObject.CollisionBounds.Add(new Rectangle(8, 20, 12, 2));

                        ((BodyHuman)_RaceObject.Body).Hair.TexturePath = "Character/Hair1";

                        EquipmentObject var_EquipmentObject_Armor = GameLibrary.Factory.EquipmentFactory.equipmentFactory.createEquipmentArmorObject(GameLibrary.Enums.ArmorEnum.GoldenArmor);
                        var_EquipmentObject_Armor.PositionInInventory = 0;

                        _RaceObject.Body.setEquipmentObject(var_EquipmentObject_Armor);

                        EquipmentObject var_EquipmentObject_Sword = GameLibrary.Factory.EquipmentFactory.equipmentFactory.createEquipmentWeaponObject(GameLibrary.Enums.WeaponEnum.Sword);
                        var_EquipmentObject_Sword.PositionInInventory = 1;

                        _RaceObject.Body.setEquipmentObject(var_EquipmentObject_Sword);

                        break;
                    }
            }
        }

        public PlayerObject createPlayerObject(RaceEnum objectRace, FactionEnum objectFaction, CreatureEnum objectType, GenderEnum objectGender)
        {
            PlayerObject playerObject = new PlayerObject();
            playerObject.Scale = 1;
            playerObject.Velocity = new Vector3(0, 0, 0);
            playerObject.FactionEnum = objectFaction;
            playerObject.RaceEnum = objectRace;
            playerObject.Gender = objectGender;
            playerObject.Name = NameFactory.getName(objectType, objectGender);

            this.createBodySettings(playerObject, objectRace, objectGender);

            return playerObject;
        }

        public PlayerObject createPlayerObject(PlayerObject _PlayerObject)
        {
            PlayerObject playerObject = this.createPlayerObject(RaceEnum.Human, FactionEnum.Beerdrinker, CreatureEnum.Archer, GenderEnum.Male);
            playerObject.Scale = _PlayerObject.Scale;
            playerObject.Velocity = new Vector3(0, 0, 0);
            playerObject.FactionEnum = _PlayerObject.FactionEnum;
            playerObject.RaceEnum = _PlayerObject.RaceEnum;
            playerObject.Gender = _PlayerObject.Gender;
            playerObject.Name = _PlayerObject.Name;
            playerObject.Body.setColor(_PlayerObject.Body.BodyColor);

            return playerObject;
        }

        public NpcObject createNpcObject(RaceEnum objectRace, FactionEnum objectFaction, CreatureEnum objectType, GenderEnum objectGender)
        {
            NpcObject npcObject = new NpcObject();
            npcObject.Scale = 1;
            npcObject.Velocity = new Vector3(0, 0, 0);
            npcObject.FactionEnum = objectFaction;
            npcObject.RaceEnum = objectRace;
            npcObject.Gender = objectGender;
            npcObject.Name = NameFactory.getName(objectType, objectGender);

            this.createBodySettings(npcObject, objectRace, objectGender);

            npcObject.Tasks.Add(new AttackRandomTask(npcObject, TaskPriority.Attack_Random));
            //npcObject.Tasks.Add(new Model.Object.Task.Tasks.WalkRandomTask(npcObject, Model.Object.Task.Tasks.TaskPriority.Walk_Random));

            return npcObject;
        }
    }
}
