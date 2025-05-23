﻿namespace Blocks.Domain;

public interface IAuditableAction
{
		int CreatedById { get; set; }
		DateTime CreatedOn { get; }
		public string Action { get; }
		string? Comment { get; }
}

public interface IAuditableAction<TActionType> : IAuditableAction
		where TActionType : Enum
{
		TActionType ActionType { get; }
}
