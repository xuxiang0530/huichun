using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CompanyManager.DataManager;
using MySql.Data.MySqlClient;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.PowerPoint;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace CompanyManager.model
{
    class Hfile : MySqlConn
    {
        #region properity
        private int _fileid;        //不等于-1时,有文件
        private int _filetypeid;
        private string _fileName;
        private string _fileFullName;
        private DateTime _upLoadDate;
        private int _uploadUserId;
        private bool _disableFlag;
        private byte[] _fileData;
        private string _note;

        public int Fileid
        {
            get
            {
                return _fileid;
            }

            set
            {
                _fileid = value;
            }
        }

        public string FileName
        {
            get
            {
                return _fileName;
            }

            set
            {
                _fileName = value;
            }
        }

        public string FileFullName
        {
            get
            {
                return _fileFullName;
            }

            set
            {
                _fileFullName = value;
            }
        }
        

        public int UploadUserId
        {
            get
            {
                return _uploadUserId;
            }

            set
            {
                _uploadUserId = value;
            }
        }

        public bool DisableFlag
        {
            get
            {
                return _disableFlag;
            }

            set
            {
                _disableFlag = value;
            }
        }

        public byte[] FileData
        {
            get
            {
                return _fileData;
            }

            set
            {
                _fileData = value;
            }
        }

        public string Note
        {
            get
            {
                return _note;
            }

            set
            {
                _note = value;
            }
        }

        public int Filetypeid
        {
            get
            {
                return _filetypeid;
            }

            set
            {
                _filetypeid = value;
            }
        }

        public string UpLoadDate
        {
            get
            {
                return _upLoadDate.ToString("yyyy-MM-dd HH:mm");
            }

            set
            {
                _upLoadDate = Convert.ToDateTime(value);
            }
        }
        #endregion

        public Hfile()
        {
            Fileid = -1;

        }

        public bool save()
        {
            if(Fileid == -1)
            {
                return false;
            }
            string sqlstr = "insert into T_file (filetypeid,filename,filefullname,uploaddate,uploaduserid,disableflag,filedata,note) values (?filetypeid,?filename,?filefullname,?uploaddate,?uploaduserid,?disableflag,?filedata,?note)";
            MySqlParameter[] para = new MySqlParameter[8];

            para[0] = new MySqlParameter("?filetypeid", MySqlDbType.Int32);
            para[1] = new MySqlParameter("?filename", MySqlDbType.VarChar, 150);
            para[2] = new MySqlParameter("?filefullname", MySqlDbType.VarChar, 250);
            para[3] = new MySqlParameter("?uploaddate", MySqlDbType.DateTime);
            para[4] = new MySqlParameter("?uploaduserid", MySqlDbType.Int32);
            para[5] = new MySqlParameter("?disableflag", MySqlDbType.Int32);
            para[6] = new MySqlParameter("?filedata", MySqlDbType.MediumBlob);
            para[7] = new MySqlParameter("?note", MySqlDbType.VarChar,550);

            para[0].Value = Filetypeid;
            para[1].Value = FileName;
            para[2].Value = FileFullName;
            para[3].Value = UpLoadDate;
            para[4].Value = xuxstatic.xuxSeecion.LOGINUSER.Userid;
            para[5].Value = DisableFlag ? 1 : 0;
            para[6].Value = FileData;
            para[7].Value = Note;

            if (ExecuteNonQuery(sqlstr, para) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Hfile(int fileId)
        {
            string sqlstr = string.Format("SELECT filetypeid,filename,filefullname,uploaddate,uploaduserid,disableflag,filedata,note from T_file WHERE fileid = {0}",Fileid.ToString());

            System.Data.DataTable dt = Select(sqlstr);

            if (dt.Rows.Count > 0)
            {
                Fileid = fileId;
                Filetypeid = Convert.ToInt32( dt.Rows[0].ItemArray[dt.Columns.IndexOf("filetypeid")]);
                FileName = dt.Rows[0].ItemArray[dt.Columns.IndexOf("filename")].ToString();
                FileFullName = dt.Rows[0].ItemArray[dt.Columns.IndexOf("filefullname")].ToString();
                UpLoadDate = dt.Rows[0].ItemArray[dt.Columns.IndexOf("uploaddate")].ToString();
                Filetypeid = Convert.ToInt32(dt.Rows[0].ItemArray[dt.Columns.IndexOf("uploaduserid")]);
                DisableFlag = Convert.ToInt32(dt.Rows[0].ItemArray[dt.Columns.IndexOf("disableflag")]) == 1?true:false;
                FileData = (byte[])dt.Rows[0].ItemArray[dt.Columns.IndexOf("filedata")];
                Note = dt.Rows[0].ItemArray[dt.Columns.IndexOf("note")].ToString();
                
            }
            else
            {
                Fileid = -1;
            }
            
        }


        public bool upLoadFile(string filePath)
        {
            string tempPath;// = @System.AppDomain.CurrentDomain.BaseDirectory + "\\LOG.txt";
            string fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);
            string[] splitName = fileName.Split('.');
            try
            {
                switch (splitName[1])
                {
                    case "xls":
                    case "xlsx":
                        tempPath = string.Format("{0}\\{1}.pdf", @System.AppDomain.CurrentDomain.BaseDirectory, splitName[0]);
                        if (!FConvertEXL(filePath, tempPath))
                        {
                            return false;
                        }
                        break;
                    case "doc":
                    case "docx":
                        tempPath = string.Format("{0}\\{1}.pdf", @System.AppDomain.CurrentDomain.BaseDirectory, splitName[0]);
                        if (!FConvertWORD(filePath, tempPath))
                        {
                            return false;
                        }
                        break;
                    case "pdf":
                        tempPath = string.Format("{0}\\{1}.pdf", @System.AppDomain.CurrentDomain.BaseDirectory, splitName[0]);
                        File.Copy(filePath, tempPath);
                        break;
                    default:
                        return false;
                        break;

                }


                FileStream fs = File.Open(tempPath, FileMode.Open);
                FileData = new byte[fs.Length];
                fs.Read(FileData, 0, FileData.Length);
                Fileid = -1;
                fileName = tempPath.Substring(tempPath.LastIndexOf("\\") + 1);
                FileFullName = tempPath.Substring(tempPath.LastIndexOf(".") + 1).ToLower();
                DisableFlag = true;
                fs.Close();
                File.Delete(tempPath);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        //将word文档转换成PDF格式
        private bool FConvertWORD(string sourcePath, string targetPath
            //, Word.WdExportFormat exportFormat
            )
        {
            Word.WdExportFormat exportFormat = Word.WdExportFormat.wdExportFormatPDF;
            bool result;
            object paramMissing = Type.Missing;
            Word.ApplicationClass wordApplication = new Word.ApplicationClass();
            Word._Document wordDocument = null;
            try
            {
                object paramSourceDocPath = sourcePath;
                string paramExportFilePath = targetPath;

                Word.WdExportFormat paramExportFormat = exportFormat;
                bool paramOpenAfterExport = false;
                Word.WdExportOptimizeFor paramExportOptimizeFor =
                        Word.WdExportOptimizeFor.wdExportOptimizeForPrint;
                Word.WdExportRange paramExportRange = Word.WdExportRange.wdExportAllDocument;
                int paramStartPage = 0;
                int paramEndPage = 0;
                Word.WdExportItem paramExportItem = Word.WdExportItem.wdExportDocumentContent;
                bool paramIncludeDocProps = true;
                bool paramKeepIRM = true;
                Word.WdExportCreateBookmarks paramCreateBookmarks =
                        Word.WdExportCreateBookmarks.wdExportCreateWordBookmarks;
                bool paramDocStructureTags = true;
                bool paramBitmapMissingFonts = true;
                bool paramUseISO19005_1 = false;

                wordDocument = wordApplication.Documents.Open(
                        ref paramSourceDocPath, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing);

                if (wordDocument != null)
                    wordDocument.ExportAsFixedFormat(paramExportFilePath,
                            paramExportFormat, paramOpenAfterExport,
                            paramExportOptimizeFor, paramExportRange, paramStartPage,
                            paramEndPage, paramExportItem, paramIncludeDocProps,
                            paramKeepIRM, paramCreateBookmarks, paramDocStructureTags,
                            paramBitmapMissingFonts, paramUseISO19005_1,
                            ref paramMissing);
                result = true;
            }
            finally
            {
                if (wordDocument != null)
                {
                    wordDocument.Close(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordDocument = null;
                }
                if (wordApplication != null)
                {
                    wordApplication.Quit(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordApplication = null;
                }
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
            }
            return result;
        }

        //将excel文档转换成PDF格式
        private bool FConvertEXL(string sourcePath, string targetPath
            //, XlFixedFormatType targetType
            )
        {
            XlFixedFormatType targetType = XlFixedFormatType.xlTypePDF;
            bool result;
            object missing = Type.Missing;
            Excel.ApplicationClass application = null;
            Workbook workBook = null;
            try
            {
                application = new Excel.ApplicationClass();
                object target = targetPath;
                object type = targetType;
                workBook = application.Workbooks.Open(sourcePath, missing, missing, missing, missing, missing,
                        missing, missing, missing, missing, missing, missing, missing, missing, missing);

                workBook.ExportAsFixedFormat(targetType, target, XlFixedFormatQuality.xlQualityStandard, true, false, missing, missing, missing, missing);
                result = true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close(true, missing, missing);
                    workBook = null;
                }
                if (application != null)
                {
                    application.Quit();
                    application = null;
                }
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
            }
            return result;
        }

        //将ppt文档转换成PDF格式
        private bool FConvertPPT(string sourcePath, string targetPath
            //, PpSaveAsFileType targetFileType
            )
        {
            PpSaveAsFileType targetFileType = PpSaveAsFileType.ppSaveAsPDF;
            bool result;
            object missing = Type.Missing;
            PowerPoint.ApplicationClass application = null;
            Presentation persentation = null;
            try
            {
                application = new PowerPoint.ApplicationClass();
                persentation = application.Presentations.Open(sourcePath, MsoTriState.msoTrue, MsoTriState.msoFalse, MsoTriState.msoFalse);
                persentation.SaveAs(targetPath, targetFileType, Microsoft.Office.Core.MsoTriState.msoTrue);

                result = true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                if (persentation != null)
                {
                    persentation.Close();
                    persentation = null;
                }
                if (application != null)
                {
                    application.Quit();
                    application = null;
                }
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
            }
            return result;
        }
    }
}
