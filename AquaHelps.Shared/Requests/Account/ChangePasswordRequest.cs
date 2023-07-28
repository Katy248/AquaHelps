using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaHelps.Shared.Requests.Account;
public record ChangePasswordRequest(string OldPassword, string NewPassword);
