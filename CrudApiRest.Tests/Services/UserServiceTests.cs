using CrudApiRest.Application.Services;
using CrudApiRest.Data.Interfaces;
using CrudApiRest.Data.Models;
using CrudApiRest.Shared.Common.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CrudApiRest.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockRepository;
        private readonly UserService _service;
        private readonly User _request;

        public UserServiceTests()
        {
            _mockRepository = new Mock<IUserRepository>();
            _service = new UserService(_mockRepository.Object);
            _request = new User()
            {
                Password = "Teste@1234"
            };
        }

        [Fact]
        public void MustCallRepositoryOnInsert()
        {
            _mockRepository.Setup(x => x.Insert(It.IsAny<User>()))
                .Returns(new User());

            var result = _service.Insert(_request);

            _mockRepository.Verify(x => x.Insert(It.IsAny<User>()), Times.Once);
            _mockRepository.VerifyNoOtherCalls();

            Assert.IsType<User>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        public void MustCallRepositoryOnUpdatePassword(int id)
        {
            _mockRepository.Setup(x => x.UpdatePassword(It.Is<int>(s => s == id), It.IsAny<User>()))
                .Returns(1);

            var result = _service.UpdatePassword(id, _request);

            _mockRepository.Verify(x => x.UpdatePassword(It.Is<int>(s => s == id), It.IsAny<User>()), Times.Once);
            _mockRepository.VerifyNoOtherCalls();

            Assert.IsType<int>(result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void MustCallRepositoryOnListWhenFilterIsNullOrEmpty(string filter)
        {
            var paging = new PagingData() { Filter = filter };
            _mockRepository.Setup(x => x.List(It.IsAny<PagingData>()))
                .Returns(new List<User>());

            var result = _service.List(paging);

            _mockRepository.Verify(x => x.List(It.IsAny<PagingData>()), Times.Once);
            _mockRepository.VerifyNoOtherCalls();

            Assert.IsType<List<User>>(result);
        }

        [Theory]
        [InlineData("1234")]
        [InlineData("teste")]
        public void MustCallRepositoryOnListWhenFilterIsntNullOrEmpty(string filter)
        {
            var paging = new PagingData() { Filter = filter };
            _mockRepository.Setup(x => x.ListByFilter(It.IsAny<PagingData>()))
                .Returns(new List<User>());

            var result = _service.List(paging);

            _mockRepository.Verify(x => x.ListByFilter(It.IsAny<PagingData>()), Times.Once);
            _mockRepository.VerifyNoOtherCalls();

            Assert.IsType<List<User>>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        public void MustCallRepositoryOnFindById(int id)
        {
            _mockRepository.Setup(x => x.FindById(It.Is<int>(s => s == id)))
                .Returns(new User());

            var result = _service.FindById(id);

            _mockRepository.Verify(x => x.FindById(It.Is<int>(s => s == id)), Times.Once);
            _mockRepository.VerifyNoOtherCalls();

            Assert.IsType<User>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        public void MustCallRepositoryOnUpdate(int id)
        {
            _mockRepository.Setup(x => x.Update(It.Is<int>(s => s == id), It.IsAny<User>()))
                .Returns(new User());

            var result = _service.Update(id, new User());

            _mockRepository.Verify(x => x.Update(It.Is<int>(s => s == id), It.IsAny<User>()), Times.Once);
            _mockRepository.VerifyNoOtherCalls();

            Assert.IsType<User>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        public void MustCallRepositoryOnDelete(int id)
        {
            _mockRepository.Setup(x => x.Delete(It.Is<int>(s => s == id)))
                .Returns(1);

            var result = _service.Delete(id);

            _mockRepository.Verify(x => x.Delete(It.Is<int>(s => s == id)), Times.Once);
            _mockRepository.VerifyNoOtherCalls();

            Assert.IsType<int>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        public void MustGetTokenOnGetJwtToken(int id)
        {
            _mockRepository.Setup(x => x.FindById(It.Is<int>(s => s == id)))
                .Returns(new User() { Id = id, Login = "teste", Name = "teste" });

            var result = _service.GetJwtToken(id);

            _mockRepository.Verify(x => x.FindById(It.Is<int>(s => s == id)), Times.Once);
            _mockRepository.VerifyNoOtherCalls();

            Assert.IsType<JwtToken>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        public void MustReturnNullWhenFindByIdReturnNullOnGetJwtToken(int id)
        {
            _mockRepository.Setup(x => x.FindById(It.Is<int>(s => s == id)));

            var result = _service.GetJwtToken(id);

            _mockRepository.Verify(x => x.FindById(It.Is<int>(s => s == id)), Times.Once);
            _mockRepository.VerifyNoOtherCalls();

            Assert.Null(result);
        }
    }
}
