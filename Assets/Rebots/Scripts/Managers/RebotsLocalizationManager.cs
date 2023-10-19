using HelpDesk.Sdk.Common.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Rebots.HelpDesk
{
    public class RebotsLocalizationManager
    {
        public string language;
        string defaultLanguage = "ko";
        int? defaultLanguageIndex;

        public Dictionary<string, string> translationDic = new();

        public List<RebotsLanguageInfo> settingLanguages;

        private TextAsset translationFile;
        private List<string> translanguageList = new List<string>();
        private readonly char lineSeperator = '\n';

        public RebotsLocalizationManager(TextAsset translationFile)
        {
            this.translationFile = translationFile;
            settingLanguages = new();
        }

        public void SetAvailableLanguage(string langCode)
        {
            RebotsLanguage langValue = RebotsLanguageUtility.StringToEnum(langCode);

            if (langValue != RebotsLanguage.None)
            {
                var languageItem = new RebotsLanguageInfo()
                {
                    languageValue = langValue,
                    index = (int)langValue,
                    languageCode = langCode,
                    isCurrent = false
                };
                settingLanguages.Add(languageItem);
            }
        }

        public string SetLanguage(RebotsLanguage selectLanguage)
        {
            var count = settingLanguages.Count();
            for (int i = 0; i < count; i++)
            {
                var language = settingLanguages[i];
                if (language.languageValue == selectLanguage)
                {
                    language.isCurrent = true;
                    this.language = language.languageCode;
                }
                else
                {
                    language.isCurrent = false;
                }
            }
            return language;
        }

        public string SetLanguage(string selectLanguage)
        {
            var count = settingLanguages.Count();
            for (int i = 0; i < count; i++)
            {
                var language = settingLanguages[i];
                if (language.languageCode == selectLanguage)
                {
                    language.isCurrent = true;
                    this.language = language.languageCode;
                }
                else
                {
                    language.isCurrent = false;
                }
            }
            return language;
        }

        public bool CheckSupportedLanguage(RebotsLanguage selectLanguage)
        {
            return settingLanguages.Any(x => x.languageValue == selectLanguage);
        }

        public void ReadData(string helpDeskLocale = "", bool checkSuccess = true)
        {
            translationDic.Clear();
            translanguageList.Clear();
            string[] records = translationFile.text.Split(lineSeperator);
            var isFirstLine = true;
            var currentLocaleIndex = -1;
            int? success = null;
            Regex elementParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            foreach (string record in records)
            {
                if (!string.IsNullOrEmpty(record))
                {
                    string[] fields = elementParser.Split(record);
                    if (isFirstLine)
                    {
                        if (!string.IsNullOrEmpty(this.language) && fields.Contains(this.language))
                        {
                            currentLocaleIndex = Array.FindIndex(fields, x => x.Equals(this.language, StringComparison.InvariantCultureIgnoreCase));
                            success = 1;
                        }
                        else if (!string.IsNullOrEmpty(helpDeskLocale) && fields.Contains(helpDeskLocale))
                        {
                            currentLocaleIndex = Array.FindIndex(fields, x => x.Equals(helpDeskLocale, StringComparison.InvariantCultureIgnoreCase));
                            this.language = helpDeskLocale;
                        }
                        else if (fields.Any(x => x.ToLower() == this.defaultLanguage))
                        {
                            currentLocaleIndex = Array.FindIndex(fields, t => t.Equals(this.defaultLanguage, StringComparison.InvariantCultureIgnoreCase));
                            this.language = this.defaultLanguage;
                            success = 0;
                        }
                        else
                        {
                            // TODO: language not found
                            throw new Exception("locale can not be found");
                        }

                        if (fields.Any(x => x.ToLower() == this.defaultLanguage))
                        {
                            defaultLanguageIndex = Array.FindIndex(fields, x => x.Equals(this.defaultLanguage, StringComparison.InvariantCultureIgnoreCase));
                        }

                        translanguageList.AddRange(fields);
                        isFirstLine = false;
                    }
                    else
                    {
                        var translation = "";
                        if (fields.Length > currentLocaleIndex)
                        {
                            translation = GetRidOfCommaEscapers(fields[currentLocaleIndex]);
                            if (string.IsNullOrEmpty(translation))
                            {
                                if (defaultLanguageIndex != null)
                                {
                                    translation = GetRidOfCommaEscapers(fields[defaultLanguageIndex.Value]);
                                }
                            }
                        }
                        else if (defaultLanguageIndex.HasValue && fields.Length > defaultLanguageIndex.Value)
                        {
                            translation = GetRidOfCommaEscapers(fields[defaultLanguageIndex.Value]);
                        }

                        translationDic.Add(fields[0], translation);
                    }
                }
            }

            if (checkSuccess)
            {
                if (success.HasValue)
                {
                    if (success == 1)
                    {
                        Debug.Log("locale: locale set");
                    }
                    else if (success == 0)
                    {
                        Debug.Log("locale: default set");
                    }
                }
                else
                {
                    Debug.Log("locale: helpdesk set");
                }
            }
        }

        private static string GetRidOfCommaEscapers(string translation)
        {
            if (translation.StartsWith("\"") && translation.EndsWith("\""))
            {
                return translation.Substring(1, translation.Length - 2);
            }
            return translation;
        }
        
    }

    public class RebotsLanguageInfo
    {
        public RebotsLanguage languageValue;
        public int index;
        public string languageCode;
        public bool isCurrent;
    }
}
