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
function SetScrollPos(ctl) {
    setCookie(ctl.id, ctl.scrollTop, 1);
}

function specialcharecter(el) {
    var iChars = "!`@#$%^*()+=[]\\\';/{}|\":<>?~_"; //Removed "& ,- ." from special character list
    var data = el.value;
    for (var i = 0; i < data.length; i++) {
        if (iChars.indexOf(data.charAt(i)) != -1) {
            alert("Your string has special characters. \nThese are not allowed. Please enter this valid characters (a-z, A-Z, 0-9, full stop(.), Comma(,), hyphen(-) and ampersand(&)).");
            el.value = el.value.substring(0, el.value.length - 1);
            return false;
        }
    }
}

function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

function getFileSize(objfile) {
    var file = objfile.files[0];
    
    //alert(file.size);
    var size = file.size;
    var maxsize = (size / 1000) / 1000;
    //alert(maxsize);
     if (maxsize > 5) {
        alert("File size should not be greater than 5 mb.")
        objfile.value = "";
        return true;
    }
    else {
        return false;
    }
}

function getFileSizeKB(objfile) {
    var file = objfile.files[0];
    var size = file.size;
    var maxsize = (size / 1024);
    //alert(maxsize);
    if (maxsize > 300) {
        alert("Uploaded file size can not be greater than 300 kb.")
        objfile.value = "";
        return true;
    }

}


function getFileSize100KB(objfile) {
    var file = objfile.files[0];
    var size = file.size;
    var maxsize = (size / 1024);
    //alert(maxsize);
    if (maxsize > 100) {
        alert("Uploaded file size can not be greater than 100 kb.")
        objfile.value = "";
        return true;
    }

}

function getFileSize2mb(objfile) {
    var file = objfile.files[0];

    //alert(file.size);
    var size = file.size;
    var maxsize = (size / 1000) / 1000;
    //alert(maxsize);
    if (maxsize > 2) {
        alert("File size should not be greater than 2 mb.")
        objfile.value = "";
        return true;
    }
    else {
        return false;
    }
}

function checkInt(el) {
    //for validating only numeric values
    var ex = /^\d+$/;
    if (ex.test(el.value) == false) {
        el.value = el.value.substring(0, el.value.length - 1);
    }
}




