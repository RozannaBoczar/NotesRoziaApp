namespace NotesApp.Application.Notes.UpdateNote;

public sealed record UpdateNoteRequest(Guid Id, string Title, string Content);
