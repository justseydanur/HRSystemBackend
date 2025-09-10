using System;

public class UpdateUserDTO
{
    

    public int id { get; set; }
    public string firstName { get; set; } = null!;
    public string lastName { get; set; }
    public string tckimlik { get; set; } = null!;
    public DateTime? dogumTarihi { get; set; } = null!;
    public string telNo { get; set; } = null!;
    public string email { get; set; } = null!;
    public string personnelPhoto { get; set; } = null!;
    public string PasswordSalt { get; set; } = null!;

    public string departmentId;
    public string adres { get; set; } = null!;
}

