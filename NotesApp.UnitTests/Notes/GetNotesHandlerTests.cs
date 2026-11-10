using Moq;
using NotesApp.Application.Abstractions;
using NotesApp.Application.Notes.GetNotes;
using NotesApp.Domain.Entities;
using Xunit;
using NotesApp.UnitTests.TestData;

namespace NotesApp.UnitTests.Notes
{
    public class GetNotesHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ShouldReturnAllNotes()
        {
            //arrange
            var repositoryMock = new Mock<INoteRepository>();
            var notesList = NoteFaker.Generate(10);

            repositoryMock
            .Setup(x => x.GetAllAsync(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(notesList);

            var handler = new GetNotesHandler(repositoryMock.Object);

            //act
            var response = await handler.HandleAsync();

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

            repositoryMock.Verify(
                x => x.GetAllAsync(It.IsAny<CancellationToken>()),
                Times.Once);
        }

    }
}
