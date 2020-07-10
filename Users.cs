using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using MySql;
using MySql.Data.MySqlClient;

namespace PrdmDB
{
    public class Users
    {
        #region Propieties

        private string dTYPEField;
        private int idField;
        private string customerCardField;
        private bool deactivatedField;
        private string identifierField;
        private string internalIdField;
        private string loginField;
        private string nameField;
        private string passMD5Field;
        private string passwordField;
        private string roleField;
        private string availableChannelField;
        private string logicalIdField;
        

        public string DTYPE { get => dTYPEField; set => dTYPEField = value; }
        public int Id { get => idField; set => idField = value; }
        public string CustomerCard { get => customerCardField; set => customerCardField = value; }
        public bool Deactivated { get => deactivatedField; set => deactivatedField = value; }
        public string Identifier { get => identifierField; set => identifierField = value; }
        public string InternalId { get => internalIdField; set => internalIdField = value; }
        public string Login { get => loginField; set => loginField = value; }
        public string Name { get => nameField; set => nameField = value; }
        public string PassMD5 { get => passMD5Field; set => passMD5Field = value; }
        public string Password { get => passwordField; set => passwordField = value; }
        public string Role { get => roleField; set => roleField = value; }
        public string AvailableChannel { get => availableChannelField; set => availableChannelField = value; }
        public string LogicalId { get => logicalIdField; set => logicalIdField = value; }


        #endregion

        #region Constructor
        public Users() { this.Init(); }
        public Users(string dTYPE, int id, string customerCard, bool deactivated, string identifier, string internalId,
                     string login, string name, string passMD5, string password, string role, string availableChannel, string logicalId)
        {
            this.DTYPE = dTYPE;
            this.Id = id;
            this.CustomerCard = customerCard;
            this.Deactivated = deactivated;
            this.Identifier = identifier;
            this.InternalId = internalId;
            this.Login = login;
            this.Name = name;
            this.PassMD5 = passMD5;
            this.Password = password;
            this.Role = role;
            this.AvailableChannel = availableChannel;
            this.LogicalId = logicalId;
        }
        private void Init()
        {
            this.DTYPE = String.Empty;
            this.Id = 0;
            this.CustomerCard = String.Empty;
            this.Deactivated = true;
            this.Identifier = String.Empty;
            this.InternalId = String.Empty;
            this.Login = String.Empty;
            this.Name = String.Empty;
            this.PassMD5 = String.Empty;
            this.Password = String.Empty;
            this.Role = String.Empty;
            this.AvailableChannel = String.Empty;
            this.LogicalId = String.Empty;
        }
        #endregion

        #region CRUD
        
        public void Store() {
            DB myDB = new DB();
            string query = "INSERT INTO users (DTYPE,id,customerCard,deactivated,identifier,internalId,login,name,passMD5,password,role,availableChannel,logicalId) " +
                            "VALUES ( '" + this.DTYPE + "','" + this.Id + "','" + this.CustomerCard + "','" + this.Deactivated + "','" + this.Identifier + "','" +
                            this.InternalId + "','" + this.Login + "','" + this.Name + "','" + this.PassMD5 + "','" + this.Password + "','" + this.Role + "','" +
                            this.AvailableChannel + "','" + this.LogicalId + "');";
            myDB.ExecuteNonQuery(query);
        }

        public List<Users> Load() {
            List<Users> listUsers = new List<Users>();
            DB myDB = new DB();
            string query = "SELECT * FROM users;";
            try
            {
                myDB.Conection.Open();
                myDB.Command = new MySqlCommand(query, myDB.Conection);
                myDB.Reader = myDB.Command.ExecuteReader();
                if (myDB.Reader.HasRows)
                {
                    while (myDB.Reader.Read())
                    {
                        Users user = new Users();
                        this.DTYPE = !myDB.Reader.IsDBNull(0) ? myDB.Reader.GetString("DTYPE") : string.Empty;
                        this.Id = !myDB.Reader.IsDBNull(1) ? myDB.Reader.GetInt32("id") : 0;
                        this.CustomerCard = !myDB.Reader.IsDBNull(2) ? myDB.Reader.GetString("customerCard") : string.Empty;
                        this.Deactivated = !myDB.Reader.IsDBNull(3) ? myDB.Reader.GetBoolean("deactivated") : true;
                        this.Identifier = !myDB.Reader.IsDBNull(4) ? myDB.Reader.GetString("identifier") : string.Empty;
                        this.InternalId = !myDB.Reader.IsDBNull(5) ? myDB.Reader.GetString("internalId") : string.Empty;
                        this.Login = !myDB.Reader.IsDBNull(6) ? myDB.Reader.GetString("login") : string.Empty;
                        this.Name = !myDB.Reader.IsDBNull(7) ? myDB.Reader.GetString("name") : string.Empty;
                        this.PassMD5 = !myDB.Reader.IsDBNull(8) ? myDB.Reader.GetString("passMD5") : string.Empty;
                        this.Password = !myDB.Reader.IsDBNull(9) ? myDB.Reader.GetString("password") : string.Empty;
                        this.Role = !myDB.Reader.IsDBNull(10) ? myDB.Reader.GetString("role") : string.Empty;
                        this.AvailableChannel = !myDB.Reader.IsDBNull(11) ? myDB.Reader.GetString("availableChannel") : string.Empty;
                        this.LogicalId = !myDB.Reader.IsDBNull(12) ? myDB.Reader.GetString("logicalId") : string.Empty;
                        listUsers.Add(user);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            myDB.Conection.Close();
            myDB.ProcessLog(new StackTrace().GetFrame(0).GetMethod().Name, 0, Utilities.Logger.LogType.Info, query);
            return listUsers;

        }

        public void Load(int id) {
            DB myDB = new DB();
            string query = "SELECT * FROM users WHERE id = '" + id + "';";
            try
            {
                myDB.Conection.Open();
                myDB.Command = new MySqlCommand(query, myDB.Conection);
                myDB.Reader = myDB.Command.ExecuteReader();
                if (myDB.Reader.HasRows)
                {
                    while (myDB.Reader.Read())
                    {
                        this.DTYPE = !myDB.Reader.IsDBNull(0) ? myDB.Reader.GetString("DTYPE") : string.Empty;
                        this.Id = !myDB.Reader.IsDBNull(1) ? myDB.Reader.GetInt32("id") : 0;
                        this.CustomerCard = !myDB.Reader.IsDBNull(2) ? myDB.Reader.GetString("customerCard") : string.Empty;
                        this.Deactivated = !myDB.Reader.IsDBNull(3) ? myDB.Reader.GetBoolean("deactivated") : true;
                        this.Identifier = !myDB.Reader.IsDBNull(4) ? myDB.Reader.GetString("identifier") : string.Empty;
                        this.InternalId = !myDB.Reader.IsDBNull(5) ? myDB.Reader.GetString("internalId") : string.Empty;
                        this.Login = !myDB.Reader.IsDBNull(6) ? myDB.Reader.GetString("login") : string.Empty;
                        this.Name = !myDB.Reader.IsDBNull(7) ? myDB.Reader.GetString("name") : string.Empty;
                        this.PassMD5 = !myDB.Reader.IsDBNull(8) ? myDB.Reader.GetString("passMD5") : string.Empty;
                        this.Password = !myDB.Reader.IsDBNull(9) ? myDB.Reader.GetString("password"): string.Empty;
                        this.Role = !myDB.Reader.IsDBNull(10) ? myDB.Reader.GetString("role") : string.Empty;
                        this.AvailableChannel = !myDB.Reader.IsDBNull(11) ? myDB.Reader.GetString("availableChannel") : string.Empty;
                        this.LogicalId = !myDB.Reader.IsDBNull(12) ? myDB.Reader.GetString("logicalId") : string.Empty;
                    }
                }
                else
                {
                    this.Init();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            myDB.Conection.Close();
            myDB.ProcessLog(new StackTrace().GetFrame(0).GetMethod().Name, 0, Utilities.Logger.LogType.Info, query);

        }

        public void Update() {
            DB myDB = new DB();
            string query = "UPDATE users SET DTYPE = '" + this.DTYPE + "', customerCard = '" + this.CustomerCard +
                "', deactivated = '" + this.Deactivated + "', identifier = '" + this.Identifier + "', internalId = '" + this.InternalId +
                "', login = '" + this.Login + "', name = '" + this.Name + "', passMD5 = '" + this.PassMD5 +
                "', password = '" + this.Password + "', role = '" + this.Role + "', availableChannel = '" + this.AvailableChannel +
                "', logicalId = '" + this.LogicalId + "' WHERE id = '" + this.Id + "';";
            myDB.ExecuteNonQuery(query);
        }

        public void Delete() {
            DB myDB = new DB();
            string query = "DELETE FROM users WHERE id = '" + this.Id + "';";
            myDB.ExecuteNonQuery(query);
            this.Init();
        }
        #endregion

        #region Métodos Secundarios
        public void FindByLogin(string login) {
            DB myDB = new DB();
            string query = "SELECT * FROM users WHERE login LIKE '" + login + "';";
            try
            {
                myDB.Conection.Open();
                myDB.Command = new MySqlCommand(query, myDB.Conection);
                myDB.Reader = myDB.Command.ExecuteReader();
                if (myDB.Reader.HasRows)
                {
                    while (myDB.Reader.Read())
                    {
                        this.DTYPE = !myDB.Reader.IsDBNull(0) ? myDB.Reader.GetString("DTYPE") : string.Empty;
                        this.Id = !myDB.Reader.IsDBNull(1) ? myDB.Reader.GetInt32("id") : 0;
                        this.CustomerCard = !myDB.Reader.IsDBNull(2) ? myDB.Reader.GetString("customerCard") : string.Empty;
                        this.Deactivated = !myDB.Reader.IsDBNull(3) ? myDB.Reader.GetBoolean("deactivated") : true;
                        this.Identifier = !myDB.Reader.IsDBNull(4) ? myDB.Reader.GetString("identifier") : string.Empty;
                        this.InternalId = !myDB.Reader.IsDBNull(5) ? myDB.Reader.GetString("internalId") : string.Empty;
                        this.Login = !myDB.Reader.IsDBNull(6) ? myDB.Reader.GetString("login") : string.Empty;
                        this.Name = !myDB.Reader.IsDBNull(7) ? myDB.Reader.GetString("name") : string.Empty;
                        this.PassMD5 = !myDB.Reader.IsDBNull(8) ? myDB.Reader.GetString("passMD5") : string.Empty;
                        this.Password = !myDB.Reader.IsDBNull(9) ? myDB.Reader.GetString("password") : string.Empty;
                        this.Role = !myDB.Reader.IsDBNull(10) ? myDB.Reader.GetString("role") : string.Empty;
                        this.AvailableChannel = !myDB.Reader.IsDBNull(11) ? myDB.Reader.GetString("availableChannel") : string.Empty;
                        this.LogicalId = !myDB.Reader.IsDBNull(12) ? myDB.Reader.GetString("logicalId") : string.Empty;
                    }
                }
                else 
                { 
                    this.Init();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            myDB.Conection.Close();
            myDB.ProcessLog(new StackTrace().GetFrame(0).GetMethod().Name, 0, Utilities.Logger.LogType.Info, query);

        }
        #endregion
    }
}
