using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;  
/// <summary>
/// Summary description for ApplicationManager
/// </summary>
public enum EObjectType
{
    Application,Module,Page
}
public class EAuthenticationManager
{
    bool _IsView=false ;
    bool _IsAdd = false;
    bool _IsUpdate = false;
    bool _IsDelete = false;
    bool _IsVerify = false;
    bool _IsVerify2 = false;
    bool _IsPrint = false;
    bool _IsSuperUser = false;
    public EAuthenticationManager(int _ObjectId, int _UserId, ObjectType _ObjectType)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT isnull(SUPERUSER,'N') FROM USERMASTER WHERE LOGINID=" + _UserId.ToString());
        if (dt != null)
            if (dt.Rows.Count > 0)
            {
                _IsSuperUser = (dt.Rows[0][0].ToString() == "Y");
                if (_IsSuperUser)
                {
                    _IsView = true;
                    _IsAdd = true;
                    _IsUpdate = true;
                    _IsDelete = true;
                    _IsVerify= true;
                    _IsVerify2 = true; 
                    _IsPrint = true; 
                    return; 
                }
            }
        //--------------------------
        ECommon.Set_ParameterLength(2); 
        if (_ObjectType==ObjectType.Application)
        {
            ECommon.Set_Procedures("getApplicationRights");
            ECommon.Set_Parameters(new EMyParameter("AppId",_ObjectId),new EMyParameter("UserId",_UserId));   
        }
        else if (_ObjectType == ObjectType.Module)
            {
            ECommon.Set_Procedures("getModuleRights");
            ECommon.Set_Parameters(new EMyParameter("ModId",_ObjectId),new EMyParameter("UserId",_UserId)); 
        }
        else
        {
            ECommon.Set_Procedures("getPageRights");
            ECommon.Set_Parameters(new EMyParameter("PageId",_ObjectId),new EMyParameter("UserId",_UserId)); 
            
        }
        dt = ECommon.Execute_Procedures_Select().Tables[0]; 
        if (dt !=null )
        {
            _IsView =Convert.ToBoolean(dt.Rows[0]["IsView"]);
            _IsAdd = Convert.ToBoolean(dt.Rows[0]["IsAdd"]);
            _IsUpdate = Convert.ToBoolean(dt.Rows[0]["IsUpdate"]);
            _IsDelete = Convert.ToBoolean(dt.Rows[0]["IsDelete"]);
            _IsVerify= Convert.ToBoolean(dt.Rows[0]["IsVerify"]);
            _IsVerify2 = Convert.ToBoolean(dt.Rows[0]["IsVerify2"]); 
            _IsPrint = Convert.ToBoolean(dt.Rows[0]["IsPrint"]); 
        }
        
    }
    public bool IsView
    {
        get{return _IsView;}
    }
    public bool IsAdd
    {
        get { return _IsAdd; }
    }
    public bool IsUpdate
    {
        get { return _IsUpdate; }
    }
    public bool IsDelete
    {
        get { return _IsDelete; }
    }
    public bool IsVerify
    {
        get { return _IsVerify; }
    }
    public bool IsVerify2
    {
        get { return _IsVerify2; }
    }
    public bool IsPrint
    {
        get { return _IsPrint; }
    }
    public bool IsSuperUser
    {
        get { return _IsSuperUser; }
    }
}
