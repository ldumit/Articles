using FastEndpoints;
using Production.API.Features.Shared;

namespace Production.API.Features.AssignTypesetter
{
    public class AssignTypesetterSummary: Summary<AssignTypesetterEndpoint>
		{
				public AssignTypesetterSummary()
				{
						//todo improve the summary
						Summary = "Updates an existing customer in the system";
						Description = "Updates an existing customer in the system";
						Response<ArticleCommandResponse>(201, "Customer was successfully updated");
				}
		}
}
