using Assessment.Application.Services;
using Assessment.Core.Components;
using Assessment.Core.Interfaces;
using Assessment.Core.Validators;
using Assessment.Infrastructure.TextExtractors;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Dependency Injection
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IDocumentTextExtractor, PdfTextExtractor>();
builder.Services.AddScoped<IFileUploadValidator, FileUploadValidator>();
builder.Services.AddScoped<IExtractedTextValidator, ExtractedTextValidator>();
builder.Services.AddScoped<IDocumentClassifier, Classifier>();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger alleen tijdens development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
