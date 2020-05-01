// Decompiled by AS3 Sorcerer 6.30
// www.as3sorcerer.com

//kabam.rotmg.memMarket.signals.MemMarketAddSignal

package kabam.rotmg.memMarket.signals
{
    import org.osflash.signals.Signal;
    import kabam.rotmg.messaging.impl.incoming.market.MarketAddResult;

    public class MemMarketAddSignal extends Signal 
    {

        public static var instance:MemMarketAddSignal;

        public function MemMarketAddSignal()
        {
            super(MarketAddResult);
            instance = this;
        }

    }
}//package kabam.rotmg.memMarket.signals

