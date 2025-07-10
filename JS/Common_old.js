   function msieversion()
   {
      var ua = window.navigator.userAgent
      var msie = ua.indexOf ( "MSIE " )

      if ( msie > 0 )      // If Internet Explorer, return version number
         return parseInt (ua.substring (msie+5, ua.indexOf (".", msie )))
      else                 // If another browser, return 0
         return 0

 }
 var Version = msieversion();
 var CSSName = "class";
 if (Version == 7) { CSSName = "className" };

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
