using System.Net.Http;
using System.Threading.Tasks;

namespace Labrune.Translators
{
    public interface ITranslator
    {
        Task<string> Translate(HttpClient client, string text, string[] rules);
    }
}
