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
                    return new OpenAITranslator(apiKey);
                case "DeepL":
                    return new DeepLTranslator(apiKey);
                case "Google Gemini":
                    return new GeminiTranslator(apiKey);
                default:
                    throw new ArgumentException("Invalid translator provider specified.");
            }
        }
    }
}
