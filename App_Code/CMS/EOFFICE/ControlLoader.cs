using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for ControlLoader
/// </summary>
/// 
public enum DataName
{
    EmployeeStatus, 
    Education,
    Gender,
    Office,
    Nationality,
    BloodGroup,
    CivilStatus,
    Qualification,
    country,
    ShirtSize,
    Position,
    HR_Department,
    Visa,
    Ramittance,
    VesselType,
    Rank,
    SignOffReason,
    HR_Medical,
    HR_LeaveTypeMaster,
    HR_Relation,
    ValidUsers,
    Hr_PersonalDetails,
    ForwardedUsers,
    LeaveStatus,
    HR_PeapCategory,
    HR_LeavePurpose
}
public class ControlLoader
{
    public ControlLoader()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static void LoadControl(DropDownList ddl, DataName dn, string AdditionalItem, string AdditionalValue)
    {
        switch (dn)
        {
            case DataName.EmployeeStatus:
                ddl.DataSource = getDataFromXML(dn); 
                ddl.DataTextField = "EmployeeStatusName";
                ddl.DataValueField = "EmployeeStatusId";
                break;
            case DataName.Gender:
                ddl.DataSource = getDataFromXML(dn); 
                ddl.DataTextField = "GenderName";
                ddl.DataValueField = "GenderId";
                break;
            case DataName.Office:
                ddl.DataSource = getDataFromDB(dn, "OfficeName", "");
                ddl.DataTextField = "OfficeName";
                ddl.DataValueField = "OfficeId";
                break;
            case DataName.BloodGroup :
                ddl.DataSource = getDataFromXML(dn); 
                ddl.DataTextField = "BloodGroupName";
                ddl.DataValueField = "BloodGroupID";
                break;
            case DataName.CivilStatus : 
                ddl.DataSource = getDataFromXML(dn);
                ddl.DataTextField = "CivilStatusName";
                ddl.DataValueField = "CivilStatusId";
                break;
            case DataName.Qualification : 
                ddl.DataSource = getDataFromXML(dn);
                ddl.DataTextField = "QualificationName";
                ddl.DataValueField = "QualificationID";
                break;
            case DataName.country:
                ddl.DataSource = getDataFromDB(dn, "CountryName", "");
                ddl.DataTextField = "CountryName";
                ddl.DataValueField = "CountryId";
                break;
            case DataName.ShirtSize :
                ddl.DataSource = getDataFromXML(dn); 
                ddl.DataTextField = "ShirtSize";
                ddl.DataValueField = "ShirtId";
                break;
            case DataName.Position :
                ddl.DataSource = getDataFromDB(dn, "PositionName","");
                ddl.DataTextField = "PositionName";
                ddl.DataValueField = "PositionId";
                break;
            case DataName.HR_Department:
                ddl.DataSource = getDataFromDB(dn, "DeptName", "");
                ddl.DataTextField = "DeptName";
                ddl.DataValueField = "DeptId";
                break;
            case DataName.VesselType:
                ddl.DataSource = getDataFromDB(dn, "VesselTypeName", "");
                ddl.DataTextField = "VesselTypeName";
                ddl.DataValueField = "VesselTypeId";
                break;
            case DataName.Rank:
                ddl.DataSource = getDataFromDB(dn, "RankName", "");
                ddl.DataTextField = "RankName";
                ddl.DataValueField = "RankId";
                break;
            case DataName.SignOffReason:
                ddl.DataSource = getDataFromDB(dn, "SignOffReason", "");
                ddl.DataTextField = "SignOffReason";
                ddl.DataValueField = "SignOffReasonId";
                break;
            case DataName.Visa:
                ddl.DataSource = getDataFromXML(dn);
                ddl.DataTextField = "VisaName";
                ddl.DataValueField = "VisaId";
                break;
            case DataName.Ramittance:
                ddl.DataSource = getDataFromXML(dn);
                ddl.DataTextField = "ramittanceName";
                ddl.DataValueField = "ramittanceId";
                break;
            case DataName.HR_Medical:
                ddl.DataSource = getDataFromDB(dn, "MedicalDocumentName", "");
                ddl.DataTextField = "MedicalDocumentName";
                ddl.DataValueField = "MedicalDocumentId";
                break;
            case DataName.HR_LeaveTypeMaster:
                ddl.DataSource = getDataFromDB(dn, "LeaveTypeName", "");
                ddl.DataTextField = "LeaveTypeName";
                ddl.DataValueField = "LeaveTypeId";
                break;
            case DataName.HR_Relation:
                ddl.DataSource = getDataFromDB(dn, "RelationName", "");
                ddl.DataTextField = "RelationName";
                ddl.DataValueField = "RelationId";
                break;
            case DataName.ValidUsers:
                ddl.DataSource = getDataFromDB(dn, "", "");
                ddl.DataTextField = "UserId";
                ddl.DataValueField = "LoginId";
                break;
            case DataName.Hr_PersonalDetails:
                ddl.DataSource = getDataFromDB(dn, "FirstName", "");
                ddl.DataTextField = "FirstName";
               ddl.DataValueField = "EmpId";
                break;
            case DataName.ForwardedUsers:
                ddl.DataSource = getDataFromDB(dn, "Name", "");
                ddl.DataTextField = "Name";
                ddl.DataValueField = "EmpId";
                break;
            case DataName.LeaveStatus :
                ddl.DataSource = getDataFromDB(dn, "StatusCode", "");
                ddl.DataTextField = "Status";
                ddl.DataValueField = "StatusCode";
                break;
            case DataName.HR_PeapCategory:
                ddl.DataSource = getDataFromDB(dn, "Category", "");
                ddl.DataTextField = "Category";
                ddl.DataValueField = "PID";
                break;
            case DataName.HR_LeavePurpose:
                ddl.DataSource = getDataFromDB(dn, "Purpose", "");
                ddl.DataTextField = "Purpose";
                ddl.DataValueField = "PurposeId";
                break;
           default:
                break;
        }
        ddl.DataBind();
        if (AdditionalItem.Trim()!="NONE")
        {
            ddl.Items.Insert(0,new ListItem(" < " + AdditionalItem + " > ", AdditionalValue));     
        }
    }
    public static void LoadControl(DropDownList ddl, DataName dn, string AdditionalItem, string AdditionalValue,string Filter)
    {

        switch (dn)
        {
            case DataName.EmployeeStatus:
                ddl.DataSource = getDataFromXML(dn);
                ddl.DataTextField = "EmployeeStatusName";
                ddl.DataValueField = "EmployeeStatusId";
                break;
            case DataName.Gender:
                ddl.DataSource = getDataFromXML(dn);
                ddl.DataTextField = "GenderName";
                ddl.DataValueField = "GenderId";
                break;
            case DataName.Office:
                ddl.DataSource = getDataFromDB(dn, "OfficeName", Filter);
                ddl.DataTextField = "OfficeName";
                ddl.DataValueField = "OfficeId";
                break;
            case DataName.BloodGroup:
                ddl.DataSource = getDataFromXML(dn);
                ddl.DataTextField = "BloodGroupName";
                ddl.DataValueField = "BloodGroupID";
                break;
            case DataName.CivilStatus:
                ddl.DataSource = getDataFromXML(dn);
                ddl.DataTextField = "CivilStatusName";
                ddl.DataValueField = "CivilStatusId";
                break;
            case DataName.Qualification:
                ddl.DataSource = getDataFromXML(dn);
                ddl.DataTextField = "QualificationName";
                ddl.DataValueField = "QualificationID";
                break;
            case DataName.country:
                ddl.DataSource = getDataFromDB(dn, "CountryName", Filter);
                ddl.DataTextField = "CountryName";
                ddl.DataValueField = "CountryId";
                break;
            case DataName.ShirtSize:
                ddl.DataSource = getDataFromXML(dn);
                ddl.DataTextField = "ShirtSize";
                ddl.DataValueField = "ShirtId";
                break;
            case DataName.Position:
                ddl.DataSource = getDataFromDB(dn, "PositionName", Filter);
                ddl.DataTextField = "PositionName";
                ddl.DataValueField = "PositionId";
                break;
            case DataName.HR_Department:
                ddl.DataSource = getDataFromDB(dn, "DeptName", Filter);
                ddl.DataTextField = "DeptName";
                ddl.DataValueField = "DeptId";
                break;
            case DataName.VesselType:
                ddl.DataSource = getDataFromDB(dn, "VesselTypeName", "");
                ddl.DataTextField = "VesselTypeName";
                ddl.DataValueField = "VesselTypeId";
                break;
            case DataName.Rank:
                ddl.DataSource = getDataFromDB(dn, "RankName", "");
                ddl.DataTextField = "RankName";
                ddl.DataValueField = "RankId";
                break;
            case DataName.SignOffReason:
                ddl.DataSource = getDataFromDB(dn, "SignOffReason", "");
                ddl.DataTextField = "SignOffReason";
                ddl.DataValueField = "SignOffReasonId";
                break;
            case DataName.Visa:
                ddl.DataSource = getDataFromXML(dn);
                ddl.DataTextField = "VisaName";
                ddl.DataValueField = "VisaId";
                break;
            case DataName.Ramittance:
                ddl.DataSource = getDataFromXML(dn);
                ddl.DataTextField = "ramittanceName";
                ddl.DataValueField = "ramittanceId";
                break;
            case DataName.HR_LeaveTypeMaster:
                ddl.DataSource = getDataFromDB(dn, "LeaveTypeName", Filter);
                ddl.DataTextField = "LeaveTypeName";
                ddl.DataValueField = "LeaveTypeId";
                break;
            case DataName.HR_Relation:
                ddl.DataSource = getDataFromDB(dn, "RelationName", "");
                ddl.DataTextField = "RelationName";
                ddl.DataValueField = "RelationId";
                break;
            case DataName.ValidUsers:
                ddl.DataSource = getDataFromDB(dn, "", Filter);
                ddl.DataTextField = "UserId";
                ddl.DataValueField = "LoginId";
                break;
            case DataName.Hr_PersonalDetails:
                ddl.DataSource = getDataFromDB(dn, "", Filter);
                ddl.DataTextField = "FirstName";
                ddl.DataValueField = "EmpId";
                break;
            case DataName.ForwardedUsers:
                ddl.DataSource = getDataFromDB(dn, "", Filter);
                ddl.DataTextField = "Name";
                ddl.DataValueField = "EmpId";
                break;
            case DataName.LeaveStatus:
                ddl.DataSource = getDataFromDB(dn, "", Filter);
                ddl.DataTextField = "Status";
                ddl.DataValueField = "StatusCode";
                break;
            case DataName.HR_PeapCategory:
                ddl.DataSource = getDataFromDB(dn, "", "");
                ddl.DataTextField = "Category";
                ddl.DataValueField = "PID";
                break;
            case DataName.HR_LeavePurpose:
                ddl.DataSource = getDataFromDB(dn, "", "");
                ddl.DataTextField = "Purpose";
                ddl.DataValueField = "PurposeId";
                break;
            default:
                break;
        }
        ddl.DataBind();
        if (AdditionalItem.Trim() != "NONE")
        {
            ddl.Items.Insert(0, new ListItem(" < " + AdditionalItem + " > ", AdditionalValue));
        }
    }
    public static string getRootPath()
    {
        return HttpContext.Current.Server.MapPath("~\\Modules\\eOffice");
    }
    public static DataTable getDataFromDB(DataName dn,string SortBy, string Filter)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string Sql="SELECT * FROM " + dn.ToString() + ((Filter.Trim()=="")?"":" WHERE ") + Filter +  ((SortBy.Trim()=="")?"":" ORDER BY ") + SortBy ;
        //Common.Execute_Procedures_Select_ByQuery(Sql); 
        dt=Common.Execute_Procedures_Select_ByQueryCMS(Sql);
        return dt;   
    }
    public static DataTable getDataFromXML(DataName dn)
    {
        DataSet ds = new DataSet();
        ds.ReadXml(getRootPath() + "\\ControlLoaderXML\\" + dn.ToString() + ".xml");
        return ds.Tables[0];
    }
}

