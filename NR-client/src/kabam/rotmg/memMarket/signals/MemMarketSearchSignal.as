//kabam.rotmg.memMarket.signals.MemMarketSearchSignal

package kabam.rotmg.memMarket.signals
{
    import org.osflash.signals.Signal;
    import kabam.rotmg.messaging.impl.incoming.market.MarketSearchResult;

    public class MemMarketSearchSignal extends Signal 
    {

        public static var instance:MemMarketSearchSignal;

        public function MemMarketSearchSignal()
        {
            super(MarketSearchResult);
            instance = this;
        }

    }
}//package kabam.rotmg.memMarket.signals

