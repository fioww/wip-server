﻿//kabam.rotmg.memMarket.utils.ItemUtils

package kabam.rotmg.memMarket.utils
{
import com.company.assembleegameclient.objects.ObjectLibrary;

public class ItemUtils
    {

        public static function isBanned(itemType:int):Boolean
        {
            return ((ObjectLibrary.isSoulbound(itemType)));
        }

    }
}//package kabam.rotmg.memMarket.utils

