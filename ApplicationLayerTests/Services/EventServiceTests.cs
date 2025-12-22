using Application.Interfaces;
using Application.Providers;
using Application.Services;
using AutoMapper;
using Domain.DtoEntities;
using Domain.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayerTests.Services
{
    [TestClass]
    public class EventServiceTests
    {
        [TestMethod]
        public async Task AddAsync_Should_Map_And_Save_Event()
        {
            // Arrange
            var dto = new EventCreationDto();
            var mappedEvent = new Event();

            var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(m => m.Map<Event>(dto))
                .Returns(mappedEvent);

            var eventRepoMock = new Mock<IEventRepository>();
            eventRepoMock
                .Setup(r => r.AddAsync(It.IsAny<Event>()))
                .ReturnsAsync((Event e) => e);

            var service = new EventService(
                eventRepoMock.Object,
                Mock.Of<IParticipantsRepository>(),
                mapperMock.Object,
                Mock.Of<IImageUploader>()
            );

            // Act
            var result = await service.AddAsync(dto);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().NotBe(Guid.Empty);

            mapperMock.Verify(m => m.Map<Event>(dto), Times.Once);
            eventRepoMock.Verify(
                r => r.AddAsync(It.Is<Event>(e => e.Id != Guid.Empty)),
                Times.Once
            );
        }

        [TestMethod]
        public async Task UpdateAsync_Should_Throw_When_Event_Not_Found()
        {
            // Arrange
            var eventRepoMock = new Mock<IEventRepository>();
            eventRepoMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Event?)null);

            var service = new EventService(
                eventRepoMock.Object,
                Mock.Of<IParticipantsRepository>(),
                Mock.Of<IMapper>(),
                Mock.Of<IImageUploader>()
            );

            // Act
            Func<Task> act = () =>
                service.UpdateAsync(new EventCreationDto(), Guid.NewGuid());

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>();

            eventRepoMock.Verify(
                r => r.GetByIdAsync(It.IsAny<Guid>()),
                Times.Once
            );
        }
    }
}