using System.Net.Http;
using System.Threading.Tasks;

namespace Labrune.Translators
{
    public interface ITranslator
    {
        bool RequiresApiKey { get; }
        bool SupportsCustomPrompt { get; }
        bool SupportsModelSelection { get; }
        Task<string> Translate(HttpClient client, string text, string customPrompt, string modelName, string extraParams);
    }
}
