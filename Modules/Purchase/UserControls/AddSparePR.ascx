    <%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddSparePR.ascx.cs" Inherits="UserControls_AddSparePR" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />

<script type="text/javascript" >
    function FillAllTextBox() 
    {

        var chkall = document.getElementById('ucSpare_chkAll');
        var FirstBoxes = document.getElementById("ucSpare_rptData_ctl00_txtTargetCompDate"); //   TargetDate
        var HFTotalGrdRow = document.getElementById('ucSpare_HFTotalGrdRow');
       if (chkall.checked) 
       {
           for (i = 0; i < (HFTotalGrdRow.value); i++) 
           {
               if (i <= 9) 
               {
                   document.getElementById("ucSpare_rptData_ctl0" + i + "_txtTargetCompDate").value = FirstBoxes.value;
               }
               else
               {
                   document.getElementById("ucSpare_rptData_ctl" + i + "_txtTargetCompDate").value = FirstBoxes.value;
               }
           }
        }
        else
         {
             for (i = 0; i < (HFTotalGrdRow.value); i++) {
                 if (i <= 9) {
                     document.getElementById("ucSpare_rptData_ctl0" + i + "_txtTargetCompDate").value = "";
                 }
                 else {
                     document.getElementById("ucSpare_rptData_ctl" + i + "_txtTargetCompDate").value = "";
                 }
             }    
        }
    }
    function Txt_click(obj) 
    {
        //alert(window.clipboardData);
       // obj.value ="5 april";
    }
    function PringPR() {
        window.open("Print.aspx?PRID=<%=PRId  %>&PRType=2");
    }
</script>
<script type="text/javascript">
    function OpenDocument(TableID, PoId, VesselCode) {
        // alert(VesselCode);
        window.open("ShowDocuments.aspx?DocId=" + TableID + "&PoId=" + PoId + "&VesselCode=" + VesselCode + "&PRType=SP");
    }
</script>
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%;font-family:Arial;">
<tr>
    <td colspan="2">
        
    </td>
</tr>
<tr>
        <td align="left" style="width: 22%; padding :3px;" valign="top">
            <table style="border:solid 1px #4371a5;" cellpadding="2" cellspacing="0" width="100%">
                <tr>
                <td colspan="2" class="blueheader"><strong>Spares Requisition</strong>
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
                            Vessel :</td>
                        <td style="text-align: left; padding-bottom: 2px;">
                            <asp:DropDownList ID="ddlvessel" runat="server" CssClass="dropdown_field" 
                                Width="174px" OnSelectedIndexChanged="ddlvessel_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList></td>
                    </tr>
                    <tr style="display:none;">
                        <td style="text-align: left;padding-right: 3px; " valign="top">
                            PRType :</td>
                        <td style="text-align: left; padding-bottom: 1px;">
                            <asp:DropDownList ID="ddlPRType" runat="server" CssClass="input_field" 
                                 Width="174px" Enabled="false">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: left; padding-right: 3px;" valign="top">
                            PR#:</td>
                        <td style="text-align: left; padding-bottom: 1px;">
                            <asp:TextBox ID="txtprno" maxlength="30" runat="server" CssClass="input_field" Width="170px"  Enabled="false" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: left; padding-right: 3px;" valign="top">
                            Date :
                        </td>
                        <td style="text-align: left; padding-bottom: 1px;">
                            <asp:TextBox ID="txtDate" maxlength="10" runat="server" Text="" CssClass="input_field" Width="170px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; padding-right: 3px;" valign="top">
                            Dept. :</td>
                        <td style="text-align: left; padding-bottom: 1px;">
                            <asp:DropDownList ID="ddldepartment" runat="server" CssClass="dropdown_field"
                                Width="174px" onselectedindexchanged="ddldepartment_SelectedIndexChanged"  AutoPostBack="true" >
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: left; padding-right: 3px;" valign="top">
                            Acct. Code :
                        </td>
                        <td style="text-align: left; padding-bottom: 1px;">
                            <asp:DropDownList ID="ddlAccountCode" runat="server" CssClass="dropdown_field" Width="174px"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; padding-right: 3px;" valign="top">
                            Port :
                        </td>
                        <td style="text-align: left; padding-bottom: 1px;">
                            <asp:TextBox ID="txtPort" maxlength="50" runat="server"  Text="" CssClass="input_field" Width="170px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="padding-right: 3px; text-align: left" valign="top">
                            ETA :</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtETA" maxlength="10" runat="server" CssClass="input_field" 
                                Width="170px"></asp:TextBox></td>
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
                            Submitted By :</td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtcreated" maxlength="50" runat="server" CssClass="input_field" Width="170px" ></asp:TextBox>
                            <%--<asp:TextBox ID="txtcreated" runat="server" CssClass="input_field" MaxLength="100"
                                TabIndex="3" Width="150px"></asp:TextBox>--%></td>
                    </tr>
                    <tr>
                        <td style="text-align: left; padding-right: 3px;" valign="top" colspan="2">
                            Remarks for Purchase department :</td>
                        
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: left;">
                            <asp:TextBox ID="txtsmdremarks" runat="server" CssClass="input_field" 
                                Height="60px" MaxLength="500"  TextMode="MultiLine" Width="300px"></asp:TextBox>
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
                                <%--  <asp:ImageButton ID="ImgAttachment" runat="server" Visible="false" ImageUrl="~/Modules/HRD/Images/paperclip12.gif" OnClick="ImgAttachment_Click" />--%>
                                    
                                     <asp:Button ID="btnAddDoc" runat="server" CssClass="btn" Text="Upload" OnClick="btnAddDoc_Click" />
                                    &nbsp; &nbsp;
                                    <span>
                                         <asp:ImageButton id="ImgAttachment" runat="server" ImageUrl="../../HRD/Images/paperclip12.gif" onclick="ImgAttachment_Click" ToolTip="Click to view attached documents"/> 
                                    (<asp:Label ID="lblAttchmentCount" runat="server" Text="0"></asp:Label>) 
                                     </span>
                                </td>
                            </tr>
                        <tr style="display:none;">
                            <td style="text-align: right; padding-right: 3px;" valign="top">
                                Crew# :</td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtcrewno" runat="server" CssClass="input_field" MaxLength="6" 
                                    Width="170px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td style="text-align: right; padding-right: 3px;" valign="top">
                                Dt.Ctd. :</td>
                            <td style="text-align: left; padding-bottom: 1px;">
                                <asp:TextBox ID="txtdatecreated" runat="server" CssClass="input_field" 
                                    MaxLength="20"  Width="170px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td style="text-align: right; padding-right: 3px;" valign="top">
                                Priority :</td>
                            <td style="text-align: left; padding-bottom: 1px;">
                                <asp:DropDownList ID="ddlpriority" runat="server" CssClass="dropdown_field" 
                                    Width="174px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td style="text-align: right; padding-right: 3px;" valign="top">
                                Port :</td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtportdelv" runat="server" CssClass="input_field" 
                                    MaxLength="100"  Width="170px"></asp:TextBox>
                            </td>
                        </tr>
                </table>
            <table border="0" cellpadding="2" cellspacing="0" width="100%" style="text-align:left;border:solid 1px #4371a5;"  >
                <tr>
                <td colspan="2" class="blueheader"><strong>Equipment Info for Spares</strong></td>
                </tr>
                    <tr>
                        <td style="text-align: left; padding-right: 3px;" valign="top">
                            Equip Name:
                        </td>
                        <td style="text-align: left; padding-bottom: 1px;">
                            <asp:TextBox ID="txtEquipName" maxlength="50" runat="server" Width="170px" CssClass="input_field"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td style="text-align: left; padding-right: 3px;" valign="top">
                            Model/Type :
                        </td>
                        <td style="text-align: left; padding-bottom: 1px;">
                            <asp:TextBox ID="txtModelType" maxlength="50" runat="server" Width="170px" CssClass="input_field"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td style="text-align: left; padding-right: 3px;" valign="top">
                            Serial# :
                        </td>
                        <td style="text-align: left; padding-bottom: 1px;">
                            <asp:TextBox ID="txtSerialNo" maxlength="50" runat="server" Width="170px" CssClass="input_field"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td style="text-align: left; padding-right: 3px;" valign="top">
                            Year Built :
                        </td>
                        <td style="text-align: left; padding-bottom: 1px;">
                            <asp:TextBox ID="txtYearBuild" runat="server" Width="170px" MaxLength="4" onkeypress='fncInputNumericValuesOnly(event)'   CssClass="input_field"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td style="text-align: left; padding-right: 3px;" valign="top" colspan="2">
                           Maker's name and Address 
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; padding-bottom: 1px;" colspan="2">
                            <asp:TextBox ID="txtMakerNameAdd" runat="server" Width="300px" Height="55px" TextMode="MultiLine"  CssClass="input_field"></asp:TextBox>
                        </td>
                        
                    </tr>
                </table>
        </td>
        <td align="left" style="width: 78%; padding: 3px;" valign="top">
                <table  cellspacing="0" cellpadding="0" width="100%" border="0" >
                    <tr>
                        <td class="blueheader"><strong>List of Items</strong></td>
                    </tr>
                    <tr>
                        <td style="padding-bottom:2px; padding-top:2px;">
                            <asp:Button ID="btnaddnew" runat="server" CssClass="btn" Text="Add New" Width="65px" onclick="btnaddnew_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height:376px">
                            <div style="width:100%;height:376px;  overflow-x:scroll; overflow-y:scroll;" id="rptheader" runat="server">
                            <table cellpadding="0" cellspacing="0" border="0" width="98%">
                            <tr class= "headerstylegrid">
                            <td class="blueheader" align="center"  style="width:5%">S No.</td>
                            <td class="blueheader" align="center" style="width:34%">Item Desc.</td>
                            <td class="blueheader" align="center" style="width:10%">Part#</td>
                            <td class="blueheader" align="center" style="width:8%">Drawing#</td>
                            <td class="blueheader" align="center" style="width:10%">Code#</td>
                            <td class="blueheader" align="center" style="width:7%">Qty</td>
                            <td class="blueheader" align="center" style="width:12%">UOM</td>
                            <td class="blueheader" align="center" style="width:7%">ROB</td>
                          
                            <%--<td class="blueheader" align="center" style="width:14%">Date
                                <asp:CheckBox ID="chkAll" runat="server" onclick="FillAllTextBox()"/>
                            </td>--%>
                            <td class="blueheader" align="left" style="width:7%">Del</td>
                            </tr>
                            <asp:Repeater runat="Server" ID="rptData" OnItemDataBound=" rptData_OnItemDataBound" onitemcommand="rptData_ItemCommand" ><%--rptData  rptPRData--%>
                            <ItemTemplate>
                            <tr style="padding-top:1px;" class="rowstyle" align="center"  >
                                <td >
                                    <asp:Label ID="lblRowNumber" runat="server" Text="1"  ></asp:Label>
                                     <asp:HiddenField ID="hfRecID" runat="server" Value='<%#Eval("RecID") %>' />
                                    <asp:HiddenField ID="hdnPOStatusId" runat="server" Value='<%#Eval("POStatusId") %>' />
                              
                                </td>
                                <td >
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%" >
                                    <tr>
                                        <td  >
                                            <asp:TextBox ID="txtAddedDesc" runat="server" Width="350px" Text='<%#Eval("Description") %>' TextMode="MultiLine" Height="40px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    </table>
                                </td>
                                <td  >
                                    <asp:TextBox ID="txtPartNo" runat="server" Text='<%#Eval("PartNo") %>'  MaxLength="100" Width="75px"></asp:TextBox>
                                </td>
                                <td  >
                                    <asp:TextBox ID="txtDrawingNo" runat="server" Text='<%#Eval("EquipItemDrawing") %>' MaxLength="100" Width="75px" ></asp:TextBox>
                                </td>
                                <td   >
                                    <asp:TextBox ID="txtCodeNo" runat="server" Text='<%#Eval("EquipItemCode") %>' Width="75px" ></asp:TextBox>                                                                               
                                </td>
                                 <td  >
                                <asp:TextBox ID="txtQuantity" runat="server" Text='<%#Eval("Qty") %>' onkeypress='fncInputNumericValuesOnly(event)' style="text-align:right;"   MaxLength="10" Width="30px" ></asp:TextBox>
                                </td>
                                <td  >
                                    <asp:DropDownList ID="ddlUOM" runat="server"   Width="90px" ></asp:DropDownList>
                                </td>
                                <td  >
                                    <asp:TextBox ID="txtROB" runat="server" Text='<%#Eval("QtyOB") %>' style="text-align:right;"  onkeypress='fncInputNumericValuesOnly(event);AddRows();'  CssClass="input_field" MaxLength="10" Width="40px" ></asp:TextBox>
                                </td>
                               
                                <%--<td   >
                                    <asp:TextBox ID="txtTargetCompDate" runat="server" Text='<%#Eval("targetCompDate1") %>' onkeypress="AddRows();" Width="70px"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" TargetControlID ="txtTargetCompDate"></asp:CalendarExtender>
                                </td> --%>
                                 <td style="padding-left:10px; padding:2px;" >
                                    <asp:ImageButton ID="imgDelete" runat="server" ToolTip="Delete" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/delete.jpg" onkeypress="DeleteSpareRows(this);" OnClick="imgDelete_Click" OnClientClick="javascript:return window.confirm('Are you Sure to Delete?');" />
                                    <asp:Label ID="lblSrNo" style="font-size: smaller; color: Maroon; display: none;" runat="server" ></asp:Label>
                                </td>                                                              
                            </tr>
                           <tr>
                            <td  align=left width="5%"></td>
                            <td  align=left width="30%">   
                                <%--<asp:TextBox ID="txtAddedDesc" runat="server" CssClass="input_field"  Width="300px" Text="" TextMode="MultiLine" ></asp:TextBox>--%>
                            </td>
                            <td align=left width="10%"></td>
                            <td align=left width="10%"></td>
                            <td align=left width="10%"></td>
                            <td align=left style="width: 10%"></td>                     
                            <td align=left width="5%"></td>
                            <td align=left width="5%"></td>
                            <td align=left width="5%" ></td>
                           </tr>
                           
                            </ItemTemplate> 
                            </asp:Repeater>
                            </table>
                            </div>
                        </td>
                    </tr>
                    <tr >
                        <td style="text-align:right; padding:2px;">
                                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" ></asp:Label>
                                       <asp:Button ID="imgSave" runat="server" Text="Save"  OnClick="imgSave_Click" CssClass="btn" /> &nbsp;
                                       <asp:Button  ID="imgCancel" runat="server" Text="Close" OnClick="imgCancel_OnClick" CssClass="btn" /> &nbsp;
                                       <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID ="txtDate"></asp:CalendarExtender>
                                       <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtETA"></asp:CalendarExtender>
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
    var Id='tr' + document.getElementById('ucSpare_hfPRID').value;
    lastSel=document.getElementById(Id);
    var Vessel=document.getElementById("ucSpare_ddlvessel");
    var vslNames=[<%=vesselList%>];
    Vessel.onchange=function()
    {
        Vessel.setAttribute("title",vslNames[Vessel.selectedIndex]);
    }
    </script> 
    
    <script type="text/javascript" >
//    var Vessel=document.getElementById("ucSpare_ddldepartment");
//    var AccNames=[<%=AccountList%>];
//    Vessel.onchange=function()
//    {
//        Vessel.setAttribute("title",AccNames[Vessel.selectedIndex]);
//    }
    </script> 