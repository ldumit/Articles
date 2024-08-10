using Articles.EntityFrameworkCore;
using Auth.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Persistence.EntityConfigurations
{
		internal class UserEntityConfiguration : AuditedEntityConfigurationBase<User>
		{
				public UserEntityConfiguration()
				{
				}
		}
}
