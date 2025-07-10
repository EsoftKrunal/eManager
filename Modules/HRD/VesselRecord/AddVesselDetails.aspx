<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddVesselDetails.aspx.cs" Inherits="AddVesselDetails" Culture="auto" UICulture="auto" %>
<%@ Register Src="~/Modules/HRD/VesselRecord/VesselBudget.ascx" TagName="VesselBudget" TagPrefix="uc2" %>
<%@ Register Src="~/Modules/HRD/VesselRecord/CrewMatrix.ascx" TagName="CrewMatrix" TagPrefix="uc5" %>
<%@ Register Src="~/Modules/HRD/VesselRecord/VesselDocuments.ascx" TagName="VesselDocuments" TagPrefix="uc1" %>
<%@ Register Src="VesselMiningScale.ascx" TagName="VesselMiningScale" TagPrefix="uc2" %>

<%@ Register Src="~/Modules/HRD/VesselRecord/AddVesselMiningScale.ascx"TagName="AddVesselMiningScale" TagPrefix="Addd" %>

<%@ Register Src="~/Modules/HRD/VesselRecord/CrewMatrixUpload.ascx" TagName="CrewSireUpload" TagPrefix="ucupld" %>
<%@ Register Src="~/Modules/HRD/VesselRecord/VesselDetailsGeneral.ascx" TagName="VesselDetailsGeneral" TagPrefix="uc3" %>
<%@ Register Src="~/Modules/HRD/VesselRecord/VesselDetailsOther.ascx" TagName="VesselDetailsOther" TagPrefix="uc4" %>
<%@ Register TagName="menu" Src="~/Modules/HRD/UserControls/VesselMenu.ascx" TagPrefix="mtm"  %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Crew Member Details</title>
     <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"> 
    <link href="../styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/Gridstyle.css" />
    
    <script type="text/javascript" src="../Scripts/jquery.js"></script>
    <script type="text/javascript" src="../Scripts/thickbox.js"></script>
    <link rel="stylesheet" href="../Styles/thickbox.css" type="text/css" media="screen" />
  
    <script type="text/javascript">
        function refreshParent() {            
        window.opener.location.reload();
    }
</script>
    <script language="javascript" type="text/javascript">
function CallPrint(strid)
{
 var prtContent = document.getElementById(strid);
 var WinPrint = window.open('','','letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
 WinPrint.document.write(prtContent.innerHTML);
 WinPrint.document.close();
 WinPrint.focus();
 WinPrint.print();
 WinPrint.close();
 }
function Show_Image_Large(obj)
{
window.open(obj.src,"","resizable=1,toolbar=0,scrollbars=1,status=0"); 
}
function Show_Image_Large1(path)
{
window.open(path,"","resizable=1,toolbar=0,scrollbars=1,status=0"); 
}
    </script>
    <script type="text/javascript">
// Free for any type of use so long as original notice remains unchanged.
// Report errors to feedback@ashishware.com
//Copyrights 2006, Ashish Patil , ashishware.com
//////////////////////////////////////////////////////////////////////////

function ToolTip(id,isAnimated,aniSpeed)
{ var isInit = -1;
  var div,divWidth,divHeight;
  var xincr=10,yincr=10;
  var animateToolTip =false;
  var html;
  
  function Init(id)
  {
   div = document.getElementById(id);
   if(div==null) return;
   
   if((div.style.width=="" || div.style.height==""))
   {alert("Both width and height must be set");
   return;}
   
   divWidth = parseInt(div.style.width);
   divHeight= parseInt(div.style.height);
   if(div.style.overflow!="hidden")div.style.overflow="hidden";
   if(div.style.display!="none")div.style.display="none";
   if(div.style.position!="absolute")div.style.position="absolute";
   
   if(isAnimated && aniSpeed>0)
   {xincr = parseInt(divWidth/aniSpeed);
    yincr = parseInt(divHeight/aniSpeed);
    animateToolTip = true;
    }
        
   isInit++; 
   
  }
  
    
  this.Show =  function(e,strHTML)
  {
    if(isInit<0) return;
    
    var newPosx,newPosy,height,width;
    if(typeof( document.documentElement.clientWidth ) == 'number' ){
    width = document.body.clientWidth;
    height = document.body.clientHeight;}
    else
    {
    width = parseInt(window.innerWidth);
    height = parseInt(window.innerHeight);
    
    }
    var curPosx = (e.x)?parseInt(e.x):parseInt(e.clientX);
    var curPosy = (e.y)?parseInt(e.y):parseInt(e.clientY);
    
    if(strHTML!=null)
    {html = strHTML;
     div.innerHTML=html;}
    
    if((curPosx+divWidth+10)< width)
    newPosx= curPosx+10;
    else
    newPosx = curPosx-divWidth;

    if((curPosy+divHeight)< height)
    newPosy= curPosy;
    else
    newPosy = curPosy-divHeight-10;

   if(window.pageYOffset)
   { newPosy= newPosy+ window.pageYOffset;
     newPosx = newPosx + window.pageXOffset;}
   else
   { newPosy= newPosy+ document.body.scrollTop;
     newPosx = newPosx + document.body.scrollLeft;}

    div.style.display='block';
    //debugger;
    //alert(document.body.scrollTop);
    div.style.top= newPosy + "px";
    div.style.left= newPosx+ "px";

    div.focus();
    if(animateToolTip){
    div.style.height= "0px";
    div.style.width= "0px";
    ToolTip.animate(div.id,divHeight,divWidth);}
      
    
    }

    

   this.Hide= function(e)
    {div.style.display='none';
    if(!animateToolTip)return;
    div.style.height= "0px";
    div.style.width= "0px";}
    
   this.SetHTML = function(strHTML)
   {html = strHTML;
    div.innerHTML=html;} 
    
    ToolTip.animate = function(a,iHeight,iWidth)
  { a = document.getElementById(a);
         
   var i = parseInt(a.style.width)+xincr ;
   var j = parseInt(a.style.height)+yincr;  
   
   if(i <= iWidth)
   {a.style.width = i+"px";}
   else
   {a.style.width = iWidth+"px";}
   
   if(j <= iHeight)
   {a.style.height = j+"px";}
   else
   {a.style.height = iHeight+"px";}
   
   if(!((i > iWidth) && (j > iHeight)))      
   setTimeout( "ToolTip.animate('"+a.id+"',"+iHeight+","+iWidth+")",1);
    }
    
   Init(id);
}
//--------------------------
var t1=null;
var l1="Tooltip for line one";
var l2="Tooltip for line two";
function init()
{
 t1 = new ToolTip("a",false);
}
</script>
</head>
<body style=" margin: 0 0 0 0;" onload="init()"  onunload="refreshParent();" >
<div id="a" style="background-color:ivory; border:solid 1px black;width:184px;height:84px;text-align: left; padding-left:3px; display:none "></div>
<form id="form1" runat="server">
    <div style="text-align: center">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <%--<td style="width:150px; text-align :left; vertical-align : top;">
            <mtm:menu runat="server" ID="menu2" />  
        </td>--%>
        <td style=" text-align :left; vertical-align : top;"  colspan="2">
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; width :100%">
            <tr>
                <td align="center" style="background-color:#4371a5; height: 23px; width: 100%;" class="text" >
                    <img runat="server" id="imgHelp" moduleid="4" style ="cursor:pointer;float :right; padding-right :5px;" src="../images/help.png" alt="Help ?"/> 
                    Manning Status [ <%=Session["VesselName"].ToString()%> ]
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr><td style="padding-right:10px; text-align:center; color:Red; width: 825px; height: 13px;"><asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td></tr>
                        <tr>
                            <td style="padding-right: 10px; padding-left: 10px; padding-bottom: 10px;text-align: left;">
                                <table cellpadding="0" cellspacing="0" width="100%" style="display:none;" >
                                    <tr>
                                        <td style="height: 38px; width:1003px;">
                                    <asp:Menu ID="Menu1" runat="server" OnMenuItemClick="Menu1_MenuItemClick" Orientation="Horizontal"
                                        StaticEnableDefaultPopOutImage="False" Width="357px" meta:resourcekey="Menu1Resource1">
                                        <Items>
                                    <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/particulars1_a.gif"  Text=" " Value="0" ></asp:MenuItem>
                                    <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/particulars2_d.gif" Text=" " Value="1" ></asp:MenuItem>
                                    <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/budgeting_d.gif" Text=" " Value="2" ></asp:MenuItem>
                                    <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/crewDocs_d.gif" Text=" " Value="3" ></asp:MenuItem>
                                    <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/crewMatrix_d.gif" Text=" " Value="4" ></asp:MenuItem>
                                    <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/manningScale_d.gif" Text=" " Value="5"></asp:MenuItem>
                                    <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/matrix_d.gif" Text=" " Value="6" ></asp:MenuItem>
                                    </Items>
                                    </asp:Menu>
                                            <asp:HiddenField ID="HiddenPK" runat="server" />
                                        </td>
                                        <td style=" height: 38px; text-align: center">
                                            <asp:ImageButton ID="imgbtn_Documents" Visible="false"  runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/btnVesselDetails2.gif"
                                                OnClick="imgbtn_Documents_Click" ToolTip="Crew Documents" /></td>
                                        <td style="height: 38px; text-align: center">
                                            <asp:ImageButton ID="imgbtn_Search" runat="server" Visible="false"  CausesValidation="False" ImageUrl="~/Modules/HRD/Images/btnSearch.gif"
                                                OnClick="imgbtn_Search_Click" ToolTip="Search" />
                                                <asp:Button runat="server" ID="btnback" PostBackUrl="~/crewrecord/crewsearch.aspx" Text="Back" Width="80px"  CssClass="btn" CausesValidation="false"  /> 
                                                </td>
                                        <td style="height: 38px">
                                          </td>
                                    </tr>
                                </table>
                                     <div id="divPrint">
                                    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                        <asp:View ID="Tab1" runat="server">
                                            <uc3:VesselDetailsGeneral ID="VesselDetailsGeneral1" runat="server" />
                                        </asp:View>
                                        <asp:View ID="Tab2" runat="server">
                                            <uc4:VesselDetailsOther ID="VesselDetailsOther1" runat="server" />
                                        </asp:View>
                                        <asp:View ID="Tab3" runat="server">
                                            <uc2:VesselBudget ID="VesselBudget1" runat="server" />
                                        </asp:View>
                                        <asp:View ID="Tab4" runat="server">
                                            <uc1:VesselDocuments ID="VesselDocuments1" runat="server" />   
                                        </asp:View>
                                         <asp:View ID="Tab5" runat="server">
                                             <uc5:CrewMatrix ID="CrewMatrixl1" runat="server" />
                                        </asp:View>
                                         <asp:View ID="Tab6" runat="server">
                                             <%--<uc2:VesselMiningScale id="VesselMiningScale1" runat="server"></uc2:VesselMiningScale>--%>
                                             <Addd:AddVesselMiningScale ID="AddVesselMiningScale1" runat="server" />
                                             
                                        </asp:View>
                                         <asp:View ID="View7" runat="server">
                                             <ucupld:CrewSireUpload id="VesselMiningScale2" runat="server"></ucupld:CrewSireUpload>
                                        </asp:View>
        </asp:MultiView> </div></td></tr></table>
                </td>
            </tr>
        </table>
        </td>
        </tr>
        </table>
        </div>

    
    </form>
    
</body>
</html>
                                        
