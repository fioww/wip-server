package kabam.rotmg.ui.signals {
import kabam.rotmg.ui.model.Key;

import org.osflash.signals.Signal;

public class ShowLegendaryNotificationSignal extends Signal {

    public static var instance:ShowLegendaryNotificationSignal;

    public function ShowLegendaryNotificationSignal() {
        instance = this;
    }

}
}
