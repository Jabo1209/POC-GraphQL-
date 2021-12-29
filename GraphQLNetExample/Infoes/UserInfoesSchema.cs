using GraphQL.Types;

namespace GraphQLNetExample.Infoes;

public class UserInfoesSchema : Schema
{
    public UserInfoesSchema(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Query = serviceProvider.GetRequiredService<UserInfoesQuery>();
        Mutation = serviceProvider.GetRequiredService<UserInfoesMutation>();
    }

}