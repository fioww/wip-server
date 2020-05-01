package kabam.rotmg.text.view.stringBuilder {
import kabam.rotmg.language.model.StringMap;

public class StaticStringBuilder implements StringBuilder {

    private var string:String;
    private var prefix:String;
    private var postfix:String;

    public function StaticStringBuilder(string:String = "") {
        this.string = string;
        this.prefix = "";
        this.postfix = "";
    }

    public function setString(string:String):StaticStringBuilder {
        this.string = string;
        return (this);
    }

    public function setPrefix(prefix:String):StaticStringBuilder {
        this.prefix = prefix;
        return (this);
    }

    public function setPostfix(postfix:String):StaticStringBuilder {
        this.postfix = postfix;
        return (this);
    }

    public function setStringMap(stringMap:StringMap):void {
    }

    public function getString():String {
        return (((this.prefix + this.string) + this.postfix));
    }


}
}
