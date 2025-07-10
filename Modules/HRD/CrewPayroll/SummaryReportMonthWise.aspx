<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SummaryReportMonthWise.aspx.cs" Inherits="Modules_HRD_CrewPayroll_SummaryReportMonthWise_aspx" %>

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
                                            <td style="text-align: right; padding-right: 5px;width:75px;">
                                                Vessel :
                                            </td>
                                            <td style="text-align: left;width:150px">
                                                <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="required_box" TabIndex="3" Width="198px"></asp:DropDownList>
                                                <asp:HiddenField ID="hfd_PayrollId" runat="server" />
                                            </td>
                                            <td style="text-align: left;width:20px">     
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddl_Vessel" ErrorMessage="*" MaximumValue="1000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                            </td>
                                            <td style="text-align: right; padding-right: 5px;width:60px">Month:
                                            </td>
                                            <td style="text-align: left;width:80px">
                                                <asp:DropDownList ID="ddl_Month" runat="server" CssClass="required_box" TabIndex="3"
                                                    Width="75px" >
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
                                            <td style="text-align: left;width:20px">
                                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="ddl_Month"
                                                    ErrorMessage="*" MaximumValue="1000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                            </td>
                                            <td style="text-align: right; padding-right: 5px;width:60px">
                                                Year:
                                            </td>
                                            <td style="text-align: left;width:80px">
                                                <asp:DropDownList ID="ddl_Year" runat="server" CssClass="required_box" TabIndex="3"
                                                    Width="75px">
                                                    <asp:ListItem Value="0">&lt; Select &gt; </asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: left;width:20px">
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddl_Year"
                                                    ErrorMessage="*" Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                                            </td>
                                            <td>
                                                <asp:Button ID="btn_search" runat="server" CssClass="btn" TabIndex="6" Text="Show Data" CausesValidation="false" OnClick="btn_search_Click"/> 
                                                
                                                  <asp:Button ID="btnDownload" runat="server" CssClass="btn" TabIndex="6" Text="Download Ship Template"  CausesValidation="false" OnClick="btnDownload_Click" OnClientClick="aspnetForm.target ='_blank';" /> 
                                                 <asp:Button ID="btnImportShipData" runat="server" CssClass="btn" TabIndex="7" Text="Import Ship Data"  CausesValidation="false" OnClick="btnImportShipData_Click" OnClientClick="aspnetForm.target ='_blank';" /> 
                                                <asp:Button ID="btnExportToExcel" runat="server" CssClass="btn" TabIndex="8" Text="Export To Excel"  CausesValidation="false" OnClick="btnExportToExcel_Click" />
                                                
                                                 <a href="javascript:void(0);" onclick="printPageArea('printableArea')" class="btn btn-create" style="width:100px;display:none;" >Print</a>
                                               
                            
                            &nbsp;
                                               
                                               

                                                <%--<a href="WageScaleMasterNew.aspx" target="_blank">Open Wage Scale</a>--%>
                                            </td>
                                            <td>
                                               
                                            </td>
                                        </tr>
            <tr>
                <td colspan="11"  align="center">
                 
                    <asp:Label ID="lbl_Message" runat="server" ForeColor="Red" Text="Label"></asp:Label>
                    <asp:HiddenField ID="hdnCPBAID" runat="server" Value="0" />
                </td>
            </tr>
                                    </table>                             
            </div>
        </div>
        <br /> 
        <div style="height:30px"><br /><br /></div>
            <div>
                <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center" width="100%">
                    <tr>
                                <td style="text-align: left;width:70% ">
                               
                           <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td valign="top" >
                                                
                                                 <div  style="overflow-y: scroll; overflow-x: scroll;height:650px;">
                                                     <br />
                                                <table cellpadding="2" cellspacing="0" style="border-collapse:collapse;width:100%;overflow-y: scroll; overflow-x: scroll;" border="1">
                                                    <thead style="position:sticky;top:0px;">
                                                    <tr class= "headerstylegrid" style=" height:45px;font-size:14px;">
                                                       <%-- <td style="width :4%;">
                                                            View
                                                        </td>--%>
                                                        <td style="width :32%;text-align :center;" colspan="6">
                                                            <strong>Crew List</strong><b>
                                                                <asp:Label ID="lblTotCrew" runat="server" ></asp:Label>
                                                                </b>
                                                        </td>
                                                        <td style="width :18%;text-align :center;" colspan="3">
                                                            Earnings
                                                        </td>
                                                        <td style="width :6%;text-align :center;" >
                                                            Deductions
                                                        </td>
                                                         <td style="width :18%;text-align :center;" colspan="3">
                                                            Balance Of Wages
                                                        </td>
                                                    </tr>
                                                    <tr class= "headerstylegrid" style=" height:45px;font-size:14px;">
                                                       <%-- <td style="width :4%;"></td>--%>
                                                        <td style="width :5%;">Crew #</td>
                                                        <td style="width :12%;">Name</td>
                                                        <td style="width :5%;text-align :center">Rank</td>
                                                       <%-- <td style="width :7%;text-align :center">Sign On Date</td>--%>
                                                        <td style="width :3%;text-align :center">FD</td>
                                                        <td style="width :3%;text-align :center">TD</td>
                                                        <td style="width :4%;text-align :center">Extra OT Hrs</td>
                                                        <td style="width :6%;text-align :center">Monthly Wages</td>
                                                        <td style="width :6%;text-align :center">Extra Earnings</td>
                                                        <td style="width :6%;text-align :center">Total Earnings</td>
                                                        <td style="width :6%;text-align :center">Total Deductions</td>
                                                        <td style="width :6%;text-align :center">Current Month</td>
                                                        <td style="width :6%;text-align :center">Previous Month </td>
                                                        <td style="width :6%;text-align :center">Closing Balance</td>
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
                                                        <%--<td style="text-align :center;color:black;"><%#Eval("SignOnDate")%></td> --%>
                                                        <td style="text-align :center;color:black;"><%#Eval("FD")%></td> 
                                                        <td style="text-align :center;color:black;"><%#Eval("TD")%></td> 
                                                        <td style="text-align :center;color:black;"><%#Eval("ExtraOTHrs")%></td> 
                                                        <td style="text-align :center;color:black;"><%#Eval("Earnings")%></td>
                                                        <td style="text-align :center;color:black;"><%#Eval("ExtraEarings")%></td>
                                                         <td style="text-align :center;color:black;"><%#Eval("MonthlyWages")%></td> 
                                                         <td style="text-align :center;color:black;"><%#Eval("TotalDeductions")%></td> 
                                                         <td style="text-align :center;color:black;"><%#Eval("CurMonBal")%></td> 
                                                         <td style="text-align :center;color:black;"><%#Eval("PrevMonBal")%></td> 
                                                         <td style="text-align :center;color:black;"><%#Eval("BalanceOfWages")%></td> 
                                                        
                                                    </tr>
                                                    </ItemTemplate>
                                                    </asp:Repeater>

                                                  </tbody>
                                                    <tfoot>
<tr  style=" height:45px;font-size:14px;">
                                                        <td colspan="5"></td>
                                                        <td style="width :4%;text-align :center"><b> Total : </b> </td>
                                                        <td style="width :6%;text-align :center"><b><asp:Label ID="lblTotalEarnings" runat="server" Text="0"></asp:Label></b></td>
                                                        <td style="width :6%;text-align :center"><b><asp:Label ID="lblTotalExtraEarnings" runat="server" Text="0"></asp:Label></b></td>
                                                         <td style="width :6%;text-align :center"><b><asp:Label ID="lblTotalMonthlyWages" runat="server" Text="0"></asp:Label></b></td>
                                                        <td style="width :6%;text-align :center"><b><asp:Label ID="lblTotalDeduction" runat="server" Text="0"></asp:Label></b> </td>
                                                        <td style="width :6%;text-align :center"><b><asp:Label ID="lblTotalCurrentMonth" runat="server" Text="0"></asp:Label></b></td>
                                                        <td style="width :6%;text-align :center"><b><asp:Label ID="lblTotalPreviousMonth" runat="server" Text="0"></asp:Label></b> </td>
                                                        <td style="width :6%;text-align :center"><b><asp:Label ID="lblTotalClosingBalance" runat="server" Text="0"></asp:Label></b></td>
                                                    </tr>
        </tfoot>                                       
                                                </table>
                                                     </div>
                                            </td>
                                            
                                        </tr>
                                       
                               
                                    </table>
              
                </td>
                        <td style="text-align: left;width:30%;vertical-align:top; ">
                            <table width="100%">
                                <tr class="text headerband" align="center">
                                    <td colspan="2">
                                         Approval Detail
                                    </td>
                                   
                                </tr>
                                <tr>
                                    <td width="32%" style="padding: 2px 5px 2px 0px;text-align:right;">
                                        Portage Bill Period :
                                    </td>
                                    <td width="68%" style="padding: 2px 0px 2px 5px;text-align:left;">
                                         <asp:Label ID="lblMonthYear" runat="server" Font-Bold="true" Font-Size="16px" Font-Names="Arial;"></asp:Label> 
                                    </td>

                                </tr>
                                <tr>
                                    <td width="32%" style="padding: 2px 5px 2px 0px;text-align:right;">
                                        Crew Verified Count : 
                                    </td>
                                    <td width="68%" style="padding: 2px 0px 2px 5px;text-align:left;">
                                        <asp:Label ID="lblVerfiedCount" runat="server" Font-Bold="true" Font-Size="16px" Font-Names="Arial;"></asp:Label>
                                    </td>

                                </tr>
                                <tr>
                                    <td width="32%" style="padding: 2px 5px 2px 0px;text-align:right;">
                                        Crew Unverified Count : 
                                    </td>
                                    <td width="68%" style="padding: 2px 0px 2px 5px;text-align:left;">
                                        <asp:Label ID="lblUnverfiedCount" runat="server" Font-Bold="true" Font-Size="16px" Font-Names="Arial;"></asp:Label>
                                    </td>

                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-top:3px;padding-bottom:3px;">
                                        <hr style="width:90%;text-align:center;height:5px;background-color:gray;" />
                                    </td>
                                </tr>
                                <tr id="trSubmitforApproval" runat="server" visible="false">
                                    <td colspan="2">
                                       
                                   <table width="98%">
                                       <tr >
                                           <td width="32%" style="padding: 2px 5px 2px 0px;text-align:right;">
                                                Remarks :
                                           </td>
                                           <td width="68%" style="padding: 2px 0px 2px 5px;text-align:left;">
                                               <asp:TextBox ID="txtSubmitApprovalRemarks" runat="server" TextMode="MultiLine" Height="50px" Font-Names="Arial" MaxLength="200" Width="98%"></asp:TextBox>
                                           </td>
                                           
                                       </tr>
                                        <tr>
                                           <td width="32%" style="padding: 2px 5px 2px 0px;text-align:right;">
                                                First Approval :
                                           </td>
                                           <td width="68%" style="padding: 2px 0px 2px 5px;text-align:left;">
                                              <asp:DropDownList ID="ddlFirstApproval" runat="server" Width="200px"></asp:DropDownList>
                                           </td>
                                           
                                       </tr>
                                       <tr>
                                           <td width="32%" style="padding: 2px 5px 2px 0px;text-align:right;">
                                                Second Approval :
                                           </td>
                                           <td width="68%" style="padding: 2px 0px 2px 5px;text-align:left;">
                                              <asp:DropDownList ID="ddlSecondApproval" runat="server" Width="200px"></asp:DropDownList>
                                           </td>
                                           
                                       </tr>
                                       <tr>
                                           <td width="28%" style="padding: 2px 5px 2px 0px;text-align:right;">
                                                Approval Submitted By/On :
                                           </td>
                                           <td width="72%" style="padding: 2px 0px 2px 5px;text-align:left;">
                                               <asp:Label ID="lblSubmitforApproval" runat="server"   Font-Names="Arial" ></asp:Label>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td colspan="2" align="center" style="padding: 5px 0px 5px 0px;">
                                              <asp:Button ID="btnSubmitForApproval" runat="server" Text="Submit For Approval" OnClick="btnSubmitForApproval_Click" Width="165px" Font-Names="Arial" CssClass="btn"  ></asp:Button>
                                           </td>
                                           
                                       </tr>
                                   </table>
                                   </td>
                                </tr>
                                <tr id="trFirstApproval" runat="server" visible="false">
                                    <td colspan="2">
                                   <table width="98%">
                                       <tr >
                                           <td class="text headerband" colspan="2" style="padding: 5px 0px 5px 0px;" >
                                                First Approval
                                           </td>
                                           
                                       </tr>
                                       <tr >
                                           <td width="32%" style="padding: 2px 5px 2px 0px;text-align:right;">
                                                Remarks :
                                           </td>
                                           <td width="68%" style="padding: 2px 0px 2px 5px;text-align:left;">
                                               <asp:TextBox ID="txtFirstApprovalRemarks" runat="server" TextMode="MultiLine" Height="50px" Font-Names="Arial" MaxLength="200" Width="98%"></asp:TextBox>
                                           </td>
                                           
                                       </tr>
                                        <tr>
                                           <td width="32%" style="padding: 2px 5px 2px 0px;text-align:right;">
                                                Approved By/On :
                                           </td>
                                           <td width="68%" style="padding: 2px 0px 2px 5px;text-align:left;">
                                               <asp:Label ID="lblFirstApprovalByOn" runat="server"   Font-Names="Arial" ></asp:Label>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td colspan="2" align="center" style="padding: 5px 0px 5px 0px;">
                                              <asp:Button ID="btnFirstApproval" runat="server" Text="Submit" OnClick="btnFirstApproval_Click" Width="100px" Font-Names="Arial" CssClass="btn"  ></asp:Button>
                                           </td>
                                           
                                       </tr>
                                   </table>
                                   </td>
                                </tr>
                                <tr id="trSecondApproval" runat="server" visible="false">
                                    <td colspan="2">
                                   <table width="98%">
                                       <tr >
                                           <td class="text headerband" colspan="2" style="padding: 5px 0px 5px 0px;" >
                                                Second Approval
                                           </td>
                                       </tr>
                                       <tr >
                                           <td width="32%" style="padding: 2px 5px 2px 0px;text-align:right;">
                                                Remarks :
                                           </td>
                                           <td width="68%" style="padding: 2px 0px 2px 5px;text-align:left;">
                                               <asp:TextBox ID="txtSecondApproval" runat="server" TextMode="MultiLine" Height="50px" Font-Names="Arial" MaxLength="200" Width="98%"></asp:TextBox>
                                           </td>
                                           
                                       </tr>
                                       <tr>
                                           <td width="32%" style="padding: 2px 5px 2px 0px;text-align:right;">
                                                Approved By/On :
                                           </td>
                                           <td width="68%" style="padding: 2px 0px 2px 5px;text-align:left;">
                                               <asp:Label ID="lblSecondApprovedByOn" runat="server"  Font-Names="Arial"  ></asp:Label>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td colspan="2" align="center" style="padding: 5px 0px 5px 0px;">
                                              <asp:Button ID="btnSubmit2" runat="server" Text="Submit" OnClick="btnSubmit2_Click"  Width="100px" Font-Names="Arial" CssClass="btn" ></asp:Button>
                                           </td>
                                           
                                       </tr>
                                   </table>
                                   </td>
                                </tr>
                            </table>
                        </td>
                </tr>
                </table>
            </div>
            <div  id="div1" runat="server" visible="false">
                <asp:GridView CellPadding="0" CellSpacing="0" ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="both"  BorderStyle="Solid" BorderWidth="1px" OnDataBound="HeaderBound">
                       
                        <Columns>
                            <asp:BoundField DataField="Sno" HeaderText="SR No." >
                                <HeaderStyle Width="75px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"   Width="70px"  />
                            </asp:BoundField>
                            <asp:BoundField DataField="Nationality" HeaderText="Nationality">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"   Width="75px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CrewNo" HeaderText="Crew #" >
                                <HeaderStyle  Width="75px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"   Width="75px"  />
                            </asp:BoundField>
                            <asp:BoundField DataField="CrewName" HeaderText="Crew Name" >
                                <HeaderStyle  Width="175px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"  Width="175px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Rank" HeaderText="Rank">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" Width="100px"  />
                            </asp:BoundField>

                            <asp:BoundField DataField="Status" HeaderText="Status">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" Width="100px"   />
                            </asp:BoundField>
                            <asp:BoundField DataField="NoOfDays" HeaderText="No. of Days">
                                <HeaderStyle  Width="75px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"  Width="75px"  />
                            </asp:BoundField>
                            <asp:BoundField DataField="Basic" HeaderText="Basic Wages">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>

                            </asp:BoundField>
                            <asp:BoundField DataField="Fixed_Overtime" HeaderText="Fixed Overtime">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Leave_Pay" HeaderText="Leave Pay">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Subsistence_Allowance" HeaderText="Subsistence Allowance">
                                <HeaderStyle ></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Pension_Fund" HeaderText="Pension Fund">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Uniform_Allowance" HeaderText="Uniform Allowance">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="GOT" HeaderText="GOT">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TravelPayAmount" HeaderText="Travel Pay">
                                <HeaderStyle ></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ExtraOTAmount" HeaderText="Extra OT">
                                <HeaderStyle ></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Joining_Exp_Reimb" HeaderText="Joining Exp Reimb">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                          <%--  <asp:BoundField DataField="Joining_Allow" HeaderText="Joining Allow">
                                <HeaderStyle ></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Left" Width="70px"></ItemStyle>
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="Hold_Cleaning_Bonus" HeaderText="Hold Cleaning Bonus">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="HRA_Allowance" HeaderText="HRA Allow">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Extra_Work_Allowance" HeaderText="Extra Work Allow">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Tank_Cleaning_Allowance" HeaderText="Tank Cleaning Allow">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Reefer_Bonus" HeaderText="Reefer Bonus">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Watchkeeping_Allowance" HeaderText="Watchkeeping Allow">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Laundry_Allowance" HeaderText="Laundry Allow">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="OtherAmount" HeaderText="Other Amount">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="OtherEarnings" HeaderText="Other Earnings">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalEarnings" HeaderText="Total Earnings">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Regular_Allotment" HeaderText="Regular Allotment">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Left" Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Special_Allotment" HeaderText="Special Allotment">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Cash_Advance" HeaderText="Cash Advance">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Bonded_Stores" HeaderText="Bonded Stores">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Left" Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Phone_Internet" HeaderText="Phone Internet">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Employee_PF" HeaderText="Employee PF">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Airfare_Deduction" HeaderText="Airfare Deduction">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Addl_Allot_Bank_Charges" HeaderText="Additional Allotment Bank Charges">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Other_Recoverable" HeaderText="Other Recoverables">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Other_Deductions" HeaderText="Other Deductions">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalDeductions" HeaderText="Total Deductions">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                           
                            <asp:BoundField DataField="CurMonBal" HeaderText="Bal. Of Wages">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PrevMonBal" HeaderText="Opening Bal. Of Wages">
                                <HeaderStyle  Width="100px"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="BalanceOfWages" HeaderText="Closing Bal. Of Wages">
                                <HeaderStyle  Width="100px" ></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                        </Columns>

                        <SelectedRowStyle CssClass="selectedtowstyle" />
                                                                           
                                                                            <HeaderStyle CssClass="headerstylegrid"  />
                                                                            <RowStyle CssClass="rowstylegrid" />
                    </asp:GridView>
            </div>
            </div>
    </form>
</body>
</html>
