<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AlertMangement.aspx.cs" Inherits="AlertMangement" %>
<%@ Register Src="~/UserControls/MessageBox.ascx" TagName="Message" TagPrefix="mtm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Role Management</title>
    <style type="text/css">
    .scroll
    {
        height:500px;
        overflow-x:hidden;
        overflow-y:scroll;
        border-bottom:solid 1px #c2c2c2; 
    }
    </style>
</head>
<body style="font-family:Calibri; margin:0px; font-size:14px; background-color:White;">
   <form id="form1" runat="server">
   <input type="hidden" name="rpt_Data1$ctl00$hfdVesselId" id="rpt_Data1_hfdVesselId_0" />
   <div style="text-align:center">
   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
   <div>
   <table cellpadding="0" cellspacing="0" width="100%">
   <tr>
   <td style="vertical-align:top">
   <div style="padding:5px; background-color:#66C2FF; font-weight:bold;">Alert Types</div>
   <div class="scroll">
   <div style="text-decoration:none; display:none;">
        <asp:TextBox ID="txtAlertTypeId" runat="server"></asp:TextBox>
        <asp:Button ID="btnShowPos" OnClick="btnShowVesselPositions_Click" runat="server" />
    </div>
   <table cellpadding="0" cellspacing="0" width="100%" border="1" bordercolor="#c2c2c2" style="border-collapse:collapse;">
   <tr style="font-weight:bold; background-color:#66C2FF;color:White;">
    <td style="width:50px">Select</td>
    <td style="width:50px">Edit</td>
    <td style="text-align:left; padding:5px;">Alert Name</td>
    <td style="width:300px">Alert Days</td>
   </tr>
   <asp:Repeater runat="server" ID="rptItems">
   <ItemTemplate>
   <tr>
    <td><input type="radio" name="rere" onclick="SelectAlert(this);" id='<%#"rad_" + Eval("AlertTypeId").ToString()%>' typeid='<%#Eval("AlertTypeId")%>' /> </td>
    <td><asp:ImageButton ID="btnEdit" ImageUrl="~/images/editX12.jpg" runat="server" CssClass='<%#Eval("AlertDays")%>' CommandArgument='<%#Eval("AlertTypeId")%>' OnClick="btnEdit_Click" /></td> 
    <td style="text-align:left; padding:3px;"><%#Eval("AlertTypeName")%></td>
    <td align="left">&nbsp;<b><%#Eval("AlertDays")%> Days</b> &nbsp;<%#Eval("AlertMode")%>&nbsp;<%#Eval("AlertOn")%></td>
   </tr>
   </ItemTemplate>
   </asp:Repeater>
   </table>
   </div>
   </td>
   <td style="background-color:#c2c2c2; width:3px;">&nbsp;</td>
   <td style="width:250px; vertical-align:top; text-align:left">
   <div style="padding:5px; background-color:#66C2FF; font-weight:bold;"> Applicable Ranks</div>
   <div class="scroll">
   <table cellpadding="3" cellspacing="0" width="100%" border="1" bordercolor="#c2c2c2" style="border-collapse:collapse;">
   <asp:Repeater runat="server" ID="rptVP">
   <ItemTemplate>
   <tr>
   <td><%#Eval("Positionname")%></td>
   </tr>
   </ItemTemplate>
   </asp:Repeater>
   </table>
   </div>
   </td>
   <td style="background-color:#c2c2c2; width:3px;">&nbsp;</td>
   </tr>
   </table>
   </div>
 
<div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dv_EditAlert" runat="server" visible="false">
                <center>
                  <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                  <div style="position: relative; width: 350px; height: 350px; padding: 3px; text-align: center;background: white; z-index: 150; top: 50px; border: solid 10px gray;">
            
            <div style="padding:3px; background-color:orange; text-align:center; font-weight:bold;" >Edit Alert</div>
            <div style="border-bottom:none;" class="scrollbox">
                <table cellspacing="0" rules="none" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">                    
                   <tr>
                        <td style="text-align:right; width:150px; ">Alert Days : </td>
                        <td style="text-align:left">
                         <asp:TextBox ID="txtDays" runat="server" Width="100px" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        
                        <td colspan="2" >
                         <div style="padding:5px; background-color:#66C2FF;"><b>Applicable Ranks</b></div>
                           <div style="height:200px;overflow-x:hidden;overflow-y:scroll; border-bottom:solid 1px #c2c2c2;" >
                           <table cellpadding="3" cellspacing="0" width="100%" border="1" bordercolor="#c2c2c2" style="border-collapse:collapse;">
                           <asp:Repeater runat="server" ID="rptEditVP">
                           <ItemTemplate>
                           <tr>
                           <td align="left" ><%#Eval("Positionname")%></td>
                           <td><asp:CheckBox ID="chkselect" VPId='<%#Eval("VPId")%>' runat="server" /></td>
                           </tr>
                           </ItemTemplate>
                           </asp:Repeater>
                           </table>
                           </div>
                        </td>
                    </tr>
                </table>
             </div>           

            <div style="text-align:center; padding:5px;">
                    <div style="text-align:left;float:left; padding-top:5px;">
                        <asp:Label ID="lblMsg" ForeColor="Red" runat="server"></asp:Label>
                    </div>
                    <div style="text-align:left;float:right">
                        <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" style=" padding:3px; border:none; color:White; background-color:#2E9AFE; width:80px;"  />
                        <asp:Button runat="server" ID="btnClose" Text="Close" OnClick="btnClose_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                    </div>
            </div>

            
            </div>
                </center>
            </div>

   <script type="text/javascript" >
       function SelectAlert(ctl) {
           var txt = document.getElementById("txtAlertTypeId");
           var btn = document.getElementById("btnShowPos");
           txt.value = ctl.getAttribute("typeid");
           btn.click();
       }

       var selctl=document.getElementById("rad_" + <%=AlertId.ToString()%>);
       if(selctl!=null)
       {
            selctl.setAttribute('checked',true);
       }

   </script>

   </form>
</body>
</html>
