namespace NotesApp.Application.Notes.UpdateNote;

public sealed record UpdateNoteRequest(int Id, string Title, string Content);
