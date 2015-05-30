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
        UserNotFriendly
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
                    return System.Net.HttpStatusCode.BadRequest;
                case JsonResponseCode.AccessDenied:
                case JsonResponseCode.InvalidToken:
                case JsonResponseCode.LoginError:
                case JsonResponseCode.UsernameExists:
                case JsonResponseCode.UserNotFriendly:
                    return System.Net.HttpStatusCode.Forbidden;

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

                default:
                    throw new NotImplementedException("No error message for JsonResponseCode " + e);
            }
        }

    }
}
