<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuestionMapping.aspx.cs" Inherits="Registers_QuestionMapping" ValidateRequest="false" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>EMANAGER</title>
       <link href="../../HRD/Styles/style.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
     
    <style type="text/css">
        .style1
        {
            width: 349px;
        }
        .style2
        {
            width: 138px;
        }
        .style3
        {
            width: 234px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server" ></ajaxToolkit:ToolkitScriptManager> 
    <center>
     <table cellpadding="0" cellspacing="0" style="width: 95%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; background-color:#f9f9f9">
     <tr>
        <td colspan="5" style=" height:23px; color :White; text-align :center " class="text headerband"><b>Question Mapping</b></td>
     </tr>
     <tr>
        <td>
        <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">    
     <tr>
       <td>
       <script type="text/javascript">
       function DescQuestion()
       {
        var val = document.getElementById("hfdQno").value; 
        Desc(val); 
       }
       function DescQuestion1()
       {
        var val = document.getElementById("hfdQno1").value; 
        Desc(val); 
       }
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
            <asp:HiddenField ID="HiddenInspectionCheckList" runat="server" />
            <asp:Panel ID="pnl_InspectionChkLst" runat="server" Width="100%">
            <center>
                <table border="0" cellpadding="2" cellspacing="0" style="text-align: center" width="100%;">
                      <tr style=" font-size :12px; color :White ; height :20px; " class= "headerstylegrid" >
                          <td align="right" style="text-align: right; padding-right:5px; " >&nbsp;</td>
                          <td style="text-align: left;"><span style=" font-size :11px;color :White ; font-weight:bold " >Source</span></td>
                          <td align="right" style="text-align: right; padding-right:5px;">&nbsp;</td>
                          <td style="text-align: left;">&nbsp;</td>
                          <td style="text-align: left; padding-right: 5px;"><span style=" font-size :11px; color :White ; font-weight:bold;" >Destination</span></td>
                          <td style="text-align: left;">&nbsp;</td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right: 5px;width:150px;" >
                              Inspection Group:</td>
                          <td style="text-align: left;" >
                              <asp:DropDownList ID="ddl_InspGroup" runat="server" AutoPostBack="True" 
                                  CssClass="input_box" OnSelectedIndexChanged="ddl_InspGroup_SelectedIndexChanged" 
                                  TabIndex="1" Width="260px">
                              </asp:DropDownList>
                          </td>
                           <td style="text-align: right; padding-right: 5px;">
                              &nbsp;</td>
                          <td align="right" style="padding-right: 5px; text-align:right;width:150px; ">
                              Inspection Group:</td>
                          <td style="text-align: left;">
                               <asp:DropDownList ID="ddl_InspGroup1" runat="server" AutoPostBack="True" 
                                  CssClass="input_box" OnSelectedIndexChanged="ddl_InspGroup1_SelectedIndexChanged" 
                                  TabIndex="1" Width="260px">
                              </asp:DropDownList></td>
                         
                          <td style="text-align: left;width:150px;">
                              &nbsp;</td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right: 5px;" >
                              Version #: </td>
                          <td style="text-align: left;" >
                              <asp:DropDownList ID="ddlVersions" runat="server" AutoPostBack="True" 
                                  CssClass="input_box" TabIndex="4" 
                                  Width="260px">
                              </asp:DropDownList>
                          </td>
                          <td align="right" style="text-align: right; padding-right: 5px;">
                              &nbsp;</td>
                        <td align="right" style="text-align: right; padding-right: 5px;" >
                              Version #: </td>
                          <td style="text-align: left;">
                              <asp:DropDownList ID="ddlVersions1" runat="server" AutoPostBack="True" 
                                  CssClass="input_box" TabIndex="4" 
                                  Width="260px">
                              </asp:DropDownList>
                          </td>
                          <td style="text-align: left;">
                              &nbsp;</td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right: 5px; vertical-align :top;" >
                              Question #:</td>
                          <td style="text-align: left;vertical-align :top;" >
                              <asp:UpdatePanel runat="server" ID="up1">
                              <ContentTemplate>
                                  <asp:TextBox runat="server" ID="txtQno" CssClass="input_box" AutoPostBack="true" ontextchanged="txtQno_TextChanged"></asp:TextBox> 
                                  <asp:HiddenField runat="server" ID="hfdQno" />
                                  <img ID="imgdesc" onclick="DescQuestion();" src="../../HRD/Images/cv.png" alt=""/>
                                  <br />
                                  <asp:Label ID="lblQno" runat="server" Width="300px"></asp:Label>
                              </ContentTemplate> 
                              </asp:UpdatePanel>   
                              </td>
                          <td align="right" style="text-align: right; padding-right: 5px;">
                              &nbsp;</td>
                          <td align="right" style="text-align: right; padding-right: 5px;vertical-align :top;" >
                              Question #:</td>
                          <td style="text-align: left;vertical-align :top;">
                              <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                              <ContentTemplate>
                              <asp:TextBox runat="server" ID="txtQno1" CssClass="input_box" AutoPostBack="true" ontextchanged="txtQno1_TextChanged"></asp:TextBox> 
                              <asp:HiddenField runat="server" ID="hfdQno1" />
                              <img ID="img1" onclick="DescQuestion1();" src="../../HRD/Images/cv.png" alt=""/>
                              <br />
                              <asp:Label ID="lblQno1" runat="server" Width="300px" ></asp:Label>
                              </ContentTemplate>
                              </asp:UpdatePanel>
                              </td>
                          <td style="text-align: left;">
                              <asp:Button ID="btnAdd" runat="server" CssClass="btn" OnClick="Add_Mapping" 
                                  Text="Add" Width="100px" />
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right: 5px;" >
                              &nbsp;</td>
                          <td style="text-align: left;" >
                              &nbsp;</td>
                          <td align="right" style="text-align: right; padding-right: 5px;">
                              &nbsp;</td>
                          <td align="right" style="text-align: right; padding-right: 5px;" >
                              &nbsp;</td>
                          <td style="text-align: left;">
                              &nbsp;</td>
                          <td style="text-align: left;">
                              &nbsp;</td>
                      </tr>
                      <tr>
                          <td align="right" colspan="6" 
                              style="padding-right: 5px; height: 3px; text-align: center">
                              <asp:Label ID="lbl_InspectionCheckList_Message" runat="server" 
                                  ForeColor="#C00000"></asp:Label>
                          </td>
                      </tr>
                </table>   
                <table cellspacing="0" rules="all" border="1" style="width:98%;border-collapse:collapse;">
			                <tr class= "headerstylegrid">
				                <th scope="col" style="width :45%; text-align :center " >Source</th>
				                <th scope="col" style="width :45%; text-align :center ">Destination</th>
				                <th scope="col" style="width :10%; text-align :center ">Delete</th>
			                </tr></table>
                <asp:GridView ID="GridView_InspectionCheckList" runat="server" 
                                  AutoGenerateColumns="False" Width="98%" GridLines="Both" 
                                  onrowdeleting="GridView_InspectionCheckList_RowDeleting" 
                                  onrowcommand="GridView_InspectionCheckList_RowCommand" >
                            <RowStyle CssClass="rowstyle" />
                            <Columns>
                            <asp:TemplateField HeaderText="Question#" ItemStyle-Width="5%" >
                                <ItemTemplate>
                                <asp:Label ID="lbl_QuestNo" runat="server" Text='<%#Eval("QuestionNo")%>'></asp:Label>
                                <asp:HiddenField ID="Hidden_QId" runat="server" Value='<%#Eval("ID")%>' />
                                <asp:HiddenField ID="Hidden_QId1" runat="server" Value='<%#Eval("ID1")%>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="5%"><ItemTemplate><img style='display:<%# (Eval("Id").ToString().Trim()=="")?"None":"Block" %>; cursor: hand' onclick="<%# "Desc('" + Eval("Id").ToString() + "');" %>"  src="../../HRD/Images/cv.png" /></ItemTemplate><ItemStyle></ItemStyle></asp:TemplateField>
                            <asp:BoundField DataField="Question" HeaderText="Question"><ItemStyle HorizontalAlign="Left" Width="35%" ></ItemStyle></asp:BoundField>
                           
                            <asp:BoundField DataField="QuestionNo1" HeaderText="Question#"><ItemStyle HorizontalAlign="Left" Width="5%"></ItemStyle></asp:BoundField>
                            <asp:TemplateField ItemStyle-Width="5%"><ItemTemplate><img style='display:<%# (Eval("Id").ToString().Trim()=="")?"None":"Block" %>; cursor: hand' onclick="<%# "Desc('" + Eval("Id").ToString() + "');" %>"  src="../../HRD/Images/cv.png" /></ItemTemplate><ItemStyle></ItemStyle></asp:TemplateField>
                            <asp:BoundField DataField="Question1" HeaderText="Question"><ItemStyle HorizontalAlign="Left" Width="35%"></ItemStyle></asp:BoundField>
                            
                            <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                    <center>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/delete.jpg" ToolTip="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" CommandArgument='<%#Eval("ID").ToString()+ "|"+Eval("ID1").ToString()%>'  />
                                    </center>
                                </ItemTemplate>
                            <ItemStyle></ItemStyle></asp:TemplateField>
                            </Columns>
                            <pagerstyle horizontalalign="Center" />
                            <SelectedRowStyle CssClass="selectedtowstyle" />
                            <HeaderStyle CssClass="headerstylefixedheadergrid" />
                            </asp:GridView>
            </center> 
            </asp:Panel>
            </td>
         </tr>
        </table>
       </td>
      </tr>
     </table>
        </td>
     </tr>
     </table>
     </center>
</form>
</body>
</html>


