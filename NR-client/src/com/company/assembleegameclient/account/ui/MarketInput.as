
//com.company.assembleegameclient.account.ui.MarketInput

package com.company.assembleegameclient.account.ui
{
    import flash.display.Sprite;
    import com.company.ui.SimpleText;
    import flash.filters.DropShadowFilter;
    import flash.display.LineScaleMode;
    import flash.display.CapsStyle;
    import flash.display.JointStyle;
    import flash.events.Event;

    public class MarketInput extends Sprite 
    {

        public static const HEIGHT:int = 88;

        public var nameText_:SimpleText;
        public var inputText_:SimpleText;
        public var errorText_:SimpleText;

        public function MarketInput(name:String, isPassword:Boolean, error:String)
        {
            this.nameText_ = new SimpleText(18, 0xB3B3B3, false, 0, 0);
            this.nameText_.setBold(true);
            this.nameText_.text = name;
            this.nameText_.updateMetrics();
            this.nameText_.filters = [new DropShadowFilter(0, 0, 0)];
            addChild(this.nameText_);
            this.inputText_ = new SimpleText(20, 0xB3B3B3, true, 238, 30);
            this.inputText_.y = 30;
            this.inputText_.x = 6;
            this.inputText_.border = false;
            this.inputText_.displayAsPassword = isPassword;
            this.inputText_.updateMetrics();
            addChild(this.inputText_);
            graphics.lineStyle(2, 0x454545, 1, false, LineScaleMode.NORMAL, CapsStyle.ROUND, JointStyle.ROUND);
            graphics.beginFill(0x333333, 1);
            graphics.drawRect(0, this.inputText_.y, 238, 30);
            graphics.endFill();
            graphics.lineStyle();
            this.inputText_.addEventListener(Event.CHANGE, this.onInputChange);
            this.errorText_ = new SimpleText(12, 16549442, false, 0, 0);
            this.errorText_.y = (this.inputText_.y + 32);
            this.errorText_.text = error;
            this.errorText_.updateMetrics();
            this.errorText_.filters = [new DropShadowFilter(0, 0, 0)];
            addChild(this.errorText_);
        }

        public function text():String
        {
            return (this.inputText_.text);
        }

        public function setError(error:String):void
        {
            this.errorText_.text = error;
            this.errorText_.updateMetrics();
        }

        public function onInputChange(event:Event):void
        {
            this.setError("");
        }


    }
}//package com.company.assembleegameclient.account.ui

