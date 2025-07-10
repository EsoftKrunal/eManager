using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;

/// <summary>
/// Summary description for PurchaseRequest
/// </summary>
public class PurchaseRequest
{
	public PurchaseRequest()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //Function for Vessels binding 
    public DataSet SelectAllVessel()
    {
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Qry", "select * from POS_FetchingVessels order by VesselName"));
        DataSet result = new DataSet();
        result = Common.Execute_Procedures_Select();
        return result;
    }
    //Function for PR binding 
    public DataSet SearchPurchaseRequests(string PRNumber, string vesselId, string Fromdt, string Todate, string statusId, string OrderBy, string AscORDesc)
    {
        Common.Set_Procedures("SearchPurchaseRequests");
        Common.Set_ParameterLength(7);
        Common.Set_Parameters(new MyParameter("@PRNumber", PRNumber),
            new MyParameter("@VesselId", vesselId),
            new MyParameter("@PRFromDate", Fromdt),
            new MyParameter("@PRToDate", Todate),
            new MyParameter("@StatusId", statusId),
            new MyParameter("@OrderBy", OrderBy),
            new MyParameter("@AscORDesc", AscORDesc));
        DataSet result = new DataSet();
        result = Common.Execute_Procedures_Select();
        return result;
    }
    //Function for Update status 'deletion'
    public void UpdateStatusOnDeletionOfPR(int PRId)
    {
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@qry", "UPDATE POS_PurchaseRequest SET StatusId=4 WHERE PRId=" + PRId.ToString()));
        bool res1;
        res1 = Common.Execute_Procedures_IUD(new DataSet());
    }
    // function for insert Purchase request
    public int InsertPR(int PRId, string VesselId, int makerid, int UserId, string PRCodePrefix, int DeptId, string Remarks)
    {
        DataSet ds = new DataSet();
        Common.Set_Procedures("InsertUpdatePurchaseRequest");
        ds.Tables.Clear();
        Common.Set_ParameterLength(8);
        Common.Set_Parameters(new MyParameter("@PRID", PRId),
        new MyParameter("@VESSELID", VesselId),
        new MyParameter("@MakerId", makerid),
        new MyParameter("@CREATEDBY", UserId),
        new MyParameter("@MODIFIEDBY", UserId),
        new MyParameter("@PRCodePrefix", PRCodePrefix),
        new MyParameter("@DeptId", DeptId),
        new MyParameter("@Remarks", Remarks));
        bool res1;
        res1 = Common.Execute_Procedures_IUD(ds);
        try
        {
            int intPRID = Convert.ToInt32(ds.Tables["InsertUpdatePurchaseRequest_Result"].Rows[0][0].ToString());
            return intPRID;
        }
        catch { return 0; }
    }
    // function for insert Purchase request Selected Items
    public void InsertPRSelectedItems(string ItemId, int PRId, string Qty)
    {
        try
        {
            DataSet ds = new DataSet();
            Common.Set_Procedures("InsertPurchaseRequestItemsRelationship");
            ds.Tables.Clear();
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(new MyParameter("@ItemId", ItemId),
            new MyParameter("@PRId", PRId),
            new MyParameter("@ItemQuantity", Qty));
            bool res1;
            res1 = Common.Execute_Procedures_IUD(ds);
        }
        catch { }
    }
    // Get selected Purchase Request
    public DataSet GetSelectedPR(string PRId)
    {
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Qry", "SELECT PR.*,AC.ParentAccountId as ParentAccountId " +
                    " from POS_PurchaseRequest PR " +
                    " Left join POS_AccountHead AC on PR.AccountHeadId=AC.AccountHeadId " +
                    " WHERE PR.PRId=" + PRId + " "));
        DataSet result = new DataSet();
        result = Common.Execute_Procedures_Select();
        return result;
    }
    // Get selected Items of Purchase Request
    public DataSet GetSelectedItemsOfPR(string PRId)
    {
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Qry", "SELECT PRR.*,IM.ItemName,IM.ItemCode,ITM.ItemTypeName,UM.UnitName as UOM " +
                    " from POS_PurchaseRequestItemsRelationship PRR " +
                    " Left join POS_PurchaseRequest PR ON PR.PRId=PRR.PRId " +
                    " Left join POS_ItemMaster IM ON PRR.ItemId=IM.ItemId " +
                    " Left join POS_ItemTypeMaster ITM ON ITM.ItemTypeId=IM.ItemTypeId " +
                    " Left join POS_UnitOfMeasurementMaster UM ON IM.UnitId=UM.UnitId " +
                    " WHERE PRR.PRId=" + PRId + " "));
        DataSet result = new DataSet();
        result = Common.Execute_Procedures_Select();
        return result;
    }
    // function for Add Nodes in XML file
    public void SaveXml(string PRDt,string VslId,string mkrid, string Itmtypid, string AccHdId,string DeptmtId, string fileName,string Remark)
    {
        try
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlElement requistion = xmldoc.CreateElement("Requisition");
            XmlElement PRDate = xmldoc.CreateElement("PRDate");
            XmlElement VesselId = xmldoc.CreateElement("VesselId");
            XmlElement MakerId = xmldoc.CreateElement("MakerId");
            XmlElement ItemTypeId = xmldoc.CreateElement("ItemTypeId");
            XmlElement AccHeadId = xmldoc.CreateElement("AccHeadId");
            XmlElement DeptId = xmldoc.CreateElement("DeptId");
            XmlElement Remarks = xmldoc.CreateElement("Remarks");
            XmlElement Items = xmldoc.CreateElement("Items");
            requistion.AppendChild(PRDate);
            requistion.AppendChild(VesselId);
            requistion.AppendChild(MakerId);
            requistion.AppendChild(ItemTypeId);
            requistion.AppendChild(AccHeadId);
            requistion.AppendChild(DeptId);
            requistion.AppendChild(Remarks);
            requistion.AppendChild(Items);
            PRDate.InnerText = PRDt;
            VesselId.InnerText = VslId;
            MakerId.InnerText = mkrid;
            ItemTypeId.InnerText = Itmtypid;
            AccHeadId.InnerText = AccHdId;
            DeptId.InnerText = DeptmtId;
            Remarks.InnerText = Remark;
            xmldoc.AppendChild(requistion);
            xmldoc.Save(HttpContext.Current.Server.MapPath(fileName));
        }
        catch (Exception ex)
        {

        }
    }
    // function for Add Selected Items List in XML file
    public void AddItemsInXML(string ItmId,string quantity,string fileName)
    {
        try
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(HttpContext.Current.Server.MapPath("Requisition.xml"));
            XmlNodeList xml = xmldoc.GetElementsByTagName("Items");
            foreach (XmlNode nd in xml)
            {
                if (nd.Name == "Items")
                {
                    XmlElement Item = xmldoc.CreateElement("Item");
                    XmlElement ItemId = xmldoc.CreateElement("ItemId");
                    XmlElement QTY = xmldoc.CreateElement("QTY");
                    Item.AppendChild(ItemId);
                    Item.AppendChild(QTY);
                    ItemId.InnerText = ItmId;
                    QTY.InnerText = quantity;
                    nd.AppendChild(Item);
                    xmldoc.Save(HttpContext.Current.Server.MapPath(fileName));
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    
    // Get Vessel Id by Vessel Name
    public int GetVesselIdByVesselName(string VesselName)
    {
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Qry", "select VesselId from POS_FetchingVessels where VesselName='" + VesselName + "'"));
        DataSet result = new DataSet();
        result = Common.Execute_Procedures_Select();
        if (result.Tables[0].Rows.Count > 0)
        {
            return Common.CastAsInt32(result.Tables[0].Rows[0][0].ToString());
        }
        else
        {
            return 0;
        }
    }
    // Get Account Head Id by Account No.
    public int GetAccountHeadIdByAccountHeadNo(string AccountCode)
    {
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Qry", "select AccountHeadId from POS_AccountHead where AccountCode='" + AccountCode + "'"));
        DataSet result = new DataSet();
        result = Common.Execute_Procedures_Select();
        if (result.Tables[0].Rows.Count > 0)
        {
            return Common.CastAsInt32(result.Tables[0].Rows[0][0].ToString());
        }
        else
        {
            return 0;
        }
    }

    //Get Unit Id by Unit Name.
    public int GetUnitIdByUnitName(string UOM)
    {
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Qry", "select UnitId from POS_UnitOfMeasurementMaster where UnitName='" + UOM + "'"));
        DataSet result = new DataSet();
        result = Common.Execute_Procedures_Select();
        if (result.Tables[0].Rows.Count > 0)
        {
            return Common.CastAsInt32(result.Tables[0].Rows[0][0].ToString());
        }
        else
        {
            return 0;
        }
    }

    // get partno by itemid
    public string GetPartNoByItemId(int itemid, string partno)
    {
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Qry", "if exists(select PartNo from POS_ItemMasterForSpares where ItemId=" + itemid + " and PartNo='"+ partno +"') select 1 else select 0"));
        DataSet result = new DataSet();
        result = Common.Execute_Procedures_Select();
        if (result.Tables[0].Rows.Count > 0)
        {
            return result.Tables[0].Rows[0][0].ToString();
        }
        else
        {
            return "";
        }
    }

    // get drawingno by itemid
    public string GetDrawNoByItemId(int itemid, string drawingno)
    {
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Qry", "if exists(select DrawingNo from POS_ItemMasterForSpares where ItemId=" + itemid + " and DrawingNo='" + drawingno + "') select 1 else select 0"));
        DataSet result = new DataSet();
        result = Common.Execute_Procedures_Select();
        if (result.Tables[0].Rows.Count > 0)
        {
            return result.Tables[0].Rows[0][0].ToString();
        }
        else
        {
            return "";
        }
    }

    //Get Unit Id by Unit Name.
    public int GetItemIdByItemName(string ItemName)
    {
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Qry", "select ItemId from POS_ItemMasterForSpares where ItemName='" + ItemName + "'"));
        DataSet result = new DataSet();
        result = Common.Execute_Procedures_Select();
        if (result.Tables[0].Rows.Count > 0)
        {
            return Common.CastAsInt32(result.Tables[0].Rows[0][0].ToString());
        }
        else
        {
            return 0;
        }
    }

    //Get Stores Item Id by Item Name.
    public int GetStoresItemIdByItemName(string ItemName)
    {
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Qry", "select ItemId from POS_ItemMasterForStores where ItemName='" + ItemName + "'"));
        DataSet result = new DataSet();
        result = Common.Execute_Procedures_Select();
        if (result.Tables[0].Rows.Count > 0)
        {
            return Common.CastAsInt32(result.Tables[0].Rows[0][0].ToString());
        }
        else
        {
            return 0;
        }
    }

    // Get Department Id by Department Name.
    public int GetDeptIdByDeptName(string DeptName)
    {
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Qry", "select DepartmentId from POS_DepartmentMaster where isnull(IsActive,1)=1 and DepartmentName='" + DeptName + "'"));
        DataSet result = new DataSet();
        result = Common.Execute_Procedures_Select();
        if (result.Tables[0].Rows.Count > 0)
        {
            return Common.CastAsInt32(result.Tables[0].Rows[0][0].ToString());
        }
        else
        {
            return 0;
        }
    }
    // Get PR Type Id by PR Type.
    public int GetPRTypeIdByPRType(string PRType)
    {
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Qry", "select RequestTypeId from POS_RequestTypeMaster where RequestTypeName='" + PRType + "'"));
        DataSet result = new DataSet();
        result = Common.Execute_Procedures_Select();
        if (result.Tables[0].Rows.Count > 0)
        {
            return Common.CastAsInt32(result.Tables[0].Rows[0][0].ToString());
        }
        else
        {
            return 0;
        }
    }
    // Get Priority Id by Priority.
    public int GetPriorityIdByPriority(string Priority)
    {
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Qry", "select PriorityId from POS_PriorityMaster where PriorityName='" + Priority + "'"));
        DataSet result = new DataSet();
        result = Common.Execute_Procedures_Select();
        if (result.Tables[0].Rows.Count > 0)
        {
            return Common.CastAsInt32(result.Tables[0].Rows[0][0].ToString());
        }
        else
        {
            return 0;
        }
    }
    // Get Maker Id by Maker Name.
    public int GetMakerIdByMakerName(string MakerName)
    {
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Qry", "select MakerId from POS_MakerMaster where MakerName='" + MakerName + "'"));
        DataSet result = new DataSet();
        result = Common.Execute_Procedures_Select();
        if (result.Tables[0].Rows.Count > 0)
        {
            return Common.CastAsInt32(result.Tables[0].Rows[0][0].ToString());
        }
        else
        {
            return 0;
        }
    }
    // Get Item Type Id by Item Type Code.
    public int GetItemTypeIdByItemTypeCode(string ItemTypeCode)
    {
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Qry", "select ItemTypeId from POS_ItemTypeMaster where ItemTypeCode='" + ItemTypeCode + "'"));
        DataSet result = new DataSet();
        result = Common.Execute_Procedures_Select();
        if (result.Tables[0].Rows.Count > 0)
        {
            return Common.CastAsInt32(result.Tables[0].Rows[0][0].ToString());
        }
        else
        {
            return 0;
        }
    }
    // Get UserLogin Id by User Login Name.
    public int GetUserLoginIdByUserLoginName(string UserLoginName)
    {
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Qry", "select LoginId from POS_FetchingUsers where UserId='" + UserLoginName + "'"));
        DataSet result = new DataSet();
        result = Common.Execute_Procedures_Select();
        if (result.Tables[0].Rows.Count > 0)
        {
            return Common.CastAsInt32(result.Tables[0].Rows[0][0].ToString());
        }
        else
        {
            return 0;
        }
    }
    // Get Item Id by Item Code.
    public int GetItemIdByItemCode(string ItemCode)
    {
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Qry", "select ItemId from POS_ItemMaster where ItemCode='" + ItemCode + "'"));
        DataSet result = new DataSet();
        result = Common.Execute_Procedures_Select();
        if (result.Tables[0].Rows.Count > 0)
        {
            return Common.CastAsInt32(result.Tables[0].Rows[0][0].ToString());
        }
        else
        {
            return 0;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="PRID"></param>
    /// <param name="PRNUMBER"></param>
    /// <param name="PRTYPE"></param>
    /// <param name="VESSELID"></param>
    /// <param name="ACCOUNTHEADID"></param>
    /// <returns></returns>
    /// 
    public static DataSet SelectPurchaseRequests(string PRID, string PRNUMBER, char? PRTYPE, int? VESSELID, int? ACCOUNTHEADID)
    {
        Common.Set_Procedures("SelectPurchaseRequisition");
        Common.Set_ParameterLength(5);
        Common.Set_Parameters(new MyParameter("@PRID", PRID == "" ? DBNull.Value : getObject(PRID)),
            new MyParameter("@PRNUMBER", PRNUMBER != "" ? getObject(PRNUMBER) : DBNull.Value ),
            new MyParameter("@PRTYPE",  PRTYPE ),
            new MyParameter("@VESSELID", VESSELID),
            new MyParameter("@ACCOUNTHEADID",ACCOUNTHEADID));
        DataSet result = new DataSet();
        result = Common.Execute_Procedures_Select();
        return result;
    }
    private static object getObject(object ob)
    {
        Object obs= new object();
        obs=ob;
        return obs;
    }

    public static DataSet SelectPurchaseRequests(int? VESSELID)
    {
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Qry", "select 0 as PRID , '< Select >' as PRNumber from POS_PurchaseRequest " +
               "union select PRId,PRNumber from POS_PurchaseRequest where statusid=2 and VesselId=" + VESSELID + " and PRId not in(select PRId from dbo.POS_RFQMaster)"));
        DataSet result = new DataSet();
        result = Common.Execute_Procedures_Select();
        return result;

        
    }

    public static DataSet SelectPRDetail(int PRID)
    {



        string sql = " SELECT     PI.ItemId, convert(int, PI.ItemQuantity)as ItemQuantity, IM.ItemName, ITM.ItemTypeName, ITM.ParentId " +
                       " ,(select ItemTypeName from POS_ItemTypeMaster where Itemtypeid=ITM.ParentId)as Category " +

                        " FROM dbo.POS_PurchaseRequest AS PR INNER JOIN " +
                      " dbo.POS_PurchaseRequestItemsRelationship AS PI ON PR.PRId = PI.PRId INNER JOIN "+
                      "dbo.POS_ItemMaster AS IM ON PI.ItemId = IM.ItemId INNER JOIN "+
                      "dbo.POS_ItemTypeMaster AS ITM ON IM.ItemTypeId = ITM.ItemTypeId  WHERE PR.PRId=" + PRID;

        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Qry", sql));
        DataSet result = new DataSet();
        result = Common.Execute_Procedures_Select();
        return result;

    }



}
