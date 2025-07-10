<%@ Page Language="C#" MasterPageFile="~/Modules/HRD/CrewPlanning.master" AutoEventWireup="true" CodeFile="POList.aspx.cs" Inherits="CrewOperation_POList" Title="Untitled Page" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">

    <script language="javascript">
function Calculate(FT,ST,MT)
{
    var a,b;
    a=parseFloat(document.getElementById(FT).getAttribute("value"));
    b=parseFloat(document.getElementById(MT).getAttribute("value"));
    
    if ((a==null) || isNaN(a))
    {
    a="0";
    }
    if ((b==null) || isNaN(b))
    {
    b="0";
    }

    b=a/b; 
    b=Math.round(b*100)/100
    document.getElementById(ST).setAttribute("value",b); 

}
function openpage()
{
var url = "../Registers/PopUp_Port.aspx"
window.open(url,null,'title=no,toolbars=no,scrollbars=yes,width=400,height=200,left=50,top=50,addressbar=no');
return false;
}
</script>
<asp:Label ID="Label1" runat="server" ForeColor="Red" ></asp:Label>
<asp:Label ID="Label2" runat="server" ForeColor="Red" ></asp:Label>
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtexchangerate" ErrorMessage="Exch rate allow 2 decimal places." ValidationExpression="\b\d{1,13}\.?\d{0,2}"></asp:RegularExpressionValidator>
<table cellspacing="0" width="100%" border="1" >
    <tr><td style=" background-color :#e2e2e2" >
    <table width="100%" cellpadding="0" cellspacing="0">
    <tr>
       <td style="padding-right: 5px; height: 13px; text-align :right ">Vessel:</td>
       <td align="left" style="width: 289px; height: 13px"><asp:DropDownList ID="ddvessel" runat="server" AutoPostBack="True" CssClass="required_box" Width="200px" OnSelectedIndexChanged="ddvessel_SelectedIndexChanged1" TabIndex="1">
                          </asp:DropDownList>
                          <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddvessel"
                              ErrorMessage="Required" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
                      </td>
                      <td style="padding-right: 5px; width: 62px; height: 13px;text-align :right ">
                          Country:</td>
                      <td align="left" style="width: 218px; height: 13px;">
                          <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" CssClass="required_box"
                              OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" Width="150px" 
                              TabIndex="2">
                          </asp:DropDownList>
                          <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="ddlCountry"
                              ErrorMessage="Required" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator></td>
                      <td  style="padding-right: 5px; height: 13px;text-align :right ">
                          Port:</td>
                      <td align="left" style="height: 13px">
                          <asp:DropDownList ID="ddport" runat="server" CssClass="required_box" TabIndex="3"
                            Width="150px" AutoPostBack="True" 
                              OnSelectedIndexChanged="ddport_SelectedIndexChanged">
                          </asp:DropDownList>
                          <asp:ImageButton ID="imgaddport" runat="server" CausesValidation="false" OnClientClick="return openpage();" ImageUrl="~/Modules/HRD/Images/add_16.gif"  />
                          <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="ddport"
                              ErrorMessage="Required" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
                      </td>
                  </tr>
              <tr>
                      <td align="right" style="padding-right: 5px; height: 19px; text-align: right;">
                          Port Call #:</td>
                      <td align="left" style="height: 19px; width: 289px;">
                          <asp:DropDownList ID="ddPortcall" runat="server" CssClass="required_box" TabIndex="4"
                            Width="200px" AutoPostBack="True" 
                              OnSelectedIndexChanged="ddPortcall_SelectedIndexChanged">
                          </asp:DropDownList>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddPortcall"
                              ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                            </td>
                      <td align="right" style="padding-right: 5px; height: 19px; width: 62px; text-align: right;">
                     ETA:</td>
                      <td align="left" style="height: 19px; width: 218px;">
                            <asp:TextBox ID="txteta" runat="server" CssClass="input_box" TabIndex="5" Width="80px"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" CausesValidation="False" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txteta" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txteta" ErrorMessage="Required." Enabled="false" ></asp:RequiredFieldValidator>  
                          </td>
                      <td align="right" style="padding-right: 5px; height: 19px; text-align: right;">
                        ETD:</td>
                      <td align="left" style="height: 19px">
                            <asp:TextBox ID="txtetd" runat="server" CssClass="input_box" TabIndex="6" Width="80px"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" CausesValidation="False" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtetd" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtetd" ErrorMessage="Required." Enabled="false" ></asp:RequiredFieldValidator>                                
                      </td>
                  </tr>
              <tr>
                    <td align="right" style="padding-right: 5px; height: 19px; text-align: right;">Vendor:</td>
                    <td align="left" style="height: 19px; width: 289px;"><asp:DropDownList ID="ddvendor" runat="server" CssClass="input_box" TabIndex="7" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddvendor_SelectedIndexChanged"></asp:DropDownList></td>
                    <td align="right" style="padding-right: 5px; height: 19px; width: 62px; text-align: right">PO Dt:</td>
                    <td align="left" style="height: 19px; width: 218px;">
                        <asp:TextBox ID="txtpodt" runat="server" CssClass="required_box" TabIndex="9" Width="80px" AutoPostBack="True" OnTextChanged="txtpodt_TextChanged"></asp:TextBox><asp:ImageButton ID="imgpodate" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtpodt" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtpodt" ErrorMessage="Required."></asp:RequiredFieldValidator>  
         
                        </td>
                    <td align="right" style="padding-right: 5px; height: 19px; text-align: right;">Curr &amp; Exch. Rate:</td>
                    <td align="left" style="height: 19px"><asp:DropDownList ID="ddlcurrency" 
                            runat="server" CssClass="input_box" TabIndex="10" Width="92px" ></asp:DropDownList>-<asp:TextBox 
                            ID="txtexchangerate" runat="server" CssClass="required_box" TabIndex="8" 
                            Width="49px" style="text-align:right" MaxLength="8" ></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtexchangerate"
                              ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                            </td>
                </tr>
              </table>
    </td></tr>
    <tr>
        <td align="right" style="padding-bottom: 0px;">
            <div style="overflow-y: scroll; overflow-x:hidden ; width:100%; height: 150px">
                <asp:GridView ID="GvPO" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" Style="text-align: center" Width="98%" OnRowDataBound="GvPO_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr. #">
                        <ItemStyle HorizontalAlign="Left" Width="40px" />
                        <ItemTemplate>
                        <asp:Label ID="lblsrno" runat="server"></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="A/C Code">
                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                        <ItemTemplate>
                        <asp:DropDownList ID="ddAC" CssClass="required_box" runat="server" Width="170px" DataTextField="CodeName" DataValueField="AccountHeadId" DataSource='<%# accountcode() %>'></asp:DropDownList>
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description Of Services">
                        <ItemStyle HorizontalAlign="Left" Width="200px"/>
                        <ItemTemplate>
                        <asp:TextBox ID="txtdesofservices" CssClass="input_box" runat="server" Width="200px" MaxLength="245"></asp:TextBox>
                        </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="Service" HeaderText="Description Of Services">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>--%>
                        <asp:TemplateField HeaderText="RFQ#">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                        <ItemTemplate>
                        <asp:DropDownList ID="ddrfq" CssClass="required_box" runat="server" Width="140px" DataTextField="RFQNo" DataValueField="agentid" DataSource='<%#rfqnumber() %>'></asp:DropDownList>
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amt. Local Curr">
                        <ItemStyle HorizontalAlign="Left" Width="90px" />
                        <ItemTemplate>
                        <asp:TextBox ID="txtamtlocalcurrency" CssClass="input_box" runat="server" Width="90px" Text="0.0" MaxLength="10" style="text-align:right;"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender id="FilteredTextBoxExtender2" runat="server" ValidChars="." TargetControlID="txtamtlocalcurrency" FilterType="Numbers,Custom">
                    </ajaxToolkit:FilteredTextBoxExtender>
                        </ItemTemplate>
                        
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="currency" HeaderText="Amt. Local Currency">
                            <ItemStyle HorizontalAlign="Left" Width="130px" />
                        </asp:BoundField>--%>
                        <asp:TemplateField HeaderText="Amt.(USD)">
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                        <ItemTemplate>
                        <asp:TextBox ID="txtamtusd" CssClass="input_box" runat="server"  ReadOnly="true" Width="60px" Text="0.0" style="text-align:right;"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender id="FilteredTextBoxExtender3" runat="server" ValidChars="." TargetControlID="txtamtusd" FilterType="Numbers,Custom">
                    </ajaxToolkit:FilteredTextBoxExtender>
                        </ItemTemplate>
                        </asp:TemplateField>
                       <%-- <asp:BoundField DataField="amt." HeaderText="Amt.(USD)">
                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                        </asp:BoundField>--%>
                    </Columns>
                   <RowStyle CssClass="rowstyle" />
                <SelectedRowStyle CssClass="selectedtowstyle" />
                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                </asp:GridView>
            </div>
        </td>
    </tr>
    <tr>
        <td style="padding-bottom: 0px; padding-top: 0px; background-color:#e2e2e2 ">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                <td style="text-align: right">
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgpodate" PopupPosition="TopRight" TargetControlID="txtpodt"></ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" PopupPosition="TopRight" TargetControlID="txteta"></ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton2" PopupPosition="TopRight" TargetControlID="txtetd"></ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers,Custom" TargetControlID="txtexchangerate" ValidChars="."></ajaxToolkit:FilteredTextBoxExtender>
                        <asp:HiddenField ID="HiddenVendorId" runat="server" />
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <asp:HiddenField ID="HiddenFieldVesselId" runat="server" />
                        <asp:HiddenField ID="HiddenFieldPodate" runat="server" />
                </td>
                <td></td>
                <td style="text-align: right">
                    <span style="float :left " >
                    <asp:Button ID="btn_checkcount" runat="server" CssClass="btn" OnClick="btn_checkcount_Click" Text="Check Count" CausesValidation="False" Width="85px" />
                    &nbsp;&nbsp;&nbsp;
                    Total Crew Count :<asp:Label ID="lbl_CrewCount" runat="server" ></asp:Label>
                    </span>
                    <asp:Button ID="btn_save" runat="server" CssClass="btn" OnClick="btn_save_Click" TabIndex="11" Text="Save" Width="59px" />
                    <asp:Button ID="btn_Cancel" runat="server" CssClass="btn" OnClick="btn_Cancel_Click" TabIndex="12" Text="Cancel" Width="59px" CausesValidation="False" />
                    <asp:Button ID="btn_Printpo" runat="server" CssClass="btn" OnClick="btn_Printpo_Click1" Text="Print PO" Width="59px" CausesValidation="False" Enabled="False" />
                </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
    <td style="height: 43px; background-color :#e2e2e2">
        <table style="width: 100%"><tr>
            <td style="width: 106px; text-align: right">
                Vessel:</td>
            <td style="width: 172px; text-align: left">
                <asp:DropDownList ID="ddl_VesselName" runat="server" CssClass="input_box" Width="278px" TabIndex="13">
                </asp:DropDownList></td>
        <td style="width: 92px; text-align: right">
            Country:</td>
        <td style="width: 86px; text-align: left"><asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" CssClass="input_box"
                              OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="210px" TabIndex="16">
                </asp:DropDownList></td>
        <td style="width: 43px; text-align: right">
            Port:</td>
            <td colspan="2" style="text-align: left">
                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="input_box" TabIndex="17"
                            Width="205px">
                    </asp:DropDownList></td>
        </tr>
            <tr>
                <td style="width: 106px; text-align: right">
                    Vendor:</td>
                <td style="width: 172px; text-align: left">
                    <asp:DropDownList ID="ddl_VendorName" runat="server" CssClass="input_box" Width="278px">
                    </asp:DropDownList></td>
                <td style="width: 92px; text-align: right">
                    PO REF#:</td>
                <td style="width: 86px; text-align: left">
                    <asp:TextBox ID="txtsearchpo" runat="server" CssClass="input_box" MaxLength="45" TabIndex="14"></asp:TextBox></td>
                <td style="width: 43px; text-align: right">
                </td>
                <td style="width: 192px; text-align: left">
                    <asp:Button ID="btnsearch" runat="server" CssClass="btn" OnClick="btnsearch_Click"
            TabIndex="18" Text="Search" Width="59px" ValidationGroup="ss" /></td>
                <td style="text-align: left">
                    </td>
            </tr>
        </table>
    </td></tr>
    <tr>
    <td style=" text-align:center; padding-bottom: 0px;">
    <table cellpadding="0" cellspacing ="0" border ="1">
    <tr>
    <td>
    <asp:Label ID="lbl_gvpo" runat="server"></asp:Label>
    <div style="overflow-y: scroll; overflow-x: scroll; width:810px; height: 150px">
    <asp:GridView ID="gvPOList" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" Style="text-align: center" Width="97%" DataKeyNames="PoId" OnSelectedIndexChanged="gvPOList_SelectedIndexChanged" OnPreRender="gvPOList_PreRender" OnRowDeleting="gvPOList_RowDeleting">
    <Columns>
    <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True">
    <ItemStyle Width="35px" />
    </asp:CommandField>
    <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemStyle Width="35px" />
    <ItemTemplate>
    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
    </ItemTemplate>
    </asp:TemplateField>
    <asp:BoundField DataField="PORefNo" HeaderText="PO Ref. #">
            <ItemStyle HorizontalAlign="Left" Width="100px" />
        </asp:BoundField>
    <asp:BoundField DataField="VSL" HeaderText="VSL">
            <ItemStyle HorizontalAlign="Left" Width="30px"/>
        </asp:BoundField>
    <asp:BoundField DataField="Port" HeaderText="Port">
            <ItemStyle HorizontalAlign="Left" Width="90px" />
        </asp:BoundField>
    <asp:BoundField DataField="PortCallReferenceNo" HeaderText="Port Call Ref#">
            <ItemStyle HorizontalAlign="Left" Width="100px" />
        </asp:BoundField>
    <asp:BoundField DataField="RFQNo" HeaderText="RFQ#">
            <ItemStyle HorizontalAlign="Left" Width="50px"/>
        </asp:BoundField>
        <asp:TemplateField HeaderText="PO Dt.">
        <ItemTemplate >
        <%# DateTime.Parse(Eval("PoDate").ToString()).ToString("dd-MMM-yyyy")%>
        </ItemTemplate>
        </asp:TemplateField>
    <%--<asp:BoundField DataField="PoDate" HeaderText="PO Dt.">
            <ItemStyle HorizontalAlign="Left" Width="60px" />
        </asp:BoundField>--%>
    <asp:BoundField DataField="POAmount" HeaderText="PO Amt." DataFormatString="{0:0.0}" HtmlEncode="False">
            <ItemStyle HorizontalAlign="Right" Width="70px"  />
        </asp:BoundField>
    <asp:BoundField DataField="Currency" HeaderText="Curr">
            <ItemStyle HorizontalAlign="Right" Width="30px"  />
        </asp:BoundField>
    <asp:BoundField DataField="Status" HeaderText="Status">
            <ItemStyle HorizontalAlign="Left" Width="30px" />
        </asp:BoundField>
    </Columns>
    <RowStyle CssClass="rowstyle" />
    <SelectedRowStyle CssClass="selectedtowstyle" />
    <HeaderStyle CssClass="headerstylefixedheadergrid" />
</asp:GridView>
        </div>
    </td>
    <td style="width :252px; background-color :#e2e2e2">
    <table cellpadding="0" cellspacing="0">
    <tr><td style="padding-right: 5px" align="right">
            Annual Budget:</td><td>
                <asp:TextBox ID="txtbudget" runat="server" CssClass="input_box" TabIndex="19" 
                    Width="100px" style="text-align:right" ReadOnly="True"></asp:TextBox>
                </td></tr>
                 <tr><td style="padding-right: 5px" align="right">
                     Year to Dt. Committed:</td><td>
                     <asp:TextBox ID="txtcommitted" runat="server" CssClass="input_box" TabIndex="20" 
                             Width="100px" style="text-align:right" ReadOnly="True"></asp:TextBox></td></tr>
            <tr>
                <td align="right" style="padding-right: 5px; height: 19px;">
                    Year to Dt. Actual:</td>
                <td style="height: 19px">
                    <asp:TextBox ID="txtActual" runat="server" CssClass="input_box" TabIndex="21" 
                        Width="100px" style="text-align:right" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right" style="padding-right: 5px">
                    Year to Dt. Consumed:</td>
                <td>
                    <asp:TextBox ID="txtcunsumed" runat="server" CssClass="input_box" TabIndex="22" 
                        Width="100px" style="text-align:right" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right" style="padding-right: 5px">
                    Budget Remaining:</td>
                <td>
                    <asp:TextBox ID="txtremaining" runat="server" CssClass="input_box" 
                        TabIndex="23" Width="100px" style="text-align:right" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right" style="padding-right: 5px">
                    Utilization (%):</td>
                <td>
                    <asp:TextBox ID="txtutilization" runat="server" CssClass="input_box" 
                        TabIndex="24" style="text-align:right"
                        Width="100px" ReadOnly="True"></asp:TextBox></td>
            </tr>
                </table>
    </td>
    </tr>
    </table>
    </td>
    </tr>
    </table>
</asp:Content>

