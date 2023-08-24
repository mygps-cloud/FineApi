using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fine.Api.Exceptions.UserCarExceptions
{
    public class NoUserCarInformationException:Exception
    {
        public NoUserCarInformationException():base("There Are No Cars") { }
    }
}
