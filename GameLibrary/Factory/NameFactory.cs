using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Factory.FactoryEnums;

namespace GameLibrary.Factory
{
    public class NameFactory
    {
        public static String getName(CreatureEnum type, GenderEnum gender)
        {
            String result = "";
            switch (type)
            {
                case CreatureEnum.Archer:
                    {
                        if (gender == GenderEnum.Male)
                        {
                            result = "Bogenschütze";
                        }
                        if (gender == GenderEnum.Female)
                        {
                            result = "Bogenschützin";
                        }
                        break;
                    }
                case CreatureEnum.Captain:
                    {
                        if (gender == GenderEnum.Male)
                        {
                            result = "Kapitän";
                        }
                        if (gender == GenderEnum.Female)
                        {
                            result = "Kapitän";
                        }
                        break;
                    }
                case CreatureEnum.Chieftain:
                    {
                        if (gender == GenderEnum.Male)
                        {
                            result = "Anführer";
                        }
                        if (gender == GenderEnum.Female)
                        {
                            result = "Anführerin";
                        }
                        break;
                    }
                case CreatureEnum.Child:
                    {
                        if (gender == GenderEnum.Male)
                        {
                            result = "Junge";
                        }
                        if (gender == GenderEnum.Female)
                        {
                            result = "Mädchen";
                        }
                        break;
                    }
                case CreatureEnum.Commandant:
                    {
                        if (gender == GenderEnum.Male)
                        {
                            result = "Kommandant";
                        }
                        if (gender == GenderEnum.Female)
                        {
                            result = "Kommandantin";
                        }
                        break;
                    }
                case CreatureEnum.Farmer:
                    {
                        if (gender == GenderEnum.Male)
                        {
                            result = "Bauer";
                        }
                        if (gender == GenderEnum.Female)
                        {
                            result = "Bäuerin";
                        }
                        break;
                    }
                case CreatureEnum.Guard:
                    {
                        if (gender == GenderEnum.Male)
                        {
                            result = "Wächter";
                        }
                        if (gender == GenderEnum.Female)
                        {
                            result = "Wächterin";
                        }
                        break;
                    }
                case CreatureEnum.Hunter:
                    {
                        if (gender == GenderEnum.Male)
                        {
                            result = "Jäger";
                        }
                        if (gender == GenderEnum.Female)
                        {
                            result = "Jägerin";
                        }
                        break;
                    }
                case CreatureEnum.Mage:
                    {
                        if (gender == GenderEnum.Male)
                        {
                            result = "Magier";
                        }
                        if (gender == GenderEnum.Female)
                        {
                            result = "Magierin";
                        }
                        break;
                    }
                case CreatureEnum.Peasant:
                    {
                        if (gender == GenderEnum.Male)
                        {
                            result = "Bürger";
                        }
                        if (gender == GenderEnum.Female)
                        {
                            result = "Bürgerin";
                        }
                        break;
                    }
                case CreatureEnum.Priest:
                    {
                        if (gender == GenderEnum.Male)
                        {
                            result = "Priester";
                        }
                        if (gender == GenderEnum.Female)
                        {
                            result = "Priesterin";
                        }
                        break;
                    }
                case CreatureEnum.Slavehunter:
                    {
                        if (gender == GenderEnum.Male)
                        {
                            result = "Sklavenjäger";
                        }
                        if (gender == GenderEnum.Female)
                        {
                            result = "Sklavenjägerin";
                        }
                        break;
                    }
                case CreatureEnum.Soldier:
                    {
                        if (gender == GenderEnum.Male)
                        {
                            result = "Soldat";
                        }
                        if (gender == GenderEnum.Female)
                        {
                            result = "Soldatin";
                        }
                        break;
                    }
                case CreatureEnum.Sorcerer:
                    {
                        if (gender == GenderEnum.Male)
                        {
                            result = "Illusionist";
                        }
                        if (gender == GenderEnum.Female)
                        {
                            result = "Illusionistin";
                        }
                        break;
                    }
                case CreatureEnum.Spearman:
                    {
                        if (gender == GenderEnum.Male)
                        {
                            result = "Lanzenträger";
                        }
                        if (gender == GenderEnum.Female)
                        {
                            result = "Lanzenträgerin";
                        }
                        break;
                    }
                case CreatureEnum.Warlock:
                    {
                        if (gender == GenderEnum.Male)
                        {
                            result = "Hexenmeister";
                        }
                        if (gender == GenderEnum.Female)
                        {
                            result = "Hexenmeisterin";
                        }
                        break;
                    }
                case CreatureEnum.Warlord:
                    {
                        if (gender == GenderEnum.Male)
                        {
                            result = "Kriegsherr";
                        }
                        if (gender == GenderEnum.Female)
                        {
                            result = "Kriegsherrin";
                        }
                        break;
                    }
                case CreatureEnum.Wizard:
                    {
                        if (gender == GenderEnum.Male)
                        {
                            result = "Zauberer";
                        }
                        if (gender == GenderEnum.Female)
                        {
                            result = "Zauberin";
                        }
                        break;
                    }
                default:
                    {
                        result = "Unbekannt";
                        break;
                    }
            }
            return result;
        }

    }
}
