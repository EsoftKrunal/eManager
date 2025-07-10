<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewNetPayableReport.aspx.cs" Inherits="Modules_HRD_CrewPayroll_CrewNetPayableReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <script type="text/javascript" src="../Scripts/jquery.js"></script>
   
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js"></script>
   <style type="text/css">
       .headerstylegrid th {
	background-color: Black;
	height: 23px;
	border: 1px solid #bbbbbb;
	font-family: Arial, sans-serif;
	font-weight: bold;
	font-size: 12px;
	position: sticky;
	cursor: default;
	top: expression(this.parentNode.parentNode.parentNode.scrollTop-2);
	z-index: 0;
	padding-left: 5px;
	text-align: left;
	color:#fff;
    top:0px;
}
       .rowstylegrid
{
           
           border-left : 1px solid white;
           border-right : 1px solid white;
           border-bottom : 1px solid white;
           border-top : 1px solid white;
	       background-color : lightblue;
           padding-left:2px;
}

   </style>
    <link href="css/GridviewScroll.css" rel="stylesheet" />
   <%-- <script src="js/gridviewScroll.min.js" type="text/javascript"></script>
<script type="text/javascript">
        $(document).ready(function () {
            $('#<%=GridView1.ClientID%>').gridviewScroll({
                width: 500,
                height: 500,
                freezesize: 1,
                arrowsize: 30,
                varrowtopimg: "images/arrowvt.png",
                varrowbottomimg: "images/arrowvb.png",
                harrowleftimg: "images/arrowhl.png",
                harrowrightimg: "images/arrowhr.png"
            });
        });
</script>
    <style type="text/css">
        #scrolledGridView {
            overflow-x: scroll;
            overflow-y: scroll;
            text-align: left;
            width: 100%; /* i.e. too small for all the columns */
        }

        .pinned {
            position: fixed; /* i.e. not scrolled */
            background-color: White; /* prevent the scrolled columns showing through */
            z-index: 100; /* keep the pinned on top of the scrollables */
        }

        .scrolled {
            position: relative;
            left: 600px; /* i.e. col1 Width + col2 width */
            overflow: hidden;
            white-space: nowrap;
            min-width: 100%; /* set your real column widths here */
        }

        .col1 {
            left: 0px;
            width: 50px;
        }

        .col2 {
            left: 50px; /* i.e. col1 Width */
            width: 75px;
        }

        .col3 {
            left: 125px; /* i.e. col1 Width */
            width: 100px;
        }

        .col4 {
            left: 225px; /* i.e. col1 Width */
            width: 175px;
        }

        .col5 {
            left: 400px; /* i.e. col1 Width */
            width: 75px;
        }

        .col6 {
            left: 475px; /* i.e. col1 Width */
            width: 75px;
        }

        .col7 {
            left: 550px; /* i.e. col1 Width */
            width: 50px;
        }
    </style>--%>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <%-- <div class="text headerband">
            Download Ship Template
        </div>--%>
        <div style="font-family:Arial;font-size:12px;">
            <div style="padding: 5px; font-family: Arial; font-size: 12px;">
                <table style="background-color: #f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: right; padding-right: 5px;">Vessel :
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="required_box" TabIndex="3" Width="198px"></asp:DropDownList>
                            <asp:HiddenField ID="hfd_PayrollId" runat="server" />
                        </td>
                        <td style="text-align: left;">
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddl_Vessel" ErrorMessage="*" MaximumValue="1000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                        </td>
                        <td style="text-align: right; padding-right: 5px;">Month:
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddl_Month" runat="server" TabIndex="3"
                                Width="94px">
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
                        <td style="text-align: left;">
                            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="ddl_Month"
                                ErrorMessage="*" MaximumValue="1000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                        </td>
                        <td style="text-align: right; padding-right: 5px;">Year:
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddl_Year" runat="server" CssClass="required_box" TabIndex="3"
                                Width="94px">
                                <asp:ListItem Value="0">&lt; Select &gt; </asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left;">
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddl_Year"
                                ErrorMessage="*" Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:Button ID="btn_search" runat="server" CssClass="btn" TabIndex="6" Text="Show Data" Width="120px" CausesValidation="false" OnClick="btn_search_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnExportToExcel" runat="server" CssClass="btn" TabIndex="7" Text="Export To Excel" Width="120px" CausesValidation="false" OnClick="btnExportToExcel_Click" />
                            &nbsp;
                                               


                                                
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="11" align="center">

                            <asp:Label ID="lbl_Message" runat="server" ForeColor="Red" Text="Label"></asp:Label>

                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <%--<br />
        <div style="height: 30px">
            <br />
        </div>--%>
        <div  style="overflow-y: scroll; overflow-x: scroll;height:700px;width:100%;font-family:Arial;font-size:12px;" id="scrolledGridView" >
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid; border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align: center" width="100%">
            <tr>
                <td style="text-align: left; ">
                   
                    <asp:GridView CellPadding="0" CellSpacing="0" ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="both"  BorderStyle="Solid" BorderWidth="1px" >
                       <%--OnDataBound="HeaderBound"--%>
                        <Columns>
                            <asp:BoundField DataField="Sno" HeaderText="SR No." >
                                <HeaderStyle Width="75px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"   Width="70px"  />
                            </asp:BoundField>
                           
                            <asp:BoundField DataField="CrewName" HeaderText="Crew Name" >
                                <HeaderStyle  Width="175px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"  Width="175px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Rank" HeaderText="Rank">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" Width="100px"  />
                            </asp:BoundField>

                            <asp:BoundField DataField="Basic" HeaderText="Basic Wages">
                                <HeaderStyle  Width="120px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ExtraOTAmount" HeaderText="Extra OT">
                                <HeaderStyle  Width="120px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="FOT_GOT" HeaderText="FOT/GOT">
                                <HeaderStyle  Width="120px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Hold_Cleaning_Bonus" HeaderText="Hold Cleaning">
                                <HeaderStyle  Width="120px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="Leave_Pay" HeaderText="Leave Pay">
                                <HeaderStyle  Width="120px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="OtherAmount" HeaderText="Others">
                                <HeaderStyle  Width="120px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                           
                            <asp:BoundField DataField="Pension_Fund" HeaderText="Pension Fund">
                                <HeaderStyle  Width="120px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Provident_Fund_Earning" HeaderText="Provident Fund Earnings">
                                <HeaderStyle  Width="120px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                              <asp:BoundField DataField="Service_Incentive" HeaderText="Serv Inc">
                                <HeaderStyle  Width="120px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="Special_Allowance" HeaderText="Special Allowance">
                                <HeaderStyle  Width="120px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="Subsistence_Allowance" HeaderText="Sub Allowance">
                                <HeaderStyle Width="120px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SWB" HeaderText="SWB">
                                <HeaderStyle Width="120px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="TravelPayAmount" HeaderText="Travel Pay">
                                <HeaderStyle Width="120px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Uniform_Allowance" HeaderText="Uniform Allowance">
                                <HeaderStyle  Width="120px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="CurrMonBal" HeaderText="Current Month Earnings">
                                <HeaderStyle  Width="120px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="PrevMonBal" HeaderText="Previous Month BOW">
                                <HeaderStyle  Width="120px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                           
                            <asp:BoundField DataField="TotalEarnings" HeaderText="Total Earnings">
                                <HeaderStyle  Width="120px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                         
                            <asp:BoundField DataField="Bonded_Stores" HeaderText="Bonded Stores">
                                <HeaderStyle  Width="120px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Cash_Advance" HeaderText="Cash Advanced">
                                <HeaderStyle  Width="120px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Employee_PF" HeaderText="PF Deductions">
                                <HeaderStyle  Width="120px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Other_Deductions" HeaderText="Other Deductions">
                                <HeaderStyle  Width="120px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="Total_Deductions" HeaderText="Total Deductions">
                                <HeaderStyle  Width="120px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Net_Amount" HeaderText="Net_Amount">
                                <HeaderStyle  Width="120px" ></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                        </Columns>

                        <SelectedRowStyle CssClass="selectedtowstyle" />
                                                                           
                                                                            <HeaderStyle CssClass="headerstylegrid"  />
                                                                           <%-- <RowStyle CssClass="rowstylegrid" />--%>
                    </asp:GridView>
                    
                </td>

            </tr>

        </table>
 </div>
    </form>
</body>
</html>
