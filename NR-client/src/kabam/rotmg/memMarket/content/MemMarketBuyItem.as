
//kabam.rotmg.memMarket.content.MemMarketBuyItem

package kabam.rotmg.memMarket.content
{
    import com.company.assembleegameclient.ui.TextButton;
    import com.company.ui.SimpleText;
    import flash.display.Bitmap;
    import flash.text.TextFieldAutoSize;
    import kabam.rotmg.memMarket.utils.IconUtils;
    import com.company.assembleegameclient.game.GameSprite;
    import kabam.rotmg.messaging.impl.data.MarketData;
    import kabam.rotmg.memMarket.utils.DialogUtils;
    import flash.events.MouseEvent;
    import flash.events.Event;
    import com.company.assembleegameclient.util.Currency;

    public class MemMarketBuyItem extends MemMarketItem 
    {

        private var buyButton_:TextButton;
        private var priceText_:SimpleText;
        private var timeText_:SimpleText;
        private var currency_:Bitmap;

        public function MemMarketBuyItem(gameSprite:GameSprite, data:MarketData)
        {
            super(gameSprite, OFFER_WIDTH, OFFER_HEIGHT, 80, data.itemType_, data);
            this.icon_.x = 22;
            this.icon_.y = -8;
            this.buyButton_ = new TextButton(10, "Buy", 96);
            this.buyButton_.x = 2;
            this.buyButton_.y = 62;
            this.updateButton();
            addChild(this.buyButton_);
            this.priceText_ = new SimpleText(10, 0xFFFFFF, false, width, 0);
            this.priceText_.setBold(true);
            this.priceText_.htmlText = (('<p align="center">' + this.data_.price_) + "</p>");
            this.priceText_.wordWrap = true;
            this.priceText_.multiline = true;
            this.priceText_.autoSize = TextFieldAutoSize.CENTER;
            this.priceText_.y = 49;
            addChild(this.priceText_);
            var unix:Number = (this.data_.timeLeft_ * 1000);
            var later:Date = new Date(unix);
            var now:Date = new Date();
            var ms:Number = Math.floor((later.time - now.time));
            var hours:Number = (ms / 3600000);
            this.timeText_ = new SimpleText(10, 0xFFFFFF, false, width, 0);
            this.timeText_.setBold(true);
            this.timeText_.htmlText = (('<p align="center">' + hours.toFixed(1)) + "h</p>");
            this.timeText_.wordWrap = true;
            this.timeText_.multiline = true;
            this.timeText_.autoSize = TextFieldAutoSize.CENTER;
            this.timeText_.y = 39;
            addChild(this.timeText_);
            this.currency_ = this.data_.currency_ == Currency.FAME ? new Bitmap(IconUtils.getFameIcon(24)) : new Bitmap(IconUtils.getCoinIcon(24));
            this.currency_.x = 76;
            this.currency_.y = 38;
            addChild(this.currency_);
        }

        private function onBuyClick(event:MouseEvent):void
        {
            DialogUtils.makeCallbackDialog(this.gameSprite_, "Verification", "Are you sure you want to buy this item?", "Yes", "No", this.onVerified);
        }

        private function onVerified(event:Event):void
        {
            this.gameSprite_.gsc_.marketBuy(this.id_);
        }

        public function updateButton():void
        {
            var currencyAmount:int = ((this.data_.currency_ == Currency.FAME) ? this.gameSprite_.map.player_.fame_ : this.gameSprite_.map.player_.credits_);
            if (currencyAmount >= this.data_.price_)
            {
                this.buyButton_.addEventListener(MouseEvent.CLICK, this.onBuyClick);
                this.buyButton_.setEnabled(true);
            }
            else
            {
                this.buyButton_.removeEventListener(MouseEvent.CLICK, this.onBuyClick);
                this.buyButton_.setEnabled(false);
            }
        }

        override public function dispose():void
        {
            this.buyButton_.removeEventListener(MouseEvent.CLICK, this.onBuyClick);
            this.buyButton_ = null;
            this.priceText_ = null;
            this.timeText_ = null;
            this.currency_ = null;
            super.dispose();
        }


    }
}//package kabam.rotmg.memMarket.content

