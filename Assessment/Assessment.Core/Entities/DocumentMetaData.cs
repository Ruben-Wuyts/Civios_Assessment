using Assessment.Core.Enums;

namespace Assessment.Core.Entities
{
    public class DocumentMetadata
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public IntendedVisibility? IntendedVisibility { get; set; }

        private DocumentMetadata()
        {
            //EF Core
        }

        public DocumentMetadata(string author, string title, DateTime creationDate, string description, IntendedVisibility intendedVisibility)
        {
            Author = author;
            Title = title;
            CreationDate = creationDate;
            Description = description;
            IntendedVisibility = intendedVisibility;
        }

    }
}
