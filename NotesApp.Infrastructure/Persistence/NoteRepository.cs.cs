using Microsoft.EntityFrameworkCore;
using NotesApp.Application.Abstractions;
using NotesApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotesApp.Infrastructure.Persistence
{
    public class NoteRepository : INoteRepository
    {
        private readonly NotesDbContext _context;

        public NoteRepository(NotesDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Note note, CancellationToken cancellationToken = default)
        {
            await _context.Notes.AddAsync(note, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Note>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Notes
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Note?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Notes
                .FirstOrDefaultAsync(note => note.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(Note note, CancellationToken cancellationToken = default)
        {
            _context.Notes.Update(note);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Note note, CancellationToken cancellationToken = default)
        {
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
