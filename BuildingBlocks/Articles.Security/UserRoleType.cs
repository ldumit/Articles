using System.ComponentModel;

namespace Articles.Security;

public enum UserRoleType : int
{
		[Description("Editorial Office")]
		EOF = 1,
		[Description("Review Editor")]
		RE = 2,
		[Description("Author")]
		AUT = 3,
		[Description("Corresponding Author")]
		CORAUT = 4,
		[Description("Submitting Author")]
		SAUT = 5,
		[Description("Co-Author")]
		COAUT = 6,
		[Description("Production Office")]
		POF = 7,
		[Description("Typesetter")]
		TSOF = 8,
		[Description("User Admin")]
		ADMIN = 100
}

public static class Role
{
		public const string CORAUT = nameof(UserRoleType.CORAUT);
		public const string POF = nameof(UserRoleType.POF);
		public const string TSOF = nameof(UserRoleType.TSOF);

		public const string EOF = nameof(UserRoleType.EOF);
}