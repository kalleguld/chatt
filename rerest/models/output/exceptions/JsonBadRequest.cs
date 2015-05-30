namespace rerest.models.output.exceptions
{
    class JsonBadRequest : JsonException
    {
        public JsonBadRequest(string developerMessage) : base(400, 400, developerMessage)
        {
        }
    }
}
