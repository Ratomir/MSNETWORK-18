using Newtonsoft.Json;
using System.Collections.Generic;

namespace MSNetwork18.Model.WorldBank
{
    public class WorldBankModel
    {
        [JsonProperty("approvalfy")]
        public long Approvalfy { get; set; }

        [JsonProperty("board_approval_month")]
        public string BoardApprovalMonth { get; set; }

        [JsonProperty("boardapprovaldate")]
        public System.DateTimeOffset Boardapprovaldate { get; set; }

        [JsonProperty("borrower")]
        public string Borrower { get; set; }

        [JsonProperty("closingdate")]
        public System.DateTimeOffset Closingdate { get; set; }

        [JsonProperty("country_namecode")]
        public string CountryNamecode { get; set; }

        [JsonProperty("countrycode")]
        public string Countrycode { get; set; }

        [JsonProperty("countryname")]
        public string Countryname { get; set; }

        [JsonProperty("countryshortname")]
        public string Countryshortname { get; set; }

        [JsonProperty("docty")]
        public string Docty { get; set; }

        [JsonProperty("envassesmentcategorycode")]
        public string Envassesmentcategorycode { get; set; }

        [JsonProperty("grantamt")]
        public long Grantamt { get; set; }

        [JsonProperty("ibrdcommamt")]
        public long Ibrdcommamt { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("idacommamt")]
        public long Idacommamt { get; set; }

        [JsonProperty("impagency")]
        public string Impagency { get; set; }

        [JsonProperty("lendinginstr")]
        public string Lendinginstr { get; set; }

        [JsonProperty("lendinginstrtype")]
        public string Lendinginstrtype { get; set; }

        [JsonProperty("lendprojectcost")]
        public long Lendprojectcost { get; set; }

        [JsonProperty("majorsector_percent")]
        public List<MajorSectorPercentModel> MajorsectorPercent { get; set; }

        [JsonProperty("mjsector_namecode")]
        public List<NameCodeModel> MjsectorNamecode { get; set; }

        [JsonProperty("mjtheme")]
        public List<string> Mjtheme { get; set; }

        [JsonProperty("mjtheme_namecode")]
        public List<NameCodeModel> MjthemeNamecode { get; set; }

        [JsonProperty("mjthemecode")]
        public string Mjthemecode { get; set; }

        [JsonProperty("prodline")]
        public string Prodline { get; set; }

        [JsonProperty("prodlinetext")]
        public string Prodlinetext { get; set; }

        [JsonProperty("productlinetype")]
        public string Productlinetype { get; set; }

        [JsonProperty("project_abstract")]
        public ProjectAbstractModel ProjectAbstract { get; set; }

        [JsonProperty("project_name")]
        public string ProjectName { get; set; }

        [JsonProperty("projectdocs")]
        public List<ProjectDocModel> Projectdocs { get; set; }

        [JsonProperty("projectfinancialtype")]
        public string Projectfinancialtype { get; set; }

        [JsonProperty("projectstatusdisplay")]
        public string Projectstatusdisplay { get; set; }

        [JsonProperty("regionname")]
        public string Regionname { get; set; }

        [JsonProperty("sector")]
        public List<SectorModel> Sector { get; set; }

        [JsonProperty("sector1")]
        public MajorSectorPercentModel Sector1 { get; set; }

        [JsonProperty("sector2")]
        public MajorSectorPercentModel Sector2 { get; set; }

        [JsonProperty("sector3")]
        public MajorSectorPercentModel Sector3 { get; set; }

        [JsonProperty("sector4")]
        public MajorSectorPercentModel Sector4 { get; set; }

        [JsonProperty("sector_namecode")]
        public List<NameCodeModel> SectorNamecode { get; set; }

        [JsonProperty("sectorcode")]
        public string Sectorcode { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("supplementprojectflg")]
        public string Supplementprojectflg { get; set; }

        [JsonProperty("theme1")]
        public MajorSectorPercentModel Theme1 { get; set; }

        [JsonProperty("theme_namecode")]
        public List<NameCodeModel> ThemeNamecode { get; set; }

        [JsonProperty("themecode")]
        public string Themecode { get; set; }

        [JsonProperty("totalamt")]
        public long Totalamt { get; set; }

        [JsonProperty("totalcommamt")]
        public long Totalcommamt { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
