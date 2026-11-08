using NotesApp.Application.Abstractions;
using NotesApp.Domain.Entities;

namespace NotesApp.Application.Notes.UpdateNote;

public sealed class UpdateNoteHandler(INoteRepository repository)
{
    public async Task<Note?> HandleAsync(UpdateNoteRequest request, CancellationToken cancellationToken = default)
    {
        var note = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (note is null)
        {
            return null;
        }

        note.Title = request.Title;
        note.Content = request.Content;
        note.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(note, cancellationToken);

        return note;
    }
}
