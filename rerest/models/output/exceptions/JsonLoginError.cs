namespace rerest.models.output.exceptions
{
    class JsonLoginError : JsonException
    {
        public JsonLoginError() : base(403, 3, 
            "No user with that username or password", 
            "No user with that username or password") { }
    }
}
