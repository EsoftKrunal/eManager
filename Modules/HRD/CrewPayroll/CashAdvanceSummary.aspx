<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CashAdvanceSummary.aspx.cs" Inherits="Modules_HRD_CrewPayroll_CashAdvanceSummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
     <script type="text/javascript" src="../Scripts/jquery.js"></script>
      <link href="../Styles/mystyle.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css">
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@100;200;300;400;500;600;700;800;900&display=swap" rel="stylesheet">
    <style>
        @page {
            size: A4 portrait;
            margin-top: 15px;
            margin-bottom: 10px;
            margin-left: 20px;
        }
    </style>
    <script>
        function printPageArea(areaID) {
            var printContent = document.getElementById(areaID).innerHTML;
            var originalContent = document.body.innerHTML;
            document.body.innerHTML = printContent;
            window.print();
            document.body.innerHTML = originalContent;
        }
    </script>

    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js "></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js "></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/js/bootstrap.min.js "></script>
</head>
<body class="A4 portrait">
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <div id="printableArea">
        <div class="stickyHeader" >
            <div style="padding:5px;font-family:Arial;font-size:12px;">
                <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td style="text-align: right; padding-right: 5px;">
                                                Vessel :
                                            </td>
                                            <td style="text-align: left;width:150px">
                                                <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="required_box" TabIndex="3" Width="198px"></asp:DropDownList>
                                                <asp:HiddenField ID="hfd_PayrollId" runat="server" />
                                            </td>
                                            <td style="text-align: left;width:50px">     
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddl_Vessel" ErrorMessage="*" MaximumValue="1000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                            </td>
                                            <td style="text-align: right; padding-right: 5px;width:75px">Month:
                                            </td>
                                            <td style="text-align: left;width:100px">
                                                <asp:DropDownList ID="ddl_Month" runat="server" CssClass="required_box" TabIndex="3"
                                                    Width="94px" >
                                                    <asp:ListItem Value="0">&lt; Select &gt;</asp:ListItem>
                                                    <asp:ListItem Value="1">Jan</asp:ListItem>
                                                    <asp:ListItem Value="2">Feb</asp:ListItem>
                                                    <asp:ListItem Value="3">Mar</asp:ListItem>
                                                    <asp:ListItem Value="4">Apr</asp:ListItem>
                                                    <asp:ListItem Value="5">May</asp:ListItem>
                                                    <asp:ListItem Value="6">Jun</asp:ListItem>
                                                    <asp:ListItem Value="7">Jul</asp:ListItem>
                                                    <asp:ListItem Value="8">Aug</asp:ListItem>
                                                    <asp:ListItem Value="9">Sep</asp:ListItem>
                                                    <asp:ListItem Value="10">Oct</asp:ListItem>
                                                    <asp:ListItem Value="11">Nov</asp:ListItem>
                                                    <asp:ListItem Value="12">Dec</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: left;width:30px">
                                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="ddl_Month"
                                                    ErrorMessage="*" MaximumValue="1000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                            </td>
                                            <td style="text-align: right; padding-right: 5px;width:75px">
                                                Year:
                                            </td>
                                            <td style="text-align: left;width:100px">
                                                <asp:DropDownList ID="ddl_Year" runat="server" CssClass="required_box" TabIndex="3"
                                                    Width="94px">
                                                    <asp:ListItem Value="0">&lt; Select &gt; </asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: left;width:50px">
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddl_Year"
                                                    ErrorMessage="*" Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                                            </td>
                                            <td>
                                                <asp:Button ID="btn_search" runat="server" CssClass="btn" TabIndex="6" Text="Show Data" Width="120px" CausesValidation="false" OnClick="btn_search_Click"/> &nbsp;
                                                
                                                 &nbsp;
                                                 <a href="javascript:void(0);" onclick="printPageArea('printableArea')" class="btn btn-create" style="width:100px;">Print</a>
                                                <asp:Label ID="lbl_gv_Main" runat="server"></asp:Label>
                                               

                                                <%--<a href="WageScaleMasterNew.aspx" target="_blank">Open Wage Scale</a>--%>
                                            </td>
                                            <td>
                                               
                                            </td>
                                        </tr>
            <tr>
                <td colspan="11"  align="center">
                 
                    <asp:Label ID="lbl_Message" runat="server" ForeColor="Red" Text="Label"></asp:Label>
                
                </td>
            </tr>
                                    </table>                             
            </div>
        </div>
        <br /> 
        <div style="height:30px"><br /></div>
            <div>
                <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center" width="100%">
                    <tr>
                                <td style="text-align: left;width:100% " id="td_list">
                                 <%--   <asp:UpdatePanel runat="server" id="up1">
            <ContentTemplate>--%>
                           <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td valign="top" >
                                                <div style="overflow-y: hidden; overflow-x: hidden;width:100%;" >
                                                    <table cellpadding="2" cellspacing="0" width="100%" border="0" >
                                                        <tr>
                                                            <td colspan="6" style=" text-align :left; height :29px;">
                                                                <strong>Crew List</strong><b>
                                                                <asp:Label ID="lblTotCrew" runat="server" ></asp:Label>
                                                                </b>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div  style="overflow-y: scroll; overflow-x: scroll;height:650px;">
                                                <table cellpadding="2" cellspacing="0" style="border-collapse:collapse;width:100%;" border="1">
                                                    <thead style="position:sticky;top:0px;">
                                                    <tr class= "headerstylegrid" style=" height:45px;font-size:14px;">
                                                       <%-- <td style="width :4%;"></td>--%>
                                                        <td style="width :6%;">Crew #</td>
                                                        <td style="width :12%;">Name</td>
                                                        <td style="width :6%;text-align :center">Rank</td>
                                                        <%--<td style="width :7%;text-align :center">Sign On Date</td>--%>
                                                        <td style="width :4%;text-align :center">FD</td>
                                                        <td style="width :4%;text-align :center">TD</td>
                                                       
                                                       <td style="width :7%;text-align :center">Cash Advance</td>
                                                       <%--  <td style="width :7%;text-align :center">Bonded Stores</td>--%>
                                                        
                                                    </tr>
                                                     </thead>
                                                    <tbody>
                                                    <asp:Repeater runat="server" ID="rptPersonal">
                                                    <ItemTemplate>
                                                    <tr style='font-size:14px;' >
                                                  
                                                       <%-- <td>
                                                            <asp:ImageButton runat="server" ID="btnHourG" CausesValidation="false" OnClick="btnHourG_Click" CommandArgument='<%#Eval("Sno")%>' ImageUrl="~/Modules/HRD/Images/HourGlass.gif" />
                                                        </td>--%>
                                                        <td >
                                                            <a onclick="OpenModifyContract(<%#Eval("ContractID")%>)" style="cursor:pointer;">
                                                                <asp:Label ID="lblCrewNo" runat="server" Text='<%#Eval("CrewNumber")%>' ForeColor="Black"></asp:Label>
                                                                </a>
                                                           <%-- <asp:HiddenField ID="hfContractID" runat="server" Value='<%#Eval("ContractID")%>' />
                                                            <asp:HiddenField ID="hfdFD" runat="server" Value='<%#Eval("StartDay")%>' />
                                                            <asp:HiddenField ID="hfdTD" runat="server" Value='<%#Eval("EndDay")%>' />
                                                            <asp:HiddenField ID="hfdOT" runat="server" Value='<%#Eval("ExtraOTdays")%>' />--%>
                                                        </td>
                                                        <td>           
                                                            <div style="width :100%; overflow :hidden;color:black;padding-left:5px;">
                                                            <%#Eval("CrewName")%>
                                                            </div>  
                                                        </td>
                                                        <td style="text-align :center;color:black;"><%#Eval("RankCode")%></td>
                                                       <%-- <td style="text-align :center;color:black;"><%#Eval("SignOnDate")%></td> --%>
                                                        <td style="text-align :center;color:black;"><%#Eval("FD")%></td> 
                                                        <td style="text-align :center;color:black;"><%#Eval("TD")%></td> 
                                                       
                                                        <td style="text-align :center;color:black;"><%#Eval("Cash_Advance")%></td> 
                                                        <%--<td style="text-align :center;color:black;"><%#Eval("Bonded_Stores")%></td> --%>
                                                       
                                                        
                                                    </tr>
                                                    </ItemTemplate>
                                                    </asp:Repeater>
                                                        </tbody>
                                                    <tfoot>
                                                    <tr  style=" height:45px;font-size:14px;">
                                                        <td colspan="4"></td>
                                                        <td style="width :4%;text-align :center"><b> Total : </b> </td>
                                                        
                                                        <td style="width :7%;text-align :center"><b><asp:Label ID="lblTotalCashAdvance" runat="server" Text="0"></asp:Label></b></td>
                                                      <%--  <td style="width :7%;text-align :center"><b><asp:Label ID="lblTotalBondedStores" runat="server" Text="0"></asp:Label></b> </td>--%>
                                                        
                                                    </tr></tfoot>
                                                </table>
                                                    </div>
                                            </td>
                                            
                                        </tr>
                                       
                               
                                    </table>
               <%-- </ContentTemplate>
                                        </asp:UpdatePanel>--%>
                </td>
                </tr>
                </table>
            </div>
            </div>
    </form>
</body>
</html>
