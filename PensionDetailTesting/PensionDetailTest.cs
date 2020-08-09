using MFRP_Pension_Detail;
using MFRP_Pension_Detail.Controllers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.IO;

namespace PensionDetailTesting
{
    public class PensionDetailTests
    {
        public List<PensionerDetail> pensionDetail = new List<PensionerDetail>
        {
            new PensionerDetail{
                            Name="sahil",
                            Dateofbirth=new DateTime(1998,03,01),
                            Pan="BCFPN1234E",
                            AadharNumber="111122223333",
                            SalaryEarned=40000,
                            Allowances=5000,
                            PensionType=PensionTypeValue.Family,
                            BankName="HDFC",
                            AccountNumber="123456789789",
                            BankType=BankType.Private
                           }
        };
        public static IConfiguration configuration;
        PensionerDetailController controllerObj;

        [SetUp]
        public void Setup()
        {
            configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();
            controllerObj = new PensionerDetailController(configuration);

            var pension = pensionDetail.AsQueryable();
            var mockset = new Mock<PensionerDetail>();
            mockset.As<IQueryable<PensionerDetail>>().Setup(m => m.Provider).Returns(pension.Provider);
            mockset.As<IQueryable<PensionerDetail>>().Setup(m => m.Expression).Returns(pension.Expression);
            mockset.As<IQueryable<PensionerDetail>>().Setup(m => m.ElementType).Returns(pension.ElementType);
            mockset.As<IQueryable<PensionerDetail>>().Setup(m => m.GetEnumerator()).Returns(pension.GetEnumerator());
        }

        [Test]
        public void PensionDetail_PensionerData_IsNotNull()
        {
            string aadhar = pensionDetail[0].AadharNumber;
            var result = controllerObj.PensionerDetailByAadhar(aadhar);
            var type1 = result;
            Assert.IsNotNull(type1);

        }
    }
}