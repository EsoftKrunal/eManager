<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VesselassignmentHistory.aspx.cs" Inherits="Emtm_Vesselassignment" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../JS/jquery.min.js" type="text/javascript"></script>
    <script src="../JS/KPIScript.js" type="text/javascript"></script>
      <link rel="Stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />
  
    <script type="text/javascript">
        $(window).load(function () {
            var radio = $("input[Type=radio]");
            if (radio.length > 0) {                
                $(radio[0]).click();
            }
        })

        $(document).ready(function () {

            $(".assignmentDate").click(function () {
                $("#hfdVesselAssignmentID").val($(this).attr("tableid"));
                $("#hfdAssignmentDate").val($(this).attr("adate"));

                $("#btnTempPost").click();
            });

            $("body").on("click", "#iClosePopup", function () {
                $("#btnClosePopup").click();
            });
        })
    </script>
    <style type="text/css">
        .desg
        {
            font-size:15px;
            color:#808080;
            font-style:italic;
        }
        .desguser
        {
            font-size:17px;
        }
	.box
	{	
	 width:30px;height:30px;background-color:#279c87;margin:0 auto;position:relative;
        }
        .box:hover
	{	
	  background-color:#f8852e;
        }
    </style>

</head>
<body style="font-family:Calibri; font-size:12px; margin:0px;">
    <form id="form1" runat="server">
   
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
       <div style="background-color:#1b70b0;color:white;text-align:center; height:40px;line-height:40px;font-size:18px;position:fixed;width:100%;z-index:1;">
           Vessel Assignment History
		<asp:Label  runat="server" id="lblvesselname"> </asp:Label>
           <asp:DropDownList ID="ddlTemp" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btnTempPost_OnClick" style='display:none;'>
               <asp:ListItem Value="asdf">sadfsd</asp:ListItem>
               <asp:ListItem Value="aaa">aaa</asp:ListItem>
           </asp:DropDownList>
       </div>
        <div style="position:absolute;top:40px;left:0px;width:100px;margin-bottom:140px;">
            <asp:Repeater runat="server" ID="rptDays">
                <ItemTemplate>
                <div style="width:5px;height:90px;background-color:#d0d0d0;margin:0 auto;">
                    
                </div>
                <div class="box">
                    <input type="radio" name="aDate" style="margin:9px 0px 0px 9px;" class="assignmentDate" adate= <%#Common.ToDateString(Eval("EffDate"))%> />                    
                    <span style="position: absolute; left: 35px; top: 8px; width: 400px; font-size: 14px; color: #ff6a00;">
                        <%#Common.ToDateString(Eval("EffDate"))%>
			<div style="color:#1b70b0">
    			        <asp:Repeater runat="server" ID="rptChanges" DataSource='<%#getChanges(Eval("EffDate"))%>'>
		                <ItemTemplate>
 				<div>	(<b> <%#Eval("UserName") %> </b> assigned as <b style="color:#279c87"> <%#Eval("PositionName") %>  </b>)</div>
			        </ItemTemplate>
		                </asp:Repeater>
			</div> 
                    </span>
                </div>
                </ItemTemplate>
            </asp:Repeater>
        </div> 
       
        <asp:UpdatePanel ID="UP1" runat="server">
            <ContentTemplate>         
                <%--<input type="button" style="background-color:#f8852e;color:white;padding:8px; width:150px; position:fixed;top:55px; right:20px;border:none;" value="Modify Assignement" />--%>
                <asp:Button ID="btnModifyAssignement" runat="server" Text="Modify Assignement" OnClick="btnModifyAssignement_OnClick" style="background-color:#f8852e;color:white;padding:8px; width:150px; position:fixed;top:55px; right:20px;border:none;" />
                <asp:HiddenField ID="hfdVesselAssignmentID" runat="server" />
                <asp:HiddenField ID="hfdAssignmentDate" runat="server" />
                <asp:Button ID="btnTempPost" runat="server" Text="Click" OnClick="btnTempPost_OnClick" style="display:block;" />   
                <div style="position:fixed;top:100px; right:20px;  left:500px;min-height:400px; background-color:#f2f2f2;padding:0px; border:solid 1px #bed9ed" id="divAvailableUser" runat="server" visible="false">
            <div style="color:#1b70b0;font-size:15px; background-color:#bed9ed;padding:10px;">
           <b>
               
               <asp:Label ID="lblVesselAssignmentListHeading" runat="server"></asp:Label>
           </b>
               
           </div><br/><br/>
            <asp:Repeater ID="rptAvailableUsers" runat="server">
                <ItemTemplate>
                    <div style="width:45%;display:inline-block;margin-left:25px;">
                        <div class="desg"><%#Eval("PositionName") %> </div>
                        <div class="desguser"><%#Eval("UserName") %> </div>
                        <div style="border-top:solid 1px #cecdcd; margin-bottom:10px;margin-top:5px;"></div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
           
       </div>
            

       <div style="position:fixed;top:0px; left:0px;height:100%; width:100%;background-color:rgba(0,0,0,0.5); height:100%;" id="divModifyAssignment" runat="server" visible="false">
           <div style="width:600px; margin:0 auto;margin-top:50px; height:400px; background-color:white;">
           <div style="background-color:white;">
               <div style="color:#1b70b0;font-size:15px; background-color:#bed9ed;padding:10px;">
                <b>Modify Vessel Assignments as on <%=DateTime.Today.ToString("dd-MMM-yyyy") %></b>
                   
                   <i class="fa fa-close" id="iClosePopup" style="color:#ff0000;float:right; font-size:18px;cursor:pointer;"></i>
                   <asp:Button ID="btnClosePopup" runat="server" style="display:none;" OnClick="btnClosePopup_OnClick" />
               </div>
               <div style="padding:5px; background-color:#f1f1f1">
                   <%--<div style="height:365px;overflow-y:scroll;overflow-x:hidden;" class="ScrollAutoReset">--%>
                   <div style="height:365px;overflow-y:scroll;overflow-x:hidden;" id="divUsersforPosition" class="ScrollAutoReset">
                       <table>
                           <asp:Repeater runat="server" ID="rptusers">
                               <ItemTemplate>
                                   <tr>
                                       <td>
                                           <asp:CheckBox ID="chkSelectForUser" runat="server" OnCheckedChanged="OnCheckedChanged_chkSelectForUser" AutoPostBack="true" />
                                           <asp:HiddenField ID="hfdTableID" runat="server" Value=<%#Eval("TableID") %> />
                                           <asp:HiddenField ID="hfdCurrUserID" runat="server" Value=<%#Eval("UserID") %> />
                                       </td>
                                       <td>
                                           <asp:Label ID="lblPositionName"  runat="server" Text='<%#Eval("PositionName") %> '></asp:Label>
                                           <asp:HiddenField ID="hfdVesselPositionID" runat="server" Value=<%#Eval("VPID") %> />
                                           
                                            <i style="font-weight:bold; color:#1b70b0">( <%#Eval("UserName") %> )</i></td>
                                       <td> 
                                           <asp:DropDownList ID="ddlUsers" runat="server" DataSource=<%#ddlUser(Eval("VPID"))%> DataTextField="UserName" DataValueField="UserID" Visible="false" ></asp:DropDownList>
                                           <%--<i class="fa fa-pencil" style="color:#d33c3c;cursor:pointer;" ></i> --%>
                                       </td>
                                   </tr>
                                   </ItemTemplate>
                               </asp:Repeater>
                       </table>
                   </div>
                   <div style="margin:10px;text-align:center;color:#333;">
                       Effective From : <asp:TextBox ID="txtEffectiveDate" runat="server" Width="100px"></asp:TextBox>
                       <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtEffectiveDate" PopupPosition="BottomRight"></ajaxToolkit:CalendarExtender>
                       </div>
                   <div style="margin:0px;text-align:center;color:#333;">
                        <%--<input type="button" style="background-color:#f8852e;color:white;padding:8px; width:150px;border:none;" value="Save Assignement" />--%>
                       <asp:Button ID="btnSave" runat="server" OnClick="btnSave_OnClick" Text="Save Assignement" style="background-color:#f8852e;color:white;padding:8px; width:150px;border:none;" />
                       <div style="padding:5px;color:red;">
                           &nbsp; <asp:Label ID="lblMsg" runat="server"></asp:Label>
                       </div>
                   </div>
               </div>
           </div>
           </div>
       </div>

                <%--------------------------------------------------------------------------%>
                 
                <%--------------------------------------------------------------------------%>
                </ContentTemplate>
        </asp:UpdatePanel>
    
    </form>
</body>
</html>
