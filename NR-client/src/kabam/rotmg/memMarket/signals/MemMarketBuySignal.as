
//kabam.rotmg.memMarket.signals.MemMarketBuySignal

package kabam.rotmg.memMarket.signals
{
    import org.osflash.signals.Signal;
    import kabam.rotmg.messaging.impl.incoming.market.MarketBuyResult;

    public class MemMarketBuySignal extends Signal 
    {

        public static var instance:MemMarketBuySignal;

        public function MemMarketBuySignal()
        {
            super(MarketBuyResult);
            instance = this;
        }

    }
}//package kabam.rotmg.memMarket.signals

