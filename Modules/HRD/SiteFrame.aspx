<%@ Page Language="C#" MasterPageFile="~/Modules/HRD/SiteMainPage.master" AutoEventWireup="true" CodeFile="SiteFrame.aspx.cs" Inherits="SiteFrame" Title="SHIPSOFT :: The Crew Management System" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<script type="text/javascript">
//Input the IDs of the IFRAMES you wish to dynamically resize to match its content height:
//Separate each ID with a comma. Examples: ["myframe1", "myframe2"] or ["myframe"] or [] for none:

var iframeids=["iHomePage","SearchFrame"]

//Should script hide iframe from browsers that don't support this script (non IE5+/NS6+ browsers. Recommended):
var iframehide="yes"

var getFFVersion=navigator.userAgent.substring(navigator.userAgent.indexOf("Firefox")).split("/")[1]
var FFextraHeight=parseFloat(getFFVersion)>=0.1? 16 : 0 //extra height in px to add to iframe in FireFox 1.0+ browsers

function resizeCaller() 
{
    var dyniframe=new Array()
    for (i=0; i<iframeids.length; i++){
        if (document.getElementById)
            resizeIframe(iframeids[i])

            //reveal iframe for lower end browsers? (see var above):
        if ((document.all || document.getElementById) && iframehide=="no"){
            var tempobj=document.all? document.all[iframeids[i]] : document.getElementById(iframeids[i])
            tempobj.style.display="block"
        }
    }
}

function resizeIframe(frameid)
{
    var currentfr=document.getElementById(frameid);
    if(currentfr.src=="SiteFrame.aspx")
    {
        document.getElementById("mainheading").style.display="block";
        OpenChildPage(1);      
		document.getElementById("divhome").style.display="none";
    }
    else
    {
        if(currentfr.src=="CrewCost/AdvanceSearch.aspx" ||currentfr.src=="CrewCost/SearchMap.aspx" ||currentfr.src.indexOf("BasicSearch.aspx")>0)
        {
            document.getElementById("mainheading").style.display="block";
            document.getElementById("divhome").style.display="none";
        }
        else
        {
            document.getElementById("divhome").style.display="block";
            document.getElementById("mainheading").style.display="none";
        }   
    }
       
    if (currentfr && !window.opera)
    {
        currentfr.style.display="block"
        if (currentfr.contentDocument && currentfr.contentDocument.body.offsetHeight) //ns6 syntax
            currentfr.height = currentfr.contentDocument.body.offsetHeight+FFextraHeight; 
            //currentfr.height = '920px';
        else if (currentfr.Document && currentfr.Document.body.scrollHeight) //ie5+ syntax
        {
            currentfr.height = currentfr.Document.body.scrollHeight;
             //currentfr.height = '920px';
        }

        if (currentfr.addEventListener)
        {
            currentfr.addEventListener("load", readjustIframe, false);
        }
        else if (currentfr.attachEvent)
        {
            currentfr.detachEvent("onload", readjustIframe) // Bug fix line
            currentfr.attachEvent("onload", readjustIframe)
        }
    }
}

function readjustIframe(loadevt) 
{
    var crossevt=(window.event)? event : loadevt
    var iframeroot=(crossevt.currentTarget)? crossevt.currentTarget : crossevt.srcElement
    if (iframeroot)
    resizeIframe(iframeroot.id);
}

function loadintoIframe(iframeid, url)
{
    if (document.getElementById)
    document.getElementById(iframeid).src=url
}

if (window.addEventListener)
    window.addEventListener("load", resizeCaller, false)
else if (window.attachEvent)
    window.attachEvent("onload", resizeCaller)
else
    window.onload=resizeCaller        

function OpenChildPage(switchValue)
{            
    switch(switchValue)
    {
        case 1:
           document.getElementById('SearchFrame').src='CrewCost/AdvanceSearch.aspx'; 
           break;

        case 2:
           document.getElementById('SearchFrame').src='CrewCost/GetCost.aspx'; 
           break;
           
        case 3:
           document.getElementById('SearchFrame').src='CrewCost/BasicSearch.aspx'; 
           break;

        case 4:
           document.getElementById('SearchFrame').src='CrewCost/GetBudget.aspx'; 
           break;

        case 5:
           document.getElementById('SearchFrame').src='CrewCost/GetAccountCode.aspx'; 
           break;
           
        case 6:
           document.getElementById('SearchFrame').src='CrewCost/AccountTransfer.aspx'; 
           break;
           
        case 7:
           document.getElementById('SearchFrame').src='CrewCost/CrewCost.aspx'; 
           break;
           
        case 8:
           document.getElementById('SearchFrame').src='CrewCost/.aspx'; 
           break;
           
        case 9:
           document.getElementById('SearchFrame').src='CrewCost/NewConstruction.aspx'; 
           break;
           
        case 10:
           document.getElementById('SearchFrame').src='CrewCost/Soldproperty.aspx'; 
           break;
    }
}
</script>
<table cellpadding="0" cellspacing="0" style="width: 99%; height:100%" border="0">
    <tr>
        <td>
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width:100%">
                        <div id="mainheading" style="display: none; width: 100%; vertical-align:top">
                            <table border="0" cellpadding="0" cellspacing="0" style="vertical-align: top">
                                <tr>
                                    <td valign="top" style="width: 100%; height: 100%;">
                                        <div id="div1" style="display: none; overflow: auto; height:800px;">
                                        <iframe id="SearchFrame" scrolling="no" marginwidth="0" marginheight="0" frameborder="0" style="overflow: visible; width: 100%; height:1000px; display: none; background-color: white">
                                        </iframe>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%;">
                        <div id="divhome" style="display: none; width: 100%; vertical-align:top">
                            <iframe id="iHomePage" src="HomePage.aspx" scrolling="no" marginwidth="0" marginheight="0" frameborder="0" style="overflow: visible; width: 100%; display: none;">
                            </iframe>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</asp:Content>
