using System;

namespace AdminPureGold.Domain.Models.WeichertCore
{
    public class WeichertOneUser
    {
        public Int32 PersonNumber { get; set; }
        public String Login { get; set; }
        public String Active { get; set; }
        public DateTime? FirstAccess { get; set; }
        public DateTime? LastAccess { get; set; }
        public bool? LockedOut { get; set; }
        public DateTime? LockedOutDate { get; set; }
        public Int32? FailedLoginCount { get; set; }
        public DateTime? PasswordChangeDate	 { get; set; }
        public String CrUser { get; set; }
        public DateTime? CrDate { get; set; }
        public String ChUser { get; set; }
        public DateTime? ChDate { get; set; }        
    }
}
