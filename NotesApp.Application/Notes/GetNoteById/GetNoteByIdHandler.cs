using NotesApp.Application.Abstractions;
using NotesApp.Domain.Entities;

namespace NotesApp.Application.Notes.GetNoteById;

public sealed class GetNoteByIdHandler(INoteRepository repository)
{
    public Task<Note?> HandleAsync(GetNoteByIdRequest request, CancellationToken cancellationToken = default)
    {
        return repository.GetByIdAsync(request.Id, cancellationToken);
    }
}
