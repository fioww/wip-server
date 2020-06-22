
//kabam.rotmg.memMarket.signals.MemMarketMyOffersSignal

package kabam.rotmg.memMarket.signals
{
    import org.osflash.signals.Signal;
    import kabam.rotmg.messaging.impl.incoming.market.MarketMyOffersResult;

    public class MemMarketMyOffersSignal extends Signal 
    {

        public static var instance:MemMarketMyOffersSignal;

        public function MemMarketMyOffersSignal()
        {
            super(MarketMyOffersResult);
            instance = this;
        }

    }
}//package kabam.rotmg.memMarket.signals

