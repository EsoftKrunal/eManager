<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddStorePR.ascx.cs" Inherits="UserControls_AddStorePR" %>
<%@ Register Src="~/Modules/Purchase/UserControls/AddRequisitionTypes.ascx" TagName="Requisition" TagPrefix="uc4" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" >
    function FillAllTextBox() 
    {

        var chkall = document.getElementById('ucStore_chkAll');
        var FirstBoxes = document.getElementById("ucStore_rptStoresData_ctl00_txttargetCompDate"); //   TargetDate
        var HFTotalGrdRow = document.getElementById('ucStore_HFTotalGrdRow');
        
       if (chkall.checked) {

           for (i = 0; i < (HFTotalGrdRow.value); i++) 
           {
               
               if (i <= 9) 
               {
                   document.getElementById("ucStore_rptStoresData_ctl0" + i + "_txtTargetCompDate").value = FirstBoxes.value;
               }
               else
               {
                   document.getElementById("ucStore_rptStoresData_ctl" + i + "_txtTargetCompDate").value = FirstBoxes.value;
               }
           }
        }
        else
         {
             for (i = 0; i < (HFTotalGrdRow.value); i++)
              {
                 if (i <= 9) {
                     document.getElementById("ucStore_rptStoresData_ctl0" + i + "_txtTargetCompDate").value = "";
                 }
                 else {
                     document.getElementById("ucStore_rptStoresData_ctl" + i + "_txtTargetCompDate").value = "";
                 }
             }    
        }
    }
    function Txt_click(obj) 
    {
        //alert(window.clipboardData);
       // obj.value ="5 april";
    }
//    function PringPR() 
//    {
//        window.open("Print.aspx?PRID=<%=PRId  %>&PRType=1");
//    }
</script>
 <script type="text/javascript">
     function OpenDocument(TableID, PoId, VesselCode) {
        // alert(VesselCode);
         window.open("ShowDocuments.aspx?DocId=" + TableID + "&PoId=" + PoId + "&VesselCode=" + VesselCode +"&PRType=SS");
     }
 </script>

<style type="text/css">
    .auto-style1 {
        width: 100%;
    }
</style>

<table cellpadding="0" cellspacing="0" width="100%" border="0" style="font-family:Arial;">
    <tr>
        <td valign="top">
            <table cellpadding="2" cellspacing="0" style="width: 100%" border="1">
                <tr>
                    <td colspan="2">
                        
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 22%" valign="top">
                        <table border="0" style="border: solid 2px #4371a5;" cellpadding="2" cellspacing="0"
                            width="100%">
                            <tr>
                                <td colspan="2" class="blueheader">
                                    Stores Requisition
                                    <asp:HiddenField ID="HFTotalGrdRow" runat="server" />
                                    <asp:HiddenField ID="hfPRID" runat="server" Value="0" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; padding-right: 3px;" valign="top" >
                                    Requisition Title :
                                </td>
                                <td style="text-align: left; padding-bottom: 2px;" valign="top">
                                    <asp:TextBox ID="txtReqTitle" MaxLength="100" runat="server" CssClass="input_field" TextMode="MultiLine" Width="200px" Height="40px" ></asp:TextBox>
                                </td>
                            </tr>
                            
                            <tr>
                                <td style="text-align: left; padding-right: 3px;" valign="top">
                                    Vessel :
                                </td>
                                <td style="text-align: left; padding-bottom: 2px;">
                                    <asp:DropDownList ID="ddlvessel" runat="server" Width="174px" OnSelectedIndexChanged="ddlvessel_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            
                            <tr>
                                <td style="text-align: left; padding-right: 3px;" valign="top">
                                    PR#:
                                </td>
                                <td style="text-align: left; padding-bottom: 1px;">
                                    <asp:TextBox ID="txtprno" MaxLength="30" runat="server" CssClass="input_field" Width="170px" Enabled="false" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; padding-right: 3px;" valign="top">
                                    Date :
                                </td>
                                <td style="text-align: left; padding-bottom: 1px;">
                                    <asp:TextBox ID="txtDate" MaxLength="10" runat="server" Text="" 
                                        Width="170px" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; padding-right: 3px;" valign="top">
                                    Dept. :
                                </td>
                                <td style="text-align: left; padding-bottom: 1px;">
                                    <asp:DropDownList ID="ddldepartment" runat="server"  Width="174px" 
                                        onselectedindexchanged="ddldepartment_SelectedIndexChanged" AutoPostBack="true" >
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; padding-right: 3px;" valign="top">
                                    Acct. Code :
                                </td>
                                <td style="text-align: left; padding-bottom: 1px;">
                                    <asp:DropDownList ID="ddlAccountCode" runat="server" Width="174px" >
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; padding-right: 3px;" valign="top">
                                    Port :
                                </td>
                                <td style="text-align: left; padding-bottom: 1px;">
                                    <asp:TextBox ID="txtPort" MaxLength="50" runat="server" Text="" 
                                        Width="170px" onfocus="showCalendar('',this,this,'','holder1',5,22,1)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right: 3px; text-align: left" valign="top">
                                    ETA :
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtETA" MaxLength="10" runat="server"  Width="170px" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right: 3px; text-align: left" valign="top">
                                    Urgent :
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox ID="chkUrgent" runat="server"></asp:CheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right: 3px; text-align: left" valign="top">
                                    Fast Track :
                                </td>
                                <td style="text-align: left">
                                   <asp:CheckBox ID="chkFastTrack" runat="server"></asp:CheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right: 3px; text-align: left" valign="top">
                                    Safety Urgent :
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox ID="chkSafetyUrgent" runat="server"></asp:CheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; padding-right: 3px;" valign="top">
                                    Submitted By :
                                </td>
                                <td style="text-align: left;">
                                     <asp:TextBox ID="txtcreated" MaxLength="50" runat="server" Width="170px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; padding-right: 3px;" valign="top" colspan="2">
                                    Remarks for Purchase department :
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: left;">
                                    <asp:TextBox ID="txtsmdremarks" runat="server" Height="70px"
                                        MaxLength="500" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                </td>
                                </tr>
                            <tr>
                                <td colspan="2" style="text-align: left; padding-right: 3px;" valign="top">
                                    Attach File :
                                </td>
                                
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: left; padding-right: 3px;" valign="top">
                                    <asp:FileUpload ID="FU" runat="server" Width="80%" CssClass="input_box" /> &nbsp;&nbsp;
                                    
                                     <asp:Button ID="btnAddDoc" runat="server" CssClass="btn" Text="Upload" OnClick="btnAddDoc_Click" />
                                    &nbsp; &nbsp;
                                    <span>
                                         <asp:ImageButton id="ImgAttachment" runat="server" ImageUrl="../../HRD/Images/paperclip12.gif" onclick="ImgAttachment_Click" ToolTip="Click to view attached documents"/> 
                                    (<asp:Label ID="lblAttchmentCount" runat="server" Text="0"></asp:Label>) 
                                     </span>
                                   

                                    <%-- <a id="ImgAttachment" runat="server" onclick="ImgAttachment_Click" >
                                         <img src="../../HRD/Images/paperclip12.gif" style="border:none"  /></a>--%>
                                              
                                <%--  <asp:ImageButton ID="ImgAttachment" runat="server" Visible="false" ImageUrl="~/Modules/HRD/Images/paperclip12.gif" OnClick="ImgAttachment_Click" />--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td  style="width: 78%; padding-left: 5px;" valign="top">
                        <table cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td class="blueheader">
                                   List of Items
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; padding: 1px;">
                                    <asp:Button ID="btnaddnew" runat="server" CssClass="btn" Text="Add New" 
                                        Width="65px" onclick="btnaddnew_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 376px">
                                    <div style="width: 100%; height: 376px; overflow-x: scroll; overflow-y: scroll;" id="rptheader" runat="server">
                                        <table cellpadding="2" cellspacing="0" border="0" width="90%">
                                            <colgroup>
                                        <col width="5%" style="text-align:center;"/>
                                        <col width="45%" style="text-align:center;"/>
                                        <col width="15%" style="text-align:center;"/>
                                        <col width="10%" style="text-align:center;"/>
                                        <col width="10%" style="text-align:center;"/>
                                        <col width="10" style="text-align:center;"/>
                                               
                                        <%--<col width="10" style="text-align:center;"/>--%>
                                        <col width="5%" style="text-align:center;"/>
                                                 </colgroup>
                                            <tr class= "headerstylegrid">
                                                <td class="blueheader" align="left">
                                                    S. No. 
                                                </td>
                                                <td class="blueheader"  >
                                                    Item Desc.
                                                </td>
                                                <td class="blueheader" >
                                                    Qty
                                                </td>
                                                <td class="blueheader" >
                                                    UOM
                                                </td>
                                                <td class="blueheader" >
                                                    ROB
                                                </td>
                                                <td class="blueheader" >
                                                    ISSA/IMPA
                                                </td>
                                                <%--<td class="blueheader" >
                                                    Date <asp:CheckBox ID="chkAll" runat="server" onclick="FillAllTextBox()"/>
                                                </td>--%>
                                                <td class="blueheader" >
                                                    Del.
                                                </td>
                                            </tr>
                                            
                                            <asp:Repeater runat="Server" ID="rptStoresData" OnItemDataBound="rptStoresData_OnItemDataBound">
                                                <ItemTemplate>
                                                    <tr style="padding-top:1px;" align="center" >
                                                        <td>
                                                            <asp:Label ID="lblRowNumber" runat="server"  Text="1"></asp:Label>
                                                            <asp:HiddenField ID="hfRecID" runat="server" Value='<%#Eval("RecID") %>' />
                                                            <asp:HiddenField ID="hdnPOStatusId" runat="server" Value='<%#Eval("POStatusId") %>' />
                                                        </td>
                                                        <td >
                                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDesc" runat="server" Text='<%#Eval("Description") %>'  TextMode="MultiLine" Width="400px" Height="40px"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td >
                                                            <asp:TextBox ID="txtQuantity" runat="server"  MaxLength="10"
                                                                Width="70px" Text='<%#Eval("Qty") %>' onkeypress='fncInputNumericValuesOnly(event)' style="text-align:right;" ></asp:TextBox>
                                                        </td>
                                                        <td > 
                                                              <asp:DropDownList ID="ddlUOM" runat="server"  DataSource='<%# BindUOM() %>' DataTextField="UOM" DataValueField="UOM"  Width="90px"> </asp:DropDownList>
                                                        </td>
                                                        <td >
                                                            <asp:TextBox ID="txtROB" runat="server"  MaxLength="10" Width="70px" Text='<%#Eval("QtyOB") %>' style="text-align:right;" onkeypress='fncInputNumericValuesOnly(event);AddStoreRows();' ></asp:TextBox>
                                                        </td>
                                                        
                                                        <td >
                                                            <asp:TextBox ID="txtISSAIMPA" runat="server" Text='<%#Eval("PARTNO") %>'    MaxLength="50"></asp:TextBox>
                                                        </td>
                                                        <%--<td>
                                                            <asp:TextBox ID="txttargetCompDate" runat="server" Text='<%#Eval("targetCompDate1") %>' Width="70px"  onkeypress="AddStoreRows();"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttargetCompDate"></asp:CalendarExtender>
                                                        </td>--%>
                                                        <td  >
                                                            <asp:ImageButton ID="btnDelete" runat="server" ToolTip="Delete" CausesValidation="False"
                                                                 ImageUrl="~/Modules/HRD/Images/Delete.jpg" onkeypress="DeleteStoreRows(this);" OnClientClick="javascript:return window.confirm('Are you Sure to Delete?');"  OnClick="btnDelete_OnClick" />
                                                            <asp:Label ID="lblSrNo" Style="font-size: smaller; color: Maroon; display: none;"
                                                                runat="server"></asp:Label>
                                                           
                                                        </td>
                                                    </tr>
                                                    <%--<tr style="display: none;">
                                                        <td  class="blue_heading" style="font-weight: normal; font-size: 9px;">
                                                            Item Rem.
                                                        </td>
                                                        <td colspan="8" align="left" width="95%">
                                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="input_field" MaxLength="499"
                                                                Width="900px" ></asp:TextBox>
                                                        </td>
                                                    </tr>--%>
                                                </ItemTemplate>
                                            </asp:Repeater>
                            
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
    <tr>
        <td align="right" style="padding-right: 5px; padding-bottom: 5px; padding-top: 5px;
            padding-left: 5px;">
            <table cellpadding="0" cellspacing="0" class="auto-style1">
                <tr>
                    <td style="text-align: left;">
                        
                    </td>
                    <td style="text-align: right;">
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDate"></asp:CalendarExtender>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtETA"></asp:CalendarExtender>
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                        <asp:Button ID="imgSave" runat="server" OnClick="imgSave_OnClick" Width="70px" CssClass="btn" Text ="Save" /> &nbsp;
                        <asp:Button ID="imgCancel" runat="server"  OnClick="imgCancel_OnClick" Text ="Close" CssClass="btn" /> &nbsp;
                        <%--<asp:ImageButton ID="imgPrint" runat="server" ImageUrl="~/Images/print.jpg" /> --%>
                        <asp:Button ID="btnreload" runat="server" CssClass="input_field" Text="Reload" Style="display: none" Width="59px" />
                        
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;font-family:Arial;font-size:12px;" runat="server" id="divAttachment" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px;width:100%; height:100%; background-color :black;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:500px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px;  ;opacity:1;filter:alpha(opacity=100)">
        <center>
        <br />
        <div class="text headerband"> <b>Attached Documents</b>
            <asp:ImageButton ID="imgClosePopup" runat="server" ImageUrl="~/Modules/HRD/Images/Close.gif"  title="Close this Window."  style="float:right;"  CssClass="btn" OnClick="imgClosePopup_Click" />
        </div>
        <br /><br />
        <div style="overflow-y: scroll; overflow-x: scroll;height:150px;">

                                 
                               <table cellpadding="2" cellspacing="0" width="98%" style="margin:auto;" >
                                   <colgroup>
                                       <col width="50px" />
                                       <col />
                                       <col width="90px" />
                                       <tr class="headerstylegrid" style="font-weight:bold;">
                                           <td ></td>
                                           <td >File Name</td>
                                           <td >Attachment</td>
                                       </tr>
                                       <asp:Repeater ID="rptDocuments" runat="server">
                                           <ItemTemplate>
                                               <tr>
                                                  <td style="text-align:center;">
                                                       <asp:ImageButton ID="ImgDelete" runat="server" ImageUrl="~/Modules/HRD/Images/delete_12.gif" OnClick="ImgDelete_Click" CommandArgument='<%#Eval("DocId")%>' visible='<%#Common.CastAsInt32(Eval("StatusId")) == 0 %>' />
                                                  </td>
                                                   <td style="text-align:left;padding-left:5px;"><%#Eval("FileName")%>
                                                       <asp:HiddenField ID="hdnDocId" runat="server" Value='<%#Eval("DocId")%> ' />
                                                   </td>
                                                   <td style="text-align:center;"> 
                                                    <%--   <asp:ImageButton ID="ImgAttachment" runat="server" ImageUrl="~/Images/paperclip.gif" OnClick="ImgAttachment_Click" CommandName='<%#Eval("DocId")%> ' />--%>

                                                        <a onclick='OpenDocument(<%#Eval("DocId")%>,<%#Eval("RequisitionId")%>,"<%#Eval("VesselCode")%>")' style="cursor:pointer;">
                                                       <img src="../../HRD/Images/paperclip12.gif" />
                                                       </a>
                                                   </td>
                                               </tr>
                                           </ItemTemplate>
                                       </asp:Repeater>
                                   </colgroup>
                        </table>
                                     </div>
        <asp:Button ID="btnPopupAttachment" runat="server" CssClass="btn" onclick="btnPopupAttachment_Click" Text="Cancel" CausesValidation="false" Width="100px" />
         </center>
        </div> 
    </center>
    </div>
<script type="text/javascript" >
    
    var Vessel=document.getElementById("ucStore_ddlvessel");
    var vslNames=[<%=vesselList%>];
    Vessel.onchange=function()
    {
        Vessel.setAttribute("title",vslNames[Vessel.selectedIndex]);
    }
    </script> 
    
    <script type="text/javascript" >
//        var Account=document.getElementById("ucStore_ddlAccountCode"); 
//        var AccNames=[<%=AccountList%>];
//        Account.onchange=function()
//        {   
//            Account.setAttribute("title",AccNames[Account.selectedIndex]);
//        }
    </script> 