<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CandidateDetailInformation.aspx.cs" Inherits="CandidateDetailInformation" MasterPageFile="~/MasterPage.master" Title="New Hire" %>
<%--<%@ Register TagName="menu" Src="~/UserControls/ModuleMenu.ascx" TagPrefix="mtm"  %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"/> 
   <%-- <link rel="stylesheet" href="../../../css/app_style.css" />--%>
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <style type="text/css" >
    .Page
    {
    	background-color:#C2C2C2;
    	color:#1589FF;
    }
    .SelectedPage
    {
    	background-color:#B38481;
    }
  
    .bordered tr td
    {
        border :solid 1px #e5e5e5;
    }

    </style>
    <style type="text/css">
         input[type=text]
{
	/*width:100%;
    top: 27px;
    left: 174px;
    background-color:white;*/
     float: left;
    padding: 3px 5px;
    font-size: 11px;
    line-height: 1.2;
    color: #555;
    background-color: #fff;
    background-image: none;
    border: 1px solid #ccc;
    border-radius: 0px; text-align:left; max-width:800px;
    -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
    box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
    -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
    -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
    transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
}
    </style>
    
    <script type="text/javascript" >
        var lastSel = null;
        function Selectrow(trSel, prid) {
            if (lastSel == null) {
                trSel.setAttribute(CSSName, "SelectedPage");
                lastSel = trSel;
                document.getElementById('hfPRID').value = prid;
            }
            else {
                if (lastSel.getAttribute("Id") == trSel.getAttribute("Id")) // clicking on same row
                {
                    //                    if(trSel.getAttribute(CSSName)=="selectedrow")
                    //                    {
                    //                        trSel.setAttribute(CSSName, lastSel.getAttribute("lastclass"));
                    //                        document.getElementById('hfPRID').value = "";
                    //                    }
                    //                    else
                    //                    {
                    trSel.setAttribute(CSSName, "SelectedPage");
                    lastSel = trSel;
                    document.getElementById('hfPRID').value = prid;
                    //}
                }
                else // clicking on ohter row
                {
                    lastSel.setAttribute(CSSName, lastSel.getAttribute("lastclass"));
                    trSel.setAttribute(CSSName, "SelectedPage");
                    lastSel = trSel;
                    document.getElementById('hfPRID').value = prid;
                }
            }
        }

        function Open() {
            window.open('NewAppEntry.aspx', '', 'width=900,height=700');
        }
    </script>
    
    
   <script language="javascript" type="text/javascript">
       function shownew() {
           window.open('CrewPersonalDetails.aspx', null, 'title=no,resizable=yes,menubar=yes,toolbars=yes,scrollbars=yes,addressbar=yes,location=yes');
       }
       function refreshPage() {
           document.getElementById('btnSearch').click();
       }
   </script>
   <script type="text/javascript" >
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
           if (isNaN(pos)) { pos = 0; }
           if (pos > 0) {
               document.getElementById(ctlid).scrollTop = pos;
           }
       }
       function SetScrollPos(ctl) {
           setCookie(ctl.id, ctl.scrollTop, 1);
       }
       //----------------------
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
       var lastSel = null;

       function Selectrow(trSel, prid) {
           if (lastSel == null) {
               trSel.setAttribute(CSSName, "selectedrow");
               lastSel = trSel;
               // document.getElementById('hfPRID').value = prid;
           }
           else {
               if (lastSel.getAttribute("Id") == trSel.getAttribute("Id")) // clicking on same row
               {
                   //                    if(trSel.getAttribute(CSSName)=="selectedrow")
                   //                    {
                   //                        trSel.setAttribute(CSSName, lastSel.getAttribute("lastclass"));
                   //                        document.getElementById('hfPRID').value = "";
                   //                    }
                   //                    else
                   //                    {
                   trSel.setAttribute(CSSName, "selectedrow");
                   lastSel = trSel;
                   //document.getElementById('hfPRID').value = prid;
                   //}
               }
               else // clicking on ohter row
               {
                   lastSel.setAttribute(CSSName, lastSel.getAttribute("lastclass"));
                   trSel.setAttribute(CSSName, "selectedrow");
                   lastSel = trSel;
                   // document.getElementById('hfPRID').value = prid;
               }
           }
       }
   </script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <asp:UpdateProgress runat="server" ID="UpdateProgress1">
            <ProgressTemplate>
            <div style="top :100px; width:100%; position :absolute; padding-top :100px; padding-left :90px; display:none;">
            <center >
            <div style =" border :solid 1px red; width : 120px; background-color :White; height :36px;">
            <img src="../Images/loading.gif" alt="loading ..." style ="float:left"/><span style ="font-size :11px;"><br />Loading ... </span>
            </div>
            </center>
            </div> 
            </ProgressTemplate>
        </asp:UpdateProgress> 
        <asp:UpdatePanel runat="server" ID="upd1">
        <ContentTemplate>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center;background-color:#f9f9f9">
               <tr>
                <td style="text-align:center;background-color:#4c7a6f;color:#fff;font-size: 14px;" class="text headerband">
                  <img runat="server" id="imgHome" style ="cursor:pointer;float :right; padding-right :5px;" src="~/Modules/HRD/Images/home.png" alt="Home" onclick="window.location.href='../Dashboard.aspx'" /> &nbsp;
                    New Hires
                </td>
               </tr>
                <tr>
                <td >
                    <table cellpadding="0" cellspacing="0" width ="100%">
                    <tr>
                        <td style="text-align: center;" align="center">
                        <asp:HiddenField ID="hfPRID" runat="server" Value="0" />       
                        <table style=" width :100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                <center>
                                    <table width="100%" cellpadding="1" border="0" style=" border-collapse:collapse;"  >
                                        <tr>
                                            <td style=" text-align :right;padding-top:5px;padding-bottom:5px; " >
                                                Application Id/Name :</td>
                                            <td style=" text-align :left;padding-top:5px;padding-bottom:5px; " >
                                            <asp:TextBox ID="txtIDName" runat="server" CssClass="input_box" Width="195px"></asp:TextBox>
                                            </td>
                                            <td style=" text-align :right ;padding-top:5px;padding-bottom:5px;" >
                                                Rank :</td>
                                            <td style=" text-align :left;padding-top:5px;padding-bottom:5px; " >
                                                <asp:DropDownList ID="ddlRank" runat="server" 
                                                    CssClass="input_box" Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style=" text-align :right;padding-top:5px;padding-bottom:5px; " >
                                                Nationality :</td>
                                            <td style=" text-align :left;padding-top:5px;padding-bottom:5px; " >
                                                       
                                                <asp:DropDownList ID="ddlNat" runat="server" 
                                                    CssClass="input_box" Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style=" text-align :right;padding-top:5px;padding-bottom:5px;">
                                                Status :</td>
                                            <td style=" text-align :left;padding-top:5px;padding-bottom:5px; ">
                                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="input_box" Width="150px">
                                               <%-- <asp:ListItem Text="< All >" Value="0" ></asp:ListItem>
                                                <asp:ListItem Text="Applicant" Value="1" Selected="True" ></asp:ListItem>
                                                <asp:ListItem Text="Ready for Approval" Value="2" ></asp:ListItem>
                                                <asp:ListItem Text="Approved" Value="3" ></asp:ListItem>
                                                <asp:ListItem Text="Rejected" Value="4" ></asp:ListItem>
                                                <asp:ListItem Text="Archived" Value="5" ></asp:ListItem>--%>
                                                        <asp:ListItem Text="< All >" Value="0" ></asp:ListItem>
                                                <asp:ListItem Text="Applicant" Value="1" Selected="True" ></asp:ListItem>
                                                <asp:ListItem Text="Awaiting Manning Approval" Value="2" ></asp:ListItem>
                                                <asp:ListItem Text="Ready for Proposal" Value="3" ></asp:ListItem>
                                                <asp:ListItem Text="Manning Rejected" Value="4" ></asp:ListItem>
                                                <asp:ListItem Text="Archived" Value="5" ></asp:ListItem>
                                                </asp:DropDownList></td>
                                            
                                        </tr>
                                        <tr>
                                            <td style=" text-align :right;padding-top:5px;padding-bottom:5px; " >
                                                Vessel Exp. :  </td>
                                            <td style=" text-align :left;padding-top:5px;padding-bottom:5px; " >
                                                <asp:DropDownList ID="ddlVType" runat="server" 
                                                    CssClass="input_box"  Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style=" text-align :right;padding-top:5px;padding-bottom:5px; " >
                                                Available From : </td>
                                            <td style=" text-align :left;padding-top:5px;padding-bottom:5px; " >
                                                <asp:TextBox ID="txt_SignOn_Date" runat="server" CssClass="input_box" MaxLength="10"
                                            Width="100px" TabIndex="8" ></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                            </td>
                                            <td style =" text-align:right;padding-top:5px;padding-bottom:5px;">
                                                To :</td>
                                            <td style =" text-align:left;padding-top:5px;padding-bottom:5px;">
                                                <asp:TextBox ID="txt_SignOff_Date" runat="server" CssClass="input_box" MaxLength="10"
                                            Width="100px" TabIndex="9" ></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                                <td style =" text-align:right ;padding-top:5px;padding-bottom:5px;" >
                                                City :
                                                </td>
                                                <td style=" text-align :left;padding-top:5px;padding-bottom:5px; " >
                                                <asp:TextBox runat ="server" CssClass="input_box" id="txtcity" MaxLength="50" Width="145px" ></asp:TextBox>
                                                </td>
                                            
                                        </tr>
                                        <tr>
                                            <td style=" text-align :right;padding-top:5px;padding-bottom:5px; " >
                                                Passport# :  </td>
                                            <td style=" text-align :left;padding-top:5px;padding-bottom:5px; " >
                                                <asp:TextBox ID="txtPassportNo" runat="server" CssClass="input_box" MaxLength="10" Width="195px" TabIndex="8"></asp:TextBox>
                                            </td>
                                            <td style=" text-align :right;padding-top:5px;padding-bottom:5px; " >Office : </td>
                                            <td style=" text-align :left;padding-top:5px;padding-bottom:5px; " >
                                                <asp:DropDownList ID="ddlOffice" runat="server" CssClass="input_box" Width="150px">
                                                </asp:DropDownList>

                                            </td>
                                            <td style =" text-align:right;padding-top:5px;padding-bottom:5px;">
                                               <%-- Created By :--%>

                                            </td>
                                            <td style =" text-align:left;padding-top:5px;padding-bottom:5px;">
                                                <%--<asp:DropDownList ID="ddlCreatedBy" runat="server" ></asp:DropDownList>--%>
                                            </td>
                                            <td style =" text-align:right ;padding-top:5px;padding-bottom:5px;">&nbsp;</td>
                                            <td style=" text-align :left;padding-top:5px;padding-bottom:5px; margin-left: 40px;" >
                                                <asp:Button runat="server" id="btnSearch" class="btn" OnClick="UpdateList" Text="Search" />
                                               <%-- <input type="button" value="New Applicant" class="btn" style="width:110px; text-align: center;" onclick="Open()" />--%>
                                                 <asp:Button runat="server" id="btnNewApplicant" class="btn"  Text="New Applicant" OnClick="btnNewApplicant_Click" />
                                            </td>
                                           
                                        </tr>
                                    </table>
                                </center>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton2" TargetControlID="txt_SignOff_Date" PopupPosition="TopLeft"></ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupPosition="TopLeft" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" TargetControlID="txt_SignOn_Date"></ajaxToolkit:CalendarExtender>
                                
                                <div style="overflow-x:hidden; overflow-y :scroll;height:30px; border:solid 1px gray;" >
                                <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse: collapse;height: 30px; color: black;" class="bordered">
                                <colgroup >
                                <col width="5%;" /> 
                                <col width="8%;" /> 
                                <col width="20%;"/> 
                                <col width="10%;" />  
                                <col width="8%;" /> 
                                <col width="9%;" /> 
                                <col width="10%;" /> 
                                <col width="12%;" /> 
                                <col width="3%;" />
                                <col width="15%;" style=" display:none;" />
                                </colgroup>
                                <tr class="headerstylegrid"  style="font-family: Arial, sans-serif;	font-weight:bold;">
                                <td scope="col" style=" text-align:left">View</td>
                                <td scope="col" style=" text-align:left" >Applicant ID</td>
                                <td scope="col" style=" text-align:left" >Name</td>
                                <td scope="col" style=" text-align:left" >Rank</td>
                                <td scope="col" style=" text-align:left" >Nationality</td>
                                <td scope="col" style=" text-align:left" >City</td>
                                <td scope="col" style=" text-align:left" >Available From</td>
                                <td scope="col" style=" text-align:left" >Applicant Status</td>
                                <td scope="col" style=" text-align:left" >CV</td>
                                <td scope="col" style=" text-align:left;display:none;">Proposal Action</td>
                                </tr>
                                </table>
                                </div>
                                <div style="overflow-x:hidden; overflow-y :scroll; width:100%;height:383px;" id="dvscroll_cdinfo" onscroll="SetScrollPos(this)">
                                <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse: collapse;" class="bordered">
                                <colgroup >
                                <col width="5%;" /> 
                                <col width="8%;" /> 
                                <col width="20%;"/> 
                                <col width="10%;" />  
                                <col width="8%;" /> 
                                <col width="9%;" /> 
                                <col width="10%;" /> 
                                <col width="12%;" /> 
                                <col width="3%;" />
                                <col width="15%;" style="display:none;" />
                                </colgroup>
                                <asp:Repeater runat="server" ID="rptData" OnItemDataBound="rptData_ItemDataBound">
                                <ItemTemplate>
                                <tr id='tr<%#Eval("CandidateId")%>' onclick='Selectrow(this,<%#Eval("CandidateId")%>);' lastclass='alternaterow'>
                                    <td style =" text-align :center;  ">
                                    <asp:ImageButton runat="server" ID="imgView" ToolTip="View" ImageUrl="~/Modules/HRD/Images/HourGlass.gif" CommandArgument='<%#Eval("CandidateId")%>' OnClick="Open_Candidate" />
                                    </td>
                                    <td style=" ">&nbsp;<%#Eval("CandidateId")%></td>
                                    <td style=" text-align :left;" >&nbsp;<%#Eval("Name")%></td>
                                    <td style=" text-align :left;">&nbsp;<%#Eval("Rank")%></td>
                                    <td style=" text-align :left;">&nbsp;<%#Eval("Country")%></td>
                                    <td style=" text-align :left;">&nbsp;<%#Eval("City")%></td>
                                    <td style=" text-align :left;">&nbsp;<%#Eval("AvailableFrom")%></td>
                                    <td style=" text-align :left;">&nbsp;<%#Eval("StatusName")%><asp:HiddenField ID="hdfStatusName" runat="server" Value='<%#Eval("StatusName")%>' />
                                    </td>
                                    <td style=" text-align :left;">
                                        <a title="Click here to open applicant CV." href='/<%#Eval("AppName")%>/EMANAGERBLOB/HRD/Applicant/CV/<%#Eval("FileName")%>' style='display:<%#(Eval("FileName").ToString()=="")?"none":"block"%>' target="_blank"><img src="../Images/paperclip.gif" style="border : none" /></a>
                                     </td>
                                   <%-- <td style=" ">
                                        <asp:HiddenField ID="hfdFileName" runat="server" Value='<%#Eval("FileName")%>' />
                                        <img src='~/Modules/HRD/Images/icon_phone.jpg' style='display :<%# (Eval("DISCTYPE").ToString()=="P")?"":"none"%>' title="<%#Eval("DISCUSSION")%>" />
                                        <img src='~/Modules/HRD/Images/icon-email.gif' style='display :<%# (Eval("DISCTYPE").ToString()=="E")?"":"none"%>' title="<%#Eval("DISCUSSION")%>"/>
                                        <img src='~/Modules/HRD/Images/icon_user.gif' style='display :<%# (Eval("DISCTYPE").ToString().Trim()=="I")?"":"none"%>' title="<%#Eval("DISCUSSION")%>"/>
                                    </td>
                                    <td style=" text-align :left;">&nbsp;
                                        <%#Eval("ModifiedBy")%>/<%# Common.ToDateString( Eval("ModifiedOn"))%></td>--%>
                                    
                                    <td style =" text-align :left;font-size:12px;display:none;  ">
                                    <asp:LinkButton runat="server" ID="lbProposalToOwner" Text="Proposal to Client"  CommandArgument='<%#Eval("CandidateId")%>' OnClick="ProposalToOwner" Visible="false" Font-Size="14px" ForeColor="DarkGreen" /> 
                                        <asp:LinkButton runat="server" ID="lbResendProposal" Text="Resend"  CommandArgument='<%#Eval("CandidateId")%>' OnClick="lbResendProposal_click" Visible="false" Font-Size="14px" ForeColor="DarkGreen"  /> <asp:Label ID="lblslase" runat="server" Text="/" Visible="false"></asp:Label> 
                                        <asp:LinkButton runat="server" ID="lbWithDraw" Text="Withdraw"  CommandArgument='<%#Eval("CandidateId")%>' OnClientClick="return confirm('Are you sure to withdraw proposal for this applicant?');" OnClick="lbWithDraw_click" Visible="false" Font-Size="14px" ForeColor="DarkGreen" /> 
                                        <asp:HiddenField ID="hdnCrewNumber" runat="server" Value='<%#Eval("CrewNumber")%>' />
                                        <asp:HiddenField ID="hdnClientStatus" runat="server" Value='<%#Eval("ClientStatus")%>' />
                                    </td>
                                     
                                    
                                </tr> 
                                </ItemTemplate> 
                                </asp:Repeater> 
                                </table> 
                                <asp:Label ID="lbl_License_Message" runat="server"></asp:Label>
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
            <div style="padding: 2px; font-size: 15px; text-align: center; background-color:#e5e5e5;color:white;margin:0px auto;">
                <table cellpadding="2" cellspacing="2" border="0" style="margin:0px auto;"   >                                     
                    <tr>
                        <td style="width:120px;float:right;">&nbsp;
                            <asp:LinkButton ID="lnkPrev20Pages" runat="server" Text='<< Previous 20' OnClick="lnkPrev20Pages_OnClick" Font-Size="15px" style="text-decoration:none;" Visible="false" ></asp:LinkButton>
                        </td>
                        <asp:Repeater ID="rptPageNumber" runat="server" >
                            <ItemTemplate>
                            <td id='tr<%#Eval("PageNumber")%>' class='<%#(Convert.ToInt32(Eval("PageNumber"))!=PageNo)?"Page":"SelectedPage"%>' onclick='Selectrow(this,<%#Eval("PageNumber")%>);' lastclass='Page' style="width:15px;border:solid 1px #c2c2c2">
                                <asp:LinkButton ID="lnPageNumber" runat="server" Text='<%#Eval("PageNumber") %>' OnClick="lnPageNumber_OnClick" Font-Size="15px" style="text-decoration:none;" ></asp:LinkButton>
                            </td>
                            </ItemTemplate>
                        </asp:Repeater>
                        <td style="width:120px;">
                            &nbsp;
                            <asp:LinkButton ID="lnkNext20Pages" runat="server" Text='Next 20 >>' OnClick="lnkNext20Pages_OnClick" Font-Size="15px" style="text-decoration:none;" ></asp:LinkButton>
                        </td>
                        <td>
                            <asp:Label ID="lblRCount" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
             <div style="position: absolute; top: 25px; left: 100px; height: 650px; width: 85%;" id="dvProposalToOwner" runat="server" visible="false">
            <center>
                <div style="position: absolute; top: 25px; left: 100px; height: 650px; width: 85%; background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                <div style="position: relative; width: 85%; height: 600px; padding: 3px; text-align: center; background: white; z-index: 150; top: 50px; border: solid 1px Black">
                      <div style=" text-align: center;vertical-align:central " class="text headerband">
                   Send Proposal to Client
                           <div style=" float :right " >
                          <asp:ImageButton ID="ibClose" runat="server" ImageUrl="~/Modules/HRD/Images/Close.gif" OnClick="ibClose_Click" CausesValidation="false" />
                               </div>
             </div>
                    <div style=" text-align: center;width:100%;padding-left:100px;padding-right:100px;padding:5px;">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="true" ></asp:Label> 
                        <asp:HiddenField ID="hdnRandomPwd" runat="server" ></asp:HiddenField> 
                         <asp:HiddenField ID="hdnCandidateId" runat="server" ></asp:HiddenField> 
                         <asp:HiddenField ID="hdnResendProposal" runat="server" Value="false" ></asp:HiddenField>
                        <asp:HiddenField ID="hdnCompany" runat="server" Value="false" ></asp:HiddenField>
                    </div>
            <div style=" text-align: center;width:100%;">
                <table style="width:100%;" >
                    <tr>
                        <td style="padding-right:5px;text-align:right;width:200px;padding:5px;">
                            Client Name : 
                        </td>
                        <td style="padding-left:5px;text-align:left;padding:5px;">
                            <asp:DropDownList ID="ddl_Ownerlist" runat="server" Width="450px" TabIndex="1"  CssClass="input_box" Style="background-color: lightyellow" OnSelectedIndexChanged="ddl_Ownerlist_SelectedIndexChanged" AutoPostBack="true"  ></asp:DropDownList> &nbsp;  <asp:CompareValidator ID="cv_Ownerlist" runat="server" ControlToValidate="ddl_Ownerlist" ErrorMessage="Required." Operator="GreaterThan" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                        </td>
                       
                    </tr>
                     <tr>
                        <td style="padding-right:5px;text-align:right;width:200px;padding:5px;">
                            Vessel : 
                        </td>
                        <td style="padding-left:5px;text-align:left;padding:5px;">
                            <asp:DropDownList ID="ddl_Vessel" runat="server" Width="450px" TabIndex="1"  CssClass="input_box" Style="background-color: lightyellow"  AutoPostBack="true" OnSelectedIndexChanged="ddl_Vessel_SelectedIndexChanged"   ></asp:DropDownList> &nbsp;  <asp:CompareValidator ID="cv_Vessel" runat="server" ControlToValidate="ddl_Vessel" ErrorMessage="Required." Operator="GreaterThan" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                        </td>
                       
                    </tr>
                    <tr>
                        <td style="padding-right:5px;text-align:right;width:200px;padding:5px;">
                            Manning Officer Remarks : 
                        </td>
                        <td style="padding-left:5px;text-align:left;padding:5px;" >
                            <asp:TextBox ID="txtManningOfficerRemarks" runat="server" TabIndex="2" Width="700px" TextMode="MultiLine"  CssClass="input_box" Style="background-color: lightyellow" Height="55px" MaxLength="500"></asp:TextBox> &nbsp; <asp:RequiredFieldValidator ID="rfvManningOfficerRemarks" runat="server" ControlToValidate="txtManningOfficerRemarks" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                        </td>
                       
                    </tr>
                    <tr>
                        <td style="padding-right:5px;text-align:right;width:200px;padding:5px;">
                            From Email : 
                        </td>
                        <td style="padding-left:5px;text-align:left;vertical-align:middle;padding:5px;" >
                             <asp:TextBox ID="txtFromAddress" runat="server" TabIndex="3" TextMode="SingleLine" MaxLength="200"   Width="700px" ReadOnly="true" ></asp:TextBox> &nbsp; <asp:RequiredFieldValidator ID="rfvFromAddress" runat="server" ControlToValidate="txtFromAddress" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                        </td>
                       
                    </tr>
                    <tr>
                        <td style="padding-right:5px;text-align:right;width:200px;padding:5px;">
                            To Email : 
                        </td>
                        <td style="padding-left:5px;text-align:left;vertical-align:middle;padding:5px;" >
                             <asp:TextBox ID="txtToEmail" runat="server" TabIndex="4" TextMode="SingleLine" MaxLength="500"  Width="700px" Style="background-color: lightyellow"  ></asp:TextBox> &nbsp; <asp:RequiredFieldValidator ID="rfvToEmail" runat="server" ControlToValidate="txtToEmail" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                        </td>
                       
                    </tr>
                     <tr>
                        <td style="padding-right:5px;text-align:right;width:200px;padding:5px;">
                            BCC Email : 
                        </td>
                        <td style="padding-left:5px;text-align:left;vertical-align:middle;padding:5px;" >
                             <asp:TextBox ID="txtBCCEmail" runat="server" TabIndex="4" TextMode="SingleLine" MaxLength="500"  Width="700px" Style="background-color: lightyellow"  ></asp:TextBox>
                        </td>
                       
                    </tr>
                    <tr>
                        <td style="padding-right:5px;text-align:right;width:200px;padding:5px;">
                            Subject : 
                        </td>
                        <td style="padding-left:5px;text-align:left;vertical-align:middle;padding:5px;" >
                             <asp:TextBox ID="txtSubject" runat="server" TabIndex="5" TextMode="SingleLine" MaxLength="500"  Width="700px" Style="background-color: lightyellow"  ></asp:TextBox>
                        </td>
                        
                    </tr>
                     <tr>
                        <td style="padding-right:5px;text-align:right;width:200px;padding:5px;">
                            Email Body : 
                        </td>
                        <td style="padding-left:5px;text-align:left;vertical-align:middle;padding:5px;" >
                             <%--<asp:TextBox ID="txtEmailBody" runat="server" TabIndex="6" TextMode="MultiLine" MaxLength="4000"   CssClass="input_box" Width="700px" Height="200px" ></asp:TextBox> &nbsp;  <asp:RequiredFieldValidator ID="rfvEmailBody" runat="server" ControlToValidate="txtEmailBody" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>--%>
                            <div contenteditable="true" runat="server" id="dvContent" style="overflow-x:hidden; overflow-y :scroll; width:100%;height:200px;"  onscroll="SetScrollPos(this)">
                            <asp:Literal runat="server" ID="litMessage"></asp:Literal> 
                            </div>
                        </td>
                      
                    </tr>
                </table>
            </div>
             <div style=" text-align: center;width:70%;padding-left:100px;padding-right:100px;padding:5px;">
                 <asp:Button ID="btnSendProposal" runat="server" CssClass="btn" Width="150px" CausesValidation="false" TabIndex="7" Text="Send Mail to Client" OnClick="btnSendProposal_Click" />
                 <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text=" Cancel " TabIndex="8" CausesValidation="false" OnClick="btnCancel_Click"  />
             </div>
                    
                </div>
            </center>
        </div>
        </ContentTemplate> 
        </asp:UpdatePanel>
</asp:Content>
