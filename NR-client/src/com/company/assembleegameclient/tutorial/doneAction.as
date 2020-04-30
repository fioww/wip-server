package com.company.assembleegameclient.tutorial {
import com.company.assembleegameclient.game.GameSprite;

public function doneAction(_arg1:GameSprite, _arg2:String):void {
    if (_arg1.tutorial_ == null) {
        return;
    }
    _arg1.tutorial_.doneAction(_arg2);
}

}
