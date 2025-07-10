<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModifyVendorProfile_Approval.aspx.cs" Inherits="Docket_ModifyVendorProfile" Async="true"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EMANAGER</title>  
     <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="../JS/JQuery.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>     
    <script type="text/javascript">
        function Validate(sender, args) {
            var repeator = document.getElementById("tblVendors");
            var checkBoxes = repeator.getElementsByTagName("input");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
</script>
    <style type="text/css">
    body
    {
        font-family:Calibri;
        font-size:14px;
        margin:0px;
        padding:0px;
    }
    *
    {
        box-sizing:border-box;
        -moz-box-sizing: border-box;
        -webkit-box-sizing: border-box;
        box-sizing: border-box;
        color:#555;
    }
     h1
    {
        font-size:22px;
        background-color: #35356f;
        border-bottom: 5px solid #00001f;
        color:#fff;
        padding:10px;
        margin:0px;
    }
    
    h2 {
      background-color: #00b359;
      color: white;
      font-size: 17px;
      margin: 0;
      padding: 10px;
      text-align: left;
    }

    .center
    {
        text-align:center;
    }
    .center div
    {
        margin:0 auto;
    }
    .left
    {
        text-align:left;
    }
    .right
    {
        text-align:left;
    }
   
   .activecell  h2 
   {
      background-color: #7cb02c;
      color:white;
      font-size: 17px;
      margin: 0;
      padding: 10px;
      text-align: left;
    }
    .activecell
    {
        background-color:#f1f7e9;
    }
     h2.active:after {
      content:"action im progress";
    }
    
.label
{display:inline-block; text-align:left; float:left; width:400px; color:#222 }
.label:after
{content:":"; text-align:right; width:100px; padding-right:5px; display:inline; float:right; position:absolute; right:5px;top:0px;}
.controlarea
{display:inline; text-align:left;  empty-cells:show; }

.row
{
    padding:4px;
    text-align:left;
    border-bottom:solid 1px #eee;
}
.control
{
    border:solid 1px #ddd;
    padding:5px;
    line-height:14px;
}
  
.actionbox
{
    text-align:center;
    padding:5px;
    position:fixed;
    bottom:0px;
    background-color:#eeeeee;
    width:100%;
    border-top:solid 1px #999;
}

.info
{
    color:Maroon;
    font-style:italic;
    font-size:11px;
}
    .bold
    {
    font-weight:800;
    
    }
.circle
{
    border-radius:8px;
    width:18px;
    height:18px;
    line-height:18px;
    color:#7cb02c;
    font-size:12px;
    display:inline-block;
    float:left;
    background-color:#fff;
    margin-right:10px;
}
.bgmodal
{
    background-color:Black;
    opacity: 0.6;
    filter: alpha(opacity=60);
    position:fixed;
    top:0px;
    left:0px;
    height:100%;
    width:100%;
    z-index:5;
}
.modalframe
{
    position:fixed;
    top:0px;
    left:0px;
    height:100%;
    width:100%;
    z-index:6;
    text-align:center;
    margin:0px auto;
    padding-top:0%;    
}
.modalborder
{
    background:rgba(0,0,0,0.3);
    width:80%;
    margin:0px auto;
    padding:0px;
    border:solid 10px grey;
}
.modalcontainer
{
    height:100%;
    background-color:White;
    padding:10px;
}

.btn {
  background-color: #66C266;
  border: 1px solid #f0f0f0;
  border-radius: 4px;
  color: #fff;
  text-align:center;
  width:150px;
  padding: 5px 0px 7px 0px;
}
.btnpn {
  background-color: #eee;
  border: 1px solid green;
  border-radius: 4px;
  color: green;
  text-align:center;
  width:150px;
  padding: 3px 0px 3px 0px;
}
.close
{
    background-color:#FF6262;
    border: 1px solid #FF6262;
    color:#fff;
}
.btn:hover, .btn:focus {
  background-color: #5CAF5C;
  border-color: #35356f;
  color: #fff;
}
.close:hover,.close:focus
{
   background-color:#FF7373;
   border: 1px solid #FF7373;
}
hr
{
    margin-bottom:0px;
    padding-bottom:0px;
}

.alternate_table 
{
    border:solid 1px #f1f1f1;
}
.alternate_table tr:nth-child(even)
{
    background-color:#f1f1f1;
}
.alternate_table tr:nth-child(odd)
{
    background-color:white;
}

.msgbox
{
    padding:5px 5px 7px 5px;
    border:solid 1px #eee;
    text-align:center;
    margin-left:10px;
    margin-right:10px;
}

</style>
    <style type="text/css">
ul
{
    padding:0px;
    margin:0px;
}

.topmenu
{
    list-style:none;
    padding-left:0px;
}
.topmenu li
{
    display:inline-block;
    float:left;
    width:230px;
    padding-left:0px;
    margin-left:0px;
    margin-right:3px;
    text-align:left
}
.topmenu li a
{
    background-color:#f0f5f5;
    color:#222;
    margin-right:2px;
    padding:6px;
    display:block;
    text-decoration:none;
}
.topmenu li a:hover
{
    background-color:#00803f;
    color:#fff;
    cursor:pointer;
}
.topmenu .activemenu
{
    background-color:#00b359;
    color:white;
    cursor:pointer;
}
.done
{
    background-image:url('./Images/check_white.png');
    background-repeat:no-repeat;
    background-position:205px center;
}


.leftmenu
{
    list-style:none;
    padding-left:0px;
}

.leftmenu li a
{
    background-color:#f0f5f5;
    color:#666;
    margin-bottom:2px;
    padding:12px;
    display:block;
    text-decoration:none;
}
.leftmenu li a:hover
{
    background-color:#00803f;
    color:white;
    cursor:pointer;
}

.leftmenu .activemenu
{
    background-color:#00b359;
    color:white;
    cursor:pointer;
}
.mark1
{
    background-color:#35356f;
    padding:12px;
    
}
.mark1 > span
{
    color:White;
}
.msgbox
{
    padding:5px;
}
.success
{
    color:#009933;
    background-color:#ccffdd;
}
.error
{
    color:Red;
    background-color:#ffcccc;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="bgmodal" runat="server" id="modalBox" visible="false">2</div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:HiddenField ID="hfSID" runat="server" />
    <div class="msgbox" runat="server" id="lblMessage"></div>
    <div>
    <div style="display:none"><asp:Label runat="server" ID="lblCompanyname"></asp:Label><asp:Label runat="server" ID="lblEmailAddress"></asp:Label></div>
    <div class="center">
    <div style="padding:5px">
        <div style="border-bottom:solid 5px #00b359; overflow:auto; margin-bottom:5px; ">
            <ul class="topmenu">
            <li><a class="activemenu" onclick="GoToPage2(1);">1. Justification for new Vendor</a></li>
            <li><a onclick="GoToPage2(2);">2. IInd Proposor Details</a></li>
            <li><a onclick="GoToPage2(3);">3. Ist Approval</a></li>
            <li><a onclick="GoToPage2(4);">4. IInd Approval</a></li>
            </ul>
        </div>
        <asp:Button runat="server" ID="btnTabNo" OnClick="btnTabNo_Click" CausesValidation="false" style="display:none" />
        <asp:HiddenField runat="server" ID="hid_TabNo" Value="1" />
        <asp:Panel runat="server" ID="pnlA1">
        <div class="formcontainer">
        <table cellpadding="3" cellspacing="0" width="99%" class="alternate_table" border="0">
        <tr>
        <td style="text-align:left;">
            <asp:CheckBoxList ID="chk_justificationVendors" runat="server">
            <asp:ListItem Text="Vendor in this location not available" Value="1"></asp:ListItem>
            <asp:ListItem Text="Approved vendor's can not supply" Value="2"></asp:ListItem>
            <asp:ListItem Text="Introduce better vendor" Value="3"></asp:ListItem>
            <asp:ListItem Text="New product/service" Value="4"></asp:ListItem>
            <asp:ListItem Text="Maker Approved" Value="5"></asp:ListItem>
            </asp:CheckBoxList>
              <table cellpadding="3" cellspacing="0" width="100%" class="alternate_table" border="0">
             <colgroup>
             <col style="text-align:left" />
             <col style="text-align:left" />
             </colgroup>
             <tr><td colspan="2">Remarks</td></tr>
             <tr>
                <td colspan="2"><asp:TextBox runat="server" ID="txtPropRemarks" MaxLength="500" TextMode="MultiLine" Rows="8" Width="98%" ></asp:TextBox></td>
             </tr>
             <tr>
                <td style="width:100px">Proposed By</td>
                <td><asp:Label runat="server" ID="txt_ProposedBy" MaxLength="500" ></asp:Label></td>
             </tr>
             <tr>
             <td>Position</td>
             <td><asp:Label runat="server" ID="txt_proposedPosition"   MaxLength="500"></asp:Label></td>
             </tr>
             <tr>
             <td>Date</td>
             <td><asp:Label runat="server" ID="txt_ProposedOn" MaxLength="500"></asp:Label></td>
             </tr>
             <tr>
             <td>Select IInd Proposer</td>
             <td><asp:DropDownList ID="ddl_SecondedTo" runat="server" ValidationGroup="vp1"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ControlToValidate="ddl_SecondedTo" InitialValue="0" ValidationGroup="vp1"></asp:RequiredFieldValidator></td>
             </tr>
             </table>           
             <div style="text-align:right; padding:4px;">
                <asp:Button runat="server" ID="btnSubmit" CssClass="btn" Text="Save" onclick="btnSubmit_Click" ValidationGroup="vp1" OnClientClick="this.value='Loading..';" />
             </div>
        </td>
        <td style="vertical-align:top">
        <div class="bold">&nbsp;<span>Select approved vendors whome you have tried for similier services.</span>( <asp:LinkButton ID="btn_selctvendors" runat="server"  Text="Select" OnClick="btn_selectvendors_Click" /> )</div>
        <div class="row">
         <table cellpadding="3" cellspacing="0" width="99%" class="alternate_table">
          <tr style="background-color:#fef6cd; font-weight:bold; height:30px;">
            <td style="text-align:left">Vendor Name</td> 
            <td style="text-align:left">Email</td>              
            <td></td>
           </tr> 
            <asp:Repeater ID="rpt_VendorsNameForJustification" runat="server" OnItemCommand="rpt_VendorsNameForJustification_ItemCommand">
                <ItemTemplate>
                <tr>
                   <td style="text-align:left"><%#Eval("SupplierName")%><asp:HiddenField ID="hdn_SupplierID" Value='<%#Eval("SupplierID") %>' runat="server"></asp:HiddenField></td>                            
                    <td><%#Eval("SupplierEmail")%></td>                    
                   <td>
                       <asp:HiddenField ID="hdn_SecondedToValue" runat="server"></asp:HiddenField>
                       <asp:ImageButton ToolTip="Delete a record."  OnClientClick="javascript:return confirm('Are you sure to delete record?')" ID="imgBtnDelete" CommandName="Delete" CommandArgument='<%#Eval("SupplierID") %>' runat="server" ImageUrl="~/Images/Delete.png"></asp:ImageButton>
                   </td>
                </tr>
                </ItemTemplate> 
           </asp:Repeater>
           </table>   
         </div>
        </td>
        </tr>
        </table>
         </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlA2" Visible="false">
        <div class="formcontainer">
            <table cellpadding="3" cellspacing="0" width="100%" class="alternate_table">
             <colgroup>
             <col style="text-align:left" />
             <col style="text-align:left" />
             </colgroup>
              <tr><td colspan="2">Remarks</td></tr>
             <tr>
                <td colspan="2"><asp:TextBox runat="server" ID="txtSecRemarks" MaxLength="500" TextMode="MultiLine" Rows="4" Width="98%" ></asp:TextBox></td>
             </tr>
             <tr>
             <td  style="width:100px">IInd Proposer</td>
             <td><asp:CheckBox ID="chk_SecondedTo_save" runat="server" Text="I Agree with proposer"  /></td>
             </tr>
             <tr>
             <td>Name</td>
             <td><asp:Label runat="server" ID="lblSecondedBy" ></asp:Label></td>
             </tr>
             <tr>
             <td>Position</td>
             <td><asp:Label runat="server" ID="lblSecondedByPos" ></asp:Label></td>
             </tr>
             <tr>
             <td>Date</td>
             <td><asp:Label runat="server" ID="lblSecondedOn" ></asp:Label></td>
             </tr>
             </table>

             <div style="text-align:right; padding:4px;">
               <asp:Button runat="server" ID="btnSave_IIndProposer" onclick="btnSave_IIndProposer_Click" CssClass="btn" Text="Save" />
               <asp:Button runat="server" ID="btnSendForApproval" CssClass="btn" Text="Send for Approval" OnClick="btnSendForApproval_Click" />
            </div>
         </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlA3" Visible="false">
        <div class="formcontainer" style="text-align:left">
        <div>
                <asp:HiddenField ID="hdn_VRID" runat="server" />
                <asp:HiddenField ID="hdn_RequestStatus" runat="server" />
                <asp:RadioButtonList ID="rd_ApprovalAcrion" runat="server"  AutoPostBack="true" 
                    RepeatDirection="Horizontal" 
                    onselectedindexchanged="rd_ApprovalAcrion_SelectedIndexChanged">
                <asp:ListItem Text="Approved" Value="A"></asp:ListItem>
                <asp:ListItem Text="Reject" Value="R"></asp:ListItem>
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="rd_ApprovalAcrion" InitialValue="" ValidationGroup="vg1"></asp:RequiredFieldValidator>
            </div>
        <table cellpadding="3" cellspacing="0" width="100%" class="alternate_table">
         <colgroup>
         <col style="text-align:left" />
         <col style="text-align:left" />
         </colgroup>
         <tr>
            <td style="width:100px">Name</td>
            <td><asp:Label runat="server" ID="txt_ApprovedBy" Width="90%" MaxLength="250" ValidationGroup="vg1" ></asp:Label></td>
         </tr>
         <tr>
         <td>Position</td>
         <td><asp:Label runat="server" ID="txt_ApprovedPosition" Width="90%" MaxLength="250" ValidationGroup="vg1" ></asp:Label></td>
         </tr>
         <tr id="tr_apprvalType" runat="server">
         <td>Approval Type</td>
         <td>  <asp:DropDownList ID="ddlApprovalType" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="ddlApprovalType_OnSelectedIndexChanged"> 
                <asp:ListItem Text="Approval Type" Value="0"></asp:ListItem>
                <asp:ListItem Text="Nominated / Contracted" Value="1"></asp:ListItem>
                <asp:ListItem Text="OTA" Value="2"></asp:ListItem>
                <asp:ListItem Text="Other" Value="3"></asp:ListItem>
                <asp:ListItem Text="Owner's Recommendation" Value="6"></asp:ListItem>    
<asp:ListItem Text="Maker" Value="7"></asp:ListItem>            
            </asp:DropDownList>   
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" InitialValue="0" ValidationGroup="vg1" ControlToValidate="ddlApprovalType"></asp:RequiredFieldValidator>       
                <asp:LinkButton Text="Select Fleet/Vessel" ID="lnkApp1" runat="server" Visible="false" OnClick="lnkApp1_Click"></asp:LinkButton>
          </td>
         </tr>
         <tr id="tr_Validity1" runat="server">
         <td>Valid Till Date</td>
         <td>    <asp:TextBox ID="txt_ValidityDate" runat="server" MaxLength="11" Width="100px" ValidationGroup="vg1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txt_ValidityDate" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                <asp:CalendarExtender runat="server" ID="c1" Format="dd-MMM-yyyy" TargetControlID="txt_ValidityDate" ></asp:CalendarExtender>
         </td>
         </tr>
         <tr>
         <td>Remarks</td>
         <td>  <asp:TextBox runat="server" ID="txt_ApprovedRemakrs" CssClass="control" Width="90%"  TextMode="MultiLine" Rows="3" MaxLength="250" ValidationGroup="vg1"></asp:TextBox>  
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="txt_ApprovedRemakrs" ValidationGroup="vg1"></asp:RequiredFieldValidator>         
        </td>
         </tr>
        </table>
        <div style="padding:5px">
            <b>Please select list of service which can be approved from vendor selection.</b>
            <div>
                <asp:CheckBoxList id="chkAppServices" runat="server" RepeatColumns="4" ></asp:CheckBoxList>               
            </div>
        </div>
        <div style="text-align:right; padding:4px;">
              <asp:Button runat="server" ID="btnSave_IstApproval"  OnClick="btnSave_IstApproval_Click" CssClass="btn" Text="Save" ValidationGroup="vg1" OnClientClick="this.value='Loading..';" />       
        </div>
        </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlA4" Visible="false">
        <div class="formcontainer"  style="text-align:left">
        <div>           
                    <asp:RadioButtonList ID="rd_ApprovalAcrion_2" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" onselectedindexchanged="rd_ApprovalAcrion_2_SelectedIndexChanged" >
                    <asp:ListItem Text="Approved" Value="A" ></asp:ListItem>
                    <asp:ListItem Text="Reject" Value="R"></asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" InitialValue="" ValidationGroup="vg2" ControlToValidate="rd_ApprovalAcrion_2"></asp:RequiredFieldValidator>       
                    
            </div>  
        <table cellpadding="3" cellspacing="0" width="100%" class="alternate_table">
         <colgroup>
         <col style="text-align:left" />
         <col style="text-align:left" />
         </colgroup>
         
         <tr id="tr_apprvalType_2" runat="server">
         <td>Approval Type</td>
         <td><asp:DropDownList ID="ddlApprovalType_2" runat="server" Width="150px" ValidationGroup="vg2" AutoPostBack="true" OnSelectedIndexChanged="ddlApprovalType_2_OnSelectedIndexChanged"> 
                <asp:ListItem Text="Approval Type" Value="0"></asp:ListItem>
                <asp:ListItem Text="Nominated / Contracted" Value="1"></asp:ListItem>
                <asp:ListItem Text="OTA" Value="2"></asp:ListItem>
                <asp:ListItem Text="Other" Value="3"></asp:ListItem>               
                <asp:ListItem Text="Owner's Recommendation" Value="6"></asp:ListItem>  
<asp:ListItem Text="Maker" Value="7"></asp:ListItem>              
            </asp:DropDownList>   
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" InitialValue="0" ControlToValidate="ddlApprovalType_2" ValidationGroup="vg2" Display="Static"></asp:RequiredFieldValidator>       
            <asp:LinkButton Text="Select Fleet/Vessel" ID="lnkApp2" runat="server" Visible="false" OnClick="lnkApp2_Click"></asp:LinkButton>
             </td>
         </tr>
        <tr id="tr_validity_2" runat="server">
         <td>Valid Till Date</td>
         <td>       <asp:TextBox ID="txt_ValidityDate_2" runat="server" MaxLength="11" Width="100px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" ControlToValidate="txt_ValidityDate_2" ValidationGroup="vg2" Display="Static"></asp:RequiredFieldValidator>
                <asp:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd-MMM-yyyy" TargetControlID="txt_ValidityDate_2"></asp:CalendarExtender>
      </td>
         </tr>
         <tr>
         <td>Remarks</td>
         <td>    <asp:TextBox runat="server" ID="txt_ApprovedRemakrs_2" CssClass="control" Width="90%"  TextMode="MultiLine" Rows="3" MaxLength="250" ValidationGroup="vg2" ></asp:TextBox>  
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" ControlToValidate="txt_ApprovedRemakrs_2" ValidationGroup="vg2" Display="Static"></asp:RequiredFieldValidator>         
       </td>
         </tr>
         <tr>
            <td style="width:100px">Name</td>
            <td><asp:Label runat="server" ID="txt_ApprovedBy_2" Width="90%" MaxLength="250" ></asp:Label></td>
         </tr>
         <tr>
         <td>Position</td>
         <td><asp:Label runat="server" ID="txt_ApprovedPosition_2"  Width="90%" MaxLength="250" ></asp:Label> </td>
         </tr>
         </table>
        <div style="text-align:right; padding:4px;">
        <asp:Button runat="server" ID="btnSave_IIndApproval"  OnClick="btnSave_IIndApproval_Click" CssClass="btn" Text="Save" ValidationGroup="vg2" OnClientClick="this.value='Loading..';"/>       
        </div>
        </div>
        </asp:Panel>
        </div>
    </div>
    <%--for proposal select vendors--%>
    <div class="modalframe" runat="server" id="modalframe_SelectVendors" visible="false">
        <div class="modalborder">
            <div class="modalcontainer" style="overflow-y: hidden;height: 500px;">
            <h2>Vendors for justification</h2>
             <div style="height:420px; OVERFLOW-Y: scroll; OVERFLOW-X:hidden ;">  
             <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please select at least one record." ClientValidationFunction="Validate" ForeColor="Red"></asp:CustomValidator>
             <table cellpadding="3"  id="tblVendors" cellspacing="0"  border="1" rules="rows" width="100%" class="alternate_table" style="border-collapse:collapse" >
                    <colgroup>
                    <col width="50px" />
                    <col width="50px" />  
                    <col   />
                    <col width="100px" />  
                    <col width="200px" /> 
                    </colgroup>
                <asp:Repeater ID="rpt_VendorList" runat="server"  >
                    <ItemTemplate>
                             <tr>
                                <td><asp:CheckBox ID="chk_VendorList" Text="" runat="server" /></td>
                                <td align="center"> <%#Eval("srno")%>.</td>
                                <td align="left">
                                    <asp:HiddenField ID="hdn_SupplierID" runat="server"  Value='<%#Eval("SupplierID")%>'></asp:HiddenField>
                                     <asp:Label ID="lblSupplierName" runat="server" Text='<%#Eval("SupplierName")%>'></asp:Label>
                                </td>
                                <td align="left"> <%#Eval("TravID")%> </td>
                                <td align="left"> <%#Eval("SupplierEmail")%> </td>                                
                            </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            
            </div>           
           <div>            
              <asp:Button runat="server" ID="btn_SaveVendors" OnClick="btn_savemodel3_Click" CssClass="btn" Text="Save"  />       
              <asp:Button runat="server" ID="btn_CloseVendors" OnClick="btnCloseModal3_Click" CssClass="btn close" Text="Close" CausesValidation="false"/>
            </div>
       
            </div>
         </div>
     </div>
    <%--for select gm/dgm to forward for approval--%>
    <div class="modalframe" runat="server" id="dv_SendForApproval" visible="false">
        <div class="modalborder">
            <div class="modalcontainer">
            <h2>Select Employee to forward for Approval</h2>
            
             <div class="row" >           
                <span class="label">Forwarded To </span>
             <span class="controlarea">&nbsp;
                <asp:DropDownList runat="server" ID="ddlFwdAppTo" ValidationGroup="v45"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ControlToValidate="ddlFwdAppTo" InitialValue="0" ValidationGroup="v45"></asp:RequiredFieldValidator>
                </span>
             </div>
            <div>
              <asp:Button runat="server" ID="btnPOPSendForApp" OnClick="btnPOPSendForApp_Click" CssClass="btn" Text="Save" ValidationGroup="v45"/>
              <asp:Button runat="server" ID="btnClosePOPSendForApp" OnClick="btnClosePOPSendForApp_Click" CssClass="btn close" Text="Close" CausesValidation="false"/>
            </div>
        
            </div>
            </div>
        </div>
    <%--for select nomination--%>
    <div class="modalframe" runat="server" id="dvNomination" visible="false">
        <div class="modalborder">
            <div class="modalcontainer">
             <table width="100%" cellpadding="4">
             <tr style="font-weight:bold; font-size:16px;">
             <td>Select Fleet</td>
             <td>Select Vessel</td>
             </tr>
             <tr>
             <td style="text-align:left; width:30%;">
                <div style="height:400px; overflow-x:hidden; overflow-y:scroll; text-align:left; border:solid 1px #e2e2e2">
                    <asp:CheckBoxList runat="server" ID="chkFleets"></asp:CheckBoxList>
                </div>
             </td>
             <td>
                <div style="height:400px; overflow-x:hidden; overflow-y:scroll;text-align:left; border:solid 1px #e2e2e2">
                    <asp:CheckBoxList runat="server" ID="chkVessels" RepeatColumns="3" Width="100%"></asp:CheckBoxList>
                    </div>
                    </td>
             </tr>
             </table>
            <div style="padding:5px">
              <asp:HiddenField runat="server" ID="hfdNominationStage" />

              <asp:Button runat="server" ID="btnSaveNomination" OnClick="btnSaveNomination_Click" CssClass="btn" Text="Save" ValidationGroup="v45"/>
              <asp:Button runat="server" ID="btnCloseNomination" OnClick="btnCloseNomination_Click" CssClass="btn close" Text="Close" CausesValidation="false"/>
            </div>
             </div>
            
        
            </div>
            </div>
    </div>
    <script type="text/javascript" language="javascript">

        function SetActiveStageTab(MaxStage) {
            var i = 1;
            if ($(".topmenu").length > 0) {
                $(".topmenu li").each(function (i, o) {
                    if (i < MaxStage)
                        $(this).find("a").addClass("done");
                    i++;
                });
            }
        }
        function SetTab2() {
             if ($(".topmenu").length>0) {
                 $(".topmenu li > a").removeClass("activemenu");
                 $(".topmenu li:nth-child(" + $("#hid_TabNo").val() + ") > a").addClass("activemenu");
                 SetActiveStageTab(<%=ApprovalStageDone%>);
             }
         }
         SetActiveStageTab();
         function GoToPage2(pageno) {
             SetTab2();
             $("#hid_TabNo").val(pageno);
             $("#btnTabNo").click();
         }

    </script>
    </form>
</body>
</html>
