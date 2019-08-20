using Microsoft.VisualStudio.TestTools.UnitTesting;
using SparkPostNet.Models;
using System.Collections.Generic;

namespace SparkPostNetIntegrationTests {
    [TestClass]
    public partial class Client {
        [TestMethod]
        public void SendTransmission_T1() {
            using (var client = new SparkPostNet.Client(Config.ApiKey)) {
                var result = client.SendTransmission(new Transmission() {
                    CampaignID = "Integration-Tests-2019",
                    Recipients = new List<Recipient>() {
                        new Recipient() {
                           Address = new Contact() {
                                Email = Config.TestEmail,
                                Name = "Jagos2018"
                            }
                        }
                    },
                    Content = new Content() {
                        From = new Contact() {
                            Email = "noreply@gunterweb.ca",
                            Name = "NoReply"
                        },
                        Html = "<p>Demo email<br><br><h3>Test</h3></p><p>Thanks!</p>",
                        Subject = "Integration Test Email - 2019"
                    }
                }).GetAwaiter().GetResult();

                Assert.IsTrue(result.ID.Length > 2);
                Assert.AreEqual(1, result.TotalAcceptedRecipients);
            }
        }
    }
}
