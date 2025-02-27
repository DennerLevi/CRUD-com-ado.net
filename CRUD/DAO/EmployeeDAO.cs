using CRUD.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CRUD.DAO
{
    public class EmployeeDAO
    {
        private readonly string _connectionString;

        public EmployeeDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int InsertEmployee(EmployeeModel employee)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SP_InsertEmployee", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
                    cmd.Parameters.AddWithValue("@Status", employee.Status);
                    cmd.Parameters.AddWithValue("@Turn", employee.Turn);
                    cmd.Parameters.AddWithValue("@CreationDate", employee.CreationDate);

                    // Se a SP retorna o ID gerado com SCOPE_IDENTITY():
                    object result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
        }

        public List<EmployeeModel> GetAllEmployees()
        {
            List<EmployeeModel> employees = new List<EmployeeModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SP_ReturnEmployees", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var emp = new EmployeeModel
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Department = reader.GetInt32(reader.GetOrdinal("Department")),
                                Status = reader.GetInt32(reader.GetOrdinal("Status")),
                                Turn = reader.GetInt32(reader.GetOrdinal("Turn")),
                                CreationDate = reader.GetDateTime(reader.GetOrdinal("CreationDate")),
                                UpdateDate = reader.IsDBNull(reader.GetOrdinal("UpdateDate"))
                                                ? (DateTime?)null
                                                : reader.GetDateTime(reader.GetOrdinal("UpdateDate")),
                                DeletionDate = reader.IsDBNull(reader.GetOrdinal("DeletionDate"))
                                                ? (DateTime?)null
                                                : reader.GetDateTime(reader.GetOrdinal("DeletionDate"))
                            };
                            employees.Add(emp);
                        }
                    }
                }
            }
            return employees;
        }

        public List<EmployeeModel> FilterEmployees(string name, string lastName, int? status)
        {
            List<EmployeeModel> employees = new List<EmployeeModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SP_FilterEmployees", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Se o parâmetro for nulo em C#, mande como DBNull.Value 
                    cmd.Parameters.AddWithValue("@Name", (object)name ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LastName", (object)lastName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Status", (object)status ?? DBNull.Value);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var emp = new EmployeeModel
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Department = reader.GetInt32(reader.GetOrdinal("Department")),
                                Status = reader.GetInt32(reader.GetOrdinal("Status")),
                                Turn = reader.GetInt32(reader.GetOrdinal("Turn")),
                                CreationDate = reader.GetDateTime(reader.GetOrdinal("CreationDate")),
                                UpdateDate = reader.IsDBNull(reader.GetOrdinal("UpdateDate"))
                                                ? (DateTime?)null
                                                : reader.GetDateTime(reader.GetOrdinal("UpdateDate")),
                                DeletionDate = reader.IsDBNull(reader.GetOrdinal("DeletionDate"))
                                                ? (DateTime?)null
                                                : reader.GetDateTime(reader.GetOrdinal("DeletionDate"))
                            };
                            employees.Add(emp);
                        }
                    }
                }
            }
            return employees;
        }

        public int UpdateEmployee(EmployeeModel employee)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SP_UpdateEmployee", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", employee.Id);
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
                    cmd.Parameters.AddWithValue("@Status", employee.Status);
                    cmd.Parameters.AddWithValue("@Turn", employee.Turn);
                    cmd.Parameters.AddWithValue("@UpdateDate", employee.UpdateDate ?? (object)DBNull.Value);

                    object result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result); // Retorna RowsAffected
                }
            }
        }

        public int InactiveEmployee(int id, DateTime deletionDate)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SP_InactiveEmployee", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@DeletionDate", deletionDate);

                    object result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result); // Retorna RowsAffected
                }
            }
        }

        public int DeleteEmployee(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SP_DeleteEmployee", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", id);

                    object result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result); // Retorna RowsAffected
                }
            }
        }
    }
}
