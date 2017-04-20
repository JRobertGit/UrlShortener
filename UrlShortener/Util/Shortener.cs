namespace UrlShortener.Util
{
    using System.Linq;
    using System.Text;

    public class Shortener
    {
        public static readonly string Alphabet = "abcdefghijklmnopqrstuvwxyz0123456789_-";
        public static readonly int Base = Alphabet.Length;

        public static string ShortenUrl(int idValue)
        {
            if (idValue == 0)
            {
                return Alphabet[0].ToString();
            }

            var shUrl = new StringBuilder();

            while (idValue > 0)
            {
                shUrl.Append(Alphabet[idValue % Base]);
                idValue = idValue / Base;
            }

            return string.Join(string.Empty, shUrl.ToString().Reverse());
        }

        public static int RecoverId(string shUrl)
        {
            var i = 0;

            foreach (var c in shUrl)
            {
                i = i * Base + Alphabet.IndexOf(c);
            }

            return i;
        }
    }
}