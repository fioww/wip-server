package com.company.assembleegameclient.ui {
import kabam.rotmg.text.view.stringBuilder.LineBuilder;
import kabam.rotmg.text.view.stringBuilder.StaticStringBuilder;

import org.osflash.signals.Signal;

public class DeprecatedTextButton extends TextButtonBase {

    public const textChanged:Signal = new Signal();

    public function DeprecatedTextButton(text:int, key:String, size:int = 0, staticStringBuilder:Boolean = false) {
        super(size);
        addText(text);
        if (staticStringBuilder) {
            text_.setStringBuilder(new StaticStringBuilder(key));
        }
        else {
            text_.setStringBuilder(new LineBuilder().setParams(key));
        }
        text_.textChanged.addOnce(this.onTextChanged);
    }

    protected function onTextChanged():void {
        initText();
        this.textChanged.dispatch();
    }


}
}
