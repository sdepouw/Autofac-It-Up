using Refit;

namespace AutofacItUp.Console;

public interface ICatFactsClient
{
  [Get("/facts")]
  Task<List<CatFact>> GetTheFactsAsync(CancellationToken cancellationToken);
}
