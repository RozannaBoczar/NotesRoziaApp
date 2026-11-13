using Microsoft.EntityFrameworkCore;
using NotesApp.Application.Abstractions;
using NotesApp.Infrastructure.Persistence;
using NotesApp.Application.Notes.CreateNote;
using NotesApp.Application.Notes.GetNotes;
using NotesApp.Application.Notes.GetNoteById;
using NotesApp.Application.Notes.UpdateNote;
using NotesApp.Application.Notes.DeleteNote;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<CreateNoteHandler>();
builder.Services.AddScoped<GetNotesHandler>();
builder.Services.AddScoped<GetNoteByIdHandler>();
builder.Services.AddScoped<UpdateNoteHandler>();
builder.Services.AddScoped<DeleteNoteHandler>();

builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddDbContext<NotesDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
app.UseCors("AllowReactApp");
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();

public partial class Program
{
}
