using Bogus;
using Moq;
using NotesApp.Application.Abstractions;
using NotesApp.Application.Notes.DeleteNote;
using NotesApp.Domain.Entities;
using Xunit;

namespace NotesApp.UnitTests.Notes
{
    public class DeleteNoteHandlerTests
    {
        private readonly Mock<INoteRepository> _noteRepositoryMock;
        private readonly DeleteNoteHandler _handler;
        private readonly Faker _faker;

        public DeleteNoteHandlerTests()
        {
            _noteRepositoryMock = new Mock<INoteRepository>();

            _handler = new DeleteNoteHandler(
                _noteRepositoryMock.Object
            );
            _faker = new Faker();
        }


        [Fact]
        public async Task HandleAsync_ShouldReturnTrue()
        {
            // arrange
            var guidToDelete = Guid.NewGuid();
            var noteToDelete = new Note()
            {
                Id = guidToDelete,
                Title = _faker.Lorem.Sentence(10),
                Content = _faker.Lorem.Lines(5),
                CreatedAt = new DateTime()
            };

            _noteRepositoryMock
            .Setup(x => x.GetByIdAsync(
                guidToDelete,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(noteToDelete);

            var request = new DeleteNoteRequest(guidToDelete);
            //act
            var response = await _handler.HandleAsync(request);

            //assert
            Assert.True(response);
            _noteRepositoryMock.Verify(x => x.DeleteAsync(
                noteToDelete,
                It.IsAny<CancellationToken>()),
            Times.Once);

        }

        [Fact]
        public async Task HandleAsync_ShouldReturnFalse()
        {
            // arrange
            var guidToDelete = Guid.NewGuid();

            _noteRepositoryMock
            .Setup(x => x.GetByIdAsync(
                guidToDelete,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Note?)null);

            var request = new DeleteNoteRequest(guidToDelete);
            //act
            var response = await _handler.HandleAsync(request);

            //assert
            Assert.False(response);
            _noteRepositoryMock.Verify(x => x.DeleteAsync(
                It.IsAny<Note>(),
                It.IsAny<CancellationToken>()),
            Times.Never);

        }

        [Fact]
        public async Task HandleAsync_ShouldThrowException_WhenGetByIdFails()
        {
            // arrange
            var guidToDelete = Guid.NewGuid();
            _noteRepositoryMock
            .Setup(x => x.GetByIdAsync(
                guidToDelete,
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

            var request = new DeleteNoteRequest(guidToDelete);

            //act
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.HandleAsync(request));

            //assert
            Assert.Equal("Database error", exception.Message);
            _noteRepositoryMock.Verify(x => x.DeleteAsync(
                It.IsAny<Note>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
        }

        [Fact]
        public async Task HandleAsync_ShouldThrowException_WhenDeleteFails()
        {
            // arrange
            var guidToDelete = Guid.NewGuid();
            var noteToDelete = new Note()
            {
                Id = guidToDelete,
                Title = _faker.Lorem.Sentence(10),
                Content = _faker.Lorem.Lines(5),
                CreatedAt = new DateTime()
            };

            _noteRepositoryMock
            .Setup(x => x.GetByIdAsync(
                guidToDelete,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(noteToDelete);

            _noteRepositoryMock
            .Setup(x => x.DeleteAsync(
                It.IsAny<Note>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

            var request = new DeleteNoteRequest(guidToDelete);

            //act
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.HandleAsync(request));

            //assert
            Assert.Equal("Database error", exception.Message);
            _noteRepositoryMock.Verify(x => x.DeleteAsync(
                It.IsAny<Note>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        }

    }
}
