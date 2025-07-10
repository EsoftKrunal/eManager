function msieversion() {
    var ua = window.navigator.userAgent
    var msie = ua.indexOf("MSIE ")

    if (msie > 0)      // If Internet Explorer, return version number
        return parseInt(ua.substring(msie + 5, ua.indexOf(".", msie)))
    else                 // If another browser, return 0
        return 0

}
var Version = msieversion();
var CSSName = "class";
if (Version == 7) { CSSName = "className" };


function getCookie(c_name) {
    var i, x, y, ARRcookies = document.cookie.split(";");
    for (i = 0; i < ARRcookies.length; i++) {
        x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
        y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == c_name) {
            return unescape(y);
        }
    }
}
function setCookie(c_name, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
    document.cookie = c_name + "=" + c_value;
}
function SetLastFocus(ctlid) {
    pos = getCookie(ctlid);
    if (isNaN(pos))
    { pos = 0; }
    if (pos > 0) {
        document.getElementById(ctlid).scrollTop = pos;
    }
}
 
//---------------------------------

function ShowSelection(ctl) {
    $.each($(ctl).children(), function (i, O) { $(O).addClass("tdSel"); });
}
function HideSelection(ctl) {
    $.each($(ctl).children(), function (i, O) { $(O).removeClass("tdSel"); });
}
function RegisterForAutoScroll() {
    $.each($(".ScrollAutoReset"), function (i, o) {
        $(o).scroll(function () {
            SetScrollPos(this); 
            }); 
        });
}
function CallAfterRefresh(sender, args) {
    $.each($(".ScrollAutoReset"), function (i, o) {
            SetLastFocus($(this).attr('id'));
    });
        RegisterForAutoScroll();
        HighlightOnHover();
}
// Function will call after Update Panel Post back;
function SetOnLoad() {
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(CallAfterRefresh);
    try {
        if (Page_CallAfterRefresh != null)
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Page_CallAfterRefresh);
    } catch (err)
        { }
}
function SetScrollPos(ctl) {
    setCookie(ctl.id, ctl.scrollTop, 1);
}
function HighlightOnHover() {
    $(".newformat tbody tr").mouseover(function () { ShowSelection($(this)); });
    $(".newformat tbody tr").mouseout(function () { HideSelection($(this)); });
}
$(document).ready(function () {
    CallAfterRefresh(null, null);
    RegisterForAutoScroll();
    $(window).load(function () { SetOnLoad(); });
    HighlightOnHover();
});
 