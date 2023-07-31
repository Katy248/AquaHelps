using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquaHelps.Domain.Models;
using AutoMapper;

namespace AquaHelps.Application.CQRS.Posts.Models;
[AutoMap(typeof(Post))]
public class PostDto
{
    public string Text { get; set; }
    public string CreatorId { get; set; }
    public DateTime CreatedOn { get; set; }
}
