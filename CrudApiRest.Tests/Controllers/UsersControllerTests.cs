using CrudApiRest.Application.Interfaces;
using CrudApiRest.Data.Models;
using CrudApiRest.Shared.Common.ViewModels;
using CrudApiRest.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;
using Xunit2.Should;

namespace CrudApiRest.Tests.Controllers
{
    public class UsersControllerTests
    {
        private readonly Mock<IUserService> _mockService;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            _mockService = new Mock<IUserService>();
            _controller = new UsersController(_mockService.Object);
        }

        [Fact]
        public void MustCallServiceOnGet()
        {
            _mockService.Setup(x => x.List(It.IsAny<PagingData>()))
                .Returns(new List<User>() { new User() });

            var result = _controller.Get(new PagingData());

            _mockService.Verify(x => x.List(It.IsAny<PagingData>()), Times.Once);
            _mockService.VerifyNoOtherCalls();

            Assert.IsType<List<User>>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(0)]
        public void MustCallServiceOnGetById(int id)
        {
            _mockService.Setup(x => x.FindById(It.Is<int>(s => s == id)))
                .Returns(new User());

            var result = _controller.GetById(id);

            _mockService.Verify(x => x.FindById(It.Is<int>(s => s == id)), Times.Once);
            _mockService.VerifyNoOtherCalls();

            Assert.IsType<User>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(0)]
        public void MustCallServiceOnGetByIdWhenIsNull(int id)
        {
            _mockService.Setup(x => x.FindById(It.Is<int>(s => s == id)));

            var result = _controller.GetById(id);

            _mockService.Verify(x => x.FindById(It.Is<int>(s => s == id)), Times.Once);
            _mockService.VerifyNoOtherCalls();

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void MustCallServiceOnPost()
        {
            _mockService.Setup(x => x.Insert(It.IsAny<User>()))
                .Returns(new User());

            var result = _controller.Post(new User());

            _mockService.Verify(x => x.Insert(It.IsAny<User>()), Times.Once);
            _mockService.VerifyNoOtherCalls();

            Assert.IsType<User>(result);
        }

        [Fact]
        public void MustCallServiceOnPostWhenIsNull()
        {
            _mockService.Setup(x => x.Insert(It.IsAny<User>()));

            var result = _controller.Post(new User());

            _mockService.Verify(x => x.Insert(It.IsAny<User>()), Times.Once);
            _mockService.VerifyNoOtherCalls();

            Assert.IsType<StatusCodeResult>(result);
            ((StatusCodeResult)result).StatusCode.ShouldBe((int)HttpStatusCode.InternalServerError);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(0)]
        public void MustCallServiceOnPut(int id)
        {
            _mockService.Setup(x => x.Update(It.Is<int>(s => s == id), It.IsAny<User>()))
                .Returns(new User());

            var result = _controller.Put(id, new User());

            _mockService.Verify(x => x.Update(It.Is<int>(s => s == id), It.IsAny<User>()), Times.Once);
            _mockService.VerifyNoOtherCalls();

            Assert.IsType<User>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(0)]
        public void MustCallServiceOnPutWhenIsNull(int id)
        {
            _mockService.Setup(x => x.Update(It.Is<int>(s => s == id), It.IsAny<User>()));

            var result = _controller.Put(id, new User());

            _mockService.Verify(x => x.Update(It.Is<int>(s => s == id), It.IsAny<User>()), Times.Once);
            _mockService.VerifyNoOtherCalls();

            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(0)]
        public void MustCallServiceOnPutPass(int id)
        {
            _mockService.Setup(x => x.UpdatePassword(It.Is<int>(s => s == id), It.IsAny<User>()))
                .Returns(1);

            var result = _controller.PutPass(id, new User());

            _mockService.Verify(x => x.UpdatePassword(It.Is<int>(s => s == id), It.IsAny<User>()), Times.Once);
            _mockService.VerifyNoOtherCalls();

            Assert.IsType<OkResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(0)]
        public void MustCallServiceOnPutPassWhenResultIsZero(int id)
        {
            _mockService.Setup(x => x.UpdatePassword(It.Is<int>(s => s == id), It.IsAny<User>()))
                .Returns(0);

            var result = _controller.PutPass(id, new User());

            _mockService.Verify(x => x.UpdatePassword(It.Is<int>(s => s == id), It.IsAny<User>()), Times.Once);
            _mockService.VerifyNoOtherCalls();

            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(0)]
        public void MustCallServiceOnDelete(int id)
        {
            _mockService.Setup(x => x.Delete(It.Is<int>(s => s == id)))
                .Returns(1);

            var result = _controller.Delete(id);

            _mockService.Verify(x => x.Delete(It.Is<int>(s => s == id)), Times.Once);
            _mockService.VerifyNoOtherCalls();

            Assert.IsType<OkResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(0)]
        public void MustCallServiceOnDeleteWhenResultIsZero(int id)
        {
            _mockService.Setup(x => x.Delete(It.Is<int>(s => s == id)))
                .Returns(0);

            var result = _controller.Delete(id);

            _mockService.Verify(x => x.Delete(It.Is<int>(s => s == id)), Times.Once);
            _mockService.VerifyNoOtherCalls();

            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        public void MustGetTokenOnGetJwtToken(int id)
        {
            _mockService.Setup(x => x.GetJwtToken(It.Is<int>(s => s == id)))
                .Returns(new JwtToken());

            var result = _controller.GetToken(id);

            _mockService.Verify(x => x.GetJwtToken(It.Is<int>(s => s == id)), Times.Once);
            _mockService.VerifyNoOtherCalls();

            Assert.IsType<JwtToken>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        public void MustReturnNullWhenFindByIdReturnNullOnGetJwtToken(int id)
        {
            _mockService.Setup(x => x.GetJwtToken(It.Is<int>(s => s == id)));

            var result = _controller.GetToken(id);

            _mockService.Verify(x => x.GetJwtToken(It.Is<int>(s => s == id)), Times.Once);
            _mockService.VerifyNoOtherCalls();

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
