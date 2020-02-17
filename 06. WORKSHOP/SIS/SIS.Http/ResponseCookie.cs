using System;
using System.Text;

namespace SIS.Http
{
    public class ResponseCookie : Cookie
    {
        public ResponseCookie(string name, string value)
            :base(name, value)
        {
            Path = "/";
            SameSite = SameSiteType.None;
            Expires = DateTime.UtcNow.AddDays(30);
        }
        public string Domain { get; set; }
        public string Path { get; set; }
        public DateTime? Expires { get; set; }
        public long? MaxAge { get; set; }
        public bool Secure { get; set; }
        public bool HttpOnly { get; set; }
        public SameSiteType SameSite { get; set; }

        public override string ToString()
        {
            var cookieBuilder = new StringBuilder();
            cookieBuilder.Append($"{Name}={Value}");

            if (MaxAge.HasValue)
            {
                cookieBuilder.Append($"; Max-Age={MaxAge.Value.ToString()}");
            }
            else if (Expires.HasValue)
            {
                cookieBuilder.Append($"; Expires={Expires.Value.ToString("R")}");
            }

            if (!string.IsNullOrWhiteSpace(Domain))
            {
                cookieBuilder.Append($"; Domain={Domain}");
            }

            if (!string.IsNullOrWhiteSpace(Path))
            {
                cookieBuilder.Append($"; Path={Path}");
            }

            if (Secure)
            {
                cookieBuilder.Append("; Secure");
            }

            if (HttpOnly)
            {
                cookieBuilder.Append("; HttpOnly");
            }

            cookieBuilder.Append($"; SameSite={SameSite.ToString()}");

            return cookieBuilder.ToString();
        }
    }
}
