using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ltl_cloudstorage.Dtos
{
    public class UserDto
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public byte[] Avatar { get; set; }
        public string Reputation { get; set; }
        public string Introduction { get; set; }
    }
}
