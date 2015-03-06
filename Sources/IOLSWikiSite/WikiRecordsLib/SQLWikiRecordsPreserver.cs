using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiRecordsLib
{
    public class SQLWikiRecordsPreserver : WikiRecordsPreserver
    {
        private static string _connectionString = string.Empty;

        public SQLWikiRecordsPreserver(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override bool CreateWikiRecord(WikiRecord record)
        {
            bool isSuccessfully = false;
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                using (SqlCommand command = new SqlCommand("CREATE_WIKIRECORD", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    var paraName = new SqlParameter("@NAME", SqlDbType.NVarChar);
                    paraName.Value = record.Name;
                    var paraKeyword1 = new SqlParameter("@KEYWORD1", SqlDbType.NVarChar);
                    paraKeyword1.Value = record.Keyword1;
                    var paraKeyword2 = new SqlParameter("@KEYWORD2", SqlDbType.NVarChar);
                    paraKeyword2.Value = record.Keyword2;
                    var paraKeyword3 = new SqlParameter("@KEYWORD3", SqlDbType.NVarChar);
                    paraKeyword3.Value = record.Keyword3;
                    var paraDescription = new SqlParameter("@DESCRIPTION", SqlDbType.NVarChar);
                    paraDescription.Value = record.Description;
                    var paraAuthor = new SqlParameter("@AUTHOR", SqlDbType.NVarChar);
                    paraAuthor.Value = record.Author;
                    command.Parameters.AddRange(new[] { paraName, paraKeyword1, paraKeyword2, paraKeyword3, paraDescription, paraAuthor });
                    connection.Open();
                    command.ExecuteNonQuery();
                    isSuccessfully = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return isSuccessfully;
        }

        public override bool? IsWikiNameExisted(string name)
        {
            bool? isExisted = false;
            if (string.IsNullOrEmpty(name))
            {
                name = string.Empty;
            }
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                using (SqlCommand command = new SqlCommand("IS_NAME_EXISTED", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    var paraName = new SqlParameter("@NAME", SqlDbType.NVarChar);
                    paraName.Value = name;
                    var paraReturn = new SqlParameter();
                    paraReturn.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.AddRange(new[] { paraName, paraReturn });
                    connection.Open();
                    command.ExecuteNonQuery();

                    int returnValue = (int)paraReturn.Value;
                    isExisted = returnValue == 1 ? true : false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                isExisted = null;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return isExisted;
        }

        public override bool UpdateWikiRecord(WikiRecord record)
        {
            bool isSuccessfully = false;
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                using (SqlCommand command = new SqlCommand("UPDATE_WIKIRECORD", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    var paraID = new SqlParameter("@ID", SqlDbType.Int);
                    paraID.Value = record.ID;
                    var paraKeyword1 = new SqlParameter("@KEYWORD1", SqlDbType.NVarChar);
                    paraKeyword1.Value = record.Keyword1;
                    var paraKeyword2 = new SqlParameter("@KEYWORD2", SqlDbType.NVarChar);
                    paraKeyword2.Value = record.Keyword2;
                    var paraKeyword3 = new SqlParameter("@KEYWORD3", SqlDbType.NVarChar);
                    paraKeyword3.Value = record.Keyword3;
                    var paraDescription = new SqlParameter("@DESCRIPTION", SqlDbType.NVarChar);
                    paraDescription.Value = record.Description;
                    var paraLastEditor = new SqlParameter("@LASTEDITOR", SqlDbType.NVarChar);
                    paraLastEditor.Value = record.LastEditor;
                    command.Parameters.AddRange(new[] { paraID, paraKeyword1, paraKeyword2, paraKeyword3, paraDescription, paraLastEditor });
                    connection.Open();
                    command.ExecuteNonQuery();
                    isSuccessfully = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return isSuccessfully;
        }

        public override bool DeleteWikiRecrod(WikiRecord record)
        {
            bool isSuccessfully = false;
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                using (SqlCommand command = new SqlCommand("DELETE_WIKIRECORD", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    var paraID = new SqlParameter("@ID", SqlDbType.Int);
                    paraID.Value = record.ID;
                    command.Parameters.Add(paraID);
                    connection.Open();
                    command.ExecuteNonQuery();
                    isSuccessfully = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return isSuccessfully;
        }

        public override bool QueryWikiRecords(string keyword, out IEnumerable<WikiRecord> records)
        {
            bool isSuccessfully = false;
            var recordList = new List<WikiRecord>();
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                using (SqlCommand command = new SqlCommand("GET_WIKIRECORDS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    var paraKeyword = new SqlParameter("@KEYWORD", SqlDbType.NVarChar);
                    paraKeyword.Value = keyword;
                    command.Parameters.Add(paraKeyword);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var record = GenerateWikiRecord(reader);
                            recordList.Add(record);
                        }
                    }
                }
                isSuccessfully = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            records = recordList;
            return isSuccessfully;
        }

        private static WikiRecord GenerateWikiRecord(SqlDataReader reader)
        {
            int id = (int)reader["id"];
            var name = (string)reader["Name"];
            var keyword1 = (string)reader["Keyword1"];
            var keyword2 = (string)reader["Keyword2"];
            var keyword3 = (string)reader["Keyword3"];
            var description = (string)reader["Description"];
            var creationTime = (DateTime)reader["CreatationTime"];
            var lastModificationTime = (DateTime)reader["LastModificationTime"];
            var author = (string)reader["Author"];
            var lastEditor = (string)reader["LastEditor"];

            var record = new WikiRecord
            {
                ID = id,
                Name = name,
                Keyword1 = keyword1,
                Keyword2 = keyword2,
                Keyword3 = keyword3,
                Description = description,
                CreationTime = creationTime,
                LastModificationTime = lastModificationTime,
                Author = author,
                LastEditor = lastEditor
            };
            return record;
        }

        public override bool TryGetWikiRecord(int id, out WikiRecord record)
        {
            bool isSuccessfully = false;
            record = null;
            var recordList = new List<WikiRecord>();
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                using (SqlCommand command = new SqlCommand("GET_WIKIRECORD_BYID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    var paraID = new SqlParameter("@ID", SqlDbType.Int);
                    paraID.Value = id;
                    command.Parameters.Add(paraID);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            record = GenerateWikiRecord(reader);
                            isSuccessfully = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return isSuccessfully;
        }
    }
}
