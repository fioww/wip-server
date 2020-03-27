package com.company.assembleegameclient.ui.marketplace {
import com.company.assembleegameclient.account.ui.TextInputField;
import com.company.assembleegameclient.objects.Player;
import com.company.assembleegameclient.ui.options.*;
import com.company.assembleegameclient.game.GameSprite;
import com.company.assembleegameclient.parameters.Parameters;
import com.company.assembleegameclient.screens.TitleMenuOption;
import com.company.assembleegameclient.sound.Music;
import com.company.assembleegameclient.sound.SFX;
import com.company.assembleegameclient.ui.StatusBar;
import com.company.assembleegameclient.ui.panels.itemgrids.InventoryGrid;
import com.company.rotmg.graphics.ScreenGraphic;
import com.company.ui.BaseSimpleText;
import com.company.util.AssetLibrary;
import com.company.util.KeyCodes;

import flash.display.BitmapData;
import flash.display.Sprite;
import flash.display.StageDisplayState;
import flash.events.Event;
import flash.events.KeyboardEvent;
import flash.events.MouseEvent;
import flash.filters.DropShadowFilter;
import flash.geom.Point;
import flash.net.URLRequest;
import flash.net.URLRequestMethod;
import flash.net.navigateToURL;
import flash.system.Capabilities;
import flash.text.TextFieldAutoSize;
import flash.ui.Mouse;
import flash.ui.MouseCursor;
import flash.ui.MouseCursorData;

import kabam.rotmg.constants.GeneralConstants;

import kabam.rotmg.game.view.components.InventoryTabContent;
import kabam.rotmg.messaging.impl.data.StatData;

import kabam.rotmg.text.model.TextKey;
import kabam.rotmg.text.view.TextFieldDisplayConcrete;
import kabam.rotmg.text.view.stringBuilder.LineBuilder;
import kabam.rotmg.text.view.stringBuilder.StaticStringBuilder;
import kabam.rotmg.text.view.stringBuilder.StringBuilder;
import kabam.rotmg.ui.UIUtils;

public class Marketplace extends Sprite {
    public static const Y_POSITION:int = 550;
    private var settings_:Vector.<Sprite>;
    private var gs_:GameSprite;
    private var closeButton_:TitleMenuOption;
    private var slotNum_:TextInputField;
    private var priceAmnt_:TextInputField;
    private var player_:Player;
    private var createButton_:TitleMenuOption;

    public function Marketplace(_arg1:GameSprite) {
        var _local2:TextFieldDisplayConcrete;
        this.settings_ = new Vector.<Sprite>();
        super();
        this.gs_ = _arg1;
        graphics.clear();
        graphics.beginFill(0x2B2B2B, 0.8);
        graphics.drawRect(0, 0, 800, 600);
        graphics.endFill();
        graphics.lineStyle(1, 0x5E5E5E);
        graphics.moveTo(0, 100);
        graphics.lineTo(800, 100);
        graphics.lineStyle();
        _local2 = new TextFieldDisplayConcrete().setSize(36).setColor(0xFFFFFF);
        _local2.setBold(true);
        _local2.setStringBuilder(new LineBuilder().setParams("Create/Remove Offers"));
        _local2.setAutoSize(TextFieldAutoSize.CENTER);
        _local2.filters = [new DropShadowFilter(0, 0, 0)];
        _local2.x = ((800 / 2) - (_local2.width / 2));
        _local2.y = 8;
        addChild(_local2);
        addChild(new ScreenGraphic());


        /*SLOT NUMBER TEXT FIELD*/
        this.slotNum_ = new TextInputField("Slot Number of Item", false);
        this.slotNum_.inputText_.restrict = "1-8";
        this.slotNum_.inputText_.maxChars = 1;
        this.slotNum_.x = 140;
        this.slotNum_.y = 110;
        addChild(this.slotNum_);
        /*SLOT NUMBER TEXT FIELD*/

        /*PRICE AMOUNT TEXT FIELD*/
        this.priceAmnt_ = new TextInputField("Price Amount", false);
        this.priceAmnt_.inputText_.restrict = "1-100000";
        this.priceAmnt_.inputText_.maxChars = 6;
        this.priceAmnt_.x = 140;
        this.priceAmnt_.y = 220;
        addChild(this.priceAmnt_);
        /*PRICE AMOUNT TEXT FIELD*/

        /*CREATE BUTTON*/
        this.createButton_ = new TitleMenuOption("CREATE", 50, false);
        this.createButton_.x = 450;
        this.createButton_.y = 180;
        this.createButton_.addEventListener(MouseEvent.CLICK, this.onCreateClick_);
        addChild(this.createButton_);
        /*CREATE BUTTON*/

        /*CLOSE BUTTON*/
        this.closeButton_ = new TitleMenuOption("Close", 36, false);
        this.closeButton_.setVerticalAlign(TextFieldDisplayConcrete.MIDDLE);
        this.closeButton_.setAutoSize(TextFieldAutoSize.CENTER);
        this.closeButton_.addEventListener(MouseEvent.CLICK, this.onCloseClick);
        addChild(this.closeButton_);
        /*CLOSE BUTTON*/

        addEventListener(Event.ADDED_TO_STAGE, this.onAddedToStage);
        addEventListener(Event.REMOVED_FROM_STAGE, this.onRemovedFromStage);
    }

    private function onCreateClick_(_arg1:MouseEvent):void {
        //make player say "/market slotNum_.text() + priceAmnt_.text()"
    }

    private function onCloseClick(_arg1:MouseEvent):void {
        this.close();
    }

    private function onAddedToStage(_arg1:Event):void {
        this.closeButton_.x = (stage.stageWidth / 2);
        this.closeButton_.y = Y_POSITION;
        if (Capabilities.playerType == "Desktop") {
            Parameters.data_.fullscreenMode = (stage.displayState == "fullScreenInteractive");
            Parameters.save();
        }
        stage.addEventListener(KeyboardEvent.KEY_DOWN, this.onKeyDown, false, 1);
        stage.addEventListener(KeyboardEvent.KEY_UP, this.onKeyUp, false, 1);
    }

    private function onRemovedFromStage(_arg1:Event):void {
        stage.removeEventListener(KeyboardEvent.KEY_DOWN, this.onKeyDown, false);
        stage.removeEventListener(KeyboardEvent.KEY_UP, this.onKeyUp, false);
    }

    private function onKeyDown(_arg1:KeyboardEvent):void {
        if ((((Capabilities.playerType == "Desktop")) && ((_arg1.keyCode == KeyCodes.ESCAPE)))) {
            Parameters.data_.fullscreenMode = false;
            Parameters.save();
            this.refresh();
        }
        if (_arg1.keyCode == Parameters.data_.options) {
            this.close();
        }
        _arg1.stopImmediatePropagation();
    }

    private function close():void {
        stage.focus = null;
        parent.removeChild(this);
    }

    private function onKeyUp(_arg1:KeyboardEvent):void {
        _arg1.stopImmediatePropagation();
    }

    private function refresh():void {
        var _local2:BaseOption;
        var _local1:int;
        while (_local1 < this.settings_.length) {
            _local2 = (this.settings_[_local1] as BaseOption);
            if (_local2 != null) {
                _local2.refresh();
            }
            _local1++;
        }
    }
}
}
