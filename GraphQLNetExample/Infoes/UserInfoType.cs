using GraphQL.Types;

namespace GraphQLNetExample.Infoes;

public class UserInfoType : ObjectGraphType<UserInfo>
{
    public UserInfoType()
    {
        Name = "UserInfo";
        Description = "UserInfo Type";
        Field(d => d.ID, nullable: false).Description("UserInfo ID");
        Field(d => d.Email, nullable: false).Description("UserInfo Email");
        Field(d => d.Password, nullable: false).Description("UserInfo Password");
    }
}