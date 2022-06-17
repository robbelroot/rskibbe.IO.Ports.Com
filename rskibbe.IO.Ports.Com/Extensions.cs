namespace rskibbe.IO.Ports.Com
{
    public static class Extensions
    {

        public static bool ExtractByte(this string str, out byte number)
        {
            number = 0;
            var digits = str.Where(x => char.IsDigit(x));
            if (digits.Count() == 0)
                return false;
            var idString = string.Join("", digits);
            number = Convert.ToByte(idString);
            return true;
        }

        public static bool ExtractInt(this string str, out int number)
        {
            number = 0;
            var digits = str.Where(x => char.IsDigit(x));
            if (digits.Count() == 0)
                return false;
            var idString = string.Join("", digits);
            number = Convert.ToInt32(idString);
            return true;
        }

        public static bool ExtractLong(this string str, out long number)
        {
            number = 0;
            var digits = str.Where(x => char.IsDigit(x));
            if (digits.Count() == 0)
                return false;
            var idString = string.Join("", digits);
            number = Convert.ToInt64(idString);
            return true;
        }

    }
}
