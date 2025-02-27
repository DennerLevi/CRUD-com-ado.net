namespace CRUD.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Department { get; set; } // Mapeia o enum DepartmentEnum
        public int Status { get; set; }     // Mapeia o enum StatusEnum
        public int Turn { get; set; }       // Mapeia o enum TurnEnum
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletionDate { get; set; }
    }

}
