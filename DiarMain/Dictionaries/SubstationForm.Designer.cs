namespace DiarMain
{
    partial class SubstationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubstationForm));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.cbCanEdit = new DevExpress.XtraEditors.CheckEdit();
            this.controlNavigator1 = new DevExpress.XtraEditors.ControlNavigator();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.GridGC = new DevExpress.XtraGrid.GridControl();
            this.qSubstationsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetQuery = new DiarMain.DataSetQuery();
            this.GridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSubstationID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubstationName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubstationType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repSubstationType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colBranchID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repBranch = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.qBranchesSubjectsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colSubjectID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repSubject = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.qSubjectsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colReadOnly = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repYesNo = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.toolTip = new DevExpress.Utils.ToolTipController(this.components);
            this.dataSetMain = new DiarMain.DataSetMain();
            this.qBranchesSubjectsTableAdapter = new DiarMain.DataSetQueryTableAdapters.QBranchesSubjectsTableAdapter();
            this.panelSelect = new DevExpress.XtraEditors.PanelControl();
            this.bSelect = new DevExpress.XtraEditors.SimpleButton();
            this.qSubjectsTableAdapter = new DiarMain.DataSetQueryTableAdapters.QSubjectsTableAdapter();
            this.qSubstationsTableAdapter = new DiarMain.DataSetQueryTableAdapters.QSubstationsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbCanEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridGC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qSubstationsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repSubstationType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repBranch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qBranchesSubjectsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repSubject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qSubjectsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repYesNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetMain)).BeginInit();
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
            this.panelControl1.Size = new System.Drawing.Size(913, 58);
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
            this.cbCanEdit.Size = new System.Drawing.Size(286, 25);
            this.cbCanEdit.TabIndex = 3;
            this.cbCanEdit.CheckedChanged += new System.EventHandler(this.cbCanEdit_CheckedChanged);
            this.cbCanEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridViewView_KeyDown);
            // 
            // controlNavigator1
            // 
            this.controlNavigator1.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.controlNavigator1.Appearance.Options.UseFont = true;
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
            this.GridGC.DataSource = this.qSubstationsBindingSource;
            this.GridGC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridGC.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GridGC.Location = new System.Drawing.Point(0, 58);
            this.GridGC.MainView = this.GridView;
            this.GridGC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GridGC.Name = "GridGC";
            this.GridGC.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repBranch,
            this.repYesNo,
            this.repSubject,
            this.repSubstationType});
            this.GridGC.Size = new System.Drawing.Size(913, 479);
            this.GridGC.TabIndex = 2;
            this.GridGC.ToolTipController = this.toolTip;
            this.GridGC.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridView});
            // 
            // qSubstationsBindingSource
            // 
            this.qSubstationsBindingSource.DataMember = "QSubstations";
            this.qSubstationsBindingSource.DataSource = this.dataSetQuery;
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
            this.colSubstationID,
            this.colSubstationName,
            this.colSubstationType,
            this.colBranchID,
            this.colSubjectID,
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
            this.GridView.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.GridView_CellValueChanged);
            this.GridView.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.GridView_InvalidRowException);
            this.GridView.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.GridView_ValidateRow);
            this.GridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridViewView_KeyDown);
            this.GridView.DoubleClick += new System.EventHandler(this.GridView_DoubleClick);
            // 
            // colSubstationID
            // 
            this.colSubstationID.FieldName = "SubstationID";
            this.colSubstationID.Name = "colSubstationID";
            // 
            // colSubstationName
            // 
            this.colSubstationName.Caption = "Наименование";
            this.colSubstationName.FieldName = "SubstationName";
            this.colSubstationName.Name = "colSubstationName";
            this.colSubstationName.OptionsFilter.AllowFilter = false;
            this.colSubstationName.Visible = true;
            this.colSubstationName.VisibleIndex = 0;
            this.colSubstationName.Width = 315;
            // 
            // colSubstationType
            // 
            this.colSubstationType.Caption = "Тип";
            this.colSubstationType.ColumnEdit = this.repSubstationType;
            this.colSubstationType.FieldName = "SubstationType";
            this.colSubstationType.Name = "colSubstationType";
            this.colSubstationType.OptionsFilter.AllowFilter = false;
            this.colSubstationType.Visible = true;
            this.colSubstationType.VisibleIndex = 1;
            this.colSubstationType.Width = 150;
            // 
            // repSubstationType
            // 
            this.repSubstationType.AutoHeight = false;
            this.repSubstationType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repSubstationType.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("VAL", "Name1")});
            this.repSubstationType.DisplayMember = "VAL";
            this.repSubstationType.Name = "repSubstationType";
            this.repSubstationType.NullText = "";
            this.repSubstationType.ShowFooter = false;
            this.repSubstationType.ShowHeader = false;
            this.repSubstationType.ValidateOnEnterKey = true;
            this.repSubstationType.ValueMember = "KEY";
            // 
            // colBranchID
            // 
            this.colBranchID.Caption = "Филиал";
            this.colBranchID.ColumnEdit = this.repBranch;
            this.colBranchID.FieldName = "BranchID";
            this.colBranchID.Name = "colBranchID";
            this.colBranchID.OptionsFilter.AllowFilter = false;
            this.colBranchID.Visible = true;
            this.colBranchID.VisibleIndex = 2;
            this.colBranchID.Width = 155;
            // 
            // repBranch
            // 
            this.repBranch.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repBranch.Appearance.Options.UseFont = true;
            this.repBranch.AppearanceDropDown.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repBranch.AppearanceDropDown.Options.UseFont = true;
            this.repBranch.AppearanceDropDownHeader.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repBranch.AppearanceDropDownHeader.Options.UseFont = true;
            this.repBranch.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "Открыть справочник \"Филиалы\"", null, null, true)});
            this.repBranch.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BranchName", 250, "Филиал"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SubjectName", 150, "Субъект")});
            this.repBranch.DataSource = this.qBranchesSubjectsBindingSource;
            this.repBranch.DisplayMember = "BranchName";
            this.repBranch.Name = "repBranch";
            this.repBranch.NullText = "";
            this.repBranch.PopupWidth = 420;
            this.repBranch.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.repBranch.ValidateOnEnterKey = true;
            this.repBranch.ValueMember = "BranchID";
            this.repBranch.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemLookUpEdit1_ButtonClick);
            // 
            // qBranchesSubjectsBindingSource
            // 
            this.qBranchesSubjectsBindingSource.DataMember = "QBranchesSubjects";
            this.qBranchesSubjectsBindingSource.DataSource = this.dataSetQuery;
            // 
            // colSubjectID
            // 
            this.colSubjectID.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.colSubjectID.AppearanceCell.Options.UseBackColor = true;
            this.colSubjectID.Caption = "Субъект";
            this.colSubjectID.ColumnEdit = this.repSubject;
            this.colSubjectID.FieldName = "SubjectID";
            this.colSubjectID.Name = "colSubjectID";
            this.colSubjectID.OptionsColumn.AllowEdit = false;
            this.colSubjectID.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colSubjectID.OptionsFilter.AllowFilter = false;
            this.colSubjectID.Visible = true;
            this.colSubjectID.VisibleIndex = 3;
            this.colSubjectID.Width = 155;
            // 
            // repSubject
            // 
            this.repSubject.AutoHeight = false;
            this.repSubject.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repSubject.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SubjectName", "Name6")});
            this.repSubject.DataSource = this.qSubjectsBindingSource;
            this.repSubject.DisplayMember = "SubjectName";
            this.repSubject.Name = "repSubject";
            this.repSubject.NullText = "";
            this.repSubject.ValueMember = "SubjectID";
            // 
            // qSubjectsBindingSource
            // 
            this.qSubjectsBindingSource.DataMember = "QSubjects";
            this.qSubjectsBindingSource.DataSource = this.dataSetQuery;
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
            this.colReadOnly.Width = 120;
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
            // dataSetMain
            // 
            this.dataSetMain.DataSetName = "DataSetMain";
            this.dataSetMain.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // qBranchesSubjectsTableAdapter
            // 
            this.qBranchesSubjectsTableAdapter.ClearBeforeFill = true;
            // 
            // panelSelect
            // 
            this.panelSelect.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelSelect.Controls.Add(this.bSelect);
            this.panelSelect.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelSelect.Location = new System.Drawing.Point(0, 537);
            this.panelSelect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelSelect.Name = "panelSelect";
            this.panelSelect.Size = new System.Drawing.Size(913, 48);
            this.panelSelect.TabIndex = 3;
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
            // qSubjectsTableAdapter
            // 
            this.qSubjectsTableAdapter.ClearBeforeFill = true;
            // 
            // qSubstationsTableAdapter
            // 
            this.qSubstationsTableAdapter.ClearBeforeFill = true;
            // 
            // SubstationForm
            // 
            this.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 585);
            this.Controls.Add(this.GridGC);
            this.Controls.Add(this.panelSelect);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Caramel";
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimizeBox = false;
            this.Name = "SubstationForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Подстанции/станции";
            this.Load += new System.EventHandler(this.SubstationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbCanEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridGC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qSubstationsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repSubstationType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repBranch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qBranchesSubjectsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repSubject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qSubjectsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repYesNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelSelect)).EndInit();
            this.panelSelect.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraGrid.GridControl GridGC;
        private DevExpress.XtraGrid.Views.Grid.GridView GridView;
        private DevExpress.XtraEditors.ControlNavigator controlNavigator1;
        private DevExpress.XtraEditors.CheckEdit cbCanEdit;
        private DataSetQuery dataSetQuery;
        private DevExpress.XtraGrid.Columns.GridColumn colSubstationID;
        private DevExpress.XtraGrid.Columns.GridColumn colSubstationName;
        private DevExpress.XtraGrid.Columns.GridColumn colBranchID;
        private DevExpress.XtraGrid.Columns.GridColumn colReadOnly;
        private DataSetMain dataSetMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repBranch;
        private DevExpress.Utils.ToolTipController toolTip;
        private System.Windows.Forms.BindingSource qBranchesSubjectsBindingSource;
        private DataSetQueryTableAdapters.QBranchesSubjectsTableAdapter qBranchesSubjectsTableAdapter;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repYesNo;
        private DevExpress.XtraEditors.PanelControl panelSelect;
        private DevExpress.XtraEditors.SimpleButton bSelect;
        private DevExpress.XtraGrid.Columns.GridColumn colSubjectID;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repSubject;
        private System.Windows.Forms.BindingSource qSubjectsBindingSource;
        private DataSetQueryTableAdapters.QSubjectsTableAdapter qSubjectsTableAdapter;
        private DevExpress.XtraGrid.Columns.GridColumn colSubstationType;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repSubstationType;
        private System.Windows.Forms.BindingSource qSubstationsBindingSource;
        private DataSetQueryTableAdapters.QSubstationsTableAdapter qSubstationsTableAdapter;
    }
}