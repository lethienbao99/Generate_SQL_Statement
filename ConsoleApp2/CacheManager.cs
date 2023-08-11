using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public static class CacheManager
    {
        public static void Run() {
            string input = @"
LEFT JOIN (select id, isdelete, ProfileID, EmployeeGroupFirstDetailID, EmployeeGroupSecondDetailID,DecisionNo from #tblWorkHistory) hw ON hw.ProfileID = hp.ID AND hw.""IsDelete"" IS NULL
LEFT JOIN (select ""TradeUnionistPositionName"", id, IsDelete from ""Cat_TradeUnionistPosition"") ctup ON hp.""TradeUnionistPositionID"" = ctup.id AND ctup.""IsDelete"" IS NULL
LEFT JOIN #tblSalaryInformation ssf ON ssf.ProfileID = hp.ID AND ssf.""IsDelete"" IS NULL
LEFT JOIN #tblAnnualHealth tah ON tah.ProfileID = hp.ID AND tah.""IsDelete"" IS NULL
Left JOIN #tblEnumHealthCheckGroup teh ON teh.EnumKey = tah.HealthCheckGroup
left join Med_TypeResultHealth mtrh1 on mtrh1.ID=tah.TypeResultHealthID and mtrh1.IsDelete is null
LEFT JOIN #tblImmunizationRecord mir ON mir.ProfileID = hp.ID AND mir.""IsDelete"" IS NULL
left join Med_Medicine mm on mm.ID=mir.MedicineID and mm.IsDelete is null 
left join Cat_Company ccy ON hp.CompanyID = ccy.""ID"" AND ccy.""IsDelete"" IS NULL
left join Cat_AbilityTile ca ON hp.AbilityTileID = ca.Id AND ca.""IsDelete"" IS NULL
LEFT JOIN Cat_Bank cb1 ON cb1.ID = ssf.BankID AND cb1.""IsDelete"" IS NULL
LEFT JOIN Cat_Bank cb2 ON cb2.ID = ssf.BankID2 AND cb2.""IsDelete"" IS NULL
LEFT JOIN Cat_Bank cb3 ON cb3.ID = ssf.BankID3 AND cb3.""IsDelete"" IS NULL
LEFT JOIN Hre_CandidateGeneral hcg ON hcg.ProfileID = hp.id AND hcg.""IsDelete"" IS NULL
LEFT JOIN Cat_DORmitORy cdtr ON cdtr.id = hp.DormitoryID AND cdtr.""IsDelete"" IS NULL
left join Cat_NameEntity cners on cners.ID = hp.ReplaceForReasonID and cners.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, ProvinceName,Code from ""Cat_Province"") SocialInsPlace ON SocialInsPlace.id=hp.SocialInsPlaceID AND SocialInsPlace.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, Code, PositionName,PositionNameInLaw, ""PositionEngName"",ParentPositionID from ""Cat_Position"") cp ON hp.""PositionID"" = cp.id AND cp.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, CostCentreName,Code from ""Cat_CostCentre"") cc ON hp.""CostCentreID"" = cc.id AND cc.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, EthnicGroupName, OtherName from ""Cat_EthnicGroup"") ceg ON hp.""EthnicID"" = ceg.id AND ceg.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, ReligionName, OtherName from ""Cat_Religion"") cr ON hp.""ReligionID"" = cr.id AND cr.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, JobTitleName, JobTitleNameEn, Code from ""Cat_JobTitle"") cj ON hp.""JobTitleID"" = cj.id AND cj.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, EmployeeTypeName, AnotherName, Notes from ""Cat_EmployeeType"") cet ON hp.""EmpTypeID"" = cet.id AND cet.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, OrgStructureName, OrderNumber, Code, OrgStructureOtherName,OrgStructureNameEN from ""Cat_OrgStructure"") co ON hp.""OrgStructureID"" = co.id AND co.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, CountryName,Code from ""Cat_Country"") cc1 ON hp.""NationalityID"" = cc1.id AND cc1.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, ProfileName,PositionID,NameEnglish,CodeEmp, NameFamily, FirstName, MiddleName from ""Hre_Profile"") spPf1 ON sppf1.id = hp.""SupervisorID"" AND spPf1.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, ProfileName,PositionID,NameEnglish,CodeEmp, NameFamily, FirstName, MiddleName from ""Hre_Profile"") spPf2 ON sppf2.id = hp.""HighSupervisorID"" AND spPf2.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, ProfileName,PositionID,NameEnglish,CodeEmp, NameFamily, FirstName, MiddleName from ""Hre_Profile"") spPf3 ON spPf3.id = hp.""MidSupervisorID"" AND spPf3.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, ProfileName,PositionID,NameEnglish,CodeEmp, NameFamily, FirstName, MiddleName from ""Hre_Profile"") spPf4 ON spPf4.id = hp.NextSupervisorID AND spPf4.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, NameEntityName, OtherName from ""Cat_NameEntity"") cne ON hp.""EducationLevelID""=cne.""ID"" AND cne.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, NameEntityName, OtherName from ""Cat_NameEntity"") cne1 ON hp.""GraduatedLevelID""=cne1.id AND cne1.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, ResignReasonName from ""Cat_ResignReason"") crr ON crr.id = hp.""ResReasonID"" AND crr.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, WorkPlaceName, Code , Address, AnotherName, ProvinceID from ""Cat_WorkPlace"" ) cwp ON cwp.id = hp.""WorkPlaceID"" AND cwp.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, CountryName from ""Cat_Country"") cc2 ON cc2.id=hp.""TCountryID"" AND cc2.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, CountryName from ""Cat_Country"") cc3 ON cc3.id=hp.""PCountryID"" AND cc3.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, ProvinceName,Code from ""Cat_Province"") cp1 ON cp1.id=hp.""TProvinceID"" AND cp1.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, ProvinceName,Code from ""Cat_Province"") cp2 ON cp2.id=hp.""PProvinceID"" AND cp2.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, DistrictName,Code from ""Cat_District"") cd ON cd.id=hp.""TDistrictID"" AND cd.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, DistrictName,Code from ""Cat_District"") cd1 ON cd1.id=hp.""PDistrictID"" AND cd1.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, PayrollGroupName from ""Cat_PayrollGroup"") cpg ON cpg.id=hp.""PayrollGroupID"" AND cpg.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, ShopName, ""ShopGroupID"", Code from ""Cat_Shop"") cs ON cs.id=hp.""ShopID"" AND cs.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, ShopGroupName from ""Cat_ShopGroup"") csg ON cs.""ShopGroupID""=csg.id AND csg.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, SalaryClassName,Code,SalaryClassTypeID from ""Cat_SalaryClass"") csc ON csc.id = hp.""SalaryClassID"" AND csc.""IsDelete"" IS NULL
LEFT JOIN (select id, isdelete, SalaryClassTypeName, Code from ""Cat_SalaryClassType"" ) csct ON csct.id=csc.""SalaryClassTypeID"" AND csct.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, ContractTypeName from ""Cat_ContractType"") cct ON cct.id = hp.""ContractTypeID"" AND cct.""IsDelete"" IS NULL 
LEFT JOIN (select OtherCertifications,OtherFileAttached,id , isdelete, ""CodeCandidate"",StrTagID,CandidateNumber,DateExam,AbilityTileID from ""Rec_Candidate"") rc ON rc.id = hp.""CandidateID"" AND rc.""IsDelete"" IS NULL 
LEFT JOIN (select id , isdelete, ""AbilityTitleVNI"" from ""Cat_AbilityTile"") cat ON cat.id=hp.""AbilityTileID"" AND cat.""IsDelete"" IS NULL
LEFT JOIN (select * from Cat_OrgUnit ) cou ON cou.OrgstructureID = hp.OrgstructureID AND cou.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, CountryName from ""Cat_Country"") cc4 on cc4.ID=hp.""IDNoCountryID"" AND cc4.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, ProvinceName from ""Cat_Province"") cp4 on cp4.ID=hp.""IDNoProvinceID"" AND cp4.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, DistrictName from ""Cat_District"") cd4 on cd4.ID=hp.""IDNoDistrictID"" AND cd4.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, VillageName from ""Cat_Village"") cv2 on cv2.ID=hp.""IDNoAVillageID"" AND cv2.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, VillageName,Code from ""Cat_Village"") cv ON hp.VillageID = cv.Id AND cv.""IsDelete"" IS NULL
left join (select id , isdelete, orgstructureid, info1, info2, info3, info4, info5, info6
, info7, info8, info9, info10, info11, info12, info13, info14, info15, info16 from Cat_OtherInfoOrg) coio on coio.orgstructureid = co.id and coio.isdelete is null
LEFT JOIN (select id , isdelete, VillageName,Code from ""Cat_Village"") cv1 on cv1.ID = hp.TAVillageID AND cv1.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, NameEntityName from ""Cat_NameEntity"") vehi ON hp.""VehicleID""=vehi.id AND vehi.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, SpecialAreaName from ""Cat_SpecialArea"") csa1 on csa1.ID=hp.""SpecialAreaID"" AND csa1.""IsDelete"" IS NULL
LEFT JOIN (select id , isdelete, ""CompanyName"", CompanyNameEN,AddressVN,AddressEN,Phone,Image,
ShortName,ChairmanNameVN,ChairmanNameEN,ChairmanNationalityVN,ChairmanNationalityEN,ChairmanGender,
ChairmanJobtitileVN,ChairmanJobtitleEN,ChairmanPositionVN,ChairmanPositionEN from ""Cat_Company"") ccp ON ccp.id = hp.""CompanyID"" AND ccp.""IsDelete"" IS NULL
LEFT JOIN (select id, isdelete, PositionName,PositionEngName from Cat_Position) cpspPf1 ON cpspPf1.id = spPf1.PositionID	AND cpspPf1.""IsDelete"" IS NULL
LEFT JOIN (select id, isdelete, PositionName,PositionEngName from Cat_Position) cpspPf2 ON cpspPf2.id = spPf2.PositionID	AND cpspPf2.""IsDelete"" IS NULL
LEFT JOIN Hre_ProfileMoreInfo hpmf ON hpmf.id = hp.ProfileMoreInfoID AND hpmf.""IsDelete"" IS NULL
LEFT JOIN (select id, isdelete,HDTJobTypeName from ""Cat_HDTJobType"" where ""IsDelete"" IS NULL) chdt ON chdt.id = hpmf.""HDTJobTypeID""
 Left JOIN #tblEnumToxicLevel toxic ON toxic.EnumKey = hpmf.ToxicLevel
LEFT JOIN (select id, ProfileMoreInfoID , isdelete, ProfileName from ""Hre_Profile"" WITH (NOLOCK)) spPf5 ON spPf5.ID = hpmf.AnnunciatorID AND spPf5.""IsDelete"" IS NULL
left join (select id, isdelete, SourceAdsName, Code from Cat_SourceAds ) cas ON hpmf.SourceAdsID = cas.Id AND cas.""IsDelete"" IS NULL
LEFT JOIN Cat_District cd2 on cd2.ID = hpmf.SocialInsDistrictID and cd2.""IsDelete"" IS NULL
left join (select id, isdelete, PoliticalLevelName, Code from Cat_PoliticalLevel ) PoliticalLevel ON hpmf.PoliticalLevelID = PoliticalLevel.Id AND PoliticalLevel.""IsDelete"" IS NULL
left join (select id, isdelete, CommunistPartyPositionName, Code from Cat_CommunistPartyPosition ) CommunistParty ON hpmf.CommunistPartyPositionID = CommunistParty.Id AND CommunistParty.""IsDelete"" IS NULL
left join (select id, isdelete, YouthUnionPositionName, Code from Cat_YouthUnionPosition ) YouthUnionPosition ON hpmf.YouthUnionPositionID = YouthUnionPosition.Id AND YouthUnionPosition.""IsDelete"" IS NULL
Left JOIN #tblEnumExpertiseType tset ON tset.EnumKey = hpmf.CardIssueType
left join (select id, isdelete, FundName, Code from Cat_Fund ) caf ON hpmf.FundID = caf.Id AND caf.""IsDelete"" IS NULL
LEFT JOIN (select ProvinceName, id, IsDelete,Code from ""Cat_Province"") cp5 on cp5.ID=hpmf.ProvinceBirthCertificateID AND cp5.""IsDelete"" IS NULL
LEFT JOIN (select DistrictName, id, IsDelete,Code from ""Cat_District"") cd5 on cd5.ID=hpmf.DistrictBirthCertificateID AND cd5.""IsDelete"" IS NULL
LEFT JOIN (select VillageName, id, IsDelete,Code from ""Cat_Village"") cv5 on cv5.ID=hpmf.VillageBirthCertificateID AND cv5.""IsDelete"" IS NULL
LEFT JOIN (select CostActivity, id, IsDelete from Cat_CostActivity) ccat on ccat.ID=hp.CostActivityID AND ccat.""IsDelete"" IS NULL
LEFT JOIN (select NameEntityName, id, IsDelete from ""Cat_NameEntity"") cne6 ON cne6.id = hp.""EmployeeGroupID"" AND cne6.""IsDelete"" IS NULL
LEFT JOIN (select NameEntityName, OtherName, id, IsDelete from ""Cat_NameEntity"") cne7 ON cne7.id = hp.TypeOfTransferID AND cne7.""IsDelete"" IS NULL
left join Cat_NameEntity cne10 on cne10.ID = hp.NationalityGroupID and cne10.""IsDelete"" IS NULL 
LEFT JOIN (select id , isdelete, NameEntityName from cat_nameentity) AreaPostJobWork ON AreaPostJobWork.id = hp.AreaPostJobWorkID AND AreaPostJobWork.""IsDelete"" IS null
LEFT JOIN (select id , isdelete, NameEntityName from cat_nameentity) AreaPostJob ON AreaPostJob.id = hp.AreaPostJobID AND AreaPostJob.""IsDelete"" IS null
LEFT JOIN (select id, isdelete, UnitName, UnitCode from Cat_UnitStructure ) cust on cust.id = hp.UnitStructureID AND cust.IsDelete Is NULL 
LEFT JOIN (select CountryName, id, IsDelete from Cat_Country) pcountry ON pcountry.id = hp.PCountryID AND pcountry.IsDelete IS NULL
LEFT JOIN (select ProvinceName, id, IsDelete from Cat_Province) pProvince ON pProvince.id = hp.pProvinceID AND pProvince.IsDelete IS NULL
LEFT JOIN (select DistrictName, id, IsDelete from Cat_District) pDistrict ON pDistrict.id = hp.pDistrictID AND pDistrict.IsDelete IS NULL
LEFT JOIN (select VillageName, id, IsDelete from Cat_Village) pvillage ON pvillage.id = hp.VillageID AND pvillage.IsDelete IS NULL
LEFT JOIN (select CountryName, id, IsDelete from Cat_Country) tcountry ON tcountry.id = hp.TCountryID AND tcountry.IsDelete IS NULL
LEFT JOIN (select ProvinceName, id, IsDelete from Cat_Province) tProvince ON tProvince.id = hp.TProvinceID AND tProvince.IsDelete IS NULL
LEFT JOIN (select DistrictName, id, IsDelete from Cat_District) tDistrict ON tDistrict.id = hp.TDistrictID AND tDistrict.IsDelete IS NULL
LEFT JOIN (select VillageName, id, IsDelete from Cat_Village) tvillage ON tvillage.id = hp.TAVillageID AND tvillage.IsDelete IS NULL
left join (select id, isdelete, NameEntityname from ""Cat_NameEntity"") StopWorkingForm ON hp.StopWorkingFormID = StopWorkingForm.Id AND StopWorkingForm.""IsDelete"" IS NULL
LEFT JOIN (SELECT id,CutOffDurationName,IsDelete FROM dbo.Att_CutOffDuration) ac ON ac.ID= hp.CutOffDurationID AND ac.IsDelete IS NULL
left JOIN Cat_JobTitleProfessional cjp ON hpmf.JobtitleProfesionalID = cjp.ID AND cjp.""IsDelete"" IS NULL
LEFT JOIN (select id,isdelete,NameEntityName from Cat_NameEntity ) name1 on name1.ID = hp.DistributionChannelID and name1.IsDelete is null
LEFT JOIN (select id,isdelete,NameEntityName from Cat_NameEntity ) name2 on name2.ID = hp.MarketDomainID and name2.IsDelete is null
LEFT JOIN (select id,isdelete,NameEntityName from Cat_NameEntity ) name3 on name3.ID = hp.RegionMarketID and name3.IsDelete is null
LEFT JOIN (select id,isdelete,NameEntityName from Cat_NameEntity ) name4 on name4.ID = hp.MarketAreaID and name4.IsDelete is null
LEFT JOIN (select id,isdelete,NameEntityName from Cat_NameEntity ) name5 on name5.ID = hp.OriginalDistributorID and name5.IsDelete is null
LEFT JOIN (select id,isdelete, OtherName from Cat_NameEntity ) cne2 on cne2.ID = hp.NationalityGroupID and cne2.IsDelete is null
left join (select id, isdelete, NameEntityName from Cat_NameEntity) tosn ON hp.""TypeOfStopID"" = tosn.""ID"" AND tosn.""IsDelete"" IS NULL
left join (SELECT id, IsDelete, LastWorkingDay FROM ""Hre_ProfileMoreInfo"") hpif on hpif.ID = hp.ProfileMoreInfoID and hpif.Isdelete is null
LEFT JOIN Med_TypeResultHealth mtrh on mtrh.ID=hpmf.TypeResultHealthID and mtrh.IsDelete is null
Left JOIN #tblEnumGenderViewNew egv ON egv.EnumKey = hp.""Gender""
LEFT JOIN #tblEnumGenderViewNew egv1 ON egv1.EnumKey = hpmf.""HouseholderGender""
Left JOIN #tblEnumMarriageStatusViewNew msv ON msv.EnumKey = hp.MarriageStatus
Left JOIN #tblEnumLaborTypeView ltv ON ltv.EnumKey = hp.LaborType
Left JOIN #tblEnumStatusSynView ssv ON ssv.EnumKey = hp.StatusSyn
Left JOIN #tblEnumEmploymentTypeView etv ON etv.EnumKey = hp.EmploymentType
Left JOIN #tblEnumProbationTimeUnit ptu ON ptu.EnumKey = hp.""ProbationTimeUnit""
Left JOIN #tblEnumStopworkType tst ON tst.EnumKey = hp.""StopWorkType""
Left JOIN #tblEnumTermTimeUnit ttu ON ttu.EnumKey = hpmf.WHTermTimeUnit
Left JOIN #tblEnumSocialInsNoStatusViewNew esins ON esins.EnumKey = hp.""SocialInsNoStatus""
LEFT JOIN (SELECT id,isdelete,RegionName FROM dbo.Cat_Region) cr1 ON cr1.id = hp.RegionID AND cr1.IsDelete IS null
left join (select id,isdelete,RelativeTypeName from Cat_RelativeType ) crt on crt.ID = hpmf.HouseHolderRelativeTypeID and crt.IsDelete is null
LEFT JOIN (select st.ID, st.IsDelete, st.ProfileID, st.PITCode, st.DateEffective from Sal_Tax) st on st.ProfileID = hp.ID AND st.""IsDelete"" IS NULL 
LEFT JOIN #tblEnumPITFormulaTypeView ptv ON ptv.EnumKey = st.PITCode
LEFT JOIN #tblEnumSalaryFormView sf ON sf.EnumKey = hpmf.SalaryPaidByTheFormOf
left join #tblCurrentContract CurContract on CurContract.ProfileID = hp.ID
left join #tblFirstContract FirstContract on FirstContract.ProfileID = hp.ID
LEFT JOIN (select id , isdelete, ProfileName from Hre_Profile) Introducer ON Introducer.id = hpmf.IntroducerID AND Introducer.""IsDelete"" IS null
left join #tblStopWorking hsw ON hp.id = hsw.ProfileID AND hp.DateQuit = hsw.DateStop AND hsw.""IsDelete"" IS NULL
LEFT JOIN (select ID,ProfileID,HealthInsNo, HealthInsIssueDate,HealthTreatmentPlaceID,HealthInsExpiredDate,ReceiveHealthInsDate,ProvinceHospital,HealthTreatmentPlaceCode, FiveConsecutiveYearsFrom, IsDelete FROM Hre_HealthInsuranceCard) hhic on hhic.ID = hic.ProfileID and hhic.IsDelete is null
LEFT JOIN dbo.Cat_HealthTreatmentPlace hhkk ON hhkk.ID = hhic.HealthTreatmentPlaceID AND hhkk.IsDelete IS NULL
Left JOIN #tblEnumGenderViewNew egv2 ON egv2.EnumKey = hpmf.GenderOfContact
Left JOIN #tblEnumGenderViewNew egv3 ON egv3.EnumKey = hpmf.GenderOfContact2
 left join #tblCandidateGeneral general on general.ProfileID = hp.id
 left join (select id, isdelete, UsualAllowanceName from Cat_UsualAllowance) Allowance1 ON general.AllowanceID1 = Allowance1.ID AND Allowance1.""IsDelete"" IS NULL
 left join (select id, isdelete, UsualAllowanceName from Cat_UsualAllowance) Allowance2 ON general.AllowanceID2 = Allowance2.ID AND Allowance2.""IsDelete"" IS NULL
 left join (select id, isdelete, UsualAllowanceName from Cat_UsualAllowance) Allowance3 ON general.AllowanceID3 = Allowance3.ID AND Allowance3.""IsDelete"" IS NULL
 left join (select id, isdelete, UsualAllowanceName from Cat_UsualAllowance) Allowance4 ON general.AllowanceID4 = Allowance4.ID AND Allowance4.""IsDelete"" IS NULL
 left join (select id, isdelete, UsualAllowanceName from Cat_UsualAllowance) Allowance5 ON general.AllowanceID5 = Allowance5.ID AND Allowance5.""IsDelete"" IS NULL
 left join (select id, isdelete, UsualAllowanceName from Cat_UsualAllowance) Allowance6 ON general.AllowanceID6 = Allowance6.ID AND Allowance6.""IsDelete"" IS NULL
 left join (select id, isdelete, UsualAllowanceName from Cat_UsualAllowance) Allowance7 ON general.AllowanceID7 = Allowance7.ID AND Allowance7.""IsDelete"" IS NULL
 left join (select id, isdelete, UsualAllowanceName from Cat_UsualAllowance) Allowance8 ON general.AllowanceID8 = Allowance8.ID AND Allowance8.""IsDelete"" IS NULL
 left join (select id, isdelete, UsualAllowanceName from Cat_UsualAllowance) Allowance9 ON general.AllowanceID9 = Allowance9.ID AND Allowance9.""IsDelete"" IS NULL
 left join (select id, isdelete, UsualAllowanceName from Cat_UsualAllowance) Allowance10 ON general.AllowanceID10 = Allowance10.ID AND Allowance10.""IsDelete"" IS NULL
 left join (select id, isdelete, UsualAllowanceName from Cat_UsualAllowance) Allowance11 ON general.AllowanceID11 = Allowance11.ID AND Allowance11.""IsDelete"" IS NULL
 left join (select id, isdelete, UsualAllowanceName from Cat_UsualAllowance) Allowance12 ON general.AllowanceID12 = Allowance12.ID AND Allowance12.""IsDelete"" IS NULL
 left join (select id, isdelete, UsualAllowanceName from Cat_UsualAllowance) Allowance13 ON general.AllowanceID13 = Allowance13.ID AND Allowance13.""IsDelete"" IS NULL
 left join (select id, isdelete, UsualAllowanceName from Cat_UsualAllowance) Allowance14 ON general.AllowanceID14 = Allowance14.ID AND Allowance14.""IsDelete"" IS NULL
 left join (select id, isdelete, UsualAllowanceName from Cat_UsualAllowance) Allowance15 ON general.AllowanceID15 = Allowance15.ID AND Allowance15.""IsDelete"" IS NULL
 left join (select id, isdelete, CurrencyName from Cat_Currency) Currency ON general.CurrencyID = Currency.ID AND Currency.""IsDelete"" IS NULL
 left join #Temp_Eva_Performance tep ON tep.ProfileID = hp.""ID"" 
 Left JOIN #tblEmailSendingStatus esv ON esv.EnumKey = tep.""EvaSendMailSatus""
	left join #tblContract hc ON hc.ProfileID = hp.""ID""
 left join Sys_UserInfo sui on sui.ID = hc.UserApproveID AND sui.""IsDelete"" IS NULL
 LEFT JOIN ""Hre_Profile"" hp2 ON hp2.id = sui.ProfileID AND hp2.""IsDelete"" IS NULL
 Left JOIN #tblEnumSendMailStatusEmpNew SendMailStatusEmpNew ON SendMailStatusEmpNew.EnumKey = hpmf.SendMailStatusEmpNew
 LEFT JOIN (select id,isdelete,NameEntityName from Cat_NameEntity ) name6 on name6.ID = hpmf.DelegationReasonID and name6.IsDelete is null
 LEFT JOIN(SELECT id,isdelete,AbilityTitleVNI FROM Cat_AbilityTile) ca1 ON ca1.ID = rc.AbilityTileID AND ca1.IsDelete IS NULL
 LEFT JOIN (select id,isdelete,NameEntityName from Cat_NameEntity ) name7 on name7.ID = hpmf.DelegationReasonID2 and name7.IsDelete is null
LEFT JOIN (select id,isdelete,ProfileName from Hre_Profile) hp3 ON hp3.id = hpmf.ProfileSingID AND hp3.""IsDelete"" IS null
LEFT JOIN #tblHistoryMedical Medical ON Medical.""ProfileID"" = hp.ID
LEFT JOIN (select ID, IsDelete, DiseaseName from ""Med_Disease"" ) Med_D ON Med_D.""ID"" = Medical.""DiseaseID"" AND Med_D.IsDelete IS NULL
left join (select id, isdelete, ProvinceName, CountryID from Cat_Province ) cpow ON cpow.ID = cwp.ProvinceID and cpow.""IsDelete"" is null
left join (select id, isdelete, CountryName from Cat_Country ) ctow ON ctow.ID = cpow.CountryID and ctow.""IsDelete"" is null
LEFT JOIN #tblStopWorking2 thsw ON thsw.ProfileID = hp.Id AND thsw.""IsDelete"" IS NULL
 LEFT JOIN (select id, isdelete, ObjectID, ProfileID, DeclineReason from #tblEvaluationDocumentdecline) hedd ON hedd.ProfileID = hp.ID and hedd.IsDelete is null
 left join (select id, isdelete, PositionName, PositionEngName, Code , ParentPositionID from Cat_Position ) cp6 ON cp6.Id = cp.ParentPositionID AND cp6.""IsDelete"" IS NULL
 left join Cat_EmpGroupFirstDetail cefgd on cefgd.ID = hw.EmployeeGroupFirstDetailID and cefgd.IsDelete is null
left join Cat_EmpGroupSecondDetail cesgd on cesgd.ID = hw.EmployeeGroupSecondDetailID and cesgd.IsDelete is null
LEFT JOIN (select id, CompanyID, ProfileID, NoDecision from Cat_DelegateCompany where IsDelete is null ) Delegate ON Delegate.id = hpmf.DelegateCompanyID
LEFT JOIN (select id, CodeEmp, ProfileName from Hre_Profile where IsDelete is null ) DelegatePro on DelegatePro.id = Delegate.ProfileID
 Left JOIN #tblEnumStatusAccount365 Account365 ON Account365.EnumKey = hpmf.Isaccount365
LEFT JOIN (select ProvinceName,Code , id, IsDelete from ""Cat_Province"" ) cpss1 ON cpss1.ID = hp.PlaceOfBirthID AND cpss1.IsDelete is null
LEFT JOIN (select ProvinceName,Code , id, IsDelete from ""Cat_Province"" ) cpss2 ON cpss2.ID = hp.PlaceOfIssueID AND cpss2.IsDelete is null
LEFT JOIN (select ProvinceName,Code , id, IsDelete from ""Cat_Province"" ) cpProvince ON cpProvince.id = hpmf.OriginProvinceID AND cpProvince.""IsDelete"" IS NULL
LEFT JOIN (select DistrictName,Code , id, IsDelete from ""Cat_District"" ) cpDistrict ON cpDistrict.id = hpmf.OriginDistrictID AND cpDistrict.""IsDelete"" IS NULL
LEFT JOIN (select VillageName,Code , id, IsDelete from ""Cat_Village"" ) cpVillage ON cpVillage.id=hpmf.OriginVillageID AND cpVillage.""IsDelete"" IS NULL
LEFT JOIN (select IDCardIssuePlaceName,Code , id, IsDelete from ""Cat_IDCardIssuePlace"" ) CardPlace ON CardPlace.id=hpmf.IDCardIssuePlaceID AND CardPlace.""IsDelete"" IS NULL
LEFT JOIN (select PassportIssuePlaceName,Code , id, IsDelete from ""Cat_PassportIssuePlace"" ) PassPlace ON PassPlace.id=hpmf.PassportPlaceNewID AND PassPlace.""IsDelete"" IS NULL
LEFT JOIN (SELECT CountryName, id, IsDelete, Nationality from ""Cat_Country"" ) newNationality2ID ON newNationality2ID.ID = hpmf.Nationality2ID AND newNationality2ID.""IsDelete""  IS NULL
LEFT JOIN (select ProfileName,id, IsDelete, CodeEmp from ""Hre_Profile"" ) profileroot on profileroot.ID=hp.RootProfileID AND profileroot.""IsDelete"" IS NULL
 LEFT JOIN (select * from Cat_OrgUnit_Translate where IsDelete is null) cout on cout.OriginID = cou.ID
";

            var listDataTableOrigin = new List<FactoryCacheTable.DataTableOrigin>();
            var dataRetun = new Dictionary<string, FactoryCacheTable.DataTableLink>();
            string[] tableNames2 = FactoryCacheTable.GenerateTableLinks(input, "GET_CONTRACTAPK", "Hre_Profile.hp", out listDataTableOrigin, out dataRetun);


            string inputColumn = @"
cout.E_COMPANY as E_COMPANY_EN,
cout.E_BRANCH as E_BRANCH_EN,
cout.E_UNIT as E_UNIT_EN,
cout.E_DIVISION as E_DIVISION_EN,
cout.E_DEPARTMENT as E_DEPARTMENT_EN,
cout.E_TEAM as E_TEAM_EN,
cout.E_SECTION as E_SECTION_EN,
cout.E_OU_L8 as E_OU_L8_EN,
cout.E_OU_L9 as E_OU_L9_EN,
cout.E_OU_L10 AS E_OU_L10_EN,
cout.E_OU_L11 AS E_OU_L11_EN,
cout.E_OU_L12 AS E_OU_L12_EN,
hp.MiddleName,
cet.Notes as CetNotes,
hp.""ProfileName"",
hp.""CodeEmp"",
evaDocStatus.EnumTranslate AS ""EvaDocumentStatusView"",
cas.SourceAdsName as RecruitmentSource,
 esins.EnumTranslate AS ""SocialInsNoStatusView"",
Med_D.DiseaseName as ""MedProfileDiseaseName"",
Medical.DateIn as ""MedProfileDateIn"",
Medical.DiseasesInPast as ""MedProfileDiseasesInPast"",
Medical.Description as ""MedProfileDescription"",
tah.DateReceived as AHDateReceived,
teh.EnumTranslate AS HealthCheckGroupName,
tah.BloodType as AnnualHealthBloodType,
tah.Note as AnnualHealthNote,
mtrh1.TypeResultHealthName as AHTypeResultHealthName,
tah.Result as AHResult,
tah.Diagnostic,
tah.MedicalAdvice,
mm.MedicineName as IRMedicineName, 
mir.InjectionNo,
mir.InjectionDate,
mir.InjectionPlace,
mir.HealthNote1,
hpmf.TAddressEN,
hpmf.PAddressEN,
hp.ReplaceForReasonID,
hp.ReplaceForProfileID,
hp.ProfileName as ReplaceForProfileName,
vehi.NameEntityName as ProfileAllVehicle,
csc.""SalaryClassName"" as ProfileAllSalaryClassName,
hpmf.Talent as ProfileAllTalent,
hpmf.ClasifiedReason,
cne.NameEntityName AS ProfileAllEducationLevelName,
hp.DateSenior,
esv.EnumTranslate AS PerformanceEvaSendMailSatus,
hp.TaskLongTerm,
tep.DateEffectProbation,
tep.TotalMarkProbation,
tep.LevelNameProbation,
tep.TemplateNameProbation,
tep.PerformanceTypeNameProbation,
tep.Status,
tep.TotalMarkProbation,
ssv.EnumTranslate AS ""StatusSynViewNew"",
hp.ReasonDeny,
caf.FundName,
hhic.HealthTreatmentPlaceCode AS HealthTreatmentPlaceCodeView,
hhkk.HealthTreatmentName AS HealthTreatmentNameView,
hhic.ProvinceHospital AS ProvinceHospitalView,
CurContract.DateStart as ""CurrentDateStart"",
CurContract.DateEnd as ""CurrentDateEnd"",
CurContract.ContractTypeName as ""CurrentContractTypeName"",
FirstContract.ContractTypeName as ""FirstContractTypeName"",
CurContract.DateExtend as DateExtend,
hp.ID,
hp.""FirstName"",
hp.""NameFamily"",
hp.""NameEnglish"",
hp.""ImagePath"",
hp.""CodeEmpClient"",
hp.""CodeTax"",
hp.""CodeAttendance"",
hp.""StatusSyn"",
hp.""DateHire"",
hp.""DateEndProbation"",
hp.IsNotEnoughHealth,
hp.""OrgStructureID"",
hp.""SocialInsNo"",
hp.""SocialInsOldNo"",
hp.""IsRegisterSocialIns"",
hp.""IsRegisterHealthIns"",
hp.""IsRegisterUnEmploymentIns"",
hp.""SocialInsIssuePlace"",
hp.""SocialInsIssueDate"",
hp.""SocialInsDateReg"",
hp.""ReceiveSocialIns"",
hp.""ReceiveSocialInsDate"",
hp.""SocialInsSubmitBookStatus"",
hp.""SocialInsSubmitBookDate"",
hp.""HealthTreatmentPlace"",
hp.""HealthTreatmentPlaceCode"",
hp.""HealthInsNo"",
hp.""HealthInsIssueDate"",
hp.""HealthInsExpiredDate"",
hp.""UnEmpInsDateReg"",
hp.""UnEmpInsCountMonthOld"",
hp.""PositionID"",
hp.""DateOfEffect"",
hp.""CostCentreID"",
hp.""WorkingPlace"",
hp.""SalaryClassID"",
csc.""SalaryClassName"",
hp.""Gender"",
hp.""DayOfBirth"",
hp.""MonthOfBirth"",
hp.""YearOfBirth"",

hp.""PlaceOfBirth"",
hp.""NationalityID"",
hp.""EthnicID"",
hp.""ReligionID"",
hp.""BloodType"",
hp.""Height"",
hp.""Weight"",
hp.""IDNo"",
hp.""E_IDNo"",
hp.""IDDateOfIssue"",
hp.""IDPlaceOfIssue"",
hp.""PassportNo"",
hp.""E_PassportNo"",
hp.""PassportDateOfExpiry"",
hp.""PassportDateOfIssue"",
hp.""PassportPlaceOfIssue"",
hp.""Email"",
hp.""Cellphone"",
hp.""E_Cellphone"",
hp.""HomePhone"",
hp.""BusinessPhone"",
hp.""JobTitleID"",
hp.""SupervisorID"",
hp.""HighSupervisorID"",
hp.""DateApplyAttendanceCode"",
hp.""DateOfIssuedTaxCode"",
hp.""Email2"",
hp.""ReceiveHealthIns"",
hp.""ReceiveHealthInsDate"",
hp.""ResonBackList"",
hp.""ResignNo"",
hp.""IsBlackList"",
hp.""DateQuit"",
hp.""Notes"",
hp.""UserCreate"",
hp.""UserUpdate"",
hp.""DateCreate"",
hp.""DateUpdate"",
hp.""UserLockID"",
hp.""DateLock"",
hp.""IsDelete"",
hp.""ServerUpdate"",
hp.""ServerCreate"",
hp.""IPCreate"",
cp.""PositionName"",
cp.Code AS ""PositionCode"",
cc.""CostCentreName"",
cc.Code as CostCentreCode,
ceg.""EthnicGroupName"",
cr.""ReligionName"",
cj.""JobTitleName"",
cj.""JobTitleNameEn"",
hp.""EmpTypeID"",
cet.""EmployeeTypeName"",
co.""OrgStructureName"",
co.""Code"" as ""OrgStructureCode"",
co.OrgStructureName AS OrgStructureFullName,
cc1.""CountryName"" as ""NationalityName"",
hp.""EducationLevelID"" AS ""EducationLevelID"",
cne.""NameEntityName"" AS ""EducationLevelName"",
cne10.NameEntityName AS NationalityGroup,
crr.""ResignReasonName"",
hp.""WorkPlaceID"",
cwp.""WorkPlaceName"",
cwp.AnotherName as ""WorkPlaceOtherName"",
cet.AnotherName as ""EmployeeTypeOtherName"",
cne.OtherName as ""EducationLevelOtherName"",
cne7.OtherName as ""TypeOfTransferOtherName"",
cne7.NameEntityName as MovementTypeNameNew,
cne7.NameEntityName as TypeOfTransferName,
hp.""Origin"",
hp.""GraduatedLevelID"",
cne1.""NameEntityName"" AS ""GraduatedLevelName"",
hp.""MarriageStatus"",
hp.""AddressEmergency"",
hp.""LaborType"",
hp.""SikillLevel"",
hp.""LocationCode"",
hp.""TCountryID"",
cc2.""CountryName"" AS ""TCountryName"",
hp.""PCountryID"",
cc3.""CountryName"" AS ""PCountryName"",
hp.""TProvinceID"",
cp1.""ProvinceName"" AS ""TProvinceName"",
hp.""PProvinceID"",
cp2.""ProvinceName"" AS ""PProvinceName"",
hp.""TDistrictID"",
cd.""DistrictName"" AS ""TDistrictName"",
hp.""PDistrictID"",
cd1.""DistrictName"" AS ""PDistrictName"",
cpg.""PayrollGroupName"",
hp.""PayrollGroupID"",
hp.""ShopID"",
cs.""ShopName"",
cs.Code as ""ShopCode"",
hp.""StopWorkType"",
hp.""TypeSuspense"",
cct.""ContractTypeName"",
hp.""ContractTypeID"",
hp.""SocialInsNoStatus"",
csg.""ShopGroupName"",
rc.""CodeCandidate"",
hp.""DateOfBirth"",
cat.""AbilityTitleVNI"",
cou.E_COMPANY,
cou.E_BRANCH,
cou.E_UNIT,
cou.E_DIVISION,
cou.E_DEPARTMENT,
cou.E_TEAM,
cou.E_SECTION,
cou.E_COMPANY_E,
cou.E_BRANCH_E,
cou.E_UNIT_E,
cou.E_DIVISION_E,
cou.E_DEPARTMENT_E,
cou.E_TEAM_E,
cou.E_SECTION_E,
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
cou.E_COMPANY_E,
cou.E_BRANCH_E,
cou.E_UNIT_E,
cou.E_DIVISION_E,
cou.E_DEPARTMENT_E,
cou.E_TEAM_E,
cou.E_SECTION_E,
hp.candidateid,
hp.AddressSecondaryEmergency,
ccp.""CompanyName"",
ccp.""CompanyNameEN"",
ccp.""AddressVN"", 
ccp.""AddressEN"",
ccp.""Phone"",
ccp.""Image"",
ccp.ShortName,
ccp.ChairmanNameVN,
ccp.ChairmanNameEN,
ccp.ChairmanNationalityVN,
ccp.ChairmanNationalityEN,
ccp.ChairmanGender,
ccp.ChairmanJobtitileVN,
ccp.ChairmanJobtitleEN,
ccp.ChairmanPositionVN,
ccp.ChairmanPositionEN,
cjp.JobTitleProfessionalName,
cc4.""CountryName"" as IDNoCountryName,
cp4.""ProvinceName"" as IDNoProvinceName,
cd4.""DistrictName"" as IDNoDistrictName,
cv2.VillageName as ""IDNoAVillageName"",
cv.VillageName AS PVillageName,
co.OrderNumber,
coio.Info1,
coio.Info2,
coio.Info3,
coio.Info4,
coio.Info5,
coio.Info6,
coio.Info7,
coio.Info8,
coio.Info9,
coio.Info10,
coio.Info11,
coio.Info12,
coio.Info13,
coio.Info14,
coio.Info15,
coio.Info16,
cv1.VillageName as ""TVillageName"",
hp.CoatSize,
hp.PantSize,
hp.ShoeSize,
vehi.""NameEntityName"" AS ""VehicleName"",
hp.NameContactForEmergency,
hp.Relationship1,
hp.CellPhoneForEmergency,
csa1.SpecialAreaName,
hp.SpecialAreaID,
hp.SocialInsDateReg,
hp.HealthInsIssueDate,
hp.DateOfIssuedTaxCode,
hp.NameForSecondaryEmergency,
hp.CellPhoneForSecondaryEmergency,
hp.TaxDepartment,
hp.WorkPermitExpiredDate,
hp.IDNoAddress,
hp.ProvinceHospital,
hp.FileAttach as FileAttachment,
hp.TaskShortTerm,
csc.Code as ""SalaryClassCode"",
sppf1.""ProfileName"" AS ""SupervisorName"",
sppf1.NameEnglish AS SupervisorEngName,
cpspPf1.PositionName as ""SupervisorPositionName"",
cpspPf1.PositionEngName as ""SupervisorPositionEngName"",
spPf5.ProfileName as AnnunciatorName,
spPf2.""ProfileName"" AS ""HighSupervisorName"",
spPf4.ProfileName AS ""NextSupervisorName"",
spPf3.ProfileName AS ""MidSupervisorName"",
spPf2.NameEnglish AS ""HighSupervisorEngName"",
cpspPf2.PositionName as ""HighSupervisorPositionName"",
cpspPf2.PositionEngName as ""HighSupervisorPositionEngName"",
cp5.ProvinceName as ""ProvinceBirthName"",
cd5.DistrictName as ""DistrictBirthName"",
cv5.VillageName as ""VillageBirthName"",
cp5.Code as ProvinceBirthCertificateCode,
cd5.code as DistrictBirthCertificateCode,
cv5.Code as VillageBirthCertificateCode,
cp2.Code as PProvinceCode,
cd1.Code as PDistrictCode,
cv.Code as VillageCode,
cp1.Code as TProvinceCode,
cd.Code as TDistrictCode,
cv1.Code as TVillageCode,
cc1.Code as NationalityCode,
hpmf.TextLink,
hpmf.HouseholderFullName,
hpmf.HouseholdBookNo,
hpmf.PapersType,
hpmf.Telephone,
ccat.CostActivity,
hpmf.YoutubeChannel,hpmf.FacebookAccount,hpmf.SkypeAccount,hpmf.InstagramAccount,hpmf.""FavoriteColor"",
hpmf.""Habit"",hpmf.""Genitive"",hpmf.""TimeOfBirth"",hpmf.""LinkedInAccount"",
cp.""PositionEngName"",
sppf1.""ProfileName"" AS ""ProfileSupervisorName"",
sppf1.CodeEmp AS ""CodeSupervisor"",
cne6.""NameEntityName"" as ""EmployeeGroupName"",
cj.Code AS ""JobTitleCode"",
spPf1.""ProfileName"" AS ""SupervisorName"",
sppf1.CodeEmp AS ""SupervisorCode"",
hp.IDCard,
hp.E_IDCard,
hp.IDCardDateOfIssue,
hp.IDCardPlaceOfIssue,
hp.DatehireNew,
hpmf.IDCardDateOfExpiry,
hpmf.IsRetire,
hpmf.DateJoinCorporation,
egv.EnumTranslate as ""GenderView"",
egv1.EnumTranslate as HouseholderGenderView,
msv.EnumTranslate as ""MarriageStatusView"",
ltv.EnumTranslate as ""LaborTypeView"",
ssv.EnumTranslate AS ""StatusSynView"",
ssv.EnumTranslate AS StatusEmp,
etv.EnumTranslate AS ""EmploymentTypeView"",
hp.HealthStatus,
hp.TradeUnionistEnrolledDate,
hp.TradeUnionistEndDate,
ptu.EnumTranslate AS ""ProbationTimeUnitView"",
spPf2.CodeEmp AS ""HighSupervisorCode"",
hpmf.HouseHoldCode,
crt.RelativeTypeName,
AreaPostJobWork.NameEntityName as AreaPostJobWorkName,
AreaPostJob.NameEntityName as AreaPostJobName,
co.OrgStructureOtherName,
cwp.Code as WorkPlaceCode,
ptv.EnumTranslate AS PITCodeView,
ccp.Image as ""CompanyLogo"",
crt.RelativeTypeName AS HouseHolderRelativeTypeView,
cwp.Address AS WorkPlaceAddress,
hpmf.HouseHoldDateOfBirth,
hp.SocialInsNo AS SocialInsCode,
cr1.RegionName,
cr1.RegionName as Region,
spPf3.CodeEmp AS ""MidSupervisorCode"",
spPf1.NameFamily AS ""SupervisorNameFamily"",
spPf1.FirstName AS ""SupervisorFirstName"",
spPf1.MiddleName AS ""SupervisorMiddleName"",
spPf2.NameFamily AS ""HighSupervisorNameFamily"",
spPf2.FirstName AS ""HighSupervisorFirstName"",
spPf2.MiddleName AS ""HighSupervisorMiddleName"",
spPf3.NameFamily AS ""MidSupervisorNameFamily"",
spPf3.FirstName AS ""MidSupervisorFirstName"",
spPf3.MiddleName AS ""MidSupervisorMiddleName"",
cp.PositionNameInLaw,
cust.UnitName,
cust.UnitCode,
pcountry.CountryName as PCountryName,
pProvince.ProvinceName as PProvinceName,
pDistrict.DistrictName as PDistrictName,
pvillage.VillageName as PVillageName,
tcountry.CountryName as TCountryName,
tProvince.ProvinceName as TProvinceName,
tDistrict.DistrictName as TDistrictName,
tvillage.VillageName as TVillageName,
co.OrgStructureNameEN,
co.OrgStructureNameEN AS OrgStructureNameEng,
rc.StrTagID,
rc.CandidateNumber,
rc.DateExam,
cust.UnitName as ""UnitStructureName"",
hp.SocialInsNote as E_SocialInsNote,
hpmf.LastNameEN,
hpmf.FirstNameEN,
hpmf.MiddleNameEN,
hp.Fingercode,hp.IDCardCodeAtt,
StopWorkingForm.NameEntityName as StopWorkingFormName,
hp.Settlement,
hp.MonnthSettlement,
ac.CutOffDurationName,
hp.Email3,
hp.Email4,
hp.Relationship2,
name1.NameEntityName as DistributionChannelName,
name2.NameEntityName as MarketDomainName,
name3.NameEntityName as RegionMarketName,
name4.NameEntityName as MarketAreaName,
name5.NameEntityName as OriginalDistributorName,
hp.OtherDistributors,
ceg.OtherName as EthnicGroupOtherName,
cr.OtherName as ReligionOtherName,
cne2.OtherName as NationalityGroupOtherName,
cne1.OtherName as GraduatedLevelOtherName,
hpmf.IntroducePerson,
hpmf.IntroducerID,
Introducer.ProfileName as IntroducerName,
hsw.ApproveComment as CommentOfApprover,
hsw.ApproveComment1 as CommentOfFirstApprover,
hsw.ApproveComment2 as CommentOfSecondApprover,
hsw.ApproveComment3 as CommentOfThirdApprover,
hsw.ApproveComment4 as CommentOfLastApprover,
hpmf.ScannedCopyOfIDNo,
hpmf.ScannedCopyOfIDCard,
hpmf.ScannedCopyOfPassport,
hpmf.PlaceOfBirthRegistration,
hhic.FiveConsecutiveYearsFrom,
hpmf.CoefficientOfWorkmanship,
sf.EnumTranslate as SalaryPaidByTheFormOfView,
hpmf.GenderOfContact,
hpmf.GenderOfContact2,
egv2.EnumTranslate as GenderOfContactView,
egv3.EnumTranslate as GenderOfContact2View,
hp.ProbationTime,
hpmf.Attachment as JobDescriptionAttachment,
hp.IDCardCodeAtt,
hp.""DateSenior"",
hpmf.HouseHoldDayOfBirth,
hpmf.HouseHoldMonthOfBirth,
hpmf.HouseHoldYearOfBirth,
hpmf.WHDecisionNo,
hpmf.WHDecisionDate,
hpmf.WHTermTime,
hpmf.WHTermTimeUnit,
hpmf.WHDateNotice,
hpmf.WHRequirCondit,
hpmf.WHRromotion,
hpmf.WHDescription,
hpmf.WHNote,
hpmf.SendingEmpInfoMailTime,
hp.""PAddress"" as ProfilePAddress,
hp.""TAddress"" as ProfileTAddress,
tosn.""NameEntityName"" AS TypeOfStopName,
tosn.""NameEntityName"" AS ""TypeOfStop"",
tst.EnumTranslate as ""StopWorkTypeName"",
SocialInsPlace.ProvinceName as ""SocialInsPlaceName"",
ssf.AccountName as SalAccountName1,
ssf.AccountNo as SalAccountNo1,
cb1.BankName as SalBankName1,
cb1.BankCode as SalBankCode1,
ssf.AccountName2 as SalAccountName2,
ssf.AccountNo2 as SalAccountNo2,
cb2.BankName as SalBankName2,
cb2.BankCode as SalBankCode2,
ssf.AccountName3 as SalAccountName3,
ssf.AccountNo3 as SalAccountNo3,
cb3.BankName as SalBankName3,
cb3.BankCode as SalBankCode3,
ssf.DateExpired as SalDateExpired,
ssf.DateExpired2 as SalDateExpired2,
ssf.DateExpired3 as SalDateExpired3,
ssf.DateReleased as SalDateReleased,
ssf.DateReleased2 as SalDateReleased2,
ssf.DateReleased3 as SalDateReleased3,
csc.""SalaryClassName"" as PersonnelLevel,
cp6.PositionName as ParentPositionNameTitle,
egv.EnumTranslate as ""GenderViewNew"",
general.ProbationDay,
 general.RateProbation,
 Allowance1.UsualAllowanceName as UsualAllowanceName1General,
 Allowance2.UsualAllowanceName as UsualAllowanceName2General,
 Allowance3.UsualAllowanceName as UsualAllowanceName3General,
 Allowance4.UsualAllowanceName as UsualAllowanceName4General,
 Allowance5.UsualAllowanceName as UsualAllowanceName5General,
 Allowance6.UsualAllowanceName as UsualAllowanceName6General,
 Allowance7.UsualAllowanceName as UsualAllowanceName7General,
 Allowance8.UsualAllowanceName as UsualAllowanceName8General,
 Allowance9.UsualAllowanceName as UsualAllowanceName9General,
 Allowance10.UsualAllowanceName as UsualAllowanceName10General,
 Allowance11.UsualAllowanceName as UsualAllowanceName11General,
 Allowance12.UsualAllowanceName as UsualAllowanceName12General,
 Allowance13.UsualAllowanceName as UsualAllowanceName13General,
 Allowance14.UsualAllowanceName as UsualAllowanceName14General,
 Allowance15.UsualAllowanceName as UsualAllowanceName15General,
 Currency.CurrencyName,
 ptu.EnumTranslate AS ""ProbationTimeUnitView"",general.RateProbation as GeneralRateProbation,
ttu.EnumTranslate as WHTermTimeUnitView,
hp.DateComeBack,
hpmf.DateStartProbation,
hpmf.CodeMed,
 ccy.CompanyName as ProfileCompanyName,
 ca.AbilityTitleVNI as AbilityTileName,
tset.EnumTranslate as CardIssueTypeView,
profileroot.ProfileName as ProfileNameRoot,
profileroot.id as IDRoot,
profileroot.CodeEmp as CodeEmpRoot,
profileroot.CodeEmp as ""RootCodeEmp"",
hpmf.""IsCommunistPartyMember"" ,
hpmf.""CommunistPartyEnrolledDate"" ,
hpmf.""CommunistEffectiveDate"" ,
hpmf.""CommunistPartyCardNo"" ,
hpmf.""PoliticalLevelID"" ,
PoliticalLevel.PoliticalLevelName,
hpmf.""CommunistPartyPositionID"" ,
CommunistParty.CommunistPartyPositionName,
hpmf.""PositionEffectiveDate"" ,
hpmf.""UnionPartyReferer1"" ,
hpmf.""UnionPartyReferer2"" ,
hpmf.""IsYouthUnionist"" ,
hpmf.""YouthUnionEnrolledDate"" ,
hpmf.""AdmissionPlace"" ,
hpmf.""YouthUnionPositionID"" ,
YouthUnionPosition.YouthUnionPositionName,
hpmf.""UnionPositionEffectiveDate"",
hpmf.CommunistPartyReserveDate,
hpmf.CommunistActivityUnit,
hpmf.RetirementNoticeDate,
ctup.""TradeUnionistPositionName"",
hp.IsTradeUnionist,
hc.ContractNo,
hcg.GeneralContractNo,
hc.DateStart as DateContractStart,
hc.DateEnd as DateContractEnd,
hp2.ProfileName as EvaluatorName,
hp2.Email as EvaluatorEmail,
hsw.Attachment,
mtrh.TypeResultHealthName,
hpmf.TypeResultHealthID,
hpmf.ClasifiedReason,
hp.DormitoryID,
hp.HealthStatus,
hp.VehicleID,
cners.NameEntityName as ReplaceForReasonName,
crd.Code as StoredDocumentsCode,
SendMailStatusEmpNew.EnumTranslate as SendMailStatusEmpNewView,
hpmf.NoOfMonthSocialInsBefore,
hpmf.DateStartSocialIns,
hpmf.DateStartSocialInsActual,
hp.IsTradeUnionist,
hpmf.DateStartChallenge,
hpmf.DateEndChallenge,
name6.NameEntityName as DelegationReasonName,
hp.DateQuit,
ca1.AbilityTitleVNI as ExAbilityTitleVNI,
name7.NameEntityName as DelegationReasonName2,
hp3.ProfileName as ProfileSingName,
cpow.ProvinceName as ""ProvinceOfWork"",
cpow.ProvinceName as ProvinceOfWorkProfileWorking,
ctow.CountryName as ""CountryOfWork"",
hpmf.SocialInsDistrictID,
cd2.DistrictName as ""SocialInsDistrictName"",
hp.DateComeBack as ""DateComeBackViewProfile"",
hp.OtherReason,
 csc.SalaryClassName as SalaryClassNameView,
 thsw.Note as NoteSW,
 thsw.OtherReason as OtherReasonSW,
hpmf.IsNotDeductSeniority,
hpmf.UserSubmitID,
hpmf.SeniorityBonusDate,
hpmf.AnnualLeaveDate,
hpmf.AnnunciatorID,
hedd.DeclineReason,
 rc.OtherCertifications,
 rc.OtherFileAttached,
 hpif.LastWorkingDay as LastWorkingDayByMoreInfo,
 cefgd.EmpGroupFirstDetailName as EmployeeGroupFirstDetailName,
cesgd.EmpGroupSecondDetailName as EmployeeGroupSecondDetailName,
hpmf.AnnunciatorID,
hpmf.Isaccount365,
cs.Code as ShopCode,
Account365.EnumTranslate AS Isaccount365View,
cas.SourceAdsName,
cpss1.ProvinceName as PlaceOfBirthIDView,
cpProvince.ProvinceName as OriginProvinceIDView,
cpDistrict.DistrictName as OriginDistrictIDView,
cpVillage.VillageName as OriginVillageIDView,
cpss2.ProvinceName as PlaceOfIssueIDView,
PassPlace.PassportIssuePlaceName as PassportPlaceIDView,
CardPlace.IDCardIssuePlaceName as IDCardIssuePlaceIDView,
newNationality2ID.Nationality AS ""Nationality2Name"",
hpmf.DrivingLicenseNumber, hpmf.DrivingLicenseDate, hpmf.DrivingLicensePlace,
hpmf.YearSeniorityReserved,
hw.DecisionNo,
hpmf.OtherName as OtherNameEmployee,
csct.SalaryClassTypeName,
hpmf.BMI,
hpmf.DateRecord,
hpmf.AttachFile,
chdt.HDTJobTypeName as ProHDTJobTypeName,
 toxic.EnumTranslate as ToxicLevelView,
 hpmf.DelegateCompanyID as MoreInfoDelegateCompanyID,
DelegatePro.ProfileName AS MoreInfoDelegateCompanyName,
ccy.Code as CompanyCode,
Delegate.NoDecision as MoreInfoNoDecision
";

            FactoryCacheTable.GenerateSelectColumns(inputColumn, listDataTableOrigin, dataRetun, "profile");


            Console.ReadLine();
        }
    }
}
