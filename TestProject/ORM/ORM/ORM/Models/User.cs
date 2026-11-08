using System;
using System.Collections.Generic;

namespace ORM.Models;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual UserProfile? UserProfile { get; set; }
}
