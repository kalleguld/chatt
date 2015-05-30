namespace rerest.models.output.exceptions
{
    class JsonInvalidToken : JsonException
    {
        public JsonInvalidToken() : base(403, 5, 
            "The token guid was not found, is expired, or has been retracted. Get a new token."){ }
    }
}
