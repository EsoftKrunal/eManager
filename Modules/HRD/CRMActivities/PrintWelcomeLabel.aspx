<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintWelcomeLabel.aspx.cs" Inherits="CRMActivities_PrintWelcomeLabel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title><meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"> 
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
<script type="text/javascript" language="javascript" >
    function printBDLabel(Ids) {
        window.open('BirthDayLabelPrinting.aspx?Ids=' + Ids, '_blank', '', '');
    }    
</script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div style="border:1px solid #000; width:99%;" >   
     <div style="padding:6px; background-color:#80E680; font-size:14px; "><strong>Print Labels</strong></div>
     <div style="width:100%; text-align:left; overflow-y:hidden; overflow-x:hidden; height:575px;">
               <table border="0" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="text-align: center; border-collapse:collapse; width:100%;">
               <tr>
                    <td>
                        <table cellpadding="2" cellspacing="1" width="100%" border="0" style="border-collapse:collapse;font-weight:bold; ">
                            <tr>                            
                            <td style="text-align:right;">Recruiting Office : </td>
                            <td style="text-align:left;"><asp:DropDownList ID="ddl_Recr_Office" runat="server" CssClass="input_box" Width="165px" ></asp:DropDownList> </td>
                            <td style="text-align:right;">Rank : </td>
                            <td style="text-align:left;"><asp:DropDownList ID="ddl_Rank_Search" runat="server" CssClass="input_box" Width="165px" ></asp:DropDownList></td>
                            <td style="text-align:right;">Officer Rating : </td>
                            <td style="text-align:left;"><asp:DropDownList ID="ddl_OR_Search" AutoPostBack="true" OnSelectedIndexChanged="ddl_OR_Search_OnSelectedIndexChanged" runat="server" CssClass="input_box" Width="165px" >
                                                 <asp:ListItem Text="< All >" Value="0" ></asp:ListItem>
                                                 <asp:ListItem Text="O" Value="O" ></asp:ListItem>
                                                 <asp:ListItem Text="R" Value="R" ></asp:ListItem>
                                             </asp:DropDownList>                            
                            </td>
                            
                            <td style="text-align:right; padding-right:10px;"><asp:Label runat="server" ID="lblRcount1"></asp:Label>&nbsp;&nbsp;<asp:Button ID="btnSearch" OnClick="btnSearch_Click" Text="Search" runat="server" CssClass="btn_All" /></td>
                            </tr>
                            <tr>
                            <td colspan="9" style="text-align:right; padding-right:10px;">        
                            <asp:Label runat="server" ID="lblMsg" ForeColor="Red"></asp:Label>
                            </td>
                            </tr>
                        </table>
                    </td>
               </tr>
                     <tr> 
                                                 
                          <td>
                               
                                    <div style="height:25px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
                                    <table cellpadding="2" cellspacing="1" width="100%" border="0" style="border-collapse:collapse; height:25px; background-color:#FFEB99; font-weight:bold; ">
                                          <colgroup>  
                                            <col style="width:50px; text-align:center;" />
                                            <col style="width:60px; text-align:center;" />
                                            <col style="width:200px;text-align:left;" />
                                            <col style="width:70px; text-align:left;" />
                                            <col style="width:90px; text-align:left;" />
                                            <col style="width:100px; text-align:center;" />
                                            <col style="width:100px; text-align:left;" />
                                            <col style="text-align:left;" />
                                            <col style="width:120px; text-align:left;" />
                                            <col style="width:20px;"/>
                                        </colgroup>
                                    <tr>
                                    <td><asp:CheckBox ID="chkSelectAll_Print_Crew" AutoPostBack="true" ToolTip="Select All"  OnCheckedChanged="chkCheckAll_Print_CheckedChanged" runat="server"  /></td>            
                                    <td>Crew#</td>
                                    <td>&nbsp;Crew Name</td>
                                    <td>Rank</td>
                                    <td>DOB</td>                                    
                                    <td>Crew Status</td>
                                    <td>Rect. Office</td>
                                    <td>Address</td>
                                    <td>City</td>
                                    <td>&nbsp;</td>
                                    </tr>
                                    </table>
                                    </div>
                                    <div style="height:450px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
                                    <table cellpadding="2" cellspacing="1" width="100%" border="0" style="border-collapse:collapse;" >
                                    <colgroup>  
                                            <col style="width:50px; text-align:center;" />
                                            <col style="width:60px; text-align:center;" />
                                            <col style="width:200px;text-align:left;" />
                                            <col style="width:70px; text-align:left;" />
                                            <col style="width:90px; text-align:center;" />
                                            <col style="width:100px; text-align:left;" />
                                            <col style="width:100px; text-align:left;" />
                                            <col style="text-align:left;" />
                                            <col style="width:120px; text-align:left;" />
                                            <col style="width:20px;"/>
                                        </colgroup>
                                    <asp:Repeater runat="server" ID="rpt_PrintLabel_Crew">
                                    <ItemTemplate>
                                    <tr >
                                        <td><asp:CheckBox ID="chkSelect" CrewId='<%#Eval("CrewId")%>'  runat="server" /> </td>            
                                        <td><%#Eval("CrewNumber")%></td>
                                        <td>&nbsp;<%#Eval("CrewName")%></td>
                                        <td><%#Eval("RankCode")%></td>
                                        <td><%#Common.ToDateString(Eval("DateOfBirth"))%></td>                                    
                                        <td>&nbsp;<%#Eval("CrewStatusName")%></td>
                                        <td>&nbsp;<%#Eval("RecruitingOfficeName")%></td> 
                                        <td>&nbsp;<%#Eval("Address")%></td> 
                                        <td>&nbsp;<%#Eval("City")%></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                    <tr style="background-color:#CCE6FF;" >
                                        <td><asp:CheckBox ID="chkSelect" CrewId='<%#Eval("CrewId")%>'  runat="server" /> </td>            
                                        <td><%#Eval("CrewNumber")%></td>
                                        <td>&nbsp;<%#Eval("CrewName")%></td>
                                        <td><%#Eval("RankCode")%></td>
                                        <td><%#Common.ToDateString(Eval("DateOfBirth"))%></td>                                    
                                        <td>&nbsp;<%#Eval("CrewStatusName")%></td>
                                        <td>&nbsp;<%#Eval("RecruitingOfficeName")%></td> 
                                        <td>&nbsp;<%#Eval("Address")%></td> 
                                        <td>&nbsp;<%#Eval("City")%></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    </AlternatingItemTemplate>
                                    </asp:Repeater>
                                    </table>
                                    </div>
                               

                               
                               <div style=" text-align:center; width:100%; padding-top: 10px; ">
                                 <asp:Button ID="btnPrintBDLabel" runat="server" Text="Print Label" Width="80px" OnClick="btnPrintBDLabel_Click" style=" background-color:Green; color:White; border:none; padding:4px;"/>
                                 <asp:Button ID="btnClosePrintDiv" runat="server" Text="Close" Width="80px" OnClientClick="javascript:window.close();"  CausesValidation="false" style=" background-color:red; color:White; border:none; padding:4px;"/>
                               </div>
                             
                          </td>
                     </tr>   
               </table>
             </div>
    </div>
    </div>
    </form>
</body>
</html>
