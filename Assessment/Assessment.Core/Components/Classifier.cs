using Assessment.Core.Enums;
using Assessment.Core.Interfaces;
using Assessment.Core.Results;

namespace Assessment.Core.Components
{
    public class Classifier : IDocumentClassifier
    {
        public DataClassificationResult AssignClassification(string text)
        {
            var keywords = KeywordDictionary.CreateDictionary();

            var priority = new[]
            {
                DataClassification.SensitivePersonalData,
                DataClassification.PersonalData,
                DataClassification.InternalData,
                DataClassification.PublicData
            };

            foreach (var classification in priority)
            {
                foreach (var keyword in keywords[classification]) 
                { 
                    if(text.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    {
                        return new DataClassificationResult
                            (
                            classification, 
                            $"Keyword '{keyword}' detected."
                            );
                    }
                }
            }

            return new DataClassificationResult
                (
                DataClassification.PublicData, 
                "No classification keywords detected"
                );
        }
    }
}
