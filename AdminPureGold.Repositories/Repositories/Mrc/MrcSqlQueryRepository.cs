using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.Mrc;

namespace AdminPureGold.Repositories.Repositories.Mrc
{
    public class MrcSqlQueryRepository<T> : GenericSqlQueryRepository<T>, IMrcSqlQueryRepository<T> where T : class, new()
    {
        public MrcSqlQueryRepository()
            : base(new MrcContext())
        {

        }

        public IEnumerable<T> GetMailingSchedule(int transactionId)
        {
            string sql = "Select A.MailingID "
                + ", A.AppObjectToTransactionID "
                + ", A.ScheduledPrintDate "
                //+ ", ActualPrintDate = Case WHEN ActualPrintDate IS NULL THEN 'Mark as Printed' else 'Printed on ' + CAST(ActualPrintDate as varchar(10)) END"
                + ", ActualPrintDate  "
                + ", C.AppObjectDesc as PrintType "
                + " from tbPG_Mailing A "
                + " INNER JOIN tbAppObjectToTransaction B "
                + " ON A.AppObjectToTransactionID = B.AppObjectToTransactionID "
                + " INNER JOIN tbAppObject C "
                + " ON B.AppObjectID = C.AppObjectID "
                + " WHERE B.TransactionID = '" + transactionId.ToString(CultureInfo.InvariantCulture) + "'";

            return base.ExecWithStoreProcedure(sql).ToList();
        }
        public void SetMailing(int mailingId)
        {
            string sql = "Update tbPG_Mailing Set ActualPrintDate = Case WHEN ActualPrintDate IS NULL Then GETDATE() Else Null END where MailingID = '" + mailingId + "'";
            base.ExecSqlCommand(sql);
        }

        #region Print Job
        public T CreatePrintJob(int printJobTypeId, string printJobDescription, DateTime startDate, DateTime endDate, int crtBy)
        {
            string sql = "EXEC proc_PG_PrintJob_Create '" + printJobDescription + "', '" + startDate + "', '" + endDate + "', '" + printJobTypeId + "', '" + crtBy + "'";
            return base.ExecWithStoreProcedure(sql).SingleOrDefault();
        }
        public IEnumerable<T> GetMissingCustomers(int printJobId)
        {
            string sql = "EXEC proc_PG_PrintJob_MissingCustomers " + printJobId;
            return base.ExecWithStoreProcedure(sql).ToList();
        }
        public Int32? CreateTransactionFromListId(int listId)
        {
            var pListId = new SqlParameter
            {
                ParameterName = "@ListID",
                Value = listId,
                SqlDbType = SqlDbType.Int

            };
            var pOutputTransactionId = new SqlParameter
            {
                ParameterName = "@OutputTransactionID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            string sql = "proc_PG_Admin_WeichertSL_Create_FromListID_Test @ListID, @OutputTransactionID out";
            base.ExecSqlCommand(sql, pListId, pOutputTransactionId);
            return (Int32?)pOutputTransactionId.Value;
        }
        public void ExportPrintJob(int printJobId)
        {
            //string sql = "EXEC proc_PG_PrintJob_Export " + printJobId;
            string sql = "EXEC proc_PG_PrintJob_Export_Test " + printJobId;
            base.ExecSqlCommand(sql);
        }
        #endregion

        #region Survey

        public IEnumerable<T> GetSurveyReportDetail(int choiceId, DateTime startDate, DateTime endDate)
        {
            string sql = "EXEC proc_PG_SurveyReport '" + choiceId + "', '" + startDate + "', '" + endDate + "'";
            return base.ExecWithStoreProcedure(sql).ToList();
        }

        public void SaveSurvey(string referenceNumber, int saleId, string inputBy)
        {
            string sql = "Insert Into tbPGS_Survey( ReferenceNumber, SaleId, InputDate, InputBy) Values ('" + referenceNumber + "', '" + saleId + "', GetDate(), '" + inputBy + "')";
            base.ExecSqlCommand(sql);
        }
        public void SaveSurveyAnswer(int surveyId, int choiceId)
        {
            string sql = "If NOT Exists ( Select SurveyAnswerID from tbPGS_SurveyAnswer where SurveyId = '" + surveyId + "' and ChoiceId = '" + choiceId + "' )  " +
                    " Begin " +
                        "Insert Into tbPGS_SurveyAnswer( SurveyId, ChoiceId) Values ('" + surveyId + "', '" + choiceId + "')" +
                    " End ";
            base.ExecSqlCommand(sql);
        }
        public void SaveSurveyAnswerText(int surveyId, int choiceId, string answerText)
        {
            string sql = "If Exists ( Select SurveyAnswerTextID from tbPGS_SurveyAnswerText where SurveyAnswerId in ( Select SurveyAnswerID from tbPGS_SurveyAnswer where SurveyId = '" + surveyId + "' and ChoiceId = '" + choiceId + "' ) ) " +
                " Begin " +
                    " UPDATE tbPGS_SurveyAnswerText Set SurveyAnswerText = '" + answerText + "' where SurveyAnswerId in ( Select SurveyAnswerID from tbPGS_SurveyAnswer where SurveyId = '" + surveyId + "' and ChoiceId = '" + choiceId + "') " +
                " End " +
                " Else " +
                " Begin " +
                    "If NOT Exists ( Select SurveyAnswerID from tbPGS_SurveyAnswer where SurveyId = '" + surveyId + "' and ChoiceId = '" + choiceId + "' )  " +
                    " Begin " +
                        " Insert Into tbPGS_SurveyAnswer ( SurveyId, ChoiceId) Values ( '" + surveyId + "', '" + choiceId + "') " +
                        " Insert Into tbPGS_SurveyAnswerText ( SurveyAnswerId, SurveyAnswerText) Select SurveyAnswerID, '" + answerText + "' from tbPGS_SurveyAnswer where SurveyId = '" + surveyId + "' and ChoiceId = '" + choiceId + "' " +
                    " End " +
                    " Else " +
                    " Begin " +
                        " Insert Into tbPGS_SurveyAnswerText ( SurveyAnswerId, SurveyAnswerText) Select SurveyAnswerID, '" + answerText + "' from tbPGS_SurveyAnswer where SurveyId = '" + surveyId + "' and ChoiceId = '" + choiceId + "' " +
                    " END " +
                " End ";
            base.ExecSqlCommand(sql);
        }
        public void DeleteSurveyAnswer(int surveyId, int choiceId)
        {

            string sql = "If Exists ( Select SurveyAnswerTextID from tbPGS_SurveyAnswerText where SurveyAnswerId in ( Select SurveyAnswerID from tbPGS_SurveyAnswer where SurveyId = '" + surveyId + "' and ChoiceId = '" + choiceId + "' ) ) " +
                    " Begin " +
                        "DELETE FROM tbPGS_SurveyAnswerText where SurveyAnswerID in ( Select SurveyAnswerID from tbPGS_SurveyAnswer where SurveyId = '" + surveyId + "' and ChoiceId = '" + choiceId + "' )" +
                    " End ";
            base.ExecSqlCommand(sql);

            string sql2 = "If Exists ( Select SurveyAnswerID from tbPGS_SurveyAnswer where SurveyId = '" + surveyId + "' and ChoiceId = '" + choiceId + "' ) " +
                    " Begin " +
                        "DELETE FROM tbPGS_SurveyAnswer where SurveyId = '" + surveyId + "' and ChoiceID = '" + choiceId + "' " +
                    " End ";
            base.ExecSqlCommand(sql2);

        }
        #endregion

        #region Email
        public void LoadEmailTable(String printType, Int32 month1, Int32 year1, Int32 month2, Int32 year2)
        {
            string sql = "EXEC proc_PG_Emails_LoadTable '" + printType + "', '" + month1 + "', '" + year1 + "', '" + month2 + "', '" + year2 + "'";
            base.ExecSqlCommand(sql);
        }

        public void InsertMessageCenterMessageForAgent()
        {

            // This Will Be done with Linq. . . . just have this here for a guide

            //Int32 RecipientWPersNo
            //Int32 SenderWPersNo
            //DateTime DueDate
            //String SubjectText
            //String MessageLink
            //String MessageBody
            //Int32 Priority

            //string sql = "EXEC proc_PG_Emails_LoadTable '" + printType + "', '" + month1 + "', '" + year1 + "', '" + month2 + "', '" + year2 + "'";
            //base.ExecSqlCommand(sql);
        }
        
        #endregion

    }
}
