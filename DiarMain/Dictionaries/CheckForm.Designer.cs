namespace DiarMain
{
    partial class CheckForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckForm));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.controlNavigator1 = new DevExpress.XtraEditors.ControlNavigator();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.GridGC = new DevExpress.XtraGrid.GridControl();
            this.qChecksBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetQuery = new DiarMain.DataSetQuery();
            this.GridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCheckID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubjectID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBranchID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubstationID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateBegin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateEnd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolTip = new DevExpress.Utils.ToolTipController(this.components);
            this.colSubjectName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBranchName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubstationName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.qChecksTableAdapter = new DiarMain.DataSetQueryTableAdapters.QChecksTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridGC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qChecksBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.controlNavigator1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1164, 59);
            this.panelControl1.TabIndex = 1;
            // 
            // controlNavigator1
            // 
            this.controlNavigator1.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.controlNavigator1.Appearance.Options.UseFont = true;
            this.controlNavigator1.Buttons.Append.Hint = "Добавить запись";
            this.controlNavigator1.Buttons.Append.ImageIndex = 0;
            this.controlNavigator1.Buttons.CancelEdit.Hint = "Отменить изменения";
            this.controlNavigator1.Buttons.CancelEdit.ImageIndex = 3;
            this.controlNavigator1.Buttons.CancelEdit.Visible = false;
            this.controlNavigator1.Buttons.Edit.Hint = "Редактировать запись";
            this.controlNavigator1.Buttons.Edit.ImageIndex = 4;
            this.controlNavigator1.Buttons.Edit.Visible = false;
            this.controlNavigator1.Buttons.EndEdit.Hint = "Подтвердить изменения";
            this.controlNavigator1.Buttons.EndEdit.ImageIndex = 2;
            this.controlNavigator1.Buttons.EndEdit.Visible = false;
            this.controlNavigator1.Buttons.First.Visible = false;
            this.controlNavigator1.Buttons.ImageList = this.imageList1;
            this.controlNavigator1.Buttons.Last.Visible = false;
            this.controlNavigator1.Buttons.Next.Visible = false;
            this.controlNavigator1.Buttons.NextPage.Visible = false;
            this.controlNavigator1.Buttons.Prev.Visible = false;
            this.controlNavigator1.Buttons.PrevPage.Visible = false;
            this.controlNavigator1.Buttons.Remove.Hint = "Удалить запись";
            this.controlNavigator1.Buttons.Remove.ImageIndex = 1;
            this.controlNavigator1.CustomButtons.AddRange(new DevExpress.XtraEditors.NavigatorCustomButton[] {
            new DevExpress.XtraEditors.NavigatorCustomButton(8, 4, "Редактировать  запись")});
            this.controlNavigator1.Location = new System.Drawing.Point(7, 6);
            this.controlNavigator1.LookAndFeel.SkinName = "Caramel";
            this.controlNavigator1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.controlNavigator1.Name = "controlNavigator1";
            this.controlNavigator1.NavigatableControl = this.GridGC;
            this.controlNavigator1.ShowToolTips = true;
            this.controlNavigator1.Size = new System.Drawing.Size(232, 48);
            this.controlNavigator1.TabIndex = 2;
            this.controlNavigator1.Text = "controlNavigator1";
            this.controlNavigator1.ToolTipController = this.toolTip;
            this.controlNavigator1.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.controlNavigator1_ButtonClick);
            this.controlNavigator1.Click += new System.EventHandler(this.controlNavigator1_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "03.png");
            this.imageList1.Images.SetKeyName(1, "04.png");
            this.imageList1.Images.SetKeyName(2, "02_.png");
            this.imageList1.Images.SetKeyName(3, "01_.png");
            this.imageList1.Images.SetKeyName(4, "19_.png");
            this.imageList1.Images.SetKeyName(5, "34.png");
            // 
            // GridGC
            // 
            this.GridGC.DataSource = this.qChecksBindingSource;
            this.GridGC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridGC.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GridGC.Location = new System.Drawing.Point(0, 59);
            this.GridGC.MainView = this.GridView;
            this.GridGC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GridGC.Name = "GridGC";
            this.GridGC.Size = new System.Drawing.Size(1164, 526);
            this.GridGC.TabIndex = 2;
            this.GridGC.ToolTipController = this.toolTip;
            this.GridGC.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridView});
            // 
            // qChecksBindingSource
            // 
            this.qChecksBindingSource.DataMember = "QChecks";
            this.qChecksBindingSource.DataSource = this.dataSetQuery;
            // 
            // dataSetQuery
            // 
            this.dataSetQuery.DataSetName = "DataSetQuery";
            this.dataSetQuery.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // GridView
            // 
            this.GridView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GridView.Appearance.HeaderPanel.Options.UseFont = true;
            this.GridView.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GridView.Appearance.Row.Options.UseFont = true;
            this.GridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCheckID,
            this.colSubjectID,
            this.colBranchID,
            this.colSubstationID,
            this.colDateBegin,
            this.colDateEnd});
            this.GridView.CustomizationFormBounds = new System.Drawing.Rectangle(408, 375, 216, 199);
            this.GridView.GridControl = this.GridGC;
            this.GridView.Name = "GridView";
            this.GridView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.GridView.OptionsDetail.EnableMasterViewMode = false;
            this.GridView.OptionsMenu.EnableColumnMenu = false;
            this.GridView.OptionsNavigation.AutoFocusNewRow = true;
            this.GridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.GridView.OptionsSelection.EnableAppearanceHideSelection = false;
            this.GridView.OptionsView.ShowGroupPanel = false;
            this.GridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.GridView_FocusedRowChanged);
            this.GridView.ShowFilterPopupListBox += new DevExpress.XtraGrid.Views.Grid.FilterPopupListBoxEventHandler(this.GridView_ShowFilterPopupListBox);
            this.GridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridViewView_KeyDown);
            this.GridView.DoubleClick += new System.EventHandler(this.GridView_DoubleClick);
            // 
            // colCheckID
            // 
            this.colCheckID.FieldName = "CheckID";
            this.colCheckID.Name = "colCheckID";
            // 
            // colSubjectID
            // 
            this.colSubjectID.Caption = "Субъект";
            this.colSubjectID.FieldName = "SubjectName";
            this.colSubjectID.Name = "colSubjectID";
            this.colSubjectID.OptionsColumn.AllowEdit = false;
            this.colSubjectID.OptionsColumn.AllowMove = false;
            this.colSubjectID.Visible = true;
            this.colSubjectID.VisibleIndex = 0;
            this.colSubjectID.Width = 130;
            // 
            // colBranchID
            // 
            this.colBranchID.Caption = "Филиал";
            this.colBranchID.FieldName = "BranchName";
            this.colBranchID.Name = "colBranchID";
            this.colBranchID.OptionsColumn.AllowEdit = false;
            this.colBranchID.OptionsColumn.AllowMove = false;
            this.colBranchID.Visible = true;
            this.colBranchID.VisibleIndex = 1;
            this.colBranchID.Width = 180;
            // 
            // colSubstationID
            // 
            this.colSubstationID.Caption = "Подстанция/станция";
            this.colSubstationID.FieldName = "SubstationName";
            this.colSubstationID.Name = "colSubstationID";
            this.colSubstationID.OptionsColumn.AllowEdit = false;
            this.colSubstationID.OptionsColumn.AllowMove = false;
            this.colSubstationID.Visible = true;
            this.colSubstationID.VisibleIndex = 2;
            this.colSubstationID.Width = 150;
            // 
            // colDateBegin
            // 
            this.colDateBegin.Caption = "Дата начала проверки";
            this.colDateBegin.FieldName = "CheckDateBegin";
            this.colDateBegin.Name = "colDateBegin";
            this.colDateBegin.OptionsColumn.AllowEdit = false;
            this.colDateBegin.OptionsColumn.AllowMove = false;
            this.colDateBegin.Visible = true;
            this.colDateBegin.VisibleIndex = 3;
            this.colDateBegin.Width = 160;
            // 
            // colDateEnd
            // 
            this.colDateEnd.Caption = "Дата окончания проверки";
            this.colDateEnd.FieldName = "CheckDateEnd";
            this.colDateEnd.Name = "colDateEnd";
            this.colDateEnd.OptionsColumn.AllowEdit = false;
            this.colDateEnd.OptionsColumn.AllowMove = false;
            this.colDateEnd.Visible = true;
            this.colDateEnd.VisibleIndex = 4;
            this.colDateEnd.Width = 180;
            // 
            // toolTip
            // 
            this.toolTip.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolTip.Appearance.Options.UseFont = true;
            this.toolTip.Rounded = true;
            // 
            // colSubjectName
            // 
            this.colSubjectName.Caption = "Субъект";
            this.colSubjectName.FieldName = "SubjectName";
            this.colSubjectName.Name = "colSubjectName";
            this.colSubjectName.OptionsColumn.AllowEdit = false;
            this.colSubjectName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colSubjectName.OptionsColumn.AllowMove = false;
            this.colSubjectName.Visible = true;
            this.colSubjectName.VisibleIndex = 0;
            this.colSubjectName.Width = 150;
            // 
            // colBranchName
            // 
            this.colBranchName.Caption = "Филиал";
            this.colBranchName.FieldName = "BranchName";
            this.colBranchName.Name = "colBranchName";
            this.colBranchName.OptionsColumn.AllowEdit = false;
            this.colBranchName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colBranchName.OptionsColumn.AllowMove = false;
            this.colBranchName.Visible = true;
            this.colBranchName.VisibleIndex = 1;
            this.colBranchName.Width = 200;
            // 
            // colSubstationName
            // 
            this.colSubstationName.Caption = "Подстанция";
            this.colSubstationName.FieldName = "SubstationName";
            this.colSubstationName.Name = "colSubstationName";
            this.colSubstationName.OptionsColumn.AllowEdit = false;
            this.colSubstationName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colSubstationName.OptionsColumn.AllowMove = false;
            this.colSubstationName.Visible = true;
            this.colSubstationName.VisibleIndex = 2;
            this.colSubstationName.Width = 200;
            // 
            // qChecksTableAdapter
            // 
            this.qChecksTableAdapter.ClearBeforeFill = true;
            // 
            // CheckForm
            // 
            this.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 585);
            this.Controls.Add(this.GridGC);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Caramel";
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimizeBox = false;
            this.Name = "CheckForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Проверки";
            this.Load += new System.EventHandler(this.CheckForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridGC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qChecksBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraGrid.GridControl GridGC;
        private DevExpress.XtraGrid.Views.Grid.GridView GridView;
        private DevExpress.XtraEditors.ControlNavigator controlNavigator1;
        private DataSetQuery dataSetQuery;
        /*private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit2View;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;*/
        private DevExpress.Utils.ToolTipController toolTip;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckID;
        private DevExpress.XtraGrid.Columns.GridColumn colSubjectID;
        private DevExpress.XtraGrid.Columns.GridColumn colBranchID;
        private DevExpress.XtraGrid.Columns.GridColumn colSubstationID;
        private DevExpress.XtraGrid.Columns.GridColumn colDateEnd;
        /*private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repYesNo;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit4;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit5;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;*/
        private DevExpress.XtraGrid.Columns.GridColumn colSubjectName;
        private DevExpress.XtraGrid.Columns.GridColumn colBranchName;
        private DevExpress.XtraGrid.Columns.GridColumn colSubstationName;
        private DevExpress.XtraGrid.Columns.GridColumn colDateBegin;
        private System.Windows.Forms.BindingSource qChecksBindingSource;
        private DataSetQueryTableAdapters.QChecksTableAdapter qChecksTableAdapter;
        //private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit6;
    }
}