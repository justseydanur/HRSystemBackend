using System;

public class UpdateUserDTO
{
    public int? Id { get; set; }
    public string FullName { get; set; } = null!;
    public string TcNo { get; set; } = null!;
    public DateTime? BirthDate { get; set; }
    public string EmployeeNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Department { get; set; } = null!;
    public string Position { get; set; } = null!;
}

