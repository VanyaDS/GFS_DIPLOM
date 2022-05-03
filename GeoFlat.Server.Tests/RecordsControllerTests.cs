using AutoMapper;
using GeoFlat.Server.Automapper.ResponseModels;
using GeoFlat.Server.Controllers;
using GeoFlat.Server.Helpers;
using GeoFlat.Server.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace GeoFlat.Server.Tests
{
    public class RecordsControllerTests
    {
        RecordsController recordsController;

        private List<Models.Database.Entities.Record> recordsList;
        object converter;

        private Mock<ILogger<RecordsController>> loggerMock;
        private Mock<IUnitOfWork> unitOfWorkMock;
        private Mock<IMapper> mapperMock;
        private Mock<IMemoryCache> memoryCacheMock;


        public RecordsControllerTests()
        {
            loggerMock = new Mock<ILogger<RecordsController>>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            mapperMock = new Mock<IMapper>();
            memoryCacheMock = new Mock<IMemoryCache>();

            recordsController = new RecordsController
                                   (loggerMock.Object,
                                unitOfWorkMock.Object,
                                    mapperMock.Object,
                              memoryCacheMock.Object);

            recordsList = new List<Models.Database.Entities.Record>();
            converter = new CurrencyConverter
            {
                BYN = 1,
                USD = 2.8m
            };
        }

        [Fact]
        public void GetRecordsOkObjectResult()
        {
            //Arrange
            memoryCacheMock.Setup(mock => mock.TryGetValue(It.IsAny<object>(), out converter)).Returns(true);
            unitOfWorkMock.Setup(mock => mock.Records.All().Result).Returns(recordsList);
            mapperMock.Setup(mock => mock.Map<RecordResponse>
                            (It.IsAny<Models.Database.Entities.Record>()))
                            .Returns(It.IsAny<RecordResponse>());
            //Act
            var result = recordsController.GetRecords().Result;

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetRecordsMemoryCacheMockReturnsFalseObjectResult()
        {
            //Arrange
            memoryCacheMock.Setup(mock => mock.TryGetValue(It.IsAny<object>(), out converter)).Returns(false);

            //Act
            var result = recordsController.GetRecords().Result;
            //Assert
            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetRecordsNullRecordsThrowException()
        {
            //Arrange
            recordsList = null;

            memoryCacheMock.Setup(mock => mock.TryGetValue(It.IsAny<object>(), out converter)).Returns(true);
            unitOfWorkMock.Setup(mock => mock.Records.All().Result).Returns(recordsList);
            mapperMock.Setup(mock => mock.Map<RecordResponse>
                            (It.IsAny<Models.Database.Entities.Record>()))
                            .Returns(It.IsAny<RecordResponse>());
            //Act + Assert

            await Assert.ThrowsAsync<NullReferenceException>(() => recordsController.GetRecords());
        }
    }
}
