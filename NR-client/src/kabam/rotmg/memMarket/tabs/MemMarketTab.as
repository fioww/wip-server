
//kabam.rotmg.memMarket.tabs.MemMarketTab

package kabam.rotmg.memMarket.tabs
{
    import flash.display.Sprite;
    import com.company.assembleegameclient.game.GameSprite;

    public class MemMarketTab extends Sprite 
    {

        public var gameSprite_:GameSprite;

        public function MemMarketTab(gameSprite:GameSprite)
        {
            this.gameSprite_ = gameSprite;
            graphics.clear();
            graphics.lineStyle(1, 0x5E5E5E);
            graphics.moveTo(265, 100);
            graphics.lineTo(265, 525);
            graphics.lineStyle();
        }

        public function dispose():void
        {
            this.gameSprite_ = null;
            var i:int = (numChildren - 1);
            while (i >= 0)
            {
                removeChildAt(i);
                i--;
            }
        }


    }
}//package kabam.rotmg.memMarket.tabs

