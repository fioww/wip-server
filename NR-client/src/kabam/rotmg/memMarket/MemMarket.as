
//kabam.rotmg.memMarket.MemMarket

package kabam.rotmg.memMarket
{
    import flash.display.Sprite;
    import com.company.assembleegameclient.game.GameSprite;
    import com.company.ui.SimpleText;
    import com.company.assembleegameclient.screens.TitleMenuOption;
    import com.company.assembleegameclient.ui.options.OptionsTabTitle;
    import kabam.rotmg.memMarket.tabs.MemMarketTab;
    import flash.text.TextFieldAutoSize;
    import flash.filters.DropShadowFilter;
    import com.company.rotmg.graphics.ScreenGraphic;
    import flash.events.MouseEvent;
    import kabam.rotmg.memMarket.tabs.MemMarketSellTab;
    import kabam.rotmg.memMarket.tabs.MemMarketBuyTab;
    import flash.events.Event;

    public class MemMarket extends Sprite 
    {

        private static const BUY:String = "Buy";
        private static const SELL:String = "Sell";
        private static const TABS:Vector.<String> = new <String>[BUY, SELL];

        private var gameSprite_:GameSprite;
        private var titleText_:SimpleText;
        private var closeButton_:TitleMenuOption;
        private var tabs_:Vector.<OptionsTabTitle>;
        private var content_:Vector.<MemMarketTab>;
        private var selectedTab_:OptionsTabTitle;
        private var creatorText_:SimpleText;

        public function MemMarket(gameSprite:GameSprite)
        {
            var xOffset:int;
            var tab:OptionsTabTitle;
            super();
            this.gameSprite_ = gameSprite;
            graphics.clear();
            graphics.beginFill(0x2B2B2B, 0.8);
            graphics.drawRect(0, 0, 800, 600);
            graphics.endFill();
            graphics.lineStyle(1, 0x5E5E5E);
            graphics.moveTo(0, 100);
            graphics.lineTo(800, 100);
            graphics.lineStyle();
            this.titleText_ = new SimpleText(36, 0xFFFFFF, false, 800, 0);
            this.titleText_.setBold(true);
            this.titleText_.htmlText = '<p align="center">Market</p>';
            this.titleText_.autoSize = TextFieldAutoSize.CENTER;
            this.titleText_.filters = [new DropShadowFilter(0, 0, 0)];
            this.titleText_.updateMetrics();
            this.titleText_.x = ((800 / 2) - (this.titleText_.width / 2));
            this.titleText_.y = 8;
            addChild(this.titleText_);
            addChild(new ScreenGraphic());
            this.closeButton_ = new TitleMenuOption("Close", 36, false);
            this.closeButton_.x = ((710 / 2) - (this.closeButton_.width / 2));
            this.closeButton_.y = 530;
            this.closeButton_.addEventListener(MouseEvent.CLICK, this.onClose);
            addChild(this.closeButton_);
            this.tabs_ = new Vector.<OptionsTabTitle>();
            xOffset = 14;
            var i:int;
            while (i < TABS.length)
            {
                tab = new OptionsTabTitle(TABS[i]);
                tab.x = xOffset;
                tab.y = 78;
                tab.addEventListener(MouseEvent.CLICK, this.onTab);
                addChild(tab);
                this.tabs_.push(tab);
                xOffset = (xOffset + 108);
                i++;
            }
            this.content_ = new Vector.<MemMarketTab>();
            this.setTab(this.tabs_[0]);
            this.creatorText_ = new SimpleText(16, 0xFFFFFF, false, 200);
            this.creatorText_.setBold(true);
            this.creatorText_.text = "";
            this.creatorText_.y = 582;
            addChild(this.creatorText_);
        }

        private function onTab(event:MouseEvent):void
        {
            var tab:OptionsTabTitle = (event.currentTarget as OptionsTabTitle);
            this.setTab(tab);
        }

        private function setTab(tab:OptionsTabTitle):void
        {
            if (tab == this.selectedTab_)
            {
                return;
            }
            if (this.selectedTab_ != null)
            {
                this.selectedTab_.setSelected(false);
            }
            this.selectedTab_ = tab;
            this.selectedTab_.setSelected(true);
            this.removeLastContent();
            switch (this.selectedTab_.text_)
            {
                case SELL:
                    this.addContent(new MemMarketSellTab(this.gameSprite_));
                    break;
                case BUY:
                    this.addContent(new MemMarketBuyTab(this.gameSprite_));
                    break;
            }
        }

        private function removeLastContent():void
        {
            var i:MemMarketTab;
            for each (i in this.content_)
            {
                i.dispose();
                removeChild(i);
            }
            this.content_.length = 0;
        }

        private function addContent(content:MemMarketTab):void
        {
            addChild(content);
            this.content_.push(content);
        }

        private function onClose(event:Event):void
        {
            var tab:OptionsTabTitle;
            var content:MemMarketTab;
            var i:int;
            this.gameSprite_.mui_.setEnablePlayerInput(true);
            this.gameSprite_ = null;
            this.titleText_ = null;
            this.closeButton_.removeEventListener(MouseEvent.CLICK, this.onClose);
            this.closeButton_ = null;
            for each (tab in this.tabs_)
            {
                tab.removeEventListener(MouseEvent.CLICK, this.onTab);
                tab = null;
            }
            this.tabs_.length = 0;
            this.tabs_ = null;
            for each (content in this.content_)
            {
                content.dispose();
                content = null;
            }
            this.content_.length = 0;
            this.content_ = null;
            this.selectedTab_ = null;
            this.creatorText_ = null;
            i = (numChildren - 1);
            while (i >= 0)
            {
                removeChildAt(i);
                i--;
            }
            stage.focus = null;
            parent.removeChild(this);
        }


    }
}//package kabam.rotmg.memMarket

