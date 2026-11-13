using Bogus;
using Moq;
using NotesApp.Application.Abstractions;
using NotesApp.Application.Notes.GetNotes;
using NotesApp.Domain.Entities;
using NotesApp.UnitTests.TestData;
using Xunit;

namespace NotesApp.UnitTests.Notes
{
    public class GetNotesHandlerTests
    {
        private readonly Mock<INoteRepository> _noteRepositoryMock;
        private readonly GetNotesHandler _handler;
        private readonly Faker _faker;

        public GetNotesHandlerTests()
        {
            _noteRepositoryMock = new Mock<INoteRepository>();
            _handler = new GetNotesHandler(_noteRepositoryMock.Object);
            _faker = new Faker();

        }
        [Fact]
        public async Task HandleAsync_ShouldReturnAllNotes()
        {
            //arrange
            var notesList = NoteFaker.Generate(10);

            _noteRepositoryMock
            .Setup(x => x.GetAllAsync(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(notesList);

            //act
            var response = await _handler.HandleAsync();

            //assert
            Assert.NotNull(response);
            Assert.Equal(10, response.Count);
            Assert.All(response, note =>
            {
                Assert.NotEqual(Guid.Empty, note.Id);
                Assert.NotEmpty(note.Content);
                Assert.NotEmpty(note.Title);
                Assert.NotEqual(default, note.CreatedAt);
                Assert.NotEqual(default, note.UpdatedAt);
            });

            _noteRepositoryMock.Verify(
                x => x.GetAllAsync(It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnEmptyList_WhenNoNotes()
        {
            //arrange
            var notesList = new List<Note>();

            _noteRepositoryMock
            .Setup(x => x.GetAllAsync(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(notesList);

            //act
            var response = await _handler.HandleAsync();

            //assert
            Assert.NotNull(response);
            Assert.Empty(response);

            _noteRepositoryMock.Verify(
                x => x.GetAllAsync(It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnError()
        {
            //arrange
            _noteRepositoryMock
            .Setup(x => x.GetAllAsync(
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database connection failed"));

            //act
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.HandleAsync());

            //assert
            Assert.Equal("Database connection failed", exception.Message);

            _noteRepositoryMock.Verify(
                x => x.GetAllAsync(It.IsAny<CancellationToken>()),
                Times.Once);
        }

    }
}
