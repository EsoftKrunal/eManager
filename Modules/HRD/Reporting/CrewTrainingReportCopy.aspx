<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewTrainingReportCopy.aspx.cs" Inherits="Reporting_CRMReportCopy"  MasterPageFile="~/Modules/HRD/CrewTraining.master" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"/> 
   <%-- <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
   
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />--%>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
    <style type="text/css" >
    .fixedbar
    {
        position:fixed;
        margin:140px 0px 0px 140px;   
        background-color:#f0f0f0;  
        z-index:100;
        border:solid 1px #5c5c5c;
    }
    </style>
    <link rel="stylesheet" href="../../../css/app_style.css" />
     <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
     <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
<%--<form id="form1" runat="server" defaultbutton="btn_show">--%>
<div>
    
<%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
<table style="width :100%" cellpadding="0" cellspacing="0">
<tr>
<%--<td style="width:150px; text-align :left; vertical-align : top;">
<mtm:menu runat="server" ID="menu2" />  
</td>--%>
<td style=" text-align :left; vertical-align : top;" > 
<table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
       <tr>
        <td valign="top" >
            <asp:UpdatePanel ID="UP1" runat="server">
                <ContentTemplate>
                
             <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;text-align: center" width="100%">
             <%--<tr>
                <td align="center" style="width: 100%; background-color: #4371a5">Training Requirements & Tracking</td>
             </tr>--%>
             <tr>
                 <td style="text-align: center;"><asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label></td>
             </tr>
             <tr>
                 <td style="text-align: center; padding : 3px;">
                 <table width="100%" border="0" cellpadding="2" cellspacing="0" rules="none" >
                 <tr>
                 <td style="text-align: right">Training Location</td>
                 <td style="text-align: left">
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="120px" CssClass="required_box" >
                                    <asp:ListItem Text="< Select >" Value="" Selected="True"> </asp:ListItem>
                                    <asp:ListItem Text="Shipboard" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Shorebased" Value="0"></asp:ListItem>
                                 </asp:DropDownList>
                     </td>
                 <td style="text-align: right">Crew # :</td>
                 <td style="text-align: left">
                    <asp:TextBox ID="txt_MemberId_Search" runat="server" CssClass="input_box" MaxLength="6"
                         TabIndex="1" Width="60px"></asp:TextBox>
                 </td>
                 <td style="text-align: right">&nbsp;Vessel :</td>
                 <td style="text-align: left; width: 187px;">
                 <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="input_box" TabIndex="10" Width="167px"></asp:DropDownList>
                 </td>
                 
                 </tr>
                     <tr>
                        <td style="height: 19px;text-align:right;" >
                            &nbsp;Report Type :</td>
                         <td style="height: 19px;text-align:left;">
                         <asp:DropDownList ID="ddlFilterField" runat="server" CssClass="input_box" 
                         Width="150px">
                         <asp:ListItem Selected="True" Text="Due Training" Value="0"></asp:ListItem>
                         <asp:ListItem Text="Planned Training" Value="1"></asp:ListItem>
                         <asp:ListItem Text="Done Training" Value="2"></asp:ListItem>
                     </asp:DropDownList>
                             
                         </td>
                         
                         <td style="text-align: right; height: 19px;">
                         Crew Name :
                             </td>
                         <td style="text-align: left; height: 19px;">
                              <asp:TextBox ID="txtName" runat="server" CssClass="input_box" MaxLength="6" 
                                  TabIndex="1" Width="160px"></asp:TextBox>
                         </td>
                         <td style="text-align: right; height: 19px;">
                             
                        </td>
                         <td style="text-align: left; width: 250px; height: 19px;">
                            <asp:CheckBox ID="chkRec" runat="server" Text="Promotion Recommended" /> 
                            
                         </td>
                     </tr>
                     <tr>
                            
                        
                        <td style="text-align: right; ">
                            Nationality :
                        </td>
                        <td style="text-align: left; ">
                            <asp:DropDownList ID="ddl_Nationality" runat="server" CssClass="input_box" 
                                TabIndex="7" Width="167px">
                                <asp:ListItem Text="&lt; Select &gt;"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                         <td style="text-align: right; ">Rank :</td>
                         <td style="text-align: left; ">
                            <asp:DropDownList ID="ddlRank" runat="server" CssClass="input_box" Width="100px" ></asp:DropDownList>
                         </td>
                          <td colspan="2" style="text-align: center; ">
                             <table>
                                 <tr>
                                     <td>From : </td>
                                     <td> <asp:TextBox ID="txtFromDt" runat="server" Width="80px" CssClass="input_box" ></asp:TextBox> </td>
                                     <td>To :</td>
                                     <td><asp:TextBox ID="txtToDate" runat="server" Width="80px" CssClass="input_box" ></asp:TextBox></td>
                                 </tr>
                             </table>
                             
                            
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd-MMM-yyyy"
                           PopupButtonID="txtFromDt" PopupPosition="Left" 
                           TargetControlID="txtFromDt">
                           </ajaxToolkit:CalendarExtender>
                           
                           <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                               PopupButtonID="txtToDate" PopupPosition="Left" 
                               TargetControlID="txtToDate">
                           </ajaxToolkit:CalendarExtender>
                        </td>
                        
                     </tr>
                     <tr>
                        <td style="text-align: right; ">
                            Training :
                        </td>
                         <td colspan="" align="left" style="text-align:left">
                           <asp:TextBox ID="txtTraining" runat="server" CssClass="input_box" Width="150px"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
                                CompletionListCssClass="CList" 
                                CompletionListHighlightedItemCssClass="CListItemH" 
                                CompletionListItemCssClass="CListItem" DelimiterCharacters="" Enabled="True" 
                                MinimumPrefixLength="1" ServiceMethod="GetTrainingName" 
                                ServicePath="~/App_Code/CMS/WebService.cs" TargetControlID="txtTraining">
                            </ajaxToolkit:AutoCompleteExtender>
                         </td>
                        <td></td>
                        <td></td>
                        <td></td>
                         <td align="left">
                            <asp:Button ID="btn_show" runat="server" CommandArgument="Due" CssClass="btn" 
                                 OnClick="btn_show_Click" TabIndex="2" Text="Show Training" Width="100px" />
                             <asp:Button ID="btnClear" runat="server" CausesValidation="false" 
                                 CommandArgument="Due" CssClass="btn" OnClick="btnClear_Click" TabIndex="2" 
                                 Text=" Clear " Width="80px" />
                         </td>
                     </tr>
                 </table>
                 </td>
             </tr>
             <tr>
                 <td>
                 </td>
             </tr>
           <tr>
           <td style="text-align: left">
           <iframe runat="server" id="IFRAME1" frameborder="1" style="width: 100%; height:350px; overflow:auto"></iframe>
           </td>
          </tr>
         </table>
             
         </ContentTemplate>
            </asp:UpdatePanel>
        </td>
       </tr>
      </table>
</td> </tr> </table> 
</div>
<%--</form>--%>

</asp:Content>