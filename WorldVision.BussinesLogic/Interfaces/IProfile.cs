using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.Interfaces
{
    public interface IProfile
    {
        int Id { get; set; }
        string Username { get; set; }
        string Email { get; set; }
    }
}
