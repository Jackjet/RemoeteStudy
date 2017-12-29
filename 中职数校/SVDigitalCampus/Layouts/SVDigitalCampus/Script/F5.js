/**
 * Created by ÏþÖÒ on 2015/8/19.
 */

//½ûÓÃF5
$(document).ready(function() {
    $(document).bind("keydown",function(e){
        e=window.event||e;
        if(e.keyCode==116){
            e.keyCode = 0;
            var obj=document.getElementById("iframbox");
            if(obj==null)
                window.location.reload();
            else
                obj.src =obj.src;
            return false;
        }
    });
});




