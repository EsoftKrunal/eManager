<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VesselDocuments.aspx.cs" Inherits="VesselDocumentsPage" ValidateRequest="false" EnableEventValidation="false"  Culture="auto" UICulture="auto" %>
<%@ Register TagName="menu" Src="~/Modules/HRD/UserControls/VesselMenu.ascx" TagPrefix="mtm"  %>
<%@ Register Src="Arrival.ascx" TagName="Arrival" TagPrefix="uc1" %>
<%@ Register Src="SundayNoonReport.ascx" TagName="SundayNoon" TagPrefix="uc4" %>
<%@ Register Src="dailynoonreport.ascx" TagName="DailyNoon" TagPrefix="uc3" %>
<%@ Register Src="DepartureReport.ascx" TagName="Departure" TagPrefix="uc6" %>
<%@ Register Src="CrewMatrix.ascx" TagName="CrewMatrix" TagPrefix="uc5" %>
<%@ Register Src="VesselMiningScale.ascx" TagName="VesselMiningScale" TagPrefix="uc2" %>
<%@ Register src="VesselCertificates.ascx" tagname="VesselCertificates" tagprefix="uc7" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" tagprefix="ajaxToolkit"%>
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
    <script language="javascript" type="text/javascript">
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
    
</head>
<body style=" margin: 0 0 0 0;" onload="init()" >
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    <Services>
        <asp:ServiceReference Path="WebService.asmx" />
    </Services>
</asp:ScriptManager>

<div id="a" style="background-color:ivory; border:solid 1px black;width:300px;height:150px;text-align: left; padding-left:3px; display:none ">
</div>
    <div style="text-align: center"  >
     <table style="width :100%;" cellpadding="0" cellspacing="0" >
        <tr>
        <td style="width:150px; text-align :left; vertical-align : top;">
            <mtm:menu runat="server" ID="menu2" />  
        </td>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="width:100%;border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center">
            <tr>
                <td align="center" style=" width: 100%;" class="text headerband" >
                    Vessel Master</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr><td style="width: 100%">&nbsp;</td></tr>
                        <tr><td style="padding-right:10px; text-align:center; color:Red; width: 100%; height: 13px;">
                        <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td></tr>
                        <tr>
                            <td style="padding-right: 10px; padding-left: 10px; padding-bottom: 10px;text-align: left; width:100%;">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="height: 38px; width:100%;">
                                    <asp:Menu ID="Menu1" runat="server" OnMenuItemClick="Menu1_MenuItemClick" Orientation="Horizontal" StaticEnableDefaultPopOutImage="False" Width="357px" meta:resourcekey="Menu1Resource1">                                        <Items>
                                    <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/crewMatrix_a.gif" Text=" " Value="0" meta:resourcekey="MenuItemResource1"></asp:MenuItem>
                                    <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/manningScale_d.gif" Text=" " Value="1" meta:resourcekey="MenuItemResource2"></asp:MenuItem>
                                    <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/vesselPosition_d.gif" Text=" " Value="2" meta:resourcekey="MenuItemResource3"></asp:MenuItem>
                                    <%--<asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/cert_d.gif" Text=" " Value="3" meta:resourcekey="MenuItemResource3"></asp:MenuItem>--%>
                                    
                                           </Items>
                                    </asp:Menu>
                                            &nbsp;
                                        </td>
                                        <td style=" height: 38px; text-align: center">
                                            <asp:ImageButton ID="imgbtn_Documents" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/btnVesselDetails1.gif"
                                                OnClick="imgbtn_Documents_Click" ToolTip="Crew Documents" /></td>
                                        <td style="height: 38px; text-align: center">
                                            <asp:ImageButton ID="imgbtn_Search" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/btnSearch.gif"
                                                OnClick="imgbtn_Search_Click" ToolTip="Search" /></td>
                                        <td style="height: 38px">
                                          </td>
                                    </tr>
                                </table>
                                <div id="divPrint">
                                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                        <asp:View ID="Tab1" runat="server"><asp:HiddenField ID="HiddenPK" runat="server" />
                                                <uc5:CrewMatrix ID="CrewMatrixl1" runat="server" />
                                        </asp:View>
                                        <asp:View ID="Tab2" runat="server">
                                                <uc2:VesselMiningScale id="VesselMiningScale1" runat="server"></uc2:VesselMiningScale>
                                            </asp:View>
                                        <asp:View ID="Tab3" runat="server">
                                        <table width="100%">
                                      <tr><td style="padding-right: 7px; padding-left: 5px"> 
        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid;">
             <legend><strong>Vessel </strong></legend>
               <table cellpadding="0" cellspacing="0" width="100%">
                   <tr>
                       <td align="right" style="width: 17%; height: 15px; text-align: right">
                      
                       </td>
                       <td rowspan="1" style="padding-left: 5px; width: 18%; height: 15px; text-align: left">
                       </td>
                       <td align="right" style="padding-left: 5px; width: 16%; height: 15px">
                           &nbsp;&nbsp;</td>
                       <td style="padding-left: 5px; width: 16%; height: 15px; text-align: left">
                           &nbsp;</td>
                       <td align="right" style="padding-left: 5px; width: 9%; height: 15px; text-align: right">
                       </td>
                       <td style="padding-left: 5px; width: 53%; height: 15px; text-align: left">
                       </td>
                       <td style="padding-left: 5px; width: 22%; height: 15px; text-align: left">
                       </td>
                   </tr>
                      <tr>
                    <td align="right" style="width: 17%; height: 15px; text-align: right">
                        Vessel Name:</td>
                    <td rowspan="1" style="padding-left: 5px; width: 18%; height: 15px; text-align: left">
                              <asp:TextBox ID="txt_arrival_VesselName" runat="server" CssClass="input_box" ReadOnly="True"
                                  Width="228px" MaxLength="49"></asp:TextBox></td>
                    <td align="right" style="padding-left: 5px; width: 16%; height: 15px">
                              Former Name:</td>
                    <td style="padding-left: 5px; width: 16%; height: 15px; text-align: left">
                        <asp:TextBox ID="txt_arrival_FormerVesselName" runat="server" CssClass="input_box" ReadOnly="True"
                                  Width="228px" MaxLength="49"></asp:TextBox></td>
                    <td align="right" style="padding-left: 5px; width: 9%; height: 15px; text-align: right">
                              Flag:</td>
                    <td style="padding-left: 5px; width: 53%; height: 15px; text-align: left">
                             <asp:DropDownList ID="ddl_arrival_flag" runat="server" CssClass="input_box" Width="176px"></asp:DropDownList></td>
                    <td style="padding-left: 5px; width: 22%; height: 15px; text-align: left">
                    </td>
                </tr>
                   <tr>
                       <td align="right" style="width: 17%; height: 15px; text-align: right">
                           &nbsp;</td>
                       <td rowspan="1" style="padding-left: 5px; width: 18%; height: 15px; text-align: left">
                           &nbsp;</td>
                       <td align="right" style="padding-left: 5px; width: 16%; height: 15px">
                       </td>
                       <td style="padding-left: 5px; width: 16%; height: 15px; text-align: left">
                       </td>
                       <td align="right" style="padding-left: 5px; width: 9%; height: 15px; text-align: right">
                       </td>
                       <td style="padding-left: 5px; width: 53%; height: 15px; text-align: left">
                       </td>
                       <td style="padding-left: 5px; width: 22%; height: 15px; text-align: left">
                       </td>
                   </tr>
                  </table>
            </fieldset></td></tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        <tr><td style="padding-right: 7px; padding-left: 5px"> 
                                        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid;">
                                        <legend><strong>Reports Type</strong></legend>
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                                RepeatDirection="Horizontal" Width="100%">
                                                <asp:ListItem Value="0" Selected="True">Arrival</asp:ListItem>
                                                <asp:ListItem Value="1">Sunday Noon</asp:ListItem>
                                                <asp:ListItem Value="2">Daily Noon</asp:ListItem>
                                                <asp:ListItem Value="3">Departure</asp:ListItem>
                                            </asp:RadioButtonList></fieldset></td></tr>
                                            <tr><td width="100%"><asp:MultiView ID="multi1" runat="server" ActiveViewIndex=0 >
                                            <asp:View ID="poition_view1" runat="server"><uc1:Arrival ID="arrival_control" runat="server"/></asp:View>
                                             <asp:View ID="position_view2" runat="server"><uc4:SundayNoon ID="sundaynoon_control" runat="server"/></asp:View>
                                            <asp:View ID="position_view3" runat="server"><uc3:DailyNoon ID="dailynoon_control" runat="server"/></asp:View>
                                             <asp:View ID="position_view4" runat="server"><uc6:Departure ID="departure_control" runat="server"/></asp:View>
                                            </asp:MultiView></td></tr>
                                            </table>
                                            </asp:View>
                                        <asp:View ID="Tab4" runat="server">
                                        
                                        
                                        
                                            <uc7:VesselCertificates ID="VesselCertificates1" runat="server" />
                                        
                                        
                                        
                                        </asp:View> 
                                </asp:MultiView>
                                </div>
                                </td>
                                </tr>
                                </table>
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
                                        
