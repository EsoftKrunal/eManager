<%@ Page Language="C#" MasterPageFile="~/Modules/HRD/CrewTraining.master" AutoEventWireup="true" CodeFile="AssessmentQuestion.aspx.cs" Inherits="CrewOperatin_AssessmentQuestion"%>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
<asp:ScriptManagerProxy ID="ScriptManager1" runat="server"></asp:ScriptManagerProxy>
  <%--  <link rel="stylesheet" type="text/css" href="../Styles/quesiton.css" />--%>
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<style type="text/css">
.selbtn
{
	background-color :#c2c2c2;
	border:solid 1px gray;
	border :none;
	padding:5px 10px 5px 10px;
}
.btn1
{
	background-color :#669900;
	color :White;
	border :none;
    padding:5px 10px 5px 10px;
    
}
</style>

    <div style="text-align: center">    
         
        <div id="div-datagrid" style=" width:100%; overflow:hidden; text-align:center;padding:5px;">
            <table cellpadding="5" cellspacing="5" rules="all" width="100%">
                <tr class="headerstyle">
                    <td> 
                        Select Assessment : <asp:DropDownList ID="ddlAssessment" runat="server" Width="500px" OnSelectedIndexChanged="ddlAssessment_OnSelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
            <%--------------------------------------------------------------------------------%>

            <div id="divContainer" runat="server" visible="false" style="">
                        <div style="padding:10px;background-color:#e5f5ff;">
                        <table width="100%" cellpadding="0" cellspacing="0" >                            
                            <col />
                            <col width="200px" />
                            <tr>
                                <td>
                                    <div style="font-size :15px; font-weight:bold;margin-left:20px;">
                                        <asp:Label ID="lblAssessmentHeading" runat="server" ForeColor="#333"></asp:Label>
                                    </div>
                                </td>
                                <td style="text-align:right;">
                                    <asp:Button runat="server" ID="btnaddnew" Text=" + Add New " CssClass="btncls" OnClick="btnAdd_Click" />                                     
                                    <a ID="aListQuestion" runat="server" class="QuestList btncls" href="" style="text-decoration:none;padding:7px 20px 7px 20px;" target="_blank">List</a>
                                </td>
                            </tr>
                            
                        </table>
                        </div>
                        <%----------------------------------------------------------------------------------------------------%>
                        <div id="divQuesOneByOne" runat="server" style="border:solid 0px red;">
                        <div style="background-color:#ffffcc;">
                        <center>
                        <table cellpadding="2" border="0" >
                                    <colgroup>
                                        <col width="70px" />
                                        <col width="100px" />
                                        <col width="70px" />
                                        <tr>
                                            <td style="width:50px">
                                                <asp:Button ID="btnPrev" runat="server" class="btncls"  text="&lt; Prev"  OnClick="btnPrev_OnClick"/>
                                                <%----%>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="lblTotalQuestion" runat="server"></asp:Label>
                                            </td>
                                            <td style="width:50px">
                                                <asp:Button ID="btnNext" runat="server" class="btncls" text="Next &gt;"  OnClick="btnNext_OnClick"/>
                                            </td>
                                        </tr>
                                    </colgroup>
                                    </table>
                        </center>
                        </div>
                        <table width="100%" >
                            <tr>
                            <td>
                                <div class="Question">
                                    <%--<span class="QuesNO" >15</span>                            --%>
                                    <asp:Label ID="lblQuestion" runat="server" Font-Bold="true" ></asp:Label>                            
                                </div>
                            </td>
                        </tr>    
                        </table>
                        <table width="100%" class="answers"  style="margin:10px;" >
                        
                        <tr>
                            <td id="tdOptionA" runat="server" style="width:50%">
                            <span class="Option">A</span>
                                <p>
                                    <asp:Label ID="lblOptionA" runat="server" ></asp:Label>
                                </p>
                            </td>
                            <td id="tdOptionB" runat="server">
                            <span class="Option">B</span>
                                <p>
                                    <asp:Label ID="lblOptionB" runat="server" ></asp:Label>
                                 </p>
                            </td>
                        </tr>
                        <tr>
                            <td id="tdOptionC" runat="server">
                            <span class="Option">C</span>
                                <p>
                                    <asp:Label ID="lblOptionC" runat="server" ></asp:Label>
                                 </p>
                            </td>
                            <td id="tdOptionD" runat="server">
                            <span class="Option">D</span>
                                <p>
                                    <asp:Label ID="lblOptionD" runat="server" ></asp:Label>
                                </p>
                            </td>
                        </tr>                          
                        </table>
                        <div><b>Status : <asp:Label ID="lblStatus" runat="server" ></asp:Label> </b></div>
                        </div>
                    </div>
            
        
                  
        <asp:Label ID="lblMsg" runat="server" ForeColor="#C00000"></asp:Label>&nbsp;<br />
    </div>

</asp:Content>
 