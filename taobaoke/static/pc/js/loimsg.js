function loiMsg(msg,timeout,callback) {
    if(typeof(timeout)=="undefined") {
        timeout = 3000;
    }
    var obj = $(".firetips_layer_wrap");
    if(obj.length>0) {
        $(".firetips").html(msg)
        $(".firetips_layer_wrap").fadeIn()
    } else {
        $("body").append('<style>.firetips_layer_wrap {position: fixed; width: 100%;top: 46%; left: 0; z-index: 10000;font-size: 16px;text-align: center;font-size: 16px;}.firetips_layer {display: inline-block;height: 68px;line-height: 68px;background: #f2dede;-webkit-box-shadow: 0 5px 5px rgba(0, 0, 0, .15);box-shadow: 0 5px 5px rgba(0, 0, 0, .15);border: 1px solid #eed3d7;padding: 0 28px 0 67px;position: relative;color: #b94a48;}.firetips_layer .hits { position: absolute;width: 30px;height: 28px;left: 23px;top: 20px;background-repeat: no-repeat;background-position: -113px -103px;background-size: 351px 332px; background-image: url("http://pic.vronline.com/open/images/personal.png");}</style><div class="firetips_layer_wrap"><span class="firetips_layer" style="z-index: 10000;"><span class="hits"></span><span class="firetips">'+msg+'</span></span></div>');
    }
    setTimeout(function(){
        $(".firetips_layer_wrap").fadeOut();
        if(typeof(callback)=="function") {
            callback()
        }
    }, timeout);
}