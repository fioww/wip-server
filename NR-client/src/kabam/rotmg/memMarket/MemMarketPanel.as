
//kabam.rotmg.memMarket.MemMarketPanel

package kabam.rotmg.memMarket
{
    import com.company.assembleegameclient.ui.panels.ButtonPanel;
    import com.company.assembleegameclient.game.GameSprite;
    import flash.events.MouseEvent;

    public class MemMarketPanel extends ButtonPanel 
    {

        public function MemMarketPanel(gameSprite:GameSprite)
        {
            super(gameSprite, "Market", "Open");
        }

        override protected function onButtonClick(event:MouseEvent):void
        {
            if (this.gs_.model.isAdmin() && this.gs_.model.getRank() < 100)
            {

                return;
            }
            this.gs_.mui_.setEnablePlayerInput(false);
            this.gs_.addChild(new MemMarket(this.gs_.mui_.gs_));
        }


    }
}//package kabam.rotmg.memMarket

