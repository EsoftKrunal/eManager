<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PeapRegister.aspx.cs" Inherits="PeapRegister" MasterPageFile="~/Modules/eOffice/StaffAdmin/StaffAdmin.master" %>
<%--<%@ Register TagName="menu" Src="~/UserControls/ModuleMenu.ascx" TagPrefix="mtm"  %>--%>
<%@ Register src="~/Modules/eOffice/StaffAdmin/PeapHeaderMenu.ascx" tagname="Hr_PeapHeaderMenu" tagprefix="Peap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

   
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">     
  <link href="../../HRD/Styles/StyleSheet.css" rel="vstylesheet" type="text/css" />
    <script type="text/javascript" src="../../JS/jquery.min.js"></script>
     <script src="../../JS/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
     <!-- Auto Complete -->
     <link rel="stylesheet" href="../../JS/AutoComplete/jquery-ui.css" />
     <script src="../../JS/AutoComplete/jquery-ui.js" type="text/javascript"></script>
     <script type="text/javascript">
         function RegisterAutoComplete() 
         {
             $("#TabContainer1_TabPanel1_txtlinkkpiname").keydown(function () {
                 $("#TabContainer1_TabPanel1_hfdlinkkpiid").val("");
             });

             $(function () {
                 //------------
                 $("#TabContainer1_TabPanel1_txtlinkkpiname").autocomplete(
                 {
                     source: function (request, response) {
                         $.ajax({
                             url: "http://emanagershore.energiosmaritime.com/cms/getautocompletedata.ashx",
                             dataType: "json",
                             headers: { "cache-control": "no-cache" },
                             type: "POST",
                             data: { Key: $("#TabContainer1_TabPanel1_txtlinkkpiname").val(), Type: "KPI" },
                             cache: false,
                             success: function (data) {
                                 response($.map(data.geonames, function (item) { return { label: item.kname, value: item.kname, bidid: item.entryid } }
                                    ));
                             }
                         });
                     },
                     minLength: 2,
                     select: function (event, ui) {
                         $("#TabContainer1_TabPanel1_hfdlinkkpiid").val(ui.item.bidid);
                         
                     },
                     open: function () {
                         $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
                     },
                     close: function () {
                         $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
                     }
                 });
             });
         }
         function getBaseURL() {
             var url = window.location.href.split('/');
             var baseUrl = url[0] + '//' + url[2] + '/' + url[3];
             return baseUrl;
         }
        </script>
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
        function fncInputIntegerValuesOnly(evnt) {
            if (!(event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }
    </script>

       
        
        <asp:UpdateProgress runat="server" ID="UpdateProgress1">
            <ProgressTemplate>
            <div style="top :100px; width:100%; position :absolute; padding-top :100px; padding-left :90px;">
            <center >
            <div style ="width : 120px; height :36px;">
            <img src="../Images/loading.gif" alt="loading ..." style ="float:left"/><span style ="font-size :11px;"><br />Loading ... </span>
            </div>
            </center>
            </div> 
            </ProgressTemplate>
        </asp:UpdateProgress> 
       
           
                      
           <%--------------------------------------------------------------------------------------------------------------------------%>
           <table width="100%" cellpadding="0" cellspacing="0" style="border-collapse:collapse;">
                <tr>
                   
                    <td valign="top">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center;background-color:#f9f9f9">
                            <tr>
                                <td  > <Peap:Hr_PeapHeaderMenu ID="uc_Peap" runat="server" />  </td>
                            </tr>
                        </table>
                        <%-------------------------%>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <div id="divMain" runat="server" style="padding: 5px; border-bottom: solid 1px #4371a5;">
                                        <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" AutoPostBack="true"
                                            CssCssClass="ajax__myTab" Height="432px" Width="100%" OnActiveTabChanged="TabContainer1_ActiveTabChanged">
                                            <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="Self Assessment Questions">
                                                <ContentTemplate>
                                                    <asp:UpdatePanel runat="server" ID="upd1">
                                                        <ContentTemplate>
                                                        <table cellpadding="1" cellspacing="1" border="0" width="100%">
                                                            <tr>
                                                                <td style="text-align: right; font-weight: bold; width: 100px;">
                                                                    Peap Level :&nbsp;
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" Height="20px" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                            <table cellpadding="2" cellspacing="2" border="0" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <div style="overflow-x: hidden; overflow-y: hidden; width: 100%;">
                                                                        <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                                                                            <table width="100%" cellspacing="0" cellpadding="1" border="1" rules="all" style="border-collapse: collapse; height:26px;">
                                                                                <colgroup>
                                                                                    <col width="50px;" />
                                                                                    <col width="50px;" />
                                                                                    <col width="50px;" />
                                                                                    <col />
                                                                                    <%--<col width="80px" />--%>
                                                                                    <col width="25px;" />  
                                                                                </colgroup>
                                                                                <tr align="left" class= "headerstylegrid">
                                                                                    <th scope="col" style="font-size: 11px;">
                                                                                        Edit
                                                                                    </th>
                                                                                    <th scope="col" style="font-size: 11px;">
                                                                                        Delete
                                                                                    </th>
                                                                                    <th scope="col" style="font-size: 11px;">
                                                                                        Sr No.
                                                                                    </th>
                                                                                    <th scope="col" style="font-size: 11px;">
                                                                                        Question
                                                                                    </th>
                                                                                    <%--<th scope="col" style="font-size: 11px;">
                                                                                        Status
                                                                                    </th>--%>
                                                                                    <th scope="col" style=" font-size :11px;" >&nbsp;</th>
                                                                                </tr>
                                                                            </table>
                                                                            </div>
                                                                        </div>
                                                                        <div id="divSelfAssessment" runat="server" style="overflow-x: hidden; overflow-y: scroll;
                                                                            width: 100%; height: 300px; border: solid 1px Gray;" onscroll="SetScrollPos(this)">
                                                                            <table width="100%" cellspacing="0" cellpadding="1" border="1" rules="all" style="border-collapse: collapse;">
                                                                                <colgroup>
                                                                                    <col width="50px;" />
                                                                                    <col width="50px;" />
                                                                                    <col width="50px;" />
                                                                                    <col />
                                                                                    <%--<col width="80px" />--%>
                                                                                    <col width="25px;" /> 
                                                                                </colgroup>
                                                                                <asp:Repeater runat="server" ID="rptSelfAssessment">
                                                                                    <ItemTemplate>
                                                                                        <tr class='<%# (Common.CastAsInt32(Eval("QID"))==QidSelfAssesment)?"selectedrow":"row"%>'>
                                                                                            <td style="text-align: center; font-size: 11px;">
                                                                                                <asp:ImageButton ID="imgEditSelfAppraisal" runat="server" OnClick="imgEditSelfAppraisal_OnClick"
                                                                                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" CausesValidation="false" />
                                                                                                <asp:HiddenField ID="hfQuestionID" runat="server" Value='<%#Eval("QID")%>' />
                                                                                                <asp:HiddenField ID="hfCategory" runat="server" Value='<%#Eval("QCat")%>' />
                                                                                            </td>
                                                                                            <td style="text-align: center; font-size: 11px;">
                                                                                                <asp:ImageButton ID="imgDeleteSelfAppraisal" runat="server" OnClick="imgDeleteSelfAppraisal_OnClick" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="return confirm('Are you sure to delete ?');" CausesValidation="false" Visible='<%#Common.CastAsInt32(Session["loginid"])==1%>' />
                                                                                            </td>
                                                                                            <td style="text-align: center; font-size: 11px;">
                                                                                                <%#Eval("SR")%>
                                                                                            </td>
                                                                                            <td style="text-align: left; font-size: 11px;">
                                                                                                <asp:Label ID="lblQuestion" runat="server" Text='<%#Eval("Question")%>'></asp:Label>
                                                                                            </td>

                                                                                            <%--<td style=" text-align :left;font-size :11px;">&nbsp;<%#Eval("Status1")%></td>--%>
                                                                                            
                                                                                           <td>&nbsp;</td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </table>
                                                                        </div>
                                                                        <div id="tblSelfAssessment" runat="server" visible="false">
                                                                            <table cellpadding="1" cellspacing="1" border="0" width="100%">
                                                                                <col width="110px" />
                                                                                <col />
                                                
                                                                                <tr>
                                                                                    <td style="text-align: right; font-weight: bold;">
                                                                                        Question :
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:TextBox ID="txtQuestionAPP" runat="server" Width="95%" MaxLength="200" ></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                                                            ControlToValidate="txtQuestionAPP"></asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                        <div style="float: right; padding: 2px;height:30px;">
                                                                            <asp:Label ID="lblMsgSelfAppraisal" runat="server" Style="color: Red;"></asp:Label>
                                                                            <asp:Button ID="btnAddSelfAssessment" runat="server" Text=" Add New" CausesValidation="false" Height="25px"
                                                                                OnClick="btnAddSelfAssessment_OnClick" CssClass="btn" Width="100px" />
                                                                            <asp:Button ID="btnSaveAppraisalMaster" runat="server" Text="  Save  " OnClick="btnSaveAppraisalMaster_OnClick"
                                                                                CssClass="btn" Visible="false" Width="100px" Height="25px" />
                                                                            <asp:Button ID="btnCancel" runat="server" Text=" Cancel " CausesValidation="false"
                                                                                OnClick="btnCancel_OnClick" CssClass="btn" Visible="false" Width="100px" Height="25px" />
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                             <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="Competency">
                                                <ContentTemplate>
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                                        <ContentTemplate>
                                                        <table cellpadding="1" cellspacing="1" border="0" width="100%">
                                                            <tr>
                                                                <td style="text-align: right; font-weight: bold; width: 100px;">
                                                                    Peap Level :&nbsp;
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:DropDownList ID="ddlCategory1" runat="server" AutoPostBack="True" Height="20px" OnSelectedIndexChanged="ddlCategory1_SelectedIndexChanged"></asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                            <table cellpadding="2" cellspacing="2" border="0" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <div style="overflow-x: hidden; overflow-y: hidden; width: 100%;">
                                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                        <tr>
                                                                        <td>
                                                                        <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                                                                     
                                                                         <table width="100%" cellspacing="0" cellpadding="1" border="1" rules="all" style="border-collapse: collapse; height:26px;">
                                                                                <colgroup>
                                                                                    <col width="50px;" />
                                                                                    <col width="50px;" />
                                                                                    <col width="50px;" />
                                                                                    <col />
                                                                                    <col width="25px;" />
                                                                                </colgroup>
                                                                                <tr align="left" class="blueheader">
                                                                                    <th scope="col" style="font-size: 11px;">
                                                                                        Edit
                                                                                    </th>
                                                                                    <th scope="col" style="font-size: 11px;">
                                                                                        Delete
                                                                                    </th>
                                                                                    <th scope="col" style="font-size: 11px;">
                                                                                        Sr No.
                                                                                    </th>
                                                                                    <th scope="col" style="font-size: 11px;">
                                                                                        Competency
                                                                                    </th>
                                                                                    <th scope="col" style="font-size: 11px;">
                                                                                        &nbsp;
                                                                                    </th>
                                                                                </tr>
                                                                            </table>
                                                                            </div>

                                                                             <div id="divCompetency" runat="server" style="overflow-x: hidden; overflow-y: scroll;
                                                                            width: 100%; height: 375px; border: solid 1px Gray;" onscroll="SetScrollPos(this)">
                                                                            <table width="100%" cellspacing="0" cellpadding="1" border="1" rules="all" style="border-collapse: collapse;">
                                                                                <colgroup>
                                                                                    <col width="50px;" />
                                                                                    <col width="50px;" />
                                                                                    <col width="50px;" />
                                                                                    <col />
                                                                                    <col width="25px;" />
                                                                                </colgroup>
                                                                                <asp:Repeater runat="server" ID="rptCompetency">
                                                                                    <ItemTemplate>
                                                                                        <tr class='<%# (Common.CastAsInt32(Eval("CID"))==CID)?"selectedrow":"row"%>'>
                                                                                            <td style="text-align: center; font-size: 11px;">
                                                                                                <asp:ImageButton ID="imgEditCompetency" runat="server" OnClick="imgEditCompetency_OnClick" ImageUrl="~/Modules/HRD/Images/edit.jpg" CausesValidation="false" />
                                                                                                <asp:HiddenField ID="hfCID" runat="server" Value='<%#Eval("CID")%>' />
                                                                                                <asp:HiddenField ID="hfCategory" runat="server" Value='<%#Eval("QCAT")%>' />
                                                                                                <asp:HiddenField ID="hfDescr" runat="server" Value='<%#Eval("Descr")%>' />
                                                                                            </td>
                                                                                            <td style="text-align: center; font-size: 11px;"><asp:ImageButton ID="imgDeleteCompetency" runat="server" OnClick="imgDeleteCompetency_OnClick" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="return confirm('Are you sure to delete ?');" CausesValidation="false" Visible='<%#Common.CastAsInt32(Session["loginid"])==1%>'/></td>
                                                                                            <td style="text-align: center; font-size: 11px;"><%#Eval("SR")%></td>
                                                                                            <td style="text-align: left; font-size: 11px;"><asp:Label ID="lblCompetency" runat="server" Text='<%#Eval("Competency")%>'></asp:Label></td>
                                                                                            <td>&nbsp;</td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </table>
                                                                            </div>
                                                                        </td>
                                                                        <td style="width:400px">
                                                                         <div id="tblCompetency" runat="server" visible="false">
                                                                            <table cellpadding="4" cellspacing="1" border="0" width="100%">
                                                                                <tr>
                                                                                    <td><b>Competency :</b></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="text-align: left;" colspan="5">
                                                                                        <asp:TextBox ID="txtCompetency" TextMode="MultiLine" runat="server" Width="99%"  Height="40px" ></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ControlToValidate="txtCompetency"  ></asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td><b>Description :</b></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="text-align: left;" colspan="5">
                                                                                        <asp:TextBox ID="txtCompDescr" TextMode="MultiLine" runat="server" Width="99%" Height="250px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                        <div style="padding: 2px; text-align:right;">
                                                                            <asp:Label ID="lblMsgCP" runat="server" Style="color: Red;"></asp:Label>
                                                                            <asp:Button ID="btnAddCP" runat="server" Text=" Add New" OnClick="btnAddCP_OnClick" CssClass="btn" Width="100px" CausesValidation="false" />
                                                                            <asp:Button ID="btnSaveCP" runat="server" Text="  Save  " OnClick="btnSaveCP_OnClick" CssClass="btn" Visible="false" Width="100px" />
                                                                            <asp:Button ID="btnCancelCP" runat="server" Text=" Cancel " OnClick="btnCancelCP_OnClick" CssClass="btn" Visible="false" Width="100px" CausesValidation="false" />
                                                                        </div>
                                                                        </td>
                                                                        </tr>
                                                                        </table>
                                                                        
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                            <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="KRA">
                                                <ContentTemplate>
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                                        <ContentTemplate>
                                                         <table cellpadding="1" cellspacing="1" border="0" width="100%">
                                                            <tr>
                                                                <td style="text-align: right; font-weight: bold; width: 100px;">
                                                                    Peap Level :&nbsp;
                                                                </td>
                                                                <td style="text-align: left; width:300px">
                                                                    <asp:DropDownList ID="ddlCategory2" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="ddlCategory2_SelectedIndexChanged" Width="280px"></asp:DropDownList>
                                                                </td>
                                                               <td style="text-align: right; font-weight: bold; width: 150px;">
                                                                    Position Group :&nbsp;
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:DropDownList ID="ddlPositionJR" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="ddlPositionJR_SelectedIndexChanged"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="ddlPositionJR" ValidationGroup="v11"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                            <table cellpadding="2" cellspacing="2" border="0" width="100%">
                                                             <tr >
                                                             <td>
                                                            <table cellpadding="2" cellspacing="2" border="0" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <div style="overflow-x: hidden; overflow-y: hidden; width: 100%;">
                                                                        <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                                                                            <table width="100%" cellspacing="0" cellpadding="1" border="1" rules="all" style="border-collapse: collapse; height:26px;">
                                                                                <colgroup>
                                                                                    <col width="50px;" />
                                                                                    <col width="50px;" />
                                                                                    <col width="50px;" />
                                                                                    <col />
                                                                                    <col width="70px;" />
                                                                                    <col width="200px;" />
<%--                                                                                    <col width="200px;" />--%>
                                                                                   <%-- <col width="80px;" />--%>
                                                                                    <col width="120px;" />
                                                                                    <col width="25px;"
                                                                                </colgroup>
                                                                                <tr align="left" class="blueheader">
                                                                                    <th scope="col" style="font-size: 11px;">
                                                                                        Edit
                                                                                    </th>
                                                                                    <th scope="col" style="font-size: 11px;">
                                                                                        Delete
                                                                                    </th>
                                                                                    <th scope="col" style="font-size: 11px;">
                                                                                        Sr No.
                                                                                    </th>
                                                                                    <th scope="col" style="font-size: 11px; text-align:left">
                                                                                        KRA Name
                                                                                    </th>
                                                                                    <th scope="col" style="font-size: 11px;">
                                                                                        weightage
                                                                                    </th>
                                                                                    <th scope="col" style="font-size: 11px;">
                                                                                        Category
                                                                                    </th>
                                                                                   <%-- <th scope="col" style="font-size: 11px;">
                                                                                        Position
                                                                                    </th>--%>
                                                                                   <%-- <th scope="col" style="font-size: 11px;">
                                                                                        Status
                                                                                    </th>--%>
                                                                                    <th scope="col" style="font-size: 11px;">
                                                                                        
                                                                                    </th>
                                                                                    <th scope="col">&nbsp;</th>
                                                                                </tr>
                                                                            </table>
                                                                            </div>
                                                                        </div>
                                                                        <div id="DivJobResponsibility" runat="server" style="overflow-x: hidden; overflow-y: scroll;
                                                                            width: 100%; height: 230px; border: solid 1px Gray;" onscroll="SetScrollPos(this)">
                                                                            <table width="100%" cellspacing="0" cellpadding="1" border="1" rules="all" style="border-collapse: collapse;">
                                                                                <colgroup>
                                                                                    <col width="50px;" />
                                                                                    <col width="50px;" />
                                                                                    <col width="50px;" />
                                                                                    <col />
                                                                                    <col width="70px;" />
                                                                                    <col width="200px;" />
<%--                                                                                    <col width="200px;" />--%>
                                                                                   <%-- <col width="80px;" />--%>
                                                                                    <col width="120px;" />
                                                                                    <col width="25px;" />
                                                                                </colgroup>
                                                                                <asp:Repeater runat="server" ID="rptJobResponsibility">
                                                                                    <ItemTemplate>
                                                                                        <tr class='<%# (Common.CastAsInt32(Eval("JSID"))==JRID)?"selectedrow":"row"%>'>
                                                                                            <td style="text-align: center; font-size: 11px;">
                                                                                                <asp:ImageButton ID="imgEditJobResponsibility" runat="server" OnClick="imgEditJobResponsibility_OnClick"
                                                                                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" CausesValidation="false" />
                                                                                                <asp:HiddenField ID="hfJSID" runat="server" Value='<%#Eval("JSID")%>' />
                                                                                                <asp:HiddenField ID="hfCategory" runat="server" Value='<%#Eval("QCAT")%>' />
                                                                                                <asp:HiddenField ID="ddlPosition" runat="server" Value='<%#Eval("PositionID")%>' />
                                                                                                <asp:HiddenField ID="hfdKRA" runat="server" Value='<%#Eval("kra_groupid")%>' />
                                                                                                
                                                                                            </td>
                                                                                            <td style="text-align: center; font-size: 11px;">
                                                                                                <asp:ImageButton ID="imgDeleteJobResponsibility" runat="server" OnClick="imgDeleteJobResponsibility_OnClick"
                                                                                                    ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="return confirm('Are you sure to delete ?');"
                                                                                                    CausesValidation="false"  Visible='<%#Common.CastAsInt32(Session["loginid"])==1%>'/>
                                                                                            </td>
                                                                                            <td style="text-align: center; font-size: 11px;">
                                                                                                <%#Eval("SR")%>
                                                                                            </td>
                                                                                            <td style="text-align: left; font-size: 11px;">
                                                                                                <asp:Label ID="lblJobResponsibility" runat="server" Text='<%#Eval("JobResponsibility")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td style="text-align: center; font-size: 11px;">
                                                                                                <asp:Label ID="lblWaitage" runat="server" Text='<%#Eval("Waitage").ToString() == "" ? "" : Eval("Waitage").ToString() + "%"%>'></asp:Label>                                                                                                
                                                                                            </td>
                                                                                            <td style="text-align: left; font-size: 11px;">
                                                                                                <asp:Label ID="lblKRAGroup" runat="server" Text='<%#Eval("KRA_Groupname")%>'></asp:Label>
                                                                                            </td>
                                                                                           <%-- <td style="text-align: left; font-size: 11px;">
                                                                                                &nbsp;<%#Eval("PositionName")%>
                                                                                            </td>--%>
                                                                                            
                                                                                            <%--<td style="text-align: left; font-size: 11px;">
                                                                                                &nbsp;<%#Eval("Status1")%>
                                                                                            </td>--%>

                                                                                            <td style="text-align: left; font-size: 11px;">
                                                                                                <asp:Button ID="btnAssignKPI" Text="Assign KPI" CommandArgument='<%#Eval("JSID")%>' OnClick="btnAssignKPI_Click" CausesValidation="false" Font-Bold="true" CssClass='<%#Eval("JobResponsibility")%>' Width="100px" runat="server" />
                                                                                            </td>
                                                                                            <td>&nbsp;</td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </table>
                                                                        </div>
                                                                        <div id="tblJobResponsibility" runat="server" visible="false">
                                                                            <table cellpadding="1" cellspacing="1" border="0" width="100%">
                                                                                <col width="130px" />
                                                                                <col />
                                                                                <col width="130px" />
                                                                                <tr>
                                                                                    <td style="text-align: right; font-weight: bold;">
                                                                                        KRA Name :
                                                                                    </td>
                                                                                    <td style="text-align: left;" colspan="3">
                                                                                        <asp:TextBox ID="txtJobResponsibility" TextMode="MultiLine" runat="server" Width="99%"  ValidationGroup="v11"
                                                                                            Height="70px"  ></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                                                            ControlToValidate="txtJobResponsibility"></asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                     <td style="text-align: right; font-weight: bold;">
                                                                                        weightage(%) :
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:TextBox ID="txtWaitage" onkeypress="fncInputIntegerValuesOnly(event)" MaxLength="3"  runat="server"   ValidationGroup="v11"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                                                                            ControlToValidate="txtWaitage"></asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                     <td style="text-align: right; font-weight: bold;">
                                                                                        Select Category :
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:DropDownList runat="server" ID="ddlKRACat"></asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="ddlKRACat"></asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                        <div style="float: right; padding: 2px;">
                                                                            <asp:Label ID="lblMsgJR" runat="server" Style="color: Red;"></asp:Label>
                                                                            <asp:Button ID="btnAddJR" runat="server" Text=" Add New" OnClick="btnAddJR_OnClick"
                                                                                CausesValidation="false" CssClass="btn" Width="100px" />
                                                                            <asp:Button ID="btnSaveJR" runat="server" Text="  Save  " OnClick="btnSaveJR_OnClick"  ValidationGroup="v11"
                                                                                CssClass="btn" Visible="false" Width="100px" />
                                                                            <asp:Button ID="btnCancelJR" runat="server" Text=" Cancel " OnClick="btnCancelJR_OnClick"
                                                                                CssClass="btn" Visible="false" Width="100px" CausesValidation="false" />
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            </td>
                                                                </tr>
                                                                </table>
                                                           <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dvAssignKPI" visible="false" >
                                                            <center>
                                                            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                                                            <div style="position :relative; width:850px; height:430px; text-align :center; border :solid 5px #4371a5; background : white; z-index:150;top:30px; opacity:1;filter:alpha(opacity=100)">
                                                                <asp:UpdatePanel runat="server" ID="UpKPI">
                                                                <ContentTemplate>
                                                                <center>
                                                                <table cellpadding="1" cellspacing="0" border="0" width="100%;">
                                                                    <tr>
                                                                        <td colspan="2" style="background-color:#d2d2d2; padding:4px; text-align:center"><b> Assign KPI to KRA</b></td>
                                                                    </tr>
                                                                    <tr>               
                                                                        <td style="text-align:right; width:120px;"><b>KPI Name</b></td>         
                                                                        <td>
                                                                            :<asp:TextBox ID="txtKPIName"  MaxLength="500"  runat="server"  Width="450px"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ControlToValidate="txtKPIName"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align:right; "><b>Description</b></td>         
                                                                        <td style="text-align:left;" >
                                                                            :<asp:TextBox ID="txtKPIValue" MaxLength="500"  runat="server"  Width="450px"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" ControlToValidate="txtKPIValue"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align:right;  "><b>KPI Linked</b></td>         
                                                                        <td style="text-align:left;" >
                                                                              :<asp:TextBox ID="txtlinkkpiname" runat="server" Width="450px"></asp:TextBox>
                                                                            <asp:ImageButton runat="server" ID="btnSelectKPI" OnClick="btnSelectKPI_Click" ImageUrl="~/Modules/HRD/Images/magnifier.png" CausesValidation="false" />
                                                                            <asp:HiddenField ID="hfdlinkkpiid" runat="server" ></asp:HiddenField>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align:right; padding-right:170px; padding-bottom:5px;" colspan="2">
                                                                        <asp:Label ID="lblKPIMsg" runat="server" style="color:Red;"></asp:Label>&nbsp;
                                                                            <asp:Button ID="btnSaveKPI" runat="server" CssClass="btn" OnClick="btnSaveKPI_Click" Text="Save"  />
                                                                            <asp:Button ID="btnCancelKPI" runat="server" CssClass="btn"  OnClick="btnCancelKPI_Click"  Text="Cancel" CausesValidation="false" />
                                                                        </td>
                                                                    </tr>
                    
                                                                    <tr>
                                                                        <td style="text-align: left;"  colspan="2">
                                                                            <table width="100%" cellspacing="0" cellpadding="1" border="1" rules="all" style="border-collapse: collapse;">
                                                                                <colgroup>
                                                                                    <col width="25px;" />
                                                                                    <col width="25px;" />
                                                                                    <col />
                                                                                    <col width="250px;" />
                                                                                    <col width="250px;" />
                                                                                    <col width="17px;" />
                                                                                </colgroup>
                                                                                <tr align="left" class="blueheader">
                                                                                    <th scope="col" style="font-size: 11px;">
                                                                                    </th>
                                                                                    <th scope="col" style="font-size: 11px;">
                                                                                    </th>
                                                                                    <th scope="col" style="font-size: 11px; text-align:left;">
                                                                                        KPI Name
                                                                                    </th>
                                                                                    <th scope="col" style="font-size: 11px; text-align:left;">
                                                                                        Description
                                                                                    </th>
                                                                                    <th scope="col" style="font-size: 11px; text-align:left;">
                                                                                        KPI Key
                                                                                    </th>
                                                                                    <th scope="col" style="font-size: 11px;">
                                                                                    </th>
                                                                                </tr>
                                                                            </table>
                                                                            <div id="Div1" runat="server" style="overflow-x: hidden; overflow-y: scroll; width: 100%;height: 265px; border: solid 1px Gray;" onscroll="SetScrollPos(this)">
                                                                                <table width="100%" cellspacing="0" cellpadding="1" border="1" rules="all" style="border-collapse: collapse;">
                                                                                    <colgroup>
                                                                                        <col width="25px;" />
                                                                                        <col width="25px;" />
                                                                                        <col />
                                                                                        <col width="250px;" />
                                                                                        <col width="250px;" />
                                                                                        <col width="17px;" />
                                                                                    </colgroup>
                                                                                    <asp:Repeater runat="server" ID="rptKPI">
                                                                                        <ItemTemplate>
                                                                                            <tr class='<%# (Common.CastAsInt32(Eval("KPIId"))==KPIId)?"selectedrow":"row"%>'>
                                                                                                <td style="text-align: center; font-size: 11px;">
                                                                                                    <asp:ImageButton ID="imgEditKPI" runat="server" OnClick="imgEditKPI_Click" CommandArgument='<%#Eval("KPIId")%>' ImageUrl="~/Modules/HRD/Images/edit.jpg" CausesValidation="false" />
                                                                                                    <asp:HiddenField ID="hfJSID" runat="server" Value='<%#Eval("KPIId")%>' />                                                    

                                                                                                </td>
                                                                                                <td style="text-align: center; font-size: 11px;">
                                                                                                    <asp:ImageButton ID="imgDeleteKPI" runat="server" OnClick="imgDeleteKPI_Click" CommandArgument='<%#Eval("KPIId")%>' ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="return confirm('Are you sure to delete ?');" CausesValidation="false" />
                                                                                                </td>
                                                                                                <td style="text-align: left; font-size: 11px;">
                                                                                                    <asp:Label ID="lblKPIName" runat="server" Text='<%#Eval("KPIName")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td style="text-align: left; font-size: 11px;">
                                                                                                    <asp:Label ID="lblKPIValue" runat="server" Text='<%#Eval("KPIValue")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td style="text-align: left; font-size: 11px;">
                                                                                                    <asp:Label ID="lblLinkKPIName" runat="server" Text='<%#Eval("linkname")%>'></asp:Label>
                                                                                                    <asp:HiddenField ID="hfdLinkKPIId" runat="server" Value='<%#Eval("KPIKey")%>' />                                                    
                                                                                                </td>  
                                                                                                <td></td>                                               
                                                                                                <%-- <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>--%>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </table>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align:center"  colspan="2">
                                                                            <asp:Button ID="btnCloseKPI" runat="server" CssClass="btn"  OnClick="btnCloseKPI_Click"  style="background-color:Red; color:#fff; height:25px; width:100px;" Text="Close" CausesValidation="false" />
                                                                        </td>
                                                                    </tr>
                    
                                                                </table>
                                                                </center>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:PostBackTrigger ControlID="btnCloseKPI" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </div> 
                                                            </center>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                        </ajaxToolkit:TabContainer>
                                    </div>
                                </td>
                            </tr>
                        </table>                        
                     </td>
                </tr>
            </table>       
   </asp:Content>
