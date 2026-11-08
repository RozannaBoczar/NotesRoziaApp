using NotesApp.Domain.Entities;

namespace NotesApp.Application.Abstractions;

public interface INoteRepository
{
    Task<Note> CreateAsync(Note note, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Note>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Note?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task UpdateAsync(Note note, CancellationToken cancellationToken = default);

    Task DeleteAsync(Note note, CancellationToken cancellationToken = default);
}
