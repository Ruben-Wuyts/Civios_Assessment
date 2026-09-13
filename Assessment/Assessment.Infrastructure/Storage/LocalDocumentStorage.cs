using Assessment.Core.Enums;
using Assessment.Core.Interfaces;


namespace Assessment.Infrastructure.Storage
{
    public class LocalDocumentStorage : IDocumentStorage
    {
        //Een bestand mag alleen in Pending blijven zolang er een geldige Analyzed-record bestaat die nog op bevestiging wacht.
        public async Task<string> SaveTemporaryAsync(Stream stream, string fileName)
        {
            var directoryPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "LocalFiles",
                "Pending");

            Directory.CreateDirectory(directoryPath); //Als Directory bestaat gebeurt er niets

            var safeFileName = Path.GetFileName(fileName); //kleine securitymaatregel tegen path manipulation
            var uniqueFileName = $"{Guid.NewGuid()}_{safeFileName}"; //zeker zijn dat bestanden mekaar niet kunnen overschrijven vanwege zelfde naam

            var fullPath = Path.Combine(directoryPath, uniqueFileName);

            if (stream.CanSeek)
            {
                stream.Position = 0; //zeker zijn dat je aan het begin van de file start
            }

            await using var fileStream = new FileStream(
                fullPath,
                FileMode.CreateNew,
                FileAccess.Write);

            await stream.CopyToAsync(fileStream);

            return fullPath;
        }

        public Task<string> MoveToFinalStorageAsync(string currentPath, DataClassification classification)
        {
            var folderName = classification switch
            {
                DataClassification.PublicData => "Public",
                DataClassification.InternalData => "Internal",
                DataClassification.PersonalData => "Personal",
                DataClassification.SensitivePersonalData => "Sensitive",
                _ => throw new ArgumentOutOfRangeException(nameof(classification))
            };

            var targetDirectory = Path.Combine(
                Directory.GetCurrentDirectory(),
                "LocalFiles",
                folderName);

            Directory.CreateDirectory(targetDirectory);

            var fileName = Path.GetFileName(currentPath);

            if (!File.Exists(currentPath))
            {
                throw new FileNotFoundException("The document could not be found in temporary storage.", currentPath);
            }
            var finalPath = Path.Combine(targetDirectory, fileName);

            File.Move(currentPath, finalPath);

            return Task.FromResult(finalPath);


        }

    }
}
