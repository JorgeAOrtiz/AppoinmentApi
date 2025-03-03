using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using AppointmentApi.Controllers;
using Appointment.Core.Interfaces;
using Appointment.Domain.Models;
using System.Collections.Generic;

namespace AppointmentApi.Tests
{
    public class AppointmentsControllerTests
    {
        private readonly Mock<IAppointmentService> _mockAppointmentService;
        private readonly AppointmentsController _controller;

        public AppointmentsControllerTests()
        {
            _mockAppointmentService = new Mock<IAppointmentService>();
            _controller = new AppointmentsController(_mockAppointmentService.Object);
        }

        [Fact]
        public void GetAppointments_ReturnsOkResult_WithListOfAppointments()
        {
            // Arrange
            var appointments = new List<Appointment.Domain.Models.Appointment>
            {
                new Appointment.Domain.Models.Appointment { Id = 1, UserId = 1, Date = DateTime.Now, Description = "Test Appointment", State = AppointmentState.Pending }
            };
            _mockAppointmentService.Setup(service => service.ListAppointments()).Returns(appointments);

            // Act
            var result = _controller.GetAppointments();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Appointment.Domain.Models.Appointment>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public void GetAppointment_ReturnsNotFound_WhenAppointmentDoesNotExist()
        {
            // Arrange
            _mockAppointmentService.Setup(service => service.ListAppointments()).Returns(new List<Appointment.Domain.Models.Appointment>());

            // Act
            var result = _controller.GetAppointment(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void AddAppointment_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var appointment = new Appointment.Domain.Models.Appointment { Id = 1, UserId = 1, Date = DateTime.Now, Description = "Test Appointment", State = AppointmentState.Pending };

            // Act
            var result = _controller.AddAppointment(appointment);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetAppointment", createdAtActionResult.ActionName);
        }
    }
}