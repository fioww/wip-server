package com.company.assembleegameclient.ui.tooltip {
import com.company.assembleegameclient.constants.InventoryOwnerTypes;
import com.company.assembleegameclient.game.events.KeyInfoResponseSignal;
import com.company.assembleegameclient.objects.ObjectLibrary;
import com.company.assembleegameclient.objects.Player;
import com.company.assembleegameclient.parameters.Parameters;
import com.company.assembleegameclient.ui.LineBreakDesign;
import com.company.util.BitmapUtil;
import com.company.util.KeyCodes;

import flash.display.Bitmap;
import flash.display.BitmapData;
import flash.filters.DropShadowFilter;
import flash.utils.Dictionary;

import kabam.rotmg.constants.ActivationType;
import kabam.rotmg.core.StaticInjectorContext;
import kabam.rotmg.messaging.impl.data.StatData;
import kabam.rotmg.messaging.impl.incoming.KeyInfoResponse;
import kabam.rotmg.text.model.TextKey;
import kabam.rotmg.text.view.TextFieldDisplayConcrete;
import kabam.rotmg.text.view.stringBuilder.AppendingLineBuilder;
import kabam.rotmg.text.view.stringBuilder.LineBuilder;
import kabam.rotmg.text.view.stringBuilder.StaticStringBuilder;
import kabam.rotmg.text.view.stringBuilder.StringBuilder;
import kabam.rotmg.ui.model.HUDModel;

public class EquipmentToolTip extends ToolTip {

    private static const MAX_WIDTH:int = 230;

    public static var keyInfo:Dictionary = new Dictionary();

    private var icon:Bitmap;
    public var titleText:TextFieldDisplayConcrete;
    private var tierText:TextFieldDisplayConcrete;
    private var descText:TextFieldDisplayConcrete;
    private var line1:LineBreakDesign;
    private var effectsText:TextFieldDisplayConcrete;
    private var line2:LineBreakDesign;
    private var restrictionsText:TextFieldDisplayConcrete;
    private var player:Player;
    private var isEquippable:Boolean = false;
    private var objectType:int;
    private var titleOverride:String;
    private var descriptionOverride:String;
    private var curItemXML:XML = null;
    private var objectXML:XML = null;
    private var slotTypeToTextBuilder:SlotComparisonFactory;
    private var restrictions:Vector.<Restriction>;
    private var effects:Vector.<Effect>;
    private var uniqueEffects:Vector.<Effect>;
    private var itemSlotTypeId:int;
    private var invType:int;
    private var inventorySlotID:uint;
    private var inventoryOwnerType:String;
    private var isInventoryFull:Boolean;
    private var playerCanUse:Boolean;
    private var comparisonResults:SlotComparisonResult;
    private var powerText:TextFieldDisplayConcrete;
    private var keyInfoResponse:KeyInfoResponseSignal;
    private var originalObjectType:int;

    public function EquipmentToolTip(objectType:int, player:Player, invType:int, inventoryOwnerType:String) {
        var hudModel:HUDModel;
        this.uniqueEffects = new Vector.<Effect>();
        this.objectType = objectType;
        this.originalObjectType = this.objectType;
        this.player = player;
        this.invType = invType;
        this.inventoryOwnerType = inventoryOwnerType;
        this.isInventoryFull = ((player) ? player.isInventoryFull() : false);
        if ((((this.objectType >= 0x9000)) && ((this.objectType <= 0xF000)))) {
            this.objectType = 36863;
        }
        this.playerCanUse = ((player) ? ObjectLibrary.isUsableByPlayer(this.objectType, player) : false);
        var availableSlot:int = ((player) ? ObjectLibrary.getMatchingSlotIndex(this.objectType, player) : -1);
        var bgColor:uint = ((((this.playerCanUse) || ((this.player == null)))) ? 0x363636 : 6036765);
        var outlineColor:uint = ((((this.playerCanUse) || ((player == null)))) ? 0x9B9B9B : 10965039);
        super(bgColor, 1, outlineColor, 1, true);
        this.slotTypeToTextBuilder = new SlotComparisonFactory();
        this.objectXML = ObjectLibrary.xmlLibrary_[this.objectType];
        this.isEquippable = !((availableSlot == -1));
        this.effects = new Vector.<Effect>();
        this.itemSlotTypeId = int(this.objectXML.SlotType);
        if (this.player == null) {
            this.curItemXML = this.objectXML;
        }
        else {
            if (this.isEquippable) {
                if (this.player.equipment_[availableSlot] != -1) {
                    this.curItemXML = ObjectLibrary.xmlLibrary_[this.player.equipment_[availableSlot]];
                }
            }
        }
        this.addIcon();
        if ((((this.originalObjectType >= 0x9000)) && ((this.originalObjectType <= 0xF000)))) {
            if (keyInfo[this.originalObjectType] == null) {
                this.addTitle();
                this.addDescriptionText();
                this.keyInfoResponse = StaticInjectorContext.getInjector().getInstance(KeyInfoResponseSignal);
                this.keyInfoResponse.add(this.onKeyInfoResponse);
                hudModel = StaticInjectorContext.getInjector().getInstance(HUDModel);
                hudModel.gameSprite.gsc_.keyInfoRequest(this.originalObjectType);
            }
            else {
                this.titleOverride = (keyInfo[this.originalObjectType][0] + " Key");
                this.descriptionOverride = (((keyInfo[this.originalObjectType][1] + "\n") + "Created By: ") + keyInfo[this.originalObjectType][2]);
                this.addTitle();
                this.addDescriptionText();
            }
        }
        else {
            this.addTitle();
            this.addDescriptionText();
        }
        this.addTierText();
        this.handleWisMod();
        this.buildCategorySpecificText();
        this.addUniqueEffectsToList();
        this.addNumProjectilesTagsToEffectsList();
        this.addProjectileTagsToEffectsList();
        this.addActivateTagsToEffectsList();
        this.addActivateOnEquipTagsToEffectsList();
        this.addDoseTagsToEffectsList();
        this.addMpCostTagToEffectsList();
        this.addFameBonusTagToEffectsList();
        this.makeEffectsList();
        this.makeLineTwo();
        this.makeRestrictionList();
        this.makeRestrictionText();
        //this.makeItemPowerText();
    }

    /*
    private function makeItemPowerText():void {
        var color:int;
        if (this.objectXML.hasOwnProperty("feedPower")) {
            color = ((((this.playerCanUse) || ((this.player == null)))) ? 0xFFFFFF : 16549442);
            this.powerText = new TextFieldDisplayConcrete().setSize(12).setColor(color).setBold(true).setTextWidth((((MAX_WIDTH - this.icon.width) - 4) - 30)).setWordWrap(true);
            this.powerText.setStringBuilder(new StaticStringBuilder().setString(("Feed Power: " + this.objectXML.feedPower)));
            this.powerText.filters = [new DropShadowFilter(0, 0, 0, 0.5, 12, 12)];
            waiter.push(this.powerText.textChanged);
            addChild(this.powerText);
        }
    }
    */

    private function onKeyInfoResponse(info:KeyInfoResponse):void {
        this.keyInfoResponse.remove(this.onKeyInfoResponse);
        this.removeTitle();
        this.removeDesc();
        this.titleOverride = info.name;
        this.descriptionOverride = info.description;
        keyInfo[this.originalObjectType] = [info.name, info.description, info.creator];
        this.addTitle();
        this.addDescriptionText();
    }

    private function addUniqueEffectsToList():void {
        var xmlList:XMLList;
        var xml:XML;
        var extraTooltipName:String;
        var extraTooltipDescription:String;
        var text:String;
        var lineBuilder:AppendingLineBuilder;
        if (this.objectXML.hasOwnProperty("ExtraTooltipData")) {
            xmlList = this.objectXML.ExtraTooltipData.EffectInfo;
            for each (xml in xmlList) {
                extraTooltipName = xml.attribute("name");
                extraTooltipDescription = xml.attribute("description");
                text = ((((extraTooltipName) && (extraTooltipDescription))) ? ": " : "\n");
                lineBuilder = new AppendingLineBuilder();
                if (extraTooltipName) {
                    lineBuilder.pushParams(extraTooltipName);
                }
                if (extraTooltipDescription) {
                    lineBuilder.pushParams(extraTooltipDescription, {}, TooltipHelper.getOpenTag(16777103), TooltipHelper.getCloseTag());
                }
                lineBuilder.setDelimiter(text);
                this.uniqueEffects.push(new Effect(TextKey.BLANK, {"data": lineBuilder}));
            }
        }
    }

    private function isEmptyEquipSlot():Boolean {
        return (((this.isEquippable) && ((this.curItemXML == null))));
    }

    private function addIcon():void {
        var xml:XML = ObjectLibrary.xmlLibrary_[this.objectType];
        var iconScale:int = 5;
        if ((((this.objectType == 4173)) || ((this.objectType == 4172)))) {
            iconScale = 8;
        }
        if (xml.hasOwnProperty("ScaleValue")) {
            iconScale = xml.ScaleValue;
        }
        var icon:BitmapData = ObjectLibrary.getRedrawnTextureFromType(this.objectType, 60, true, true, iconScale);
        icon = BitmapUtil.cropToBitmapData(icon, 4, 4, (icon.width - 8), (icon.height - 8));
        this.icon = new Bitmap(icon);
        addChild(this.icon);
    }

    private function addTierText():void {
        var notPet = !this.isPet();
        var notConsumable = !this.objectXML.hasOwnProperty("Consumable");
        var notTreasure = !this.objectXML.hasOwnProperty("Treasure");
        var hasTier:Boolean = this.objectXML.hasOwnProperty("Tier");
        if (((((notPet) && (notConsumable))) && (notTreasure))) {
            this.tierText = new TextFieldDisplayConcrete().setSize(16).setColor(0xFFFFFF).setTextWidth(30).setBold(true);
            if (hasTier) {
                this.tierText.setStringBuilder(new LineBuilder().setParams(TextKey.TIER_ABBR, {"tier": this.objectXML.Tier}));
            }
            else {
                if (this.objectXML.hasOwnProperty("@setType")) {
                    this.tierText.setColor(0xFF9900);
                    this.tierText.setStringBuilder(new StaticStringBuilder("ST"));
                }
                else {
                    this.tierText.setColor(9055202);
                    this.tierText.setStringBuilder(new LineBuilder().setParams(TextKey.UNTIERED_ABBR));
                }
            }
            addChild(this.tierText);
        }
    }

    private function isPet():Boolean {
        var activateTags:XMLList;
        activateTags = this.objectXML.Activate.(text() == "PermaPet");
        return ((activateTags.length() >= 1));
    }

    private function removeTitle() {
        removeChild(this.titleText);
    }

    private function removeDesc() {
        removeChild(this.descText);
    }

    private function addTitle():void {
        var isUsable:int = ((((this.playerCanUse) || ((this.player == null)))) ? 0xFFFFFF : 16549442);
        this.titleText = new TextFieldDisplayConcrete().setSize(16).setColor(isUsable).setBold(true).setTextWidth((((MAX_WIDTH - this.icon.width) - 4) - 30)).setWordWrap(true);
        if (this.titleOverride) {
            this.titleText.setStringBuilder(new StaticStringBuilder(this.titleOverride));
        }
        else {
            this.titleText.setStringBuilder(new LineBuilder().setParams(ObjectLibrary.typeToDisplayId_[this.objectType]));
        }
        this.titleText.filters = [new DropShadowFilter(0, 0, 0, 0.5, 12, 12)];
        waiter.push(this.titleText.textChanged);
        addChild(this.titleText);
    }

    private function buildUniqueTooltipData():String {
        var xmlList:XMLList;
        var effect:Vector.<Effect>;
        var xml:XML;
        if (this.objectXML.hasOwnProperty("ExtraTooltipData")) {
            xmlList = this.objectXML.ExtraTooltipData.EffectInfo;
            effect = new Vector.<Effect>();
            for each (xml in xmlList) {
                effect.push(new Effect(xml.attribute("name"), xml.attribute("description")));
            }
        }
        return ("");
    }

    private function makeEffectsList():void {
        var lineBuilder:AppendingLineBuilder;
        if (((((!((this.effects.length == 0))) || (!((this.comparisonResults.lineBuilder == null))))) || (this.objectXML.hasOwnProperty("ExtraTooltipData")))) {
            this.line1 = new LineBreakDesign((MAX_WIDTH - 12), 0);
            this.effectsText = new TextFieldDisplayConcrete().setSize(14).setColor(0xB3B3B3).setTextWidth(MAX_WIDTH).setWordWrap(true).setHTML(true);
            lineBuilder = this.getEffectsStringBuilder();
            this.effectsText.setStringBuilder(lineBuilder);
            this.effectsText.filters = [new DropShadowFilter(0, 0, 0, 0.5, 12, 12)];
            if (lineBuilder.hasLines()) {
                addChild(this.line1);
                addChild(this.effectsText);
            }
        }
    }

    private function getEffectsStringBuilder():AppendingLineBuilder {
        var lineBuilder:AppendingLineBuilder = new AppendingLineBuilder();
        this.appendEffects(this.uniqueEffects, lineBuilder);
        if (this.comparisonResults.lineBuilder.hasLines()) {
            lineBuilder.pushParams(TextKey.BLANK, {"data": this.comparisonResults.lineBuilder});
        }
        this.appendEffects(this.effects, lineBuilder);
        return (lineBuilder);
    }

    private function appendEffects(effs:Vector.<Effect>, lineBuilder:AppendingLineBuilder):void {
        var eff:Effect;
        var openingTag:String;
        var closingTag:String;
        for each (eff in effs) {
            openingTag = "";
            closingTag = "";
            if (eff.color_) {
                openingTag = (('<font color="#' + eff.color_.toString(16)) + '">');
                closingTag = "</font>";
            }
            lineBuilder.pushParams(eff.name_, eff.getValueReplacementsWithColor(), openingTag, closingTag);
        }
    }

    private function addNumProjectilesTagsToEffectsList():void {
        if (((this.objectXML.hasOwnProperty("NumProjectiles")) && !this.comparisonResults.processedTags.hasOwnProperty(this.objectXML.NumProjectiles.toXMLString()))) {
            this.effects.push(new Effect(TextKey.SHOTS, {"numShots": this.objectXML.NumProjectiles}));
        }
    }

    private function addFameBonusTagToEffectsList():void {
        var fameBonus:int;
        var color:uint;
        var prevFameBonus:int;
        if (this.objectXML.hasOwnProperty("FameBonus")) {
            fameBonus = int(this.objectXML.FameBonus);
            color = ((this.playerCanUse) ? TooltipHelper.BETTER_COLOR : TooltipHelper.NO_DIFF_COLOR);
            if (((!((this.curItemXML == null))) && (this.curItemXML.hasOwnProperty("FameBonus")))) {
                prevFameBonus = int(this.curItemXML.FameBonus.text());
                color = TooltipHelper.getTextColor((fameBonus - prevFameBonus));
            }
            this.effects.push(new Effect(TextKey.FAME_BONUS, {"percent": (this.objectXML.FameBonus + "%")}).setReplacementsColor(color));
        }
    }

    private function addMpCostTagToEffectsList():void {
        if (this.objectXML.hasOwnProperty("MpEndCost")) {
            if (!this.comparisonResults.processedTags[this.objectXML.MpEndCost[0].toXMLString()]) {
                this.effects.push(new Effect(TextKey.MP_COST, {"cost": this.objectXML.MpEndCost}));
            }
        }
        else {
            if (((this.objectXML.hasOwnProperty("MpCost")) && (!(this.comparisonResults.processedTags[this.objectXML.MpCost[0].toXMLString()])))) {
                if (!this.comparisonResults.processedTags[this.objectXML.MpCost[0].toXMLString()]) {
                    this.effects.push(new Effect(TextKey.MP_COST, {"cost": this.objectXML.MpCost}));
                }
            }
        }
    }

    private function addDoseTagsToEffectsList():void {
        if (this.objectXML.hasOwnProperty("Doses")) {
            this.effects.push(new Effect(TextKey.DOSES, {"dose": this.objectXML.Doses}));
        }
        if (this.objectXML.hasOwnProperty("Quantity")) {
            this.effects.push(new Effect("Quantity: {quantity}", {"quantity": this.objectXML.Quantity}));
        }
    }

    private function addProjectileTagsToEffectsList():void {
        var projectile:XML;
        var minDamage:int;
        var maxDamage:int;
        var range:Number;
        var processedTags:XML;
        if (((this.objectXML.hasOwnProperty("Projectile")) && (!(this.comparisonResults.processedTags.hasOwnProperty(this.objectXML.Projectile.toXMLString()))))) {
            projectile = XML(this.objectXML.Projectile);
            minDamage = int(projectile.MinDamage);
            maxDamage = int(projectile.MaxDamage);
            this.effects.push(new Effect(TextKey.DAMAGE, {"damage": (((minDamage == maxDamage)) ? minDamage : ((minDamage + " - ") + maxDamage)).toString()}));
            range = ((Number(projectile.Speed) * Number(projectile.LifetimeMS)) / 10000);
            this.effects.push(new Effect(TextKey.RANGE, {"range": TooltipHelper.getFormattedRangeString(range)}));
            if (this.objectXML.Projectile.hasOwnProperty("MultiHit")) {
                this.effects.push(new Effect(TextKey.MULTIHIT, {}).setColor(TooltipHelper.NO_DIFF_COLOR));
            }
            if (this.objectXML.Projectile.hasOwnProperty("PassesCover")) {
                this.effects.push(new Effect(TextKey.PASSES_COVER, {}).setColor(TooltipHelper.NO_DIFF_COLOR));
            }
            if (this.objectXML.Projectile.hasOwnProperty("ArmorPiercing")) {
                this.effects.push(new Effect(TextKey.ARMOR_PIERCING, {}).setColor(TooltipHelper.NO_DIFF_COLOR));
            }
            for each (processedTags in projectile.ConditionEffect) {
                if (this.comparisonResults.processedTags[processedTags.toXMLString()] == null) {
                    this.effects.push(new Effect(TextKey.SHOT_EFFECT, {"effect": ""}));
                    this.effects.push(new Effect(TextKey.EFFECT_FOR_DURATION, {
                        "effect": this.objectXML.Projectile.ConditionEffect,
                        "duration": this.objectXML.Projectile.ConditionEffect.@duration
                    }).setColor(TooltipHelper.NO_DIFF_COLOR));
                }
            }
        }
    }

    private function addActivateTagsToEffectsList():void {
        var _local1:XML;
        var _local2:String;
        var _local3:int;
        var _local4:int;
        var _local5:String;
        var _local6:String;
        var _local7:Object;
        var _local8:String;
        var _local9:uint;
        var _local10:XML;
        var _local11:Object;
        var _local12:String;
        var _local13:uint;
        var _local14:XML;
        var _local15:String;
        var _local16:Object;
        var _local17:String;
        var _local18:Object;
        var _local19:Number;
        var _local20:Number;
        var _local21:Number;
        var _local22:Number;
        var _local23:Number;
        var _local24:Number;
        var _local25:Number;
        var _local26:Number;
        var _local27:Number;
        var _local28:Number;
        var _local29:Number;
        var _local30:Number;
        var _local31:AppendingLineBuilder;
        for each (_local1 in this.objectXML.Activate) {
            _local5 = this.comparisonResults.processedTags[_local1.toXMLString()];
            if (!this.comparisonResults.processedTags[_local1.toXMLString()]) {
                _local6 = _local1.toString();
                switch (_local6) {
                    case ActivationType.COND_EFFECT_AURA:
                        this.effects.push(new Effect(TextKey.PARTY_EFFECT, {"effect": new AppendingLineBuilder().pushParams(TextKey.WITHIN_SQRS, {"range": _local1.@range}, TooltipHelper.getOpenTag(TooltipHelper.NO_DIFF_COLOR), TooltipHelper.getCloseTag())}));
                        this.effects.push(new Effect(TextKey.EFFECT_FOR_DURATION, {
                            "effect": _local1.@effect,
                            "duration": _local1.@duration
                        }).setColor(TooltipHelper.NO_DIFF_COLOR));
                        break;
                    case ActivationType.COND_EFFECT_SELF:
                        this.effects.push(new Effect(TextKey.EFFECT_ON_SELF, {"effect": ""}));
                        this.effects.push(new Effect(TextKey.EFFECT_FOR_DURATION, {
                            "effect": _local1.@effect,
                            "duration": _local1.@duration
                        }));
                        break;
                    case ActivationType.HEAL:
                        this.effects.push(new Effect(TextKey.INCREMENT_STAT, {
                            "statAmount": (("+" + _local1.@amount) + " "),
                            "statName": new LineBuilder().setParams(TextKey.STATUS_BAR_HEALTH_POINTS)
                        }));
                        break;
                    case ActivationType.HEAL_NOVA:
                        this.effects.push(new Effect(TextKey.PARTY_HEAL, {
                            "effect": new AppendingLineBuilder().pushParams(TextKey.HP_WITHIN_SQRS, {
                                "amount": _local1.@amount,
                                "range": _local1.@range
                            }, TooltipHelper.getOpenTag(TooltipHelper.NO_DIFF_COLOR), TooltipHelper.getCloseTag())
                        }));
                        break;
                    case ActivationType.MAGIC:
                        this.effects.push(new Effect(TextKey.INCREMENT_STAT, {
                            "statAmount": (("+" + _local1.@amount) + " "),
                            "statName": new LineBuilder().setParams(TextKey.STATUS_BAR_MANA_POINTS)
                        }));
                        break;
                    case ActivationType.MAGIC_NOVA:
                        this.effects.push(new Effect(TextKey.FILL_PARTY_MAGIC, (((_local1.@amount + " MP at ") + _local1.@range) + " sqrs")));
                        break;
                    case ActivationType.TELEPORT:
                        this.effects.push(new Effect(TextKey.BLANK, {"data": new LineBuilder().setParams(TextKey.TELEPORT_TO_TARGET)}));
                        break;
                    case ActivationType.VAMPIRE_BLAST:
                        this.effects.push(new Effect(TextKey.STEAL, {
                            "effect": new AppendingLineBuilder().pushParams(TextKey.HP_WITHIN_SQRS, {
                                "amount": _local1.@totalDamage,
                                "range": _local1.@radius
                            }, TooltipHelper.getOpenTag(TooltipHelper.NO_DIFF_COLOR), TooltipHelper.getCloseTag())
                        }));
                        break;
                    case ActivationType.TRAP:
                        _local7 = ((_local1.hasOwnProperty("@condEffect")) ? _local1.@condEffect : new LineBuilder().setParams(TextKey.CONDITION_EFFECT_SLOWED));
                        _local8 = ((_local1.hasOwnProperty("@condDuration")) ? _local1.@condDuration : "5");
                        this.effects.push(new Effect(TextKey.TRAP, {
                            "data": new AppendingLineBuilder().pushParams(TextKey.HP_WITHIN_SQRS, {
                                "amount": _local1.@totalDamage,
                                "range": _local1.@radius
                            }, TooltipHelper.getOpenTag(TooltipHelper.NO_DIFF_COLOR), TooltipHelper.getCloseTag()).pushParams(TextKey.EFFECT_FOR_DURATION, {
                                "effect": _local7,
                                "duration": _local8
                            }, TooltipHelper.getOpenTag(TooltipHelper.NO_DIFF_COLOR), TooltipHelper.getCloseTag())
                        }));
                        break;
                    case ActivationType.STASIS_BLAST:
                        this.effects.push(new Effect(TextKey.STASIS_GROUP, {"stasis": new AppendingLineBuilder().pushParams(TextKey.SEC_COUNT, {"duration": _local1.@duration}, TooltipHelper.getOpenTag(TooltipHelper.NO_DIFF_COLOR), TooltipHelper.getCloseTag())}));
                        break;
                    case ActivationType.DECOY:
                        this.effects.push(new Effect(TextKey.DECOY, {"data": new AppendingLineBuilder().pushParams(TextKey.SEC_COUNT, {"duration": _local1.@duration}, TooltipHelper.getOpenTag(TooltipHelper.NO_DIFF_COLOR), TooltipHelper.getCloseTag())}));
                        break;
                    case ActivationType.LIGHTNING:
                        this.effects.push(new Effect(TextKey.LIGHTNING, {
                            "data": new AppendingLineBuilder().pushParams(TextKey.DAMAGE_TO_TARGETS, {
                                "damage": _local1.@totalDamage,
                                "targets": _local1.@maxTargets
                            }, TooltipHelper.getOpenTag(TooltipHelper.NO_DIFF_COLOR), TooltipHelper.getCloseTag())
                        }));
                        break;
                    case ActivationType.POISON_GRENADE:
                        this.effects.push(new Effect(TextKey.POISON_GRENADE, {"data": ""}));
                        this.effects.push(new Effect(TextKey.POISON_GRENADE_DATA, {
                            "damage": _local1.@totalDamage,
                            "duration": _local1.@duration,
                            "radius": _local1.@radius
                        }).setColor(TooltipHelper.NO_DIFF_COLOR));
                        break;
                    case ActivationType.REMOVE_NEG_COND:
                        this.effects.push(new Effect(TextKey.REMOVES_NEGATIVE, {}).setColor(TooltipHelper.NO_DIFF_COLOR));
                        break;
                    case ActivationType.REMOVE_NEG_COND_SELF:
                        this.effects.push(new Effect(TextKey.REMOVES_NEGATIVE, {}).setColor(TooltipHelper.NO_DIFF_COLOR));
                        break;
                    case ActivationType.GENERIC_ACTIVATE:
                        _local9 = 16777103;
                        if (this.curItemXML != null) {
                            _local10 = this.getEffectTag(this.curItemXML, _local1.@effect);
                            if (_local10 != null) {
                                _local19 = Number(_local1.@range);
                                _local20 = Number(_local10.@range);
                                _local21 = Number(_local1.@duration);
                                _local22 = Number(_local10.@duration);
                                _local23 = ((_local19 - _local20) + (_local21 - _local22));
                                if (_local23 > 0) {
                                    _local9 = 0xFF00;
                                }
                                else {
                                    if (_local23 < 0) {
                                        _local9 = 0xFF0000;
                                    }
                                }
                            }
                        }
                        _local11 = {
                            "range": _local1.@range,
                            "effect": _local1.@effect,
                            "duration": _local1.@duration
                        };
                        _local12 = "Within {range} sqrs {effect} for {duration} seconds";
                        if (_local1.@target != "enemy") {
                            this.effects.push(new Effect(TextKey.PARTY_EFFECT, {"effect": LineBuilder.returnStringReplace(_local12, _local11)}).setReplacementsColor(_local9));
                        }
                        else {
                            this.effects.push(new Effect(TextKey.ENEMY_EFFECT, {"effect": LineBuilder.returnStringReplace(_local12, _local11)}).setReplacementsColor(_local9));
                        }
                        break;
                    case ActivationType.STAT_BOOST_AURA:
                        _local13 = 16777103;
                        if (this.curItemXML != null) {
                            _local14 = this.getStatTag(this.curItemXML, _local1.@stat);
                            if (_local14 != null) {
                                _local24 = Number(_local1.@range);
                                _local25 = Number(_local14.@range);
                                _local26 = Number(_local1.@duration);
                                _local27 = Number(_local14.@duration);
                                _local28 = Number(_local1.@amount);
                                _local29 = Number(_local14.@amount);
                                _local30 = (((_local24 - _local25) + (_local26 - _local27)) + (_local28 - _local29));
                                if (_local30 > 0) {
                                    _local13 = 0xFF00;
                                }
                                else {
                                    if (_local30 < 0) {
                                        _local13 = 0xFF0000;
                                    }
                                }
                            }
                        }
                        _local3 = int(_local1.@stat);
                        _local15 = LineBuilder.getLocalizedString2(StatData.statToName(_local3));
                        _local16 = {
                            "range": _local1.@range,
                            "stat": _local15,
                            "amount": _local1.@amount,
                            "duration": _local1.@duration
                        };
                        _local17 = "Within {range} sqrs increase {stat} by {amount} for {duration} seconds";
                        this.effects.push(new Effect(TextKey.PARTY_EFFECT, {"effect": LineBuilder.returnStringReplace(_local17, _local16)}).setReplacementsColor(_local13));
                        break;
                    case ActivationType.INCREMENT_STAT:
                        _local3 = int(_local1.@stat);
                        _local4 = int(_local1.@amount);
                        _local18 = {};
                        if (((!((_local3 == StatData.HP_STAT))) && (!((_local3 == StatData.MP_STAT))))) {
                            _local2 = TextKey.PERMANENTLY_INCREASES;
                            _local18["statName"] = new LineBuilder().setParams(StatData.statToName(_local3));
                            this.effects.push(new Effect(_local2, _local18).setColor(16777103));
                            break;
                        }
                        _local2 = TextKey.BLANK;
                        _local31 = new AppendingLineBuilder().setDelimiter(" ");
                        _local31.pushParams(TextKey.BLANK, {"data": new StaticStringBuilder(("+" + _local4))});
                        _local31.pushParams(StatData.statToName(_local3));
                        _local18["data"] = _local31;
                        this.effects.push(new Effect(_local2, _local18));
                        break;
                }
            }
        }
    }

    private function getEffectTag(xml:XML, effectValue:String):XML {
        var matches:XMLList;
        var tag:XML;
        matches = xml.Activate.(text() == ActivationType.GENERIC_ACTIVATE);
        for each (tag in matches) {
            if (tag.@effect == effectValue) {
                return (tag);
            }
        }
        return null;
    }

    private function getStatTag(xml:XML, statValue:String):XML {
        var matches:XMLList;
        var tag:XML;
        matches = xml.Activate.(text() == ActivationType.STAT_BOOST_AURA);
        for each (tag in matches) {
            if (tag.@stat == statValue) {
                return (tag);
            }
        }
        return null;
    }

    private function addActivateOnEquipTagsToEffectsList():void {
        var activateOnEquip:XML;
        var effNotPushed:Boolean = true;
        for each (activateOnEquip in this.objectXML.ActivateOnEquip) {
            if (effNotPushed) {
                this.effects.push(new Effect(TextKey.ON_EQUIP, ""));
                effNotPushed = false;
            }
            if (activateOnEquip.toString() == "IncrementStat") {
                this.effects.push(new Effect(TextKey.INCREMENT_STAT, this.getComparedStatText(activateOnEquip)).setReplacementsColor(this.getComparedStatColor(activateOnEquip)));
            }
        }
    }

    private function getComparedStatText(xml:XML):Object {
        var statName:int = int(xml.@stat);
        var statAmount:int = int(xml.@amount);
        var isPositive:String = (((statAmount) > -1) ? "+" : "");
        return ({
            "statAmount": ((isPositive + String(statAmount)) + " "),
            "statName": new LineBuilder().setParams(StatData.statToName(statName))
        });
    }

    private function getComparedStatColor(activateXML:XML):uint {
        var match:XML;
        var otherAmount:int;
        var stat:int = int(activateXML.@stat);
        var amount:int = int(activateXML.@amount);
        var textColor:uint = ((this.playerCanUse) ? TooltipHelper.BETTER_COLOR : TooltipHelper.NO_DIFF_COLOR);
        var otherMatches:XMLList;
        if (this.curItemXML != null) {
            otherMatches = this.curItemXML.ActivateOnEquip.(@stat == stat);
        }
        if (((!((otherMatches == null))) && ((otherMatches.length() == 1)))) {
            match = XML(otherMatches[0]);
            otherAmount = int(match.@amount);
            textColor = TooltipHelper.getTextColor((amount - otherAmount));
        }
        if (amount < 0) {
            textColor = 0xFF0000;
        }
        return (textColor);
    }

    private function addEquipmentItemRestrictions():void {
        if (!this.objectXML.hasOwnProperty("Treasure")) {
            this.restrictions.push(new Restriction(TextKey.EQUIP_TO_USE, 0xB3B3B3, false));
            if (((this.isInventoryFull) || ((this.inventoryOwnerType == InventoryOwnerTypes.CURRENT_PLAYER)))) {
                this.restrictions.push(new Restriction(TextKey.DOUBLE_CLICK_EQUIP, 0xB3B3B3, false));
            }
            else {
                this.restrictions.push(new Restriction(TextKey.DOUBLE_CLICK_TAKE, 0xB3B3B3, false));
            }
        }
    }

    private function addAbilityItemRestrictions():void {
        this.restrictions.push(new Restriction(TextKey.KEYCODE_TO_USE, 0xFFFFFF, false));
    }

    private function addConsumableItemRestrictions():void {
        this.restrictions.push(new Restriction(TextKey.CONSUMED_WITH_USE, 0xB3B3B3, false));
        if (((this.isInventoryFull) || ((this.inventoryOwnerType == InventoryOwnerTypes.CURRENT_PLAYER)))) {
            this.restrictions.push(new Restriction(TextKey.DOUBLE_CLICK_OR_SHIFT_CLICK_TO_USE, 0xFFFFFF, false));
        }
        else {
            this.restrictions.push(new Restriction(TextKey.DOUBLE_CLICK_TAKE_SHIFT_CLICK_USE, 0xFFFFFF, false));
        }
    }

    private function addReusableItemRestrictions():void {
        this.restrictions.push(new Restriction(TextKey.CAN_BE_USED_MULTIPLE_TIMES, 0xB3B3B3, false));
        this.restrictions.push(new Restriction(TextKey.DOUBLE_CLICK_OR_SHIFT_CLICK_TO_USE, 0xFFFFFF, false));
    }

    private function makeRestrictionList():void {
        var xml:XML;
        var meetsRequirement:Boolean;
        var stat:int;
        var value:int;
        this.restrictions = new Vector.<Restriction>();
        if (((((this.objectXML.hasOwnProperty("VaultItem")) && (!((this.invType == -1))))) && (!((this.invType == ObjectLibrary.idToType_["Vault Chest"]))))) {
            this.restrictions.push(new Restriction(TextKey.STORE_IN_VAULT, 16549442, true));
        }
        if (this.objectXML.hasOwnProperty("Soulbound")) {
            this.restrictions.push(new Restriction(TextKey.ITEM_SOULBOUND, 0xB3B3B3, false));
        }
        if (this.objectXML.hasOwnProperty("@setType")) {
            this.restrictions.push(new Restriction(("This item is a part of " + this.objectXML.attribute("setName")), 0xFF9900, false));
        }
        if (this.playerCanUse) {
            if (this.objectXML.hasOwnProperty("Usable")) {
                this.addAbilityItemRestrictions();
                this.addEquipmentItemRestrictions();
            }
            else {
                if (this.objectXML.hasOwnProperty("Consumable")) {
                    this.addConsumableItemRestrictions();
                }
                else {
                    if (this.objectXML.hasOwnProperty("InvUse")) {
                        this.addReusableItemRestrictions();
                    }
                    else {
                        this.addEquipmentItemRestrictions();
                    }
                }
            }
        }
        else {
            if (this.player != null) {
                this.restrictions.push(new Restriction(TextKey.NOT_USABLE_BY, 16549442, true));
            }
        }
        var usable:Vector.<String> = ObjectLibrary.usableBy(this.objectType);
        if (usable != null) {
            this.restrictions.push(new Restriction(TextKey.USABLE_BY, 0xB3B3B3, false));
        }
        for each (xml in this.objectXML.EquipRequirement) {
            meetsRequirement = ObjectLibrary.playerMeetsRequirement(xml, this.player);
            if (xml.toString() == "Stat") {
                stat = int(xml.@stat);
                value = int(xml.@value);
                this.restrictions.push(new Restriction(((("Requires " + StatData.statToName(stat)) + " of ") + value), ((meetsRequirement) ? 0xB3B3B3 : 16549442), ((!meetsRequirement))));
            }
        }
    }

    private function makeLineTwo():void {
        this.line2 = new LineBreakDesign((MAX_WIDTH - 12), 0);
        addChild(this.line2);
    }

    private function makeRestrictionText():void {
        if (this.restrictions.length != 0) {
            this.restrictionsText = new TextFieldDisplayConcrete().setSize(14).setColor(0xB3B3B3).setTextWidth((MAX_WIDTH - 4)).setIndent(-10).setLeftMargin(10).setWordWrap(true).setHTML(true);
            this.restrictionsText.setStringBuilder(this.buildRestrictionsLineBuilder());
            this.restrictionsText.filters = [new DropShadowFilter(0, 0, 0, 0.5, 12, 12)];
            waiter.push(this.restrictionsText.textChanged);
            addChild(this.restrictionsText);
        }
    }

    private function buildRestrictionsLineBuilder():StringBuilder {
        var restriction:Restriction;
        var openingTag:String;
        var closingTag:String;
        var unUsableClass:String;
        var lineBuilder:AppendingLineBuilder = new AppendingLineBuilder();
        for each (restriction in this.restrictions) {
            openingTag = ((restriction.bold_) ? "<b>" : "");
            openingTag = openingTag.concat((('<font color="#' + restriction.color_.toString(16)) + '">'));
            closingTag = "</font>";
            closingTag = closingTag.concat(((restriction.bold_) ? "</b>" : ""));
            unUsableClass = ((this.player) ? ObjectLibrary.typeToDisplayId_[this.player.objectType_] : "");
            lineBuilder.pushParams(restriction.text_, {
                "unUsableClass": unUsableClass,
                "usableClasses": this.getUsableClasses(),
                "keyCode": KeyCodes.CharCodeStrings[Parameters.data_.useSpecial]
            }, openingTag, closingTag);
        }
        return (lineBuilder);
    }

    private function getUsableClasses():StringBuilder {
        var usableClasses:String;
        var objType:Vector.<String> = ObjectLibrary.usableBy(this.objectType);
        var lineBuilder:AppendingLineBuilder = new AppendingLineBuilder();
        lineBuilder.setDelimiter(", ");
        for each (usableClasses in objType) {
            lineBuilder.pushParams(usableClasses);
        }
        return (lineBuilder);
    }

    private function addDescriptionText():void {
        this.descText = new TextFieldDisplayConcrete().setSize(14).setColor(0xB3B3B3).setTextWidth(MAX_WIDTH).setWordWrap(true);
        if (this.descriptionOverride) {
            this.descText.setStringBuilder(new StaticStringBuilder(this.descriptionOverride));
        }
        else {
            this.descText.setStringBuilder(new LineBuilder().setParams(String(this.objectXML.Description)));
        }
        this.descText.filters = [new DropShadowFilter(0, 0, 0, 0.5, 12, 12)];
        waiter.push(this.descText.textChanged);
        addChild(this.descText);
    }

    override protected function alignUI():void {
        this.titleText.x = (this.icon.width + 4);
        this.titleText.y = ((this.icon.height / 2) - (this.titleText.height / 2));
        if (this.tierText) {
            this.tierText.y = ((this.icon.height / 2) - (this.tierText.height / 2));
            this.tierText.x = (MAX_WIDTH - 30);
        }
        this.descText.x = 4;
        this.descText.y = (this.icon.height + 2);
        if (contains(this.line1)) {
            this.line1.x = 8;
            this.line1.y = ((this.descText.y + this.descText.height) + 8);
            this.effectsText.x = 4;
            this.effectsText.y = (this.line1.y + 8);
        }
        else {
            this.line1.y = (this.descText.y + this.descText.height);
            this.effectsText.y = this.line1.y;
        }
        this.line2.x = 8;
        this.line2.y = ((this.effectsText.y + this.effectsText.height) + 8);
        var resTextY:uint = (this.line2.y + 8);
        if (this.restrictionsText) {
            this.restrictionsText.x = 4;
            this.restrictionsText.y = resTextY;
            resTextY = (resTextY + this.restrictionsText.height);
        }
        if (this.powerText) {
            if (contains(this.powerText)) {
                this.powerText.x = 4;
                this.powerText.y = resTextY;
            }
        }
    }

    private function buildCategorySpecificText():void {
        if (this.curItemXML != null) {
            this.comparisonResults = this.slotTypeToTextBuilder.getComparisonResults(this.objectXML, this.curItemXML);
        }
        else {
            this.comparisonResults = new SlotComparisonResult();
        }
    }

    private function handleWisMod():void {
        var itemXML:XML;
        var xml:XML;
        var effName:String;
        var useWisMod:String;
        if (this.player == null) {
            return;
        }
        var totalWisdom:Number = (this.player.wisdom_ + this.player.wisdomBoost_);
        if (totalWisdom < 30) {
            return;
        }
        var item:Vector.<XML> = new Vector.<XML>();
        if (this.curItemXML != null) {
            this.curItemXML = this.curItemXML.copy();
            item.push(this.curItemXML);
        }
        if (this.objectXML != null) {
            this.objectXML = this.objectXML.copy();
            item.push(this.objectXML);
        }
        for each (xml in item) {
            for each (itemXML in xml.Activate) {
                effName = itemXML.toString();
                if (itemXML.@effect != "Stasis") {
                    useWisMod = itemXML.@useWisMod;
                    if (!(((((((useWisMod == "")) || ((useWisMod == "false")))) || ((useWisMod == "0")))) || ((itemXML.@effect == "Stasis")))) {
                        switch (effName) {
                            case ActivationType.HEAL_NOVA:
                                itemXML.@amount = this.modifyWisModStat(itemXML.@amount, 0);
                                itemXML.@range = this.modifyWisModStat(itemXML.@range);
                                break;
                            case ActivationType.COND_EFFECT_AURA:
                                itemXML.@duration = this.modifyWisModStat(itemXML.@duration);
                                itemXML.@range = this.modifyWisModStat(itemXML.@range);
                                break;
                            case ActivationType.COND_EFFECT_SELF:
                                itemXML.@duration = this.modifyWisModStat(itemXML.@duration);
                                break;
                            case ActivationType.STAT_BOOST_AURA:
                                itemXML.@amount = this.modifyWisModStat(itemXML.@amount, 0);
                                itemXML.@duration = this.modifyWisModStat(itemXML.@duration);
                                itemXML.@range = this.modifyWisModStat(itemXML.@range);
                                break;
                            case ActivationType.GENERIC_ACTIVATE:
                                itemXML.@duration = this.modifyWisModStat(itemXML.@duration);
                                itemXML.@range = this.modifyWisModStat(itemXML.@range);
                                break;
                        }
                    }
                }
            }
        }
    }

    private function modifyWisModStat(amount:String, power:Number = 1):String {
        var amount_:Number;
        var posNegMult:int;
        var factor:Number;
        var finalAmount = "-1";
        var totalWisdom:Number = (this.player.wisdom_ + this.player.wisdomBoost_);
        if (totalWisdom < 30) {
            finalAmount = amount;
        } else {
            amount_ = Number(amount);
            posNegMult = (((amount_) < 0) ? -1 : 1);
            factor = (((amount_ * totalWisdom) / 150) + (amount_ * posNegMult));
            factor = (Math.floor((factor * Math.pow(10, power))) / Math.pow(10, power));
            if ((factor - (int(factor) * posNegMult)) >= ((1 / Math.pow(10, power)) * posNegMult)) {
                finalAmount = factor.toFixed(1);
            } else {
                finalAmount = factor.toFixed(0);
            }
        }
        return (finalAmount);
    }


}
}

import kabam.rotmg.text.view.stringBuilder.AppendingLineBuilder;
import kabam.rotmg.text.view.stringBuilder.LineBuilder;

class Effect {

    public var name_:String;
    public var valueReplacements_:Object;
    public var replacementColor_:uint = 16777103;
    public var color_:uint = 0xB3B3B3;

    public function Effect(name:String, valueReplacements:Object) {
        this.name_ = name;
        this.valueReplacements_ = valueReplacements;
    }

    public function setColor(color:uint):Effect {
        this.color_ = color;
        return (this);
    }

    public function setReplacementsColor(replacementColor:uint):Effect {
        this.replacementColor_ = replacementColor;
        return (this);
    }

    public function getValueReplacementsWithColor():Object {
        var _local4:String;
        var _local5:LineBuilder;
        var _local1:Object = {};
        var _local2 = "";
        var _local3 = "";
        if (this.replacementColor_) {
            _local2 = (('</font><font color="#' + this.replacementColor_.toString(16)) + '">');
            _local3 = (('</font><font color="#' + this.color_.toString(16)) + '">');
        }
        for (_local4 in this.valueReplacements_) {
            if ((this.valueReplacements_[_local4] is AppendingLineBuilder)) {
                _local1[_local4] = this.valueReplacements_[_local4];
            }
            else {
                if ((this.valueReplacements_[_local4] is LineBuilder)) {
                    _local5 = (this.valueReplacements_[_local4] as LineBuilder);
                    _local5.setPrefix(_local2).setPostfix(_local3);
                    _local1[_local4] = _local5;
                }
                else {
                    _local1[_local4] = ((_local2 + this.valueReplacements_[_local4]) + _local3);
                }
            }
        }
        return (_local1);
    }


}
class Restriction {

    public var text_:String;
    public var color_:uint;
    public var bold_:Boolean;

    public function Restriction(text:String, color:uint, bold:Boolean) {
        text_ = text;
        color_ = color;
        bold_ = bold;
    }

}

