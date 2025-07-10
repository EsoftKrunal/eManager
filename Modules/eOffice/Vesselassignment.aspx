<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vesselassignment.aspx.cs" Inherits="Emtm_Vesselassignment" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
    
    .hdng
    {
     text-align:left; padding:5px; font-size:14px; background-color:#85E0FF; color:GREY; border-bottom:solid 1px #eeeeee;
    }
    .header tr td
    {
        background-color:#777777;
        color:White;
    }
    .datatable tr
    {
        background-color:#E6F5FF;
        color:#333;
    }
    .datatable tr:hover
    {
        background-color:#FFE0B2;
        color:#333;
    }
    .center
    {
        text-align:center;
    }
    .left
    {
        text-align:left;
    }
    .right
    {
        text-align:right;
    }
    .btn-error
    {
        color:White;
        background-color:red;
        border:none;
        padding:4px 15px 4px 15px;
        
    }
    .btn-ok
    {
        color:White;
        background-color:#4DB8FF;
        border:none;
        padding:4px 15px 4px 15px;
    }
    .btn:hover
    {
        background-color:#000000;
    }
     </style>
    <script src="../JS/jquery.min.js" type="text/javascript"></script>
    <script src="../JS/KPIScript.js" type="text/javascript"></script>
</head>
<body style="font-family:Calibri; font-size:12px; margin:0px;">
    <form id="form1" runat="server">
   <div>
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>        
        <td style=" text-align :left; vertical-align : top;" > 
     <table width="100%" align="center" cellspacing="0">
     <tr>
        <td style="vertical-align:top;">
          <div style="border:solid 1px #c2c2c2; border-bottom:none; background-color : #777777; color:white; font-size :14px; font-weight:bold; padding:7px; box-sizing:border-box; text-align:left;">
              <asp:DropDownList ID="ddlFleet" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlFleet_OnSelectedIndexChanged"></asp:DropDownList>
          </div>
           <div class="scrollbox" style="WIDTH: 100%; text-align:center; height:26px; overflow-x:hidden; overflow-y:scroll;" >
          <table width="100%" cellpadding="5" cellspacing="1" class="header">
          <tr>
            <td style="width:20px">SR#</td>
            <td style="width:20px">View</td>
            <td>Vessel Name</td>
            <td style="width:110px">Fleet Name</td>
            <td style="width:110px">Tech Suptd.</td>
            <td style="width:110px">Marine Suptd.</td>
            <td style="width:110px">Tech Assistant</td>
            <td style="width:110px">Marine Assistant</td>
            <td style="width:110px">SPA</td>
            <td style="width:110px">Account Officer</td>
            <td style="width:110px">Fleet Manager</td>
            <td style="width:20px">&nbsp;</td>
            </tr>
          </table>
          </div>
          <div class="scrollbox ScrollAutoReset" style="WIDTH: 100%; text-align:center; height:360px; overflow-x:hidden; overflow-y:scroll; text-align:left; font-size:11px; background-color:#999;" id="f11">
      
            <table width="100%" cellpadding="5" cellspacing="1" class="datatable">
              <asp:Repeater ID="rptVessels" runat="server">
              <ItemTemplate>
              <tr>
                <td style="width:20px" class="center"><%#Eval("SRNO")%></td>
                <td style="width:20px" class="center">
                    <%--<asp:ImageButton runat="server" OnClick="btnEdit_Click" CommandArgument='<%#Eval("VesselId")%>' ID="btnEdit" ImageUrl="~/Modules/HRD/Images/AddPencil.gif" />--%>
                    <a target="_blank" href="Emtm_VesselassignmentHistory.aspx?VesselID=<%#Eval("VesselId")%>" > <img src="../Images/magnifier.png" style="border:none;" /> </a>
                </td>
                <td style="text-align:left"><%#Eval("Vesselname")%></td>
                <td style="width:110px"><%#Eval("FLEETNAME")%></td>
                <td style="width:110px"><%#Eval("TECHSUPDT")%></td>
                <td style="width:110px"><%#Eval("MARINESUPDT")%></td>
                <td style="width:110px"><%#Eval("TECHASSISTANT")%></td>
                <td style="width:110px"><%#Eval("MARINEASSISTANT")%></td>
                <td style="width:110px"><%#Eval("SPA")%></td>
                <td style="width:110px"><%#Eval("AcctOfficer")%></td>
                <td style="width:110px"><%#Eval("fleetmanager")%></td>
                <td style="width:20px">&nbsp;</td>
              </tr>
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
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dv_vsl" visible="false" >
            <center>
            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color:Black; z-index:100; opacity:0.7;filter:alpha(opacity=70)"></div>
               <div style="position :relative; width:95%; padding:0px; text-align :center; border :solid 3px #4DB8FF; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
                  <div style='padding:5px; background:#4DB8FF; color:White; text-align:center; font-size:18px;'><asp:Label  runat="server" ID="lblVesselname"></asp:Label> </div>
                  <table cellpadding="2" cellspacing="1" border="0" width="100%">
                  <tr>
                  <td class="right">Effective Date : </td>
                  <td class="left">
                    <asp:TextBox runat="server" ID="txtEffDate" Text="" MaxLength="15" Width="100px"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="*" ControlToValidate="txtEffDate"></asp:RequiredFieldValidator>
                  </td>
                  <td class="right">Fleet Manager : </td>
                  <td class="left"><asp:DropDownList runat="server" ID="ddlFM" Width="300px"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlFM" InitialValue="0"></asp:RequiredFieldValidator>
                  </td>
                  </tr>
                  <tr>
                  <td class="right">Technical Suptd. : </td>
                  <td class="left"><asp:DropDownList runat="server" ID="ddlTS" Width="300px"></asp:DropDownList>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="ddlTS" InitialValue="0"></asp:RequiredFieldValidator>
                  </td>
                  <td class="right">Marine Suptd. : </td>
                  <td class="left"><asp:DropDownList runat="server" ID="ddlMS" Width="300px"></asp:DropDownList>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="ddlMS" InitialValue="0"></asp:RequiredFieldValidator>
                  </td>
                  </tr>
                  <tr>
                  <td class="right">Technical Asst. : </td>
                  <td class="left"><asp:DropDownList runat="server" ID="ddlTA" Width="300px"></asp:DropDownList>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="ddlTA" InitialValue="0"></asp:RequiredFieldValidator>
                  </td>
                  <td class="right">Marine Asst. : </td>
                  <td class="left"><asp:DropDownList runat="server" ID="ddlMA" Width="300px"></asp:DropDownList>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="ddlMA" InitialValue="0"></asp:RequiredFieldValidator>
                  </td>
                  </tr>
                  <tr>
                  <td class="right">Sea Personal Asst. : </td>
                  <td class="left"><asp:DropDownList runat="server" ID="ddlSPA" Width="300px"></asp:DropDownList>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ControlToValidate="ddlSPA" InitialValue="0"></asp:RequiredFieldValidator>
                  </td>
                  <td class="right">Account Officer : </td>
                  <td class="left"><asp:DropDownList runat="server" ID="ddlAO" Width="300px"></asp:DropDownList>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ControlToValidate="ddlAO" InitialValue="0"></asp:RequiredFieldValidator>
                  </td>
                  </tr>
                  </table>
                  <div>
                     <div class="scrollbox" style="WIDTH: 100%; text-align:center; height:26px; overflow-x:hidden; overflow-y:scroll;" >
                        <table width="100%" cellpadding="5" cellspacing="1" class="header">
                       <tr>
                            <td style="width:20px">SR#</td>
                            <td>Eff. Date</td>
                            <td style="width:130px">Tech Suptd.</td>
                            <td style="width:130px">Marine Suptd.</td>
                            <td style="width:130px">Tech Assistant</td>
                            <td style="width:130px">Marine Assistant</td>
                            <td style="width:130px">SPA</td>
                            <td style="width:130px">Account Officer</td>
                            <td style="width:130px">Fleet Manager</td>
                            <td style="width:20px">&nbsp;</td>
                            </tr>
                        </table>
                        </div>
                        <div class="scrollbox ScrollAutoReset" style="WIDTH: 100%; text-align:center; height:160px; overflow-x:hidden; overflow-y:scroll; text-align:left; font-size:11px; background-color:#999;" id="Div1">
                        <table width="100%" cellpadding="5" cellspacing="1" class='datatable'>
                                <asp:Repeater ID="rptVesselHistory" runat="server">
                                <ItemTemplate>
                                <tr>
                                <td style="width:20px" class="center"><%#Eval("SRNO")%></td>
                                <td style="text-align:left"><%#Common.ToDateString(Eval("EFFDate"))%></td>
                                <td style="width:130px"><%#Eval("TECHSUPDT")%></td>
                                <td style="width:130px"><%#Eval("MARINESUPDT")%></td>
                                <td style="width:130px"><%#Eval("TECHASSISTANT")%></td>
                                <td style="width:130px"><%#Eval("MARINEASSISTANT")%></td>
                                <td style="width:130px"><%#Eval("SPA")%></td>
                                <td style="width:130px"><%#Eval("AcctOfficer")%></td>
                                <td style="width:130px"><%#Eval("fleetmanager")%></td>
                                <td style="width:20px">&nbsp;</td>
                                </tr>
                                </ItemTemplate>

                                </asp:Repeater>
                                </table>
                        </div>
                       </div>
                        <div style="padding:5px; background-color:#FFFFD1; text-align:right;">
                        <asp:Label runat="server" ID="lblMsg" ForeColor="Red" Font-Bold="true" style="float:left" Font-Size="18px"></asp:Label>
                      <asp:Button runat="server" ID="Button1" Text="Save" OnClick="btnSave_Click" CssClass="btn-ok" />
                      <asp:Button runat="server" ID="btnClose" Text="Close" OnClick="btnClose_Click" CssClass="btn-error" CausesValidation="false" />
                      </div>
                  </div>
             </center>
        </div>
      </div>
    <ajaxToolkit:CalendarExtender runat="server" ID="Cal1" TargetControlID="txtEffDate" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>  
    </form>
</body>
</html>
