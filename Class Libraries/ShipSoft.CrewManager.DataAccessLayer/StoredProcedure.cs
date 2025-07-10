using System;
using System.Collections.Generic;
using System.Text;

namespace ShipSoft.CrewManager.DataAccessLayer
{
    public class StoredProcedure
    {
        public enum Name
        {
            #region Insert Procedures
            //--Crew member
            InsertUpdatePersonalDetails,
            insertCrewMemberQualificationDetails,
            InsertUpdateCrewRecordContactData,
            InsertUpdateExperienceDetails,
            InsertUpdateCrewMemberFamilyDetails,
            insertcrewmemberdocumentdetailsingrid,
            InsertUpdateCrewMemberFamilyVisaDetails,
            InsertUpdateProfessionalDetails,
            InsertUpdateAcademicDetails,
            InsertUpdateCrewMemberMedicalHistoryDetails,
            SelectCountryDetails, 
            InsertUpdateCountryDetails,
            insertupdatecrewmembermedicaldocumentdetails,
            InsertUpdateCrewCourseCertificateDetails,
            InsertUpdateDangerousCargoDetails,
            InsertUpdateCrewLicenseDetails,
            InsertUpdateCrewMemberCRMDetails,
            InsertUpdateCrewTrainingRequirement,
            InsertUpdateCrewOtherDocumentDetails,
            InsertUpdateCrewApprasialDetails,
            #endregion

            #region Select Procedures
            //--Crew Member
            selectCrewMemberContactDetailsById,
            selectCrewExperienceDetailsinGrid,
            selectCrewExperienceDetailsinGrid_MTMSM,
            SelectExperienceDetails,
            selectCrewMemberDetailsById,
            SelectCrewMemberDetails_Search,
            selectqualificationdetails,
            SelectQualificationDetailsdata,
            SelectProfessionalDetailsdataById,
            selectCrewProfessionalDetailsinGrid,
            selectNearestAirport,
            SelectCountryCode,
            selectCrewAcademicDetails,
            SelectPort,
            SelectTrainingIdName,
            SelectCrewMemberCourses,
            SelectCrewCourseCertificateDetails,
            SelectCrewTrainingRequirementdetails,
            SelectCrewOtherDocumentDetails,
            SelectOtherDocumentsByCrewId,
            SelectCrewApprasials,
            SelectCrewApprasials1,
            SelectFlagStateDetails,
            #endregion

            #region General Procedures
            SelectNationality,
            SelectCrewStatus,
            SelectRankAppliedId,
            SelectCountryOfBirth,
            SelectRecruitingOffice,
            SelectVesselPool,
            SelectVessel,
            SelectOwnerPool,
            SelectManningPool,
            SelectVesselType,
            SelectSignOffReason,
            #endregion

            selectdocumenttype,
            selectcoursename,
            selectdocumentsubtype,
            selectdocumentdetailsingrid,
            selectmedicaldocumentdetailsingrid,
            selectCrewMember,
            SelectFamilyDetails,
            SelectFamilyDetailsByFamilyId,
            selecttraveldocumenttype,
            SelectQualification,
            SelectVisaDetails,
            SelectcdcDetails,
            SelectVisaDetailsByFamilyDocumentId,
            SelectCDCDetailsByFamilyDocumentId,
            selectdocumentdetailsdata,
            selecttraveldocumentdetailsingrid,
            selecttraveldocumentdetailsdata,
            insertupdatecrewmembertraveldocumentdetails,
            selecttraveldocumentsubtype,
            SelectCrewMemberMedicalCaseHistory,
            selectmedicaldocumenttype,
            SelectCargoName,
            selectCrewDangerousCargoDetails,
            SelectCrewLicenses,
            SelectCrewLicenseDetails,
            SelectCRMDetails,
            SelectAppraisal,
            SelectBloodGroup,
            SelectCrmCategory,
            SelectCountryName,
            #region Miscellaneous Procedures

            CheckLogin,
            CheckAuthority,
            #endregion

            DeleteCrewMemberExperienceDetailsById,
            DeleteCrewMemberTravelDetailsById,
            DeleteCrewMemberAcademicDetailsById,
            DeleteFamilyDetailsById,
            DeleteVisaDetailsByDocumentId,
            DeleteCrewMemberMedicalDetailsById,
            DeleteCrewMemberMedicalDocumentDetailsById,
            DeleteCrewMemberDangerousCargoDetailsById,
            DeleteCrewCourseCeretificateDetailsById,
            DeleteCrewLicenseDetailsById,
            DeleteCrewTrainingRequirementById,
            DeleteCrewOtherDocumentsDetailsById,
            DeleteCrewApprasialDetailsById,
            DeleteCRMDetailsBycrewcrmId,
        }
    }
}
