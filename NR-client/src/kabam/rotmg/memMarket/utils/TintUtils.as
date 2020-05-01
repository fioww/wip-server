﻿// Decompiled by AS3 Sorcerer 6.30
// www.as3sorcerer.com

//kabam.rotmg.memMarket.utils.TintUtils

package kabam.rotmg.memMarket.utils
{
    import flash.geom.ColorTransform;
    import flash.display.Shape;

    public class TintUtils 
    {


        public static function addTint(shape:Shape, color:uint, alpha:Number):void
        {
            var transform:ColorTransform = shape.transform.colorTransform;
            transform.color = color;
            var rgb:Number = (alpha / (1 - (((transform.redMultiplier + transform.greenMultiplier) + transform.blueMultiplier) / 3)));
            transform.redOffset = (transform.redOffset * rgb);
            transform.greenOffset = (transform.greenOffset * rgb);
            transform.blueOffset = (transform.blueOffset * rgb);
            transform.redMultiplier = (transform.greenMultiplier = (transform.blueMultiplier = (1 - alpha)));
            shape.transform.colorTransform = transform;
        }

        public static function removeTint(shape:Shape):void
        {
            shape.transform.colorTransform = new ColorTransform();
        }


    }
}//package kabam.rotmg.memMarket.utils

