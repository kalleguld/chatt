namespace rerest.models.output.exceptions
{
    class JsonUsernameExists : JsonException
    {
        public JsonUsernameExists(string username) : base(403, 1,
            "Cannot create user because a user with the same username already exists",
            "Cannot create user because a user with the same username already exists") { }
    }
}
