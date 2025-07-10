using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
    public class CrewMemberQualificationDetailsSelectData : DataAccessBase
    {
        private QualificationDetails _qualificationdetails;
        public CrewMemberQualificationDetailsSelectData()
        {
            StoredProcedureName = StoredProcedure.Name.selectqualificationdetails.ToString();
        }
        public DataSet Get()
        {
            DataSet ds;
            CrewMemberQualificationdetailsSelectDataParameters dataparameters = new CrewMemberQualificationdetailsSelectDataParameters(QualificationDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(base.ConnectionString, dataparameters.Parameters);

            return ds;
        }
        public QualificationDetails QualificationDetails
        {
            get { return _qualificationdetails; }
            set { _qualificationdetails = value; }
        }
    }
    public class CrewMemberQualificationdetailsSelectDataParameters
    {
        private QualificationDetails _qualificationdetails;
        private SqlParameter[] _parameters;
        public CrewMemberQualificationdetailsSelectDataParameters(QualificationDetails qualificationdetails)
        {
            QualificationDetails = qualificationdetails;
            Build();
        }
        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@CrewId" ,QualificationDetails.CrewId )
            };

            Parameters = parameters;
        }

        public QualificationDetails QualificationDetails
        {
            get { return _qualificationdetails; }
            set { _qualificationdetails = value; }
        }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
