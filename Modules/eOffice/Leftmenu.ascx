<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Leftmenu.ascx.cs" Inherits="emtm_Mainmenu" %>
<table cellpadding="0" cellspacing ="0">
    <tr>
        <td> 
            <asp:TreeView ID="TreeView1" runat="server" >
                <Nodes>
                    <asp:TreeNode Text="Staff Admin" Value="24" >
                        <asp:TreeNode Text="Personal Details" Value="335" NavigateUrl="~/emtm/StaffAdmin/SearchDetail.aspx"></asp:TreeNode>
                        <asp:TreeNode Text="Compensation &amp; Benefits" Value="336" NavigateUrl="~/emtm/StaffAdmin/Compensation/CompensationBenifits.aspx"></asp:TreeNode>
                        <asp:TreeNode Text="Training" Value="337" NavigateUrl="~/emtm/StaffAdmin/TrainingMatrix.aspx"></asp:TreeNode>
                        <asp:TreeNode Text="Leave & Office Absence" Value="338" NavigateUrl ="~/emtm/StaffAdmin/LeaveSearch.aspx"></asp:TreeNode>
                        <asp:TreeNode Text="Applicant" Value="339" NavigateUrl ="~/emtm/StaffAdmin/Applicant.aspx"></asp:TreeNode>
                        <asp:TreeNode Text="PEAP" Value="340" NavigateUrl ="~/emtm/StaffAdmin/HR_Peap.aspx"></asp:TreeNode>
                        <asp:TreeNode Text="Registers" Value="341" NavigateUrl ="~/emtm/Registers.aspx"></asp:TreeNode>
                        <%--<asp:TreeNode Text="Talk to Employees" Value="TTM" NavigateUrl="~/emtm/MyProfile/Emtm_Profile_TalkToMD.aspx"></asp:TreeNode>--%>
                    </asp:TreeNode>
                    <asp:TreeNode Text="My Profile" Value="My Profile" NavigateUrl="~/emtm/MyProfile/Profile_PersonalDetail.aspx">
                        <asp:TreeNode Text="Personal Details" Value="Personal Details" NavigateUrl="~/emtm/MyProfile/Profile_PersonalDetail.aspx"></asp:TreeNode>
                        <asp:TreeNode Text="Compensation &amp; Benefits" Value="Compensation &amp; Benifits" NavigateUrl="~/emtm/MyProfile/CB_Details.aspx"></asp:TreeNode>
                        <asp:TreeNode Text="Training" Value="Training" NavigateUrl="~/emtm/MyProfile/Profile_TrainingManagement.aspx"></asp:TreeNode>
                        <asp:TreeNode Text="Leave & Office Absence" Value="Leave" NavigateUrl="~/emtm/MyProfile/Profile_LeaveDetails.aspx"></asp:TreeNode>
                        <%--<asp:TreeNode Text="Company Policies" Value="Company Policies"></asp:TreeNode>--%>
                        <asp:TreeNode Text="PEAP" Value="PEAP" NavigateUrl="~/emtm/MyProfile/Profile_Peap.aspx"></asp:TreeNode>
                        <asp:TreeNode Text="Hello MD" Value="TTM" NavigateUrl="~/emtm/MyProfile/Profile_TalkToMD.aspx" Target="_Blank"></asp:TreeNode>
                   </asp:TreeNode>
                   <%--<asp:TreeNode Text="Office Absence" Value="Office Absence" NavigateUrl="~/emtm/OfficeAbsence/Emtm_MainOfficeAbsence.aspx"></asp:TreeNode>--%>
                   <asp:TreeNode Text="Inventory Mgmt" Value="Inventory Mgmt" NavigateUrl="~/emtm/Inventory/Inv_ItemsEntry.aspx"></asp:TreeNode>  
                    <asp:TreeNode Text="Contract Management" Value="Contracts" NavigateUrl="~/emtm/Contracts/Contracts.aspx"></asp:TreeNode>                 
                    <asp:TreeNode Text="Emergency Contact List" Value="EmergencyContactList" NavigateUrl="~/emtm/EmergencyContactList/index.aspx"></asp:TreeNode>                 
                </Nodes>
                <NodeStyle ImageUrl="~/Modules/HRD/Images/Emtm/folder.png" HorizontalPadding="2"/>
                <LevelStyles >
                    <asp:TreeNodeStyle ImageUrl="~/Modules/HRD/Images/Emtm/folder.png"/>
                    <asp:TreeNodeStyle ImageUrl="~/Modules/HRD/Images/Emtm/newfolder.png" />
                    <asp:TreeNodeStyle ImageUrl="~/Modules/HRD/Images/Emtm/document.png"/>
                </LevelStyles>
            </asp:TreeView>
        </td>
    </tr>
</table>
