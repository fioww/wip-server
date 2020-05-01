// Decompiled by AS3 Sorcerer 6.30
// www.as3sorcerer.com

//kabam.rotmg.memMarket.signals.MemMarketRemoveSignal

package kabam.rotmg.memMarket.signals
{
    import org.osflash.signals.Signal;
    import kabam.rotmg.messaging.impl.incoming.market.MarketRemoveResult;

    public class MemMarketRemoveSignal extends Signal 
    {

        public static var instance:MemMarketRemoveSignal;

        public function MemMarketRemoveSignal()
        {
            super(MarketRemoveResult);
            instance = this;
        }

    }
}//package kabam.rotmg.memMarket.signals

