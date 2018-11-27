using DevExpress.Web.Mvc;

namespace DevExpress.Web.Demos
{
    public class GridViewToolbarHelper
    {
        public const string KeyFieldName = "ProductID";

        static MVCxGridViewColumnCollection exportedColumns;
        public static MVCxGridViewColumnCollection ExportedColumns
        {
            get
            {
                if (exportedColumns == null)
                    exportedColumns = CreateExportedColumns();
                return exportedColumns;
            }
        }

        static GridViewSettings exportGridSettings;
        public static GridViewSettings ExportGridSettings
        {
            get
            {
                if (exportGridSettings == null)
                    exportGridSettings = CreateExportGridSettings();
                return exportGridSettings;
            }
        }

        static MVCxGridViewColumnCollection CreateExportedColumns()
        {
            var columns = new MVCxGridViewColumnCollection();

            columns.Add("File_Number");
            columns.Add("File_Subject");
            columns.Add("Remarks");
            columns.Add(c =>
            {   c.FieldName = "Send_Date";
                c.Caption = "Send Date";
                c.PropertiesEdit.DisplayFormatString = "d";
                c.EditorProperties().DateEdit(p =>
                {
                    p.NullText = "MM/dd/yyyy";
                    p.EditFormat = EditFormat.Custom;
                    p.EditFormatString = "MMMM dd, yyyy";

                });

            });
            columns.Add(t =>
             {
                 t.FieldName = "Send_Time";
                 t.Caption = "Send Time";
                 t.PropertiesEdit.DisplayFormatString = "c";
             });
            columns.Add("SenderName");
            columns.Add("SendTo_Name");
            columns.Add("SendTo_Department");
            
            return columns;
        }

        static GridViewSettings CreateExportGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "gv";
            settings.KeyFieldName = KeyFieldName;
            settings.Columns.Assign(ExportedColumns);
            return settings;
        }
    }
}