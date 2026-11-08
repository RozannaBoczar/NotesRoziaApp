using NotesApp.Application.Abstractions;
using NotesApp.Domain.Entities;

namespace NotesApp.Application.Notes.GetNotes;

public sealed class GetNotesHandler(INoteRepository repository)
{
    public Task<IReadOnlyList<Note>> HandleAsync(CancellationToken cancellationToken = default)
    {
        return repository.GetAllAsync(cancellationToken);
    }
}
