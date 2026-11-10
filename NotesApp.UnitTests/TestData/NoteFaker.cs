using Bogus;
using NotesApp.Domain.Entities;

namespace NotesApp.UnitTests.TestData;

public static class NoteFaker
{
    public static List<Note> Generate(int count)
    {
        var faker = new Faker<Note>()
            .RuleFor(n => n.Id, f => Guid.NewGuid())
            .RuleFor(n => n.Title, f => f.Lorem.Sentence(4))
            .RuleFor(n => n.Content, f => f.Lorem.Paragraphs(3))
            .RuleFor(n => n.CreatedAt, f => f.Date.Recent(30))
            .RuleFor(n => n.UpdatedAt, (f, n) => n.CreatedAt);

        return faker.Generate(count);
    }
}