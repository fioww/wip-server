
//kabam.rotmg.memMarket.utils.DialogUtils

package kabam.rotmg.memMarket.utils
{
    import com.company.assembleegameclient.ui.dialogs.Dialog;
    import com.company.assembleegameclient.game.GameSprite;
    import flash.events.Event;

    public class DialogUtils 
    {


        public static function makeSimpleDialog(gameSprite:GameSprite, title:String, description:String):void
        {
            var dialog:Dialog = new Dialog(description, title, "Close", null, null, true);
            dialog.addEventListener(Dialog.LEFT_BUTTON, onDialogClose);
            gameSprite.mui_.layers.overlay.addChild(dialog);
        }

        public static function makeCallbackDialog(gameSprite:GameSprite, title:String, description:String, textOne:String, textTwo:String, callback:Function):void
        {
            var dialog:Dialog = new Dialog(description, title, textOne, textTwo, null, true);
            dialog.addEventListener(Dialog.LEFT_BUTTON, callback);
            dialog.addEventListener(Dialog.LEFT_BUTTON, onDialogClose);
            dialog.addEventListener(Dialog.RIGHT_BUTTON, onDialogClose);
            gameSprite.mui_.layers.overlay.addChild(dialog);
        }

        private static function onDialogClose(event:Event):void
        {
            var dialog:Dialog = (event.currentTarget as Dialog);
            dialog.removeEventListener(Dialog.LEFT_BUTTON, onDialogClose);
            dialog.removeEventListener(Dialog.RIGHT_BUTTON, onDialogClose);
            dialog.parent.removeChild(dialog);
        }


    }
}//package kabam.rotmg.memMarket.utils

