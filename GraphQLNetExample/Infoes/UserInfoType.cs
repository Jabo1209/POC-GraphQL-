using GraphQL.Types;

namespace GraphQLNetExample.Infoes;

public class UserInfoType : ObjectGraphType<UserInfo>
{
    public UserInfoType()
    {
        Name = "UserInfo";
        Description = "UserInfo Type";
        Field(d => d.ID, nullable: false).Description("UserInfo ID");
        Field(d => d.IuserAccount, nullable: false).Description("UserInfo IuserAccount");
        Field(d => d.IuserPassword, nullable: false).Description("UserInfo IuserPassword");
    }
}