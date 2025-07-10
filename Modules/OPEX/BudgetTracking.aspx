<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BudgetTracking.aspx.cs" Inherits="BudgetTracking" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
    </title>
    <link href="CSS/Budgetstyle.css?qssadsddsdd" rel="Stylesheet" type="text/css" />
     <style type="text/css">
      
    body
    {
        font-family:Arial;
        font-size:13px !important;
        margin:0px;
    }
    td,th
    {
   font-size:13px !important;
    }
    *
    {
        box-sizing:border-box;
    }
    .tblMonth td{
        text-align:center;
        font-weight:bold;
    }
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
    .btn
    {
        background-color:#394b65;
        color:White;
        border:none;
        padding:5px 10px 5px 10px;        
    }
    
    .btn:hover
    {
        background-color:#0052CC;
    }
    .right_align
    {
        text-align:right;
    }
    
    .hover_highlight_cat
     {
         vertical-align:middle;
         color:Red;
         height:25px;
         cursor:pointer;
         vertical-align:middle;
         border-bottom:solid 1px #dddddd;
     }
    .hover_highlight_cat_active
    {
        background-color:yellow;
        color:Black;
    }
    
    .hover_highlight_job
     {
         vertical-align:middle;
         color:blue;
         height:25px;
         cursor:pointer;
         border-bottom:solid 1px #dddddd;
     }
    .hover_highlight_job_active
    {
        background-color:yellow;
        color:Black;
    }
 
    .hover_highlight_subjob
     {
         vertical-align:middle;
         color:Black;
         height:25px;
         cursor:pointer;
         border-bottom:solid 1px #dddddd;
     }
     
    .hover_highlight_subjob_active
    {
        background-color:yellow;
    }
    
    .columnHead
    {
        background-color:#c2c2c2;
        height:50px;
        color:Black;
    }
    .newinput
    {
        border:solid 1px #c2c2c2;
        font-size:12px;
        padding:2px;
        text-align:right;
    }
    .nav
    {
        margin:0px;
        padding:0px;
        width:100%;
    }
    .inactive_tab
    {
        padding:4px;
        background-color:White;
        border:solid 1px #4D70DB;
        border-bottom:solid 1px white;
        display:block;
        position:relative;
        float:left;
        margin-right:4px;
        color:Black;
    }
     .inactive_tab a
    {
        color:Grey;
    }
    .active_tab
    {
        padding:4px;
        background-color:#4D70DB;
        border:solid 1px #4D70DB;
        display:block;
        position:relative;
        float:left;
        margin-right:4px;
        border-bottom:solid 1px #4D70DB;
    }
     .active_tab a
    { color:White; }
    .bl
    {   border-left:solid 1px #dddddd;height:25px;}
    .br
    {   border-right:solid 1px #dddddd;height:25px;}

    
    input[type='text'],select
    {
        line-height:20px;
        height:20px;
        padding-left:5px;
        border:solid 1px #c2c2c2;
        vertical-align:middle;
    }
    
    td{
        vertical-align:middle;
    }
    .table-centered tr td
    {
        text-align:center !important;
    }
    </style>
</head>
<body >
    <form id="form1" runat="server">     
    <div class="b-PageHeading">
        <div class="PageHeader">
                Budget Tracking           
        </div>
        <div style="background-color:#f6fafd;color:#222;">
            <table cellpadding="5"cellspacing="0" width="100%" border="0" style="border-collapse:collapse;margin-left:10px; font-size:14px;" >
                            <col  width="90px"/>
                            <col  width="280px"/>
                            <col  width="70px"/>
                            <col  width="280px"/>
                            <col  width="110px"/>
                            <col  width="65px"/>
                            <col  width="95px"/>
                            <col  width="200px"/>                          
                            <col  />
                            <tr >
                                <td>Company :</td>
                                <td><asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" AutoPostBack="true"  Width="250px" ></asp:DropDownList></td>
                                <td>Vessel :</td>
                                <td><asp:DropDownList ID="ddlShip" runat="server"  OnSelectedIndexChanged="ddlShip_OnSelectedIndexChanged" AutoPostBack="false"  Width="250px"></asp:DropDownList></td>
                                <td>Budget Year : </td>
                                <td><asp:TextBox runat="server" ID="lblBudgetYear" Enabled="false" Width="50px" ></asp:TextBox></td>                                   
                                <td>Category :</td>
                                <td style="text-align:left">
                                    <asp:DropDownList ID="ddlBudgetType" runat="server" Width="180px" OnSelectedIndexChanged="ddlBudgetType_OnSelectedIndexChanged" AutoPostBack="false"  ></asp:DropDownList>
                                </td> 
                                <td><asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_OnClick" CssClass="btn" Width="60px" /></td>                               
                            </tr>
                        </table> 
        </div>
        <table cellspacing="0" cellpadding="0" style="border-collapse:collapse; width:100%; height:34px; background-color:#5f5f5f;color:white;" class="rpt-table">
                <col />
                <col width="110px" />
                <col width="110px" />
                <col width="110px" />
                <col width="110px" />
                <col width="95px" />
                <col width="300px" />
                <col width="60px" />
            <tr>
                
                <td style="text-align:left;color:white;vertical-align:central;" >Description  </td>
                <td style="text-align:right;color:white;vertical-align:central;"> Budget Amt. </td>
                <td style="text-align:right;color:white;vertical-align:central;"> Cons Amt. </td>
                <td style="text-align:right;color:white;vertical-align:central;"> Proj Amt. </td>
                <td style="text-align:right;color:white;vertical-align:central;"> Var Amt. </td>
                <td style="text-align:right;color:white;vertical-align:central;"> Var(%) </td>
                <td style="text-align:left;color:white;vertical-align:central;"> Remarks </td>
                <td style="text-align:right;color:white;vertical-align:central;"> Action </td>
            </tr>
        </table>
    </div>
    <div class="b-content">
    <asp:Repeater ID="rptMinorCategory" runat="server">
                    <ItemTemplate>
                        <table cellspacing="0" cellpadding="0" style="border-collapse:collapse; width:100%;" class="rpt-table" >
                            <col />
                            <col width="110px" />
                            <col width="110px" />
                            <col width="110px" />
                            <col width="110px" />
                            <col width="95px" />
                            <col width="300px" />
                            <col width="60px" />
                            <tr class="group-1">
                                <td style="text-align:left;" > <%# Eval("MinorCat") %></td>
                                <td> <%# Eval("BudgetAmount") %></td>                                
                                <td> <%# Eval("ConsumedAmount") %></td>                                
                                <td ></td>
                                <td><span> <%# Common.CastAsDecimal(Eval("BudgetAmount"))-Common.CastAsDecimal(Eval("ConsumedAmount"))  %>  </span></td>
                                <td><span> <%# GetBudgetVariance(Eval("BudgetAmount"),Eval("ConsumedAmount"))  %> </td>
                                <td ></td>
                                <td ></td>
                            </tr>                       
                        <asp:Repeater ID="rptAccoutDetails" runat="server" DataSource=<%# BindRptAccountDetails( Eval("CoCode").ToString(),Eval("Vess").ToString(), Common.CastAsInt32( Eval("MinCatID"))) %> > 
                        <ItemTemplate>
                           
                            <tr class="group-2">
                                <td style="text-align:left;" >
                                    <span class="hrow"><%#Eval("AccountNumber")%></span> : <span class="hrow"><%#Eval("AccountName")%></span>
                                    <asp:HiddenField ID="hfCompany" runat="server" Value='<%#Eval("CoCode") %>' />
                                    <asp:HiddenField ID="hfVessel" runat="server" Value='<%#Eval("Vess") %>' />
                                    <asp:HiddenField ID="hfAccountID" runat="server" Value='<%#Eval("AccountID") %>' />
                                </td>
                                <td style="text-align:right;"><span class="hrow"> <%# Common.CastAsDecimal( Eval("BudgetAmount"))%></span></td>
                                <td style="text-align:right;"><span class="hrow"> <%# Common.CastAsDecimal( Eval("ConsumedAmount"))%></span></td>
                                <td></td>
                                <td><span> <%# Common.CastAsDecimal(Eval("BudgetAmount"))-Common.CastAsDecimal(Eval("ConsumedAmount"))  %>  </span></td>
                                <td><span> <%# GetBudgetVariance(Eval("BudgetAmount"),Eval("ConsumedAmount"))  %> </td>
                                <td></td>
                                <td style="text-align:center">
                                    <asp:ImageButton ID="btnOpenAddTaskPopup" runat="server" ImageUrl="~/Images/add.png" OnClick="btnOpenAddTaskPopup_OnClick" ToolTip="Add New Unbudgeted Task."/>
                                </td>
                                
                            </tr>
                                                 
                            <asp:Repeater ID="rptTaskDetails" runat="server" DataSource=<%# BindTaskDetails(Eval("CoCode").ToString(),Eval("Vess").ToString(), Common.CastAsInt32( Eval("MinCatID")),Common.CastAsInt32( Eval("AccountID"))) %> >
                                            <ItemTemplate>
                                                
                                                    <tr class="group-3">
                                                        
                                                        <td style="text-align:left;padding-left:0px;">
                                                            <span class='<%# ((Eval("budgeted").ToString()=="True")?"Budgeted":"UnBudgeted") %>'>
                                                                <%# ((Eval("budgeted").ToString()=="True")?"B":"U") %>
                                                            </span>
                                                            &nbsp;<asp:Label id="lblTaskDesc" runat="server" Text='<%#Eval("TaskDescription")%> '></asp:Label>
                                                            <asp:HiddenField ID="hfTaskID" runat="server" Value='<%#Eval("TaskID")%>' />
                                                            <asp:HiddenField ID="hfClosedBy" runat="server" Value='<%#Eval("ClosedBy")%>' />
                                                            <asp:HiddenField ID="hfAccountNoName" runat="server" Value='<%# Eval("AccountNumber").ToString()+" : "+Eval("AccountName").ToString()%>' />
                                                            
                                                        </td>
                                                        <td><span><%#Eval("BudgetAmount")%>  </span></td>
                                                        <td>
                                                            <span><%# Common.CastAsDecimal( Eval("ConsumedAmount"))%>  </span>
                                                            <asp:HiddenField ID="hfTaskConsumedAmount" runat="server" Value='<%# Common.CastAsDecimal( Eval("ConsumedAmount"))%>' />
                                                        </td>
                                                        <td><span>0.0  </span></td>
                                                        <td><span> <%# Common.CastAsDecimal(Eval("BudgetAmount"))-Common.CastAsDecimal(Eval("ConsumedAmount"))  %>  </span></td>
                                                        <%--<td><span> <%#  Common.CastAsDecimal( ((Common.CastAsDecimal(Eval("BudgetAmount"))-Common.CastAsDecimal(Eval("ConsumedAmount")))*100)/ Common.CastAsDecimal(Eval("BudgetAmount"))).ToString("0.00") %>  </span></td>--%>
                                                        <td><span> <%# GetBudgetVariance(Eval("BudgetAmount"),Eval("ConsumedAmount"))  %> </td>
                                                        <td style="text-align:left;"> <%#Eval("ClosureRemarks")%> </td>
                                                        <td style="text-align:center">
                                                            <asp:ImageButton ID="btnOpenTaskDetailsPopup" runat="server" ImageUrl="~/Images/magnifier.png"  ToolTip="View Purchase Orders." OnClick="btnOpenTaskDetailsPopup_OnClick"/>
                                                        </td>
                                                    </tr>
                                                    
                                            </ItemTemplate>
                                        </asp:Repeater>
                            
                            
                        </ItemTemplate>
                    </asp:Repeater>
                       
                        </table> 
                             
                    </ItemTemplate>
                </asp:Repeater>
    </div>

        <div class="b-PageFooter">
            <table cellspacing="0" cellpadding="0" style="border-collapse:collapse; width:100%; height:34px; background-color:#5f5f5f;color:white;" class="rpt-table">
               <col />
                <col width="110px" />
                <col width="110px" />
                <col width="110px" />
                <col width="110px" />
                <col width="95px" />
                <col width="300px" />
                <col width="60px" />
            <tr>                
                <td style="text-align:left;" ></td>
                <td class="" style="color:white;"> <asp:Label ID="lblTotalAmount" runat="server"></asp:Label> </td>
                <td class="" style="color:white;"> <asp:Label ID="lblTotalConsumedAmount" runat="server"></asp:Label> </td>
                <td class="">  </td>
                <td class="" style="color:white;"> <asp:Label ID="lblTotalVarianceAmount" runat="server"></asp:Label> </td>
                <td class="" style="color:white;"> <asp:Label ID="lblTotalVariancePercentage" runat="server"></asp:Label> </td>
                <td class=""> </td>
                <td class=""> </td>
            </tr>
        </table>
            <table cellspacing="0" cellpadding="0" border="0" style="border-collapse:collapse; width:100%; height:34px; background-color:#5f5f5f;color:white;font-weight:bold;" class="">
                <col width="33%"  />
                <col width="33%"  />
                <col  />
                <tr>
                    <td style="text-align:center;">
                        Total Budgeted : <asp:Label ID="lblTotalBudgetdAmount" runat="server"></asp:Label>
                    </td>
                    <td style="text-align:center;">
                        Total Unnudgeted : <asp:Label ID="lblTotalUnbudgetdAmount" runat="server"></asp:Label>
                    </td>                    
                    <td style="text-align:center;">
                        Ship Total : <asp:Label ID="lblShipTotal" runat="server"></asp:Label>
                    </td>                    
                </tr>
            </table>
        </div> 

        <%----------------------------------------------------------------------------------------------------------------------------------------------------%>
        <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dvTaskDetails" visible="false" >
            <center>
            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:80%; padding :0px; text-align :center; border :solid 5px #c2c2c2;  background : white; z-index:150;top:80px;opacity:1;filter:alpha(opacity=100)">
            <center>
                <div style="font-size: 15px;padding: 6px;background-color: #f7990d;color: white;">
                    Linked Purchase Orders                   
                </div>
                <div style="font-size:16px; padding:5px; background-color:#dadada" >
                    <table width="100%" cellpadding="3">
                        <tr>
                            <td style="width:140px;">Account Name </td>
                            <td style="width:10px">:</td>
                            <td><asp:Label ID="lblAccountNoName" runat="server" ></asp:Label>

                                <asp:LinkButton ID="btnOpenClosuerPopup" runat="server" ForeColor="Red"  Text="Close This Task" style="float:right;" OnClick="btnOpenClosuerPopup_OnClick" />
                        
                            </td>
                        </tr>
                        <tr>
                            <td>Task Details </td>
                            <td style="width:10px">:</td>
                            <td><asp:Label  ID="lblTaskDetails" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Expense Schedule</td>
                            <td style="width:10px">:</td>
                            <td><table cellpadding="3" cellspacing="0" border="1" width="100%" class="table-centered bordered" style="border-collapse:collapse;" >
                                           <col width="60px" />
                                           <col width="60px" />
                                           <col width="60px" />
                                           <col width="60px" />
                                           <col width="60px" />
                                           <col width="60px" />
                                           <col width="60px" />
                                           <col width="60px" />
                                           <col width="60px" />
                                           <col width="60px" />
                                           <col width="60px" />
                                           <col width="60px" />
                                           <tr>
                                               <td><asp:Image ImageUrl="~/Images/check_g.png" ID="imgJan" runat="server"  Visible="false" style="float:left;" />Jan</td>
                                               <td><asp:Image ImageUrl="~/Images/check_g.png"  ID="imgFeb" runat="server"  Visible="false" style="float:left;" />Feb</td>
                                               <td><asp:Image ImageUrl="~/Images/check_g.png"  ID="imgMar" runat="server"  Visible="false" style="float:left;"/>Mar</td>
                                               <td><asp:Image ImageUrl="~/Images/check_g.png"  ID="imgApr" runat="server" Visible="false"  style="float:left;"/>Apr</td>
                                               <td><asp:Image ImageUrl="~/Images/check_g.png"  ID="imgMay" runat="server"  Visible="false" style="float:left;"/>May</td>
                                               <td><asp:Image ImageUrl="~/Images/check_g.png"  ID="imgJun" runat="server"  Visible="false" style="float:left;"/>Jun</td>
                                               <td><asp:Image ImageUrl="~/Images/check_g.png"  ID="imgJul" runat="server"  Visible="false" style="float:left;"/>Jul</td>
                                               <td><asp:Image ImageUrl="~/Images/check_g.png"  ID="imgAug" runat="server"  Visible="false" style="float:left;"/>Aug</td>
                                               <td><asp:Image ImageUrl="~/Images/check_g.png"  ID="imgSep" runat="server"  Visible="false" style="float:left;"/>Sep</td>
                                               <td><asp:Image ImageUrl="~/Images/check_g.png"  ID="imgOct" runat="server"  Visible="false" style="float:left;"/>Oct</td>
                                               <td><asp:Image ImageUrl="~/Images/check_g.png"  ID="imgNov" runat="server" Visible="false"  style="float:left;"/>Nov</td>
                                               <td><asp:Image ImageUrl="~/Images/check_g.png"  ID="imgDec" runat="server"  Visible="false" style="float:left;"/>Dec</td>
                                           </tr>
                                       </table>
                            </td>
                        </tr> 
                    </table>
                </div>
                 <table cellpadding="5" cellspacing="0" border="0" width="100%" class="table-centered rpt-table" >
                     <tr style="font-size:15px;background-color:#fdd291">
                         <td>Budget Amount</td>
                         <td>Consumed Amount</td>
                         <td>Projected Amount</td>
                         <td>Var Amount</td>
                         <td>Var (%)</td>
                     </tr>
                     <tr>
                         <td><asp:Label  ID="lblTaskAmount" runat="server" ></asp:Label></td>
                         <td><asp:Label  ID="lblConsAmount" runat="server" ></asp:Label></td>
                         <td><asp:Label  ID="lblProjAmount" runat="server" ></asp:Label></td>
                         <td><asp:Label  ID="lblVarAmount" runat="server" ></asp:Label></td>
                         <td><asp:Label  ID="lblVarPer" runat="server" ></asp:Label></td>
                     </tr>

                     </table>
                    
                <div style="overflow-x:hidden;overflow-y:scroll;height:34px;">
                    <table cellpadding="4" cellspacing="0" border="0" width="100%" class="rpt-table">
                        <col width="150px" />
                        <col />
                        <col width="100px" />
                        <col width="100px" />
                        <col width="50px" />
                        <tr style="background-color:#5f5f5f;color:white; height:34px;">
                            <td style="text-align:left;color:white;"> PO#</td>
                            <td style="text-align:left;color:white;"> Supplier Name</td>
                            <td style="text-align:center;color:white;"> PO Date</td>
                            <td style="text-align:center;color:white;"> PO Amount</td>                            
                            <td style="color:white;"> </td>
                        </tr>
                    </table>
                </div>
                <div style="overflow-x:hidden;overflow-y:scroll;height:300px;">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="rpt-table">
                        <col width="150px" />
                        <col />
                        <col width="100px" />
                        <col width="100px" />
                        <col width="50px" />
                        <asp:Repeater ID="rptTaskDetails" runat="server">
                            <ItemTemplate>
                                <tr class="group-3">
                                    <td style="text-align:center"><%#Eval("BidPoNum") %></td>
                                    <td style="text-align:left"><%#Eval("SupplierName") %></td>
                                    <td><%# Common.ToDateString(Eval("PoDate")) %></td>
                                    <td><%#Common.CastAsDecimal( Eval("PoAmount")) %></td>
                                    <td style="text-align:center">
                                        <a href='VeiwRFQDetailsForApproval.aspx?BidId=<%#Eval("BidID") %>'  target="_blank"><img  src="Images/magnifier.png"/> </a>                                        
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <div style="padding:5px; background-color:#fae85a;">
                    <table width="100%" cellpadding="3">
                        <tr>
                            <td style="width:130px;">Closed By/On </td>
                            <td style="text-align:left">:&nbsp;<asp:Label ID="lblTaskClosedBy" runat="server" Text="" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Closure Comments </td>
                            <td>:&nbsp;<asp:Label ID="lblclosurecomments" runat="server" Text=""></asp:Label></td>
                        </tr>
                    </table>
                    
                     
                </div>
                  <div style="text-align:center;padding:5px;">
                        <asp:Button ID="Button2" runat="server" Text="Close"  CssClass="btn" OnClick="btnClosedetails_OnClick" />
                    </div>
            </center>
            </div>
            </center>
        </div>

        <%-----------------------------------------------------------------------------------------------------------------------%>
         <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dvClosure" visible="false" >
            <center>
            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:500px;padding :0px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:120px;opacity:1;filter:alpha(opacity=100)">
            <center>
                <div style="font-size:15px;font-weight:bold;padding:4px;" class="header">
                    Closure
                    <asp:ImageButton ID="btnCloseClosurePopup" runat="server" OnClick="btnCloseClosurePopup_OnClick"  ImageUrl="~/Images/Close.gif" style="float:right;"/>
                </div>
                <table cellpadding="3" cellspacing="3" border="0" width="100%">
                    <tr>
                        <td> <b> Ramarks</b></td>
                    </tr>
                    <tr>
                        <td> <asp:TextBox ID="txtClosureRemarks" runat="server" TextMode="MultiLine" Width="99%" Height="100px" ></asp:TextBox> </td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            <asp:Button ID="btnSaveClosure" runat="server" Text="Closure" OnClick="btnSaveClosure_OnClick" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            <asp:Label ID="lblMsgClosure" runat="server" style="color:red;"></asp:Label>
                        </td>
                    </tr>
                </table>
            </center>
            </div>
            </center>
        </div>


           <%-- TrackingTask-----------------------------------------%>
           <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dvAddTrackingTask" visible="false" >
            <center>
            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:750px; padding :0px; text-align :center; border :solid 5px #c2c2c2; background : white; z-index:150;top:110px;opacity:1;filter:alpha(opacity=100)">
            <center>
                <div style="font-size: 15px;padding: 6px;background-color: #f7990d;color: white;">
                    Add New Unbudgeted Task
                </div>                
                <div >
                    <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse:collapse; text-align:left; border-collapse:collapse;">
                               <col width="120px" />
                               <col />
                               <tr>
                                   <td >
                                      Task Description :                                       
                                   </td>
                               </tr>
                               <tr>
                                   <td>
                                       <asp:TextBox ID="txtTtDescription" runat="server" TextMode="MultiLine" Width="99%" Rows="2"></asp:TextBox>
                                   </td>
                               </tr>
                               <tr>
                                   <td>
                                       Budget Amount :                                       
                                   </td>                                   
                               </tr>  
                               <tr>
                                   <td >
                                      <asp:TextBox ID="txtTtAmount" runat="server"></asp:TextBox>                                   
                                   </td>
                               </tr>                             
                              <%-- <tr>
                                   <td>
                                       Expenses scheduled for months :                                      
                                   </td>                                   
                               </tr>   
                               <tr>
                                   <td>
                                        <table cellpadding="4" cellspacing="0" border="0" class="rpt-table table-centered" style="text-align:center;width:100%;">
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
                                           <tr class="group-3" >
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
                               </tr> --%>                                              
                           </table>
                    <div style="text-align:center;padding:5px;">
                        <asp:Button ID="btnSaveTrackingTask" runat="server" Text="Save" CssClass="btn" OnClick="btnSaveTrackingTask_OnClick" />
                        <asp:Button ID="btnClsose1" runat="server" Text="Close"  CssClass="btn" OnClick="btnCloseAddTrackingTaskPopup_OnClick" />
                    </div>
                    <div style="text-align:center;padding:5px; background-color:#fad775">
                       &nbsp; <asp:Label ID="lblMsgTrackingTask" runat="server" CssClass="error"></asp:Label>
                    </div>
                        
                </div>
            </center>
            </div>
            </center>
        </div>

    </form>
</body>
</html>

