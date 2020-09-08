using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApplication1.Controllers;
using WebApplication1.Models;
using PatientLibrary;
using PatientRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace UnitTestProject
{

    [TestClass]
    public class UnitTest1
    {
     
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            return config;
        }

        public static PatientDBContext CreateDBContext()
        {
            var config = InitConfiguration();
            var conn = config.GetConnectionString("myDb");
            var optionBuilder = new DbContextOptionsBuilder<PatientDBContext>();
            optionBuilder.UseSqlServer(conn);
            var _patientDBContext = new PatientDBContext(optionBuilder.Options);
            return _patientDBContext;

        }
        [TestMethod]
        public void TestCustomerAdd()
        {
            Customer objCust = new Customer();
            Mock<IDataLayer> mockDataLayer = new Mock<IDataLayer>();
            //mockDataLayer.Setup(x => x.Add()).Callback(() =>
            //{
            //    var x = 100;
            //});

            mockDataLayer.Setup(x => x.Add(It.IsAny<string>())).Returns(true);
            objCust.objDataDB = mockDataLayer.Object;
            objCust.Name = "Guru";
            var isadded = objCust.Add();
            Assert.AreEqual(true, isadded);
        }

        [TestMethod]
        public void TestApiPost()
        {
            Mock<HttpContext> httpMock = new Mock<HttpContext>();
            Mock<ISession> sessionmock = new Mock<ISession>();
            sessionmock.Setup(x => x.Set(It.IsAny<string>(),It.IsAny<byte[]>()))
                .Callback<string,byte[]>((s,b) =>{
                    var x = 0;
            });

            httpMock.Setup(s => s.Session).
                Returns(sessionmock.Object);

            List<PatientDTO> pdto = new List<PatientDTO>();

            pdto.Add(new PatientDTO() { name = "Guru", address = "Sion" }); 
            pdto.Add(new PatientDTO() { name = "Shivjee", address = "Mulund" });


            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            var mapper = mockMapper.CreateMapper();

            PatientApi pobApi = new PatientApi(CreateDBContext(), mapper);

            pobApi.ControllerContext.HttpContext = httpMock.Object;
            var ar = pobApi.Post(pdto);
            var or = ar as OkObjectResult;
            Assert.AreEqual(200, or.StatusCode);
            Assert.AreNotEqual(0, ((List<PatientAddress>)or.Value).Count);

        }
        [TestMethod]
        public void TestCustomerDal()
        {
           
        }
    }

   

    public class Customer
    {
        public string Name { set; get; }
        public IDataLayer objDataDB = new DataLayerDB();

        public bool Add()
        {
            if(Name.Length == 0) { return false; }
            if (objDataDB.Add(""))
            {
                Name = "Welcome " + Name;
            }
            return true;
        }
    }

    public interface IDataLayer
    {
        public bool Add(string conn);
    }
    public class DataLayerDB : IDataLayer
    {
        public bool Add(string conn)
        {
            throw new Exception("Sql Server is Gone");
        }
    }
}
