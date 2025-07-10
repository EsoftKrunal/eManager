<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="COC.aspx.cs" Inherits="FormReporting_COC" Title="EMANAGER" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
<script language="javascript" type="text/javascript">
    function CheckAll(self)
    {
        for(i=0;i<=document.getElementById("ctl00_ContentMainMaster_chklst_Vsls").cells.length-1;i++)  
        {
            if(document.getElementById("ctl00_ContentMainMaster_f").checked==true)
            {
                document.getElementById("ctl00_ContentMainMaster_chklst_Vsls_" + i).checked=true;
            } 
            else
            {
                document.getElementById("ctl00_ContentMainMaster_chklst_Vsls_" + i).checked=false;
            }
        }
    }
    function UnCheckAll(selfid) //if any internal checkbox is unchecked then select all will also become unchecked
    {
        for(i=0;i<=document.getElementById("ctl00_ContentMainMaster_chklst_Vsls").cells.length-1;i++)
        {
            if(document.getElementById("ctl00_ContentMainMaster_chklst_Vsls_" + i).checked==false)
            {
                 document.getElementById("ctl00_ContentMainMaster_chklst_AllVsl").checked=false;
            }
        }        
    }
    function OpenCOCDetailWindow(vslid)
    {
        var VesselId=vslid;
        var FromDate=document.getElementById("ctl00_ContentMainMaster_HiddenField_FromDate").value;
        var ToDate=document.getElementById("ctl00_ContentMainMaster_HiddenField_ToDate").value;
        window.open('..\\COC\\VesselWiseCOCList.aspx?COCVesselId='+VesselId+'&COCFrmDate='+FromDate+'&COCToDate='+ToDate,'COCDetail','title=no,toolbars=no,scrollbars=yes,width=1000,height=500,left=150,top=100,addressbar=no');        
    }
    function OpenAddNewWindow()
    {
        window.open('..\\COC\\AddNewCOC.aspx','AddCOC','title=no,toolbars=no,scrollbars=yes,width=800,height=450,left=200,top=100,addressbar=no');        
    }
</script>
<script language="javascript" type="text/javascript">
    function CheckOnOff(rdoId, gridName) {
        var rdo = document.getElementById(rdoId);
        var str1 = rdo.id;
        var str = str1.replace(/rd_select/, "lblid");
        document.getElementById("ctl00_ContentMainMaster_hid").value = document.getElementById(str).innerHTML;
        /* Getting an array of all the "INPUT" controls on the form.*/
        var all = document.getElementsByTagName("input");
        for (i = 0; i < all.length; i++) {
            /*Checking if it is a radio button, and also checking if the
            id of that radio button is different than "rdoId" */
            if (all[i].type == "radio" && all[i].id != rdo.id) {
                var count = all[i].id.indexOf(gridName);
                if (count != -1) {
                    all[i].checked = false;
                }
            }
        }
        rdo.checked = true; /* Finally making the clicked radio button CHECKED */
    }
    function OpenModifyCOCWindow(COCid) {
        var COCId = COCid;
        if (!(parseInt(COCId) == 0 || COCId == "")) {
            window.open('..\\COC\\ModifyCOCPopUp.aspx?COC_Id=' + COCId, 'MCOC', 'title=no,toolbars=no,scrollbars=yes,width=840,height=500,left=240,top=100,addressbar=no');
        }
        else {
            alert("Please select a COC.");
        }
    }
    function OpenCOCClosureWindow(COCid) {
        var COCId = COCid;
        if (!(parseInt(COCId) == 0 || COCId == "")) {
            window.open('..\\COC\\COCClosurePopUp.aspx?COC_Id=' + COCId, 'CCOC', 'title=no,toolbars=no,scrollbars=yes,width=840,height=320,left=240,top=100,addressbar=no');
        }
        else {
            alert("Please select a COC.");
        }
    }
    function OpenCOCTrackerReport(vesselid) {
        var VesselId = vesselid;
        var NFromDate = document.getElementById("ctl00_ContentMainMaster_HiddenField_FrmDate").value;
        var NToDate = document.getElementById("ctl00_ContentMainMaster_HiddenField_ToDate").value;
        var NDueInDays = document.getElementById("ctl00_ContentMainMaster_HiddenField_DueInDays").value;
        var NStatus = document.getElementById("ctl00_ContentMainMaster_HiddenField_Status").value;
        var COCitical = document.getElementById("ctl00_ContentMainMaster_HiddenField_Critical").value;
        var NResponsibility = document.getElementById("ctl00_ContentMainMaster_HiddenField_Responsibility").value;
        var NOverDue = document.getElementById("ctl00_ContentMainMaster_HiddenField_OverDue").value;
         alert("Pending");
    }
</script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
   <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div class="text headerband">
        COC
    </div>
    <br />
<table cellspacing="0" cellpadding="0" width="100%" align="center" border="0" style="font-family:Arial;font-size:12px;">
    <tr>
        <td valign="top" align="center">
        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 0px solid; border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table style="width: 100%" cellspacing="0" cellpadding="0">
                        <tr>
                            <td >
                               <table width="100%" border="0" cellpadding="3" cellspacing="0" style="background-color:#E9F8FC; border:solid 1px #00ABE1; margin-bottom:3px;">
                                <tr>
                                    <td style="text-align:right;width:70px;" >Fleet : </td>
                                    <td style="text-align:left;width:100px;" ><asp:DropDownList runat="server" ID="ddlFleet" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlFleet_OnSelectedIndexChanged"></asp:DropDownList>  </td>
                                    <td style="text-align:right;width:70px;" >Vessel : </td>
                                    <td style="text-align:left;width:210px;" ><asp:DropDownList runat="server" ID="ddlVessel" Width="200px"></asp:DropDownList>  </td>
                                    <td style="text-align:right;width:100px;" >Period : </td>
                                    <td style="text-align:left; width:75px;" ><asp:TextBox runat="server" ID="txtFromDt" Width="75px"  MaxLength="15" CssClass="cal"></asp:TextBox></td>
                                    <td style="width:10px">-</td>
                                    <td style="text-align:left; width:75px;" class="rb"><asp:TextBox runat="server" ID="txtToDt" Width="75px"  MaxLength="15"  CssClass="cal"></asp:TextBox></td>
                                    <td style="text-align: right;width:70px;">
                                        Status:</td>
                                    <td style="text-align: left;width:75px;">
                                        <asp:DropDownList ID="ddl_Status" runat="server" Width="74px">
                                            <asp:ListItem Value="A">All</asp:ListItem>
                                            <asp:ListItem Value="1">Closed</asp:ListItem>
                                            <asp:ListItem Value="0" Selected="True">Open</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox Style="display: none" ID="hid" runat="server" />
                                        <asp:TextBox ID="txt_VesselId" runat="server" style="display: none"></asp:TextBox>
                                    </td>
                                    <td style="text-align:right">
                                        <asp:Button runat="server" ID="btnSearch" Text="Search" onclick="btnSearch_Click" style="  border:solid 1px grey;" CssClass="btn" />
                                      <asp:Button runat="server" ID="btnClear" Text="Clear" onclick="btnClear_Click" style="  border:solid 1px grey;" CssClass="btn" />
                                      <asp:Button id="btn_AddNew" runat="server" Text="Add COC" style=" border:solid 1px grey;" OnClientClick="return OpenAddNewWindow();" CssClass="btn"></asp:Button>
                                    </td>
                                    
                                </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                             <td style="padding: 5px; text-align: left">
                                &nbsp;&nbsp;<asp:TextBox ID="TextBox1" runat="server" BackColor="#FFCCCC" CssClass="input_box"
                                    Enabled="False" ReadOnly="True" Width="14px"></asp:TextBox>
                                <em>
                                OverDue Items</em>
                             </td>
                        </tr>
                        <tr>
                            <td style="padding-right: 5px; padding-left: 10px; text-align: left" valign="top" >                              
                                 <div style="height :25px; overflow-x:hidden;overflow-y:scroll;width:100%;">
                                    <table cellspacing="0" rules="all" border="1" style="width:100%;height :25px;border-collapse:collapse;border:solid 1px #4371a5;">
                                        <colgroup>
                                            <col width="50px" />
                                            <col width="40px" />
                                            <col />
                                            <col width="170px" />
                                            <col width="120px" />
                                            <col width="120px" />
                                            <col width="120px" />
                                            <col width="25px" />
                                            <col width="20px" />
                                            </colgroup>
                                            <tr ID="tr1" style="font-size:9px;" class= "headerstylegrid">
                                                <th style="text-align:center; ">
                                                    &nbsp;Select</th>
                                                <th style="text-align:left; ">
                                                    &nbsp;View</th>
                                                <th style="text-align:left; ">
                                                    &nbsp;Vessel Name</th>
                                                <th style="text-align:left; ">
                                                    Issued From</th>
                                                <th style="text-align:left; ">
                                                    Ref #</th>
                                                <th style="text-align:center; ">
                                                    Due Date</th>
                                                <th style="text-align:left; ">
                                                    Completion Date</th>
                                                <th style="text-align:center; ">
                                                    </th>
                                                <th>
                                                </th>
                                            </tr>
                                        
                                </table>
                                </div>
                                 <div style="height :325px; overflow-x:hidden;overflow-y:scroll;width:100%;">
                                    <table cellspacing="0" cellpadding="1" rules="all" border="1" style="width:100%;border-collapse:collapse;border:solid 1px Gray;"> 
                                        <colgroup>
                                            <col width="50px" />
                                            <col width="40px" />
                                            <col />
                                            <col width="170px" />
                                            <col width="120px" />
                                            <col width="120px" />
                                            <col width="120px" />
                                            <col width="25px" />
                                            <col width="20px" />
                                        </colgroup>                                 
                                    <asp:Repeater ID="rptCOC" runat="server">
                                        <ItemTemplate>
                                            <tr onmouseout="this.style.backgroundColor=this.style.historycolor;" onmouseover="this.style.historycolor=this.style.backgroundColor;this.style.backgroundColor='#c2c2c2';" style="font-size:9px">
                                                <td style="background-color:<%#(DateTime.Parse(Eval("TargetCloseDate").ToString()) < DateTime.Parse(DateTime.Today.Date.ToString()) && (Eval("Closed").ToString() == "") ? "#FFCCCC" : "") %>">
                                                    <asp:RadioButton ID="rd_select" OnClick="javascript:CheckOnOff(this.id,'rptCOC');" GroupName="asdf" runat="server" Enabled='<%#Eval("Closed").ToString().Trim()=="" %>' />
                                                    <asp:Label ID="lblid" runat="server" Style="display: none" Text='<%# Eval("COCID") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <a target="_blank" href='<%# GetPath(Eval("UPFILENAME").ToString()).ToString() %>' style='display: <%# (Eval("UPFILENAME").ToString().Trim()=="")?"None":"Block" %>; cursor: hand'><img src="../../HRD/Images/paperclipx12.png" border="0" /></a>
                                                </td>
                                                <td style="text-align:left;"><%#Eval("VESSELNAME")%></td>
                                                <td style="text-align:left; cursor:pointer;"><%#Eval("IssuedFrom")%></td>
                                                <td style="text-align:left; cursor:pointer;"><%#Eval("COCNO")%></td>
                                                <td style="text-align:center; cursor:pointer;"><%#Common.ToDateString(Eval("TargetCloseDate"))%></td>
                                                <td style="text-align:center; cursor:pointer;"><%#Common.ToDateString(Eval("CompletionDate"))%></td>
                                                <td style="text-align:center; cursor:pointer;"><asp:ImageButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"  CausesValidation="False" CommandArgument='<%#Eval("COCID")%>' Visible='<%#Eval("Closed").ToString() == ""%>'  ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" ToolTip="Delete" /></td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr style="padding:10px;">
                            <td style="padding-right: 10px; text-align: right;">
                                <asp:Label ID="lblMsg" ForeColor="Red" style="float:left;"  runat="server"></asp:Label>
                                <asp:Button ID="btn_View" runat="server"  Text="Open/Modify"  OnClientClick='return OpenModifyCOCWindow(document.getElementById("ctl00_ContentMainMaster_hid").value);' style="  border:solid 1px grey;" CssClass="btn" />
                                <asp:Button ID="btn_Closure" runat="server"  Text="Closure"  OnClientClick='return OpenCOCClosureWindow(document.getElementById("ctl00_ContentMainMaster_hid").value);' style="  border:solid 1px grey;" CssClass="btn" />
                                <asp:Button ID="btn_Print" runat="server" Text="Print"       OnClientClick='return OpenCOCTrackerReport(document.getElementById("ctl00_ContentMainMaster_txt_VesselId").value);' style="  border:solid 1px grey;" CssClass="btn" />
                            </td>
                        </tr>
                        </table>
                        <ajaxToolkit:CalendarExtender runat="server" ID="cal1" Format="dd-MMM-yyyy" TargetControlID="txtFromDt"></ajaxToolkit:CalendarExtender>
                       <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd-MMM-yyyy" TargetControlID="txtToDt"></ajaxToolkit:CalendarExtender>
                    </td>
               </tr>
           </table>
        </fieldset>
        </td>
     </tr>
</table>
</asp:Content>

