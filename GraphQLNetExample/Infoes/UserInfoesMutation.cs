using GraphQL;
using GraphQL.Types;
using GraphQLNetExample.EntityFramework;
using GraphQLNetExample.Commons;

namespace GraphQLNetExample.Infoes
{
    public class UserInfoesMutation : ObjectGraphType
    {
        public UserInfoesMutation()
        {
            Field<UserInfoType>(
                "signup",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "account"},
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "password" }
                ),
                resolve: context =>
                {
                    var userinfoesContext = context.RequestServices.GetRequiredService<UserInfoesContext>();

                    var info = new UserInfo
                    {
                        IuserAccount = context.GetArgument<string>("account"),
                        IuserPassword = context.GetArgument<string>("password")
                    };

                    if (Helper.IsNotValidEmail(info.IuserAccount))
                    {
                        info.IuserAccount = "Not a valid Email";
                        info.IuserPassword = "Error";
                        return info;
                    }

                    if (string.IsNullOrWhiteSpace(info.IuserPassword))
                    {
                        info.IuserAccount = "Error";
                        info.IuserPassword = "Please Enter Password";
                        return info;
                    }

                    var accounts = from a in userinfoesContext.UserInfo
                                    select a;
                    accounts = accounts.Where(a => a.IuserAccount.Contains(info.IuserAccount));

                    if (accounts.Any())
                    {
                        info.IuserAccount = "Email Is Registered";
                        info.IuserPassword = "Error";
                        return info;
                    }
                    else
                    {
                        userinfoesContext.UserInfo.Add(info);
                        userinfoesContext.SaveChanges();
                        return info;
                    }
                    
                }
            );
        }
    }
}
