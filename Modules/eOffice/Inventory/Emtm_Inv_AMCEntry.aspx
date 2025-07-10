<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Emtm_Inv_AMCEntry.aspx.cs" Inherits="emtm_Inventory_Emtm_Inv_AMCEntry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AMC</title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" >
         function fncInputNumericValuesOnly(evnt) {
             if (!(event.keyCode == 45 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57 || event.keyCode == 46)) {
                 event.returnValue = false;
             }
         }
         function RefreshParent()
         {
          window.opener.Refresh();
         }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <center>
            <table width="500px">
                    <tr>
                        <td valign="top" style="border:solid 1px #4371a5;" >
                        <table width="100%" cellpadding="0" cellspacing="0">
                         
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" width="500px" >
                                    <tr>
                                        <td class="dottedscrollbox" style=" text-align :center; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                                         <asp:Label ID="lblPageHeader" Text="Add /Edit Items - [ AMC ]" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr><td>
                                    <asp:Label ID="lblMsg" runat="server" Style="color: Red; font-size: 14px;"></asp:Label>
                                            </td></tr> 
                                    <tr>
                                        <td style="vertical-align:top; padding-top:5px;" >
                                            <table cellpadding="4" cellspacing="4" border="0" style="border-collapse: collapse;" width="800px;">
                                                <tr>
                                                    <td style="text-align: right;width :200px;">
                                                        MIN Category :&nbsp;
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:DropDownList ID="ddlMinCat" runat="server" ></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right;width :200px;">
                                                        CONTRACT# :&nbsp;
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:TextBox ID="txtContractNo" required="yes" MaxLength="100" runat="server"></asp:TextBox>
                                                    </td>
                                                    
                                                 </tr>
                                                <tr>
                                                    <td style="text-align: right;width :200px;">
                                                        Start Date :&nbsp;
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:TextBox ID="txtStartDt" MaxLength="12" runat="server" ></asp:TextBox>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif"
                                                                        CausesValidation="false" />
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                                                        PopupButtonID="ImageButton1" TargetControlID="txtStartDt" PopupPosition="TopRight">
                                                                    </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                    
                                                 </tr>
                                                 <tr>   
                                                    <td style="text-align: right">
                                                        End Date :&nbsp;
                                                    </td>
                                                    <td style="text-align: left">
                                                         <asp:TextBox ID="txtEndDt" MaxLength="12" runat="server" ></asp:TextBox>
                                                         <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif"
                                                                        CausesValidation="false" />
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy"
                                                                        PopupButtonID="ImageButton3" TargetControlID="txtEndDt" PopupPosition="TopRight">
                                                                    </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                   
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right">
                                                        Contract Vendor :&nbsp;
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:TextBox ID="txtConVendor" MaxLength="500" Width="300"  runat="server"></asp:TextBox>
                                                        <ajaxToolkit:AutoCompleteExtender id="AutoCompleteExtender1" runat="server" TargetControlID="txtConVendor" ServicePath="~/WebService.asmx" ServiceMethod="GetVendorAMC" MinimumPrefixLength="1" Enabled="True" DelimiterCharacters="" CompletionListCssClass="CList"  CompletionListItemCssClass="CListItem" CompletionListHighlightedItemCssClass="CListItemH"> </ajaxToolkit:AutoCompleteExtender>
                                                    </td>
                                                    
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right">
                                                        Support Contact Details :&nbsp;
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:TextBox ID="txtConDetails" Height="50" Width="300" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                        <ajaxToolkit:AutoCompleteExtender id="AutoCompleteExtender2" runat="server" TargetControlID="txtConDetails" ServicePath="~/WebService.asmx" ServiceMethod="GetContractDetailsAMC" MinimumPrefixLength="1" Enabled="True" DelimiterCharacters="" CompletionListCssClass="CList"  CompletionListItemCssClass="CListItem" CompletionListHighlightedItemCssClass="CListItemH"> </ajaxToolkit:AutoCompleteExtender>
                                                        
                                                    </td>
                                                    
                                                </tr>
                                                </table>
                                            <asp:UpdatePanel runat="server" ID="up1">
                                            <ContentTemplate>
                                            <table cellpadding="4" cellspacing="4" border="0" style="border-collapse: collapse;" width="800px;">
                                                
                                                            <tr>
                                                                <td style="text-align: right;width :200px;">
                                                                    Currency :&nbsp;
                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:DropDownList ID="ddlCurrency" runat="server" OnSelectedIndexChanged="txtPrice_TextChanged" AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="text-align: right;width :200px;">
                                                                    &nbsp;</td>
                                                                <td style="text-align: left">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right">
                                                                    Amount :&nbsp;
                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtPrice" runat="server" AutoPostBack="True" MaxLength="15" onkeypress="fncInputNumericValuesOnly(this)" OnTextChanged="txtPrice_TextChanged"></asp:TextBox>
                                                                </td>
                                                                <td style="text-align: right">
                                                                    &nbsp;</td>
                                                                <td style="text-align: left">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right">
                                                                    Amount (US$) :&nbsp;
                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lblTotalUSDoler" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: right">
                                                                    &nbsp;</td>
                                                                <td style="text-align: left">
                                                                    &nbsp;</td>
                                                            </tr>
                                            </table>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div style="width :500px; text-align :center; padding-left :50px;">
                                             <input id="btnCancel" runat="server" type="button" value="Cancel" onclick="window.close();"  />
                                             <asp:Button runat="server" ID="btnSave" Text ="Save" OnClick="btnSave_OnClick" />
                                             <br />   <br />
                                             </div>
                                             <div style="text-align :left; padding :10px;">
                                              <span style="font-weight:bold; font-size:13px;">Include Equipment : </span>
                                            <hr />
                                            <table cellpadding="0" cellspacing="0" style="background-color: #f9f9f9; padding-top:3px;" width="100%">
                                    <tr id="trEquipment" runat="server">
                                        <td>
                                             <table cellpadding="2" cellspacing="0" width="100%">
                                                 <tr>
                                                      <td style="text-align:right; vertical-align:middle; width:150px;">Select Equipment : 
                                                          &nbsp;</td>
                                                      <td style="text-align:left; vertical-align:middle;">
                                                         <asp:DropDownList ID="ddlEquipment" Width="250px" runat="server"></asp:DropDownList>
                                                          </td>
                                                      <td style="vertical-align:middle;width:100px;"><asp:Button ID="btnSaveEquip" Text="Save" 
                                                              CssClass="btn" runat="server" style=" float:left;" 
                                                              onclick="btnSaveEquip_Click" />
                                                      </td>
                                                      <td style="vertical-align:middle;width:200px;">
                                                      <asp:Label ID="lblEqpMsg" runat="server" Style="color: Red; font-size: 12px;float:right;"></asp:Label>
                                                      </td>
                                                 </tr>
                                             </table>
                                             <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                                            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width: 100%;border-collapse: collapse; height:26px;">
                                                <colgroup>
                                                       <%if (Request.QueryString["Mode"].ToString() != "View")
                                                         {%>
                                                        <col style="width: 30px;" />
                                                        <% }%>
                                                        <col  />
                                                        <col style="width: 150px;" />
                                                        <col style="width: 150px;" />
                                                        <col style="width: 150px;" />
                                                        <col style="width: 25px;" />
                                                     <tr align="left" class="blueheader">
                                                     <%if (Request.QueryString["Mode"].ToString() != "View")
                                                       {%>
                                                        <td></td>
                                                        <% }%>
                                                        <td>
                                                            Asset Code
                                                        </td>
                                                        <td>Model#</td>
                                                        <td>
                                                            Purchase Dt.
                                                        </td>
                                                        <td>Amount(USD)</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </colgroup>
                                            </table>
                                            </div>
                                            <div id="div1" class="scrollbox" style="overflow-y: scroll; overflow-x: hidden; width: 100%; height: 130px; text-align: center;">
                                                <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width: 100%;
                                                    border-collapse: collapse;">
                                                    <colgroup>
                                                    <%if (Request.QueryString["Mode"].ToString() != "View")
                                                      {%>
                                                        <col style="width: 30px;" />
                                                        <% }%>
                                                        <col  />
                                                        <col style="width: 150px;" />
                                                        <col style="width: 150px;" />
                                                        <col style="width: 150px;" />
                                                        <col style="width: 25px;" />
                                                    </colgroup>
                                                    <asp:Repeater ID="rptEqipDetails" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="row" >
                                                            <%if (Request.QueryString["Mode"].ToString() != "View")
                                                              {%>
                                                                <td>
                                                                   <asp:ImageButton ID="imgDelEqip" runat="server" CommandArgument='<%#Eval("Id")%>' ToolTip="Delete Equipment"
                                                                        ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="imgDelEqip_OnClick" OnClientClick="return confirm('Are you sure to delete ?')" />
                                                                </td>
                                                                <% }%>
                                                                <td align="left">
                                                                    <%#Eval("AssetCode")%>
                                                                </td>
                                                                <td>
                                                                   <%#Eval("ModelNumber")%>
                                                                </td>
                                                                <td>
                                                                   <%#Eval("PurchaseDate") %>
                                                                </td>
                                                                <td align="right">
                                                                   <%#Eval("Amount") %>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                            <br />
                                             </div>
                                            <div style="text-align :left; padding :10px;">
                                            <span style="font-weight:bold; font-size:13px;">Attachments : </span>
                                            <hr />
                                            <table cellpadding="0" cellspacing="0" style="background-color: #f9f9f9; padding-top:3px;" width="100%">
                                    <tr id="trAttachment" runat="server">
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                             <table cellpadding="2" cellspacing="0" width="100%">
                                                 <tr>
                                                      <td style="text-align:right; vertical-align:middle;">Description : 
                                                          &nbsp;</td>
                                                      <td style="text-align:left; vertical-align:middle;">
                                                          <asp:TextBox ID="txtAttachmentText" runat="server" MaxLength="50" 
                                                              Width="290px" ></asp:TextBox></td>
                                                      <td style="text-align:right; vertical-align:middle;">Attachment :&nbsp;</td>
                                                      <td style="text-align:left; vertical-align:middle;">
                                                      <asp:FileUpload ID="flAttachDocs" runat="server" />
                                                      </td>
                                                      <td style="vertical-align:middle;"><asp:Button ID="btnSaveAttachment" Text="Upload" onclick="btnSaveAttachment_Click" 
                                                              CssClass="btn" runat="server" style=" float:right;" />
                                                      </td>
                                                 </tr>
                                             </table>
                                             </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnSaveAttachment" />
                                                </Triggers>
                                           </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                                            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width: 100%;border-collapse: collapse; height:26px;">
                                                <colgroup>
                                                       <%if (Request.QueryString["Mode"].ToString() != "View")
                                                         {%>
                                                        <col style="width: 30px;" />
                                                        <% }%>
                                                        <col  />
                                                        <col style="width: 30px;" />
                                                        <col style="width: 25px;" />
                                                     <tr align="left" class="blueheader">
                                                     <%if (Request.QueryString["Mode"].ToString() != "View")
                                                       {%>
                                                        <td></td>
                                                        <% }%>
                                                        <td>
                                                            Description
                                                        </td>
                                                        <td></td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </colgroup>
                                            </table>
                                            </div>
                                            <div id="divAttachment" class="scrollbox" style="overflow-y: scroll; overflow-x: hidden; width: 100%; height: 130px; text-align: center;">
                                                <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width: 100%;
                                                    border-collapse: collapse;">
                                                    <colgroup>
                                                    <%if (Request.QueryString["Mode"].ToString() != "View")
                                                      {%>
                                                        <col style="width: 30px;" />
                                                        <% }%>
                                                        <col  />
                                                        <col style="width: 30px;" />
                                                        <col style="width: 25px;" />
                                                    </colgroup>
                                                    <asp:Repeater ID="rptAttachment" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="row" >
                                                            <%if (Request.QueryString["Mode"].ToString() != "View")
                                                              {%>
                                                                <td>
                                                                   <asp:ImageButton ID="imgDel" runat="server" CommandArgument='<%#Eval("FILEID")%>' ToolTip="Delete"
                                                                        ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="imgDel_OnClick" OnClientClick="return confirm('Are you sure to delete ?')" />
                                                                </td>
                                                                <% }%>
                                                                <td align="left">
                                                                    <%#Eval("ATTACHMENTTEXT")%>
                                                                </td>
                                                                <td>
                                                                   <a runat="server" ID="ancFile"  href='<%#("~/EMANAGERBLOB/Inventory/" + Eval("FILENAME").ToString()) %>' target="_blank" Visible='<%# Eval("FILENAME").ToString() != "" %>' title="Attachment" >
                                                                    <img src="../../Images/paperclip.gif" style="border:none"  />
                                                                   </a>
                                                                </td>
                                                               <td>&nbsp;</td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
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
    </center>
    </div>
    </form>
</body>
</html>
