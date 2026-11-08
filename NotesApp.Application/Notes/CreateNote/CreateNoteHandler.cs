using NotesApp.Application.Abstractions;
using NotesApp.Domain.Entities;

namespace NotesApp.Application.Notes.CreateNote;

public sealed class CreateNoteHandler(INoteRepository repository)
{
    public Task<Note> HandleAsync(CreateNoteRequest request, CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        var note = new Note
        {
            Title = request.Title,
            Content = request.Content,
            CreatedAt = now,
            UpdatedAt = now
        };

        return repository.CreateAsync(note, cancellationToken);
    }
}
