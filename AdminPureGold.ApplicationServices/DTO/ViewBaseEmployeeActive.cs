using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdminPureGold.ApplicationServices.DTO
{
    public class ViewBaseEmployeeActive
    {
        public Int32 Wpersno { get; set; }
        public Int32 Wrelateno { get; set; }
        public Int32? WrNetId { get; set; }
        public String EmpId { get; set; }
        public String SupervisorId { get; set; }
        public Boolean IsSupervisor { get; set; }
        public String Division { get; set; }
        public String Region { get; set; }
        public String Rvp { get; set; }
        public String OfficeNumber { get; set; }
        public Int32 OfficeId { get; set; }
        public String SSNlname { get; set; }
        public String SSNsuffix { get; set; }
        public String SSNfname { get; set; }
        public String SSNMname { get; set; }
        public String CommonFname { get; set; }
        public String JobTitle { get; set; }
        public String Company { get; set; }
        public String Department { get; set; }
        public String OfficeAddress { get; set; }
        public String OfficeCity { get; set; }
        public String OfficeState { get; set; }
        public String CityState { get; set; }
        public String Building { get; set; }
        public String Location { get; set; }
        public String DeskPhone { get; set; }        
        public String DeskFax { get; set; }        
        public String OfficePhone { get; set; }        
        public String OfficeFax { get; set; }
        public String WeichertEmail { get; set; }
        public String Birthday { get; set; }
        public String Anniversary { get; set; }
        public String OfficeZip { get; set; }                
   }
}
