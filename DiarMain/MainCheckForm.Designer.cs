namespace DiarMain
{
    partial class MainCheckForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainCheckForm));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.toolTip = new DevExpress.Utils.ToolTipController(this.components);
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.teYear = new DevExpress.XtraEditors.TextEdit();
            this.bCancelFind = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.cbMonth = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.cbSubject = new DevExpress.XtraEditors.LookUpEdit();
            this.qSubjectsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetQuery = new DiarMain.DataSetQuery();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.cbBranch = new DevExpress.XtraEditors.LookUpEdit();
            this.qBranchesBySubjectBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cbSubstation = new DevExpress.XtraEditors.LookUpEdit();
            this.qSubstationsByBranchBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.qMainChecksBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colSubjectName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBranchName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubstationName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.qSubjectsTableAdapter = new DiarMain.DataSetQueryTableAdapters.QSubjectsTableAdapter();
            this.qBranchesBySubjectTableAdapter = new DiarMain.DataSetQueryTableAdapters.QBranchesBySubjectTableAdapter();
            this.qSubstationsByBranchTableAdapter = new DiarMain.DataSetQueryTableAdapters.QSubstationsByBranchTableAdapter();
            this.qMainChecksTableAdapter = new DiarMain.DataSetQueryTableAdapters.QMainChecksTableAdapter();
            this.MainGridControl = new DevExpress.XtraGrid.GridControl();
            this.MainGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cbEquipmentKindName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEquipmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEquipmentNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.defaultSkin = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teYear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qSubjectsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qBranchesBySubjectBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSubstation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qSubstationsByBranchBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qMainChecksBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Controls.Add(this.groupControl4);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1259, 139);
            this.panelControl1.TabIndex = 1;
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupControl1.Appearance.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.btnEdit);
            this.groupControl1.Controls.Add(this.btnAdd);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(220, 116);
            this.groupControl1.TabIndex = 20;
            this.groupControl1.Text = "Действия над проверками";
            // 
            // btnEdit
            // 
            this.btnEdit.AllowFocus = false;
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnEdit.Location = new System.Drawing.Point(116, 37);
            this.btnEdit.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(93, 69);
            this.btnEdit.TabIndex = 17;
            this.btnEdit.TabStop = false;
            this.btnEdit.ToolTip = "Изменить проверку";
            this.btnEdit.ToolTipController = this.toolTip;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // toolTip
            // 
            this.toolTip.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolTip.Appearance.Options.UseFont = true;
            this.toolTip.Rounded = true;
            // 
            // btnAdd
            // 
            this.btnAdd.AllowFocus = false;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnAdd.Location = new System.Drawing.Point(11, 37);
            this.btnAdd.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(93, 69);
            this.btnAdd.TabIndex = 16;
            this.btnAdd.TabStop = false;
            this.btnAdd.ToolTip = "Добавить проверку";
            this.btnAdd.ToolTipController = this.toolTip;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // groupControl4
            // 
            this.groupControl4.AppearanceCaption.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupControl4.AppearanceCaption.Options.UseFont = true;
            this.groupControl4.Controls.Add(this.teYear);
            this.groupControl4.Controls.Add(this.bCancelFind);
            this.groupControl4.Controls.Add(this.simpleButton1);
            this.groupControl4.Controls.Add(this.cbMonth);
            this.groupControl4.Controls.Add(this.labelControl5);
            this.groupControl4.Controls.Add(this.cbSubject);
            this.groupControl4.Controls.Add(this.labelControl1);
            this.groupControl4.Controls.Add(this.labelControl2);
            this.groupControl4.Controls.Add(this.labelControl4);
            this.groupControl4.Controls.Add(this.cbBranch);
            this.groupControl4.Controls.Add(this.cbSubstation);
            this.groupControl4.Controls.Add(this.labelControl3);
            this.groupControl4.Location = new System.Drawing.Point(243, 12);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(969, 116);
            this.groupControl4.TabIndex = 19;
            this.groupControl4.Text = "Поиск проверок";
            // 
            // teYear
            // 
            this.teYear.Location = new System.Drawing.Point(435, 76);
            this.teYear.Name = "teYear";
            this.teYear.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.teYear.Properties.Appearance.Options.UseFont = true;
            this.teYear.Properties.Mask.EditMask = "\\d+";
            this.teYear.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.teYear.Properties.Mask.ShowPlaceHolders = false;
            this.teYear.Properties.MaxLength = 4;
            this.teYear.Size = new System.Drawing.Size(156, 26);
            this.teYear.TabIndex = 19;
            // 
            // bCancelFind
            // 
            this.bCancelFind.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bCancelFind.Appearance.Options.UseFont = true;
            this.bCancelFind.Image = ((System.Drawing.Image)(resources.GetObject("bCancelFind.Image")));
            this.bCancelFind.Location = new System.Drawing.Point(838, 73);
            this.bCancelFind.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bCancelFind.Name = "bCancelFind";
            this.bCancelFind.Size = new System.Drawing.Size(119, 33);
            this.bCancelFind.TabIndex = 16;
            this.bCancelFind.Text = "Отменить";
            this.bCancelFind.Click += new System.EventHandler(this.bCancelFind_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(663, 73);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(167, 33);
            this.simpleButton1.TabIndex = 15;
            this.simpleButton1.Text = "Найти проверки";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // cbMonth
            // 
            this.cbMonth.Location = new System.Drawing.Point(150, 76);
            this.cbMonth.Name = "cbMonth";
            this.cbMonth.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.cbMonth.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cbMonth.Properties.Appearance.Options.UseFont = true;
            this.cbMonth.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cbMonth.Properties.AppearanceDisabled.Options.UseFont = true;
            this.cbMonth.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cbMonth.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cbMonth.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cbMonth.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.cbMonth.Properties.AppearanceFocused.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cbMonth.Properties.AppearanceFocused.Options.UseFont = true;
            this.cbMonth.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cbMonth.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.cbMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbMonth.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("VAL", "Значение", 103, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.cbMonth.Properties.DisplayMember = "VAL";
            this.cbMonth.Properties.NullText = "";
            this.cbMonth.Properties.ShowHeader = false;
            this.cbMonth.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cbMonth.Properties.ValueMember = "KEY";
            this.cbMonth.Size = new System.Drawing.Size(149, 26);
            this.cbMonth.TabIndex = 17;
            this.cbMonth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridViewView_KeyDown);
            this.cbMonth.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cbSubject_KeyUp);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.labelControl5.Location = new System.Drawing.Point(318, 79);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(99, 20);
            this.labelControl5.TabIndex = 7;
            this.labelControl5.Text = "Год проверки:";
            // 
            // cbSubject
            // 
            this.cbSubject.Location = new System.Drawing.Point(85, 39);
            this.cbSubject.Name = "cbSubject";
            this.cbSubject.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.cbSubject.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cbSubject.Properties.Appearance.Options.UseFont = true;
            this.cbSubject.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cbSubject.Properties.AppearanceDisabled.Options.UseFont = true;
            this.cbSubject.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cbSubject.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cbSubject.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cbSubject.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.cbSubject.Properties.AppearanceFocused.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cbSubject.Properties.AppearanceFocused.Options.UseFont = true;
            this.cbSubject.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cbSubject.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.cbSubject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbSubject.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SubjectName", "Subject Name", 103, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.cbSubject.Properties.DataSource = this.qSubjectsBindingSource;
            this.cbSubject.Properties.DisplayMember = "SubjectName";
            this.cbSubject.Properties.NullText = "";
            this.cbSubject.Properties.ShowHeader = false;
            this.cbSubject.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cbSubject.Properties.ValueMember = "SubjectID";
            this.cbSubject.Size = new System.Drawing.Size(214, 26);
            this.cbSubject.TabIndex = 1;
            this.cbSubject.EditValueChanged += new System.EventHandler(this.cbSubject_EditValueChanged);
            this.cbSubject.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridViewView_KeyDown);
            this.cbSubject.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cbSubject_KeyUp);
            // 
            // qSubjectsBindingSource
            // 
            this.qSubjectsBindingSource.DataMember = "QSubjects";
            this.qSubjectsBindingSource.DataSource = this.dataSetQuery;
            // 
            // dataSetQuery
            // 
            this.dataSetQuery.DataSetName = "DataSetQuery";
            this.dataSetQuery.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.labelControl1.Location = new System.Drawing.Point(12, 42);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 20);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Субъект:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.labelControl2.Location = new System.Drawing.Point(318, 42);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(56, 20);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Филиал:";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.labelControl4.Location = new System.Drawing.Point(12, 79);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(120, 20);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "Месяц проверки:";
            // 
            // cbBranch
            // 
            this.cbBranch.Location = new System.Drawing.Point(395, 39);
            this.cbBranch.Name = "cbBranch";
            this.cbBranch.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.cbBranch.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cbBranch.Properties.Appearance.Options.UseFont = true;
            this.cbBranch.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cbBranch.Properties.AppearanceDisabled.Options.UseFont = true;
            this.cbBranch.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cbBranch.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cbBranch.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cbBranch.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.cbBranch.Properties.AppearanceFocused.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cbBranch.Properties.AppearanceFocused.Options.UseFont = true;
            this.cbBranch.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cbBranch.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.cbBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbBranch.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BranchName", "Branch Name", 99, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.cbBranch.Properties.DataSource = this.qBranchesBySubjectBindingSource;
            this.cbBranch.Properties.DisplayMember = "BranchName";
            this.cbBranch.Properties.NullText = "";
            this.cbBranch.Properties.ShowHeader = false;
            this.cbBranch.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cbBranch.Properties.ValueMember = "BranchID";
            this.cbBranch.Size = new System.Drawing.Size(196, 26);
            this.cbBranch.TabIndex = 3;
            this.cbBranch.EditValueChanged += new System.EventHandler(this.cbBranch_EditValueChanged);
            this.cbBranch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridViewView_KeyDown);
            this.cbBranch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cbSubject_KeyUp);
            // 
            // qBranchesBySubjectBindingSource
            // 
            this.qBranchesBySubjectBindingSource.DataMember = "QBranchesBySubject";
            this.qBranchesBySubjectBindingSource.DataSource = this.dataSetQuery;
            // 
            // cbSubstation
            // 
            this.cbSubstation.Location = new System.Drawing.Point(765, 39);
            this.cbSubstation.Name = "cbSubstation";
            this.cbSubstation.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.cbSubstation.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cbSubstation.Properties.Appearance.Options.UseFont = true;
            this.cbSubstation.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cbSubstation.Properties.AppearanceDisabled.Options.UseFont = true;
            this.cbSubstation.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cbSubstation.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cbSubstation.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cbSubstation.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.cbSubstation.Properties.AppearanceFocused.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cbSubstation.Properties.AppearanceFocused.Options.UseFont = true;
            this.cbSubstation.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cbSubstation.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.cbSubstation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbSubstation.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SubstationName", "Substation Name", 124, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.cbSubstation.Properties.DataSource = this.qSubstationsByBranchBindingSource;
            this.cbSubstation.Properties.DisplayMember = "SubstationName";
            this.cbSubstation.Properties.NullText = "";
            this.cbSubstation.Properties.ShowHeader = false;
            this.cbSubstation.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cbSubstation.Properties.ValueMember = "SubstationID";
            this.cbSubstation.Size = new System.Drawing.Size(191, 26);
            this.cbSubstation.TabIndex = 5;
            this.cbSubstation.EditValueChanged += new System.EventHandler(this.cbSubstation_EditValueChanged);
            this.cbSubstation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridViewView_KeyDown);
            this.cbSubstation.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cbSubject_KeyUp);
            // 
            // qSubstationsByBranchBindingSource
            // 
            this.qSubstationsByBranchBindingSource.DataMember = "QSubstationsByBranch";
            this.qSubstationsByBranchBindingSource.DataSource = this.dataSetQuery;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.labelControl3.Location = new System.Drawing.Point(608, 42);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(149, 20);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Подстанция/станция:";
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
            // qMainChecksBindingSource
            // 
            this.qMainChecksBindingSource.DataMember = "QMainChecks";
            this.qMainChecksBindingSource.DataSource = this.dataSetQuery;
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
            // qSubjectsTableAdapter
            // 
            this.qSubjectsTableAdapter.ClearBeforeFill = true;
            // 
            // qBranchesBySubjectTableAdapter
            // 
            this.qBranchesBySubjectTableAdapter.ClearBeforeFill = true;
            // 
            // qSubstationsByBranchTableAdapter
            // 
            this.qSubstationsByBranchTableAdapter.ClearBeforeFill = true;
            // 
            // qMainChecksTableAdapter
            // 
            this.qMainChecksTableAdapter.ClearBeforeFill = true;
            // 
            // MainGridControl
            // 
            this.MainGridControl.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MainGridControl.BackgroundImage")));
            this.MainGridControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MainGridControl.DataSource = this.qMainChecksBindingSource;
            this.MainGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MainGridControl.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainGridControl.Location = new System.Drawing.Point(0, 139);
            this.MainGridControl.MainView = this.MainGridView;
            this.MainGridControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MainGridControl.Name = "MainGridControl";
            this.MainGridControl.Size = new System.Drawing.Size(1259, 391);
            this.MainGridControl.TabIndex = 4;
            this.MainGridControl.ToolTipController = this.toolTip;
            this.MainGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.MainGridView});
            // 
            // MainGridView
            // 
            this.MainGridView.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainGridView.Appearance.ColumnFilterButton.Options.UseFont = true;
            this.MainGridView.Appearance.ColumnFilterButtonActive.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainGridView.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
            this.MainGridView.Appearance.CustomizationFormHint.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainGridView.Appearance.CustomizationFormHint.Options.UseFont = true;
            this.MainGridView.Appearance.DetailTip.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainGridView.Appearance.DetailTip.Options.UseFont = true;
            this.MainGridView.Appearance.Empty.BackColor = System.Drawing.Color.Transparent;
            this.MainGridView.Appearance.Empty.Options.UseBackColor = true;
            this.MainGridView.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainGridView.Appearance.FilterCloseButton.Options.UseFont = true;
            this.MainGridView.Appearance.FilterPanel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainGridView.Appearance.FilterPanel.Options.UseFont = true;
            this.MainGridView.Appearance.GroupButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainGridView.Appearance.GroupButton.Options.UseFont = true;
            this.MainGridView.Appearance.GroupPanel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainGridView.Appearance.GroupPanel.Options.UseFont = true;
            this.MainGridView.Appearance.GroupRow.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainGridView.Appearance.GroupRow.Options.UseFont = true;
            this.MainGridView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainGridView.Appearance.HeaderPanel.Options.UseFont = true;
            this.MainGridView.Appearance.Row.BackColor = System.Drawing.Color.Transparent;
            this.MainGridView.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainGridView.Appearance.Row.Options.UseBackColor = true;
            this.MainGridView.Appearance.Row.Options.UseFont = true;
            this.MainGridView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.MainGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.cbEquipmentKindName,
            this.colEquipmentName,
            this.colEquipmentNumber});
            this.MainGridView.GridControl = this.MainGridControl;
            this.MainGridView.Name = "MainGridView";
            this.MainGridView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.MainGridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.MainGridView.OptionsBehavior.Editable = false;
            this.MainGridView.OptionsBehavior.ReadOnly = true;
            this.MainGridView.OptionsFilter.AllowFilterEditor = false;
            this.MainGridView.OptionsMenu.EnableColumnMenu = false;
            this.MainGridView.OptionsMenu.EnableGroupPanelMenu = false;
            this.MainGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.MainGridView.OptionsSelection.EnableAppearanceHideSelection = false;
            this.MainGridView.OptionsView.ShowGroupPanel = false;
            this.MainGridView.ShowFilterPopupListBox += new DevExpress.XtraGrid.Views.Grid.FilterPopupListBoxEventHandler(this.MainGridView_ShowFilterPopupListBox);
            this.MainGridView.DoubleClick += new System.EventHandler(this.MainGridView_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Субъект";
            this.gridColumn1.FieldName = "SubjectName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowMove = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 200;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Филиал";
            this.gridColumn2.FieldName = "BranchName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowMove = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 200;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Подстанция";
            this.gridColumn3.FieldName = "SubstationName";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowMove = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 200;
            // 
            // cbEquipmentKindName
            // 
            this.cbEquipmentKindName.Caption = "Дата начала";
            this.cbEquipmentKindName.FieldName = "CheckDateBegin";
            this.cbEquipmentKindName.Name = "cbEquipmentKindName";
            this.cbEquipmentKindName.OptionsColumn.AllowMove = false;
            this.cbEquipmentKindName.Visible = true;
            this.cbEquipmentKindName.VisibleIndex = 3;
            this.cbEquipmentKindName.Width = 150;
            // 
            // colEquipmentName
            // 
            this.colEquipmentName.Caption = "Дата окончания";
            this.colEquipmentName.FieldName = "CheckDateEnd";
            this.colEquipmentName.Name = "colEquipmentName";
            this.colEquipmentName.OptionsColumn.AllowMove = false;
            this.colEquipmentName.OptionsFilter.AllowAutoFilter = false;
            this.colEquipmentName.Visible = true;
            this.colEquipmentName.VisibleIndex = 4;
            this.colEquipmentName.Width = 150;
            // 
            // colEquipmentNumber
            // 
            this.colEquipmentNumber.Caption = "Кол-во оборудования";
            this.colEquipmentNumber.FieldName = "CntEquipments";
            this.colEquipmentNumber.Name = "colEquipmentNumber";
            this.colEquipmentNumber.OptionsColumn.AllowMove = false;
            this.colEquipmentNumber.OptionsFilter.AllowAutoFilter = false;
            this.colEquipmentNumber.Visible = true;
            this.colEquipmentNumber.VisibleIndex = 5;
            this.colEquipmentNumber.Width = 128;
            // 
            // MainCheckForm
            // 
            this.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1259, 530);
            this.Controls.Add(this.MainGridControl);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Caramel";
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(987, 439);
            this.Name = "MainCheckForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Экспресс-оценка технического состояния высоковольтного оборудования";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainCheckForm_FormClosing);
            this.Load += new System.EventHandler(this.MainCheckForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainCheckForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            this.groupControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teYear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qSubjectsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qBranchesBySubjectBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSubstation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qSubstationsByBranchBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qMainChecksBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.ImageList imageList1;
        private DataSetQuery dataSetQuery;
        /*private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit2View;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;*/
        private DevExpress.Utils.ToolTipController toolTip;
        /*private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repYesNo;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit4;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit5;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;*/
        private DevExpress.XtraGrid.Columns.GridColumn colSubjectName;
        private DevExpress.XtraGrid.Columns.GridColumn colBranchName;
        private DevExpress.XtraGrid.Columns.GridColumn colSubstationName;
        private DevExpress.XtraEditors.LookUpEdit cbSubject;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit cbSubstation;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit cbBranch;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.BindingSource qSubjectsBindingSource;
        private DataSetQueryTableAdapters.QSubjectsTableAdapter qSubjectsTableAdapter;
        private System.Windows.Forms.BindingSource qSubstationsByBranchBindingSource;
        private System.Windows.Forms.BindingSource qBranchesBySubjectBindingSource;
        private DataSetQueryTableAdapters.QBranchesBySubjectTableAdapter qBranchesBySubjectTableAdapter;
        private DataSetQueryTableAdapters.QSubstationsByBranchTableAdapter qSubstationsByBranchTableAdapter;
        private System.Windows.Forms.BindingSource qMainChecksBindingSource;
        private DataSetQueryTableAdapters.QMainChecksTableAdapter qMainChecksTableAdapter;
        private DevExpress.XtraGrid.GridControl MainGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView MainGridView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn cbEquipmentKindName;
        private DevExpress.XtraGrid.Columns.GridColumn colEquipmentName;
        private DevExpress.XtraGrid.Columns.GridColumn colEquipmentNumber;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LookUpEdit cbMonth;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.SimpleButton bCancelFind;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultSkin;
        private DevExpress.XtraEditors.TextEdit teYear;
        //private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit6;
    }
}