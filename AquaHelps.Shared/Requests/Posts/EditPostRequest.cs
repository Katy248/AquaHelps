using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaHelps.Shared.Requests.Posts;
public record EditPostRequest(string Id, string Text);