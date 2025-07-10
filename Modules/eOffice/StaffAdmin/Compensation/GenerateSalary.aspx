<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenerateSalary.aspx.cs" Inherits="emtm_StaffAdmin_Compensation_GenerateSalary" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Process Salary</title>
    <script src="../../../JS/jquery-1.10.2.js" type="text/javascript"></script>
    <link href="style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .I {
            background-color:#1fe96d!important;
            color:white;
        }
        .C {
            background-color:#1fe96d!important;
            color:white;
        }
        .D {
            background-color:#fb6e54!important;
            color:white;
        }
        .amount{
            text-align:right;
        }
        .datahead
        {
            font-size:11px;
        }
        </style>
</head>
<body >
    <form id="form2" runat="server" >
     <asp:ScriptManager ID="SM" runat="server"></asp:ScriptManager>
    <div id="divtop" style="position:fixed;width:100%;border:solid 0px blue;top:0;left:0;">
        <div class="dottedscrollbox" style=" text-align :center; font-size :13px; background-color:#333; color :White; padding :7px; ">
            Process Salary
        </div>
        
        <div class="dottedscrollbox" style=" text-align :center; font-size :13px; background-color:#aaa; color :White; padding :7px; ">
            <table cellpadding="0" cellspacing ="0" width="100%">
            <tr>
                <td style="width:100px; padding-left:20px; text-align:right;">
                    Office :
                </td>
               <td style="text-align:left; width:150px;">
                    &nbsp;<asp:DropDownList runat="server" ID="ddlOffice" AutoPostBack="true" OnSelectedIndexChanged="btnShowSalary_Click"></asp:DropDownList>
                </td>
                <td style="width:100px; padding-left:20px; text-align:right;">
                    Month :
                </td>
               <td style="text-align:left; width:80px;">
                    &nbsp;<asp:DropDownList runat="server" ID="ddlMonth" AutoPostBack="true" OnSelectedIndexChanged="btnShowSalary_Click">
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
                <td style="width:100px; padding-left:20px; text-align:right;" >
                    Year :
                </td>
               <td style="text-align:left; width:80px;">
                    &nbsp;<asp:DropDownList runat="server" ID="ddlYear" AutoPostBack="true" OnSelectedIndexChanged="btnShowSalary_Click"></asp:DropDownList>
                </td>
                <td style="text-align:left;">
                    <asp:ImageButton ID="btn_ProcessSalary" ImageUrl="~/Modules/HRD/Images/down_arrow.png" ToolTip="Import salary from master" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_ProcessSalary_Click" Text="Process Salary " OnClientClick="return window.confirm('Are you sure ? It will remove all the manual changes made in salary.');" />

                    
                    <%--<asp:Button ID="btnGeneratePaySlip" runat="server" CausesValidation="false" CssClass="btn" OnClick="btnGeneratePaySlip_Click" Text="Generate Pay Slip"  OnClientClick="return window.confirm('Are you sure ?');"/>--%>
                </td>
            </tr>
        </table>
        </div>
        <div id="divDetailHeader" runat="server" style="width:100%;border:solid 0px red;">  
            
        </div>

    </div>
         
        
        <div id="divDetailContent" runat="server" style="border:solid 0px red;margin-top:120px;margin-bottom:50px;">  

        </div>

            
        
    
        <div class="dottedscrollbox" style=" text-align :center; font-size :13px;position:fixed;width:100%;padding:5px; border:solid 0px red;height:30px;bottom:0;background-color:#f2ec7f">
            <asp:Label runat="server"  ID="lblMsg" ForeColor="Red" Font-Size="Larger" style="margin:7px;padding:7px;"></asp:Label>
            <%--<asp:Repeater runat="server" ID="rptData"></asp:Repeater>--%>
            <asp:Button ID="btnTemp" runat="server" OnClick="btnTemp_OnClick" CssClass="btnTemp" style="display:none;" />

            <asp:Button ID="btnTempReport" runat="server" OnClick="btnTempReport_OnClick" CssClass="btnTemp" style="display:none;" />
            <asp:HiddenField ID="hfEmpID" runat="server" />            
        </div>


        <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dvAddEditSalary" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
     <div style="position :relative; width:750px;text-align :center; border :solid 5px #000000;padding-bottom:5px; background : white; z-index:150;top:25px;opacity:1;filter:alpha(opacity=100)">
       <center>
             <div style="background-color:#aaa;padding:5px;color:white; font-size:20px;"> Modify Amount</div>
                
                <table border="0" cellpadding="5" cellspacing="0" style="width:100%;border-collapse:collapse;font-weight:bold; font-size:18px">
                    <tr>
                        <td style="text-align:right">Emp Name :</td>
                        <td>
                            <asp:Label ID="lblEmployeeName" runat="server" style="margin:5px; font-size:15px;"></asp:Label>
                        </td>
                        <td style="text-align:right">Period :</td>
                        <td>
                            <asp:Label ID="lblPeriod" runat="server" style="margin:5px; font-size:15px;"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" style="width:100%;border-collapse:collapse;" class="gridheader">
                    <col width="180px"/>
                        <col width="180px"/>
                        <col />
                    <tr>
                            <td> Salary Head </td>
                             <td> Amount</td>
                                <td></td>
                        </tr>
                    </table>

                    <table border="0" cellpadding="0" cellspacing="0" style="width:100%;border-collapse:collapse;" class="gridrow"> 
                       <col width="180px"/>
                        <col width="180px"/>
                        <col />
                    <asp:Repeater ID="rptEmployeeHeadValues" runat="server" >
                        
                        <ItemTemplate>
                            <tr class='<%#"cls_" + Eval("Income_Ded").ToString() %>'>
                                <td> 
                                    <%#Eval("HeadName") %>
                                    <asp:HiddenField ID="hfHeadID" runat="server" Value='<%#Eval("HeadID") %>' />
                                </td>
                                <td style="text-align:right"> 
                                    <asp:TextBox ID="txtHeadValue" runat="server" Text='<%#Eval("HeadValue") %>' style="text-align:right;"  class='<%#(Eval("Income_Ded").ToString()!="C")? ("InvomeDeductionvalue " + Eval("Income_Ded").ToString()+"N"):"" %>' ></asp:TextBox>
                                </td>
                                <td style=""> 
                                    
                                     <%#((Eval("YearlyHead").ToString()=="Y")?"Yearly":"Monthly")%>
                                     <%#((Eval("Income_Ded").ToString()=="C")?" / CTC Only":"")%>

                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            <table border="0" cellpadding="0" cellspacing="0" style="width:100%;border-collapse:collapse;font-weight:bold;" class="gridheader">
                     <col width="180px"/>
                        <col width="180px"/>
                        <col />
                    <tr>
                            <td> Net Payable Monthly : </td>
                             <td style="text-align:right">  <asp:Label runat="server" ID="lblTotal" Text="0" CssClass="ReviseTotalN"></asp:Label>  </td>
                                <td></td>
                        </tr>
                    </table>
            <br />
                
           <asp:UpdatePanel  ID="up003" runat="server">
               <ContentTemplate>

               
                <div style="height:50px;">
                <asp:Button ID="btnSaveEmpHeadValue" runat="server" CssClass="btn" onclick="btnSaveEmpHeadValue_Click" Text="Save" CausesValidation="false" Width="100px" />
                <asp:Button ID="btnClosePopup" runat="server" CssClass="btn" onclick="btnClosePopup_Click" Text="Close" CausesValidation="false" Width="100px" />

                    

                    <div>
                 <asp:Label ID="lblMsgHeadValues" runat="server" style="color:red;"></asp:Label>
                        </div>
                    </div>

                   <div style="background-color:#aaa;padding:5px;color:white; font-size:20px;"> Lock Salary</div>
                   <div style="background-color:#eaeaea;padding:10px;">

                       <table width="100%" cellpadding="5" cellspacing="0">

                           <col width="120px" />
                           <col width="100px" />
                           <col />
                           <tr>
                               <td>Payment Mode </td>
                               <td>Date</td>
                               <td>Remark</td>
                           </tr>
                           <tr>
                               <td>
                                   <asp:DropDownList ID="ddlPaymentMode" runat="server">
                                       <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                       <asp:ListItem Value="CA" Text="Cash"></asp:ListItem>
                                       <asp:ListItem Value="CH" Text="Cheque"></asp:ListItem>
                                       <asp:ListItem Value="DP" Text="Bank Deposit"></asp:ListItem>
                                    </asp:DropDownList> 
                               </td>
                               <td>
                                    <asp:TextBox ID="txtPaymentDate" runat="server" Width="90px"></asp:TextBox>
                                   <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtPaymentDate" PopupPosition="TopRight"></ajaxToolkit:CalendarExtender>
                               </td>
                               <td>
                                   
                                   <asp:TextBox ID="txtPaymentRemarks" runat="server" TextMode="MultiLine" Width="100%" Height="35px"></asp:TextBox>
                               </td>
                               
                               
                           </tr>
                           <tr>
                               <td colspan="3" style="text-align:right;">
                                   <asp:Label ID="lblMsgLock" runat="server" style="color:red;" ></asp:Label>
                                   <asp:Button ID="btnLock" runat="server" CssClass="btn" onclick="btnLock_Click" Text="Lock" CausesValidation="false" Width="100px" OnClientClick="return confirm('Are you sure to lock this?');" />
                               </td>
                           </tr>
                       </table>
                   </div>
                   </ContentTemplate>
               <Triggers>
                   <asp:PostBackTrigger ControlID="btnClosePopup" />
               </Triggers>
           </asp:UpdatePanel>
            </center>
    </div>
        </center>
            </div>
    </form>

    <script>
        function OpenPopup(EmpID) {
            var l = document.getElementById('btnTemp');
            document.getElementById('hfEmpID').value = EmpID;            
            l.click();
        }
        function PrintPaySlip(EmpID) {
            var l = document.getElementById('btnTempReport');
            document.getElementById('hfEmpID').value = EmpID;
            l.click();
        }
    </script>

        
        <script type="text/javascript">
            $(document).ready(function () {

                $(".InvomeDeductionvalue").change(function () {
                    var TE = 0;
                    var TD = 0;
                    $.each($(".IN"), function () {
                        if ($(this).val()!="") {
                            TE = TE + parseFloat($(this).val());
                        }
                    });

                    $.each($(".DN"), function () {
                        if ($(this).val() != "") {
                            TD = TD + parseFloat($(this).val());
                        }
                    });
                    
                    $(".ReviseTotalN").html(TE - TD);
                });
            })
        </script>


    <script >

        $(window).resize(function () {
            $('#divDetailContent').css('margin-top', $("#divtop").height());
        });

        $(document).ready(function () {
            //$("#divDetailContent").height($("#divtop").height());
            $('#divDetailContent').css('margin-top', $("#divtop").height());
        });
    </script>
</body>
</html>

