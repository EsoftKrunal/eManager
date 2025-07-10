<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VIQPreparation.aspx.cs" Inherits="Vetting_VIQPreparation" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/LPSQE/VIMS/VIMSMenu.ascx" tagname="VIMSMenu" tagprefix="mtm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>eMANAGER</title>
    <script src="jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script src="VIMSScript.js" type="text/javascript"></script>
    <link href="VIMSStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function OpenPopUP(id) {
            window.open('VIQDetails.aspx?VIQId=' + id, '');
        }
        $(document).ready(function () {
            $(".radselect").change(function () {
                var viqid = $(".radselect").filter(":checked").attr('viqid');
                $("#frmDet").attr("src", "VIQDetails.aspx?VIQId=" +viqid);
            });
        });

    </script>
    <style type="text/css">
    .bar
    {
        background-color:LightGreen; 
        height:14px;
    }
    </style>
    </head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <mtm:VIMSMenu runat="server" ID="VIMSMenu1" />
    <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">    
     <tr>
       <td>
        <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="text-align: center; width:550px;">
                <table border="1" cellpadding="3" cellspacing="0" style="text-align: center; border-collapse:collapse;" width="100%" bordercolor="gray">
                      <tr>
                          <td style="text-align:center;background-color:#E2E2E2; ">
                            <asp:RadioButton Text="Open" ID="radOpen" GroupName="grp" runat="server" Checked="true" AutoPostBack="true" OnCheckedChanged="grp_OnCheckedChanged"/>
                            <asp:RadioButton Text="Closed" ID="radClosed" GroupName="grp" runat="server" AutoPostBack="true" OnCheckedChanged="grp_OnCheckedChanged"/>
                            <asp:RadioButton Text="All" ID="radAll" GroupName="grp" runat="server" AutoPostBack="true" OnCheckedChanged="grp_OnCheckedChanged"/>
                         </td>
                      </tr>
                <tr>
                <td>
                 <table cellpadding="0" cellspacing="0" style="width: 100%; padding-bottom: 2px">
                    <tr>
                        <td>
                            <div style="height:23px; overflow-y:scroll;overflow-x:hidden;">
                            <table cellpadding="1" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor="#07B3D3" height="23px">
                            <thead>
                                <tr >
                                    <td style="width:50px;color:White;">Select</td>
                                    <td style="color:White;">VIQ #</td>
                                    <td style="width:100px;color:White; text-align:center;">Closure Date</td>
                                    <td style="width:200px;color:White;">Progress</td>
                                    <td style="width:20px;color:White;">&nbsp;</td>
                                </tr>
                            </thead>
                            </table>
                            </div>
                            <div style="height:370px; overflow-y:scroll;overflow-x:hidden;" class='ScrollAutoReset' id='dv_VIQ'>
                            <table cellpadding="1" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor="#07B3D3">
                            <asp:Repeater runat="server" ID="rpt_Questions"> 
                            <ItemTemplate>
                            <tr>
                                <td style="width:50px">
                                    <input type="radio" class="radselect" name="radselect" viqid='<%#Eval("VIQId")%>' />
                                </td>
                                <td style=""><%#Eval("VIQNO")%></td>
                                <td style="width:100px;text-align:center;"><%#Common.ToDateString(Eval("TargetDate"))%></td>
                                <td style="text-align:center;width:200px; ">
                                    <div style="width:180px; background-color:White; border:solid 1px green; height:14px; position:relative; cursor:pointer;" >
                                        <div style="position:relative; z-index:100; text-align:center; color:Red;"><%#Eval("DONEQ")%> of <%#Eval("NOQ")%></div>
                                        <div style='position:absolute; top:0px;left:0px;width:<%#Eval("PERDONE")%>%' class='bar'></div>
                                    </div>
                                </center>
                                </td>
                                <td style="text-align:left;width:20px;">&nbsp;</td>
                            </tr>
                            </ItemTemplate>
                            </asp:Repeater>
                            </table>
                            </div>
                            <div style=" height:30px; background-color:#FFF5F5; padding-top:4px; text-align:center;  border:solid 1px #c2c2c2;">
                                <asp:FileUpload runat="server" ID="flpImport" Width="300px" />
                                <asp:Button runat="server" ID="btnImport" Text="Import" CssClass="Btn1" 
                                    Width="100px" style="padding:1px" onclick="btnImport_Click" />
                            </div>
                        </td>
                    </tr>
                </table>
                </td>
                </tr>
                </table>
             </td>
             <td valign="top" style="vertical-align:top">
                <iframe width="100%" id='frmDet' height="460px" frameborder="no" scrolling="no" src="VIQDetails.aspx"></iframe>
             </td>
         </tr>
        </table>
       </td>
      </tr>
     </table>
   </div>
    </form>
</body>
</html>


