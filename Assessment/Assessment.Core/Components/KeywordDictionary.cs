using Assessment.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Core.Components
{
    public class KeywordDictionary: Dictionary<DataClassification, List<string>>
    {
        public static KeywordDictionary CreateDictionary()
        {
            KeywordDictionary outgoingDictionary = new KeywordDictionary
            {
                { DataClassification.PublicData, new List<string>() { "brochure", "openbaar", "folder" } },
                { DataClassification.InternalData, new List<string>() { "memo", "vergadering", "nota" } },
                { DataClassification.PersonalData, new List<string>() { "naam", "adres", "e-mailadres", "rijksregisternummer" } },
                { DataClassification.SensitivePersonalData, new List<string>() { "medisch", "financiële", "strafrechtelijke" } }
            };

            return outgoingDictionary;
        }
    }
}
