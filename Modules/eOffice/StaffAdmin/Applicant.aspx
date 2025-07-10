<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Applicant.aspx.cs" Inherits="Applicant" MasterPageFile="~/Modules/eOffice/StaffAdmin/StaffAdmin.master" %>
<%--<%@ Register TagName="menu" Src="~/UserControls/ModuleMenu.ascx" TagPrefix="mtm"  %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"> 
    
   <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    
    
    <style type="text/css" >
    .SelectedPage
    {
    	background-color:#B38481;
    }
    </style>
    <script type="text/javascript" >
        var lastSel=null;
        function Selectrow(trSel, prid) 
        {
            if(lastSel==null)
            {
                trSel.setAttribute(CSSName, "SelectedPage");
                lastSel=trSel;
                document.getElementById('hfPRID').value = prid;
            }
            else
            {
                if(lastSel.getAttribute("Id")==trSel.getAttribute("Id")) // clicking on same row
                {   
                    //                    if(trSel.getAttribute(CSSName)=="selectedrow")
                    //                    {
                    //                        trSel.setAttribute(CSSName, lastSel.getAttribute("lastclass"));
                    //                        document.getElementById('hfPRID').value = "";
                    //                    }
                    //                    else
                    //                    {
                        trSel.setAttribute(CSSName, "SelectedPage");
                        lastSel=trSel;
                        document.getElementById('hfPRID').value = prid;
                    //}
                }
                else // clicking on ohter row
                {
                    lastSel.setAttribute(CSSName, lastSel.getAttribute("lastclass"));
                    trSel.setAttribute(CSSName, "SelectedPage");
                    lastSel=trSel;
                    document.getElementById('hfPRID').value = prid;
                }
            }
        }
        
        function Open()
        {
            window.open('NewAppEntry.aspx','','');
        }
    </script>    
    
   <script language="javascript" type="text/javascript">
   function shownew()
   {
    window.open('CrewPersonalDetails.aspx',null,'title=no,resizable=yes,menubar=yes,toolbars=yes,scrollbars=yes,addressbar=yes,location=yes');
   }
   function refreshPage() {
       document.getElementById('btnReferesh').click();   
   }
   </script>
   <script type="text/javascript" >
    function getCookie(c_name)
    {
    var i,x,y,ARRcookies=document.cookie.split(";");
    for (i=0;i<ARRcookies.length;i++)
    {
      x=ARRcookies[i].substr(0,ARRcookies[i].indexOf("="));
      y=ARRcookies[i].substr(ARRcookies[i].indexOf("=")+1);
      x=x.replace(/^\s+|\s+$/g,"");
      if (x==c_name)
        {
        return unescape(y);
        }
      }
    }
    function setCookie(c_name,value,exdays)
    {
    var exdate=new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var c_value=escape(value) + ((exdays==null) ? "" : "; expires="+exdate.toUTCString());
    document.cookie=c_name + "=" + c_value;
    }
    function SetLastFocus(ctlid)
    {
        pos=getCookie(ctlid);
        if(isNaN(pos)) 
        {pos=0;}
        if(pos>0)
        {
        document.getElementById(ctlid).scrollTop=pos;
        }
    }
    function SetScrollPos(ctl)
    {
        setCookie(ctl.id,ctl.scrollTop,1); 
    }
    //----------------------
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
    var lastSel=null;

    function Selectrow(trSel, prid) 
        {
            if(lastSel==null)
            {
                trSel.setAttribute(CSSName, "selectedrow");
                lastSel=trSel;
               // document.getElementById('hfPRID').value = prid;
            }
            else
            {
                if(lastSel.getAttribute("Id")==trSel.getAttribute("Id")) // clicking on same row
                {   
                    //                    if(trSel.getAttribute(CSSName)=="selectedrow")
                    //                    {
                    //                        trSel.setAttribute(CSSName, lastSel.getAttribute("lastclass"));
                    //                        document.getElementById('hfPRID').value = "";
                    //                    }
                    //                    else
                    //                    {
                        trSel.setAttribute(CSSName, "selectedrow");
                        lastSel=trSel;
                        //document.getElementById('hfPRID').value = prid;
                    //}
                }
                else // clicking on ohter row
                {
                    lastSel.setAttribute(CSSName, lastSel.getAttribute("lastclass"));
                    trSel.setAttribute(CSSName, "selectedrow");
                    lastSel=trSel;
                  // document.getElementById('hfPRID').value = prid;
                }
            }
        }
    </script>

      
        <asp:UpdateProgress runat="server" ID="UpdateProgress1">
            <ProgressTemplate>
            <div style="top :100px; width:100%; position :absolute; padding-top :100px; padding-left :90px;">
            <center >
            <div style ="width : 120px; height :36px;">
            <img src="../../HRD/Images/loading.gif" alt="loading ..." style ="float:left"/><span style ="font-size :11px;"><br />Loading ... </span>
            </div>
            </center>
            </div> 
            </ProgressTemplate>
        </asp:UpdateProgress> 
        <asp:UpdatePanel runat="server" ID="upd1">
        <ContentTemplate>
           <table width="100%" cellpadding="0" cellspacing="0" style="border-collapse:collapse;">
                <tr>
                   
                    <td valign="top">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center;background-color:#f9f9f9">
                            <tr>
                            <td align="center" style=" height: 23px" class="text headerband" >Applicant List</td>
                            </tr>
                                <tr>
                                <td >
                                    <table cellpadding="0" cellspacing="0" width ="100%">
                                    <tr>
                                        <td><asp:Label ID="lbl_info" runat="server" Width="100%" ForeColor="Red"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;" align="center">
                                        <asp:HiddenField ID="hfPRID" runat="server" Value="0" />       
                                        <table style=" width :100%"  border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                        <center>
                                                            <table width="100%" border="0" >
                                                                <tr>
                                                                    <td style=" text-align :right " >
                                                                        Application Name :</td>
                                                                    <td style=" text-align :left " >
                                                                    <asp:TextBox ID="txtIDName" runat="server" AutoPostBack="true" 
                                                                            CssClass="input_box" OnTextChanged="UpdateList" Width="127px"></asp:TextBox>
                                                                    </td>
                                                                    <td style=" text-align :right " >
                                                                        Position :</td>
                                                                    <td style=" text-align :left " >
                                                                        <asp:DropDownList ID="ddlRank" runat="server" AutoPostBack="true" 
                                                                            CssClass="input_box" OnSelectedIndexChanged="UpdateList" Width="100px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style=" text-align :right " >
                                                                        Nationality :</td>
                                                                    <td style=" text-align :left " >
                                                                       
                                                                        <asp:DropDownList ID="ddlNat" runat="server" AutoPostBack="true" 
                                                                            CssClass="input_box" OnSelectedIndexChanged="UpdateList" Width="150px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style=" text-align :left ">
                                                                        Status :</td>
                                                                    <td style=" text-align :left ">
                                                                         <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" CssClass="input_box" OnSelectedIndexChanged="UpdateList" Width="150px">
                                                                        <%--<asp:ListItem Text="< All >" Value="0" ></asp:ListItem>
                                                                        <asp:ListItem Text="Applicant" Value="1" Selected="True" ></asp:ListItem>
                                                                        <asp:ListItem Text="Ready for Approval" Value="2" ></asp:ListItem>
                                                                        <asp:ListItem Text="Approved" Value="3" ></asp:ListItem>
                                                                        <asp:ListItem Text="Rejected" Value="4" ></asp:ListItem>
                                                                        <asp:ListItem Text="Archived" Value="5" ></asp:ListItem>--%>
                                                                        </asp:DropDownList></td>
                                                                    <td style=" text-align :left; text-align :center">
                                                                        <asp:Button ID="btnReferesh" runat="server" Text="Search" style="display:none;" OnClick="btnReferesh_OnClick" ></asp:Button>
                                                                        <input type="button" onclick="Open()"  class="btn" value="New Applicant" style="width:90px;"/>
                                                                        <%--<asp:Button runat="server" CssClass="btn" ID="txtBack" PostBackUrl="../Emtm_Home.aspx" Width="60px" Text="Back"/>--%>
                                                                        <asp:Button runat="server" CssClass="btn" ID="btnClear" Width="60px" Text="Clear" OnClick="btnClear_OnClick"/>
                                                                        <%--<asp:Button ID="btnNew" runat="server" CssClass="btn" Width="65px" Text="New App"/>--%>
                                                                        
                                                                        
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </center>
                                                        
                                                        <div style="overflow-x:hidden; overflow-y :hidden; width:100%;">
                                                        <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                                                        <table width="100%" cellspacing="0" cellpadding="1" border="1" rules="all"  style="border-collapse:collapse; height:26px;">
                                                        <colgroup >
                                                            <col width="50px;" /> 
                                                            <col /> 
                                                            <col width="180px;" />  
                                                            <col width="160px;" /> 
                                                            <col width="100px;" /> 
                                                            <col width="50px;" />
                                                            <col width="300px;" />
                                                            <col width="25px;" />
                                                        </colgroup>
                                                        <tr align="left" class= "headerstylegrid"> 
                                                                <th scope="col" style=" font-size :11px;" >Open</th>
                                                                <th scope="col" style=" font-size :11px;" >
                                                                    <asp:LinkButton ID="lnkNameSoft" runat="server" Text="Name" CommandArgument="Name"  OnClick="Sorting_Click"></asp:LinkButton>
                                                                </th>
                                                                <th scope="col" style=" font-size :11px;" >
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" Text="Position" CommandArgument="PositionName"   OnClick="Sorting_Click"></asp:LinkButton>
                                                                    </th>
                                                                <th scope="col" style=" font-size :11px;" >Mobile #</th>
                                                                <th scope="col" style=" font-size :11px;" >
                                                                    <asp:LinkButton ID="LinkButton2" runat="server" Text="Status" CommandArgument="StatusName"  OnClick="Sorting_Click"></asp:LinkButton>
                                                                    </th>
                                                                <th scope="col" style=" font-size :11px;" >CV</th>
                                                                <th scope="col" style=" font-size :11px;">Email</th>
                                                                <th scope="col">&nbsp;</th>
                                                        </tr>
                                                        </table>
                                                        </div>
                                                        </div>
                                                        <div style="overflow-x:hidden; overflow-y :scroll; width:100%;height:390px;" id="dvscroll_cdinfo" onscroll="SetScrollPos(this)">
                                                        <table width="100%" cellspacing="0" cellpadding="1" border="1" rules="all" style="border-collapse:collapse;" >
                                                        <colgroup >
                                                            <col width="50px;" /> 
                                                            <col /> 
                                                            <col width="180px;" />  
                                                            <col width="160px;" /> 
                                                            <col width="100px;" /> 
                                                            <col width="50px;" />
                                                            <col width="300px;" />
                                                            <col width="25px;" />
                                                        </colgroup>
                                                        <asp:Repeater runat="server" ID="rptData">
                                                        <ItemTemplate>
                                                        <tr class='<%# (Common.CastAsInt32(Eval("APID"))==SelectedAPID)?"selectedrow":"row"%>'>
                                                            <td style="text-align :center; font-size :11px; ">
                                                            <asp:ImageButton runat="server" ID="imgView" ToolTip="View" ImageUrl="~/Modules/HRD/Images/HourGlass.gif" CommandArgument='<%#Eval("APID")%>' OnClick="imgView_OnClick" />
                                                            </td>
                                                            </td>
                                                            <td style=" text-align :left;font-size :11px;" >&nbsp;<%#Eval("Name")%></td>
                                                            <td style=" text-align :left;font-size :11px;">&nbsp;<%#Eval("PositionName")%></td>
                                                            <td style=" font-size :11px;">&nbsp;<%#Eval("MobNumber")%></td>
                                                            <td style=" font-size :11px;">&nbsp;<%#Eval("StatusName")%></td>
                                                            <td style=" font-size :11px;">
                                                              <a title="Click here to open applicant CV." href='../../../../EMANAGERBLOB/HRD/Applicant/<%#Eval("FileName")%>' style='display:<%#(Eval("FileName").ToString()=="")?"none":"block"%>' target="_blank">
                                                              <img src="../../HRD/Images/paperclip12.gif" style="border : none" /> </a>
                                                              <asp:HiddenField ID="hfdFileName" runat="server" Value='<%#Eval("FileName")%>' />
                                                            </td>
                                                            
                                                            <td style=" font-size :11px; text-align :left;">
                                                                <a href='mailto:<%#Eval("EmailId")%>' ><%#Eval("EmailId")%></a> 
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr> 
                                                        </ItemTemplate> 
                                                        </asp:Repeater> 
                                                        
                                                        <tr>
                                                            <td colspan="8">
                                                                
                                                            </td>
                                                        </tr>
                                                        </table> 
                                                            <asp:Label ID="lbl_License_Message" runat="server"></asp:Label>
                                                        </div>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table  cellpadding="2" cellspacing="3" border="0"  >                                     
                                                        <tr>
                                                        <td style="width:150px;float:right;">
                                                            <asp:LinkButton ID="lnkPrev20Pages" runat="server" Text='<< Previous 20' OnClick="lnkPrev20Pages_OnClick" Font-Size="11px" style="text-decoration:none;" Visible="false" ></asp:LinkButton>
                                                        </td>
                                                        <asp:Repeater ID="rptPageNumber" runat="server" >
                                                            <ItemTemplate>
                                                                <td  id='tr<%#Eval("PageNumber")%>' class='<%#(Convert.ToInt32(Eval("PageNumber"))!=PageNo)?"Page":"SelectedPage"%>' onclick='Selectrow(this,<%#Eval("PageNumber")%>);' lastclass='Page'>
                                                                <%--<td  class="Page">--%>
                                                                
                                                                    <asp:LinkButton ID="lnPageNumber" runat="server" Text='<%#Eval("PageNumber") %>' OnClick="lnPageNumber_OnClick" Font-Size="11px" style="text-decoration:none;" ></asp:LinkButton>
                                                                </td>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <td style="width:150px;">
                                                            <asp:LinkButton ID="lnkNext20Pages" runat="server" Text='Next 20 >>' OnClick="lnkNext20Pages_OnClick" Font-Size="11px" style="text-decoration:none;" ></asp:LinkButton>
                                                        </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        </td>
                                    </tr>
                                    </table>
                                    </td>
                                </tr>
                            </table>            
                     </td>
                </tr>
            </table>
            
        </ContentTemplate> 
        </asp:UpdatePanel>
   </asp:Content>
