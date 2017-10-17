namespace DiarMain
{
    partial class EquipmentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EquipmentForm));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.controlNavigator2 = new DevExpress.XtraEditors.ControlNavigator();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.GridGC = new DevExpress.XtraGrid.GridControl();
            this.qEquipmentsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetQuery = new DiarMain.DataSetQuery();
            this.GridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colEquipmentID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubjectID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBranchID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubstationID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEquipmentKindID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEquipmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEquipmentNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReadOnly = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolTip = new DevExpress.Utils.ToolTipController(this.components);
            this.controlNavigator1 = new DevExpress.XtraEditors.ControlNavigator();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.colSubjectName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBranchName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubstationName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.qEquipmentsTableAdapter = new DiarMain.DataSetQueryTableAdapters.QEquipmentsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridGC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qEquipmentsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.controlNavigator2);
            this.panelControl1.Controls.Add(this.controlNavigator1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1164, 59);
            this.panelControl1.TabIndex = 1;
            // 
            // controlNavigator2
            // 
            this.controlNavigator2.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.controlNavigator2.Appearance.Options.UseFont = true;
            this.controlNavigator2.Buttons.Append.Enabled = false;
            this.controlNavigator2.Buttons.Append.Hint = "Добавить запись";
            this.controlNavigator2.Buttons.Append.ImageIndex = 0;
            this.controlNavigator2.Buttons.Append.Visible = false;
            this.controlNavigator2.Buttons.CancelEdit.Hint = "Отменить изменения";
            this.controlNavigator2.Buttons.CancelEdit.ImageIndex = 3;
            this.controlNavigator2.Buttons.CancelEdit.Visible = false;
            this.controlNavigator2.Buttons.Edit.Hint = "Редактировать запись";
            this.controlNavigator2.Buttons.Edit.ImageIndex = 4;
            this.controlNavigator2.Buttons.Edit.Visible = false;
            this.controlNavigator2.Buttons.EndEdit.Hint = "Подтвердить изменения";
            this.controlNavigator2.Buttons.EndEdit.ImageIndex = 2;
            this.controlNavigator2.Buttons.EndEdit.Visible = false;
            this.controlNavigator2.Buttons.First.Visible = false;
            this.controlNavigator2.Buttons.ImageList = this.imageList2;
            this.controlNavigator2.Buttons.Last.Visible = false;
            this.controlNavigator2.Buttons.Next.Visible = false;
            this.controlNavigator2.Buttons.NextPage.Visible = false;
            this.controlNavigator2.Buttons.Prev.Visible = false;
            this.controlNavigator2.Buttons.PrevPage.Visible = false;
            this.controlNavigator2.Buttons.Remove.Hint = "Удалить запись";
            this.controlNavigator2.Buttons.Remove.ImageIndex = 1;
            this.controlNavigator2.Buttons.Remove.Visible = false;
            this.controlNavigator2.CustomButtons.AddRange(new DevExpress.XtraEditors.NavigatorCustomButton[] {
            new DevExpress.XtraEditors.NavigatorCustomButton(-1, 0, true, true, "Визуальное обследование", "Visual"),
            new DevExpress.XtraEditors.NavigatorCustomButton(-1, 1, true, true, "ФХА", "FHA"),
            new DevExpress.XtraEditors.NavigatorCustomButton(-1, 2, true, true, "ХАРГ", "HARG"),
            new DevExpress.XtraEditors.NavigatorCustomButton(-1, 3, true, true, "Тепловизионный контроль", "Warm"),
            new DevExpress.XtraEditors.NavigatorCustomButton(-1, 4, true, true, "Вибрационное обследование", "Vibro"),
            new DevExpress.XtraEditors.NavigatorCustomButton(-1, 5, true, true, "Определение характеристик", "Parameter"),
            new DevExpress.XtraEditors.NavigatorCustomButton(-1, 6, true, true, "Электрические измерения", "Electrical")});
            this.controlNavigator2.Location = new System.Drawing.Point(247, 6);
            this.controlNavigator2.LookAndFeel.UseDefaultLookAndFeel = false;
            this.controlNavigator2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.controlNavigator2.Name = "controlNavigator2";
            this.controlNavigator2.NavigatableControl = this.GridGC;
            this.controlNavigator2.ShowToolTips = true;
            this.controlNavigator2.Size = new System.Drawing.Size(595, 48);
            this.controlNavigator2.TabIndex = 3;
            this.controlNavigator2.Text = "controlNavigator2";
            this.controlNavigator2.ToolTipController = this.toolTip;
            this.controlNavigator2.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.controlNavigator2_ButtonClick);
            this.controlNavigator2.Click += new System.EventHandler(this.controlNavigator2_Click);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "visual.png");
            this.imageList2.Images.SetKeyName(1, "FHA.png");
            this.imageList2.Images.SetKeyName(2, "HARG.png");
            this.imageList2.Images.SetKeyName(3, "warm.png");
            this.imageList2.Images.SetKeyName(4, "vibro.png");
            this.imageList2.Images.SetKeyName(5, "parameter_small.jpg");
            this.imageList2.Images.SetKeyName(6, "electrical_small.jpg");
            // 
            // GridGC
            // 
            this.GridGC.DataSource = this.qEquipmentsBindingSource;
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
            // qEquipmentsBindingSource
            // 
            this.qEquipmentsBindingSource.DataMember = "QEquipments";
            this.qEquipmentsBindingSource.DataSource = this.dataSetQuery;
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
            this.colEquipmentID,
            this.colSubjectID,
            this.colBranchID,
            this.colSubstationID,
            this.colEquipmentKindID,
            this.colEquipmentName,
            this.colEquipmentNumber,
            this.colReadOnly});
            this.GridView.CustomizationFormBounds = new System.Drawing.Rectangle(408, 375, 216, 199);
            this.GridView.GridControl = this.GridGC;
            this.GridView.Name = "GridView";
            this.GridView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.GridView.OptionsDetail.EnableMasterViewMode = false;
            this.GridView.OptionsFilter.AllowFilterEditor = false;
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
            // colEquipmentID
            // 
            this.colEquipmentID.FieldName = "EquipmentID";
            this.colEquipmentID.Name = "colEquipmentID";
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
            // colEquipmentKindID
            // 
            this.colEquipmentKindID.Caption = "Вид оборудования";
            this.colEquipmentKindID.FieldName = "EquipmentClassName";
            this.colEquipmentKindID.Name = "colEquipmentKindID";
            this.colEquipmentKindID.OptionsColumn.AllowEdit = false;
            this.colEquipmentKindID.OptionsColumn.AllowMove = false;
            this.colEquipmentKindID.Visible = true;
            this.colEquipmentKindID.VisibleIndex = 3;
            this.colEquipmentKindID.Width = 160;
            // 
            // colEquipmentName
            // 
            this.colEquipmentName.Caption = "Дисп. наименование";
            this.colEquipmentName.FieldName = "EquipmentName";
            this.colEquipmentName.Name = "colEquipmentName";
            this.colEquipmentName.OptionsColumn.AllowEdit = false;
            this.colEquipmentName.OptionsColumn.AllowMove = false;
            this.colEquipmentName.OptionsFilter.AllowFilter = false;
            this.colEquipmentName.Visible = true;
            this.colEquipmentName.VisibleIndex = 4;
            this.colEquipmentName.Width = 180;
            // 
            // colEquipmentNumber
            // 
            this.colEquipmentNumber.Caption = "Заводской номер";
            this.colEquipmentNumber.FieldName = "EquipmentNumber";
            this.colEquipmentNumber.Name = "colEquipmentNumber";
            this.colEquipmentNumber.OptionsColumn.AllowEdit = false;
            this.colEquipmentNumber.OptionsColumn.AllowMove = false;
            this.colEquipmentNumber.OptionsFilter.AllowFilter = false;
            this.colEquipmentNumber.Visible = true;
            this.colEquipmentNumber.VisibleIndex = 5;
            this.colEquipmentNumber.Width = 150;
            // 
            // colReadOnly
            // 
            this.colReadOnly.AppearanceCell.Options.UseTextOptions = true;
            this.colReadOnly.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colReadOnly.AppearanceHeader.Options.UseTextOptions = true;
            this.colReadOnly.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colReadOnly.Caption = "Доступ ограничен";
            this.colReadOnly.FieldName = "ReadOnlyStr";
            this.colReadOnly.Name = "colReadOnly";
            this.colReadOnly.OptionsColumn.AllowMove = false;
            this.colReadOnly.OptionsColumn.ReadOnly = true;
            this.colReadOnly.OptionsFilter.AllowFilter = false;
            this.colReadOnly.Width = 150;
            // 
            // toolTip
            // 
            this.toolTip.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolTip.Appearance.Options.UseFont = true;
            this.toolTip.Rounded = true;
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
            // qEquipmentsTableAdapter
            // 
            this.qEquipmentsTableAdapter.ClearBeforeFill = true;
            // 
            // EquipmentForm
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
            this.Name = "EquipmentForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Оборудование";
            this.Load += new System.EventHandler(this.EquipmentForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridGC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qEquipmentsBindingSource)).EndInit();
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
        private System.Windows.Forms.BindingSource qEquipmentsBindingSource;
        private DataSetQueryTableAdapters.QEquipmentsTableAdapter qEquipmentsTableAdapter;
        private DevExpress.XtraGrid.Columns.GridColumn colEquipmentID;
        private DevExpress.XtraGrid.Columns.GridColumn colSubjectID;
        private DevExpress.XtraGrid.Columns.GridColumn colBranchID;
        private DevExpress.XtraGrid.Columns.GridColumn colSubstationID;
        private DevExpress.XtraGrid.Columns.GridColumn colEquipmentName;
        private DevExpress.XtraGrid.Columns.GridColumn colEquipmentNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colReadOnly;
        /*private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repYesNo;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit4;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit5;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;*/
        private DevExpress.XtraGrid.Columns.GridColumn colSubjectName;
        private DevExpress.XtraGrid.Columns.GridColumn colBranchName;
        private DevExpress.XtraGrid.Columns.GridColumn colSubstationName;
        private DevExpress.XtraGrid.Columns.GridColumn colEquipmentKindID;
        private DevExpress.XtraEditors.ControlNavigator controlNavigator2;
        private System.Windows.Forms.ImageList imageList2;
        //private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit6;
    }
}