<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayrollSheetMonthWise.aspx.cs" Inherits="Modules_HRD_CrewPayroll_PayrollSheetMonthWise" MasterPageFile="~/MasterPage.master" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
     <script type="text/javascript" src="../Scripts/jquery.js"></script>
      <link href="../Styles/mystyle.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <div class="text headerband">
            Download Ship Template
        </div>
        <div  >
            <div style="padding:5px;font-family:Arial;font-size:12px;">
                <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td style="text-align: right; padding-right: 5px;">
                                                Vessel :
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
                                                <asp:DropDownList ID="ddl_Month" runat="server"  TabIndex="3"
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
                                            <td style="text-align: left;">
                                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="ddl_Month"
                                                    ErrorMessage="*" MaximumValue="1000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                            </td>
                                            <td style="text-align: right; padding-right: 5px;">
                                                Year:
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
                                                <asp:Button ID="btn_search" runat="server" CssClass="btn" TabIndex="6" Text="Show Data" Width="120px" CausesValidation="false" OnClick="btn_search_Click"/> &nbsp;&nbsp; <asp:Button ID="btnExportToExcel" runat="server" CssClass="btn" TabIndex="7" Text="Export To Excel" Width="120px" CausesValidation="false" OnClick="btnExportToExcel_Click" /> &nbsp;
                                               


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
                <td style="text-align: left;width:100%;background-color:white; " >
                    <asp:GridView  CellPadding="0" CellSpacing="0" ID="GridView1" runat="server"  AutoGenerateColumns="False"  Width="98%"  GridLines="horizontal" >  <%--OnDataBound="SummaryBound"--%>
                                        <Columns>
                                             <asp:BoundField DataField="Sno" HeaderText="SR No." >
                                                <ItemStyle HorizontalAlign="Left" Width="75px" />
                                            </asp:BoundField>
                                                <asp:BoundField DataField="CrewNumber" HeaderText="Crew #" SortExpression="CrewNumber">
                                                <ItemStyle HorizontalAlign="Left" Width="75px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CrewName" HeaderText="Crew Name" SortExpression="FullName">
                                                <ItemStyle HorizontalAlign="Left" Width="125px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="RankCode" HeaderText="Rank" >
                                                <ItemStyle HorizontalAlign="Left" Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ContractID" HeaderText="Contract #" >
                                                <ItemStyle HorizontalAlign="Left" Width="75px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Vessel" HeaderText="Vessel" >
                                                <ItemStyle HorizontalAlign="Left" Width="75px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PayMonth" HeaderText="Pay Month" >
                                                <ItemStyle HorizontalAlign="Left" Width="135px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PayYear" HeaderText="Pay Year" >
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FD" HeaderText="FD" >
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TD" HeaderText="TD" >
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ExtraOTDays" HeaderText="Extra OT Hrs" >
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="TravelPayDays" HeaderText="Travel Pay Days" >
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="Joining_Exp_Reimb" HeaderText="Joining Exp Reimb" >
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="Joining_Allow" HeaderText="Joining Allow" >
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Hold_Cleaning_Bonus" HeaderText="Hold Cleaning Bonus" >
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="HRA_Allow" HeaderText="HRA Allow" >
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="Extra_Work_Allow" HeaderText="Extra Work Allow" >
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="Tank_Cleaning_Allow" HeaderText="Tank Cleaning Allow" >
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Reefer_Bonus" HeaderText="Reefer Bonus" >
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="Watchkeeping_Allow" HeaderText="Watchkeeping Allow" >
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:BoundField>
                                              <asp:BoundField DataField="Laundry_Allow" HeaderText="Laundry Allow" >
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Others" HeaderText="Others" >
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Regular_Allotment" HeaderText="Regular Allotment" >
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="Special_Allotment" HeaderText="Special Allotment" >
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="Cash_Advance" HeaderText="Cash Advance" >
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="Bonded_Stores" HeaderText="Bonded Stores" >
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Phone_Internet" HeaderText="Phone Internet" >
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Airfare_Deduction" HeaderText="Airfare Deduction" >
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="Addl_Allot_Bank_Charges" HeaderText="Additional Allotment Bank Charges" >
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="Other_Recoverables" HeaderText="Other Recoverables" >
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:BoundField>

                                        </Columns>                        

                                        <SelectedRowStyle CssClass="selectedtowstyle" />
                                        <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                        <RowStyle CssClass="rowstyle" />
                                    </asp:GridView>
                    </td>

        </tr>
            
            </table>
            </div>
   </asp:Content>
