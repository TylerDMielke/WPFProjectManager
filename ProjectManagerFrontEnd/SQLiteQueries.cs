using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace ProjectManagerFrontEnd
{
    public class SQLiteQueries
    {
        public SQLiteQueries()
        {

        }

        public void InitializeDatabase()
        {
            // Check if Project Table already exists 
            if (!this.SelectTableExists("project"))
            {
                this.CreateProjectTable();
            }

            // Check if Task Table already exists 
            if (!this.SelectTableExists("task"))
            {
               this.CreateTaskTable();
            }
        }

        public long InsertIntoProject(string userId, string taskName, string taskDescription)
        {
            long projectId = this.SelectMaxProjectId() + 1;

            using(SQLiteConnection connection = new SQLiteConnection("Data Source=kobold.db;Version=3;"))
            {
                string insertSql = "insert into project (id, user, name, description) values (@id, @userId,@taskName,@taskDescription)";
                using(SQLiteCommand command = new SQLiteCommand(insertSql, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@id", projectId);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@taskName", taskName);
                    command.Parameters.AddWithValue("@taskDescription", taskDescription);
                    command.Prepare(); 
                    command.ExecuteNonQuery();
                }
            }

            return projectId;
        }

        public long InsertIntoTask(string userId, string taskName, string taskDescription, int associatedStoryId, int taskStatusIndicator)
        {
            long taskId = this.SelectMaxTaskId() + 1;

            using(SQLiteConnection connection = new SQLiteConnection("Data Source=kobold.db;Version=3;"))
            {
                string insertSql = "insert into task (id, user, name, description, storyId, statusInd) values (@id, @userId,@taskName,@taskDescription,@associatedStoryId,@taskStatusIndicator)";
                using(SQLiteCommand command = new SQLiteCommand(insertSql, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@id", taskId);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@taskName", taskName);
                    command.Parameters.AddWithValue("@taskDescription", taskDescription);
                    command.Parameters.AddWithValue("@associatedStoryId", associatedStoryId);
                    command.Parameters.AddWithValue("@taskStatusIndicator", taskStatusIndicator);
                    command.Prepare();
                    command.ExecuteNonQuery();
                }
            }

            return taskId;
        }

        public long SelectMaxProjectId()
        {
            long maxProjectId = 0;

            using(SQLiteConnection connection = new SQLiteConnection("Data Source=kobold.db;Version=3;"))
            {
                string insertSql = "select max(id) from project";
                using(SQLiteCommand command = new SQLiteCommand(insertSql, connection))
                {
                    connection.Open();

                    using(SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if(reader.Read() && !reader.IsDBNull(0))
                        {
                            maxProjectId = (long) reader.GetValue(0);
                        }
                    }
                }
            }

            return maxProjectId;
        }

        private long SelectMaxTaskId()
        {
            long maxTaskId = 0;

            using(SQLiteConnection connection = new SQLiteConnection("Data Source=kobold.db;Version=3;"))
            {
                string insertSql = "select max(id) from task";
                using(SQLiteCommand command = new SQLiteCommand(insertSql, connection))
                {
                    connection.Open();

                    using(SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if(reader.Read() && !reader.IsDBNull(0))
                        {
                            maxTaskId = (long) reader.GetValue(0);
                        }
                    }
                }
            }

            return maxTaskId;
        }

        private bool SelectTableExists(string tableName)
        {
            long tableCount = -1;

            using(SQLiteConnection connection = new SQLiteConnection("Data Source=kobold.db;Version=3;"))
            {
                string selectTableSql = "select count(*) from sqlite_master where type='table' and name=@tableName";
                using(SQLiteCommand command = new SQLiteCommand(selectTableSql, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@tableName", tableName);
                    command.Prepare();

                    tableCount = (long)command.ExecuteScalar();
                }
            }

            return tableCount > 0;
        }
        
        private void CreateProjectTable()
        {
            using(SQLiteConnection connection = new SQLiteConnection("Data Source=kobold.db;Version=3;"))
            {
                string createProjectTableSql = "create table project (id long primary key, user varchar(32), name varchar(64), description varchar(512))";
                using(SQLiteCommand command = new SQLiteCommand(createProjectTableSql, connection))
                {
                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }

        private void CreateTaskTable()
        {
            using(SQLiteConnection connection = new SQLiteConnection("Data Source=kobold.db;Version=3;"))
            {
                string createTaskTableSql = "create table task (id long primary key, user varchar(32), name varchar(64), description varchar(512), storyId int, statusInd int)";
                using(SQLiteCommand command = new SQLiteCommand(createTaskTableSql, connection))
                {
                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
