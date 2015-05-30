using System;
using System.Net;

namespace rerest.models.output.exceptions
{
    public enum JsonErrorCode
    {
        //Please Do not change the order of existing values
        None,
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

    public static class JsonErrorsExtensions
    {
        public static HttpStatusCode HttpStatusCode(this JsonErrorCode e)
        {
            switch (e)
            {
                case JsonErrorCode.None:
                    return System.Net.HttpStatusCode.OK;
                case JsonErrorCode.BadRequest:
                case JsonErrorCode.MalformedParameter:
                case JsonErrorCode.MissingParameter:
                case JsonErrorCode.WrongParameterType:
                    return System.Net.HttpStatusCode.BadRequest;
                case JsonErrorCode.AccessDenied:
                case JsonErrorCode.InvalidToken:
                case JsonErrorCode.LoginError:
                case JsonErrorCode.UsernameExists:
                case JsonErrorCode.UserNotFriendly:
                    return System.Net.HttpStatusCode.Forbidden;

                default:
                    throw new NotImplementedException("No status code for JsonErrorCode " + e);
            }
        }

        public static string ErrorMessage(this JsonErrorCode e)
        {
            switch (e)
            {
                case JsonErrorCode.None:
                    return "";
                case JsonErrorCode.AccessDenied:
                    return "You do not have access to this resource";
                case JsonErrorCode.BadRequest:
                    return "The request was not understood";
                case JsonErrorCode.MissingParameter:
                    return "A required parameter was missing";
                case JsonErrorCode.MalformedParameter:
                    return "A parameter could not be understood";
                case JsonErrorCode.WrongParameterType:
                    return "A parameter was of the wrong type";
                case JsonErrorCode.InvalidToken:
                    return "The token is not valid. Probably either expired, revoked or misspelled";
                case JsonErrorCode.LoginError:
                    return "Username and password does not match";
                case JsonErrorCode.UsernameExists:
                    return "The requested username is already taken";
                case JsonErrorCode.UserNotFriendly:
                    return "That user is not a friend of yours";

                default:
                    throw new NotImplementedException("No error message for JsonErrorCode " + e);
            }
        }

    }
}
