<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="RestHourStatus.aspx.cs" Inherits="RestHourStatus"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >

<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
     <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <link href="../../../css/StyleSheet.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <title> </title>
    <style type="text/css">
        body {
            font-size:14px;
            font-family:Calibri; 
            margin:0px;        
        }

    table{border-collapse:collapse;}
    .borderd tr td{
            border:solid 1px #dddbdb;
            color:#333;
    }
    .header tr td{
        background-color:#4e4e4e;
        color:white;
    }
    .red
    {
        background-image:url('../Images/exclamation-mark-Red.png');background-repeat:no-repeat;background-position:center center;
    }
    .green
    {
        background-image:url('../Images/checked-mark-green.png');background-repeat:no-repeat;background-position:center center;
        
    }
</style>    
</head>
<body >
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>

        

         <asp:UpdateProgress runat="server" ID="UpdateProgress1">
            <ProgressTemplate>
            <div style="top :100px; width:100%; position :absolute; padding-top :100px; display:block;">
            <center >
            <div style =" border :solid 1px red; width : 120px; background-color :White; height :36px;">
            <img src="../Images/loading.gif" alt="loading ..." style ="float:left;margin-top: 12px;margin-left:5px;"/><span style ="font-size :11px;"><br />Loading ... </span>
            </div>
            </center>
            </div> 
            </ProgressTemplate>
        </asp:UpdateProgress> 


        <asp:UpdatePanel ID="up11" runat="server" >
    <ContentTemplate>       
    
<table cellpadding="4" cellspacing="0" border="0" id="tblSearchPanel1" runat="server" bordercolor="red" width="100%" style="background-color:#dddbdb">

<tr>
<td style="width:150px">
    <asp:DropDownList ID="ddlFleet" runat="server" CssClass="input_box" Width="100px"  ></asp:DropDownList>

</td>
<td style="width:50px">From :</td>
<td style="width:120px">
    <asp:DropDownList ID="ddlYear" runat="server" CssClass="input_box" Width="55px" ></asp:DropDownList>
</td>
<td style="text-align:right">
    <asp:Button runat="server" ID="btnShow" Text=" Show " OnClick="Show_Click" CssClass="btn" /> 
</td>
</tr>
</table>

<asp:Label ID="lblMsg" runat="server" ForeColor="Red" ></asp:Label>


    <div >
        <div  style="overflow-x:hidden;overflow-y:scroll; height:30px;" >        
        <table cellpadding="5" cellspacing="0" border="1" width="100%" class="borderd header" height="30px" style="text-align:right;" >
            <col width="40px" />
            <col />
            <col width="50px" />
            <col width="50px" />
            <col width="50px" />
            <col width="50px" />
            <col width="50px" />
            <col width="50px" />
            <col width="50px" />
            <col width="50px" />
            <col width="50px" />
            <col width="50px" />
            <col width="50px" />
            <col width="50px" />
            <tr class= "headerstylegrid">
                <td style="text-align:center;">Sr#</td>
                <td style="text-align:left;">Vessel Name</td>
                <td>Jan</td>          
                <td>Feb</td>
                <td>Mar</td>          
                <td>Apr</td>
                <td>May</td>          
                <td>Jun</td>
                <td>Jul</td>          
                <td>Aug</td>
                <td>Sep</td>          
                <td>Oct</td>
                <td>Nov</td>          
                <td>Dec</td>
            </tr>           
        </table>
    </div>
        <div style="overflow-x:hidden;overflow-y:scroll; height:468px;font-size:13px;color:#4e4e4e" >        
        <table cellpadding="3" cellspacing="0" border="0" width="100%" class="borderd" style="text-align:right;" >
            <colgroup>
            <col width="40px" />
            <col  />
            <col width="50px" />
            <col width="50px" />
            <col width="50px" />
            <col width="50px" />
            <col width="50px" />
            <col width="50px" />
            <col width="50px" />
            <col width="50px" />
            <col width="50px" />
            <col width="50px" />
            <col width="50px" />
            <col width="50px" />
                </colgroup>
            <asp:Repeater ID="rptCrewList" runat="server" >
                <ItemTemplate>
                <tr onmouseover="this.style.historycolor=this.style.backgroundColor;this.style.backgroundColor='#c2c2c2';" onmouseout="this.style.backgroundColor=this.style.historycolor;">
                        <td style="text-align:center;">
                            <%#Eval("Sr")%>

                        </td>
                        <td style="text-align:left;">
                            <%#Eval("Vesselname")%>

                        </td>
                        <td class='<%# ((Common.CastAsInt32(Eval("1"))>0)?"red":"green") %>' > &nbsp;</td>
                        <td class='<%# ((Common.CastAsInt32(Eval("2"))>0)?"red":"green") %>'>&nbsp; </td>
                        <td class='<%# ((Common.CastAsInt32(Eval("3"))>0)?"red":"green") %>'>&nbsp; </td>
                        <td class='<%# ((Common.CastAsInt32(Eval("4"))>0)?"red":"green") %>'>&nbsp; </td>
                        <td class='<%# ((Common.CastAsInt32(Eval("5"))>0)?"red":"green") %>'>&nbsp; </td>
                        <td class='<%# ((Common.CastAsInt32(Eval("6"))>0)?"red":"green") %>'>&nbsp; </td>
                        <td class='<%# ((Common.CastAsInt32(Eval("7"))>0)?"red":"green") %>'>&nbsp; </td>
                        <td class='<%# ((Common.CastAsInt32(Eval("8"))>0)?"red":"green") %>'>&nbsp; </td>
                        <td class='<%# ((Common.CastAsInt32(Eval("9"))>0)?"red":"green") %>'>&nbsp; </td>
                        <td class='<%# ((Common.CastAsInt32(Eval("10"))>0)?"red":"green") %>'>&nbsp;</td>
                        <td class='<%# ((Common.CastAsInt32(Eval("11"))>0)?"red":"green") %>'>&nbsp;</td>
                        <td class='<%# ((Common.CastAsInt32(Eval("12"))>0)?"red":"green") %>'>&nbsp;</td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>    
    </div>
   
    </ContentTemplate>
</asp:UpdatePanel>

</form>
</body>
</html>