package kabam.rotmg.ui.view {
import com.company.assembleegameclient.objects.Player;
import com.company.assembleegameclient.ui.ExperienceBoostTimerPopup;
import com.company.assembleegameclient.ui.StatusBar;

import flash.display.Sprite;
import flash.events.Event;

import kabam.rotmg.text.model.TextKey;

public class StatMetersView extends Sprite {

    private var expBar_:StatusBar;
    private var fameBar_:StatusBar;
    private var hpBar_:StatusBar;
    private var mpBar_:StatusBar;
    private var areTempXpListenersAdded:Boolean;
    private var curXPBoost:int;
    private var expTimer:ExperienceBoostTimerPopup;

    public function StatMetersView() {
        this.expBar_ = new StatusBar(176, 14, 5931045, 0x545454, TextKey.EXP_BAR_LEVEL, true);
        this.fameBar_ = new StatusBar(176, 14, 0xE25F00, 0x545454, TextKey.CURRENCY_FAME, true);
        this.hpBar_ = new StatusBar(176, 14, 14693428, 0x545454, TextKey.STATUS_BAR_HEALTH_POINTS, true);
        this.mpBar_ = new StatusBar(176, 14, 6325472, 0x545454, TextKey.STATUS_BAR_MANA_POINTS, true);
        this.expBar_.visible = true;
        this.fameBar_.visible = false;
        this.hpBar_.y = 24;
        this.mpBar_.y = 48;
        addChild(this.expBar_);
        addChild(this.fameBar_);
        addChild(this.hpBar_);
        addChild(this.mpBar_);
    }

    public function update(player:Player):void {
        this.expBar_.setLabelText(TextKey.EXP_BAR_LEVEL, {"level": player.level_});
        if (this.expTimer) {
            this.expTimer.update(player.xpTimer);
        }
        if (!this.expBar_.visible) {
            this.expBar_.visible = true;
            this.fameBar_.visible = false;
            this.expBar_.y = 0;
        }
        this.expBar_.draw(player.exp_, player.nextLevelExp_, 0);
        if (this.curXPBoost != player.xpBoost_) {
            this.curXPBoost = player.xpBoost_;
            if (this.curXPBoost) {
                this.expBar_.showMultiplierText();
            } else {
                this.expBar_.hideMultiplierText();
            }
        }
        if (player.xpTimer) {
            if (!this.areTempXpListenersAdded) {
                this.expBar_.addEventListener("MULTIPLIER_OVER", this.onExpBarOver);
                this.expBar_.addEventListener("MULTIPLIER_OUT", this.onExpBarOut);
                this.areTempXpListenersAdded = true;
            }
        }
        else {
            if (this.areTempXpListenersAdded) {
                this.expBar_.removeEventListener("MULTIPLIER_OVER", this.onExpBarOver);
                this.expBar_.removeEventListener("MULTIPLIER_OUT", this.onExpBarOut);
                this.areTempXpListenersAdded = false;
            }
            if (((this.expTimer) && (this.expTimer.parent))) {
                removeChild(this.expTimer);
                this.expTimer = null;
            }
        }
        if (player.level_ < 20 && !this.expBar_.visible){
            this.fameBar_.visible = false;
            this.expBar_.visible = true;
            this.expBar_.y = 0;
            this.hpBar_.y = 24;
            this.mpBar_.y = 48;
        }
        else if ((player.level_ >= 20 && player.level_ != 100) && (!this.fameBar_.visible || !this.expBar_.visible)) {
            this.fameBar_.visible = true;
            this.expBar_.visible = true;
            this.fameBar_.y = 0;
            this.expBar_.y = 16;
            this.hpBar_.y = 32;
            this.mpBar_.y = 48;
        }
        else if (player.level_ == 100 && this.expBar_.visible) {
            this.fameBar_.visible = true;
            this.expBar_.visible = false;
            this.fameBar_.y = 0;
            this.hpBar_.y = 24;
            this.mpBar_.y = 48;
        }
        this.fameBar_.draw(player.currFame_, player.nextClassQuestFame_, 0);

        this.hpBar_.draw(player.hp_, player.maxHP_, player.maxHPBoost_, player.maxHPMax_);
        this.mpBar_.draw(player.mp_, player.maxMP_, player.maxMPBoost_, player.maxMPMax_);
    }

    private function onExpBarOver(_arg1:Event):void {
        addChild((this.expTimer = new ExperienceBoostTimerPopup()));
    }

    private function onExpBarOut(_arg1:Event):void {
        if (((this.expTimer) && (this.expTimer.parent))) {
            removeChild(this.expTimer);
            this.expTimer = null;
        }
    }


}
}
