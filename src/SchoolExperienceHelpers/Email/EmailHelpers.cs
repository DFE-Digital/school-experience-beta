using System.Text;

namespace SchoolExperienceHelpers
{
    public static class EmailHelpers
    {
        public static string AnonymiseEmailAddress(string emailAddress)
        {
            int atPos = emailAddress.IndexOf('@');
            var sb = new StringBuilder();

            int pos = 0;
            while (pos < atPos)
            {
                if (pos != 0)
                {
                    sb.Append('.');
                }

                int dotPos = emailAddress.IndexOfAny(new[] { '.', '@' }, pos);

                if (dotPos == -1)
                {
                    dotPos = atPos;
                }

                var part = emailAddress.Substring(pos, dotPos - pos);
                if (part.Length > 2)
                {
                    sb.Append($"{part[0]}*{part[part.Length - 1]}");
                }
                else
                {
                    sb.Append("*");
                }

                pos = dotPos + 1;
            }

            sb.Append(emailAddress.Substring(atPos));
            return sb.ToString();
        }
    }
}
