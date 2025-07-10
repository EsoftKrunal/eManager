<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MonthlyBudgetForecasting.aspx.cs" Inherits="MonthlyBudgetForecasting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER
    </title>
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    
     <script type="text/javascript" >


         //window.onbeforeunload = function (e) {
         //    var pamt = document.getElementById("lblBudgetAmt").innerHTML;
         //    var samt = document.getElementById("lblTotBdgDB").innerHTML;
         //    var damt = document.getElementById("lblTotalTaskAmount").innerHTML;
         //    debugger;
         //    return "Pleae confirm , " + pamt + " - " + samt + " -  " + damt;
         //};

         function fncInputNumericValuesOnly(evnt) {
             if (!(event.keyCode == 45 || event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                 event.returnValue = false;
             }
         }
         function CheckMidCat() {
             while (!(SelMonth > 0 && SelMonth < 13)) {
                 var SelMonth = prompt("Enter the month", "");
                 document.getElementById('hfSelMonth').value = SelMonth;
                 if (SelMonth == null) {
                     window.close();
                     return;
                 }
                 if (!(SelMonth > 0 && SelMonth < 13)) {
                     alert("Wrong entry of month");
                 }
                 else {
                     document.getElementById('btnReload').click();
                 }
             }
         }
    </script>
    <style type="text/css">
            .Budgeted {
            background-color: #148a1a;
            border: 0px solid #a0f161;
            display: inline-block;
            width:26px;
            text-align:center;
            color:white;
            }
            .UnBudgeted {
            background-color: #f63f2d;
            border: 0px solid #a0f161;
            display: inline-block;
            width:26px;
            text-align:center;
            color:white;
            }
            input
            {
                padding:2px;
            }
    </style>
</head>
<body>
    
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>
    <div style="font-family:Arial;font-size:12px;">
    <div style="position :absolute; width:200px; text-align :center; border :solid 1px Red; background : white; z-index:150;left:80px;top:200px;" runat="server" id="dvAskMonthBox" visible="false" >
        <br /><br />
        <b>Please Enter Month </b>
        <br /><br />
        <asp:TextBox runat="server" CssClass="input_box" ID="txtMonth"></asp:TextBox> 
        <br />
            <asp:RequiredFieldValidator ErrorMessage="Required." Display="Dynamic" ControlToValidate="txtMonth" runat="server" ID="CompareValidator2" ValidationGroup="mnth" ></asp:RequiredFieldValidator>    
            <asp:CompareValidator ErrorMessage="Invalid Month." Display="Dynamic" Operator="GreaterThanEqual" ValueToCompare="1" ControlToValidate="txtMonth" runat="server" ID="c1" ValidationGroup="mnth" Type="Integer"></asp:CompareValidator>    
            <asp:CompareValidator ErrorMessage="Invalid Month." Display="Dynamic" Operator="LessThanEqual" ValueToCompare="12" ControlToValidate="txtMonth" runat="server" ID="CompareValidator1" ValidationGroup="mnth" Type="Integer"></asp:CompareValidator>    
        <br /><br />
        <asp:Button runat ="server" ID="btnSet" CssClass="btn" Text="Select Month" Width="100px" onclick="btnSet_Click" ValidationGroup="mnth"/> 
        <br /><br />
    </div> 
    </div>
    <div style ="height:520px;width:350px; z-index : 50; filter: alpha(opacity = 50);position : absolute; top:0px;left:0px;background-color:#C2C2C2;font-family:Arial;font-size:12px;" runat="server" id="dbAskMonth" visible="false" >
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    </div>
    <table style="text-align : center ;font-family:Arial;font-size:12px;" cellpadding="0" cellspacing="0" border="0" width="100%">
    <tr class="text headerband">
        <td style="padding:5px;font-size:16px;font-weight:bold;">
              Current Year Budget -  <asp:Label ID="lblfyear" runat="server" ></asp:Label>
            <asp:HiddenField ID="hfSelMonth" runat="server" Value="0" />
            <asp:Button ID="btnReload" runat="server" OnClick="btnReload_OnClick" style="display:none;" />
        </td>
    </tr>
       <%-- <tr class="header" style="background-color:#fc8181;color:white;">
        <td style="padding:5px;font-size:13px;font-weight:bold;">
              Distribute the budgeted costs as per planned task and specify  schedule for expenses        
        </td>
    </tr>--%>
    <tr>
        <td style="background-color:#feffd6;">
            
                 
            <div style="float:right; padding:10px;  font-size:20px; font-weight:bold;color:brown">
                <div>
                    
                    
                </div>
            </div>
            <table cellpadding="5" cellspacing="0" border="0" style="border-collapse:collapse; text-align:center; border-collapse:collapse; width:100%">
                <tr>
                    <td style="text-align:right;padding-right:5px; width:107px;" >
                        <b> Company : </b>
                    </td>
                    <td style="text-align:left;padding-left:5px;">
                        <asp:Label ID="lblComp" runat="server" ></asp:Label>
                    </td>
                    <td style="text-align:right;padding-right:5px; width:107px;" >
                        <b> Vessel : </b>
                    </td>
                    <td style="text-align:left;padding-left:5px;">
                        <asp:Label ID="lblVessel" runat="server" ></asp:Label>
                    </td>
                    </tr>
                    <tr>
                    <td style="text-align:right;padding-right:5px; width:115px;" >
                        <b> Account Number :</b>
                    </td>
                    <td style="text-align:left;padding-left:5px;">
                        <asp:Label ID="lblAccNum" runat="server" ></asp:Label>
                    </td>
                    <td style="text-align:right;padding-right:5px;">
                        <b>Account Name :</b>
                    </td>
                    <td style="text-align:left;padding-left:5px;">
                        <asp:Label ID="lblAccName" runat="server" style="float:left;"></asp:Label>
                        
                    </td>
                </tr>
                </table>
        </td>
    </tr>
    <tr>
    <td>
   

        

       <table style="text-align:center;border-collapse:collapse;" cellpadding="0" cellspacing="0" border="1" width="100%" >
          
           <col width="400px" />
            <col />
           <tr>
               <td style="font-size:14px;padding:5px; text-align:right;font-weight:bold;">Proposed Annual Budget : <asp:Label ID="lblBudgetAmt" runat="server"></asp:Label></td>
               <td></td>
           </tr>
           <tr>
               <td>
                   <div style="font-size:13px;font-weight:bold;background-color:#bdf9fb;color:#272323;padding:4px;height:25px;">
                       Monthly Budget

                   </div>
                   <div>
                        <table cellpadding="2" cellspacing="0" width="100%" border="1" style="border-collapse:collapse; text-align:center; border-collapse:collapse;">
       <col  />
        <col  width="120px" style="text-align:right;"/>
       <tr class="header">
         
            <td style="height:25px; vertical-align:middle;">
                Period
            </td>
            <td style="height:25px; vertical-align:middle;"> 
                Monthly Budget
            </td>
       </tr>
       <tr>
            
            <td>
                <asp:Label ID="lbl1stMonth" runat="server" ></asp:Label>
            </td>
            <td><asp:TextBox ID="txt1stMonth" runat="server" Width="120px" MaxLength="20" style="text-align:right;" AutoPostBack="true" OnTextChanged="Amount_OnTextChanged" onkeypress="fncInputNumericValuesOnly(event)" ></asp:TextBox></td>
       </tr>
       <tr>    
            
            <td>
                <asp:Label ID="lbl2ndMonth" runat="server" ></asp:Label>
            </td>
            <td><asp:TextBox ID="txt2ndMonth" runat="server" Width="120px" MaxLength="20" style="text-align:right;" AutoPostBack="true" OnTextChanged="Amount_OnTextChanged" onkeypress="fncInputNumericValuesOnly(event)"></asp:TextBox></td>
       </tr>
       <tr> 
            
            <td>
                <asp:Label ID="lbl3rdMonth" runat="server" ></asp:Label>
            </td>   
            <td><asp:TextBox ID="txt3rdMonth" runat="server" Width="120px" MaxLength="20" style="text-align:right;" AutoPostBack="true" OnTextChanged="Amount_OnTextChanged" onkeypress="fncInputNumericValuesOnly(event)" ></asp:TextBox></td>
       </tr>
       <tr>   
            
            <td>
                <asp:Label ID="lbl4thMonth" runat="server" ></asp:Label>
            </td>
            <td><asp:TextBox ID="txt4thMonth" runat="server" Width="120px" MaxLength="20" style="text-align:right;" AutoPostBack="true" OnTextChanged="Amount_OnTextChanged" onkeypress="fncInputNumericValuesOnly(event)" ></asp:TextBox></td>
       </tr>
       <tr>
            
            <td>
                <asp:Label ID="lbl5thMonth" runat="server" ></asp:Label>
            </td>
            <td><asp:TextBox ID="txt5thMonth" runat="server" Width="120px" MaxLength="20" style="text-align:right;" AutoPostBack="true" OnTextChanged="Amount_OnTextChanged" onkeypress="fncInputNumericValuesOnly(event)" ></asp:TextBox></td>
       </tr>
       <tr>
            
            <td>
                 <asp:Label ID="lbl6thMonth" runat="server" ></asp:Label>
            </td>
            <td><asp:TextBox ID="txt6thMonth" runat="server" Width="120px" MaxLength="20" style="text-align:right;" AutoPostBack="true" OnTextChanged="Amount_OnTextChanged" onkeypress="fncInputNumericValuesOnly(event)" ></asp:TextBox></td>
        </tr>
        <tr>
            
            <td>
                <asp:Label ID="lbl7thMonth" runat="server" ></asp:Label>
            </td>
            <td><asp:TextBox ID="txt7thMonth" runat="server" Width="120px" MaxLength="20" style="text-align:right;" AutoPostBack="true" OnTextChanged="Amount_OnTextChanged" onkeypress="fncInputNumericValuesOnly(event)" ></asp:TextBox></td>
       </tr>
       <tr>
            
            <td>
                <asp:Label ID="lbl8thMonth" runat="server" ></asp:Label>
            </td>
            <td><asp:TextBox ID="txt8thMonth" runat="server" Width="120px" MaxLength="20" style="text-align:right;" AutoPostBack="true" OnTextChanged="Amount_OnTextChanged" onkeypress="fncInputNumericValuesOnly(event)" ></asp:TextBox></td>
       </tr>
       <tr>
            
            <td>
                <asp:Label ID="lbl9thMonth" runat="server" ></asp:Label>
            </td>
            <td><asp:TextBox ID="txt9thMonth" runat="server" Width="120px" MaxLength="20" style="text-align:right;" AutoPostBack="true" OnTextChanged="Amount_OnTextChanged" onkeypress="fncInputNumericValuesOnly(event)" ></asp:TextBox></td>
       </tr>
       <tr>
           
            <td>
                 <asp:Label ID="lbl10thMonth" runat="server" ></asp:Label>
            </td>
            <td><asp:TextBox ID="txt10thMonth" runat="server" Width="120px" MaxLength="20" style="text-align:right;" AutoPostBack="true" OnTextChanged="Amount_OnTextChanged" onkeypress="fncInputNumericValuesOnly(event)"  ></asp:TextBox></td>
       </tr>
       <tr>
           
            <td>
                <asp:Label ID="lbl11thMonth" runat="server" ></asp:Label>
            </td>
            <td><asp:TextBox ID="txt11thMonth" runat="server" Width="120px" MaxLength="20" style="text-align:right;" AutoPostBack="true" OnTextChanged="Amount_OnTextChanged" onkeypress="fncInputNumericValuesOnly(event)" ></asp:TextBox></td>
       </tr>
       <tr>
            
            <td>    
                <asp:Label ID="lbl12thMonth" runat="server" ></asp:Label>
            </td>
            <td><asp:TextBox ID="txt12thMonth" runat="server" Width="120px" MaxLength="20" style="text-align:right;" AutoPostBack="true" OnTextChanged="Amount_OnTextChanged" onkeypress="fncInputNumericValuesOnly(event)" ></asp:TextBox></td>
        </tr>
        <tr style="background-color:#feffd6;" id="trnewBdg" runat="server">
            <td  style="text-align:right; padding:5px;">
                <b> Total Amt(US$)</b>
            </td>
            <td style ="text-align :right ;padding:5px;">
                <asp:Label ID="lblTotBdgDB" runat="server" style="font-weight:bold;"></asp:Label>
            </td>
        </tr>
        <tr style="background-color:#feffd6;" runat="server" visible ="false">
            <td  style="text-align:right;padding:5px;">
                <b> Pre. Annual Budget Amt(US$)</b>
            </td>
            <td style ="text-align :right ;padding:5px;">
                <asp:Label ID="lblAnnBdg" runat="server" style="font-weight:bold;"></asp:Label>
            </td>
        </tr>
       <%-- <tr id="trnewBdg" runat="server" style=" font-size :12px;"  >
            <td style="text-align:right;">
                <b> Annual Budget Amt(US$)</b>
            </td>
            <td style ="text-align :right ">
                <asp:Label ID="lblBudgetAmt" runat="server" style="font-weight:bold;"></asp:Label>
            </td>
        </tr>--%>
        <tr>
            <td colspan="2" style="text-align:left; font-weight:bold;">
             Comments    
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Width="99%" Height="80px" ></asp:TextBox>
            </td>
        </tr>
        </table>
                       <div style="padding:5px;">
                   <asp:Button ID="btnReAllocate"  runat="server" OnClick="btnReload_OnClick"  Text="Re-allocate Budget" style="padding:5px 15px;border:none;" CssClass="btn" />
           <asp:Button ID="btnSave"  runat="server" OnClick="btnSave_OnClick"  Text="Save Budget" style="padding:5px 15px;border:none;" CssClass="btn" />
   
                       </div>
                   </div>
               </td>
               <td>
                   <div style="font-size:13px;font-weight:bold;background-color:#bdf9fb;color:#272323;padding:4px; height:25px;">
                     <asp:UpdatePanel ID="upControls" runat="server">
                            <ContentTemplate>
                       <div style="margin:0px;padding:0px;float:left;"> 
                           <asp:button ID="btnExportTaskPopup" runat="server"  ToolTip="Import Budget Distribution" OnClick="btnExportTaskPopup_OnClick" ImageUrl="~/Modules/HRD/Images/export.png"  OnClientClick="return confirm('This will delete the existing records.Are you sure to import?');"/>
                           &nbsp;
                           <asp:ImageButton ID="btnAddTrackingTaskPopup" runat="server"  ToolTip="Add Budget Distribution" OnClick="btnAddTrackingTaskPopup_OnClick" ImageUrl="~/Modules/HRD/Images/add.png" />
                           &nbsp;
                           <asp:ImageButton ID="btnEditTrackingTask" runat="server"  ToolTip="Edit Budget Distribution" OnClick="btnEditTrackingTask_OnClick" ImageUrl="~/Modules/HRD/Images/AddPencil.gif" Visible="false" />
                           &nbsp;
                           <asp:ImageButton ID="btnDeleteTrackingTask" runat="server"  ToolTip="Delete Budget Distribution" OnClick="btnDeleteTrackingTask_OnClick" ImageUrl="~/Modules/HRD/Images/Close.gif"  Visible="false" OnClientClick="return confirm('Are you sure to delete?')" />
                           &nbsp;
                           <asp:Button ID="btnTemp" runat="server" OnClick="btnTemp_OnClick" style="display:none;" />
                            <asp:HiddenField ID="hfSelectedTaskID" runat="server" />
                       </div>

                        <%-- TrackingTask-----------------------------------------%>
                            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dvAddTrackingTask" visible="false" >
            <center>
            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:750px; padding :0px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:50px;  ;opacity:1;filter:alpha(opacity=100)">
            <center>
                <div style="font-size:15px;font-weight:bold;padding:4px;" class="text headerband">
                    Add Budget Allocation
                    <asp:ImageButton ID="btnCloseAddTrackingTaskPopup" runat="server" OnClick="btnCloseAddTrackingTaskPopup_OnClick"  ImageUrl="~/Modules/HRD/Images/Close.gif" style="float:right;"/>
                </div>
                
                <div id="divAddTrackingTask" runat="server" >
                           <table cellpadding="5" cellspacing="0" width="100%" border="0" style="border-collapse:collapse; text-align:left; border-collapse:collapse;">
                               <col width="120px" />
                               <col />
                               <tr>
                                <td>
                                    Allocation Type :
                                </td>
                            </tr>                                          
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlTaskType" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="ddlTaskType_OnSelectedIndexChanged" Enabled="false">
                                        <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Budgeted" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Unbudgeted"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>                                          
                               <tr>
                                   <td >
                                       <b>Allocation Description :</b>
                                       
                                   </td>
                               </tr>
                               <tr>
                                   <td>
                                       <asp:TextBox ID="txtTtDescription" runat="server" TextMode="MultiLine" Width="99%" Height="70px"></asp:TextBox>
                                   </td>
                               </tr>
                               <tr>
                                   <td><b> Budget Amount :</b><br />
                                       
                                   </td>                                   
                               </tr>
                               <tr>
                                   <td>
                                       <asp:TextBox ID="txtTtAmount" runat="server"></asp:TextBox>
                                   </td>
                               </tr>
                              <%-- <tr>
                                   <td> <b> Schedule for expenses :</b>
                                      
                                   </td>                                   
                               </tr>   
                               <tr>
                                   <td>
                                        <table cellpadding="2" cellspacing="0" border="0">
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
                                           <tr>
                                               <td><b>Jan</b></td>
                                               <td><b>Feb</b></td>
                                               <td><b>Mar</b></td>
                                               <td><b>Apr</b></td>
                                               <td><b>May</b></td>
                                               <td><b>Jun</b></td>
                                               <td><b>Jul</b></td>
                                               <td><b>Aug</b></td>
                                               <td><b>Sep</b></td>
                                               <td><b>Oct</b></td>
                                               <td><b>Nov</b></td>
                                               <td><b>Dec</b></td>
                                           </tr>
                                            <tr>
                                   
                                   <td>
                                        <asp:CheckBox ID="chkTtJan" runat="server" />
                                   </td>
                               
                                   
                                   <td>
                                       <asp:CheckBox ID="chkTtFeb" runat="server" />
                                   </td>
                               
                                   
                                   <td>
                                       <asp:CheckBox ID="chkTtMar" runat="server" />
                                   </td>
                              
                                   
                                   <td>
                                       <asp:CheckBox ID="chkTtApr" runat="server" />
                                   </td>
                              
                                   
                                   <td>
                                       <asp:CheckBox ID="chkTtMay" runat="server" />
                                   </td>
                              
                                   
                                   <td>
                                       <asp:CheckBox ID="chkTtJun" runat="server" />
                                   </td>
                              
                                   
                                   <td>
                                       <asp:CheckBox ID="chkTtJul" runat="server" />
                                   </td>
                               
                                   
                                   <td>
                                       <asp:CheckBox ID="chkTtAug" runat="server" />
                                   </td>
                                    
                                   
                                   <td>
                                       <asp:CheckBox ID="chkTtSep" runat="server" />
                                   </td>
                               
                              
                                   
                                   <td>
                                       <asp:CheckBox ID="chkTtOct" runat="server" />
                                   </td>
                              
                                   
                                   <td>
                                       <asp:CheckBox ID="chkTtNov" runat="server" />
                                   </td>
                              
                                   
                                   <td>
                                       <asp:CheckBox ID="chkTtDec" runat="server" />
                                   </td>
                               </tr>
                                       </table>
                                   </td>
                               </tr>      --%>
                               
                               <tr>
                                   <td>
                                       Modified By/On :     
                                   </td>
                               </tr>
                               <tr>
                                   <td>
                                       <asp:Label ID="lblTaskModifiedByOn" runat="server" style="font-weight:normal;"></asp:Label> 
                                   </td>
                               </tr>
                           </table>
                    <div style="text-align:center;padding:5px;">
                        <asp:Button ID="btnSaveTrackingTask" runat="server" Text="Save" CssClass="btn" OnClick="btnSaveTrackingTask_OnClick" />
                    </div>
                    <div style="text-align:center;padding:5px;">
                        <asp:Label ID="lblMsgTrackingTask" runat="server" CssClass="error"></asp:Label>
                    </div>
                        
                       </div>
            </center>
            </div>
            </center>
        </div>

                        <%-- Export TrackingTask-----------------------------------------%>
                                <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dvExportTask" visible="false" >
                                <center>
                                <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                                <div style="position :relative; width:450px; padding :0px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:140px;  ;opacity:1;filter:alpha(opacity=100)">
                                <center>
                                    <div style="font-size:15px;font-weight:bold;padding:4px;" class="text headerband">
                                        Import Budget Allocation
                                        <asp:ImageButton ID="btnCloseExportPopup" runat="server" OnClick="btnCloseExportPopup_OnClick"  ImageUrl="~/Modules/HRD/Images/Close.gif" style="float:right;"/>
                                    </div>
                                    <table cellpadding="5" cellspacing="5" border="0" width="100%">
                                        <tr>
                                            <td> Select  Vessel </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td> 
                                                <asp:DropDownList ID="ddlVesselExport" runat="server" style="width:300px;"></asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnExport" runat="server"  Text="Import" CssClass="btn" OnClick="btnExport_OnClick"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align:center;">
                                                <asp:Label ID="lblMsgExport" runat="server" style="color:red;font-weight:bold;"></asp:Label>
                                            </td>
                                            
                                        </tr>
                                    </table>
                                    </center>
                                </div>
                                </center>
                                </div>
                       </ContentTemplate>
                    </asp:UpdatePanel>
                       Budget Distribution Details
                       
                   </div>
                       <div id="DivTrackingTaskList" runat="server">
                           <div style="overflow-x:hidden;overflow-y:scroll;height:35px; width:100%;">
                               <table cellpadding="4" cellspacing="0" width="100%" border="1" style="border-collapse:collapse; text-align:center; border-collapse:collapse;">
                                   <col width="30px" />
                                   <col />
                                   <col width="150px" />
                                   <col width="20px" />
                                   <%--<col width="40px" />
                                   <col width="40px" />
                                   <col width="40px" />
                                   <col width="40px" />
                                   <col width="40px" />
                                   <col width="40px" />
                                   <col width="40px" />
                                   <col width="40px" />
                                   <col width="40px" />
                                   <col width="40px" />
                                   <col width="40px" />
                                   <col width="40px" />--%>
                                   
                                   <tr class="header">
                                       <td style="height:25px; vertical-align:middle;">&nbsp;</td>
                                        <td style="height:25px; vertical-align:middle; text-align:left">Description</td>
                                        <td style="height:25px; vertical-align:middle;">Amount</td>
                                         <td style="width:20px;">&nbsp;</td>
                                       <%--<td>Jan</td>
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
                                       <td>Dec</td>--%>
                                       
                                   </tr>
                               </table>
                            </div>
                           <asp:UpdatePanel ID="UpTask" runat="server" UpdateMode="Conditional">
                                   <ContentTemplate>
                           <div style="overflow-x:hidden;overflow-y:scroll;height:427px;border:solid 0px red;">
                               
                                   
                                            <table cellpadding="4" cellspacing="0" width="100%" border="1" style="border-collapse:collapse; text-align:center; border-collapse:collapse;">
                                                <colgroup>
                                                    <col width="30px" />
                                                    <col />
                                                    <col width="150px" />
                                                    <col width="20px" />
                                                    <%--<col width="40px" />
                               <col width="40px" />
                               <col width="40px" />
                               <col width="40px" />
                               <col width="40px" />
                               <col width="40px" />
                               <col width="40px" />
                               <col width="40px" />
                               <col width="40px" />
                               <col width="40px" />
                               <col width="40px" />
                               <col width="40px" />--%>
                                                </colgroup>
                           <asp:Repeater ID="rptTrackingTaskList" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <input type="radio" name="rdo" class="selRadio" TaskID='<%#Eval("TaskID") %>' onclick="click_btn(this);" />
                                                        </td>
                                                        <td align="left"><span class='<%# ((Eval("budgeted").ToString()=="True")?"Budgeted":"UnBudgeted") %>'><%# ((Eval("budgeted").ToString()=="True")?"B":"U") %></span><%#Eval("TaskDescription") %></td>
                                                        <td align="right"><%#Eval("Amount") %>
                                                            <asp:HiddenField ID="hfTaskID" runat="server" Value='<%#Eval("TaskID") %>' />
                                                        </td>
                                                        <%--<td><img src="Images/check_white.png" style='display:<%# ((Eval("Jan").ToString()=="True")?"":"none") %>'  /></td>
                                           <td><img src="Images/check_white.png" style='display:<%# ((Eval("Feb").ToString()=="True")?"":"none") %>'  /></td>
                                           <td><img src="Images/check_white.png" style='display:<%# ((Eval("Mar").ToString()=="True")?"":"none") %>'  /></td>
                                           <td><img src="Images/check_white.png" style='display:<%# ((Eval("Apr").ToString()=="True")?"":"none") %>'  /></td>
                                           <td><img src="Images/check_white.png" style='display:<%# ((Eval("May").ToString()=="True")?"":"none") %>'  /></td>
                                           <td><img src="Images/check_white.png" style='display:<%# ((Eval("Jun").ToString()=="True")?"":"none") %>'  /></td>
                                           <td><img src="Images/check_white.png" style='display:<%# ((Eval("Jul").ToString()=="True")?"":"none") %>'  /></td>
                                           <td><img src="Images/check_white.png" style='display:<%# ((Eval("Aug").ToString()=="True")?"":"none") %>'  /></td>
                                           <td><img src="Images/check_white.png" style='display:<%# ((Eval("Sep").ToString()=="True")?"":"none") %>'  /></td>
                                           <td><img src="Images/check_white.png" style='display:<%# ((Eval("Oct").ToString()=="True")?"":"none") %>'  /></td>
                                           <td><img src="Images/check_white.png" style='display:<%# ((Eval("Nov").ToString()=="True")?"":"none") %>'  /></td>
                                           <td><img src="Images/check_white.png" style='display:<%# ((Eval("Dec").ToString()=="True")?"":"none") %>'  /></td>--%>
                                                        <td style="width:20px;">&nbsp;</td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                            </table>     
                           </div>
                           <div style="overflow-x:hidden;overflow-y:scroll;height:30px;border:solid 0px red; background-color:#C2C2C2">
                               <table cellpadding="4" cellspacing="0" width="100%" border="0" style="border-collapse:collapse; text-align:center; border-collapse:collapse;">
                                   <colgroup>
                                       <col width="30px" />
                                       <col width="150px" />
                                       <col width="20px" />
                                       <%--<col width="40px" />
                               <col width="40px" />
                               <col width="40px" />
                               <col width="40px" />
                               <col width="40px" />
                               <col width="40px" />
                               <col width="40px" />
                               <col width="40px" />
                               <col width="40px" />
                               <col width="40px" />
                               <col width="40px" />
                               <col width="40px" />--%>
                                       <tr>
                                           <td style="text-align:right; vertical-align:middle;height:26px; font-size:16px;"><b>Distribution Total : </b></td>
                                           <td style="text-align:right; font-size:16px;">
                                               <asp:Label ID="lblTotalTaskAmount" runat="server"></asp:Label>
                                           </td>
                                           <td style="width:20px;">&nbsp;</td>
                                           <%--<td></td>
                                       <td></td>
                                       <td></td>
                                       <td></td>
                                       <td></td>
                                       <td></td>
                                       <td></td>
                                       <td></td>
                                       <td></td>
                                       <td></td>
                                       <td></td>
                                       <td></td>--%>
                                       </tr>
                                   </colgroup>
                                </table>
                           </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                       </div>
               </td>
               
           </tr>
       </table>
    <div style="padding:5px;">
        <asp:Label ID="lblmsg" runat="server" style=" font-size :12px; color :Red ; font-weight :bold "></asp:Label>
    </div>
            

        
       
    </td>
    </tr>
    </table>

        
        <script src="JS/jquery-1.4.2.min.js" type="text/javascript"></script>
        <script type="text/javascript">
            function click_btn(ctrl) {
                $("#hfSelectedTaskID").val($(ctrl).attr("TaskID"));
                $("#btnTemp").click();
            }
        </script>
    </form>
</body>
</html>


