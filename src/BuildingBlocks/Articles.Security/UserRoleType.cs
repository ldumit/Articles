using System.ComponentModel;

namespace Articles.Security;

public enum UserRoleType : int
{
		// Cross-domain: 1–9
		[Description("Editorial Office")]
		EOF = 1,

		// Submission: 11–19
		[Description("Author")]
		AUT = 11,
		[Description("Corresponding Author")]
		CORAUT = 12,

		// Review: 21–29
		[Description("Review Editor")]
		REVED = 21,
		[Description("Reviewer")]
		REV = 22,

		// Production: 31–39
		[Description("Production Office")]
		POF = 31,
		[Description("Typesetter")]
		TSOF = 32,

		//talk - explain the ranges 11-19 and also the gap betwen the last domain(production) to Auth, allow space for other domains/microservices 

		// Auth-only: 91–99
		[Description("User Admin")]
		USERADMIN = 91
}

public static class Role
{
		public const string ADMIN = nameof(UserRoleType.USERADMIN);

		public const string EOF = nameof(UserRoleType.EOF);
		public const string CORAUT = nameof(UserRoleType.CORAUT);

		public const string REVED = nameof(UserRoleType.REVED);
		public const string REV = nameof(UserRoleType.REV);

		public const string POF = nameof(UserRoleType.POF);
		public const string TSOF = nameof(UserRoleType.TSOF);

}