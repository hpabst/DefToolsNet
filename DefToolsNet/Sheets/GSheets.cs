using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DefToolsNet.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;

namespace DefToolsNet.Sheets
{
    public class GSheets
    {
        private string SSId;
        private string[] Scopes = {SheetsService.Scope.Spreadsheets};
        private string ApplicationName = "DefTools";
        private DbControl Db;
        private UserCredential Credential;
        private SheetsService Service;

        public GSheets(string SpreadsheetId, DbControl control)
        {
            this.SSId = SpreadsheetId;
            this.Db = control;
            this.Credential = GetCredentials();
            this.Service = GetService(this.Credential, this.ApplicationName);
        }

        public bool UpdateGoogleSheet()
        {
            this.ClearExistingSheets();
            return false;
        }

        private bool ClearExistingSheets()
        {
            Spreadsheet ss = this.Service.Spreadsheets.Get(this.SSId).Execute();
            IList<Sheet> sheets = ss.Sheets;
            ClearValuesRequest requestBody = new ClearValuesRequest();
            SpreadsheetsResource.ValuesResource.ClearRequest cr =
                this.Service.Spreadsheets.Values.Clear(requestBody, ss.SpreadsheetId, "A1:Z1000");
            try
            {
                cr.Execute();
            }
            catch (IOException e)
            {
                return false;
            }
            
            sheets.RemoveAt(0); //Can't delete the first sheet, so we clear it instead of deleting.
            BatchUpdateSpreadsheetRequest burs = new BatchUpdateSpreadsheetRequest();
            burs.Requests = new List<Request>();
            foreach (Sheet s in sheets)
            {
                Request request = new Request();
                request.DeleteSheet = new DeleteSheetRequest();
                request.DeleteSheet.SheetId = s.Properties.SheetId;
                burs.Requests.Add(request);
            }
            SpreadsheetsResource.BatchUpdateRequest content = new SpreadsheetsResource.BatchUpdateRequest(this.Service, burs, this.SSId);
            try
            {
                content.Execute();
            } catch (IOException e)
            {
                return false;
            }
            return true;
        }

        private bool CreateSummarySheet()
        {
            throw new NotImplementedException();
        }

        private bool CreateAllPlayerSheets()
        {
            throw new NotImplementedException();
        }

        private bool CreateUserSheet(WowPlayer player)
        {
            throw new NotImplementedException();
        }

        private UserCredential GetCredentials()
        {
            UserCredential credential;
            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/sheets.googleapis.com-deftools.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }
            return credential;
        }

        private SheetsService GetService(UserCredential credential, String appName)
        {
            return new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = appName
            });
        }
    }
}
