package com.company.assembleegameclient.objects {
import com.company.assembleegameclient.objects.animation.AnimationsData;
import com.company.assembleegameclient.parameters.Parameters;
import com.company.assembleegameclient.ui.tooltip.EquipmentToolTip;
import com.company.assembleegameclient.util.TextureRedrawer;
import com.company.assembleegameclient.util.redrawers.GlowRedrawer;
import com.company.util.AssetLibrary;
import com.company.util.ConversionUtil;

import flash.display.BitmapData;
import flash.utils.Dictionary;
import flash.utils.getDefinitionByName;

import kabam.rotmg.constants.GeneralConstants;
import kabam.rotmg.constants.ItemConstants;
import kabam.rotmg.messaging.impl.data.StatData;

public class ObjectLibrary {

    public static const IMAGE_SET_NAME:String = "lofiObj3";
    public static const IMAGE_ID:int = 0xFF;
    public static const propsLibrary_:Dictionary = new Dictionary();
    public static const xmlLibrary_:Dictionary = new Dictionary();
    public static const idToType_:Dictionary = new Dictionary();
    public static const typeToDisplayId_:Dictionary = new Dictionary();
    public static const typeToTextureData_:Dictionary = new Dictionary();
    public static const typeToTopTextureData_:Dictionary = new Dictionary();
    public static const typeToAnimationsData_:Dictionary = new Dictionary();
    public static const typeToIdItems_:Dictionary = new Dictionary();
    public static const idToTypeItems_:Dictionary = new Dictionary();
    public static const preloadedCustom_:Dictionary = new Dictionary();
    public static const petXMLDataLibrary_:Dictionary = new Dictionary();
    public static const skinSetXMLDataLibrary_:Dictionary = new Dictionary();
    public static const dungeonsXMLLibrary_:Dictionary = new Dictionary(true);
    public static const ENEMY_FILTER_LIST:Vector.<String> = new <String>["None", "Hp", "Defense"];
    public static const TILE_FILTER_LIST:Vector.<String> = new <String>["ALL", "Walkable", "Unwalkable", "Slow", "Speed=1"];
    public static const defaultProps_:ObjectProperties = new ObjectProperties(null);
    public static const TYPE_MAP:Object = {
        "ArenaGuard": ArenaGuard,
        "ArenaPortal": ArenaPortal,
        "CaveWall": CaveWall,
        "Character": Character,
        "CharacterChanger": CharacterChanger,
        "ClosedGiftChest": ClosedGiftChest,
        "ClosedVaultChest": ClosedVaultChest,
        "ConnectedWall": ConnectedWall,
        "Container": Container,
        "DoubleWall": DoubleWall,
        "FortuneGround": FortuneGround,
        "FortuneTeller": FortuneTeller,
        "GameObject": GameObject,
        "GuildBoard": GuildBoard,
        "GuildChronicle": GuildChronicle,
        "GuildHallPortal": GuildHallPortal,
        "GuildMerchant": GuildMerchant,
        "GuildRegister": GuildRegister,
        "Merchant": Merchant,
        "MoneyChanger": MoneyChanger,
        "MysteryBoxGround": MysteryBoxGround,
        "NameChanger": NameChanger,
        "ReskinVendor": ReskinVendor,
        "OneWayContainer": OneWayContainer,
        "Player": Player,
        "Portal": Portal,
        "Projectile": Projectile,
        "QuestRewards": QuestRewards,
        "DailyLoginRewards": DailyLoginRewards,
        "Sign": Sign,
        "SpiderWeb": SpiderWeb,
        "Stalagmite": Stalagmite,
        "Wall": Wall,
        "Pet": Pet,
        "PetUpgrader": PetUpgrader,
        "YardUpgrader": YardUpgrader,
        "MarketObject": MarketObject
    };

    public static var textureDataFactory:TextureDataFactory = new TextureDataFactory();
    public static var playerChars_:Vector.<XML> = new Vector.<XML>();
    public static var hexTransforms_:Vector.<XML> = new Vector.<XML>();
    public static var playerClassAbbr_:Dictionary = new Dictionary();
    private static var currentDungeon:String = "";


    public static function parseDungeonXML(_arg1:String, _arg2:XML):void {
        var _local3:int = (_arg1.indexOf("_") + 1);
        var _local4:int = _arg1.indexOf("CXML");
        currentDungeon = _arg1.substr(_local3, (_local4 - _local3));
        dungeonsXMLLibrary_[currentDungeon] = new Dictionary(true);
        parseFromXML(_arg2, parseDungeonCallback);
    }

    private static function parseDungeonCallback(_arg1:int, _arg2:XML) {
        if (((!((currentDungeon == ""))) && (!((dungeonsXMLLibrary_[currentDungeon] == null))))) {
            dungeonsXMLLibrary_[currentDungeon][_arg1] = _arg2;
            propsLibrary_[_arg1].belonedDungeon = currentDungeon;
        }
    }

    public static function parseFromXML(xml:XML, func:Function = null, preload:Boolean = false) : void
    {
        var objectXML:XML = null;
        var id:String = null;
        var displayId:String = null;
        var objectType:int = 0;
        var found:Boolean = false;
        var i:int = 0;
        for each(objectXML in xml.Object)
        {
            id = String(objectXML.@id);
            displayId = id;
            if(objectXML.hasOwnProperty("DisplayId"))
            {
                displayId = objectXML.DisplayId;
            }
            if(objectXML.hasOwnProperty("Group"))
            {
                if(objectXML.Group == "Hexable")
                {
                    hexTransforms_.push(objectXML);
                }
            }

            objectType = int(objectXML.@type);
            if (((objectXML.hasOwnProperty("PetBehavior")) || (objectXML.hasOwnProperty("PetAbility")))) {
                petXMLDataLibrary_[objectType] = objectXML;
            }

            objectType = int(objectXML.@type);
            propsLibrary_[objectType] = new ObjectProperties(objectXML);
            xmlLibrary_[objectType] = objectXML;
            idToType_[id] = objectType;
            typeToDisplayId_[objectType] = displayId;

            if (String(objectXML.Class) == "Equipment")
            {
                typeToIdItems_[objectType] = id.toLowerCase(); /* Saves us the power to do this later */
                idToTypeItems_[id.toLowerCase()] = objectType;
            }

            if (preload)
            {
                preloadedCustom_[objectType] = id.toLowerCase();
            }

            if (func != null) {
                (func(objectType, objectXML));
            }
            if(String(objectXML.Class) == "Player")
            {
                playerClassAbbr_[objectType] = String(objectXML.@id).substr(0,2);
                found = false;
                for(i = 0; i < playerChars_.length; i++)
                {
                    if(int(playerChars_[i].@type) == objectType)
                    {
                        playerChars_[i] = objectXML;
                        found = true;
                    }
                }
                if(!found)
                {
                    playerChars_.push(objectXML);
                }
            }
            typeToTextureData_[objectType] = new TextureDataConcrete(objectXML);
            if(objectXML.hasOwnProperty("Top"))
            {
                typeToTopTextureData_[objectType] = new TextureDataConcrete(XML(objectXML.Top));
            }
            if(objectXML.hasOwnProperty("Animation"))
            {
                typeToAnimationsData_[objectType] = new AnimationsData(objectXML);
            }
        }
    }

    public static function getIdFromType(_arg1:int):String {
        var _local2:XML = xmlLibrary_[_arg1];
        if (_local2 == null) {
            return null;
        }
        return (String(_local2.@id));
    }

    public static function getPropsFromId(_arg1:String):ObjectProperties {
        var _local2:int = idToType_[_arg1];
        return (propsLibrary_[_local2]);
    }

    public static function getXMLfromId(_arg1:String):XML {
        var _local2:int = idToType_[_arg1];
        return (xmlLibrary_[_local2]);
    }

    public static function getObjectFromType(objectType:int):GameObject {
        var objectXML:XML;
        var typeReference:String;
        try {
            objectXML = xmlLibrary_[objectType];
            typeReference = objectXML.Class;
        }
        catch (e:Error) {
            throw (new Error(("Type: 0x" + objectType.toString(16))));
        }
        var typeClass:Class = ((TYPE_MAP[typeReference]) || (makeClass(typeReference)));
        return (new (typeClass)(objectXML));
    }

    private static function makeClass(_arg1:String):Class {
        var _local2:String = ("com.company.assembleegameclient.objects." + _arg1);
        return ((getDefinitionByName(_local2) as Class));
    }

    public static function getTextureFromType(_arg1:int):BitmapData {
        var _local2:TextureData = typeToTextureData_[_arg1];
        if (_local2 == null) {
            return null;
        }
        return (_local2.getTexture());
    }

    public static function getBitmapData(_arg1:int):BitmapData {
        var _local2:TextureData = typeToTextureData_[_arg1];
        var _local3:BitmapData = ((_local2) ? _local2.getTexture() : null);
        if (_local3) {
            return (_local3);
        }
        return (AssetLibrary.getImageFromSet(IMAGE_SET_NAME, IMAGE_ID));
    }

    public static function getRedrawnTextureFromType(_arg1:int, _arg2:int, _arg3:Boolean, _arg4:Boolean = true, _arg5:Number = 5):BitmapData {
        var _local6:BitmapData = getBitmapData(_arg1);
        if (((!((Parameters.itemTypes16.indexOf(_arg1) == -1))) || ((_local6.height == 16)))) {
            _arg2 = (_arg2 * 0.5);
        }
        var _local7:TextureData = typeToTextureData_[_arg1];
        var _local8:BitmapData = Boolean(_local7) ? _local7.mask_ : null;
        var _local9:XML;

        try { _local9 = xmlLibrary_[_arg1]; }
        catch (error:Error) { }

        if (_local9 == null) return TextureRedrawer.redraw(_local6, _arg2, _arg3, 0, _arg4, _arg5);
        var _local10:int = ((_local9.hasOwnProperty("Tex1")) ? int(_local9.Tex1) : 0);
        var _local11:int = ((_local9.hasOwnProperty("Tex2")) ? int(_local9.Tex2) : 0);
        _local6 = TextureRedrawer.resize(_local6, _local8, _arg2, _arg3, _local10, _local11, _arg5);
        _local6 = GlowRedrawer.outlineGlow(_local6, 0);
        return (_local6);
    }

    public static function getSizeFromType(_arg1:int):int {
        var _local2:XML = xmlLibrary_[_arg1];
        if (!_local2.hasOwnProperty("Size")) {
            return (100);
        }
        return (int(_local2.Size));
    }

    public static function getSlotTypeFromType(_arg1:int):int {
        var _local2:XML = xmlLibrary_[_arg1];
        if (!_local2.hasOwnProperty("SlotType")) {
            return (-1);
        }
        return (int(_local2.SlotType));
    }

    public static function isEquippableByPlayer(_arg1:int, _arg2:Player):Boolean {
        if (_arg1 == ItemConstants.NO_ITEM) {
            return (false);
        }
        var _local3:XML = xmlLibrary_[_arg1];
        var _local4:int = int(_local3.SlotType.toString());
        var _local5:uint;
        while (_local5 < GeneralConstants.NUM_EQUIPMENT_SLOTS) {
            if (_arg2.slotTypes_[_local5] == _local4) {
                return (true);
            }
            _local5++;
        }
        return (false);
    }

    public static function getMatchingSlotIndex(_arg1:int, _arg2:Player):int {
        var _local3:XML;
        var _local4:int;
        var _local5:uint;
        if (_arg1 != ItemConstants.NO_ITEM) {
            _local3 = xmlLibrary_[_arg1];
            _local4 = int(_local3.SlotType);
            _local5 = 0;
            while (_local5 < GeneralConstants.NUM_EQUIPMENT_SLOTS) {
                if (_arg2.slotTypes_[_local5] == _local4) {
                    return (_local5);
                }
                _local5++;
            }
        }
        return (-1);
    }

    public static function isUsableByPlayer(_arg1:int, _arg2:Player):Boolean {
        if ((((_arg2 == null)) || ((_arg2.slotTypes_ == null)))) {
            return (true);
        }
        var _local3:XML = xmlLibrary_[_arg1];
        if ((((_local3 == null)) || (!(_local3.hasOwnProperty("SlotType"))))) {
            return (false);
        }
        var _local4:int = _local3.SlotType;
        if ((((_local4 == ItemConstants.POTION_TYPE)) || ((_local4 == ItemConstants.EGG_TYPE)))) {
            return (true);
        }
        var _local5:int;
        while (_local5 < _arg2.slotTypes_.length) {
            if (_arg2.slotTypes_[_local5] == _local4) {
                return (true);
            }
            _local5++;
        }
        return (false);
    }

    public static function isSoulbound(objectType:int):Boolean {
        var xmlLib:XML = xmlLibrary_[objectType];
        return (!(isSoulbound == null) && xmlLib.hasOwnProperty("Soulbound"));
    }

    public static function usableBy(_arg1:int):Vector.<String> {
        var _local5:XML;
        var _local6:Vector.<int>;
        var _local7:int;
        var _local2:XML = xmlLibrary_[_arg1];
        if ((((_local2 == null)) || (!(_local2.hasOwnProperty("SlotType"))))) {
            return (null);
        }
        var _local3:int = _local2.SlotType;
        if ((((((_local3 == ItemConstants.POTION_TYPE)) || ((_local3 == ItemConstants.RING_TYPE)))) || ((_local3 == ItemConstants.EGG_TYPE)))) {
            return (null);
        }
        var _local4:Vector.<String> = new Vector.<String>();
        for each (_local5 in playerChars_) {
            _local6 = ConversionUtil.toIntVector(_local5.SlotTypes);
            _local7 = 0;
            while (_local7 < _local6.length) {
                if (_local6[_local7] == _local3) {
                    _local4.push(typeToDisplayId_[int(_local5.@type)]);
                    break;
                }
                _local7++;
            }
        }
        return (_local4);
    }

    public static function playerMeetsRequirements(_arg1:int, _arg2:Player):Boolean {
        var _local4:XML;
        if (_arg2 == null) {
            return (true);
        }
        var _local3:XML = xmlLibrary_[_arg1];
        for each (_local4 in _local3.EquipRequirement) {
            if (!playerMeetsRequirement(_local4, _arg2)) {
                return (false);
            }
        }
        return (true);
    }

    public static function playerMeetsRequirement(xml:XML, player:Player):Boolean {
        var statValue:int;
        if (xml.toString() == "Stat") {
            statValue = int(xml.@value);
            switch (int(xml.@stat)) {
                case StatData.MAX_HP_STAT:
                    return ((player.maxHP_ >= statValue));
                case StatData.MAX_MP_STAT:
                    return ((player.maxMP_ >= statValue));
                case StatData.LEVEL_STAT:
                    return ((player.level_ >= statValue));
                case StatData.ATTACK_STAT:
                    return ((player.attack_ >= statValue));
                case StatData.DEFENSE_STAT:
                    return ((player.defense_ >= statValue));
                case StatData.SPEED_STAT:
                    return ((player.speed_ >= statValue));
                case StatData.VITALITY_STAT:
                    return ((player.vitality_ >= statValue));
                case StatData.WISDOM_STAT:
                    return ((player.wisdom_ >= statValue));
                case StatData.DEXTERITY_STAT:
                    return ((player.dexterity_ >= statValue));
            }
        }
        return (false);
    }

    public static function getPetDataXMLByType(_arg1:int):XML {
        return (petXMLDataLibrary_[_arg1]);
    }


}
}
