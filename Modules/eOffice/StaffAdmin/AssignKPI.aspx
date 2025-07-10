<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssignKPI.aspx.cs" Inherits="emtm_StaffAdmin_AssignKPI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link rel="Stylesheet" type="text/css" href="style.css"  />
    <link rel="Stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />
    <style type="text/css">
        table
        {
            border-collapse:collapse;
        }
        td,th
        {
            padding:5px;
        }
        
        .striped tr:nth-child(odd) td
        {
            background-color:#eeeeee !important;
        }
        .striped tr:nth-child(even) td
        {
            border:#ffffff !important;
        }
        .bordered tr td
        {
            border:solid 1px #e4e4e4;
        }
        .bordered tr td
        {
            border:solid 1px #e4e4e4;
        }
        .bordered tr th
        {
            background-color:#bed9ed;
            color:#1b70b0;
            font-size:14px;
            font-weight:normal;
            border:solid 1px #e4e4e4;
            height:25px;
        }

      
        .action {
            cursor:pointer;
            font-size:12px ;
            color:orange;
        }
          .action:hover {
            color:red;
        }
          input[type='text'],textarea
          {
              height:25px;
              width:99% !important;
              line-height:25px;
              padding-left:5px;
              padding-right:5px;
              border:solid 1px #dedede !important;
          }
          textarea
          {
              height: auto !important;
          }
       .control-label
       {
           color:#333;
           font-size:13px;
       }
       .modalpop
       {
           background-color:white;
           position:fixed;
           left:20%;
           right:20%;
           top:10%;
           border:solid 5px orange;
       }
        .btn {
            height: 35px;
            border: none;
            width: 100px !important;
            background-color: #f8852e;
            color: white;
        } 
    </style>
</head>
<body >
    <form id="form1" runat="server">
        <div style="position:fixed;width:100%; height:100%;left:0px;top:0px;background-color:rgba(0,0,0,0.5)" runat="server" id="dvmodal" visible="false" ></div>
     <div style="padding:10px; text-align:center; font-size:15px; background-color:#1b70b0;color:white; "> Assign KPI to KRA</div>
         <table width="100%" border="0" cellpadding="0" cellspacing="0" class="bordered" >
                        <tr>
                         <td style="width:100px;">KRA Name </td>
                         <td colspan="3" style="text-align:left">: <asp:Label runat="server" ID="lblJRName" Font-Bold="true"></asp:Label></td>
                     </tr>
                     <tr>
                         <td style="width:70px;">Peap Level </td>
                         <td style="text-align:left; width:300px;">: <asp:Label runat="server" ID="lblPeapLevelName" Font-Bold="true"></asp:Label></td>
                         <td style="width:60px;">Position </td>
                         <td style="text-align:left">: <asp:Label runat="server" ID="lblPositionName" Font-Bold="true"></asp:Label></td>
                         </tr>
                  
         </table>
    <div>
        <table width="100%">
            <tr>
                <td style="text-align:center;font-size:17px;">Remaining KPI</td>
                <td style="width:120px;"></td>
                <td style="text-align:center;font-size:17px;">Linked KPI</td>
            </tr>
            <tr>

                <td style="vertical-align:top; ">
                    
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="bordered striped" >
                     <thead>
                         <tr>
                             <th style="width:50px;">Select</th>
                             <th style="width:400px; text-align:left;">KPI Name</th>
                         </tr>
                     </thead>
                    </table>
                    <div style="height:500px; overflow-y:scroll;">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="bordered" >
                     <tbody>
                         <asp:Repeater runat="server" ID="rptRemainingKPI">
                             <ItemTemplate>
                                 <tr>
                                     <td style="text-align:center;width:50px;"> <asp:CheckBox runat="server" ID="chkkpiid" CssClass='<%#Eval("EntryId")%>'  /></td>
                                     <td style="width:400px;"><b style="color:brown"><%#Eval("sno")%></b> - <%#Eval("KPIName")%></td>
                                 </tr>
                             </ItemTemplate>
                         </asp:Repeater>
                     </tbody>
                    </table>
                        </div>
                </td>
                <td style="position:relative;">
                    <div style="position:absolute;margin-top:0%;">
                        <asp:Button runat="server" CssClass="btn" Text=">>" OnClick="btnAssign_Click" />
                        <br /><br />    
                        <asp:Button runat="server" CssClass="btn" Text="<<"  OnClick="btnRevoke_Click" />
                    </div>
                </td>
                <td style="vertical-align:top;">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="bordered" >
                     <thead>
                         <tr>
                             <th style="width:50px;">Select</th>
                             <th style="width:400px; text-align:left;">KPI Name</th>
                         </tr>
                     </thead>
                        </table>
                    <div style="height:500px; overflow-y:scroll;">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="bordered" >
                    
                     <tbody>
                         <asp:Repeater runat="server" ID="rptLinkedKPI">
                             <ItemTemplate>
                                 <tr>
                                     <td style="text-align:center;width:50px;"> <asp:CheckBox runat="server" ID="chkkpiid" CssClass='<%#Eval("EntryId")%>' /></td>
                                     <td style="width:400px;"><b style="color:brown"><%#Eval("sno")%></b> - <%#Eval("KPIName")%></td>
                                 </tr>
                             </ItemTemplate>
                         </asp:Repeater>
                     </tbody>
                    </table>
                        </div>
                </td>
            </tr>
        </table>
     </div>
     <div>
         
     </div>
     <%--    <div runat="server" id="dvEditKPI"  class="modalpop" visible="false"  >
            <table width="100%"  border="0">
                <tr>
                    <td style="text-align:center; background-color:#0372aa;font-size:14px;padding:7px;color:white;">Assign New KPi</td>
                </tr>
                <tr>
                    <td class="control-label">KPI Name :</td>
                </tr>
                <tr>
                    <td><asp:TextBox runat="server" ID="lblKPIName"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="control-label">Description :</td>
                </tr>
                <tr>
                    <td><asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" Rows="5"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="control-label">Linked KPI :</td>
                </tr>
                <tr>
                    <td><asp:TextBox runat="server" ID="TextBox1"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align:center">
                        <asp:Button runat="server" ID="btnSave" OnClick="btnOK_Click" Text="OK" CssClass="btn"/>
                        <asp:Button runat="server" ID="btnClose" OnClick="btnClose_Click" Text="Close" CssClass="btn"/>
                    </td>
                </tr>
            </table>

        </div>--%>
    </form>
</body>
</html>
