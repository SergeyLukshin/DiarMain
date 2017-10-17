namespace DiarMain
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.bCancel = new DevExpress.XtraEditors.SimpleButton();
            this.bActivation = new DevExpress.XtraEditors.SimpleButton();
            this.tabSettings = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.GridGC = new DevExpress.XtraGrid.GridControl();
            this.GridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repCheck = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colModuleName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnClearPicture = new DevExpress.XtraEditors.SimpleButton();
            this.btnChangePicture = new DevExpress.XtraEditors.SimpleButton();
            this.peImage = new DevExpress.XtraEditors.PictureEdit();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.GridMsg = new DevExpress.XtraGrid.GridControl();
            this.gridViewMsg = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repImage = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnLoad = new DevExpress.XtraEditors.SimpleButton();
            this.tePath = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.openFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.imageListMsg = new System.Windows.Forms.ImageList(this.components);
            this.worker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.tabSettings)).BeginInit();
            this.tabSettings.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridGC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCheck)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peImage.Properties)).BeginInit();
            this.xtraTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridMsg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMsg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePath.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // bCancel
            // 
            this.bCancel.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bCancel.Appearance.Options.UseFont = true;
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Image = ((System.Drawing.Image)(resources.GetObject("bCancel.Image")));
            this.bCancel.Location = new System.Drawing.Point(637, 350);
            this.bCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(120, 33);
            this.bCancel.TabIndex = 3;
            this.bCancel.Text = "Отменить";
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bActivation
            // 
            this.bActivation.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bActivation.Appearance.Options.UseFont = true;
            this.bActivation.Image = ((System.Drawing.Image)(resources.GetObject("bActivation.Image")));
            this.bActivation.Location = new System.Drawing.Point(504, 350);
            this.bActivation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bActivation.Name = "bActivation";
            this.bActivation.Size = new System.Drawing.Size(125, 33);
            this.bActivation.TabIndex = 2;
            this.bActivation.Text = "Сохранить";
            this.bActivation.Click += new System.EventHandler(this.bActivation_Click);
            // 
            // tabSettings
            // 
            this.tabSettings.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.tabSettings.Appearance.Options.UseFont = true;
            this.tabSettings.AppearancePage.Header.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.tabSettings.AppearancePage.Header.Options.UseFont = true;
            this.tabSettings.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.tabSettings.AppearancePage.HeaderActive.Options.UseFont = true;
            this.tabSettings.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.tabSettings.AppearancePage.HeaderDisabled.Options.UseFont = true;
            this.tabSettings.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.tabSettings.AppearancePage.HeaderHotTracked.Options.UseFont = true;
            this.tabSettings.AppearancePage.PageClient.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.tabSettings.AppearancePage.PageClient.Options.UseFont = true;
            this.tabSettings.Location = new System.Drawing.Point(12, 12);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedTabPage = this.xtraTabPage1;
            this.tabSettings.Size = new System.Drawing.Size(749, 334);
            this.tabSettings.TabIndex = 4;
            this.tabSettings.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3});
            this.tabSettings.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tabSettings_SelectedPageChanged);
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.GridGC);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(743, 301);
            this.xtraTabPage1.Text = "Подключенные модули";
            // 
            // GridGC
            // 
            this.GridGC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridGC.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GridGC.Location = new System.Drawing.Point(0, 0);
            this.GridGC.MainView = this.GridView;
            this.GridGC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GridGC.Name = "GridGC";
            this.GridGC.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repCheck});
            this.GridGC.Size = new System.Drawing.Size(743, 301);
            this.GridGC.TabIndex = 3;
            this.GridGC.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridView});
            // 
            // GridView
            // 
            this.GridView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GridView.Appearance.HeaderPanel.Options.UseFont = true;
            this.GridView.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GridView.Appearance.Row.Options.UseFont = true;
            this.GridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCheck,
            this.colModuleName});
            this.GridView.GridControl = this.GridGC;
            this.GridView.Name = "GridView";
            this.GridView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.GridView.OptionsDetail.EnableMasterViewMode = false;
            this.GridView.OptionsMenu.EnableColumnMenu = false;
            this.GridView.OptionsNavigation.AutoFocusNewRow = true;
            this.GridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.GridView.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.GridView.OptionsSelection.EnableAppearanceHideSelection = false;
            this.GridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.GridView.OptionsView.ShowGroupPanel = false;
            this.GridView.OptionsView.ShowIndicator = false;
            // 
            // colCheck
            // 
            this.colCheck.Caption = " ";
            this.colCheck.ColumnEdit = this.repCheck;
            this.colCheck.FieldName = "CHECK";
            this.colCheck.Name = "colCheck";
            this.colCheck.OptionsColumn.AllowMove = false;
            this.colCheck.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colCheck.OptionsFilter.AllowAutoFilter = false;
            this.colCheck.OptionsFilter.AllowFilter = false;
            this.colCheck.Visible = true;
            this.colCheck.VisibleIndex = 0;
            this.colCheck.Width = 45;
            // 
            // repCheck
            // 
            this.repCheck.AutoHeight = false;
            this.repCheck.Name = "repCheck";
            // 
            // colModuleName
            // 
            this.colModuleName.Caption = "Модуль";
            this.colModuleName.FieldName = "NAME";
            this.colModuleName.Name = "colModuleName";
            this.colModuleName.OptionsColumn.AllowEdit = false;
            this.colModuleName.OptionsColumn.AllowMove = false;
            this.colModuleName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colModuleName.OptionsColumn.ReadOnly = true;
            this.colModuleName.OptionsFilter.AllowAutoFilter = false;
            this.colModuleName.OptionsFilter.AllowFilter = false;
            this.colModuleName.Visible = true;
            this.colModuleName.VisibleIndex = 1;
            this.colModuleName.Width = 696;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.labelControl4);
            this.xtraTabPage2.Controls.Add(this.btnClearPicture);
            this.xtraTabPage2.Controls.Add(this.btnChangePicture);
            this.xtraTabPage2.Controls.Add(this.peImage);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(743, 301);
            this.xtraTabPage2.Text = "Основные параметры";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControl4.Location = new System.Drawing.Point(13, 10);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(232, 20);
            this.labelControl4.TabIndex = 26;
            this.labelControl4.Text = "Изображение основного экрана:";
            // 
            // btnClearPicture
            // 
            this.btnClearPicture.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClearPicture.Appearance.Options.UseFont = true;
            this.btnClearPicture.Image = ((System.Drawing.Image)(resources.GetObject("btnClearPicture.Image")));
            this.btnClearPicture.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnClearPicture.Location = new System.Drawing.Point(198, 76);
            this.btnClearPicture.LookAndFeel.SkinName = "Caramel";
            this.btnClearPicture.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClearPicture.Name = "btnClearPicture";
            this.btnClearPicture.Size = new System.Drawing.Size(32, 31);
            this.btnClearPicture.TabIndex = 11;
            this.btnClearPicture.ToolTip = "Удалить изображение";
            this.btnClearPicture.Click += new System.EventHandler(this.btnClearPicture_Click);
            // 
            // btnChangePicture
            // 
            this.btnChangePicture.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnChangePicture.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnChangePicture.Appearance.Options.UseFont = true;
            this.btnChangePicture.Appearance.Options.UseForeColor = true;
            this.btnChangePicture.Image = ((System.Drawing.Image)(resources.GetObject("btnChangePicture.Image")));
            this.btnChangePicture.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnChangePicture.Location = new System.Drawing.Point(198, 41);
            this.btnChangePicture.LookAndFeel.SkinName = "Caramel";
            this.btnChangePicture.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnChangePicture.Name = "btnChangePicture";
            this.btnChangePicture.Size = new System.Drawing.Size(32, 31);
            this.btnChangePicture.TabIndex = 10;
            this.btnChangePicture.ToolTip = "Изменить изображение";
            this.btnChangePicture.Click += new System.EventHandler(this.btnChangePicture_Click_1);
            // 
            // peImage
            // 
            this.peImage.Location = new System.Drawing.Point(13, 41);
            this.peImage.Name = "peImage";
            this.peImage.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.peImage.Properties.Appearance.Options.UseFont = true;
            this.peImage.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.peImage.Properties.AppearanceDisabled.Options.UseFont = true;
            this.peImage.Properties.AppearanceFocused.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.peImage.Properties.AppearanceFocused.Options.UseFont = true;
            this.peImage.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.peImage.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.peImage.Properties.NullText = "нет изображения";
            this.peImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.peImage.Size = new System.Drawing.Size(178, 126);
            this.peImage.TabIndex = 9;
            this.peImage.DoubleClick += new System.EventHandler(this.peImage_DoubleClick);
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.GridMsg);
            this.xtraTabPage3.Controls.Add(this.btnLoad);
            this.xtraTabPage3.Controls.Add(this.tePath);
            this.xtraTabPage3.Controls.Add(this.labelControl1);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(743, 301);
            this.xtraTabPage3.Text = "Импорт данных";
            // 
            // GridMsg
            // 
            this.GridMsg.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GridMsg.Location = new System.Drawing.Point(13, 45);
            this.GridMsg.MainView = this.gridViewMsg;
            this.GridMsg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GridMsg.Name = "GridMsg";
            this.GridMsg.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repImage});
            this.GridMsg.Size = new System.Drawing.Size(723, 249);
            this.GridMsg.TabIndex = 30;
            this.GridMsg.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMsg});
            // 
            // gridViewMsg
            // 
            this.gridViewMsg.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gridViewMsg.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewMsg.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gridViewMsg.Appearance.Row.Options.UseFont = true;
            this.gridViewMsg.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn2});
            this.gridViewMsg.GridControl = this.GridMsg;
            this.gridViewMsg.Name = "gridViewMsg";
            this.gridViewMsg.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.gridViewMsg.OptionsDetail.EnableMasterViewMode = false;
            this.gridViewMsg.OptionsMenu.EnableColumnMenu = false;
            this.gridViewMsg.OptionsNavigation.AutoFocusNewRow = true;
            this.gridViewMsg.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewMsg.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridViewMsg.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gridViewMsg.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridViewMsg.OptionsView.ColumnAutoWidth = false;
            this.gridViewMsg.OptionsView.ShowGroupPanel = false;
            this.gridViewMsg.OptionsView.ShowIndicator = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = " ";
            this.gridColumn1.ColumnEdit = this.repImage;
            this.gridColumn1.FieldName = "IMAGE";
            this.gridColumn1.ImageAlignment = System.Drawing.StringAlignment.Center;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowMove = false;
            this.gridColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn1.OptionsFilter.AllowFilter = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 30;
            // 
            // repImage
            // 
            this.repImage.Name = "repImage";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Время";
            this.gridColumn3.FieldName = "TIME";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowMove = false;
            this.gridColumn3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.ReadOnly = true;
            this.gridColumn3.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn3.OptionsFilter.AllowFilter = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 65;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Сообщение";
            this.gridColumn2.FieldName = "MSG";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowMove = false;
            this.gridColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn2.OptionsFilter.AllowFilter = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 1000;
            // 
            // btnLoad
            // 
            this.btnLoad.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnLoad.Appearance.Options.UseFont = true;
            this.btnLoad.Image = ((System.Drawing.Image)(resources.GetObject("btnLoad.Image")));
            this.btnLoad.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnLoad.Location = new System.Drawing.Point(691, 10);
            this.btnLoad.LookAndFeel.SkinName = "Caramel";
            this.btnLoad.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(45, 27);
            this.btnLoad.TabIndex = 29;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // tePath
            // 
            this.tePath.Location = new System.Drawing.Point(111, 11);
            this.tePath.Name = "tePath";
            this.tePath.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tePath.Properties.Appearance.Options.UseFont = true;
            this.tePath.Properties.ReadOnly = true;
            this.tePath.Size = new System.Drawing.Size(572, 26);
            this.tePath.TabIndex = 28;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControl1.Location = new System.Drawing.Point(13, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(82, 20);
            this.labelControl1.TabIndex = 27;
            this.labelControl1.Text = "Путь к базе:";
            // 
            // openFileDlg
            // 
            this.openFileDlg.DefaultExt = "sqlite";
            this.openFileDlg.FileName = "diar.sqlite";
            this.openFileDlg.Filter = "SQLite файлы|*.sqlite";
            this.openFileDlg.Title = "Укажите файл импортируемой базы данных";
            // 
            // imageListMsg
            // 
            this.imageListMsg.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMsg.ImageStream")));
            this.imageListMsg.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListMsg.Images.SetKeyName(0, "stop1.ico");
            this.imageListMsg.Images.SetKeyName(1, "err1.ico");
            this.imageListMsg.Images.SetKeyName(2, "ico00002.ico");
            this.imageListMsg.Images.SetKeyName(3, "ico00003.ico");
            // 
            // worker
            // 
            this.worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_DoWork);
            this.worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.worker_RunWorkerCompleted);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.bActivation;
            this.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(763, 389);
            this.Controls.Add(this.tabSettings);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bActivation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabSettings)).EndInit();
            this.tabSettings.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridGC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCheck)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peImage.Properties)).EndInit();
            this.xtraTabPage3.ResumeLayout(false);
            this.xtraTabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridMsg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMsg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePath.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton bCancel;
        private DevExpress.XtraEditors.SimpleButton bActivation;
        private DevExpress.XtraTab.XtraTabControl tabSettings;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraGrid.GridControl GridGC;
        private DevExpress.XtraGrid.Views.Grid.GridView GridView;
        private DevExpress.XtraGrid.Columns.GridColumn colCheck;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repCheck;
        private DevExpress.XtraGrid.Columns.GridColumn colModuleName;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private DevExpress.XtraEditors.SimpleButton btnClearPicture;
        private DevExpress.XtraEditors.SimpleButton btnChangePicture;
        private DevExpress.XtraEditors.PictureEdit peImage;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit tePath;
        private DevExpress.XtraEditors.SimpleButton btnLoad;
        private System.Windows.Forms.OpenFileDialog openFileDlg;
        private DevExpress.XtraGrid.GridControl GridMsg;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMsg;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repImage;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private System.Windows.Forms.ImageList imageListMsg;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private System.ComponentModel.BackgroundWorker worker;
    }
}