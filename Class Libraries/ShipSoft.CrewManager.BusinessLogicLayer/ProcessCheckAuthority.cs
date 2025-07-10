using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects; 
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessCheckAuthority:IBusinessLogic
    {
        Authority Obj;
        public ProcessCheckAuthority(int UserId,int ApplicationModuleId)
        {
            Obj = new Authority();
            Obj.UserId = UserId;
            Obj.ApplicationModuleId = ApplicationModuleId;
        }
        public void Invoke()
        {
            AuthenticationManager am = new AuthenticationManager(Obj.ApplicationModuleId, Obj.UserId, ObjectType.Module);     
            Obj.isAdd = am.IsAdd;
            Obj.isEdit = am.IsUpdate;
            Obj.isDelete = am.IsDelete;
            Obj.isPrint = am.IsPrint;
            Obj.isVerify = am.IsVerify;
            Obj.isVerify2 = am.IsVerify2;
        }
        public void InvokeByPage()
        {
            AuthenticationManager am = new AuthenticationManager(Obj.ApplicationModuleId, Obj.UserId, ObjectType.Page);
            Obj.isAdd = am.IsAdd;
            Obj.isEdit = am.IsUpdate;
            Obj.isDelete = am.IsDelete;
            Obj.isPrint = am.IsPrint;
            Obj.isVerify = am.IsVerify;
            Obj.isVerify2 = am.IsVerify2;
        }
        public Authority Authority
        {
            get { return Obj; }
        }
    }
}
