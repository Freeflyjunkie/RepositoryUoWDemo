using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdminPureGold.Domain.Models.WeichertCore
{
    public class PersonToImage
    {
        public Int32 PersonToImageId { get; set; }
        public Int32 PersonNumber { get; set; }
        public String ImagePath { get; set; }
        public String ImageDescription { get; set; }
        public String DisplayStatus { get; set; }
        public String CrUser { get; set; }
        public DateTime CrDate { get; set; }
        public String ChUser { get; set; }
        public DateTime? ChDate { get; set; }
    }
}
