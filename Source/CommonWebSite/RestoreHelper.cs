using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

namespace CommonWebSite
{
    public class RestoreHelper
    {
        public RestoreHelper()
        {

        }

        public string RestoreDatabase(String databaseName, String userName, String password, String serverName, String filePath)
        {
            ServerConnection connection = new ServerConnection(serverName, userName, password);
            Server sqlServer = new Server(connection);
            Database db = sqlServer.Databases[databaseName];
            //db.Create();  
            //db.Refresh(); 
            Restore sqlRestore = new Restore();
            sqlRestore.NoRecovery=false;

            sqlRestore.Database = databaseName;
            sqlRestore.Action = RestoreActionType.Database;
            BackupDeviceItem bdi = default(BackupDeviceItem);
            bdi = new BackupDeviceItem(filePath, DeviceType.File);
            sqlRestore.Devices.Add(bdi); 
            //sqlRestore.Devices.AddDevice(filePath, DeviceType.File);
            sqlRestore.PercentCompleteNotification = 10;
            sqlRestore.ReplaceDatabase = true;
            sqlRestore.PercentComplete += new PercentCompleteEventHandler(ProgressEventHandler);
            
            sqlRestore.SqlRestore(sqlServer);
            db.Refresh();
            db.SetOnline();
            sqlServer.Refresh();
      
            return "Restore for database successful!";
        }
        static void ProgressEventHandler(object sender, PercentCompleteEventArgs e)
        {
            Console.WriteLine(e.Percent.ToString() + "% restored");
        }

        public event EventHandler<PercentCompleteEventArgs> PercentComplete;

        void sqlRestore_PercentComplete(object sender, PercentCompleteEventArgs e)
        {
            if (PercentComplete != null) 
                PercentComplete(sender, e);
        }

        public event EventHandler<ServerMessageEventArgs> Complete;

        void sqlRestore_Complete(object sender, ServerMessageEventArgs e)
        {
            if (Complete != null)
                Complete(sender, e);
        }
    }
}
