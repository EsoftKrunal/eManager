<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportHome.aspx.cs" Inherits="ExportHome" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/Modules/PMS/UserControls/Footer.ascx" TagName="footer" TagPrefix="mtm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="CSS/style.css" rel="stylesheet" type="text/css" />
     <link href="CSS/StyleSheet.css" rel="Stylesheet" type="text/css" />
    
     <script type="text/javascript" src="eReports/JS/jquery.min.js"></script>
     <script type="text/javascript" src="eReports/JS/KPIScript.js"></script>
     <style type="text/css" >
    .btnNormal
    {
        font-family: Arial, Helvetica, sans-serifl;
	    cursor: pointer;
	    font-size: 12px;
	    font-weight: bold;
	    background-color:#CCE4F5;
	    height: 25px;	        
	    border:none;	    
	    color:#000000;
    }
    
    .btnSelected
    {
        font-family: Arial, Helvetica, sans-serifl;
	    cursor: pointer;
	    font-size: 12px;	    
	    background-color:#66AFE0;
	    font-weight: bold;
	    height: 25px;	    
	    border:none;	    
	    color:white;
	    
    }
    
    .color_tab{
      background: #3498db;
      background-image: -webkit-linear-gradient(top, #3498db, #2980b9);
      background-image: -moz-linear-gradient(top, #3498db, #2980b9);
      background-image: -ms-linear-gradient(top, #3498db, #2980b9);
      background-image: -o-linear-gradient(top, #3498db, #2980b9);
      background-image: linear-gradient(to bottom, #3498db, #2980b9);
      -webkit-border-radius: 0;
      -moz-border-radius: 0;
      border-radius: 0px;
      font-family: Arial;
      color: #ffffff;
      font-size: 12px;
      padding: 5px 9px 5px 9px;
      text-decoration: none;
    }

    .color_tab:hover {
     background: #3cb0fd;
      background-image: -webkit-linear-gradient(top, #3cb0fd, #3498db);
      background-image: -moz-linear-gradient(top, #3cb0fd, #3498db);
      background-image: -ms-linear-gradient(top, #3cb0fd, #3498db);
      background-image: -o-linear-gradient(top, #3cb0fd, #3498db);
      background-image: linear-gradient(to bottom, #3cb0fd, #3498db);
      text-decoration: none;
    }

    .color_tab_sel{
      background: #facc8c;
      background-image: -webkit-linear-gradient(top, #f7af51, #facc8c);
      background-image: -moz-linear-gradient(top, #f7af51, #facc8c);
      background-image: -ms-linear-gradient(top, #f7af51, #facc8c);
      background-image: -o-linear-gradient(top, #f7af51, #facc8c);
      background-image: linear-gradient(to bottom, #f7af51, #facc8c);
      font-family: Arial;
      color: black;
      font-size: 12px;
      padding: 5px 9px 5px 9px;
      text-decoration: none;
      border-bottom:solid 1px #facc8c;
    }
</style>
<script type="text/javascript">

    function ShowModal() {
        $("#dvModal").css('display','');
    }
    function HideModel() {
        $("#dvModal").css('display', 'none');
    }
    
</script>
</head>
<body style='font-family:Calibri; font-size:14px;'>
    <form id="form1" runat="server">
    <div>
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
     <div style="display:none; "  id="dvModal">
            <div style="position:absolute;top:0px; left:0px; width:100%; height:100%; z-index:50; background-color:black; opacity: .5;filter: alpha(opacity=50);" ></div>
            <div style="position : absolute; top:170px;left:0px; width:100%; z-index:100;  text-align :center; color :Blue;z-index:52; ">
            <center>
            <div style="border:solid 2px orange; width :200px;background-color :White;opacity: 1.0;filter: alpha(opacity=100); padding:20px; overflow:auto; " >
            <div style='font-size:17px; color:Gray;'>Processing... Please Wait !</div>
            <br />
            <img src="Images/loading.gif" alt="loading">
            <br />
                <input type="button" value="Close" onclick="HideModel();" style=" background-color:orange; color:White; border:none; padding:4px; width:120px" />
            </div>
            </center>
            </div>
        </div>
     
     <div  style=' font-size:18px; padding:5px; text-align:center;'><b>Routine Packets</b></div>
     <table cellpadding="3" cellspacing="0" width="100%" border="1" style="border-collapse:collapse">
     <thead style=" background-color:#eeeeee; font-weight:bold;">
     <tr>
     <td>Packet Name</td>
     <td style="width:200px">Last Exported On</td>
     <td style="width:100px; text-align:center;">Export</td>
     </tr>
     </thead>
     <tr>
     <td>PMS Packet</td>
     <td><asp:Label runat="server" ID="lblPMSExportedOn"></asp:Label></td>
     <td style="text-align:center"><asp:ImageButton runat="server" ID="btnExportPMS" ImageUrl="~/Modules/PMS/Images/icon_zip.gif" onclick="btnPMS_Click"></asp:ImageButton></td>
     </tr>
     <tr>
     <td>SMS Packet</td>
     <td><asp:Label runat="server" ID="lblSMSExportedOn"></asp:Label></td>
     <td style="text-align:center"><asp:ImageButton runat="server" ID="btnExportSMS" ImageUrl="~/Modules/PMS/Images/icon_zip.gif" onclick="btnSMS_Click"></asp:ImageButton></td>
     </tr>
     <tr>
     <td>MenuPlanner Packet</td>
     <td><asp:Label runat="server" ID="lblMNPExportedOn"></asp:Label></td>
     <td style="text-align:center"><asp:ImageButton runat="server" ID="btnExportMNP" ImageUrl="~/Modules/PMS/Images/icon_zip.gif" onclick="btnMNP_Click"></asp:ImageButton></td>
     </tr>
     <tr runat="server" >
     <td>Rest Hour Packet</td>
     <td><asp:Label runat="server" ID="lblRHExportedOn"></asp:Label></td>
     <td style="text-align:center"><asp:ImageButton runat="server" ID="btnExportRH" ImageUrl="~/Modules/PMS/Images/icon_zip.gif" onclick="btnExportRH_Click"></asp:ImageButton></td>
     </tr>
     </table>
     <div  style=' font-size:18px; padding:5px; text-align:center;'><b>Other Packets</b></div>
        
            <div class="dvScrollheader" style="height:25px;overflow-y:scroll;overflow-x:hidden;">  
                <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse; height:25px;" >                           
                   <colgroup>
                        <col  />
                        <col style="width:100px;" />
                        <col style="width:250px;"/>
                        <col style="width:50px;" />
                        <col style="width:20px;" />
                    </colgroup>

                        <tr style=" background-color:#666; font-weight:bold;">
                        <td style="text-align:left;"><b>Type </b></td>
                        <td style="text-align:center;"><b>Create Date</b></td>
                        <td style="text-align:center;"><b>Number </b></td>
                        <td style="text-align:center;"><b>Export</b></td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
              </div>
              <div  class="ScrollAutoReset dvScrolldata" style="height:220px;text-align:center; overflow-y:scroll;overflow-x:hidden;" id="fasd125">
                           <table cellspacing="0" rules="none" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col  />
                                    <col style="width:100px;" />
                                    <col style="width:250px;"/>
                                    <col style="width:50px;" />
                                    <col style="width:20px;" />
                                </colgroup>
                                <asp:Repeater ID="rptOtherPackets" runat="server">
                                    <ItemTemplate>
                                            <tr style='<%# (Common.CastAsInt32(Eval("RecordId"))== DoneId)? "background-color:#FFC285;" : "" %>' >
                                                <td style="text-align:left"><%#Eval("RecordType")%></td>                                                
                                                <td style="text-align:center"><%#Common.ToDateString(Eval("CreatedOn"))%></td>
                                                <td align="center"><%#Eval("RecordNo")%></td>
                                                <td align="center">
                                                   <asp:ImageButton ID="btnExport" CssClass='<%#Eval("RecordType")%>' TableId='<%#Eval("TableId")%>' ImageUrl='<%#ExportMode == "M" ? "~/Modules/PMS/Images/icon_zip.gif" : "~/Modules/PMS/Images/email.png" %>' CommandArgument='<%#Eval("RecordId")%>' OnClick="btnExport_Click" runat="server"  />
                                                </td>
                                                <td>&nbsp;</td>
                                           </tr>
                                    </ItemTemplate>       
                                </asp:Repeater>
                            </table>
            </div>

     <div style="padding:5px; background-color:#FFFFCC; text-align:left;">&nbsp;<asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label></div>

     <div ID="dv_RestHr" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
       <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:350px;padding :0px; text-align :center;background : white; z-index:150;top:100px; border:solid 5px black;">
        <center>
              <div class="box3" style='padding:10px 0px 10px  0px'><b>Rest Hour Export</b></div>
              <div >
                <table cellspacing="0" rules="none" border="0" cellpadding="5" style="width:100%;border-collapse:collapse;">
                 <tr>
                    <td style="text-align:right; width:100px;"><b>Month :</b>&nbsp;</td>
                    <td style="text-align:left;"><asp:DropDownList ID="ddlMonth" runat="server" Width="100px" >
                                                 <asp:ListItem Text="< Select >" Value=""></asp:ListItem>
                                                 <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
                                                 <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
                                                 <asp:ListItem Text="Mar" Value="3"></asp:ListItem>
                                                 <asp:ListItem Text="Apr" Value="4"></asp:ListItem>
                                                 <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                                 <asp:ListItem Text="Jun" Value="6"></asp:ListItem>
                                                 <asp:ListItem Text="Jul" Value="7"></asp:ListItem>
                                                 <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
                                                 <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
                                                 <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                                                 <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                                                 <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                                                 </asp:DropDownList>
                    </td>
                    </tr>
                    <tr>
                    <td style="text-align:right; width:100px;"><b>Year :</b>&nbsp;</td>
                    <td style="text-align:left;"><asp:DropDownList ID="ddlYear" runat="server" Width="100px"></asp:DropDownList></td>
                </tr>
                </table>
              </div>
        </center>
          <div style="padding:3px; text-align:right; border-top:solid 2px #c2c2c2; background-color:#FFFFDB">
            <asp:Label runat="server" ID="lblMsgRH" style="float:left;color:Red"></asp:Label>
            <asp:Button runat="server" ID="btnExportRestHr" Text="Export" OnClick="btnExportRestHr_Click" CausesValidation="true" style=" background-color:green; color:White; border:solid 1px grey;width:70px;"/>
            <asp:Button runat="server" ID="Button1" Text="Close" OnClick="btnClose_Click" CausesValidation="false" style=" background-color:Red; color:White; border:solid 1px grey;width:70px;"/>
          </div>
          </div>
        </center>
    </div>

     </div>
    </form>
</body>
</html>
