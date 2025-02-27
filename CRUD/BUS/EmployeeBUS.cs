using CRUD.DAO;
using CRUD.Models;

namespace CRUD.BUS
{
    public class EmployeeBUS
    {
        private readonly EmployeeDAO _employeeDao;

        public EmployeeBUS(string connectionString)
        {
            _employeeDao = new EmployeeDAO(connectionString);
        }

        public int CreateEmployee(EmployeeModel model)
        {
            // Regras de negócio, validações, etc.
            if (string.IsNullOrEmpty(model.Name))
                throw new Exception("Name não pode ser vazio.");

            // Ajustar datas, logs, etc.
            model.CreationDate = DateTime.Now;

            return _employeeDao.InsertEmployee(model);
        }

        public List<EmployeeModel> GetAllEmployees()
        {
            return _employeeDao.GetAllEmployees();
        }

        public List<EmployeeModel> FilterEmployees(string name, string lastName, int? status)
        {
            return _employeeDao.FilterEmployees(name, lastName, status);
        }

        public int UpdateEmployee(EmployeeModel model)
        {
            // Exemplo de regra de negócio
            if (model.Id <= 0)
                throw new Exception("Id inválido.");

            model.UpdateDate = DateTime.Now;
            return _employeeDao.UpdateEmployee(model);
        }

        public int InactiveEmployee(int id)
        {
            // Exemplo: inativa com data atual
            return _employeeDao.InactiveEmployee(id, DateTime.Now);
        }

        public int DeleteEmployee(int id)
        {
            return _employeeDao.DeleteEmployee(id);
        }
    }
}
