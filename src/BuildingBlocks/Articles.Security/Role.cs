using Articles.Abstractions.Enums;

namespace Articles.Security;


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