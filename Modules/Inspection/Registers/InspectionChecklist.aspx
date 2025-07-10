<%@ Page Language="C#" MasterPageFile="~/Modules/Inspection/Registers/RegistersMasterPage.master" AutoEventWireup="true" CodeFile="InspectionChecklist.aspx.cs" Inherits="Registers_InspectionChecklist" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">    
     <tr>
       <td>
       <script type="text/javascript">
        function Desc(str)
            {
                window.open('CheckListDescPopUp.aspx?ChkLstId='+str,'','title=no,resizable=no,location=no,width=400px,height=400px,top=190px,left=550px,addressbar=no,status=yes,scrollbars=1');
            }
            function AddEditCheckList()
            {
                var gid=document.getElementById('ctl00_ContentPlaceHolder1_ddl_InspGroup').value;
                var chap=document.getElementById('ctl00_ContentPlaceHolder1_ddlChapterName').value;
                var schap=document.getElementById('ctl00_ContentPlaceHolder1_ddlSubChapter').value;  
                
                window.open('AddEditCheckListPopUp.aspx?GRP=' + gid + '&MC=' + chap + '&SC=' +schap,'','title=no,resizable=no,location=no,width=1000px,height=420px,top=100px,left=100px,addressbar=no,status=yes,scrollbars=1');
            }
            function EditCheckList(Param)
            {
                window.open('AddEditCheckListPopUp.aspx?Id=' + Param,'','title=no,resizable=no,location=no,width=1000px,height=420px,top=100px,left=100px,addressbar=no,status=yes,scrollbars=1');
            }
            function PrintInspCheckListRPT()
            {
                var aa = document.getElementById('ctl00_ContentPlaceHolder1_HiddenField_ChapId').value;
                if(aa!="")
                {
                    window.open('..\\Reports\\InspCheckList_Report.aspx?ChapId='+aa,null,'title=no,toolbars=no,scrollbars=yes,width=850,height=650,left=20,top=20,addressbar=no');
                }
            }
       </script>
        <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="text-align: center;">
            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 0px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
            <asp:Panel ID="pnl_InspectionChkLst" runat="server" Width="100%">
                <table border="0" cellpadding="0" cellspacing="0" style="text-align: center" width="100%">
                      <tr>
                          <td colspan="6" style="height: 15px">
                              <asp:HiddenField ID="HiddenInspectionCheckList" runat="server" />
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px;">
                              Inspection Group:</td>
                          <td style="text-align: left;">
                              <asp:DropDownList ID="ddl_InspGroup" runat="server" CssClass="input_box" Width="203px" TabIndex="1" AutoPostBack="True" OnSelectedIndexChanged="ddl_InspGroup_SelectedIndexChanged">
                              </asp:DropDownList></td>
                          <td align="right" style="text-align: right; padding-right:15px;">
                              Chapter:</td>
                          <td style="text-align: left;"><asp:DropDownList ID="ddlChapterName" runat="server" CssClass="input_box" Width="203px" TabIndex="2" AutoPostBack="True" OnSelectedIndexChanged="ddlChapterName_SelectedIndexChanged">
                          </asp:DropDownList></td>
                          <td style="text-align: right; padding-right: 15px;">
                              Sub Chapter:</td>
                          <td style="text-align: left;">
                              <asp:DropDownList ID="ddlSubChapter" runat="server" CssClass="input_box" Width="259px" TabIndex="3" AutoPostBack="True" OnSelectedIndexChanged="ddlSubChapter_SelectedIndexChanged">
                          </asp:DropDownList></td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 3px;">
                          </td>
                          <td style="text-align: left; height: 3px;">
                              </td>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 3px;">
                          </td>
                          <td style="text-align: left; height: 3px;">
                              </td>
                          <td style="height: 3px; text-align: left;">
                          </td>
                          <td style="height: 3px; text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px;">
                              Vessel Type:
                          </td>
                          <td style="text-align: left;">
                              <asp:DropDownList ID="ddlVesselType" runat="server" CssClass="input_box" Width="203px" TabIndex="4" AutoPostBack="True" OnSelectedIndexChanged="ddlVesselType_SelectedIndexChanged"></asp:DropDownList>
                          </td>
                          <td align="right" style="text-align: right; padding-right:15px;">
                              Version #:
                          </td>
                          <td style="text-align: left;">
                              <asp:DropDownList ID="ddlVersions" runat="server" CssClass="input_box" 
                                  Width="203px" TabIndex="4" AutoPostBack="True" 
                                  OnSelectedIndexChanged="ddlVersion_SelectedIndexChanged"></asp:DropDownList>
                          </td>
                          <td style="text-align: right; padding-right: 15px;">QNo.</td>
                          <td style="text-align: left">
                          <asp:TextBox runat="server" AutoPostBack="true" OnTextChanged="Qno_Changed" Width="50px" id="txtQno" CssClass="input_box"></asp:TextBox>
                          <asp:Button ID="btn_New_InspectionCheckList" runat="server" CssClass="btn" Text="New" Width="59px" CausesValidation="False" TabIndex="13" OnClientClick="AddEditCheckList();" /></td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; height: 3px; text-align: right">
                          </td>
                          <td style="height: 3px; text-align: left">
                          </td>
                          <td align="right" style="padding-right: 15px; height: 3px; text-align: right">
                          </td>
                          <td style="height: 3px; text-align: left">
                          </td>
                          <td style="padding-right: 15px; height: 3px; text-align: right">
                          </td>
                          <td style="height: 3px; text-align: left">
                          
                          </td>
                      </tr>
                </table>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td style="text-align: right; padding-right: 17px;">
                        <div id="Div1" runat="server" style="position: relative; top: 0; text-align: center; left: 0"><asp:Label ID="lbl_InspectionCheckList_Message" runat="server" ForeColor="#C00000"></asp:Label></div>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" style="width: 100%; padding-bottom: 2px">
                    <tr>
                        <td style="height:8px"></td>
                    </tr>
                    <tr>
                        <td style="padding-right: 5px; padding-left: 5px">
                            <div style="width: 100%; height: 355px"><%-- height: 150px--%>
                       <asp:GridView ID="GridView_InspectionCheckList" runat="server" AutoGenerateColumns="False" Width="98%" GridLines="Both" OnPreRender="GridView_InspectionCheckList_PreRender" OnRowDataBound="GridView_InspectionCheckList_RowDataBound" OnRowDeleting="GridView_InspectionCheckList_RowDeleting" OnRowEditing="GridView_InspectionCheckList_RowEditing" OnRowCreated="GridView_InspectionCheckList_RowCreated" AllowPaging="True" OnPageIndexChanging="GridView_InspectionCheckList_PageIndexChanging" PageSize="13">
                            <RowStyle CssClass="rowstyle" />
                            <Columns>
                            <asp:BoundField DataField="SortOrder" HeaderText="Sr#"><ItemStyle HorizontalAlign="Left" Width="30px"></ItemStyle></asp:BoundField>
                            <asp:TemplateField HeaderText="Vessel Type">
                                <ItemTemplate>
                                <asp:Label ID="lbl_VslType" runat="server" Text='<%# Eval("VslTypeName") %>' Width="200px"></asp:Label>
                                <asp:HiddenField ID="Hidden_InspChlLstId" runat="server" Value='<%# Eval("Id") %>' />
                                <asp:HiddenField ID="Hidden_ChkLstNo" runat="server" Value='<%# Eval("QuestionNo") %>' />
                                <asp:HiddenField ID="Hidden_VslTypeId" runat="server" Value='<%# Eval("VesselType") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="VersionName" HeaderText="Version#"><ItemStyle HorizontalAlign="Left" Width="80px"></ItemStyle></asp:BoundField>
                            <asp:BoundField DataField="QuestionNo" HeaderText="Question#"><ItemStyle HorizontalAlign="Left" Width="80px"></ItemStyle></asp:BoundField>
                            <asp:BoundField DataField="Question" HeaderText="Question"><ItemStyle HorizontalAlign="Left"></ItemStyle></asp:BoundField>
                            <asp:TemplateField HeaderText="Guidance"><ItemTemplate><img ID="imgdesc" style='display:<%# (Eval("Id").ToString().Trim()=="")?"None":"Block" %>; cursor: hand' onclick="<%# "Desc('" + Eval("Id").ToString() + "');" %>"  src="../Images/cv.png" /></ItemTemplate><ItemStyle Width="35px"></ItemStyle></asp:TemplateField>
                            <%--<asp:BoundField DataField="QuestionType" HeaderText="Type"><ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle></asp:BoundField>
                            <asp:BoundField DataField="RefNo" HeaderText="Ref#"><ItemStyle HorizontalAlign="Left"></ItemStyle></asp:BoundField>
                            <asp:BoundField DataField="DeficiencyCode" HeaderText="Def. Code"><ItemStyle HorizontalAlign="Left"></ItemStyle></asp:BoundField>--%>
                            <asp:TemplateField HeaderText="Edit" ShowHeader="False"><ItemTemplate><asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/Images/edit.jpg" ToolTip="Edit" /></ItemTemplate><ItemStyle Width="30px"></ItemStyle></asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemTemplate><asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Images/delete.jpg" ToolTip="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" /></ItemTemplate><ItemStyle Width="40px"></ItemStyle></asp:TemplateField>
                            </Columns>
                            <pagerstyle horizontalalign="Center" />
                            <SelectedRowStyle CssClass="selectedtowstyle" />
                            <HeaderStyle CssClass="headerstylefixedheader" />
                     </asp:GridView>
                     </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-right: 7px; padding-left: 16px; text-align: center">
                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td style="padding-right: 8px; text-align: left">
                     <asp:Label ID="lbl_GridView_InspectionCheckList" runat="server" Text=""></asp:Label></td>
                                    <td style="padding-right: 8px; text-align: right">
                                        Created By:</td>
                                    <td style="text-align: left">
                                                                    <asp:TextBox ID="txtCreatedBy_InspChecklst" runat="server" CssClass="input_box" ReadOnly="True" Width="154px" TabIndex="-1" BackColor="Gainsboro"></asp:TextBox></td>
                                    <td style="padding-right: 8px; text-align: right">
                                        Created On:</td>
                                    <td style="text-align: left">
                                                                    <asp:TextBox ID="txtCreatedOn_InspChecklst" runat="server" CssClass="input_box" ReadOnly="True" Width="72px" TabIndex="-2" BackColor="Gainsboro"></asp:TextBox></td>
                                    <td style="padding-right: 8px; text-align: right">
                                        Modified By:</td>
                                    <td style="text-align: left">
                                                                    <asp:TextBox ID="txtModifiedBy_InspChecklst" runat="server" CssClass="input_box" ReadOnly="True" Width="154px" TabIndex="-3" BackColor="Gainsboro"></asp:TextBox></td>
                                    <td style="padding-right: 5px; text-align: right">
                                        Modified On:</td>
                                    <td style="text-align: left;">
                                                                    <asp:TextBox ID="txtModifiedOn_InspChecklst" runat="server" CssClass="input_box" ReadOnly="True" Width="72px" TabIndex="-4" BackColor="Gainsboro"></asp:TextBox></td>
                                    <td style="padding-right: 8px; text-align: left">
                        <asp:Button ID="btn_Print_InspectionCheckList" runat="server" CssClass="btn" Text="Print" Width="59px" TabIndex="15" OnClientClick="return PrintInspCheckListRPT();" OnClick="btn_Print_InspectionCheckList_Click" CausesValidation="False" /><%-- OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_pnl_InspectionChkLst');"--%></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                </asp:Panel>
                </fieldset>
                <asp:HiddenField id="HiddenFieldGridRowCount" runat="server" />
                <asp:HiddenField ID="HiddenField_ChapId" runat="server" />
          </td>
         </tr>
        </table>
       </td>
      </tr>
     </table>
</asp:Content>

