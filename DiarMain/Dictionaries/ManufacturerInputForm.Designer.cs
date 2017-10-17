namespace DiarMain
{
    partial class ManufacturerInputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManufacturerInputForm));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.cbCanEdit = new DevExpress.XtraEditors.CheckEdit();
            this.controlNavigator1 = new DevExpress.XtraEditors.ControlNavigator();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.GridGC = new DevExpress.XtraGrid.GridControl();
            this.qManufacturersInputsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetQuery = new DiarMain.DataSetQuery();
            this.GridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBranchID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEquipmentTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEquipmentKindID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.equipmentKindsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetMain = new DiarMain.DataSetMain();
            this.colReadOnly = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repYesNo = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.toolTip = new DevExpress.Utils.ToolTipController(this.components);
            this.panelSelect = new DevExpress.XtraEditors.PanelControl();
            this.bSelect = new DevExpress.XtraEditors.SimpleButton();
            this.qManufacturersInputsTableAdapter = new DiarMain.DataSetQueryTableAdapters.QManufacturersInputsTableAdapter();
            this.equipmentKindsTableAdapter = new DiarMain.DataSetMainTableAdapters.EquipmentKindsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbCanEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridGC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qManufacturersInputsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipmentKindsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repYesNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelSelect)).BeginInit();
            this.panelSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.cbCanEdit);
            this.panelControl1.Controls.Add(this.controlNavigator1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(793, 58);
            this.panelControl1.TabIndex = 1;
            // 
            // cbCanEdit
            // 
            this.cbCanEdit.Location = new System.Drawing.Point(413, 18);
            this.cbCanEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbCanEdit.Name = "cbCanEdit";
            this.cbCanEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbCanEdit.Properties.Appearance.Options.UseFont = true;
            this.cbCanEdit.Properties.Caption = "разрешить исправление";
            this.cbCanEdit.Size = new System.Drawing.Size(256, 25);
            this.cbCanEdit.TabIndex = 3;
            this.cbCanEdit.CheckedChanged += new System.EventHandler(this.cbCanEdit_CheckedChanged);
            this.cbCanEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridView_KeyDown);
            // 
            // controlNavigator1
            // 
            this.controlNavigator1.Buttons.Append.Hint = "Добавить запись";
            this.controlNavigator1.Buttons.Append.ImageIndex = 0;
            this.controlNavigator1.Buttons.CancelEdit.Hint = "Отменить изменения";
            this.controlNavigator1.Buttons.CancelEdit.ImageIndex = 3;
            this.controlNavigator1.Buttons.Edit.Hint = "Редактировать запись";
            this.controlNavigator1.Buttons.Edit.ImageIndex = 4;
            this.controlNavigator1.Buttons.EndEdit.Hint = "Подтвердить изменения";
            this.controlNavigator1.Buttons.EndEdit.ImageIndex = 2;
            this.controlNavigator1.Buttons.First.Visible = false;
            this.controlNavigator1.Buttons.ImageList = this.imageList1;
            this.controlNavigator1.Buttons.Last.Visible = false;
            this.controlNavigator1.Buttons.Next.Visible = false;
            this.controlNavigator1.Buttons.NextPage.Visible = false;
            this.controlNavigator1.Buttons.Prev.Visible = false;
            this.controlNavigator1.Buttons.PrevPage.Visible = false;
            this.controlNavigator1.Buttons.Remove.Hint = "Удалить запись";
            this.controlNavigator1.Buttons.Remove.ImageIndex = 1;
            this.controlNavigator1.Location = new System.Drawing.Point(7, 6);
            this.controlNavigator1.LookAndFeel.SkinName = "Caramel";
            this.controlNavigator1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.controlNavigator1.Name = "controlNavigator1";
            this.controlNavigator1.NavigatableControl = this.GridGC;
            this.controlNavigator1.ShowToolTips = true;
            this.controlNavigator1.Size = new System.Drawing.Size(384, 47);
            this.controlNavigator1.TabIndex = 2;
            this.controlNavigator1.Text = "controlNavigator1";
            this.controlNavigator1.ToolTipController = this.toolTip;
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
            this.GridGC.DataSource = this.qManufacturersInputsBindingSource;
            this.GridGC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridGC.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GridGC.Location = new System.Drawing.Point(0, 58);
            this.GridGC.MainView = this.GridView;
            this.GridGC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GridGC.Name = "GridGC";
            this.GridGC.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit1,
            this.repYesNo});
            this.GridGC.Size = new System.Drawing.Size(793, 479);
            this.GridGC.TabIndex = 6;
            this.GridGC.ToolTipController = this.toolTip;
            this.GridGC.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridView});
            // 
            // qManufacturersInputsBindingSource
            // 
            this.qManufacturersInputsBindingSource.DataMember = "QManufacturersInputs";
            this.qManufacturersInputsBindingSource.DataSource = this.dataSetQuery;
            this.qManufacturersInputsBindingSource.AddingNew += new System.ComponentModel.AddingNewEventHandler(this.qManufacturersInputsBindingSource_AddingNew);
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
            this.colBranchID,
            this.colEquipmentTypeName,
            this.colEquipmentKindID,
            this.colReadOnly});
            this.GridView.CustomizationFormBounds = new System.Drawing.Rectangle(408, 375, 216, 199);
            this.GridView.GridControl = this.GridGC;
            this.GridView.Name = "GridView";
            this.GridView.OptionsBehavior.Editable = false;
            this.GridView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.GridView.OptionsDetail.EnableMasterViewMode = false;
            this.GridView.OptionsMenu.EnableColumnMenu = false;
            this.GridView.OptionsNavigation.AutoFocusNewRow = true;
            this.GridView.OptionsSelection.EnableAppearanceHideSelection = false;
            this.GridView.OptionsSelection.InvertSelection = true;
            this.GridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.GridView.OptionsView.ShowGroupPanel = false;
            this.GridView.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.GridView_ShowingEditor);
            this.GridView.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.GridView_InvalidRowException);
            this.GridView.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.GridView_ValidateRow);
            this.GridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridView_KeyDown);
            this.GridView.DoubleClick += new System.EventHandler(this.GridView_DoubleClick);
            // 
            // colBranchID
            // 
            this.colBranchID.FieldName = "EquipmentKindID";
            this.colBranchID.Name = "colBranchID";
            // 
            // colEquipmentTypeName
            // 
            this.colEquipmentTypeName.Caption = "Наименование";
            this.colEquipmentTypeName.FieldName = "ManufacturerInputName";
            this.colEquipmentTypeName.Name = "colEquipmentTypeName";
            this.colEquipmentTypeName.OptionsFilter.AllowFilter = false;
            this.colEquipmentTypeName.Visible = true;
            this.colEquipmentTypeName.VisibleIndex = 0;
            this.colEquipmentTypeName.Width = 300;
            // 
            // colEquipmentKindID
            // 
            this.colEquipmentKindID.Caption = "Вид оборудования";
            this.colEquipmentKindID.ColumnEdit = this.repositoryItemLookUpEdit1;
            this.colEquipmentKindID.FieldName = "EquipmentKindID";
            this.colEquipmentKindID.Name = "colEquipmentKindID";
            this.colEquipmentKindID.OptionsFilter.AllowFilter = false;
            this.colEquipmentKindID.Visible = true;
            this.colEquipmentKindID.VisibleIndex = 1;
            this.colEquipmentKindID.Width = 250;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AppearanceDropDown.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repositoryItemLookUpEdit1.AppearanceDropDown.Options.UseFont = true;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("EquipmentKindName", "Вид оборудования")});
            this.repositoryItemLookUpEdit1.DataSource = this.equipmentKindsBindingSource;
            this.repositoryItemLookUpEdit1.DisplayMember = "EquipmentKindName";
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            this.repositoryItemLookUpEdit1.NullText = "";
            this.repositoryItemLookUpEdit1.ShowHeader = false;
            this.repositoryItemLookUpEdit1.ValidateOnEnterKey = true;
            this.repositoryItemLookUpEdit1.ValueMember = "EquipmentKindID";
            // 
            // equipmentKindsBindingSource
            // 
            this.equipmentKindsBindingSource.DataMember = "EquipmentKinds";
            this.equipmentKindsBindingSource.DataSource = this.dataSetMain;
            // 
            // dataSetMain
            // 
            this.dataSetMain.DataSetName = "DataSetMain";
            this.dataSetMain.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // colReadOnly
            // 
            this.colReadOnly.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.colReadOnly.AppearanceCell.Options.UseBackColor = true;
            this.colReadOnly.AppearanceCell.Options.UseTextOptions = true;
            this.colReadOnly.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colReadOnly.AppearanceHeader.Options.UseTextOptions = true;
            this.colReadOnly.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colReadOnly.Caption = "Доступ ограничен";
            this.colReadOnly.ColumnEdit = this.repYesNo;
            this.colReadOnly.FieldName = "ReadOnly";
            this.colReadOnly.Name = "colReadOnly";
            this.colReadOnly.OptionsColumn.AllowEdit = false;
            this.colReadOnly.OptionsFilter.AllowFilter = false;
            this.colReadOnly.Width = 150;
            // 
            // repYesNo
            // 
            this.repYesNo.AutoHeight = false;
            this.repYesNo.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repYesNo.Name = "repYesNo";
            this.repYesNo.NullText = "";
            // 
            // toolTip
            // 
            this.toolTip.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolTip.Appearance.Options.UseFont = true;
            this.toolTip.Rounded = true;
            // 
            // panelSelect
            // 
            this.panelSelect.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelSelect.Controls.Add(this.bSelect);
            this.panelSelect.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelSelect.Location = new System.Drawing.Point(0, 537);
            this.panelSelect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelSelect.Name = "panelSelect";
            this.panelSelect.Size = new System.Drawing.Size(793, 48);
            this.panelSelect.TabIndex = 4;
            // 
            // bSelect
            // 
            this.bSelect.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bSelect.Appearance.Options.UseFont = true;
            this.bSelect.Image = ((System.Drawing.Image)(resources.GetObject("bSelect.Image")));
            this.bSelect.Location = new System.Drawing.Point(5, 8);
            this.bSelect.LookAndFeel.SkinName = "Caramel";
            this.bSelect.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.bSelect.Name = "bSelect";
            this.bSelect.Size = new System.Drawing.Size(105, 33);
            this.bSelect.TabIndex = 22;
            this.bSelect.Text = "Выбрать";
            this.bSelect.Click += new System.EventHandler(this.bSelect_Click);
            // 
            // qManufacturersInputsTableAdapter
            // 
            this.qManufacturersInputsTableAdapter.ClearBeforeFill = true;
            // 
            // equipmentKindsTableAdapter
            // 
            this.equipmentKindsTableAdapter.ClearBeforeFill = true;
            // 
            // ManufacturerInputForm
            // 
            this.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 585);
            this.Controls.Add(this.GridGC);
            this.Controls.Add(this.panelSelect);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Caramel";
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimizeBox = false;
            this.Name = "ManufacturerInputForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Заводы-изготовители вводов";
            this.Load += new System.EventHandler(this.ManufacturerInputForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbCanEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridGC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qManufacturersInputsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipmentKindsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repYesNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelSelect)).EndInit();
            this.panelSelect.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.ImageList imageList1;
        private DataSetQuery dataSetQuery;
        private DevExpress.XtraEditors.ControlNavigator controlNavigator1;
        private DevExpress.XtraEditors.CheckEdit cbCanEdit;
        private DevExpress.Utils.ToolTipController toolTip;
        private DevExpress.XtraEditors.PanelControl panelSelect;
        private DevExpress.XtraEditors.SimpleButton bSelect;
        private System.Windows.Forms.BindingSource qManufacturersInputsBindingSource;
        private DataSetQueryTableAdapters.QManufacturersInputsTableAdapter qManufacturersInputsTableAdapter;
        private DevExpress.XtraGrid.GridControl GridGC;
        private DevExpress.XtraGrid.Views.Grid.GridView GridView;
        private DevExpress.XtraGrid.Columns.GridColumn colBranchID;
        private DevExpress.XtraGrid.Columns.GridColumn colEquipmentTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colEquipmentKindID;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colReadOnly;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repYesNo;
        private DataSetMain dataSetMain;
        private System.Windows.Forms.BindingSource equipmentKindsBindingSource;
        private DataSetMainTableAdapters.EquipmentKindsTableAdapter equipmentKindsTableAdapter;
    }
}