using System.ComponentModel;

namespace Articles.Abstractions.Enums;

public enum ArticleStage : int
{
		None = 0,

		//Submission
		[Description("The Author created the article")]
		Created = 101,
		[Description("Author uploaded the Manuscript")]
		ManuscriptUploaded = 102,
		[Description("The Manuscript was submitted by the author")]
		Submitted = 103,
		[Description("The Manuscript didn't pass the initial checks")]
		InitialRejected = 104,
		[Description("The Manuscript passed the initial editorial checks")]
		InitialApproved = 105,

		//Review
		[Description("Article is under review")]
		UnderReview = 201,
		[Description("Reviewer feedback received, pending editor decision")]
		ReadyForDecision = 202,
		[Description("Editor requested a revised manuscript from the author")]
		AwaitingRevision = 203, //talk: not implemented, but real, requires review rounds
		[Description("Article accepted")]
		Accepted = 204,
		[Description("Article rejected")]
		Rejected = 205,

		//Production
		[Description("Typesetter assigned")]
		InProduction = 300,
		[Description("Draft PDF uploaded")]
		DraftProduction = 301,
		[Description("Draft PDF approved")]
		FinalProduction = 302,
		[Description("Article scheduled for publication")]
		PublicationScheduled = 304,
		[Description("Article published")]
		Published = 305
}
