<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssessmentRegister.aspx.cs" Inherits="Register_AssessmentMaster" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
<asp:ScriptManagerProxy ID="ScriptManager1" runat="server"></asp:ScriptManagerProxy>
    <asp:HiddenField ID="HiddenTrainingType" runat="server" />
        <div style="padding:4px; text-align:right;">
            <asp:Label ID="lblCountRegister" runat="server" style="float:left" ></asp:Label>
            <asp:Button ID="btn_Add_AssessmentMaster" runat="server" CssClass="btn" Text="Add" Width="59px" OnClick="btn_Add_AssessmentMaster_Click" CausesValidation="False" TabIndex="3" Visible="true" />
        </div>
        <div style="overflow-x:hidden;overflow-y:scroll; height:23px;">       
            <table  cellpadding="4" cellspacing="0" border="1" width="100%" style=" border-collapse :collapse;" >            
                <col width="40px" />
                <col />
                <col width="150px" />
                <tr style="text-align :center; background-color :LightGray; font-weight:bold;" class="hd">                
                    <td>Edit</td>
                    <td style="text-align:left;">Assessment Name</td>
                    <td>Allow on Crew Portal</td>
                </tr>
            </table>
            </div>
        <div style="overflow-x:hidden;overflow-y:scroll; height:150px;"> 
            <table  cellpadding="4" cellspacing="0" border="1" width="100%" style=" border-collapse :collapse" >
                <col width="40px" />
                <col />
                <col width="150px" />
            <asp:Repeater ID="rptAssessmentMaster" runat="server">
                <ItemTemplate>
                    <tr>
                        <td> 
                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Modules/HRD/Images/addpencil.gif" OnClick="btnEdit_OnClick" /> 
                            <asp:HiddenField ID="hfAM_ID" runat="server" Value='<%#Eval("AM_ID") %>' />
                        </td>
                        <td style="text-align:left;"> 
                            <asp:Label ID="lblname" runat="server" Text='<%#Eval("AM_Name") %>' ></asp:Label>

                        </td>
                        <td style="text-align:left;"> 
                            <asp:Label ID="lblAllow" runat="server" Text='<%#((Eval("AllowOnCrewPortal").ToString()=="True")?"Yes":"No")%>' ></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
         </table>
        </div>            
        <%------------------------------------------------------------------%>
        <div id="divAddAssessment" style="position:fixed;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" visible="false" >
            <center>
                <div style="position:absolute;top:0px;left:0px; height :700px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                <div style="position :relative; width:400px; padding :10px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:5px;opacity:1;filter:alpha(opacity=100)">

                    <div style="text-align:center;background-color:#f2e9e9;padding:5px;font-size:13px;"> <b> Add Assessment </b></div>

                        <table border="0" cellpadding="5" cellspacing="2" style="text-align:left;" width="100%">
                    <col width="150px" />
                    <col />
                    <col width="200px" />
                    <tr>
                        <td>Assessment Name:</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtAssessmentName" runat="server" CssClass="required_box" Width="350px" MaxLength="49" TabIndex="1"></asp:TextBox>
                        </td>
                    </tr>
                            <tr>
                        <td>Allow to show on Crew Portal :</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkAllow" runat="server" Text="Allow"></asp:CheckBox>
                        </td>
                    </tr>
                            <tr>
                                <td style="text-align:center;">
                                    <asp:Button ID="btn_Save_AssessmentMaster" runat="server" CssClass="btn" Text="Save" Width="59px" OnClick="btn_Save_AssessmentMaster_Click" TabIndex="4"  Visible="true"/>
                                    <asp:Button ID="btn_Cancel_AssessmentMaster" runat="server" CssClass="btn" Text="Cancel" Width="59px" OnClick="btn_Cancel_AssessmentMaster_Click" CausesValidation="False" TabIndex="5"   Visible="true" />
                                 </td>
                            </tr>

                    <tr>
                        <td colspan="4" style="text-align:center;">
                            <asp:Label ID="lblMsg" runat="server" ForeColor="#C00000"></asp:Label>&nbsp;<br />
                        </td>
                    </tr>
                    </table>
                    </div>
                </center>
            </div>

</asp:Content>
 
