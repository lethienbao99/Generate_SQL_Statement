using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp2.FactoryTableLinks;

namespace ConsoleApp2
{
    public static class StoredProcedureManager
    {
        public static void Run(string spCode, bool isGenerateParam = false) {
            string input = @"
left JOIN Cat_Orgstructure co  ON hp.OrgstructureID = co.Id AND co.""IsDelete"" IS NULL
			 LEFT join (select * from #tblStopworking ) hsw on hsw.ProfileID = hp.ID
			 LEFT JOIN (select id, isdelete,SalaryClassName from ""Cat_SalaryClass"") css ON css.id =hsw.SalaryClassID AND css.""IsDelete"" is NULL
			 LEFT join (select * from #tblContract ) hctw on hctw.ProfileID = hp.ID
			 LEFT JOIN (select id,ContractTypeName,Type,IsDelete from Cat_ContractType ) cct on cct.ID = hsw.ContractTypeID and cct.Isdelete is null
			 LEFT JOIN #tblEnumTypeContractType tct ON tct.EnumKey = cct.Type
			 LEFT JOIN (select DateEndSuspension,ID,IsDelete from Hre_ProfileMoreInfo) promoreinfo1 on hp.ProfileMoreInfoID = promoreinfo1.ID
			 Left join cat_DelegateCompany cdc on cdc.id = hctw.DelegateCompanyID  and cdc.Isdelete is null
			 Left join Hre_Profile hp10 on hp10.id = cdc.ProfileID  and hp10.Isdelete is null
			left join (select id, isdelete, JobTitleName from Cat_JobTitle ) cjDel ON hp10.JobTitleID = cjDel.Id AND cjDel.""IsDelete"" IS NULL
			left join (select id, isdelete, PositionName from Cat_Position ) cpDel ON hp10.PositionID = cpDel.Id AND cpDel.""IsDelete"" IS NULL
			 left JOIN #tblPermission fcP WITH (NOLOCK) ON fcP.Id = hp.ID
left join (select id, isdelete, CompanyName,Image from ""Cat_Company"" ) cpn ON cpn.id = hp.""CompanyID"" AND cpn.""IsDelete"" IS NULL
	 left join (select id, isdelete, ResignReasonName,Code from Cat_ResignReason ) ResignReason ON ResignReason.Id = hp.ResReasonID AND ResignReason.""IsDelete"" IS NULL
	 left join (select id, isdelete, PositionName, PositionEngName, Code from Cat_Position ) cp ON hp.PositionID = cp.Id AND cp.""IsDelete"" IS NULL
	 left join (select id, isdelete, CostCentreName from Cat_CostCentre ) cc ON hp.CostCentreID = cc.Id AND cc.""IsDelete"" IS NULL
	 left join (select id, isdelete, EthnicGroupName from Cat_EthnicGroup ) ceg ON hp.EthnicID = ceg.Id AND ceg.""IsDelete"" IS NULL
	 left join (select id, isdelete, ReligionName from Cat_Religion ) cr ON hp.ReligionID = cr.Id AND cr.""IsDelete"" IS NULL
	 left join (select id, isdelete, JobTitleName, Code from Cat_JobTitle ) cj ON hp.JobTitleID = cj.Id AND cj.""IsDelete"" IS NULL
	 left join (select id, isdelete, EmployeeTypeName, AnotherName from Cat_EmployeeType ) cet ON hp.EmpTypeID = cet.Id AND cet.""IsDelete"" IS NULL
	 left join (select id, isdelete, CountryName from Cat_Country ) cc1 ON hp.NationalityID = cc1.Id AND cc1.""IsDelete"" IS NULL
	 LEFT JOIN (select id, isdelete, ProfileName from ""Hre_Profile"" WITH (NOLOCK)) spPf1 ON spPf1.id = hp.SupervisorID AND spPf1.""IsDelete"" IS NULL
	 LEFT JOIN (select id, isdelete, ProfileName from ""Hre_Profile"" WITH (NOLOCK)) spPf2 ON spPf2.id = hp.HighSupervisorID AND spPf2.""IsDelete"" IS NULL
	 left join (select id, isdelete, WorkPlaceName, AnotherName from Cat_WorkPlace ) cwp ON hp.""WorkPlaceID"" = cwp.""ID"" AND cwp.""IsDelete"" IS NULL
	 left join (select id, isdelete, NameEntityName, OtherName from Cat_NameEntity ) cne ON hp.""TypeOfStopID"" = cne.""ID"" AND cne.""IsDelete"" IS NULL
	 LEFT JOIN (select id, isdelete, NameEntityType, Code from Cat_NameEntity) cne2 ON cne2.id=hp.TypeOfStopID AND cne2.NameEntityType = ''E_TYPEOFSTOP'' AND cne2.IsDelete IS NULL
	 left join (select id, isdelete, SalaryClassName from Cat_SalaryClass ) csc ON hp.""SalaryClassID"" = csc.""ID"" AND csc.""IsDelete"" IS NULL
	 left join (select E_COMPANY, E_BRANCH, E_UNIT, E_DIVISION, E_DEPARTMENT, E_TEAM, E_SECTION, E_OU_L8, E_OU_L9, E_OU_L10,
	 E_OU_L11, E_OU_L12, E_COMPANY_CODE, E_BRANCH_CODE, E_UNIT_CODE, E_DIVISION_CODE, E_DEPARTMENT_CODE, E_TEAM_CODE, E_SECTION_CODE, E_OU_L8_CODE,
	 E_OU_L9_CODE, E_OU_L10_CODE, E_OU_L11_CODE, E_OU_L12_CODE,OrgstructureID, IsDelete from Cat_OrgUnit ) cou ON hp.OrgstructureID = cou.OrgstructureID AND cou.""IsDelete"" IS NULL
	 LEFT JOIN (select id, isdelete, CountryName from ""Cat_Country"" ) cc2 ON cc2.id=hp.""TCountryID"" AND cc2.""IsDelete"" IS NULL
	 LEFT JOIN (select id, isdelete, CountryName from ""Cat_Country"" ) cc3 ON cc3.id=hp.""PCountryID"" AND cc3.""IsDelete"" IS NULL
	 LEFT JOIN (select id, isdelete, ProvinceName from ""Cat_Province"" ) cp1 ON cp1.id=hp.""TProvinceID"" AND cp1.""IsDelete"" IS NULL
	 LEFT JOIN (select id, isdelete, ProvinceName from ""Cat_Province"" ) cp2 ON cp2.id=hp.""PProvinceID"" AND cp2.""IsDelete"" IS NULL
	 LEFT JOIN (select id, isdelete, DistrictName from ""Cat_District"" ) cd ON cd.id=hp.""TDistrictID"" AND cd.""IsDelete"" IS NULL
	 LEFT JOIN (select id, isdelete, DistrictName from ""Cat_District"" ) cd1 ON cd1.id=hp.""PDistrictID"" AND cd1.""IsDelete"" IS NULL
	 left join (select id, isdelete, villageName from cat_village ) pvila on pvila.id = hp.VillageID and pvila.isdelete is null
	 left join (select id, isdelete, villageName from cat_village ) tvillave on tvillave.id = hp.taVillageID and tvillave.isdelete is null
	 left join (select id, isdelete, NameEntityName from Cat_NameEntity ) cne1 on cne1.id = hp.GraduatedLevelID and cne1.isdelete is null
	 LEFT JOIN (select id, isdelete, UnitName from Cat_UnitStructure ) cust on cust.id = hp.UnitStructureID AND cust.IsDelete Is NULL 
	 left join (select id, isdelete, NameEntityName, OtherName from Cat_NameEntity ) education on education.id = hp.EducationLevelID and education.isdelete is null
	 left join (select ID, IsDelete, PayrollGroupName  from Cat_PayrollGroup ) cpg on cpg.ID = hp.PayrollGroupID and cpg.IsDelete is NULL
	 left join Cat_NameEntity cne10 on cne10.ID = hp.NationalityGroupID and cne10.""IsDelete"" IS NULL 
	 left join (select id, isdelete, AbilityTitleVNI from Cat_AbilityTile ) abilityTile on abilityTile.id = hp.AbilityTileID and abilityTile.isdelete is NULL
	 left join (SELECT id, IsDelete, LastWorkingDay, OriginProvinceID, OriginDistrictID, OriginVillageID, IDCardIssuePlaceID, PassportPlaceNewID, Nationality2ID,YearSeniorityReserved FROM ""Hre_ProfileMoreInfo"") hpif on hpif.ID = hp.ProfileMoreInfoID and hpif.Isdelete is null
	 Left JOIN #tblEnumGenderViewNew egv ON egv.EnumKey = hp.""Gender""
	 Left JOIN #tblEnumGenderV2 egv2 ON egv2.EnumKey = hp10.""Gender""
	 Left JOIN #tblEnumMarriageStatusViewNew msv ON msv.EnumKey = hp.MarriageStatus
     Left JOIN #tblEnumStatusSyn sttStatusSyn ON sttStatusSyn.EnumKey = hp.""StatusSyn""
	 LEFT JOIN (select NameEntityName, id, IsDelete from [Cat_NameEntity] ) EmployeeGroup on EmployeeGroup.id = hp.EmployeeGroupID AND EmployeeGroup.IsDelete Is NULL
	 LEFT JOIN (select id , isdelete, NameEntityName from cat_nameentity) AreaPostJobWork ON AreaPostJobWork.id = hp.AreaPostJobWorkID AND AreaPostJobWork.""IsDelete"" IS null
	 LEFT JOIN hre_profilemoreinfo promoreinfo ON promoreinfo.id = hp.Profilemoreinfoid AND promoreinfo.""IsDelete"" IS null
	 left join (select id, isdelete, NameEntityname from ""Cat_NameEntity"") StopWorkingForm ON hp.StopWorkingFormID = StopWorkingForm.Id AND StopWorkingForm.""IsDelete"" IS NULL
     LEFT JOIN (SELECT id,CutOffDurationName,IsDelete FROM dbo.Att_CutOffDuration) ac ON ac.ID= hp.CutOffDurationID AND ac.IsDelete IS NULL
	 Left JOIN #tblEnumTypeLaborType ttl ON ttl.EnumKey = hp.LaborType
	 Left JOIN #tblEnumProbationTimeUnit ptu ON ptu.EnumKey = hp.""ProbationTimeUnit""
	 Left JOIN #tblEnumStopworkType tst ON tst.EnumKey = hp.""StopWorkType""
	 left join (select id, isdelete, NameEntityName, OtherName from Cat_NameEntity ) cneb ON hp.""ReasonBlacklistID"" = cneb.""ID"" AND cneb.""IsDelete"" IS NULL
	 LEFT JOIN (select ProvinceName,Code , id, IsDelete from ""Cat_Province"" ) cpss1 ON cpss1.ID = hp.PlaceOfBirthID AND cpss1.IsDelete is null
     LEFT JOIN (select ProvinceName,Code , id, IsDelete from ""Cat_Province"" ) cpss2 ON cpss2.ID = hp.PlaceOfIssueID AND cpss2.IsDelete is null
     LEFT JOIN (select ProvinceName,Code , id, IsDelete from ""Cat_Province"" ) cpProvince ON cpProvince.id = hpif.OriginProvinceID AND cpProvince.""IsDelete"" IS NULL
     LEFT JOIN (select DistrictName,Code , id, IsDelete from ""Cat_District"" ) cpDistrict ON cpDistrict.id = hpif.OriginDistrictID AND cpDistrict.""IsDelete"" IS NULL
     LEFT JOIN (select VillageName,Code , id, IsDelete from ""Cat_Village"" ) cpVillage ON cpVillage.id=hpif.OriginVillageID AND cpVillage.""IsDelete"" IS NULL
     LEFT JOIN (select IDCardIssuePlaceName,Code , id, IsDelete from ""Cat_IDCardIssuePlace"" ) CardPlace ON CardPlace.id=hpif.IDCardIssuePlaceID AND CardPlace.""IsDelete"" IS NULL
     LEFT JOIN (select PassportIssuePlaceName,Code , id, IsDelete from ""Cat_PassportIssuePlace"" ) PassPlace ON PassPlace.id=hpif.PassportPlaceNewID AND PassPlace.""IsDelete"" IS NULL
	 LEFT JOIN (SELECT CountryName, id, IsDelete, Nationality from ""Cat_Country"" ) newNationality2ID ON newNationality2ID.ID = hpif.Nationality2ID AND newNationality2ID.""IsDelete""  IS NULL
	 LEFT JOIN (select id, isdelete, ProfileID,DecisionNo from #tblWorkHistory) hw ON hw.ProfileID = hp.ID AND hw.""IsDelete"" IS NULL
	";
            var listDataTableOrigin = new List<DataTableOrigin>();
            string[] tableNames2 = FactoryTableLinks.GenerateTableLinks(input, spCode, "Hre_Profile.hp", out listDataTableOrigin);


            string inputColumn = @"hsw.Attachment,
			css.SalaryClassName as StopWorkingSalaryClassName,
			cdc.NoDecision,
			hctw.DateAuthorize,
			hp10.ProfileName as DelegateName,
			hp10.NameEnglish as DelegateNameEn,
			egv2.EnumTranslate AS ""GenderViewDelegate"",
			cjDel.JobtitleName as JobtitleNameDelegate,
			cpDel.PositionName as PositionNameDelegate,
			hp.SocialInsDeliveryDate as ProfileQuitSocialInsDeliveryDate,
			 cne10.NameEntityName AS ""NationalityGroup"",
			 cpg.PayrollGroupName,
			 spPf1.ProfileName AS ""SupervisiorName"",
			 spPf2.ProfileName AS ""HighSupervisiorName"",
			 hp.Id,
			 hp.TypeOfStopID,
			 hp.StopWorkType,
			 hp.ProfileName,
			 hp.NameFamily,
			 hp.FirstName,
			 hp.NameEnglish,
			 hp.DateApplyAttendanceCode,
			 hp.ImagePath,
			 hp.CodeEmp,
			 hp.CodeTax,
			 hp.CodeAttendance,
			 hp.StatusSyn,
			 hp.DateHire,
			 hp.DateEndProbation,
			 hp.OrgStructureID,
			 hp.NationalityGroupID,
			 hp.PayrollGroupID,
			 hp.Settlement,
			 hp.MonnthSettlement,
			 hp.PositionID,
			 hp.DateOfEffect,
			 hp.CostCentreID,
			 hp.WorkingPlace,
			 hp.Gender,
			 hp.DayOfBirth,
			 hp.MonthOfBirth,
			 hp.YearOfBirth,
			 hp.PlaceOfBirth,
			 hp.NationalityID,
			 hp.EthnicID,
			 hp.ReligionID,
			 hp.BloodType,
			 hp.Height,
			 hp.Weight,
			 hp.IDNo,
			 hp.E_IDNo,
			 hp.IDDateOfIssue,
			 hp.IDPlaceOfIssue,
			 hp.PassportNo,
			 hp.E_PassportNo,
			 hp.PassportDateOfExpiry,
			 hp.PassportDateOfIssue,
			 hp.PassportPlaceOfIssue,
			 hp.IsSettlement,
			 hp.Email,
			 hp.Cellphone,
			 hp.E_Cellphone,
			 hp.HomePhone,
			 hp.BusinessPhone,
			 hp.PAddress,
			 hp.TAddress,
			 hp.JobTitleID,
			 hp.EmpTypeID,
			 hp.MarriageStatus,
			 hp.SupervisorID,
			 hp.HighSupervisorID,
			 hp.RequestDate,
			 hp.ResReasonID,
			 ResignReason.ResignReasonName,
			 ResignReason.Code as ReasonCodeLeave,
			 hp.ReceiveHealthIns,
			 hp.ReceiveHealthInsDate,
			 hp.ResonBackList,
			 hp.ResignNo,
			 hp.IsBlackList,
			 hp.DateQuit,
			 hp.Notes,
			 hp.UserCreate,
			 hp.UserUpdate,
			 hp.DateCreate,
			 hp.DateUpdate,
			 hp.UserLockID,
			 hp.DateLock,
			 hp.IsDelete,
			 hp.ServerUpdate,
			 hp.ServerCreate,
			 hp.IPCreate,
			 hp.AreaPostJobWorkID,
			 cp.PositionName,
			 cp.Code as PositionCode,
			 cc.CostCentreName,
			 ceg.EthnicGroupName,
			 cr.ReligionName,
			 cj.JobTitleName,
			 cj.Code as JobTitleCode,
			 cet.EmployeeTypeName,
			 co.OrgStructureName,
			 co.Code as OrgStructureCode,
			 hp.StoredDocuments,
			 cc1.CountryName as NationalityName,
			 cne.""NameEntityName"" AS ""TypeOfStopName"",
			 cne2.Code as LeaveTypeCode,
			 hp.StopWorkType,
			 hp.dateofbirth,
			 csc.SalaryClassName,
			 cwp.WorkPlaceName,
			 cwp.AnotherName as ""WorkPlaceOtherName"",
			 cet.AnotherName as ""EmployeeTypeOtherName"",
			 education.OtherName as ""EducationLevelOtherName"",
			 cne.""OtherName"" AS ""TypeOfStopOtherName"",
			 cou.E_COMPANY,
			 cou.E_BRANCH,
			 cou.E_UNIT,
			 cou.E_DIVISION,
			 cou.E_DEPARTMENT,
			 cou.E_TEAM,
			 cou.E_SECTION,
			 cou.E_OU_L8,
			 cou.E_OU_L9,
			 cou.E_OU_L10,
			 cou.E_OU_L11,
			 cou.E_OU_L12,
			 cou.E_COMPANY_CODE,
			 cou.E_BRANCH_CODE,
			 cou.E_UNIT_CODE,
			 cou.E_DIVISION_CODE,
			 cou.E_DEPARTMENT_CODE,
			 cou.E_TEAM_CODE,
			 cou.E_SECTION_CODE,
			 cou.E_OU_L8_CODE,
			 cou.E_OU_L9_CODE,
			 cou.E_OU_L10_CODE,
			 cou.E_OU_L11_CODE,
			 cou.E_OU_L12_CODE,
			 hp.DatehireNew,
			 cc2.""CountryName"" AS ""TCountryName"",
			 cc3.""CountryName"" AS ""PCountryName"",
			 cp2.""ProvinceName"" AS ""PProvinceName"",
			 cp1.""ProvinceName"" AS ""TProvinceName"",
			 cd.""DistrictName"" AS ""TDistrictName"",
			 cd1.""DistrictName"" AS ""PDistrictName"",
			 hp.Origin,
			 pvila.VillageName as ""PVillageName"",
			 tvillave.VillageName as ""TVillageName"",
			 cpn.Image as""CompanyLogo"",
			 hp.idcard,
			 hp.E_IDCard,
			 hp.IDCardDateOfIssue,
			 hp.IDCardPlaceOfIssue,
			 cne1.NameEntityName as ""GraduatedLevelName"",
			 cust.UnitName as ""UnitStructureName"",
			 hp.OtherReason,
			 egv.EnumTranslate as ""GenderView"",
			 msv.EnumTranslate as ""MarriageStatusView"",
			 education.nameentityname as ""EducationLevelName"",
			 hp.CodeEmpClient,
			 abilityTile.AbilityTitleVNI,
			 abilityTile.AbilityTitleVNI as AbilityTileName,
			 hp.ResignNo,
			 hp.NameForSecondaryEmergency,
			sttStatusSyn.EnumTranslate as ""StatusSynView"" ,
			 cct.ContractTypeName,
			 tct.EnumTranslate as TypeContractType,
			 hsw.PaymentDay,
			 hsw.LastWorkingDay,
			 cp.PositionEngName,
			 EmployeeGroup.NameEntityName as ""EmployeeGroupName"",
			 AreaPostJobWork.NameEntityName as AreaPostJobWorkName,
			 promoreinfo.LastNameEN,
			 promoreinfo.FirstNameEN,
			 promoreinfo.MiddleNameEN,
			 promoreinfo.SeniorityBonusDate,
			 promoreinfo.AnnualLeaveDate,
			 promoreinfo.DateEndSuspension,
			 StopWorkingForm.NameEntityName as StopWorkingFormName,
			 hp.Settlement,
			 ac.CutOffDurationName,
			 hsw.ApproveComment as CommentOfApprover,
			 hsw.ApproveComment1 as CommentOfFirstApprover,
			 hsw.ApproveComment2 as CommentOfSecondApprover,
			 hsw.ApproveComment3 as CommentOfThirdApprover,
			 hsw.ApproveComment4 as CommentOfLastApprover,
			 ttl.EnumTranslate as LaborTypeView,
			 cpn.CompanyName,
			 hp.ProbationTime,
			 hp.ProbationTimeUnit,
			 ptu.EnumTranslate AS ""ProbationTimeUnitView"",
			 tst.EnumTranslate as ""StopWorkTypeName"",
			  cneb.NameEntityName as ""BlackListReasonName"",
			  hp.DetailBlacklistReason,
			 hpif.LastWorkingDay as LastWorkingDayByMoreInfo,
			cpss1.ProvinceName as PlaceOfBirthIDView,
			cpProvince.ProvinceName as OriginProvinceIDView,
			cpDistrict.DistrictName as OriginDistrictIDView,
			cpVillage.VillageName as OriginVillageIDView,
			cpss2.ProvinceName as PlaceOfIssueIDView,
			PassPlace.PassportIssuePlaceName as PassportPlaceIDView,
			CardPlace.IDCardIssuePlaceName as IDCardIssuePlaceIDView,
			newNationality2ID.Nationality AS ""Nationality2Name"",
			hpif.YearSeniorityReserved,
			hw.DecisionNo,
			co.OrgStructureNameEN";
            FactorySelectColumns.GenerateSelectColumns(inputColumn, listDataTableOrigin, spCode);

			if(isGenerateParam)
			{
                string paramsText = @"@ProfileID NVARCHAR(max) = NULL,
				 @ProfileName NVARCHAR(100) = NULL,
				 @CodeEmp NVARCHAR(MAX) = NULL,
				 @strOrgIds varchar(max)= NULL,
				 @PositionId NVARCHAR(MAX) = NULL,
				 @Gender VARCHAR(500) = NULL,
				 @IdNo NVARCHAR(50) = NULL,
				 @JobTitleId NVARCHAR(MAX) = NULL,
				 @EmpTypeId varchar(max) = NULL,
				 @DateFrom DATETIME = NULL,
				 @DateTo DATETIME = NULL,
				 @ResreasonId NVARCHAR(MAX) = NULL,
				 @TypeOfStopID NVARCHAR(MAX) = NULL,
				 @WorkPlaceID NVARCHAR(MAX) = NULL,
				 @isALTERTemplate bit = NULL,
				 @isALTERDynamicGrid bit = NULL,
				 @ExportID uniqueidentifier = NULL,
				 @ExcelType NVARCHAR(100) =NULL,
				 @SettmentStatus NVARCHAR(100)= NULL,
				 @IsSettment bit= NULL,
				 @Settment int= null,
				 @isQuitInMonth bit = NULL,
				 @CodeEmpClient NVARCHAR(max) = null,
				 @StatusSyn VARCHAR(500) = NULL,
				 @strContractTypeID VARCHAR(max) = NULL,
				 @AreaPostJobWorkID nvarchar(max) = null,
				 @PaymentFrom DATETIME = NULL,
				 @PaymentTo DATETIME = NULL,
				 @StrCompanyID varchar(max)= NULL,
				 @strStopWorkType varchar(max)= NULL,	
				 @PayrollGroupIDs VARCHAR(MAX) = NULL,
				 @NationnalGroupIDs VARCHAR(MAX) = NULL,
				 @AbilityTileID VARCHAR(MAX) = NULL,
				 @DateHireFrom DATETIME = NULL,
				 @DateHireTo DATETIME = NULL,
				 @ResignReasonIDs VARCHAR(MAX) = NULL,
				 @DateSuspensionFrom DATETIME = NULL,
				 @DateSuspensionTo DATETIME = NULL";
                FactoryParameters.GenerateParameters(paramsText, spCode, false, true);
            }

            
            Console.ReadLine();
        }



    }
}
