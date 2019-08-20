using System.Collections.Generic;

namespace SparkPostNet.Models {
    public class Transmission {
        public string CampaignID { get; set; }
        public Content Content { get; set; }

        public IEnumerable<Recipient> Recipients { get; set; } = null;
    }
}
