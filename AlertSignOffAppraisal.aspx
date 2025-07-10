<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AlertSignOffAppraisal.aspx.cs" Inherits="AlertSignOffAppraisal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EOC Appraisal Alert</title>
</head>
<body  style="font-family :Verdana; font-size :11px; margin :0px 0px 0px 0px;" >
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center" width="100%">
            <tr style="height:23px">
                <td align="center"><div style="width :1000px;background-color:#4371a5;  height :25px; padding-top :5px; color:White"><strong>EOC Appraisal Alerts</strong></div></td>
            </tr>
            <tr style="height:30px">
                <td>
                <center>
                <div style="width :1000px;" >
                <table style="width:1000px" cellpadding="0" cellspacing="0" border="1"  style="border-collapse:collapse"  >
                <thead style=" background-color : lightgray; border: solid 1px gray; height :25px; font-weight: bold  " > 
                <td style="width:50px" >Emp#</td>
                <td style="width:650px">Name</td>
                <td style="width:100px">Vessel</td>
                <td style="width:100px">Ref. #</td>
                <td style="width:110px">Sign Off Date</td>
                </thead>
                </table>
                  <div style=" width:100%; height:450px; overflow-x:hidden; overflow-y:scroll;" > 
                   <asp:GridView ID="gv_VesselManning" ShowHeader="false" runat="server" AutoGenerateColumns="false" CellPadding="0" CellSpacing="0" GridLines="horizontal" Width="98%">
                   <Columns>
                   <asp:BoundField DataField="CrewNumber" HeaderText="Emp #" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                   <asp:BoundField DataField="FullName" HeaderText="Crew Name" ItemStyle-Width="650px" ItemStyle-HorizontalAlign="Left" />
                   <asp:BoundField DataField="Vessel" HeaderText="Vessel" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                   <asp:BoundField DataField="RefNumber" HeaderText="Contract Ref #" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right" />
                   <asp:BoundField DataField="SignOffDate" HeaderText="SignOffDate" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                   </Columns>
                        <RowStyle Height="20px"/>
                    </asp:GridView>
                  </div> 
                </div> 
                </center> 
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
