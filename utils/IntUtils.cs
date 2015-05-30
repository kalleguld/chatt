namespace utils
{
    public static class IntUtils
    {
        public static int? ParseN(string val)
        {
            if (val == null) return null;
            int i;
            return int.TryParse(val, out i) ? i : (int?) null;
        }
    }
}
