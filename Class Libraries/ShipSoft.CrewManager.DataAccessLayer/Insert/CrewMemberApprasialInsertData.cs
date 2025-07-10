using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.DataAccessLayer.Insert
{
    public class CrewMemberApprasialInsertData:DataAccessBase 
    {
  private CrewApprasialDetails  _crewAprasialDetails;
        private CrewMemberApprasialInsertDataParameters _crewapprasialinsertdataparamenters;
        public CrewMemberApprasialInsertData()
       {
           StoredProcedureName = StoredProcedure.Name.InsertUpdateCrewApprasialDetails.ToString();
       }
       public void Add()
       {
           _crewapprasialinsertdataparamenters = new CrewMemberApprasialInsertDataParameters(ApprasialDetails);
           DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
           object id = dbhelper.RunScalar(base.ConnectionString, _crewapprasialinsertdataparamenters.Parameters);
           this._crewAprasialDetails.CrewAppraisalId = Convert.ToInt32(id.ToString());
       }
        public CrewApprasialDetails ApprasialDetails
       {
           get { return _crewAprasialDetails; }
           set { _crewAprasialDetails = value; }
       }
       public class CrewMemberApprasialInsertDataParameters
       {
           private CrewApprasialDetails _crewAprasialDetails;
           private SqlParameter[] _parameters;
           public CrewMemberApprasialInsertDataParameters(CrewApprasialDetails app)
           {
               this.ApprasialDetails = app;
               if (ApprasialDetails.ApprasialFrom  == "")
               {
                   ApprasialDetails.ApprasialFrom  = null;
               }
               if (ApprasialDetails.ApprasialTo == "")
               {
                   ApprasialDetails.ApprasialTo = null;
               }

               Build();
           }
           private void Build()
           {
               SqlParameter[] parameters =
            {
                new SqlParameter("@CrewAppraisalId",ApprasialDetails.CrewAppraisalId),
                new SqlParameter("@CrewId",ApprasialDetails.CrewId),
                new SqlParameter("@AppraisalOccasionId",ApprasialDetails.AppraisalOccasionId),
                new SqlParameter("@FromDate",ApprasialDetails.ApprasialFrom),
                new SqlParameter("@ToDate",ApprasialDetails.ApprasialTo),
                new SqlParameter("@AvgMarks",ApprasialDetails.AverageMarks),
                new SqlParameter("@AppraiserRemarks",ApprasialDetails.AppraiserRemarks),
                new SqlParameter("@AppraiseeRemarks",ApprasialDetails.AppraiseeRemarks),
                new SqlParameter("@OfficeRemarks",ApprasialDetails.OfficeRemarks),
                new SqlParameter("@VesselId",ApprasialDetails.VesselId),
                new SqlParameter("@Recommended",ApprasialDetails.Recommended),
                new SqlParameter("@CreatedBy",ApprasialDetails.CreatedBy),
                new SqlParameter("@ModifiedBy",ApprasialDetails.ModifiedBy),
                new SqlParameter("@ImagePath",ApprasialDetails.ImagePath),

                new SqlParameter("@N_PerfScrore",ApprasialDetails.N_PerfScore),
                new SqlParameter("@N_CompScore",ApprasialDetails.N_CompScore),
                new SqlParameter("@N_ReportNo",ApprasialDetails.N_ReportNo),
                new SqlParameter("@N_RecommendedNew",ApprasialDetails.N_Recommended),
                new SqlParameter("@N_TrainingRequired",ApprasialDetails.N_TrainingRequired),
            };
               Parameters = parameters;
           }
           public CrewApprasialDetails ApprasialDetails
           {
               get { return _crewAprasialDetails; }
               set { _crewAprasialDetails = value; }
           }

           public SqlParameter[] Parameters
           {
               get { return _parameters; }
               set { _parameters = value; }
           }
       }
    }
}
