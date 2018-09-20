namespace CompanyManager
{
    partial class FileView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileView));
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.pageView = new O2S.Components.PDFView4NET.PDFPageView();
            this.document = new O2S.Components.PDFView4NET.PDFDocument(this.components);
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.cmsFieldContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiFieldContextMenuProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFieldContextMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsFieldContextMenu.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ofd
            // 
            this.ofd.DefaultExt = "pdf";
            this.ofd.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
            // 
            // pageView
            // 
            this.pageView.AutoScroll = true;
            this.pageView.BackColor = System.Drawing.SystemColors.Window;
            this.pageView.DefaultEllipseAnnotationBorderWidth = 1D;
            this.pageView.DefaultInkAnnotationWidth = 1D;
            this.pageView.DefaultRectangleAnnotationBorderWidth = 1D;
            this.pageView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageView.Document = this.document;
            this.pageView.DownscaleLargeImages = false;
            this.pageView.EnableRepeatedKeys = false;
            this.pageView.Location = new System.Drawing.Point(0, 0);
            this.pageView.Name = "pageView";
            this.pageView.PageDisplayLayout = O2S.Components.PDFView4NET.PDFPageDisplayLayout.OneColumn;
            this.pageView.PageNumber = 0;
            this.pageView.RenderingProgressColor = System.Drawing.Color.Empty;
            this.pageView.RequiredFormFieldHighlightColor = System.Drawing.Color.Empty;
            this.pageView.ScrollPosition = new System.Drawing.Point(0, 0);
            this.pageView.Size = new System.Drawing.Size(989, 510);
            this.pageView.SubstituteFonts = null;
            this.pageView.TabIndex = 1;
            this.pageView.VerticalPageSpacing = 5;
            this.pageView.WorkMode = O2S.Components.PDFView4NET.UserInteractiveWorkMode.PanAndScan;
            this.pageView.WorkModeChanged += new System.EventHandler<System.EventArgs>(this.pageView_WorkModeChanged);
            this.pageView.BeforeFieldAdd += new System.EventHandler<O2S.Components.PDFView4NET.BeforeFieldAddEventArgs>(this.pageView_BeforeFieldAdd);
            this.pageView.AfterFieldAdd += new System.EventHandler<O2S.Components.PDFView4NET.AfterFieldAddEventArgs>(this.pageView_AfterFieldAdd);
            this.pageView.FieldContextMenu += new System.EventHandler<O2S.Components.PDFView4NET.FieldContextMenuEventArgs>(this.pageView_FieldContextMenu);
            // 
            // document
            // 
            this.document.Metadata = null;
            this.document.PageLayout = O2S.Components.PDFView4NET.PDFPageLayout.SinglePage;
            this.document.PageMode = O2S.Components.PDFView4NET.PDFPageMode.UseNone;
            // 
            // sfd
            // 
            this.sfd.DefaultExt = "pdf";
            this.sfd.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
            // 
            // cmsFieldContextMenu
            // 
            this.cmsFieldContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFieldContextMenuProperties,
            this.tsmiFieldContextMenuDelete});
            this.cmsFieldContextMenu.Name = "cmsFieldContextMenu";
            this.cmsFieldContextMenu.Size = new System.Drawing.Size(128, 48);
            // 
            // tsmiFieldContextMenuProperties
            // 
            this.tsmiFieldContextMenuProperties.Name = "tsmiFieldContextMenuProperties";
            this.tsmiFieldContextMenuProperties.Size = new System.Drawing.Size(127, 22);
            this.tsmiFieldContextMenuProperties.Text = "Properties";
            this.tsmiFieldContextMenuProperties.Click += new System.EventHandler(this.tsmiFieldContextMenuProperties_Click);
            // 
            // tsmiFieldContextMenuDelete
            // 
            this.tsmiFieldContextMenuDelete.Name = "tsmiFieldContextMenuDelete";
            this.tsmiFieldContextMenuDelete.Size = new System.Drawing.Size(127, 22);
            this.tsmiFieldContextMenuDelete.Text = "Delete";
            this.tsmiFieldContextMenuDelete.Click += new System.EventHandler(this.tsmiFieldContextMenuDelete_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileOpenToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(989, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileOpenToolStripMenuItem
            // 
            this.fileOpenToolStripMenuItem.Name = "fileOpenToolStripMenuItem";
            this.fileOpenToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.fileOpenToolStripMenuItem.Text = "File open";
            this.fileOpenToolStripMenuItem.Click += new System.EventHandler(this.fileOpenToolStripMenuItem_Click);
            // 
            // FileView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 510);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pageView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FileView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文件查看";
            this.Load += new System.EventHandler(this.AppForm_Load);
            this.cmsFieldContextMenu.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private O2S.Components.PDFView4NET.PDFDocument document;
        private O2S.Components.PDFView4NET.PDFPageView pageView;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.SaveFileDialog sfd;
        private System.Windows.Forms.ContextMenuStrip cmsFieldContextMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiFieldContextMenuProperties;
        private System.Windows.Forms.ToolStripMenuItem tsmiFieldContextMenuDelete;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileOpenToolStripMenuItem;
    }
}

