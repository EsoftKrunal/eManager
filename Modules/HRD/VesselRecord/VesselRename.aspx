<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VesselRename.aspx.cs" Inherits="VesselRecord_VesselRename" %>
<%@ Register TagName="menu" Src="~/Modules/HRD/UserControls/VesselMenu.ascx" TagPrefix="mtm"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
     <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <style type="text/css">
        .style1
        {
            width: 208px;
        }
        .style2
        {
            width: 222px;
        }
        .style3
        {
            width: 90px;
        }
    </style>
</head>
<body style=" margin: 0 0 0 0;" >
    <form id="form1" runat="server">
    <div style="text-align: center">
       <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style="width:150px; text-align :left; vertical-align : top;">
            <mtm:menu runat="server" ID="menu2" />  
        </td>
        <td style=" text-align :right; vertical-align : top;" >
            <table border="0" cellpadding="0" cellspacing="0" style="background-color:#f9f9f9; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center;width:100%;"   >
            <tr>
                <td style="background-color:#4371a5; height: 23px; text-align:center; width: 100%;" class="text" >
                <img runat="server" id="imgHelp" moduleid="4" style ="cursor:pointer;float :right; padding-right :5px;" src="../images/help.png" alt="Help ?"/> 
                Vessel Rename</td>
            </tr>
             <tr >
                <td colspan="2" style="padding :10px; padding-top :0px;">
                 <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px">
                  <legend><strong>Select Vessel to Rename </strong></legend>
                   <table cellpadding="2" cellspacing="0" width="100%">
                  <tr>
                     <td style=" color :Red " >
                     <asp:Label runat="server" ID="lblMsg" Text="In Order to rename a exisitng vessel first create a new vessel then rename the selected vessel to new vessel. System will sign off all crew members from running vessel & move them in the pending contract list for new vessel."></asp:Label>
                     </td>
                     </tr>
                     <tr><td>
                    <table cellpadding="2" cellspacing="0" width="100%">
                    <tr>
                     <td style=" text-align :right" >
                         From&nbsp; Vessel :  
                     </td> 
                     <td style=" text-align :left" class="style2" >
                     <asp:DropDownList CssClass="input_box"  runat="server" ID="ddl_From" Width="200px"></asp:DropDownList>  
                     </td> 
                     <td class="style3" style="text-align: right">
                         Renamed
                     To 
                     </td>
                     <td style=" text-align :left" class="style1">
                     <asp:DropDownList CssClass="input_box"  runat="server" ID="ddl_To" Width="200px" ></asp:DropDownList> 
                     </td>
                     <td style=" text-align :right">
                         &nbsp;Date :</td>
                     <td style=" text-align :left">
                         <asp:TextBox ID="txttodate" runat="server" CssClass="input_box" MaxLength="15" Width="80px"></asp:TextBox>
                         <asp:ImageButton ID="imgtodate" runat="server" 
                             ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                     </td>
                     <td style=" text-align :left">
                         &nbsp;</td>
                    </tr>
                    <tr>
                     <td style=" text-align :right" >
                         &nbsp;</td> 
                     <td style=" text-align :left" class="style2" >
                         <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddl_From" ErrorMessage="Required." Operator="NotEqual" ValueToCompare="0" ></asp:CompareValidator>
                     </td> 
                     <td class="style3">
                         &nbsp;</td>
                     <td style=" text-align :left" class="style1">
                         <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddl_To" ErrorMessage="Required." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
                     </td>
                     <td style=" text-align :left">
                         &nbsp;</td>
                     <td style=" text-align :left">
                         <asp:RegularExpressionValidator ID="CompareValidator12" Display="Dynamic" runat="server" 
                             ControlToValidate="txttodate" ErrorMessage="Invalid Date." 
                             ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$"></asp:RegularExpressionValidator>
                         <asp:RequiredFieldValidator ID="CompareValidator13" runat="server" 
                             ControlToValidate="txttodate" ErrorMessage="Required."> </asp:RequiredFieldValidator>
                     </td>
                     <td style=" text-align :left">
                         &nbsp;</td>
                 </tr>
                    </table> 
                    </td></tr>
                    <tr>
                     <td >
                     <asp:Button runat ="server" ID="btn_Rename" CssClass="btn"  Text="Rename Vessel" Width="150px" onclick="btn_Rename_Click" />&nbsp;
                     <asp:Button runat ="server" ID="btn_ShowMembers" CausesValidation="false" CssClass="btn" Text="Show Crew Members" onclick="btn_ShowMembers_Click" Width="150px"/>
                     <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgtodate" PopupPosition="TopLeft" TargetControlID="txttodate"></ajaxToolkit:CalendarExtender>
                     <asp:Label runat="server"  Font-Bold ="true" ID="lblCnt"></asp:Label>  
                     </td> 
                 </tr>
                 <tr>
                     <td >
                         <div style="padding-top:1px;overflow-y:scroll;overflow-x:hidden;height:310px; width:100%; border:solid 1px gray;" >
                                    <asp:GridView ID="gvmatrix" runat="server" AutoGenerateColumns="False" DataKeyNames="CrewId" GridLines="Horizontal" Height="9px" PageSize="3" Style="text-align: center" Width="98%" >
                                                <Columns>
                                                    <asp:BoundField DataField="CrewNumber" HeaderText="Emp#" meta:resourcekey="BoundFieldResource7">
                                                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Name" meta:resourcekey="TemplateFieldResource2">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCompanyName" runat="server" meta:resourcekey="lblCompanyNameResource2" Text='<%# Eval("CrewName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="RankName" HeaderText="Rank" meta:resourcekey="BoundFieldResource7">
                                                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Nationality" HeaderText="Nationality" meta:resourcekey="BoundFieldResource11">
                                                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Sign On Date" meta:resourcekey="BoundFieldResource12" DataField="SignOnDate">
                                                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Sign Off Date" meta:resourcekey="BoundFieldResource12" DataField="SignOffDate">
                                                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                                  <RowStyle CssClass="rowstyle" />
                                                                  
                                            </asp:GridView>
                                                                                      </div>
                     </td> 
                 </tr>
                </table>        
           </fieldset>
          </td>
         </tr>
        </table> 
        </td> 
        </tr> 
        </table> 
      </div>
    </form>
</body>
</html>
