using MixERP.Net.Common;
using MixERP.Net.Common.Models.Core;
using MixERP.Net.DBFactory;
using Npgsql;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;

namespace MixERP.Net.Core.Modules.BackOffice.Data
{
    public static class Attachments
    {
        public static string DeleteReturningPath(long id)
        {
            const string sql = "DELETE from core.attachments WHERE attachment_id=@AttachmentId RETURNING file_path;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@AttachmentId", id);

                return Conversion.TryCastString(DbOperations.GetScalarValue(command));
            }
        }

        public static Collection<AttachmentModel> GetAttachments(string attachmentDirectory, string book, long id)
        {
            Collection<AttachmentModel> collection = new Collection<AttachmentModel>();

            const string sql = "SELECT attachment_id, comment, @AttachmentDirectory || file_path as file_path, original_file_name, added_on FROM core.attachments WHERE resource || resource_key=core.get_attachment_lookup_info(@Book) AND resource_id=@ResourceId;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@AttachmentDirectory", attachmentDirectory);
                command.Parameters.AddWithValue("@Book", book);
                command.Parameters.AddWithValue("@ResourceId", id);

                using (DataTable table = DbOperations.GetDataTable(command))
                {
                    if (table != null && table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            if (row != null)
                            {
                                AttachmentModel model = new AttachmentModel();

                                model.Id = Conversion.TryCastLong(row["attachment_id"]);
                                model.Comment = Conversion.TryCastString(row["comment"]);
                                model.OriginalFileName = Conversion.TryCastString(row["original_file_name"]);
                                model.FilePath = Conversion.TryCastString(row["file_path"]);
                                model.AddedOn = Conversion.TryCastDate(row["added_on"]);

                                collection.Add(model);
                            }
                        }
                    }
                }
            }

            return collection;
        }

        public static bool Save(int userId, string book, long id, Collection<AttachmentModel> attachments)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString()))
            {
                connection.Open();

                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    if (attachments != null && attachments.Count > 0)
                    {
                        try
                        {
                            foreach (AttachmentModel attachment in attachments)
                            {
                                const string sql =
                                    "INSERT INTO core.attachments(user_id, resource, resource_key, resource_id, original_file_name, file_extension, file_path, comment) " +
                                    "SELECT @UserId, core.attachment_lookup.resource, core.attachment_lookup.resource_key, @ResourceId, @OriginalFileName, @FileExtension, @FilePath, @Comment" +
                                    " FROM core.attachment_lookup WHERE book=@Book;";
                                using (NpgsqlCommand attachmentCommand = new NpgsqlCommand(sql, connection))
                                {
                                    attachmentCommand.Parameters.AddWithValue("@UserId", userId);
                                    attachmentCommand.Parameters.AddWithValue("@Book", book);
                                    attachmentCommand.Parameters.AddWithValue("@ResourceId", id);
                                    attachmentCommand.Parameters.AddWithValue("@OriginalFileName",
                                        attachment.OriginalFileName);
                                    attachmentCommand.Parameters.AddWithValue("@FileExtension",
                                        Path.GetExtension(attachment.OriginalFileName));
                                    attachmentCommand.Parameters.AddWithValue("@FilePath", attachment.FilePath);
                                    attachmentCommand.Parameters.AddWithValue("@Comment", attachment.Comment);

                                    attachmentCommand.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            return true;
                        }
                        catch (NpgsqlException)
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                }
            }

            return false;
        }
    }
}