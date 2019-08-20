namespace SparkPostNet.ResponseModels {
    public class TransmissionResponse {
        public string ID { get; set; }
        public int TotalRejectedRecipients { get; set; }
        public int TotalAcceptedRecipients { get; set; }
    }
}
