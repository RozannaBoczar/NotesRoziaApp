using NotesApp.Application.Abstractions;

namespace NotesApp.Application.Notes.DeleteNote;

public sealed class DeleteNoteHandler(INoteRepository repository)
{
    public async Task<bool> HandleAsync(DeleteNoteRequest request, CancellationToken cancellationToken = default)
    {
        var note = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (note is null)
        {
            return false;
        }

        await repository.DeleteAsync(note, cancellationToken);
        return true;
    }
}
