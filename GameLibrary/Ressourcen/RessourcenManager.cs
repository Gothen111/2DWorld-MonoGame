using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using GameLibrary.Ressourcen.Font;
using GameLibrary.Enums;

namespace GameLibrary.Ressourcen
{
    public class RessourcenManager
    {
        public static RessourcenManager ressourcenManager = new RessourcenManager();

        private Dictionary<String, SpriteFont> fonts;

        public Dictionary<String, SpriteFont> Fonts
        {
            get { return fonts; }
            set { fonts = value; }
        }

        private Dictionary<String, Texture2D> texture;

        public Dictionary<String, Texture2D> Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        private int loadingErrorsCount;

        public RessourcenManager()
        {
            fonts = new Dictionary<String, SpriteFont>();
            texture = new Dictionary<string, Texture2D>();
            this.loadingErrorsCount = 0;
        }

        private void loadFonts(ContentManager _ContentManager)
        {
            this.loadFont(_ContentManager, "Arial", "Font/Arial");
        }

        private void loadMapTextures(ContentManager _ContentManager)
        {
            for (int i = 0; i < Enum.GetValues(typeof(RegionEnum)).Length; i++)
            {
                String var_RegionType = Enum.GetValues(typeof(RegionEnum)).GetValue(i).ToString();
                String var_Path = "Region/" + var_RegionType + "/" + var_RegionType;
                this.loadTexture(_ContentManager, var_Path, var_Path);
            }
        }

        private void loadTextures(ContentManager _ContentManager)
        {
            /*loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Hill1_Center", "Region/Grassland/Block/Layer1/Hill1/GrasHill1_Center");
            loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Hill1_Corner1", "Region/Grassland/Block/Layer1/Hill1/GrasHill1_Corner1");
            loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Hill1_Corner2", "Region/Grassland/Block/Layer1/Hill1/GrasHill1_Corner2");
            loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Hill1_Corner3", "Region/Grassland/Block/Layer1/Hill1/GrasHill1_Corner3");
            loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Hill1_Corner4", "Region/Grassland/Block/Layer1/Hill1/GrasHill1_Corner4");
            loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Hill1_Left", "Region/Grassland/Block/Layer1/Hill1/GrasHill1_Left");
            loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Hill1_Right", "Region/Grassland/Block/Layer1/Hill1/GrasHill1_Right");
            loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Hill1_Top", "Region/Grassland/Block/Layer1/Hill1/GrasHill1_Top");
            loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Hill1_Bottom", "Region/Grassland/Block/Layer1/Hill1/GrasHill1_Bottom");
            loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Hill1_InsideCorner1", "Region/Grassland/Block/Layer1/Hill1/GrasHill1_InsideCorner1");
            loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Hill1_InsideCorner2", "Region/Grassland/Block/Layer1/Hill1/GrasHill1_InsideCorner2");
            loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Hill1_InsideCorner3", "Region/Grassland/Block/Layer1/Hill1/GrasHill1_InsideCorner3");
            loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Hill1_InsideCorner4", "Region/Grassland/Block/Layer1/Hill1/GrasHill1_InsideCorner4");*/

            this.loadTexture(_ContentManager, "Region/Environment/Tree_Normal", "Region/Environment/Tree_Normal");
            this.loadTexture(_ContentManager, "Region/Environment/Flower", "Region/Environment/Flower");

            this.loadTexture(_ContentManager, "Region/Grassland/Block/Environment/Tree/Tree1", "Region/Grassland/Block/Environment/Tree/Tree1");
            this.loadTexture(_ContentManager, "Region/Grassland/Block/Environment/Tree/Tree1_Dead", "Region/Grassland/Block/Environment/Tree/Tree1_Dead");
            this.loadTexture(_ContentManager, "Region/Grassland/Block/Environment/Flower/Flower1", "Region/Grassland/Block/Environment/Flower/Flower1");
            this.loadTexture(_ContentManager, "Region/Grassland/Block/Environment/Chest/Chest", "Region/Grassland/Block/Environment/Chest/Chest");
            this.loadTexture(_ContentManager, "Region/Grassland/Block/Environment/Farm/FarmHouse1", "Region/Grassland/Block/Environment/Farm/FarmHouse1");

            this.loadTexture(_ContentManager, "Region/Snowland/Block/Environment/Tree/Tree1", "Region/Snowland/Block/Environment/Tree/Tree1");

            this.loadTexture(_ContentManager, "Character/Char1_Small", "Object/Character/Char1_Small");
            this.loadTexture(_ContentManager, "Character/Char1_Small_Attack", "Object/Character/Char1_Small_Attack");

            this.loadTexture(_ContentManager, "Character/Ogre1", "Object/Character/Ogre1");

            this.loadTexture(_ContentManager, "Character/Lifebar", "Object/Character/Lifebar");

            this.loadTexture(_ContentManager, "Character/Shadow", "Object/Character/Shadow");
            this.loadTexture(_ContentManager, "Character/CreatureState", "Object/Character/CreatureState");

            this.loadTexture(_ContentManager, "Character/Cloth1", "Object/Character/Cloth1");

            this.loadTexture(_ContentManager, "Character/GoldCoin", "Object/Character/GoldCoin");

            this.loadTexture(_ContentManager, "Gui/Button", "Gui/Button");
            this.loadTexture(_ContentManager, "Gui/Button_Hover", "Gui/Button_Hover");
            this.loadTexture(_ContentManager, "Gui/Button_Pressed", "Gui/Button_Pressed");
            this.loadTexture(_ContentManager, "Gui/TextField", "Gui/TextField");
            this.loadTexture(_ContentManager, "Gui/TextField_Selected", "Gui/TextField_Selected");
            this.loadTexture(_ContentManager, "Gui/Background", "Gui/Background");

            this.loadTexture(_ContentManager, "Gui/Menu/CharacterCreation/Background", "Gui/Menu/CharacterCreation/Background");
            this.loadTexture(_ContentManager, "Gui/Menu/CharacterCreation/Plattform", "Gui/Menu/CharacterCreation/Plattform");
            this.loadTexture(_ContentManager, "Gui/Menu/CharacterCreation/ColorField", "Gui/Menu/CharacterCreation/ColorField");

            this.loadTexture(_ContentManager, "Gui/Menu/CharacterCreation/FemaleSymbol", "Gui/Menu/CharacterCreation/FemaleSymbol");
            this.loadTexture(_ContentManager, "Gui/Menu/CharacterCreation/MaleSymbol", "Gui/Menu/CharacterCreation/MaleSymbol");
            this.loadTexture(_ContentManager, "Gui/Menu/CharacterCreation/HumanHead", "Gui/Menu/CharacterCreation/HumanHead");
            this.loadTexture(_ContentManager, "Gui/Menu/CharacterCreation/UndeadHead", "Gui/Menu/CharacterCreation/UndeadHead");

            this.loadTexture(_ContentManager, "Gui/Menu/GameSurface/Interface", "Gui/Menu/GameSurface/Interface");
            this.loadTexture(_ContentManager, "Gui/Menu/GameSurface/Health", "Gui/Menu/GameSurface/Health");
            this.loadTexture(_ContentManager, "Gui/Menu/GameSurface/Mana", "Gui/Menu/GameSurface/Mana");

            this.loadTexture(_ContentManager, "Gui/Menu/Inventory/InventoryItemSpace", "Gui/Menu/Inventory/InventoryItemSpace");
            this.loadTexture(_ContentManager, "Gui/Menu/Inventory/InventoryButton", "Gui/Menu/Inventory/InventoryButton");
            this.loadTexture(_ContentManager, "Gui/Menu/Inventory/InventoryMenu", "Gui/Menu/Inventory/InventoryMenu");

            this.loadTexture(_ContentManager, "Character/Char1_Big", "Object/Character/Char1_Big");

            this.loadTexture(_ContentManager, "Object/Item/Small/Cloth1", "Object/Item/Small/Cloth1");
            this.loadTexture(_ContentManager, "Object/Item/Small/Sword1", "Object/Item/Small/Sword1");

            this.loadTexture(_ContentManager, "Character/BodyMale", "Object/Character/BodyMale");
            this.loadTexture(_ContentManager, "Character/BodyMale_Attack", "Object/Character/BodyMale_Attack");
            this.loadTexture(_ContentManager, "Character/BodyFemale", "Object/Character/BodyFemale");

            this.loadTexture(_ContentManager, "Character/GoldenArmor", "Object/Character/GoldenArmor");
            this.loadTexture(_ContentManager, "Character/Sword", "Object/Character/Sword");

            this.loadTexture(_ContentManager, "Character/Hair1", "Object/Character/Hair1");
        }

        public void loadRessources(ContentManager _ContentManager)
        {
            Logger.Logger.LogInfo("Lade Ressource ...");

            this.loadFonts(_ContentManager);

            this.loadMapTextures(_ContentManager);

            this.loadTextures(_ContentManager);

            Logger.Logger.LogInfo("Ressourcen wurden mit " + this.loadingErrorsCount + " Fehler(n) geladen");
        }

        public bool containsTextue(String _Name, Texture2D _Texture2D)
        {
            return texture.ContainsKey(_Name) || texture.ContainsValue(_Texture2D);
        }

        private void addTexture(String _Name, Texture2D _Texture2D)
        {
            if (!this.containsTextue(_Name, _Texture2D))
            {
                this.texture.Add(_Name, _Texture2D);
            }
        }

        private void loadTexture(ContentManager _ContentManager, String _Name, String _Texture2DPath)
        {
            try
            {
                this.addTexture(_Name, _ContentManager.Load<Texture2D>(_Texture2DPath));
                Logger.Logger.LogInfo("RessourcenManager->loadTexture(...) : " + _Texture2DPath + " wurde geladen!");
            }
            catch (Exception e)
            {
                Logger.Logger.LogErr("RessourcenManager->loadTexture(...) : " + _Texture2DPath + " konnte nicht gefunden werden!");
                this.loadingErrorsCount += 1;
            }
        }

        public bool containsFont(String _Name, SpriteFont _SpiteFont)
        {
            return fonts.ContainsKey(_Name) || fonts.ContainsValue(_SpiteFont);
        }

        private void addFont(String _Name, SpriteFont _SpiteFont)
        {
            if(!this.containsFont(_Name, _SpiteFont))
            {
                this.fonts.Add(_Name, _SpiteFont);
            }
        }

        private void loadFont(ContentManager _ContentManager, String _Name, String _FontPath)
        {
            try
            {
                this.addFont(_Name, _ContentManager.Load<SpriteFont>(_FontPath));
                Logger.Logger.LogInfo("RessourcenManager->loadFont(...) : " + _FontPath + " wurde geladen!");
            }
            catch (Exception e)
            {
                Logger.Logger.LogErr("RessourcenManager->loadFont(...) : " + _FontPath + " konnte nicht gefunden werden!");
                this.loadingErrorsCount += 1;
            }
        }
    }
}
