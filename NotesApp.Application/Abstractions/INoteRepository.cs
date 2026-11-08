using NotesApp.Domain.Entities;

namespace NotesApp.Application.Abstractions;

public interface INoteRepository
{
    Task CreateAsync(Note note, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Note>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Note?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task UpdateAsync(Note note, CancellationToken cancellationToken = default);

    Task DeleteAsync(Note note, CancellationToken cancellationToken = default);
}
