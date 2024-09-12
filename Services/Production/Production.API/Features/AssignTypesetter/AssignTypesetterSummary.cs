using FastEndpoints;
using Production.API.Features.Shared;

namespace Production.API.Features.AssignTypesetter
{
    public class AssignTypesetterSummary: Summary<AssignTypesetterEndpoint>
		{
				public AssignTypesetterSummary()
				{
						Summary = "Updates an existing customer in the system";
						Description = "Updates an existing customer in the system";
						Response<ArticleCommandResponse>(201, "Customer was successfully updated");
				}
		}
}
