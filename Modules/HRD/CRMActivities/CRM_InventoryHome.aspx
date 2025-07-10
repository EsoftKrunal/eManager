<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CRM_InventoryHome.aspx.cs" Inherits="CRMActivities_CRM_InventoryHome" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
    <link href="../styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />    
    <style type="text/css">
.fh1
{ 
    font-weight:bold;
    font-size:14px;
}
.fh2
{ 
    font-size:14px;
}
.fh3
{ 
    font-size:14px;
    cursor:pointer;
    text-decoration:underline;
}
.fh3:hover
{ 
    font-size:14px;
    color:Red;
    cursor:pointer;
    text-decoration:underline;
}
.btn_Close
{
    background-color:Red;
    border:solid 1px grey;
    color:White;
    width:100px;   
}
.cls_I
{
    background-color:#80E680;
}
.cls_O
{
    background-color:#FFAD99;
}
.btn_All
{
    background-color:#FF9933;
    border:solid 1px grey;
    font-weight:bold;
    color:White;
    width:100px;
}
</style> 
<%--<script type="text/javascript" language="javascript" >
    function openprintLabel() {
        window.open('PrintWelcomeLabel.aspx', '_blank', '', '');
    }
</script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>   
        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
        <ContentTemplate>
        <div >            
            <div style="width:100%;padding:5px; text-align:center;font-weight:bold;"> 
            <div style="background-color:#9933FF; color:#FFF; font-size:14px; width:100%; height:25px; vertical-align:middle; padding-top:5px;  ">Inventory - [ <asp:Label ID="lblCardType" runat="server"></asp:Label> ]</div>            
            <table cellpadding="2" cellspacing="1" width="100%" border="0" style="border-collapse:collapse; ">
            <tr>
                <td colspan="4"><div><asp:Label runat="server" ID="lblMsg" ForeColor="Red"></asp:Label></div></td>
                <td><div style="float:right; padding-right:8px;"><asp:Button ID="btnBack"  Text="Back" OnClick="btnBack_Click" class="btn_Close" runat="server" /></div></td>
            </tr>
            <tr>
                <td style="text-align:right; width:150px;">Recruiting Office : </td>
                <td style="text-align:left;width:170px;"><asp:DropDownList ID="ddl_Recr_Office" runat="server" CssClass="input_box" Width="165px" ></asp:DropDownList></td>
                <td style="text-align:center; "><asp:Label runat="server" ID="lblRcount1"></asp:Label></td>
                <td style="text-align:right; width:130px;"><asp:Button ID="btnSearch" OnClick="btnSearch_Click" Text="Search" runat="server" CssClass="btn_All" /></td>
                <td style="text-align:right; padding-right:10px;width:130px;"><asp:Button ID="btnAssignCard" OnClick="btnAssignCard_Click"  Text="Assign" runat="server" CssClass="btn_All" /></td>
                           
            </tr>
            </table>
            </div>
             <table cellpadding="2" cellspacing="1" width="100%" border="0" style="border-collapse:collapse; ">
            <tr>
                <td style="width:50%;">
            <div style="height:25px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
            <table cellpadding="2" cellspacing="1" width="100%" border="0" style="border-collapse:collapse; height:25px; background-color:#FFEB99; font-weight:bold; ">
               <colgroup>
                    <col style="width:50px; text-align:center;" />                    
                    <col style="width:100px; text-align:center;" />
                    <col style="width:100px; text-align:center;" />                    
                    <col style="text-align:left;" />                                        
                    <col style="width:20px;"/>
                </colgroup>
            <tr>
            <td><img alt="" title="Edit" src="../Images/AddPencil.gif" /></td>
            <td>Received On</td>
            <td >Cards Received</td>            
            <td >Updated By/ On</td>            
            <td >&nbsp;</td>
            </tr>
            </table>
            </div>
            <div style="height:400px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
            <table cellpadding="2" cellspacing="1" width="100%" border="0" style="border-collapse:collapse;" >
            <colgroup>
                    <col style="width:50px; text-align:center;" />                    
                    <col style="width:100px; text-align:center;" />
                    <col style="width:100px; text-align:center;" />                    
                    <col style="text-align:left;" />                                        
                    <col style="width:20px;"/>
                </colgroup>
            <asp:Repeater runat="server" ID="rpt_Received">
            <ItemTemplate>
            <tr >
                <td ><asp:ImageButton ID="btnEditInventory" ToolTip="Edit" OnClick="btnEditInventory_Click" ImageUrl="~/Modules/HRD/Images/AddPencil.gif" CommandArgument='<%#Eval("InventoryId")%>' runat="server" /> </td>                
                <td ><%#Common.ToDateString(Eval("AssignDate"))%></td>                
                <td ><%#Eval("NoOfCards")%></td>                
                <td align="left">&nbsp;<%# (Eval("UpdatedBy").ToString() == "" ? "" : Eval("UpdatedBy").ToString() + "/ " + Common.ToDateString(Eval("UpdatedOn")))%></td>               
                <td >&nbsp;</td>
            </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
            <tr style="background-color:#CCE6FF;" >
                <td ><asp:ImageButton ID="btnEditInventory" ToolTip="Edit" OnClick="btnEditInventory_Click" ImageUrl="~/Modules/HRD/Images/AddPencil.gif" CommandArgument='<%#Eval("InventoryId")%>' runat="server" /> </td>                
                <td ><%#Common.ToDateString(Eval("AssignDate"))%></td>                
                <td ><%#Eval("NoOfCards")%></td>                
                <td align="left">&nbsp;<%# (Eval("UpdatedBy").ToString() == "" ? "" : Eval("UpdatedBy").ToString() + "/ " + Common.ToDateString(Eval("UpdatedOn")))%></td>               
                <td >&nbsp;</td>
            </tr>
            </AlternatingItemTemplate>
            </asp:Repeater>
            </table>
            </div>
             </td>
             <td>
                 <div style="height:25px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
            <table cellpadding="2" cellspacing="1" width="100%" border="0" style="border-collapse:collapse; height:25px; background-color:#FFEB99; font-weight:bold; ">
               <colgroup>                                       
                    <col style="width:100px; text-align:center;" />                    
                    <col style="text-align:left;" />                                        
                    <col style="width:20px;"/>
                </colgroup>
            <tr>
            <td>Issued On</td>
            <td >Cards Issued</td>            
            <td >&nbsp;</td>
            </tr>
            </table>
            </div>
                 <div style="height:400px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
            <table cellpadding="2" cellspacing="1" width="100%" border="0" style="border-collapse:collapse;" >
            <colgroup>                  
                    <col style="width:100px; text-align:center;" />                    
                    <col style="text-align:left;" />                                        
                    <col style="width:20px;"/>
                </colgroup>
            <asp:Repeater runat="server" ID="rpt_Issued">
            <ItemTemplate>
            <tr >                
                <td ><%#Common.ToDateString(Eval("SentOn"))%></td>                
                <td ><%#Eval("Issued")%></td>
                <td >&nbsp;</td>
            </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
            <tr style="background-color:#CCE6FF;" >
                <td ><%#Common.ToDateString(Eval("SentOn"))%></td>                
                <td ><%#Eval("Issued")%></td>
                <td >&nbsp;</td>
            </tr>
            </AlternatingItemTemplate>
            </asp:Repeater>
            </table>
            </div>
             </td>          
             </tr>
             </table>
             <table cellpadding="10" cellspacing="0" width="100%" border="0" style="border-collapse:collapse; ">
                <tr style="background-color:#80E680; font-weight:bold;">
                    <td>Total Received : &nbsp;<asp:Label ID="lbltotrecvd" runat="server"></asp:Label> </td>
                    <td>Total Issued : &nbsp;<asp:Label ID="lbltotissued" runat="server"></asp:Label> </td>
                    <td>Balance : &nbsp;<asp:Label ID="lblBalance" runat="server"></asp:Label> </td>
                </tr>
            </table>
            <div style="margin-top:5px; height:25px; text-align:right; ">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
            <ProgressTemplate>
            <center>
            <div style="float:left;">
                <img src="../Images/loading.gif"/> Loading .... Please Wait
            </div>
            </center>
            </ProgressTemplate>
            </asp:UpdateProgress>
            </div>
            
        </div>
        <%-- div for sending Cards --%>
        <div style="position:absolute;top:0px;left:0px; height :250px; width:100%; " id="dv_SendCard" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:350px;  height:200px;padding :5px; text-align :center;background : white; z-index:150;top:180px; border:solid 0px black;">
            <center >
             <div style="padding:6px; background-color:#FFE6B2; font-size:14px; "><strong>Assign Cards</strong></div>
             <div style="width:100%; text-align:left; overflow-y:hidden; overflow-x:hidden; height:200px;">
               <table border="0" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="height: 130px; text-align: center; border-collapse:collapse; width:100%;">
                     <%--<tr>                         
                          <td style="text-align: right; width:150px;">
                             <b>Recruiting Office :</b>&nbsp;
                          </td>
                          <td style="text-align:left;">   
                             <asp:DropDownList ID="ddlRecOffice" runat="server" CssClass="input_box" Width="165px" ></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ValidationGroup="BD" runat="server" InitialValue="0" ControlToValidate="ddlRecOffice" ErrorMessage="*" ForeColor="Red" ></asp:RequiredFieldValidator>
                             
                          </td>
                      </tr>
                      <tr>                         
                          <td style="text-align: right; width:150px;">
                             <b>Card Type :</b>&nbsp;
                          </td>
                          <td style="text-align:left;">   
                             <asp:DropDownList ID="ddlCardType" Width="165px" CssClass="input_box" runat="server">
                              <asp:ListItem Text="< Select >" Value="0"></asp:ListItem>
                              <asp:ListItem Text="BirthDay Card" Value="1"></asp:ListItem>
                              <asp:ListItem Text="Seasons Greeting Card" Value="2"></asp:ListItem>
                              <asp:ListItem Text="Welcome Card" Value="3"></asp:ListItem>
                             </asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" ValidationGroup="BD" runat="server" ControlToValidate="ddlCardType" ErrorMessage="*" InitialValue="0" ForeColor="Red" ></asp:RequiredFieldValidator>
                             
                          </td>
                      </tr>--%>
                     <tr>                         
                          <td style="text-align: right; width:150px;">
                             <b>Issue Date :</b>&nbsp;
                          </td>
                          <td style="text-align:left;">   
                             <asp:TextBox ID="txtIssueDate" Width="100px" CssClass="input_box" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ValidationGroup="BD" runat="server" ControlToValidate="txtIssueDate" ErrorMessage="*" ForeColor="Red" ></asp:RequiredFieldValidator>
                             <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtIssueDate"></ajaxToolkit:CalendarExtender>  
                          </td>
                      </tr>
                      <tr>                         
                          <td style="text-align: right; width:150px;">
                             <b>No of Cards :</b>&nbsp;
                          </td>
                          <td style="text-align:left;">   
                             <asp:TextBox ID="txtNoofCards" Width="100px" MaxLength="5" CssClass="input_box" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" ValidationGroup="BD" runat="server" ControlToValidate="txtNoofCards" ErrorMessage="*" ForeColor="Red" ></asp:RequiredFieldValidator>
                             
                          </td>
                      </tr>                     
                        <tr>
                          <td colspan="2" style=" text-align:center;">
                              <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="BD" Width="80px" OnClick="btnSave_Click" style=" background-color:Green; color:White; border:none; padding:4px;"/>                            
                              <asp:Button ID="btn_Close" runat="server" Text="Close" Width="80px" OnClick="btn_Close_Click" CausesValidation="false" style=" background-color:red; color:White; border:none; padding:4px;"/>
                          </td>
                        </tr>
                      </table>
             </div>
             </center>
        </div>
    </center>
    </div>

        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
