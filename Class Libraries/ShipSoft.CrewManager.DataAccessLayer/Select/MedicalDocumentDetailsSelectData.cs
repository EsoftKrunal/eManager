using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
   public class MedicalDocumentDetailsSelectData:DataAccessBase
    {
       private MedicalDetails _medicaldetails;
       public MedicalDocumentDetailsSelectData()
       {
           StoredProcedureName = StoredProcedure.Name.selectmedicaldocumentdetailsingrid.ToString();
       }
       public DataSet Get()
       {
           DataSet ds;
           MedicalDetailsSelectDataParameters medicaldetailsselectdataparameters = new MedicalDetailsSelectDataParameters(MedicalDetails);
           DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
           ds = dbhelper.Run(base.ConnectionString, medicaldetailsselectdataparameters.Parameters);
           return ds;
       }
       public MedicalDetails MedicalDetails
       {
           get { return _medicaldetails; }
           set { _medicaldetails = value; }
       }
       public class MedicalDetailsSelectDataParameters
       {
           private MedicalDetails _medicaldetails;
           private SqlParameter[] _parameters;
           public MedicalDetailsSelectDataParameters(MedicalDetails medicaldetails)
           {
               MedicalDetails = medicaldetails;
               Build();
           }
           private void Build()
           {
               SqlParameter[] parameters =
            {
                new SqlParameter( "@MedicalDetailsId",MedicalDetails.MedicalDetailsId ),
                new SqlParameter("@CrewId",MedicalDetails.CrewId),
                new SqlParameter("@DocumentTypeId",MedicalDetails.DocumentTypeId)
            };

               Parameters = parameters;
           }
           public MedicalDetails MedicalDetails
           {
               get { return _medicaldetails; }
               set { _medicaldetails = value; }
           }
           public SqlParameter[] Parameters
           {
               get { return _parameters; }
               set { _parameters = value; }
           }
       }
    }
}
