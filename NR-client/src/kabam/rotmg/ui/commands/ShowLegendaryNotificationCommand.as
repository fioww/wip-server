package kabam.rotmg.ui.commands {
import com.gskinner.motion.GTween;
import flash.utils.setTimeout;
import flash.display.DisplayObjectContainer;

import kabam.rotmg.ui.view.Legendary_Notification;

import mx.core.BitmapAsset;

public class ShowLegendaryNotificationCommand {

    private static var legendaryNotification:Class = Legendary_Notification;
    private var view:BitmapAsset;

    [Inject]
    public var contextView:DisplayObjectContainer;


    public function execute():void {
        view = new legendaryNotification();
        view.x = 124;
        view.y = 15;
        this.contextView.addChild(view);
        view.alpha = 0;
        new GTween(view, 0.5, {"alpha": 1});
        setTimeout(function ():void {
            new GTween(view, 0.5, {"alpha": 0});
        }, 1500);
        setTimeout(this.remove, 2000);
    }

    private function remove():void {
        this.contextView.removeChild(view);
        view = null;
    }

}
}
