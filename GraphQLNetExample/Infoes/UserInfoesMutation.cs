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
                        Email = context.GetArgument<string>("account"),
                        Password = context.GetArgument<string>("password")
                    };

                    if (Helper.IsNotValidEmail(info.Email))
                    {
                        info.Email = "Not a valid Email";
                        info.Password = "Error";
                        return info;
                    }

                    if (string.IsNullOrWhiteSpace(info.Password))
                    {
                        info.Email = "Error";
                        info.Password = "Please Enter Password";
                        return info;
                    }

                    var accounts = from a in userinfoesContext.UserInfo
                                    select a;
                    accounts = accounts.Where(a => a.Email.Contains(info.Email));

                    if (accounts.Any())
                    {
                        info.Email = "Email Is Registered";
                        info.Password = "Error";
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
