<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="InternalInspectionsQuestions.aspx.cs" Inherits="Register_InternalInspectionsQuestions"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>EMANAGER</title>
      <link href="../../HRD/Styles/style.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
<script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
<script src="../KPIScript.js" type="text/javascript"></script>
<style type="text/css">
    .bordered tr td 
    {
        border:solid 1px #e2e2e2;
        padding:3px;
    }
    .bordered tr th
    {
        border:solid 1px #e2e2e2;
        background-color:#c2c2c2;
        padding:3px;
    }
</style>
 </head>
<body  >
<form id="form1" runat="server" >
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

        
<table cellpadding="3" cellspacing="0" style="width: 100%">
                        <tr>
                            <td style="padding:5px;text-align:right;">
                                Inspection :
                            </td>
                            <td style="padding:5px; text-align:left;">
                                <asp:DropDownList ID="ddlInspection" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlInspection_OnSelectedIndexChanged"/>
                            </td>
                            <td style="padding:5px;text-align:right;">
                                Version :
                            </td>
                            <td style="padding:5px;text-align:left;">
                                <asp:DropDownList ID="ddlVersion" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="ddlVersion_OnSelectedIndexChanged"/>
                                <asp:ImageButton ID="btnAddVersion" runat="server" OnClick="btnAddVersion_OnClick" ImageUrl="~/Modules/HRD/Images/add_12.jpg" />                                
                                <asp:ImageButton ID="btnEditVersion" runat="server" OnClick="btnEditVersion_OnClick" ImageUrl="~/Modules/HRD/Images/editX12.jpg" />                                
                            </td>
                            <td style="padding:5px;text-align:right;">
                                Category / Group :
                            </td>
                            <td style="padding:5px;text-align:left;">
                                <asp:DropDownList ID="ddlCategory" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged"/>
                                <asp:ImageButton ID="btnAddCat" runat="server" OnClick="btnAddCat_OnClick" ImageUrl="~/Modules/HRD/Images/add_12.jpg" />
                                <asp:ImageButton ID="btnEditCat" runat="server" OnClick="btnEditCat_OnClick" ImageUrl="~/Modules/HRD/Images/editX12.jpg" Visible="false" />
                            </td>
                            <td style="padding:5px;text-align:right;">
                                Question Text :
                            </td>
                            <td style="padding:5px;text-align:left;">
                                <asp:TextBox ID="txtFQText" runat="server"/>
                            </td>
                            <td style="padding:5px;text-align:left;">
                                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_OnClick" Text="Search" CssClass="btn" />
                                <asp:Button ID="btnAddQ" runat="server" OnClick="btnAddQ_OnClick" Text="+ Add New" CssClass="btn" />                                
                            </td>
                        </tr>
                    </table>
            
        </ContentTemplate>
    </asp:UpdatePanel>
<div style="width: 100%; height: 365px; overflow-x:hidden;overflow-y:scroll;" class='ScrollAutoReset' id='dv_KPI_HSQE456'>
                              <table width="100%" style="border-collapse:collapse;" class="bordered">
                              <tr class= "headerstylegrid">
                                    <th style="width:80px; text-align:center;">Edit</th>
                                    <th style="width:80px">Sr#</th>
                                    <th style="width:100px">Inspection</th>
                                    <th style="width:200px">Category/Group</th>
                                    <th style="width:100px">Question#</th>
                                    <th>Question</th>
                              </tr>
                              <asp:Repeater runat="server" ID="rptQuestions">
                              <ItemTemplate>
                                <tr>
                                    <td style="text-align:center"> 
                                        <asp:ImageButton ID="btnEditQuestion" runat="server"  ImageUrl="~/Modules/HRD/Images/editx12.jpg" OnClick="btnEditQuestion_OnClick"/>
                                        <asp:HiddenField ID="hfdQuestionID" runat="server" Value='<%#Eval("QuestionId") %>' />
                                    </td>
                                    <td><%#Eval("SrNo")%></td>
                                    <td><%#Eval("InspectionName")%></td>
                                    <td><%#Eval("CategoryName")%></td>
                                    <td><%#Eval("QuestionNo")%></td>
                                    <td><%#Eval("QuestionName")%></td>
                                </tr>                              
                              </ItemTemplate>
                              </asp:Repeater>
                              </table>
                            </div>
    <div>
        <asp:Label ID="lblMessege" runat="server" style="color:Red; font-size:12px;"></asp:Label>
    </div>
</div>

<!-- Add Version -->
<div style="position:absolute;top:50px;left:0px; height :470px; width:100%;z-index:100;" runat="server" id="dvAddVersion" visible="false" >
<center>
<div style="position:absolute;top:50px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
<div style="position :relative; width:400px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:75px;opacity:1;filter:alpha(opacity=100)">
    <div style="padding:5px; " class="text headerband">Add New Version</div>
    <div style="margin:10px;">
        <table>
        <tr>
            <td>Version : </td>
            <td><asp:TextBox runat="server" MaxLength="50" ID="txtVersion" ValidationGroup="version"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtVersion" ErrorMessage="*" ForeColor="Red" ValidationGroup="version"></asp:RequiredFieldValidator>
            </td>
        </tr>
        </table>
        <div>
            <asp:Button ID="btnSaveVersion" runat="server" OnClick="btnSaveVersion_OnClick" Text="Save" CssClass="btn" ValidationGroup="version"/>
           <asp:Button ID="btnClose" runat="server" OnClick="btnClose_OnClick" Text="Close" CssClass="btn"  CausesValidation="false"/>
        </div>
    </div>
</div>
</center>
</div>

<!-- Add Category -->
<div style="position:absolute;top:50px;left:0px; height :470px; width:100%;z-index:100;" runat="server" id="dvAddCategory" visible="false" >
<center>
<div style="position:absolute;top:50px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
<div style="position :relative; width:600px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:75px;opacity:1;filter:alpha(opacity=100)">
    <div style="padding:5px; " class="text headerband">Add New Category/Group</div>
    <div style="margin:10px;">
        <table border="0" width="100%">
       
            <tr>
            <td style="width:100px">Category Code : </td>
            <td><asp:TextBox runat="server" MaxLength="50" ID="txtCategoryCode" ValidationGroup="cat" Width="30%"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCategoryCode" ErrorMessage="*"  ForeColor="Red" ValidationGroup="cat"></asp:RequiredFieldValidator>
            </td>
        </tr>
             <tr>
            <td>Category Name : </td>
            <td><asp:TextBox runat="server" MaxLength="50" ID="txtCategory" ValidationGroup="cat" Width="90%"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCategory" ErrorMessage="*" ForeColor="Red" ValidationGroup="cat"></asp:RequiredFieldValidator>
            </td>
        </tr>
        </table>
        <br />
        <div>
            <asp:Button ID="Button1" runat="server" OnClick="btnSaveCategory_OnClick" Text="Save" CssClass="btn" ValidationGroup="cat"/>
           <asp:Button ID="Button2" runat="server" OnClick="btnClose_OnClick" Text="Close" CssClass="btn"  CausesValidation="false"/>
        </div>
        <div>
            <asp:Label ID="lblMsgAddCat" runat="server" BackColor="Red" ></asp:Label>
        </div>
    </div>
</div>
</center>
</div>

<!-- Add Question -->
<div style="position:absolute;top:50px;left:0px; height :470px; width:100%;z-index:100;" runat="server" id="dvQuestion" visible="false" >
<center>
<div style="position:absolute;top:50px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
<div style="position :relative; width:80%; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:75px;opacity:1;filter:alpha(opacity=100)">
    <div style="padding:5px; " class="text headerband" >Add New Question</div>
    <div style="margin:10px;">
        <table width="100%" border="0">
        <tr>
            <td style="vertical-align:top;width:130px;">Inspection </td>
            <td style="text-align:left">:&nbsp;<asp:Label runat="server" MaxLength="50" ID="lblInspectionName" ></asp:Label></td>
        </tr>
        <tr>
            <td style="vertical-align:top;">Version </td>
            <td style="text-align:left">:&nbsp;<asp:Label runat="server" MaxLength="50" ID="lblVersion"></asp:Label></td>
        </tr>
        <tr>
            <td style="vertical-align:top;">Category/Group </td>
            <td style="text-align:left">:&nbsp;<asp:Label runat="server" MaxLength="50" ID="lblCategory"></asp:Label></td>
        </tr>
        <tr>
            <td style="vertical-align:top;">Question No./Code</td>
            <td>:
                <asp:Label runat="server" MaxLength="50" ID="lblCatCode" style="text-align:right;"></asp:Label> . 
                <asp:TextBox runat="server" MaxLength="50" ID="txtQno" ValidationGroup="question"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtQno" ErrorMessage="*" ForeColor="Red" ValidationGroup="question"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="vertical-align:top;">Question Text </td>
            <td style="vertical-align:top;">:&nbsp;<asp:TextBox runat="server" MaxLength="50" ID="txtQuestionText" TextMode="MultiLine" ValidationGroup="question" Width="95%" Rows="5"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQuestionText" ErrorMessage="*" ForeColor="Red" ValidationGroup="question"></asp:RequiredFieldValidator>
            </td>
        </tr>
 <tr>
            <td style="vertical-align:top;">Guidance</td>
            <td style="vertical-align:top;">:&nbsp;<asp:TextBox runat="server" MaxLength="50" ID="txtGuidance" TextMode="MultiLine" ValidationGroup="question" Width="95%" Rows="5"></asp:TextBox>
            
            </td>
        </tr>
            <tr>
            <td style="vertical-align:top;">Ratings Required </td>
            <td style="text-align:left">:&nbsp;
                <asp:DropDownList runat="server" ID="ddlRating"    >
                    <asp:ListItem Text="< Select >" Value=""></asp:ListItem>
                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlRating" ErrorMessage="*" ForeColor="Red" ValidationGroup="question"></asp:RequiredFieldValidator>
            
            </td>
        </tr>
        </table>
        <div>
            <asp:Button ID="Button3" runat="server" OnClick="btnSaveQuestion_OnClick" Text="Save" CssClass="btn" ValidationGroup="question"/>
           <asp:Button ID="Button4" runat="server" OnClick="btnClose_OnClick" Text="Close" CssClass="btn"  CausesValidation="false"/>
        </div>
    </div>
</div>
</center>
</div>

  </form>
</body>
</html>
