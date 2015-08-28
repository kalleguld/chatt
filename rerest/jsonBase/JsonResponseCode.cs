using System;
using System.Net;

namespace rerest.jsonBase
{
    public enum JsonResponseCode
    {
        //Please Do not change the order of existing values
        None = 0,
        AccessDenied,
        BadRequest,
        MissingParameter,
        MalformedParameter,
        WrongParameterType,
        InvalidToken,
        LoginError,
        UsernameExists,
        UserNotFriendly,
        InvalidUsername,
        NoToken,
        WrongTokenType
    }

    public static class JsonResponseCodeExtensions
    {
        public static HttpStatusCode HttpStatusCode(this JsonResponseCode e)
        {
            switch (e)
            {
                case JsonResponseCode.None:
                    return System.Net.HttpStatusCode.OK;
                case JsonResponseCode.BadRequest:
                case JsonResponseCode.MalformedParameter:
                case JsonResponseCode.MissingParameter:
                case JsonResponseCode.WrongParameterType:
                case JsonResponseCode.InvalidUsername:
                    return System.Net.HttpStatusCode.BadRequest;
                case JsonResponseCode.AccessDenied:
                case JsonResponseCode.UsernameExists:
                case JsonResponseCode.UserNotFriendly:
                    return System.Net.HttpStatusCode.Forbidden;
                case JsonResponseCode.NoToken:
                case JsonResponseCode.WrongTokenType:
                case JsonResponseCode.LoginError:
                case JsonResponseCode.InvalidToken:
                    return System.Net.HttpStatusCode.Unauthorized;

                default:
                    throw new NotImplementedException("No status code for JsonResponseCode " + e);
            }
        }

        public static string ErrorMessage(this JsonResponseCode e)
        {
            switch (e)
            {
                case JsonResponseCode.None:
                    return "";
                case JsonResponseCode.AccessDenied:
                    return "You do not have access to this resource.";
                case JsonResponseCode.BadRequest:
                    return "The request was not understood.";
                case JsonResponseCode.MissingParameter:
                    return "A required parameter was missing.";
                case JsonResponseCode.MalformedParameter:
                    return "A parameter could not be understood.";
                case JsonResponseCode.WrongParameterType:
                    return "A parameter was of the wrong type.";
                case JsonResponseCode.InvalidToken:
                    return "The token is not valid. Probably either expired, revoked or misspelled.";
                case JsonResponseCode.LoginError:
                    return "Username and password does not match.";
                case JsonResponseCode.UsernameExists:
                    return "The requested username is already taken.";
                case JsonResponseCode.UserNotFriendly:
                    return "That user is not a friend of yours.";
                case JsonResponseCode.InvalidUsername:
                    return "Supplied username is not valid";
                case JsonResponseCode.WrongTokenType:
                    return "Authorize header is wrong. Must look like this: Token <guid>";

                default:
                    throw new NotImplementedException("No error message for JsonResponseCode " + e);
            }
        }

    }
}
