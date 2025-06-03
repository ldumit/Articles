using Auth.Domain.Users.Enums;
using Blocks.Core;
using Newtonsoft.Json;

namespace Auth.Domain.Users.ValueObjects;

public class HonorificTitle : StringValueObject
{
    [JsonConstructor]
    private HonorificTitle(string value) => Value = value;

    public static HonorificTitle FromString(string honorific)
    {
        Guard.ThrowIfNullOrWhiteSpace(honorific);

        return new HonorificTitle(honorific.Trim());
    }

    public static HonorificTitle Create(Honorific? honorific)
    {
				if(honorific.HasValue)
				    return new HonorificTitle(honorific!.ToString());

        return null;
    }
}
