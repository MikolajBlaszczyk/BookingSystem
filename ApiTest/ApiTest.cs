using Autofac.Extras.Moq;
using DataAccess;
using DeskBookingSystemAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xunit;

namespace ApiTest
{
    public class ApiTest
    {
        [Fact]
        public async void TestEveryone()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IDataProcessor>()
                    .Setup(x => x.GetAllDesks())
                    .Returns(Task.FromResult(GetSampleDesks()));

                var apiController = mock.Create<EveryoneController>();

                var expected = GetSampleDesks();
                var actual = await apiController.GetDesks();
                Assert.True(actual != "");
                Assert.Equal(expected, actual);
            }
        }


        public string GetSampleDesks()
        {
            string res = @"{ ""ID"":3""IsReserved"":false,""UserID"":0,""Position"":""Œrodek"",""Monitors"":2,""LocationID"":1}";
            return res;
        }

        public string GetSampleReservations()
        {
            string res = @"[{ 
                        ""ID"":3""IsReserved"":true,""Position"":""Œrodek"",""Monitors"":2,""LocationID"":1,""Start"":""29.05.2022"",""30.05.2022"",""resID"":19}
                        ]";
            return res;
        }
    }
}