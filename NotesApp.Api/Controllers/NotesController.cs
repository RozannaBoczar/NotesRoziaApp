using Microsoft.AspNetCore.Mvc;
using NotesApp.Application.Notes.CreateNote;
using NotesApp.Application.Notes.DeleteNote;
using NotesApp.Application.Notes.GetNoteById;
using NotesApp.Application.Notes.GetNotes;
using NotesApp.Application.Notes.UpdateNote;

namespace NotesApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly CreateNoteHandler _createHandler;
        private readonly GetNotesHandler _getAllHandler;
        private readonly GetNoteByIdHandler _getByIdHandler;
        private readonly UpdateNoteHandler _updateHandler;
        private readonly DeleteNoteHandler _deleteHandler;

        public NotesController(
            CreateNoteHandler createHandler,
            GetNotesHandler getAllHandler,
            GetNoteByIdHandler getByIdHandler,
            UpdateNoteHandler updateHandler,
            DeleteNoteHandler deleteHandler)
        {
            _createHandler = createHandler;
            _getAllHandler = getAllHandler;
            _getByIdHandler = getByIdHandler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var notes = await _getAllHandler.HandleAsync(cancellationToken);
            return Ok(notes);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var note = await _getByIdHandler.HandleAsync(
                new GetNoteByIdRequest(id), cancellationToken);

            if (note is null)
                return NotFound();

            return Ok(note);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateNoteRequest request,
            CancellationToken cancellationToken)
        {
            var note = await _createHandler.HandleAsync(request, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = note.Id },
                note);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            UpdateNoteRequest request,
            CancellationToken cancellationToken)
        {
            var updated = await _updateHandler.HandleAsync(
                new UpdateNoteRequest(id, request.Title, request.Content),
                cancellationToken);

            if (updated is null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken cancellationToken)
        {
            var deleted = await _deleteHandler.HandleAsync(
                new DeleteNoteRequest(id), cancellationToken);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
