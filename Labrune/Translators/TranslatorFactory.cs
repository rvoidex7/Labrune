using System;

namespace Labrune.Translators
{
    public static class TranslatorFactory
    {
        public static ITranslator CreateTranslator(string provider, string apiKey)
        {
            switch (provider)
            {
                case "OpenAI":
                case "Grok":
                    return new OpenAITranslator(apiKey);
                case "DeepL":
                    return new DeepLTranslator(apiKey);
                case "Google Gemini":
                    return new GeminiTranslator(apiKey);
                case "Google (Web)":
                    return new GoogleTranslator();
                default:
                    throw new ArgumentException("Invalid translator provider specified.");
            }
        }
    }
}
