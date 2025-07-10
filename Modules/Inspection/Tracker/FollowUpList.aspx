<%@ Page Language="C#" MasterPageFile="~/Modules/Inspection/Tracker/FormReportingMaster.master" AutoEventWireup="true" CodeFile="FollowUpList.aspx.cs" Inherits="FormReporting_FollowUpList" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript">
    function CheckAll(self)
    {
        for (i = 0; i <= document.getElementById("ctl00_ctl00_ContentMainMaster_ContentPlaceHolder1_chklst_Vsls").cells.length-1;i++)  
        {
            if (document.getElementById("ctl00_ctl00_ContentMainMaster_ContentPlaceHolder1_chklst_AllVsl").checked==true)
            {
                document.getElementById("ctl00_ctl00_ContentMainMaster_ContentPlaceHolder1_chklst_Vsls_" + i).checked=true;
            } 
            else
            {
                document.getElementById("ctl00_ctl00_ContentMainMaster_ContentPlaceHolder1_chklst_Vsls_" + i).checked=false;
            }
        }
    }
    function UnCheckAll(selfid) //if any internal checkbox is unchecked then select all will also become unchecked
    {
        for (i = 0; i <= document.getElementById("ctl00_ctl00_ContentMainMaster_ContentPlaceHolder1_chklst_Vsls").cells.length-1;i++)
        {
            if (document.getElementById("ctl00_ctl00_ContentMainMaster_ContentPlaceHolder1_chklst_Vsls_" + i).checked==false)
            {
                document.getElementById("ctl00_ctl00_ContentMainMaster_ContentPlaceHolder1_chklst_AllVsl").checked=false;
            }
        }        
    }
    function OpenVesselWiseDetailWindow(vslid)
    {
        var VesselId = vslid;
        
        var FollowUpCategories = document.getElementById("ctl00_ctl00_ContentMainMaster_ContentPlaceHolder1_HiddenField_FollowUpCat").value;
        var FromDate = document.getElementById("ctl00_ctl00_ContentMainMaster_ContentPlaceHolder1_HiddenField_FromDate").value;
        var ToDate = document.getElementById("ctl00_ctl00_ContentMainMaster_ContentPlaceHolder1_HiddenField_ToDate").value;
        window.open('..\\Tracker\\VesselWiseFollowUpList.aspx?FPVesselId='+VesselId+'&FPCatID='+FollowUpCategories+'&FPFrmDate='+FromDate+'&FPToDate='+ToDate,'tyu','title=no,toolbars=no,scrollbars=yes,width=1000,height=560,left=150,top=100,addressbar=no');        
    }
    function OpenAddNewWindow()
    {
        window.open('..\\Tracker\\AddFollowUpList.aspx','','title=no,toolbars=no,scrollbars=yes,width=1000,height=440,left=150,top=100,addressbar=no');        
    }
    </script>
<div style="font-family:Arial;font-size:12px;">
    <table id="filter" width="100%" style ="text-align :center" cellpadding="5" cellspacing="3" style="border:solid 1px #4371a5;" >
    <tr style="font-weight:bold;padding:3px;">
        <td style="width:100px; text-align:center">Fleet</td>
        <td style="width:200px; text-align:center">Vessel</td>
        <td style="width:200px; text-align:center">Inactive Vessels</td>
        <td style=" text-align:left;padding-left:10px;">View Report for</td>
        <td style="width:125px; text-align:center">
      <%--  <asp:LinkButton runat="server" CssClass="input_box"  ID="bgnGoback" Text="< Go Back" PostBackUrl="~/Home.aspx"/> --%>
        </td>
    </tr>
    <tr style="padding:3px;">
        <td>
            <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFleet_OnSelectedIndexChanged"  ID="ddlFleet" CssClass="input_box" Width="100%" ></asp:DropDownList>
        </td>
        <td>
            <asp:DropDownList runat="server"  ID="ddlVessel" CssClass="input_box" Width="100%"></asp:DropDownList> 
        </td>
        <td style="text-align:left"><asp:CheckBox runat="server" ID="chk_Inactive" OnCheckedChanged="chk_Inactive_OnCheckedChanged" Text="Include Inactive Vessels" AutoPostBack="true"  />  </td>
        <td style="text-align:center;padding-left:10px;">
            <table>
                <tr>
                    <td style="padding-right:5px;width:60px;text-align:right;">
                         From : 
                    </td>
                    <td style="padding-left:5px;width:120px;text-align:left;">
                         <asp:TextBox runat="server" id="txtFromDate"  MaxLength="15" CssClass="input_box" Width="80px" ></asp:TextBox>&nbsp;&nbsp;
                         <asp:ImageButton id="ImageButton3" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif"></asp:ImageButton> 
            &nbsp;&nbsp;    <ajaxToolkit:CalendarExtender id="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton3" PopupPosition="TopRight" TargetControlID="txtFromDate">
    </ajaxToolkit:CalendarExtender>
                    </td>
                    <td style="padding-right:5px;width:60px;text-align:right;">
                        TO : 
                    </td>
                    <td style="padding-left:5px;width:120px;text-align:left;">
                        <asp:TextBox runat="server" id="txtToDate"  MaxLength="15" CssClass="input_box" Width="80px"  ></asp:TextBox> &nbsp;&nbsp;
        <asp:ImageButton id="ImageButton4" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif"></asp:ImageButton> &nbsp;&nbsp;
                        <ajaxToolkit:CalendarExtender id="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton4" PopupPosition="TopRight" TargetControlID="txtToDate">
    </ajaxToolkit:CalendarExtender> 
                    </td>
                </tr>
            </table>
        </td>
        <td >
        <asp:Button runat="server" CssClass="btn" ID="Button1" Text="Show" onclick="btn_Show_Click" /> 
        <asp:Button runat="server" CssClass="btn" ID="btnClear" Text="Clear"  OnClick="btnClear_OnClick" /> 
        </td>
    </tr>
</table>
    <table style="WIDTH: 100%" cellSpacing="0" cellPadding="0">      
    <tr><td style="HEIGHT: 3px" colSpan=7>
        <table style="WIDTH: 100%;padding:5px;" cellSpacing="0" cellPadding="0">
    <tbody><tr><TD style="PADDING-LEFT: 10px; TEXT-ALIGN: left;"><asp:CheckBoxList id="chklst_FollowUpCat" runat="server" Width="451px" RepeatDirection="Horizontal">
<asp:ListItem Value="1" Selected="True">LPSQE</asp:ListItem>
<asp:ListItem Value="3" Selected="True">Technical</asp:ListItem>
<asp:ListItem Value="4" Selected="True">Vetting</asp:ListItem>
<asp:ListItem Value="5" Selected="True">Crewing</asp:ListItem>
</asp:CheckBoxList></td><td style="PADDING-RIGHT: 15px; TEXT-ALIGN: right"><asp:Button id="btn_AddNew" runat="server" Width="83px" CssClass="btn" Text="Add New" OnClientClick="return OpenAddNewWindow();" ></asp:Button></td></tr></tbody></table></td></tr>
            <tr><td colSpan=7></td></tr>
            <tr> 
    <td style="WIDTH: 100%; TEXT-ALIGN: left" vAlign=top colspan="2">
    <table style="WIDTH: 100%" cellSpacing="0" cellPadding="0">
    
    <tr><td style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; TEXT-ALIGN: left" colSpan=3>

            <div style="height :47px; overflow-x:hidden;overflow-y:scroll;width:100%;">                                                       
            <table cellspacing="0" rules="all" border="1" id="ctl00_ContentPlaceHolder1_grd_Data" style="width:100%;border-collapse:collapse;border:solid 1px #4371a5;">
                <colgroup>
                <%--<col width="50px" />--%>
                <col />
                <col width="120px" />
                <col width="120px" />
                <col width="120px" />
                <col width="17px" />
                     </colgroup>
                <tr class= "headerstylegrid" id="trNew" >
                    <%--<th scope="col">View</th>--%>
                    <th scope="col" style="text-align:left">Vessel Name</th>
                    <th scope="col" style="text-align:center">FollowUp Items</th>
                    <th scope="col" style="text-align:center">Open Items</th>
                    <th scope="col" style="text-align:center">Over Due Items</th>
                    <th></th>
                </tr>
                <tr>
                    <th colspan="2">
                        <asp:Label ID="lblTotRec" runat="server"></asp:Label>
                    </th>
                    <th></th>
                    <th></th>
                    <th></th>
                </tr>
                   
            </table>
            </div>
            <div style="height :280px; overflow-x:hidden;overflow-y:scroll;width:100%;">
                <table cellspacing="0" rules="all" border="1" id="Table1" style="width:100%;border-collapse:collapse;border:solid 1px #4371a5;">
                 <colgroup>
                <%--<col width="50px" />--%>
                <col />
                <col width="120px" />
                <col width="120px" />
                <col width="120px" />
                 <col width="17px" />
                   </colgroup>  
                <asp:Repeater ID="Grd_FollowUpList" runat="server" >
                <ItemTemplate>
                    <tr onmouseover="this.style.historycolor=this.style.backgroundColor;this.style.backgroundColor='#c2c2c2';" onmouseout="this.style.backgroundColor=this.style.historycolor;">
                        <td>
                            <a href="#" onclick='return OpenVesselWiseDetailWindow("<%# Eval("VesselId") %>");' style="cursor:pointer;" > <asp:Label ID="lblVesselName" runat="server" Text='<%# Eval("VesselName") %>'></asp:Label></a>
                            <asp:HiddenField ID="hfd_VesselId" runat="server" Value='<%# Eval("VesselId") %>' />            
                        </td>
                        <td style="text-align:center"> <%#Eval("FollowUpItems")%></td>
                        <td style="text-align:center"> <%#Eval("OpenItems")%> </td>
                        <td style="text-align:center"> <%#Eval("OverDueItems")%> </td>
                        <td>&nbsp;</td>
                    </tr>
                 </ItemTemplate>
                </asp:Repeater>
                    
                </table>
            </div>
            
                                                        
    </td></tr>
    </table>
        </td>
                </tr>
        </table>
    
    <asp:HiddenField id="HiddenField_LoginId" runat="server"></asp:HiddenField> 
    <asp:HiddenField ID="HiddenField_FollowUpCat" runat="server" />
    <asp:HiddenField ID="HiddenField_FromDate" runat="server" />
    <asp:HiddenField ID="HiddenField_ToDate" runat="server" />
</div>

   
    
    

</asp:Content>

