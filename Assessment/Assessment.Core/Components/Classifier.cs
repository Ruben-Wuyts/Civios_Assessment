using Assessment.Core.Entities;
using Assessment.Core.Enums;
using Assessment.Core.Interfaces;
using Assessment.Core.Results;

namespace Assessment.Core.Components
{
    public class Classifier : IDocumentClassifier
    {
        public DataClassificationResult AssignClassification(string text, DocumentMetadata metadata)
        {
            var keywords = KeywordDictionary.CreateDictionary();
            
            var priority = new[]
            {
                DataClassification.SensitivePersonalData,
                DataClassification.PersonalData,
                DataClassification.InternalData,
                DataClassification.PublicData
            };

            DataClassification? textClassification = null;
            string? detectedKeyword = null;
            var textToClassify = $"{text} {metadata.Title} {metadata.Description}";

            foreach (var classification in priority)
            {
                foreach (var keyword in keywords[classification]) 
                { 
                    if(textToClassify.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    {
                        textClassification = classification;
                        detectedKeyword = keyword;
                        break;
                    }
                }

                if (textClassification.HasValue) 
                {
                    break;
                }
            }


            if (metadata.IntendedVisibility == IntendedVisibility.Internal && (textClassification == null || textClassification == DataClassification.PublicData))
            {
                return new DataClassificationResult(
                    DataClassification.InternalData, 
                    "Document metadata indicates internal visibility.");
            }

            if (textClassification.HasValue)
            {
                return new DataClassificationResult(
                    textClassification.Value,
                    $"Keyword '{detectedKeyword}' detected.");
            }


            return new DataClassificationResult
                (
                DataClassification.PublicData, 
                "No classification keywords detected"
                );
        }
    }
}
