using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AmokApi.UnitTests.Helpers
{
    public interface HttpMessageHandlerProtectedMembers
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage message, CancellationToken ct);
    }
}
