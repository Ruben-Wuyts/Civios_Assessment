namespace Assessment.Core.Entities
{
    public class DocumentMetadata
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }

        public DocumentMetadata(string author, string title, DateTime creationDate, string description)
        {
             Author = author;
            Title = title;
            CreationDate = creationDate;
            Description = description;
        }

    }
}
