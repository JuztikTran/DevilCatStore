using Newtonsoft.Json;

namespace DTOs.Auth
{
    public class DTOToken
    {
        public string? UserName { get; set; }
        public string? Token { get; set; }
        public int ExpireIn { get; set; }

        public override string ToString()
        {
            var data = JsonConvert.SerializeObject(this);
            return data;
        }
    }
}
