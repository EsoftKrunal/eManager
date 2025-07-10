<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CRM_SeasonsGreetingHome.aspx.cs" Inherits="CRMActivities_CRM_SeasonsGreetingHome" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <script type="text/javascript" src="../JS/jquery.min.js"></script>
    <script type="text/javascript" src="../JS/KPIScript.js" ></script>
    <script type="text/javascript" src="../JS/jquery.datetimepicker.js" ></script>
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

</style>
</head>
<body>
    <form id="form1" runat="server">    
    <div style="text-align: center;font-family:Arial;font-size:12px;">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center" width="100%">
            <tr>
                <td style="text-align :center" class="text headerband" >Seasons Greeting Home </td>
            </tr>
            <tr>
            <td>          
                   
            <div style="text-align: center">
                <table cellpadding="5" cellspacing="5" width="100%" border="0">
                <tr> 
                    <td style="text-align:left; width:120px;"> <b>Select Office :</b>&nbsp; </td>
                    <td>
                        <asp:DropDownList ID="ddloffice" runat="server" Width="165px"  AutoPostBack="true" OnSelectedIndexChanged="ddloffice_SelectedIndexChanged" ></asp:DropDownList>
                    </td>
                </tr>                
                <tr>
                <td style="text-align:left;" colspan="2">
                   <div style="height:25px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
            <table cellpadding="2" cellspacing="1" width="100%" border="0" style="border-collapse:collapse; height:25px; background-color:#FFEB99; font-weight:bold; ">
               <colgroup>
                    <col style="width:50px; text-align:center;" />
                    <col style="text-align:left;" />
                    <col style="width:100px; text-align:center;" />
                    <col style="width:100px; text-align:center;" />
                    <col style="width:100px; text-align:center;" />                                                          
                    <col style="width:20px;"/>
                </colgroup>
            <tr class= "headerstylegrid">
            <td>Sr#</td>            
            <td>Holiday Name</td>
            <td>Holiday From</td>
            <td>Holiday To</td>
            <td></td>
            <td>&nbsp;</td>
            </tr>
            </table>
            </div>
            <div style="height:462px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
            <table cellpadding="2" cellspacing="1" width="100%" border="0" style="border-collapse:collapse;" >
            <colgroup>
                    <col style="width:50px; text-align:center;" />
                    <col style="text-align:left;" />
                    <col style="width:100px; text-align:center;" />
                    <col style="width:100px; text-align:center;" />
                    <col style="width:100px; text-align:center;" />                                      
                    <col style="width:20px;"/>
                </colgroup>
            <asp:Repeater runat="server" ID="rpt_Holiday">
            <ItemTemplate>
            <tr >
                <td align="right"><%#Eval("SrNo")%>.&nbsp;</td>            
                <td style="text-align:left;"><%#Eval("HolidayReason")%></td>
                <td><%#Common.ToDateString(Eval("HolidayFrom"))%></td>
                <td><%#Common.ToDateString(Eval("HolidayFrom"))%></td>
                <td><asp:LinkButton ID="lnkNoOfCrew" Text='<%#Eval("NoOfCrew")%>'  CommandArgument='<%#Eval("HolidayId")%>' OnClick="lnkNoOfCrew_Click" runat="server"></asp:LinkButton> </td>                
                <td>&nbsp;</td>
            </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
            <tr style="background-color:#CCE6FF;" >
                <td align="right"><%#Eval("SrNo")%>.&nbsp;</td>            
                <td style="text-align:left;"><%#Eval("HolidayReason")%></td>
                <td><%#Common.ToDateString(Eval("HolidayFrom"))%></td>
                <td><%#Common.ToDateString(Eval("HolidayFrom"))%></td>
                <td><asp:LinkButton ID="lnkNoOfCrew" Text='<%#Eval("NoOfCrew")%>'  CommandArgument='<%#Eval("HolidayId")%>' OnClick="lnkNoOfCrew_Click" runat="server"></asp:LinkButton> </td>                
                <td>&nbsp;</td>
            </tr>
            </AlternatingItemTemplate>
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
    </div>
    </form>
</body>
</html>
