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
    if (pos > 0) 
    {
        if (document.getElementById(ctlid) != null)
        {
        document.getElementById(ctlid).scrollTop = pos;
        if (document.getElementById(ctlid).scrollTop != pos) {
            setPosByTimer(ctlid, pos);
            }
        }
    }
}
function SetLastFocusTimer(ctlid,pos) 
{
    document.getElementById(ctlid.id).scrollTop = pos;
}
function setPosByTimer(ctl,pos) {
    
    window.setTimeout("SetLastFocusTimer(" + ctl + "," + pos +  ")",0);  
}


function SetScrollPos(ctl) {
    setCookie(ctl.id, ctl.scrollTop, 1);
}

function OfficeReLogin(RootUrl)
{
    alert('Session expired. Please login again.');
    alert(window.parent);
    if(window.parent!=null)
    {   
        window.parent.location=RootUrl;
        if(window.opener!=null)
        { 
            window.close(); 
        }
    }
    else
    {
        window.location=RootUrl;
        if(window.opener!=null)
        { 
            window.close(); 
        }
    }
}

