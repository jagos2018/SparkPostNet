using System;

namespace SparkPostNet.Exceptions {
    public class SparkPostNetException : Exception {
        public SparkPostNetException(string message) : base(message) {
        }

        public SparkPostNetException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
