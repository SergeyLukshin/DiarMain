namespace DiarMain
{
    partial class PassportDataForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PassportDataForm));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject9 = new DevExpress.Utils.SerializableAppearanceObject();
            this.toolTip = new DevExpress.Utils.ToolTipController(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.cbEquipmentClass = new DevExpress.XtraEditors.LookUpEdit();
            this.qEquipmentRecordBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetQuery2 = new DiarMain.DataSetQuery2();
            this.qEquipmentClassesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetQuery = new DiarMain.DataSetQuery();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnClearPicture = new DevExpress.XtraEditors.SimpleButton();
            this.btnChangePicture = new DevExpress.XtraEditors.SimpleButton();
            this.peImage = new DevExpress.XtraEditors.PictureEdit();
            this.teEquipmentNumber = new DevExpress.XtraEditors.TextEdit();
            this.teEquipmentName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.equipmentKindsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetMain = new DiarMain.DataSetMain();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.bNext = new DevExpress.XtraEditors.SimpleButton();
            this.bCancel = new DevExpress.XtraEditors.SimpleButton();
            this.bSave = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.GridVertical = new DevExpress.XtraVerticalGrid.VGridControl();
            this.qManufacturersByKindBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qEquipmentTypesByKindBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qSubjectsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qBranchesBySubjectBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qSubstationsByBranchBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qRPNTypesByKindBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qInputVoltageTypesByKindBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qManufacturersInputsByKindBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qSwitchDriveTypesByKindBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.equipmentKindsTableAdapter = new DiarMain.DataSetMainTableAdapters.EquipmentKindsTableAdapter();
            this.qEquipmentRecordTableAdapter = new DiarMain.DataSetQuery2TableAdapters.QEquipmentRecordTableAdapter();
            this.qEquipmentTypesByKindTableAdapter = new DiarMain.DataSetQueryTableAdapters.QEquipmentTypesByKindTableAdapter();
            this.qSubjectsTableAdapter = new DiarMain.DataSetQueryTableAdapters.QSubjectsTableAdapter();
            this.qBranchesBySubjectTableAdapter = new DiarMain.DataSetQueryTableAdapters.QBranchesBySubjectTableAdapter();
            this.qSubstationsByBranchTableAdapter = new DiarMain.DataSetQueryTableAdapters.QSubstationsByBranchTableAdapter();
            this.qRPNTypesByKindTableAdapter = new DiarMain.DataSetQueryTableAdapters.QRPNTypesByKindTableAdapter();
            this.qInputVoltageTypesByKindTableAdapter = new DiarMain.DataSetQueryTableAdapters.QInputVoltageTypesByKindTableAdapter();
            this.qEquipmentClassesTableAdapter = new DiarMain.DataSetQueryTableAdapters.QEquipmentClassesTableAdapter();
            this.qSwitchDriveTypesByKindTableAdapter = new DiarMain.DataSetQueryTableAdapters.QSwitchDriveTypesByKindTableAdapter();
            this.qManufacturersByKindTableAdapter = new DiarMain.DataSetQueryTableAdapters.QManufacturersByKindTableAdapter();
            this.qManufacturersInputsByKindTableAdapter = new DiarMain.DataSetQueryTableAdapters.QManufacturersInputsByKindTableAdapter();
            this.repSubstation_ = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSubjectName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBranchName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubstationName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repManufacturer = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.rep4Digits = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.rep2Digits = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repEquipmentType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repConstructionType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repCoolingSystemType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repNominalVoltageLow = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repNominalVoltageMid = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repNominalVoltageHigh = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repProtectionOilType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repInputKind = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repRPNCnt = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repRPNVoltage = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repSubject = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repBranch = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repSubstation = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repRPNKind = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repInputNumber = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repRPNType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repInputType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repManufacturerInput = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.rep5Digits = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repSwitchDriveType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repInputName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repPower = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.categoryRow1 = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rSubject = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rBranch = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rSubstation = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.categoryRow2 = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rManufacturer = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rCreateYear = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rUseBeginYear = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.categoryRow3 = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rEquipmentType = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rConstructionType = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rProtectionOilType = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rCoolingSystemType = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rSwitchDriveType = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rNominalPower = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rNominalCurrent = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.catRPN = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rRPNCnt = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rRPNVoltage = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rRPNType = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rRPNKind = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rRPNNumber = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rRPNNumber2 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rRPNNumber3 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.catVoltage = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rNominalVoltageLow = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rNominalVoltageMiddle = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rNominalVoltageHigh = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rNominalVoltageNeitral = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.catInputHighA = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rInputNameHighA = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputTypeIDHighA = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputKindHighA = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputManufacturerIDHighA = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputNumberHighA = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputCreateYearHighA = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputUseBeginYearHighA = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.catInputHighB = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rInputNameHighB = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputTypeIDHighB = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputKindHighB = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputManufacturerIDHighB = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputNumberHighB = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputCreateYearHighB = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputUseBeginYearHighB = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.catInputHighC = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rInputNameHighC = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputTypeIDHighC = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputKindHighC = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputManufacturerIDHighC = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputNumberHighC = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputCreateYearHighC = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputUseBeginYearHighC = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.catInputMiddleA = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rInputNameMiddleA = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputTypeIDMiddleA = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputKindMiddleA = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputManufacturerIDMiddleA = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputNumberMiddleA = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputCreateYearMiddleA = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputUseBeginYearMiddleA = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.catInputMiddleB = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rInputNameMiddleB = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputTypeIDMiddleB = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputKindMiddleB = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputManufacturerIDMiddleB = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputNumberMiddleB = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputCreateYearMiddleB = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputUseBeginYearMiddleB = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.catInputMiddleC = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rInputNameMiddleC = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputTypeIDMiddleC = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputKindMiddleC = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputManufacturerIDMiddleC = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputNumberMiddleC = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputCreateYearMiddleC = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputUseBeginYearMiddleC = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.catInputNeutral = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rInputNameNeutral = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputTypeIDNeutral = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputKindNeutral = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputManufacturerIDNeutral = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputNumberNeutral = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputCreateYearNeutral = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rInputUseBeginYearNeutral = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.catTmp = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rProjectLifeTime = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rActualLifeTime = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rLastWorkoverYear = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rLastTechnicalServiceYear = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rTechnicalServiceDocument = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rTechnicalServiceConclusion = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rNextTechnicalServiceYear = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rTechnicalServiceCount = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbEquipmentClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qEquipmentRecordBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetQuery2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qEquipmentClassesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peImage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teEquipmentNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teEquipmentName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipmentKindsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridVertical)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qManufacturersByKindBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qEquipmentTypesByKindBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qSubjectsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qBranchesBySubjectBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qSubstationsByBranchBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qRPNTypesByKindBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qInputVoltageTypesByKindBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qManufacturersInputsByKindBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qSwitchDriveTypesByKindBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repSubstation_)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repManufacturer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep4Digits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep2Digits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repEquipmentType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repConstructionType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCoolingSystemType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repNominalVoltageLow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repNominalVoltageMid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repNominalVoltageHigh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repProtectionOilType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repInputKind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repRPNCnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repRPNVoltage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repSubject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repBranch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repSubstation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repRPNKind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repInputNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repRPNType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repInputType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repManufacturerInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep5Digits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repSwitchDriveType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repInputName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repPower)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTip
            // 
            this.toolTip.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolTip.Appearance.Options.UseFont = true;
            this.toolTip.Rounded = true;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Padding = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.panelControl1.Size = new System.Drawing.Size(829, 192);
            this.panelControl1.TabIndex = 19;
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupControl1.Appearance.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.cbEquipmentClass);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.btnClearPicture);
            this.groupControl1.Controls.Add(this.btnChangePicture);
            this.groupControl1.Controls.Add(this.peImage);
            this.groupControl1.Controls.Add(this.teEquipmentNumber);
            this.groupControl1.Controls.Add(this.teEquipmentName);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(9, 10);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(811, 172);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "Основные параметры";
            // 
            // cbEquipmentClass
            // 
            this.cbEquipmentClass.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.qEquipmentRecordBindingSource, "EquipmentClassID", true));
            this.cbEquipmentClass.Location = new System.Drawing.Point(244, 37);
            this.cbEquipmentClass.Name = "cbEquipmentClass";
            this.cbEquipmentClass.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbEquipmentClass.Properties.Appearance.Options.UseFont = true;
            this.cbEquipmentClass.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbEquipmentClass.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cbEquipmentClass.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbEquipmentClass.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.cbEquipmentClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbEquipmentClass.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("EquipmentClassName", "Equipment Class Name", 160, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.cbEquipmentClass.Properties.DataSource = this.qEquipmentClassesBindingSource;
            this.cbEquipmentClass.Properties.DisplayMember = "EquipmentClassName";
            this.cbEquipmentClass.Properties.NullText = "";
            this.cbEquipmentClass.Properties.ShowFooter = false;
            this.cbEquipmentClass.Properties.ShowHeader = false;
            this.cbEquipmentClass.Properties.ValueMember = "EquipmentClassID";
            this.cbEquipmentClass.Size = new System.Drawing.Size(314, 26);
            this.cbEquipmentClass.TabIndex = 5;
            this.cbEquipmentClass.EditValueChanged += new System.EventHandler(this.cbEquipmentClass_EditValueChanged);
            this.cbEquipmentClass.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.cbEquipmentClass_EditValueChanging);
            // 
            // qEquipmentRecordBindingSource
            // 
            this.qEquipmentRecordBindingSource.DataMember = "QEquipmentRecord";
            this.qEquipmentRecordBindingSource.DataSource = this.dataSetQuery2;
            // 
            // dataSetQuery2
            // 
            this.dataSetQuery2.DataSetName = "DataSetQuery2";
            this.dataSetQuery2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // qEquipmentClassesBindingSource
            // 
            this.qEquipmentClassesBindingSource.DataMember = "QEquipmentClasses";
            this.qEquipmentClassesBindingSource.DataSource = this.dataSetQuery;
            // 
            // dataSetQuery
            // 
            this.dataSetQuery.DataSetName = "DataSetQuery";
            this.dataSetQuery.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControl4.Location = new System.Drawing.Point(12, 40);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(135, 20);
            this.labelControl4.TabIndex = 25;
            this.labelControl4.Text = "Вид оборудования:";
            // 
            // btnClearPicture
            // 
            this.btnClearPicture.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClearPicture.Appearance.Options.UseFont = true;
            this.btnClearPicture.Image = ((System.Drawing.Image)(resources.GetObject("btnClearPicture.Image")));
            this.btnClearPicture.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnClearPicture.Location = new System.Drawing.Point(752, 72);
            this.btnClearPicture.LookAndFeel.SkinName = "Caramel";
            this.btnClearPicture.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClearPicture.Name = "btnClearPicture";
            this.btnClearPicture.Size = new System.Drawing.Size(32, 31);
            this.btnClearPicture.TabIndex = 8;
            this.btnClearPicture.ToolTip = "Удалить изображение";
            this.btnClearPicture.ToolTipController = this.toolTip;
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
            this.btnChangePicture.Location = new System.Drawing.Point(752, 37);
            this.btnChangePicture.LookAndFeel.SkinName = "Caramel";
            this.btnChangePicture.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnChangePicture.Name = "btnChangePicture";
            this.btnChangePicture.Size = new System.Drawing.Size(32, 31);
            this.btnChangePicture.TabIndex = 7;
            this.btnChangePicture.ToolTip = "Изменить изображение";
            this.btnChangePicture.ToolTipController = this.toolTip;
            this.btnChangePicture.Click += new System.EventHandler(this.btnChangePicture_Click);
            // 
            // peImage
            // 
            this.peImage.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.qEquipmentRecordBindingSource, "Image", true));
            this.peImage.Location = new System.Drawing.Point(567, 37);
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
            this.peImage.TabIndex = 6;
            this.peImage.ToolTipController = this.toolTip;
            this.peImage.EditValueChanged += new System.EventHandler(this.peImage_EditValueChanged);
            this.peImage.DoubleClick += new System.EventHandler(this.peImage_DoubleClick);
            // 
            // teEquipmentNumber
            // 
            this.teEquipmentNumber.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.qEquipmentRecordBindingSource, "EquipmentNumber", true));
            this.teEquipmentNumber.Location = new System.Drawing.Point(244, 108);
            this.teEquipmentNumber.Name = "teEquipmentNumber";
            this.teEquipmentNumber.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.teEquipmentNumber.Properties.Appearance.Options.UseFont = true;
            this.teEquipmentNumber.Size = new System.Drawing.Size(314, 26);
            this.teEquipmentNumber.TabIndex = 3;
            this.teEquipmentNumber.EditValueChanged += new System.EventHandler(this.teEquipmentNumber_EditValueChanged);
            // 
            // teEquipmentName
            // 
            this.teEquipmentName.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.qEquipmentRecordBindingSource, "EquipmentName", true));
            this.teEquipmentName.EditValue = "";
            this.teEquipmentName.Location = new System.Drawing.Point(244, 73);
            this.teEquipmentName.Name = "teEquipmentName";
            this.teEquipmentName.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.teEquipmentName.Properties.Appearance.Options.UseFont = true;
            this.teEquipmentName.Size = new System.Drawing.Size(314, 26);
            this.teEquipmentName.TabIndex = 2;
            this.teEquipmentName.EditValueChanged += new System.EventHandler(this.teEquipmentName_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControl2.Location = new System.Drawing.Point(12, 111);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(126, 20);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Заводской номер:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControl1.Location = new System.Drawing.Point(12, 76);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(217, 20);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Диспетчерское наименование:";
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
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.panelControl4);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 722);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(829, 48);
            this.panelControl2.TabIndex = 20;
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.bNext);
            this.panelControl4.Controls.Add(this.bCancel);
            this.panelControl4.Controls.Add(this.bSave);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl4.Location = new System.Drawing.Point(308, 0);
            this.panelControl4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(521, 48);
            this.panelControl4.TabIndex = 21;
            // 
            // bNext
            // 
            this.bNext.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bNext.Appearance.Options.UseFont = true;
            this.bNext.Image = ((System.Drawing.Image)(resources.GetObject("bNext.Image")));
            this.bNext.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.bNext.Location = new System.Drawing.Point(18, 8);
            this.bNext.LookAndFeel.SkinName = "Caramel";
            this.bNext.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.bNext.Name = "bNext";
            this.bNext.Size = new System.Drawing.Size(248, 33);
            this.bNext.TabIndex = 9;
            this.bNext.Text = "Визуальное обследование";
            this.bNext.Visible = false;
            this.bNext.Click += new System.EventHandler(this.bNext_Click);
            // 
            // bCancel
            // 
            this.bCancel.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bCancel.Appearance.Options.UseFont = true;
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Image = ((System.Drawing.Image)(resources.GetObject("bCancel.Image")));
            this.bCancel.Location = new System.Drawing.Point(399, 8);
            this.bCancel.LookAndFeel.SkinName = "Caramel";
            this.bCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(111, 33);
            this.bCancel.TabIndex = 11;
            this.bCancel.Text = "Отменить";
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bSave
            // 
            this.bSave.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bSave.Appearance.Options.UseFont = true;
            this.bSave.Image = ((System.Drawing.Image)(resources.GetObject("bSave.Image")));
            this.bSave.Location = new System.Drawing.Point(274, 8);
            this.bSave.LookAndFeel.SkinName = "Caramel";
            this.bSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(117, 33);
            this.bSave.TabIndex = 10;
            this.bSave.Text = "Сохранить";
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.groupControl2);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 192);
            this.panelControl3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.panelControl3.Size = new System.Drawing.Size(829, 530);
            this.panelControl3.TabIndex = 21;
            // 
            // groupControl2
            // 
            this.groupControl2.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupControl2.Appearance.Options.UseFont = true;
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.Controls.Add(this.GridVertical);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(9, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(811, 530);
            this.groupControl2.TabIndex = 3;
            this.groupControl2.Text = "Другие параметры";
            // 
            // GridVertical
            // 
            this.GridVertical.Appearance.Category.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GridVertical.Appearance.Category.Options.UseFont = true;
            this.GridVertical.Appearance.CategoryExpandButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GridVertical.Appearance.CategoryExpandButton.Options.UseFont = true;
            this.GridVertical.Appearance.RecordValue.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GridVertical.Appearance.RecordValue.Options.UseFont = true;
            this.GridVertical.Appearance.RowHeaderPanel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GridVertical.Appearance.RowHeaderPanel.Options.UseFont = true;
            this.GridVertical.Appearance.RowHeaderPanel.Options.UseTextOptions = true;
            this.GridVertical.Appearance.RowHeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GridVertical.DataSource = this.qEquipmentRecordBindingSource;
            this.GridVertical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridVertical.LayoutStyle = DevExpress.XtraVerticalGrid.LayoutViewStyle.SingleRecordView;
            this.GridVertical.Location = new System.Drawing.Point(2, 28);
            this.GridVertical.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GridVertical.Name = "GridVertical";
            this.GridVertical.RecordWidth = 123;
            this.GridVertical.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repSubstation_,
            this.repManufacturer,
            this.rep4Digits,
            this.rep2Digits,
            this.repEquipmentType,
            this.repConstructionType,
            this.repCoolingSystemType,
            this.repNominalVoltageLow,
            this.repNominalVoltageMid,
            this.repNominalVoltageHigh,
            this.repProtectionOilType,
            this.repositoryItemMemoEdit1,
            this.repositoryItemLookUpEdit1,
            this.repInputKind,
            this.repRPNCnt,
            this.repRPNVoltage,
            this.repSubject,
            this.repBranch,
            this.repSubstation,
            this.repRPNKind,
            this.repInputNumber,
            this.repRPNType,
            this.repInputType,
            this.repManufacturerInput,
            this.rep5Digits,
            this.repSwitchDriveType,
            this.repInputName,
            this.repPower});
            this.GridVertical.RowHeaderWidth = 77;
            this.GridVertical.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.categoryRow1,
            this.categoryRow2,
            this.categoryRow3,
            this.catRPN,
            this.catVoltage,
            this.catInputHighA,
            this.catInputHighB,
            this.catInputHighC,
            this.catInputMiddleA,
            this.catInputMiddleB,
            this.catInputMiddleC,
            this.catInputNeutral,
            this.catTmp});
            this.GridVertical.Size = new System.Drawing.Size(807, 500);
            this.GridVertical.TabIndex = 1;
            this.GridVertical.ToolTipController = this.toolTip;
            this.GridVertical.FocusedRowChanged += new DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventHandler(this.GridVertical_FocusedRowChanged);
            this.GridVertical.CustomDrawRowValueCell += new DevExpress.XtraVerticalGrid.Events.CustomDrawRowValueCellEventHandler(this.GridVertical_CustomDrawRowValueCell);
            this.GridVertical.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.GridVertical_ShowingEditor);
            this.GridVertical.CellValueChanged += new DevExpress.XtraVerticalGrid.Events.CellValueChangedEventHandler(this.vGridControl1_CellValueChanged);
            this.GridVertical.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.GridVertical_ValidatingEditor);
            // 
            // qManufacturersByKindBindingSource
            // 
            this.qManufacturersByKindBindingSource.DataMember = "QManufacturersByKind";
            this.qManufacturersByKindBindingSource.DataSource = this.dataSetQuery;
            // 
            // qEquipmentTypesByKindBindingSource
            // 
            this.qEquipmentTypesByKindBindingSource.DataMember = "QEquipmentTypesByKind";
            this.qEquipmentTypesByKindBindingSource.DataSource = this.dataSetQuery;
            // 
            // qSubjectsBindingSource
            // 
            this.qSubjectsBindingSource.DataMember = "QSubjects";
            this.qSubjectsBindingSource.DataSource = this.dataSetQuery;
            // 
            // qBranchesBySubjectBindingSource
            // 
            this.qBranchesBySubjectBindingSource.DataMember = "QBranchesBySubject";
            this.qBranchesBySubjectBindingSource.DataSource = this.dataSetQuery;
            // 
            // qSubstationsByBranchBindingSource
            // 
            this.qSubstationsByBranchBindingSource.DataMember = "QSubstationsByBranch";
            this.qSubstationsByBranchBindingSource.DataSource = this.dataSetQuery;
            // 
            // qRPNTypesByKindBindingSource
            // 
            this.qRPNTypesByKindBindingSource.DataMember = "QRPNTypesByKind";
            this.qRPNTypesByKindBindingSource.DataSource = this.dataSetQuery;
            // 
            // qInputVoltageTypesByKindBindingSource
            // 
            this.qInputVoltageTypesByKindBindingSource.DataMember = "QInputVoltageTypesByKind";
            this.qInputVoltageTypesByKindBindingSource.DataSource = this.dataSetQuery;
            // 
            // qManufacturersInputsByKindBindingSource
            // 
            this.qManufacturersInputsByKindBindingSource.DataMember = "QManufacturersInputsByKind";
            this.qManufacturersInputsByKindBindingSource.DataSource = this.dataSetQuery;
            // 
            // qSwitchDriveTypesByKindBindingSource
            // 
            this.qSwitchDriveTypesByKindBindingSource.DataMember = "QSwitchDriveTypesByKind";
            this.qSwitchDriveTypesByKindBindingSource.DataSource = this.dataSetQuery;
            // 
            // equipmentKindsTableAdapter
            // 
            this.equipmentKindsTableAdapter.ClearBeforeFill = true;
            // 
            // qEquipmentRecordTableAdapter
            // 
            this.qEquipmentRecordTableAdapter.ClearBeforeFill = true;
            // 
            // qEquipmentTypesByKindTableAdapter
            // 
            this.qEquipmentTypesByKindTableAdapter.ClearBeforeFill = true;
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
            // qRPNTypesByKindTableAdapter
            // 
            this.qRPNTypesByKindTableAdapter.ClearBeforeFill = true;
            // 
            // qInputVoltageTypesByKindTableAdapter
            // 
            this.qInputVoltageTypesByKindTableAdapter.ClearBeforeFill = true;
            // 
            // qEquipmentClassesTableAdapter
            // 
            this.qEquipmentClassesTableAdapter.ClearBeforeFill = true;
            // 
            // qSwitchDriveTypesByKindTableAdapter
            // 
            this.qSwitchDriveTypesByKindTableAdapter.ClearBeforeFill = true;
            // 
            // qManufacturersByKindTableAdapter
            // 
            this.qManufacturersByKindTableAdapter.ClearBeforeFill = true;
            // 
            // qManufacturersInputsByKindTableAdapter
            // 
            this.qManufacturersInputsByKindTableAdapter.ClearBeforeFill = true;
            // 
            // repSubstation_
            // 
            this.repSubstation_.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repSubstation_.Appearance.Options.UseFont = true;
            this.repSubstation_.AppearanceDropDown.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repSubstation_.AppearanceDropDown.Options.UseFont = true;
            this.repSubstation_.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "Открыть справочник \"Подстанции\"", null, null, true)});
            this.repSubstation_.DisplayMember = "SubstationName";
            this.repSubstation_.Name = "repSubstation_";
            this.repSubstation_.NullText = "";
            this.repSubstation_.PopupFormSize = new System.Drawing.Size(600, 0);
            this.repSubstation_.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.repSubstation_.ValueMember = "SubstationID";
            this.repSubstation_.View = this.repositoryItemGridLookUpEdit1View;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repositoryItemGridLookUpEdit1View.Appearance.ColumnFilterButton.Options.UseFont = true;
            this.repositoryItemGridLookUpEdit1View.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repositoryItemGridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = true;
            this.repositoryItemGridLookUpEdit1View.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repositoryItemGridLookUpEdit1View.Appearance.Row.Options.UseFont = true;
            this.repositoryItemGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSubjectName,
            this.colBranchName,
            this.colSubstationName});
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.repositoryItemGridLookUpEdit1View.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.repositoryItemGridLookUpEdit1View.OptionsBehavior.Editable = false;
            this.repositoryItemGridLookUpEdit1View.OptionsCustomization.AllowColumnMoving = false;
            this.repositoryItemGridLookUpEdit1View.OptionsCustomization.AllowGroup = false;
            this.repositoryItemGridLookUpEdit1View.OptionsMenu.EnableColumnMenu = false;
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceHideSelection = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ColumnAutoWidth = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // colSubjectName
            // 
            this.colSubjectName.Caption = "Субъект";
            this.colSubjectName.FieldName = "SubjectName";
            this.colSubjectName.Name = "colSubjectName";
            this.colSubjectName.Visible = true;
            this.colSubjectName.VisibleIndex = 0;
            this.colSubjectName.Width = 150;
            // 
            // colBranchName
            // 
            this.colBranchName.Caption = "Филиал";
            this.colBranchName.FieldName = "BranchName";
            this.colBranchName.Name = "colBranchName";
            this.colBranchName.Visible = true;
            this.colBranchName.VisibleIndex = 1;
            this.colBranchName.Width = 200;
            // 
            // colSubstationName
            // 
            this.colSubstationName.Caption = "Подстанция";
            this.colSubstationName.FieldName = "SubstationName";
            this.colSubstationName.Name = "colSubstationName";
            this.colSubstationName.Visible = true;
            this.colSubstationName.VisibleIndex = 2;
            this.colSubstationName.Width = 230;
            // 
            // repManufacturer
            // 
            this.repManufacturer.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repManufacturer.Appearance.Options.UseFont = true;
            this.repManufacturer.AppearanceDropDown.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repManufacturer.AppearanceDropDown.Options.UseFont = true;
            this.repManufacturer.AutoHeight = false;
            this.repManufacturer.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "Открыть справочник \"Заводы-изготовители\"", null, null, true)});
            this.repManufacturer.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ManufacturerName", "Manufacturer Name", 142, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.repManufacturer.DataSource = this.qManufacturersByKindBindingSource;
            this.repManufacturer.DisplayMember = "ManufacturerName";
            this.repManufacturer.Name = "repManufacturer";
            this.repManufacturer.NullText = "";
            this.repManufacturer.ShowFooter = false;
            this.repManufacturer.ShowHeader = false;
            this.repManufacturer.ValueMember = "ManufacturerID";
            this.repManufacturer.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repManufacturer_ButtonClick);
            this.repManufacturer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.repLookUp_KeyUp);
            // 
            // rep4Digits
            // 
            this.rep4Digits.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.rep4Digits.AutoHeight = false;
            this.rep4Digits.Mask.EditMask = "\\d+";
            this.rep4Digits.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.rep4Digits.Mask.ShowPlaceHolders = false;
            this.rep4Digits.MaxLength = 4;
            this.rep4Digits.Name = "rep4Digits";
            // 
            // rep2Digits
            // 
            this.rep2Digits.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.rep2Digits.AutoHeight = false;
            this.rep2Digits.Mask.EditMask = "\\d+";
            this.rep2Digits.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.rep2Digits.Mask.ShowPlaceHolders = false;
            this.rep2Digits.MaxLength = 2;
            this.rep2Digits.Name = "rep2Digits";
            // 
            // repEquipmentType
            // 
            this.repEquipmentType.AppearanceDropDown.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repEquipmentType.AppearanceDropDown.Options.UseFont = true;
            this.repEquipmentType.AutoHeight = false;
            this.repEquipmentType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, "Открыть справочник \"Типы оборудования\"", null, null, true)});
            this.repEquipmentType.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("EquipmentTypeName", "Equipment Type Name", 117, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.repEquipmentType.DataSource = this.qEquipmentTypesByKindBindingSource;
            this.repEquipmentType.DisplayMember = "EquipmentTypeName";
            this.repEquipmentType.Name = "repEquipmentType";
            this.repEquipmentType.NullText = "";
            this.repEquipmentType.ShowFooter = false;
            this.repEquipmentType.ShowHeader = false;
            this.repEquipmentType.ValueMember = "EquipmentTypeID";
            this.repEquipmentType.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repEquipmentType_ButtonClick);
            this.repEquipmentType.KeyUp += new System.Windows.Forms.KeyEventHandler(this.repLookUp_KeyUp);
            // 
            // repConstructionType
            // 
            this.repConstructionType.AppearanceDropDown.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repConstructionType.AppearanceDropDown.Options.UseFont = true;
            this.repConstructionType.AutoHeight = false;
            this.repConstructionType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repConstructionType.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("VAL", "Name5")});
            this.repConstructionType.DisplayMember = "VAL";
            this.repConstructionType.Name = "repConstructionType";
            this.repConstructionType.NullText = "";
            this.repConstructionType.ShowFooter = false;
            this.repConstructionType.ShowHeader = false;
            this.repConstructionType.ValueMember = "KEY";
            this.repConstructionType.KeyUp += new System.Windows.Forms.KeyEventHandler(this.repLookUp_KeyUp);
            // 
            // repCoolingSystemType
            // 
            this.repCoolingSystemType.AppearanceDropDown.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repCoolingSystemType.AppearanceDropDown.Options.UseFont = true;
            this.repCoolingSystemType.AutoHeight = false;
            this.repCoolingSystemType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repCoolingSystemType.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("VAL", 140, "Name5")});
            this.repCoolingSystemType.DisplayMember = "VAL";
            this.repCoolingSystemType.Name = "repCoolingSystemType";
            this.repCoolingSystemType.NullText = "";
            this.repCoolingSystemType.ShowFooter = false;
            this.repCoolingSystemType.ShowHeader = false;
            this.repCoolingSystemType.ValueMember = "KEY";
            this.repCoolingSystemType.KeyUp += new System.Windows.Forms.KeyEventHandler(this.repLookUp_KeyUp);
            // 
            // repNominalVoltageLow
            // 
            this.repNominalVoltageLow.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repNominalVoltageLow.Appearance.Options.UseFont = true;
            this.repNominalVoltageLow.Appearance.Options.UseTextOptions = true;
            this.repNominalVoltageLow.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.repNominalVoltageLow.AppearanceDropDown.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repNominalVoltageLow.AppearanceDropDown.Options.UseFont = true;
            this.repNominalVoltageLow.AppearanceDropDown.Options.UseTextOptions = true;
            this.repNominalVoltageLow.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.repNominalVoltageLow.AutoHeight = false;
            this.repNominalVoltageLow.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repNominalVoltageLow.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("VAL", "Name4")});
            this.repNominalVoltageLow.DisplayMember = "VAL";
            this.repNominalVoltageLow.Name = "repNominalVoltageLow";
            this.repNominalVoltageLow.NullText = "";
            this.repNominalVoltageLow.ShowFooter = false;
            this.repNominalVoltageLow.ShowHeader = false;
            this.repNominalVoltageLow.ValueMember = "KEY";
            this.repNominalVoltageLow.KeyUp += new System.Windows.Forms.KeyEventHandler(this.repLookUp_KeyUp);
            // 
            // repNominalVoltageMid
            // 
            this.repNominalVoltageMid.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repNominalVoltageMid.Appearance.Options.UseFont = true;
            this.repNominalVoltageMid.Appearance.Options.UseTextOptions = true;
            this.repNominalVoltageMid.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.repNominalVoltageMid.AppearanceDropDown.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repNominalVoltageMid.AppearanceDropDown.Options.UseFont = true;
            this.repNominalVoltageMid.AppearanceDropDown.Options.UseTextOptions = true;
            this.repNominalVoltageMid.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.repNominalVoltageMid.AutoHeight = false;
            this.repNominalVoltageMid.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repNominalVoltageMid.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("VAL", "Name6")});
            this.repNominalVoltageMid.DisplayMember = "VAL";
            this.repNominalVoltageMid.Name = "repNominalVoltageMid";
            this.repNominalVoltageMid.NullText = "";
            this.repNominalVoltageMid.ShowFooter = false;
            this.repNominalVoltageMid.ShowHeader = false;
            this.repNominalVoltageMid.ValueMember = "KEY";
            this.repNominalVoltageMid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.repLookUp_KeyUp);
            // 
            // repNominalVoltageHigh
            // 
            this.repNominalVoltageHigh.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repNominalVoltageHigh.Appearance.Options.UseFont = true;
            this.repNominalVoltageHigh.Appearance.Options.UseTextOptions = true;
            this.repNominalVoltageHigh.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.repNominalVoltageHigh.AppearanceDropDown.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repNominalVoltageHigh.AppearanceDropDown.Options.UseFont = true;
            this.repNominalVoltageHigh.AppearanceDropDown.Options.UseTextOptions = true;
            this.repNominalVoltageHigh.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.repNominalVoltageHigh.AutoHeight = false;
            this.repNominalVoltageHigh.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repNominalVoltageHigh.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("VAL", "Name8")});
            this.repNominalVoltageHigh.DisplayMember = "VAL";
            this.repNominalVoltageHigh.Name = "repNominalVoltageHigh";
            this.repNominalVoltageHigh.NullText = "";
            this.repNominalVoltageHigh.ShowFooter = false;
            this.repNominalVoltageHigh.ShowHeader = false;
            this.repNominalVoltageHigh.ValueMember = "KEY";
            this.repNominalVoltageHigh.KeyUp += new System.Windows.Forms.KeyEventHandler(this.repLookUp_KeyUp);
            // 
            // repProtectionOilType
            // 
            this.repProtectionOilType.AppearanceDropDown.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repProtectionOilType.AppearanceDropDown.Options.UseFont = true;
            this.repProtectionOilType.AutoHeight = false;
            this.repProtectionOilType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repProtectionOilType.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("VAL", "Name4")});
            this.repProtectionOilType.DisplayMember = "VAL";
            this.repProtectionOilType.Name = "repProtectionOilType";
            this.repProtectionOilType.NullText = "";
            this.repProtectionOilType.ShowFooter = false;
            this.repProtectionOilType.ShowHeader = false;
            this.repProtectionOilType.ValueMember = "KEY";
            this.repProtectionOilType.KeyUp += new System.Windows.Forms.KeyEventHandler(this.repLookUp_KeyUp);
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            this.repositoryItemLookUpEdit1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.repLookUp_KeyUp);
            // 
            // repInputKind
            // 
            this.repInputKind.AutoHeight = false;
            this.repInputKind.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repInputKind.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("VAL", "Name2")});
            this.repInputKind.DisplayMember = "VAL";
            this.repInputKind.Name = "repInputKind";
            this.repInputKind.NullText = "";
            this.repInputKind.ShowFooter = false;
            this.repInputKind.ShowHeader = false;
            this.repInputKind.ValueMember = "KEY";
            this.repInputKind.KeyUp += new System.Windows.Forms.KeyEventHandler(this.repLookUp_KeyUp);
            // 
            // repRPNCnt
            // 
            this.repRPNCnt.AutoHeight = false;
            this.repRPNCnt.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repRPNCnt.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("VAL", "Name9")});
            this.repRPNCnt.DisplayMember = "VAL";
            this.repRPNCnt.Name = "repRPNCnt";
            this.repRPNCnt.NullText = "";
            this.repRPNCnt.ShowFooter = false;
            this.repRPNCnt.ShowHeader = false;
            this.repRPNCnt.ValueMember = "KEY";
            this.repRPNCnt.KeyUp += new System.Windows.Forms.KeyEventHandler(this.repLookUp_KeyUp);
            // 
            // repRPNVoltage
            // 
            this.repRPNVoltage.AutoHeight = false;
            this.repRPNVoltage.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repRPNVoltage.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("VAL", "Name10")});
            this.repRPNVoltage.DisplayMember = "VAL";
            this.repRPNVoltage.Name = "repRPNVoltage";
            this.repRPNVoltage.NullText = "";
            this.repRPNVoltage.ShowFooter = false;
            this.repRPNVoltage.ShowHeader = false;
            this.repRPNVoltage.ValueMember = "KEY";
            this.repRPNVoltage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.repLookUp_KeyUp);
            // 
            // repSubject
            // 
            this.repSubject.AutoHeight = false;
            this.repSubject.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject4, "Открыть справочник \"Субъекты\"", null, null, true)});
            this.repSubject.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SubjectName", "Name1")});
            this.repSubject.DataSource = this.qSubjectsBindingSource;
            this.repSubject.DisplayMember = "SubjectName";
            this.repSubject.Name = "repSubject";
            this.repSubject.NullText = "";
            this.repSubject.ShowFooter = false;
            this.repSubject.ShowHeader = false;
            this.repSubject.ValueMember = "SubjectID";
            this.repSubject.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repSubject_ButtonClick);
            this.repSubject.KeyUp += new System.Windows.Forms.KeyEventHandler(this.repLookUp_KeyUp);
            // 
            // repBranch
            // 
            this.repBranch.AutoHeight = false;
            this.repBranch.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, "Открыть справочник \"Филиалы\"", null, null, true)});
            this.repBranch.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BranchName", "Name2")});
            this.repBranch.DataSource = this.qBranchesBySubjectBindingSource;
            this.repBranch.DisplayMember = "BranchName";
            this.repBranch.Name = "repBranch";
            this.repBranch.NullText = "";
            this.repBranch.ShowFooter = false;
            this.repBranch.ShowHeader = false;
            this.repBranch.ValueMember = "BranchID";
            this.repBranch.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repBranch_ButtonClick);
            this.repBranch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.repLookUp_KeyUp);
            // 
            // repSubstation
            // 
            this.repSubstation.AutoHeight = false;
            this.repSubstation.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject6, "Открыть справочник \"Подстанции/станции\"", null, null, true)});
            this.repSubstation.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SubstationName", "Name3")});
            this.repSubstation.DataSource = this.qSubstationsByBranchBindingSource;
            this.repSubstation.DisplayMember = "SubstationName";
            this.repSubstation.Name = "repSubstation";
            this.repSubstation.NullText = "";
            this.repSubstation.ShowFooter = false;
            this.repSubstation.ShowHeader = false;
            this.repSubstation.ValueMember = "SubstationID";
            this.repSubstation.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repSubstation_ButtonClick_1);
            this.repSubstation.KeyUp += new System.Windows.Forms.KeyEventHandler(this.repLookUp_KeyUp);
            // 
            // repRPNKind
            // 
            this.repRPNKind.AutoHeight = false;
            this.repRPNKind.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repRPNKind.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("VAL", "Name7")});
            this.repRPNKind.DisplayMember = "VAL";
            this.repRPNKind.Name = "repRPNKind";
            this.repRPNKind.NullText = "";
            this.repRPNKind.ShowFooter = false;
            this.repRPNKind.ShowHeader = false;
            this.repRPNKind.ValueMember = "KEY";
            this.repRPNKind.KeyUp += new System.Windows.Forms.KeyEventHandler(this.repLookUp_KeyUp);
            // 
            // repInputNumber
            // 
            this.repInputNumber.AutoHeight = false;
            this.repInputNumber.MaxLength = 128;
            this.repInputNumber.Name = "repInputNumber";
            // 
            // repRPNType
            // 
            this.repRPNType.AutoHeight = false;
            this.repRPNType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject7, "Открыть справочник \"Типы РПН\"", null, null, true)});
            this.repRPNType.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RPNTypeName", "RPN Type Name", 87, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.repRPNType.DataSource = this.qRPNTypesByKindBindingSource;
            this.repRPNType.DisplayMember = "RPNTypeName";
            this.repRPNType.Name = "repRPNType";
            this.repRPNType.NullText = "";
            this.repRPNType.ShowFooter = false;
            this.repRPNType.ShowHeader = false;
            this.repRPNType.ValueMember = "RPNTypeID";
            this.repRPNType.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repRPNType_ButtonClick);
            this.repRPNType.KeyUp += new System.Windows.Forms.KeyEventHandler(this.repLookUp_KeyUp);
            // 
            // repInputType
            // 
            this.repInputType.AutoHeight = false;
            this.repInputType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject8, "Открыть справочник \"Типы вводов\"", null, null, true)});
            this.repInputType.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("InputVoltageTypeName", "Input Voltage Type Name", 132, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.repInputType.DataSource = this.qInputVoltageTypesByKindBindingSource;
            this.repInputType.DisplayMember = "InputVoltageTypeName";
            this.repInputType.Name = "repInputType";
            this.repInputType.NullText = "";
            this.repInputType.ShowFooter = false;
            this.repInputType.ShowHeader = false;
            this.repInputType.ValueMember = "InputVoltageTypeID";
            this.repInputType.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repInputType_ButtonClick);
            this.repInputType.KeyUp += new System.Windows.Forms.KeyEventHandler(this.repLookUp_KeyUp);
            // 
            // repManufacturerInput
            // 
            this.repManufacturerInput.AutoHeight = false;
            this.repManufacturerInput.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repManufacturerInput.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ManufacturerInputName", "Manufacturer Input Name", 134, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.repManufacturerInput.DataSource = this.qManufacturersInputsByKindBindingSource;
            this.repManufacturerInput.DisplayMember = "ManufacturerInputName";
            this.repManufacturerInput.Name = "repManufacturerInput";
            this.repManufacturerInput.NullText = "";
            this.repManufacturerInput.ShowFooter = false;
            this.repManufacturerInput.ShowHeader = false;
            this.repManufacturerInput.ValueMember = "ManufacturerInputID";
            this.repManufacturerInput.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repManufacturerInput_ButtonClick);
            this.repManufacturerInput.KeyUp += new System.Windows.Forms.KeyEventHandler(this.repLookUp_KeyUp);
            // 
            // rep5Digits
            // 
            this.rep5Digits.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.rep5Digits.AutoHeight = false;
            this.rep5Digits.Mask.EditMask = "\\d+";
            this.rep5Digits.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.rep5Digits.Mask.ShowPlaceHolders = false;
            this.rep5Digits.MaxLength = 5;
            this.rep5Digits.Name = "rep5Digits";
            // 
            // repSwitchDriveType
            // 
            this.repSwitchDriveType.AutoHeight = false;
            this.repSwitchDriveType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject9, "Открыть справочник \"Типы привода\"", null, null, true)});
            this.repSwitchDriveType.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SwitchDriveTypeName", "Switch Drive Type Name", 126, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.repSwitchDriveType.DataSource = this.qSwitchDriveTypesByKindBindingSource;
            this.repSwitchDriveType.DisplayMember = "SwitchDriveTypeName";
            this.repSwitchDriveType.Name = "repSwitchDriveType";
            this.repSwitchDriveType.NullText = "";
            this.repSwitchDriveType.ShowFooter = false;
            this.repSwitchDriveType.ShowHeader = false;
            this.repSwitchDriveType.ValueMember = "SwitchDriveTypeID";
            this.repSwitchDriveType.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repSwitchDriveType_ButtonClick);
            this.repSwitchDriveType.KeyUp += new System.Windows.Forms.KeyEventHandler(this.repLookUp_KeyUp);
            // 
            // repInputName
            // 
            this.repInputName.AutoHeight = false;
            this.repInputName.MaxLength = 128;
            this.repInputName.Name = "repInputName";
            // 
            // repPower
            // 
            this.repPower.AutoHeight = false;
            this.repPower.Mask.EditMask = "(\\d+|\\d+,\\d)";
            this.repPower.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.repPower.Mask.ShowPlaceHolders = false;
            this.repPower.Name = "repPower";
            this.repPower.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.repPower_KeyPress);
            // 
            // categoryRow1
            // 
            this.categoryRow1.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rSubject,
            this.rBranch,
            this.rSubstation});
            this.categoryRow1.Name = "categoryRow1";
            this.categoryRow1.OptionsRow.AllowMove = false;
            this.categoryRow1.OptionsRow.AllowSize = false;
            this.categoryRow1.Properties.Caption = "Параметры расположения";
            // 
            // rSubject
            // 
            this.rSubject.Name = "rSubject";
            this.rSubject.OptionsRow.AllowMove = false;
            this.rSubject.OptionsRow.AllowSize = false;
            this.rSubject.Properties.Caption = "Субъект";
            this.rSubject.Properties.FieldName = "SubjectID";
            this.rSubject.Properties.RowEdit = this.repSubject;
            // 
            // rBranch
            // 
            this.rBranch.Name = "rBranch";
            this.rBranch.OptionsRow.AllowMove = false;
            this.rBranch.OptionsRow.AllowSize = false;
            this.rBranch.Properties.Caption = "Филиал";
            this.rBranch.Properties.FieldName = "BranchID";
            this.rBranch.Properties.RowEdit = this.repBranch;
            // 
            // rSubstation
            // 
            this.rSubstation.Name = "rSubstation";
            this.rSubstation.OptionsRow.AllowMove = false;
            this.rSubstation.OptionsRow.AllowSize = false;
            this.rSubstation.Properties.Caption = "Подстанция/станция";
            this.rSubstation.Properties.FieldName = "SubstationID";
            this.rSubstation.Properties.RowEdit = this.repSubstation;
            // 
            // categoryRow2
            // 
            this.categoryRow2.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rManufacturer,
            this.rCreateYear,
            this.rUseBeginYear});
            this.categoryRow2.Height = 22;
            this.categoryRow2.Name = "categoryRow2";
            this.categoryRow2.OptionsRow.AllowMove = false;
            this.categoryRow2.OptionsRow.AllowSize = false;
            this.categoryRow2.Properties.Caption = "Параметры изготовления";
            // 
            // rManufacturer
            // 
            this.rManufacturer.Name = "rManufacturer";
            this.rManufacturer.OptionsRow.AllowMove = false;
            this.rManufacturer.OptionsRow.AllowSize = false;
            this.rManufacturer.Properties.Caption = "Завод-изготовитель";
            this.rManufacturer.Properties.FieldName = "ManufacturerID";
            this.rManufacturer.Properties.RowEdit = this.repManufacturer;
            // 
            // rCreateYear
            // 
            this.rCreateYear.Appearance.Options.UseTextOptions = true;
            this.rCreateYear.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rCreateYear.Name = "rCreateYear";
            this.rCreateYear.OptionsRow.AllowMove = false;
            this.rCreateYear.OptionsRow.AllowSize = false;
            this.rCreateYear.Properties.Caption = "Год изготовления";
            this.rCreateYear.Properties.FieldName = "CreateYear";
            this.rCreateYear.Properties.RowEdit = this.rep4Digits;
            // 
            // rUseBeginYear
            // 
            this.rUseBeginYear.Appearance.Options.UseTextOptions = true;
            this.rUseBeginYear.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rUseBeginYear.Name = "rUseBeginYear";
            this.rUseBeginYear.OptionsRow.AllowMove = false;
            this.rUseBeginYear.OptionsRow.AllowSize = false;
            this.rUseBeginYear.Properties.Caption = "Год ввода в эксплуатацию";
            this.rUseBeginYear.Properties.FieldName = "UseBeginYear";
            this.rUseBeginYear.Properties.RowEdit = this.rep4Digits;
            // 
            // categoryRow3
            // 
            this.categoryRow3.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rEquipmentType,
            this.rConstructionType,
            this.rProtectionOilType,
            this.rCoolingSystemType,
            this.rSwitchDriveType,
            this.rNominalPower,
            this.rNominalCurrent});
            this.categoryRow3.Name = "categoryRow3";
            this.categoryRow3.OptionsRow.AllowMove = false;
            this.categoryRow3.OptionsRow.AllowSize = false;
            this.categoryRow3.Properties.Caption = "Технические параметры";
            // 
            // rEquipmentType
            // 
            this.rEquipmentType.Name = "rEquipmentType";
            this.rEquipmentType.OptionsRow.AllowMove = false;
            this.rEquipmentType.OptionsRow.AllowSize = false;
            this.rEquipmentType.Properties.Caption = "Тип (марка) трансформатора";
            this.rEquipmentType.Properties.FieldName = "EquipmentTypeID";
            this.rEquipmentType.Properties.RowEdit = this.repEquipmentType;
            // 
            // rConstructionType
            // 
            this.rConstructionType.Name = "rConstructionType";
            this.rConstructionType.OptionsRow.AllowMove = false;
            this.rConstructionType.OptionsRow.AllowSize = false;
            this.rConstructionType.Properties.Caption = "Тип исполнения";
            this.rConstructionType.Properties.FieldName = "ConstructionType";
            this.rConstructionType.Properties.RowEdit = this.repConstructionType;
            // 
            // rProtectionOilType
            // 
            this.rProtectionOilType.Name = "rProtectionOilType";
            this.rProtectionOilType.OptionsRow.AllowMove = false;
            this.rProtectionOilType.OptionsRow.AllowSize = false;
            this.rProtectionOilType.Properties.Caption = "Тип защиты масла";
            this.rProtectionOilType.Properties.FieldName = "ProtectionOilTypeID";
            this.rProtectionOilType.Properties.RowEdit = this.repProtectionOilType;
            // 
            // rCoolingSystemType
            // 
            this.rCoolingSystemType.Name = "rCoolingSystemType";
            this.rCoolingSystemType.OptionsRow.AllowMove = false;
            this.rCoolingSystemType.OptionsRow.AllowSize = false;
            this.rCoolingSystemType.Properties.Caption = "Тип системы охлаждения";
            this.rCoolingSystemType.Properties.FieldName = "CoolingSystemTypeID";
            this.rCoolingSystemType.Properties.RowEdit = this.repCoolingSystemType;
            // 
            // rSwitchDriveType
            // 
            this.rSwitchDriveType.Name = "rSwitchDriveType";
            this.rSwitchDriveType.OptionsRow.AllowMove = false;
            this.rSwitchDriveType.OptionsRow.AllowSize = false;
            this.rSwitchDriveType.Properties.Caption = "Тип привода";
            this.rSwitchDriveType.Properties.FieldName = "SwitchDriveTypeID";
            this.rSwitchDriveType.Properties.RowEdit = this.repSwitchDriveType;
            // 
            // rNominalPower
            // 
            this.rNominalPower.Appearance.Options.UseTextOptions = true;
            this.rNominalPower.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rNominalPower.Name = "rNominalPower";
            this.rNominalPower.OptionsRow.AllowMove = false;
            this.rNominalPower.OptionsRow.AllowSize = false;
            this.rNominalPower.Properties.Caption = "Номинальная мощность, МВА";
            this.rNominalPower.Properties.FieldName = "NominalPower";
            this.rNominalPower.Properties.RowEdit = this.repPower;
            // 
            // rNominalCurrent
            // 
            this.rNominalCurrent.Appearance.Options.UseTextOptions = true;
            this.rNominalCurrent.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rNominalCurrent.Name = "rNominalCurrent";
            this.rNominalCurrent.OptionsRow.AllowMove = false;
            this.rNominalCurrent.OptionsRow.AllowSize = false;
            this.rNominalCurrent.Properties.Caption = "Номинальный ток, А";
            this.rNominalCurrent.Properties.FieldName = "NominalCurrent";
            this.rNominalCurrent.Properties.RowEdit = this.rep5Digits;
            // 
            // catRPN
            // 
            this.catRPN.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rRPNCnt,
            this.rRPNVoltage,
            this.rRPNType,
            this.rRPNKind,
            this.rRPNNumber,
            this.rRPNNumber2,
            this.rRPNNumber3});
            this.catRPN.Name = "catRPN";
            this.catRPN.Properties.Caption = "РПН";
            // 
            // rRPNCnt
            // 
            this.rRPNCnt.Name = "rRPNCnt";
            this.rRPNCnt.OptionsRow.AllowMove = false;
            this.rRPNCnt.OptionsRow.AllowSize = false;
            this.rRPNCnt.Properties.Caption = "Наличие";
            this.rRPNCnt.Properties.FieldName = "RPNCnt";
            this.rRPNCnt.Properties.RowEdit = this.repRPNCnt;
            // 
            // rRPNVoltage
            // 
            this.rRPNVoltage.Name = "rRPNVoltage";
            this.rRPNVoltage.OptionsRow.AllowMove = false;
            this.rRPNVoltage.OptionsRow.AllowSize = false;
            this.rRPNVoltage.Properties.Caption = "Класс напряжения, кВ";
            this.rRPNVoltage.Properties.FieldName = "RPNVoltage";
            this.rRPNVoltage.Properties.RowEdit = this.repRPNVoltage;
            // 
            // rRPNType
            // 
            this.rRPNType.Name = "rRPNType";
            this.rRPNType.OptionsRow.AllowMove = false;
            this.rRPNType.OptionsRow.AllowSize = false;
            this.rRPNType.Properties.Caption = "Тип";
            this.rRPNType.Properties.FieldName = "RPNTypeID";
            this.rRPNType.Properties.RowEdit = this.repRPNType;
            // 
            // rRPNKind
            // 
            this.rRPNKind.Name = "rRPNKind";
            this.rRPNKind.OptionsRow.AllowMove = false;
            this.rRPNKind.OptionsRow.AllowSize = false;
            this.rRPNKind.Properties.Caption = "Вид";
            this.rRPNKind.Properties.FieldName = "RPNKind";
            this.rRPNKind.Properties.RowEdit = this.repRPNKind;
            // 
            // rRPNNumber
            // 
            this.rRPNNumber.Name = "rRPNNumber";
            this.rRPNNumber.OptionsRow.AllowMove = false;
            this.rRPNNumber.OptionsRow.AllowSize = false;
            this.rRPNNumber.Properties.Caption = "Заводской номер (фаза A)";
            this.rRPNNumber.Properties.FieldName = "RPNNumber";
            this.rRPNNumber.Properties.RowEdit = this.repInputNumber;
            // 
            // rRPNNumber2
            // 
            this.rRPNNumber2.Name = "rRPNNumber2";
            this.rRPNNumber2.OptionsRow.AllowMove = false;
            this.rRPNNumber2.OptionsRow.AllowSize = false;
            this.rRPNNumber2.Properties.Caption = "Заводской номер (фаза B)";
            this.rRPNNumber2.Properties.FieldName = "RPNNumber2";
            this.rRPNNumber2.Properties.RowEdit = this.repInputNumber;
            // 
            // rRPNNumber3
            // 
            this.rRPNNumber3.Name = "rRPNNumber3";
            this.rRPNNumber3.OptionsRow.AllowMove = false;
            this.rRPNNumber3.OptionsRow.AllowSize = false;
            this.rRPNNumber3.Properties.Caption = "Заводской номер (фаза C)";
            this.rRPNNumber3.Properties.FieldName = "RPNNumber3";
            this.rRPNNumber3.Properties.RowEdit = this.repInputNumber;
            // 
            // catVoltage
            // 
            this.catVoltage.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rNominalVoltageLow,
            this.rNominalVoltageMiddle,
            this.rNominalVoltageHigh,
            this.rNominalVoltageNeitral});
            this.catVoltage.Name = "catVoltage";
            this.catVoltage.OptionsRow.AllowMove = false;
            this.catVoltage.OptionsRow.AllowSize = false;
            this.catVoltage.Properties.Caption = "Класс напряжения";
            // 
            // rNominalVoltageLow
            // 
            this.rNominalVoltageLow.Name = "rNominalVoltageLow";
            this.rNominalVoltageLow.OptionsRow.AllowMove = false;
            this.rNominalVoltageLow.OptionsRow.AllowSize = false;
            this.rNominalVoltageLow.Properties.Caption = "Класс напряжения НН, кВ";
            this.rNominalVoltageLow.Properties.FieldName = "NominalVoltageLow";
            this.rNominalVoltageLow.Properties.RowEdit = this.repNominalVoltageLow;
            // 
            // rNominalVoltageMiddle
            // 
            this.rNominalVoltageMiddle.Name = "rNominalVoltageMiddle";
            this.rNominalVoltageMiddle.OptionsRow.AllowMove = false;
            this.rNominalVoltageMiddle.OptionsRow.AllowSize = false;
            this.rNominalVoltageMiddle.Properties.Caption = "Класс напряжения СН, кВ";
            this.rNominalVoltageMiddle.Properties.FieldName = "NominalVoltageMiddle";
            this.rNominalVoltageMiddle.Properties.RowEdit = this.repNominalVoltageMid;
            // 
            // rNominalVoltageHigh
            // 
            this.rNominalVoltageHigh.Name = "rNominalVoltageHigh";
            this.rNominalVoltageHigh.OptionsRow.AllowMove = false;
            this.rNominalVoltageHigh.OptionsRow.AllowSize = false;
            this.rNominalVoltageHigh.Properties.Caption = "Класс напряжения ВН, кВ";
            this.rNominalVoltageHigh.Properties.FieldName = "NominalVoltageHigh";
            this.rNominalVoltageHigh.Properties.RowEdit = this.repNominalVoltageHigh;
            // 
            // rNominalVoltageNeitral
            // 
            this.rNominalVoltageNeitral.Name = "rNominalVoltageNeitral";
            this.rNominalVoltageNeitral.OptionsRow.AllowMove = false;
            this.rNominalVoltageNeitral.OptionsRow.AllowSize = false;
            this.rNominalVoltageNeitral.Properties.Caption = "Класс напряжения нейтрали, кВ";
            this.rNominalVoltageNeitral.Properties.FieldName = "NominalVoltageNeitral";
            this.rNominalVoltageNeitral.Properties.RowEdit = this.repNominalVoltageLow;
            // 
            // catInputHighA
            // 
            this.catInputHighA.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rInputNameHighA,
            this.rInputTypeIDHighA,
            this.rInputKindHighA,
            this.rInputManufacturerIDHighA,
            this.rInputNumberHighA,
            this.rInputCreateYearHighA,
            this.rInputUseBeginYearHighA});
            this.catInputHighA.Name = "catInputHighA";
            this.catInputHighA.Properties.Caption = "Ввод ВН фаза А";
            // 
            // rInputNameHighA
            // 
            this.rInputNameHighA.Name = "rInputNameHighA";
            this.rInputNameHighA.OptionsRow.AllowMove = false;
            this.rInputNameHighA.OptionsRow.AllowSize = false;
            this.rInputNameHighA.Properties.Caption = "Сторона";
            this.rInputNameHighA.Properties.FieldName = "InputNameHighA";
            this.rInputNameHighA.Properties.RowEdit = this.repInputName;
            // 
            // rInputTypeIDHighA
            // 
            this.rInputTypeIDHighA.Name = "rInputTypeIDHighA";
            this.rInputTypeIDHighA.OptionsRow.AllowMove = false;
            this.rInputTypeIDHighA.OptionsRow.AllowSize = false;
            this.rInputTypeIDHighA.Properties.Caption = "Тип";
            this.rInputTypeIDHighA.Properties.FieldName = "InputTypeIDHighA";
            this.rInputTypeIDHighA.Properties.RowEdit = this.repInputType;
            // 
            // rInputKindHighA
            // 
            this.rInputKindHighA.Name = "rInputKindHighA";
            this.rInputKindHighA.OptionsRow.AllowMove = false;
            this.rInputKindHighA.OptionsRow.AllowSize = false;
            this.rInputKindHighA.Properties.Caption = "Вид";
            this.rInputKindHighA.Properties.FieldName = "InputKindHighA";
            this.rInputKindHighA.Properties.RowEdit = this.repInputKind;
            // 
            // rInputManufacturerIDHighA
            // 
            this.rInputManufacturerIDHighA.Name = "rInputManufacturerIDHighA";
            this.rInputManufacturerIDHighA.OptionsRow.AllowMove = false;
            this.rInputManufacturerIDHighA.OptionsRow.AllowSize = false;
            this.rInputManufacturerIDHighA.Properties.Caption = "Завод-изготовитель";
            this.rInputManufacturerIDHighA.Properties.FieldName = "InputManufacturerIDHighA";
            this.rInputManufacturerIDHighA.Properties.RowEdit = this.repManufacturerInput;
            // 
            // rInputNumberHighA
            // 
            this.rInputNumberHighA.Name = "rInputNumberHighA";
            this.rInputNumberHighA.OptionsRow.AllowMove = false;
            this.rInputNumberHighA.OptionsRow.AllowSize = false;
            this.rInputNumberHighA.Properties.Caption = "Заводской номер";
            this.rInputNumberHighA.Properties.FieldName = "InputNumberHighA";
            this.rInputNumberHighA.Properties.RowEdit = this.repInputNumber;
            // 
            // rInputCreateYearHighA
            // 
            this.rInputCreateYearHighA.Appearance.Options.UseTextOptions = true;
            this.rInputCreateYearHighA.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rInputCreateYearHighA.Name = "rInputCreateYearHighA";
            this.rInputCreateYearHighA.OptionsRow.AllowMove = false;
            this.rInputCreateYearHighA.OptionsRow.AllowSize = false;
            this.rInputCreateYearHighA.Properties.Caption = "Год изготовления";
            this.rInputCreateYearHighA.Properties.FieldName = "InputCreateYearHighA";
            this.rInputCreateYearHighA.Properties.RowEdit = this.rep4Digits;
            // 
            // rInputUseBeginYearHighA
            // 
            this.rInputUseBeginYearHighA.Appearance.Options.UseTextOptions = true;
            this.rInputUseBeginYearHighA.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rInputUseBeginYearHighA.Name = "rInputUseBeginYearHighA";
            this.rInputUseBeginYearHighA.OptionsRow.AllowMove = false;
            this.rInputUseBeginYearHighA.OptionsRow.AllowSize = false;
            this.rInputUseBeginYearHighA.Properties.Caption = "Год ввода в эксплуатацию";
            this.rInputUseBeginYearHighA.Properties.FieldName = "InputUseBeginYearHighA";
            this.rInputUseBeginYearHighA.Properties.RowEdit = this.rep4Digits;
            // 
            // catInputHighB
            // 
            this.catInputHighB.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rInputNameHighB,
            this.rInputTypeIDHighB,
            this.rInputKindHighB,
            this.rInputManufacturerIDHighB,
            this.rInputNumberHighB,
            this.rInputCreateYearHighB,
            this.rInputUseBeginYearHighB});
            this.catInputHighB.Name = "catInputHighB";
            this.catInputHighB.Properties.Caption = "Ввод ВН фаза B";
            // 
            // rInputNameHighB
            // 
            this.rInputNameHighB.Name = "rInputNameHighB";
            this.rInputNameHighB.OptionsRow.AllowMove = false;
            this.rInputNameHighB.OptionsRow.AllowSize = false;
            this.rInputNameHighB.Properties.Caption = "Сторона";
            this.rInputNameHighB.Properties.FieldName = "InputNameHighB";
            this.rInputNameHighB.Properties.RowEdit = this.repInputName;
            // 
            // rInputTypeIDHighB
            // 
            this.rInputTypeIDHighB.Name = "rInputTypeIDHighB";
            this.rInputTypeIDHighB.OptionsRow.AllowMove = false;
            this.rInputTypeIDHighB.OptionsRow.AllowSize = false;
            this.rInputTypeIDHighB.Properties.Caption = "Тип";
            this.rInputTypeIDHighB.Properties.FieldName = "InputTypeIDHighB";
            this.rInputTypeIDHighB.Properties.RowEdit = this.repInputType;
            // 
            // rInputKindHighB
            // 
            this.rInputKindHighB.Name = "rInputKindHighB";
            this.rInputKindHighB.OptionsRow.AllowMove = false;
            this.rInputKindHighB.OptionsRow.AllowSize = false;
            this.rInputKindHighB.Properties.Caption = "Вид";
            this.rInputKindHighB.Properties.FieldName = "InputKindHighB";
            this.rInputKindHighB.Properties.RowEdit = this.repInputKind;
            // 
            // rInputManufacturerIDHighB
            // 
            this.rInputManufacturerIDHighB.Name = "rInputManufacturerIDHighB";
            this.rInputManufacturerIDHighB.OptionsRow.AllowMove = false;
            this.rInputManufacturerIDHighB.OptionsRow.AllowSize = false;
            this.rInputManufacturerIDHighB.Properties.Caption = "Завод-изготовитель";
            this.rInputManufacturerIDHighB.Properties.FieldName = "InputManufacturerIDHighB";
            this.rInputManufacturerIDHighB.Properties.RowEdit = this.repManufacturerInput;
            // 
            // rInputNumberHighB
            // 
            this.rInputNumberHighB.Name = "rInputNumberHighB";
            this.rInputNumberHighB.OptionsRow.AllowMove = false;
            this.rInputNumberHighB.OptionsRow.AllowSize = false;
            this.rInputNumberHighB.Properties.Caption = "Заводской номер";
            this.rInputNumberHighB.Properties.FieldName = "InputNumberHighB";
            this.rInputNumberHighB.Properties.RowEdit = this.repInputNumber;
            // 
            // rInputCreateYearHighB
            // 
            this.rInputCreateYearHighB.Appearance.Options.UseTextOptions = true;
            this.rInputCreateYearHighB.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rInputCreateYearHighB.Name = "rInputCreateYearHighB";
            this.rInputCreateYearHighB.OptionsRow.AllowMove = false;
            this.rInputCreateYearHighB.OptionsRow.AllowSize = false;
            this.rInputCreateYearHighB.Properties.Caption = "Год изготовления";
            this.rInputCreateYearHighB.Properties.FieldName = "InputCreateYearHighB";
            this.rInputCreateYearHighB.Properties.RowEdit = this.rep4Digits;
            // 
            // rInputUseBeginYearHighB
            // 
            this.rInputUseBeginYearHighB.Appearance.Options.UseTextOptions = true;
            this.rInputUseBeginYearHighB.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rInputUseBeginYearHighB.Name = "rInputUseBeginYearHighB";
            this.rInputUseBeginYearHighB.OptionsRow.AllowMove = false;
            this.rInputUseBeginYearHighB.OptionsRow.AllowSize = false;
            this.rInputUseBeginYearHighB.Properties.Caption = "Год ввода в эксплуатацию";
            this.rInputUseBeginYearHighB.Properties.FieldName = "InputUseBeginYearHighB";
            this.rInputUseBeginYearHighB.Properties.RowEdit = this.rep4Digits;
            // 
            // catInputHighC
            // 
            this.catInputHighC.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rInputNameHighC,
            this.rInputTypeIDHighC,
            this.rInputKindHighC,
            this.rInputManufacturerIDHighC,
            this.rInputNumberHighC,
            this.rInputCreateYearHighC,
            this.rInputUseBeginYearHighC});
            this.catInputHighC.Name = "catInputHighC";
            this.catInputHighC.Properties.Caption = "Ввод ВН фаза C";
            // 
            // rInputNameHighC
            // 
            this.rInputNameHighC.Name = "rInputNameHighC";
            this.rInputNameHighC.OptionsRow.AllowMove = false;
            this.rInputNameHighC.OptionsRow.AllowSize = false;
            this.rInputNameHighC.Properties.Caption = "Сторона";
            this.rInputNameHighC.Properties.FieldName = "InputNameHighC";
            this.rInputNameHighC.Properties.RowEdit = this.repInputName;
            // 
            // rInputTypeIDHighC
            // 
            this.rInputTypeIDHighC.Name = "rInputTypeIDHighC";
            this.rInputTypeIDHighC.OptionsRow.AllowMove = false;
            this.rInputTypeIDHighC.OptionsRow.AllowSize = false;
            this.rInputTypeIDHighC.Properties.Caption = "Тип";
            this.rInputTypeIDHighC.Properties.FieldName = "InputTypeIDHighC";
            this.rInputTypeIDHighC.Properties.RowEdit = this.repInputType;
            // 
            // rInputKindHighC
            // 
            this.rInputKindHighC.Name = "rInputKindHighC";
            this.rInputKindHighC.OptionsRow.AllowMove = false;
            this.rInputKindHighC.OptionsRow.AllowSize = false;
            this.rInputKindHighC.Properties.Caption = "Вид";
            this.rInputKindHighC.Properties.FieldName = "InputKindHighC";
            this.rInputKindHighC.Properties.RowEdit = this.repInputKind;
            // 
            // rInputManufacturerIDHighC
            // 
            this.rInputManufacturerIDHighC.Name = "rInputManufacturerIDHighC";
            this.rInputManufacturerIDHighC.OptionsRow.AllowMove = false;
            this.rInputManufacturerIDHighC.OptionsRow.AllowSize = false;
            this.rInputManufacturerIDHighC.Properties.Caption = "Завод-изготовитель";
            this.rInputManufacturerIDHighC.Properties.FieldName = "InputManufacturerIDHighC";
            this.rInputManufacturerIDHighC.Properties.RowEdit = this.repManufacturerInput;
            // 
            // rInputNumberHighC
            // 
            this.rInputNumberHighC.Name = "rInputNumberHighC";
            this.rInputNumberHighC.OptionsRow.AllowMove = false;
            this.rInputNumberHighC.OptionsRow.AllowSize = false;
            this.rInputNumberHighC.Properties.Caption = "Заводской номер";
            this.rInputNumberHighC.Properties.FieldName = "InputNumberHighC";
            this.rInputNumberHighC.Properties.RowEdit = this.repInputNumber;
            // 
            // rInputCreateYearHighC
            // 
            this.rInputCreateYearHighC.Appearance.Options.UseTextOptions = true;
            this.rInputCreateYearHighC.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rInputCreateYearHighC.Name = "rInputCreateYearHighC";
            this.rInputCreateYearHighC.OptionsRow.AllowMove = false;
            this.rInputCreateYearHighC.OptionsRow.AllowSize = false;
            this.rInputCreateYearHighC.Properties.Caption = "Год изготовления";
            this.rInputCreateYearHighC.Properties.FieldName = "InputCreateYearHighC";
            this.rInputCreateYearHighC.Properties.RowEdit = this.rep4Digits;
            // 
            // rInputUseBeginYearHighC
            // 
            this.rInputUseBeginYearHighC.Appearance.Options.UseTextOptions = true;
            this.rInputUseBeginYearHighC.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rInputUseBeginYearHighC.Name = "rInputUseBeginYearHighC";
            this.rInputUseBeginYearHighC.OptionsRow.AllowMove = false;
            this.rInputUseBeginYearHighC.OptionsRow.AllowSize = false;
            this.rInputUseBeginYearHighC.Properties.Caption = "Год ввода в эксплуатацию";
            this.rInputUseBeginYearHighC.Properties.FieldName = "InputUseBeginYearHighC";
            this.rInputUseBeginYearHighC.Properties.RowEdit = this.rep4Digits;
            // 
            // catInputMiddleA
            // 
            this.catInputMiddleA.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rInputNameMiddleA,
            this.rInputTypeIDMiddleA,
            this.rInputKindMiddleA,
            this.rInputManufacturerIDMiddleA,
            this.rInputNumberMiddleA,
            this.rInputCreateYearMiddleA,
            this.rInputUseBeginYearMiddleA});
            this.catInputMiddleA.Name = "catInputMiddleA";
            this.catInputMiddleA.Properties.Caption = "Ввод СН фаза А";
            // 
            // rInputNameMiddleA
            // 
            this.rInputNameMiddleA.Name = "rInputNameMiddleA";
            this.rInputNameMiddleA.OptionsRow.AllowMove = false;
            this.rInputNameMiddleA.OptionsRow.AllowSize = false;
            this.rInputNameMiddleA.Properties.Caption = "Сторона";
            this.rInputNameMiddleA.Properties.FieldName = "InputNameMiddleA";
            this.rInputNameMiddleA.Properties.RowEdit = this.repInputName;
            // 
            // rInputTypeIDMiddleA
            // 
            this.rInputTypeIDMiddleA.Name = "rInputTypeIDMiddleA";
            this.rInputTypeIDMiddleA.OptionsRow.AllowMove = false;
            this.rInputTypeIDMiddleA.OptionsRow.AllowSize = false;
            this.rInputTypeIDMiddleA.Properties.Caption = "Тип";
            this.rInputTypeIDMiddleA.Properties.FieldName = "InputTypeIDMiddleA";
            this.rInputTypeIDMiddleA.Properties.RowEdit = this.repInputType;
            // 
            // rInputKindMiddleA
            // 
            this.rInputKindMiddleA.Name = "rInputKindMiddleA";
            this.rInputKindMiddleA.OptionsRow.AllowMove = false;
            this.rInputKindMiddleA.OptionsRow.AllowSize = false;
            this.rInputKindMiddleA.Properties.Caption = "Вид";
            this.rInputKindMiddleA.Properties.FieldName = "InputKindMiddleA";
            this.rInputKindMiddleA.Properties.RowEdit = this.repInputKind;
            // 
            // rInputManufacturerIDMiddleA
            // 
            this.rInputManufacturerIDMiddleA.Name = "rInputManufacturerIDMiddleA";
            this.rInputManufacturerIDMiddleA.OptionsRow.AllowMove = false;
            this.rInputManufacturerIDMiddleA.OptionsRow.AllowSize = false;
            this.rInputManufacturerIDMiddleA.Properties.Caption = "Завод-изготовитель";
            this.rInputManufacturerIDMiddleA.Properties.FieldName = "InputManufacturerIDMiddleA";
            this.rInputManufacturerIDMiddleA.Properties.RowEdit = this.repManufacturerInput;
            // 
            // rInputNumberMiddleA
            // 
            this.rInputNumberMiddleA.Name = "rInputNumberMiddleA";
            this.rInputNumberMiddleA.OptionsRow.AllowMove = false;
            this.rInputNumberMiddleA.OptionsRow.AllowSize = false;
            this.rInputNumberMiddleA.Properties.Caption = "Заводской номер";
            this.rInputNumberMiddleA.Properties.FieldName = "InputNumberMiddleA";
            this.rInputNumberMiddleA.Properties.RowEdit = this.repInputNumber;
            // 
            // rInputCreateYearMiddleA
            // 
            this.rInputCreateYearMiddleA.Appearance.Options.UseTextOptions = true;
            this.rInputCreateYearMiddleA.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rInputCreateYearMiddleA.Name = "rInputCreateYearMiddleA";
            this.rInputCreateYearMiddleA.OptionsRow.AllowMove = false;
            this.rInputCreateYearMiddleA.OptionsRow.AllowSize = false;
            this.rInputCreateYearMiddleA.Properties.Caption = "Год изготовления";
            this.rInputCreateYearMiddleA.Properties.FieldName = "InputCreateYearMiddleA";
            this.rInputCreateYearMiddleA.Properties.RowEdit = this.rep4Digits;
            // 
            // rInputUseBeginYearMiddleA
            // 
            this.rInputUseBeginYearMiddleA.Appearance.Options.UseTextOptions = true;
            this.rInputUseBeginYearMiddleA.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rInputUseBeginYearMiddleA.Name = "rInputUseBeginYearMiddleA";
            this.rInputUseBeginYearMiddleA.OptionsRow.AllowMove = false;
            this.rInputUseBeginYearMiddleA.OptionsRow.AllowSize = false;
            this.rInputUseBeginYearMiddleA.Properties.Caption = "Год ввода в эксплуатацию";
            this.rInputUseBeginYearMiddleA.Properties.FieldName = "InputUseBeginYearMiddleA";
            this.rInputUseBeginYearMiddleA.Properties.RowEdit = this.rep4Digits;
            // 
            // catInputMiddleB
            // 
            this.catInputMiddleB.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rInputNameMiddleB,
            this.rInputTypeIDMiddleB,
            this.rInputKindMiddleB,
            this.rInputManufacturerIDMiddleB,
            this.rInputNumberMiddleB,
            this.rInputCreateYearMiddleB,
            this.rInputUseBeginYearMiddleB});
            this.catInputMiddleB.Name = "catInputMiddleB";
            this.catInputMiddleB.Properties.Caption = "Ввод СН фаза В";
            // 
            // rInputNameMiddleB
            // 
            this.rInputNameMiddleB.Name = "rInputNameMiddleB";
            this.rInputNameMiddleB.OptionsRow.AllowMove = false;
            this.rInputNameMiddleB.OptionsRow.AllowSize = false;
            this.rInputNameMiddleB.Properties.Caption = "Сторона";
            this.rInputNameMiddleB.Properties.FieldName = "InputNameMiddleB";
            this.rInputNameMiddleB.Properties.RowEdit = this.repInputName;
            // 
            // rInputTypeIDMiddleB
            // 
            this.rInputTypeIDMiddleB.Name = "rInputTypeIDMiddleB";
            this.rInputTypeIDMiddleB.OptionsRow.AllowMove = false;
            this.rInputTypeIDMiddleB.OptionsRow.AllowSize = false;
            this.rInputTypeIDMiddleB.Properties.Caption = "Тип";
            this.rInputTypeIDMiddleB.Properties.FieldName = "InputTypeIDMiddleB";
            this.rInputTypeIDMiddleB.Properties.RowEdit = this.repInputType;
            // 
            // rInputKindMiddleB
            // 
            this.rInputKindMiddleB.Name = "rInputKindMiddleB";
            this.rInputKindMiddleB.OptionsRow.AllowMove = false;
            this.rInputKindMiddleB.OptionsRow.AllowSize = false;
            this.rInputKindMiddleB.Properties.Caption = "Вид";
            this.rInputKindMiddleB.Properties.FieldName = "InputKindMiddleB";
            this.rInputKindMiddleB.Properties.RowEdit = this.repInputKind;
            // 
            // rInputManufacturerIDMiddleB
            // 
            this.rInputManufacturerIDMiddleB.Name = "rInputManufacturerIDMiddleB";
            this.rInputManufacturerIDMiddleB.OptionsRow.AllowMove = false;
            this.rInputManufacturerIDMiddleB.OptionsRow.AllowSize = false;
            this.rInputManufacturerIDMiddleB.Properties.Caption = "Завод-изготовитель";
            this.rInputManufacturerIDMiddleB.Properties.FieldName = "InputManufacturerIDMiddleB";
            this.rInputManufacturerIDMiddleB.Properties.RowEdit = this.repManufacturerInput;
            // 
            // rInputNumberMiddleB
            // 
            this.rInputNumberMiddleB.Name = "rInputNumberMiddleB";
            this.rInputNumberMiddleB.OptionsRow.AllowMove = false;
            this.rInputNumberMiddleB.OptionsRow.AllowSize = false;
            this.rInputNumberMiddleB.Properties.Caption = "Заводской номер";
            this.rInputNumberMiddleB.Properties.FieldName = "InputNumberMiddleB";
            this.rInputNumberMiddleB.Properties.RowEdit = this.repInputNumber;
            // 
            // rInputCreateYearMiddleB
            // 
            this.rInputCreateYearMiddleB.Appearance.Options.UseTextOptions = true;
            this.rInputCreateYearMiddleB.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rInputCreateYearMiddleB.Name = "rInputCreateYearMiddleB";
            this.rInputCreateYearMiddleB.OptionsRow.AllowMove = false;
            this.rInputCreateYearMiddleB.OptionsRow.AllowSize = false;
            this.rInputCreateYearMiddleB.Properties.Caption = "Год изготовления";
            this.rInputCreateYearMiddleB.Properties.FieldName = "InputCreateYearMiddleB";
            this.rInputCreateYearMiddleB.Properties.RowEdit = this.rep4Digits;
            // 
            // rInputUseBeginYearMiddleB
            // 
            this.rInputUseBeginYearMiddleB.Appearance.Options.UseTextOptions = true;
            this.rInputUseBeginYearMiddleB.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rInputUseBeginYearMiddleB.Name = "rInputUseBeginYearMiddleB";
            this.rInputUseBeginYearMiddleB.OptionsRow.AllowMove = false;
            this.rInputUseBeginYearMiddleB.OptionsRow.AllowSize = false;
            this.rInputUseBeginYearMiddleB.Properties.Caption = "Год ввода в эксплуатацию";
            this.rInputUseBeginYearMiddleB.Properties.FieldName = "InputUseBeginYearMiddleB";
            this.rInputUseBeginYearMiddleB.Properties.RowEdit = this.rep4Digits;
            // 
            // catInputMiddleC
            // 
            this.catInputMiddleC.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rInputNameMiddleC,
            this.rInputTypeIDMiddleC,
            this.rInputKindMiddleC,
            this.rInputManufacturerIDMiddleC,
            this.rInputNumberMiddleC,
            this.rInputCreateYearMiddleC,
            this.rInputUseBeginYearMiddleC});
            this.catInputMiddleC.Name = "catInputMiddleC";
            this.catInputMiddleC.Properties.Caption = "Ввод СН фаза C";
            // 
            // rInputNameMiddleC
            // 
            this.rInputNameMiddleC.Name = "rInputNameMiddleC";
            this.rInputNameMiddleC.OptionsRow.AllowMove = false;
            this.rInputNameMiddleC.OptionsRow.AllowSize = false;
            this.rInputNameMiddleC.Properties.Caption = "Сторона";
            this.rInputNameMiddleC.Properties.FieldName = "InputNameMiddleC";
            this.rInputNameMiddleC.Properties.RowEdit = this.repInputName;
            // 
            // rInputTypeIDMiddleC
            // 
            this.rInputTypeIDMiddleC.Name = "rInputTypeIDMiddleC";
            this.rInputTypeIDMiddleC.OptionsRow.AllowMove = false;
            this.rInputTypeIDMiddleC.OptionsRow.AllowSize = false;
            this.rInputTypeIDMiddleC.Properties.Caption = "Тип";
            this.rInputTypeIDMiddleC.Properties.FieldName = "InputTypeIDMiddleC";
            this.rInputTypeIDMiddleC.Properties.RowEdit = this.repInputType;
            // 
            // rInputKindMiddleC
            // 
            this.rInputKindMiddleC.Name = "rInputKindMiddleC";
            this.rInputKindMiddleC.OptionsRow.AllowMove = false;
            this.rInputKindMiddleC.OptionsRow.AllowSize = false;
            this.rInputKindMiddleC.Properties.Caption = "Вид";
            this.rInputKindMiddleC.Properties.FieldName = "InputKindMiddleC";
            this.rInputKindMiddleC.Properties.RowEdit = this.repInputKind;
            // 
            // rInputManufacturerIDMiddleC
            // 
            this.rInputManufacturerIDMiddleC.Name = "rInputManufacturerIDMiddleC";
            this.rInputManufacturerIDMiddleC.OptionsRow.AllowMove = false;
            this.rInputManufacturerIDMiddleC.OptionsRow.AllowSize = false;
            this.rInputManufacturerIDMiddleC.Properties.Caption = "Завод-изготовитель";
            this.rInputManufacturerIDMiddleC.Properties.FieldName = "InputManufacturerIDMiddleC";
            this.rInputManufacturerIDMiddleC.Properties.RowEdit = this.repManufacturerInput;
            // 
            // rInputNumberMiddleC
            // 
            this.rInputNumberMiddleC.Name = "rInputNumberMiddleC";
            this.rInputNumberMiddleC.OptionsRow.AllowMove = false;
            this.rInputNumberMiddleC.OptionsRow.AllowSize = false;
            this.rInputNumberMiddleC.Properties.Caption = "Заводской номер";
            this.rInputNumberMiddleC.Properties.FieldName = "InputNumberMiddleC";
            this.rInputNumberMiddleC.Properties.RowEdit = this.repInputNumber;
            // 
            // rInputCreateYearMiddleC
            // 
            this.rInputCreateYearMiddleC.Appearance.Options.UseTextOptions = true;
            this.rInputCreateYearMiddleC.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rInputCreateYearMiddleC.Name = "rInputCreateYearMiddleC";
            this.rInputCreateYearMiddleC.OptionsRow.AllowMove = false;
            this.rInputCreateYearMiddleC.OptionsRow.AllowSize = false;
            this.rInputCreateYearMiddleC.Properties.Caption = "Год изготовления";
            this.rInputCreateYearMiddleC.Properties.FieldName = "InputCreateYearMiddleC";
            this.rInputCreateYearMiddleC.Properties.RowEdit = this.rep4Digits;
            // 
            // rInputUseBeginYearMiddleC
            // 
            this.rInputUseBeginYearMiddleC.Appearance.Options.UseTextOptions = true;
            this.rInputUseBeginYearMiddleC.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rInputUseBeginYearMiddleC.Name = "rInputUseBeginYearMiddleC";
            this.rInputUseBeginYearMiddleC.OptionsRow.AllowMove = false;
            this.rInputUseBeginYearMiddleC.OptionsRow.AllowSize = false;
            this.rInputUseBeginYearMiddleC.Properties.Caption = "Год ввода в эксплуатацию";
            this.rInputUseBeginYearMiddleC.Properties.FieldName = "InputUseBeginYearMiddleC";
            this.rInputUseBeginYearMiddleC.Properties.RowEdit = this.rep4Digits;
            // 
            // catInputNeutral
            // 
            this.catInputNeutral.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rInputNameNeutral,
            this.rInputTypeIDNeutral,
            this.rInputKindNeutral,
            this.rInputManufacturerIDNeutral,
            this.rInputNumberNeutral,
            this.rInputCreateYearNeutral,
            this.rInputUseBeginYearNeutral});
            this.catInputNeutral.Name = "catInputNeutral";
            this.catInputNeutral.Properties.Caption = "Ввод нейтраль";
            // 
            // rInputNameNeutral
            // 
            this.rInputNameNeutral.Name = "rInputNameNeutral";
            this.rInputNameNeutral.OptionsRow.AllowMove = false;
            this.rInputNameNeutral.OptionsRow.AllowSize = false;
            this.rInputNameNeutral.Properties.Caption = "Сторона";
            this.rInputNameNeutral.Properties.FieldName = "InputNameNeutral";
            this.rInputNameNeutral.Properties.RowEdit = this.repInputName;
            // 
            // rInputTypeIDNeutral
            // 
            this.rInputTypeIDNeutral.Name = "rInputTypeIDNeutral";
            this.rInputTypeIDNeutral.OptionsRow.AllowMove = false;
            this.rInputTypeIDNeutral.OptionsRow.AllowSize = false;
            this.rInputTypeIDNeutral.Properties.Caption = "Тип";
            this.rInputTypeIDNeutral.Properties.FieldName = "InputTypeIDNeutral";
            this.rInputTypeIDNeutral.Properties.RowEdit = this.repInputType;
            // 
            // rInputKindNeutral
            // 
            this.rInputKindNeutral.Name = "rInputKindNeutral";
            this.rInputKindNeutral.OptionsRow.AllowMove = false;
            this.rInputKindNeutral.OptionsRow.AllowSize = false;
            this.rInputKindNeutral.Properties.Caption = "Вид";
            this.rInputKindNeutral.Properties.FieldName = "InputKindNeutral";
            this.rInputKindNeutral.Properties.RowEdit = this.repInputKind;
            // 
            // rInputManufacturerIDNeutral
            // 
            this.rInputManufacturerIDNeutral.Name = "rInputManufacturerIDNeutral";
            this.rInputManufacturerIDNeutral.OptionsRow.AllowMove = false;
            this.rInputManufacturerIDNeutral.OptionsRow.AllowSize = false;
            this.rInputManufacturerIDNeutral.Properties.Caption = "Завод-изготовитель";
            this.rInputManufacturerIDNeutral.Properties.FieldName = "InputManufacturerIDNeutral";
            this.rInputManufacturerIDNeutral.Properties.RowEdit = this.repManufacturerInput;
            // 
            // rInputNumberNeutral
            // 
            this.rInputNumberNeutral.Name = "rInputNumberNeutral";
            this.rInputNumberNeutral.OptionsRow.AllowMove = false;
            this.rInputNumberNeutral.OptionsRow.AllowSize = false;
            this.rInputNumberNeutral.Properties.Caption = "Заводской номер";
            this.rInputNumberNeutral.Properties.FieldName = "InputNumberNeutral";
            this.rInputNumberNeutral.Properties.RowEdit = this.repInputNumber;
            // 
            // rInputCreateYearNeutral
            // 
            this.rInputCreateYearNeutral.Appearance.Options.UseTextOptions = true;
            this.rInputCreateYearNeutral.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rInputCreateYearNeutral.Name = "rInputCreateYearNeutral";
            this.rInputCreateYearNeutral.OptionsRow.AllowMove = false;
            this.rInputCreateYearNeutral.OptionsRow.AllowSize = false;
            this.rInputCreateYearNeutral.Properties.Caption = "Год изготовления";
            this.rInputCreateYearNeutral.Properties.FieldName = "InputCreateYearNeutral";
            this.rInputCreateYearNeutral.Properties.RowEdit = this.rep4Digits;
            // 
            // rInputUseBeginYearNeutral
            // 
            this.rInputUseBeginYearNeutral.Appearance.Options.UseTextOptions = true;
            this.rInputUseBeginYearNeutral.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rInputUseBeginYearNeutral.Name = "rInputUseBeginYearNeutral";
            this.rInputUseBeginYearNeutral.OptionsRow.AllowMove = false;
            this.rInputUseBeginYearNeutral.OptionsRow.AllowSize = false;
            this.rInputUseBeginYearNeutral.Properties.Caption = "Год ввода в эксплуатацию";
            this.rInputUseBeginYearNeutral.Properties.FieldName = "InputUseBeginYearNeutral";
            this.rInputUseBeginYearNeutral.Properties.RowEdit = this.rep4Digits;
            // 
            // catTmp
            // 
            this.catTmp.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rProjectLifeTime,
            this.rActualLifeTime,
            this.rLastWorkoverYear,
            this.rLastTechnicalServiceYear,
            this.rTechnicalServiceDocument,
            this.rTechnicalServiceConclusion,
            this.rNextTechnicalServiceYear,
            this.rTechnicalServiceCount});
            this.catTmp.Name = "catTmp";
            this.catTmp.OptionsRow.AllowMove = false;
            this.catTmp.OptionsRow.AllowSize = false;
            this.catTmp.Properties.Caption = "Параметры эксплуатации";
            this.catTmp.Visible = false;
            // 
            // rProjectLifeTime
            // 
            this.rProjectLifeTime.Appearance.Options.UseTextOptions = true;
            this.rProjectLifeTime.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rProjectLifeTime.Name = "rProjectLifeTime";
            this.rProjectLifeTime.OptionsRow.AllowMove = false;
            this.rProjectLifeTime.OptionsRow.AllowSize = false;
            this.rProjectLifeTime.Properties.Caption = "Проектный срок службы, лет";
            this.rProjectLifeTime.Properties.FieldName = "ProjectLifeTime";
            this.rProjectLifeTime.Properties.RowEdit = this.rep2Digits;
            // 
            // rActualLifeTime
            // 
            this.rActualLifeTime.Appearance.Options.UseTextOptions = true;
            this.rActualLifeTime.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rActualLifeTime.Name = "rActualLifeTime";
            this.rActualLifeTime.OptionsRow.AllowMove = false;
            this.rActualLifeTime.OptionsRow.AllowSize = false;
            this.rActualLifeTime.Properties.Caption = "Фактический срок службы, лет";
            this.rActualLifeTime.Properties.FieldName = "ActualLifeTime";
            this.rActualLifeTime.Properties.RowEdit = this.rep2Digits;
            // 
            // rLastWorkoverYear
            // 
            this.rLastWorkoverYear.Appearance.Options.UseTextOptions = true;
            this.rLastWorkoverYear.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rLastWorkoverYear.Name = "rLastWorkoverYear";
            this.rLastWorkoverYear.OptionsRow.AllowMove = false;
            this.rLastWorkoverYear.OptionsRow.AllowSize = false;
            this.rLastWorkoverYear.Properties.Caption = "Год последнего капремонта";
            this.rLastWorkoverYear.Properties.FieldName = "LastWorkoverYear";
            this.rLastWorkoverYear.Properties.RowEdit = this.rep4Digits;
            // 
            // rLastTechnicalServiceYear
            // 
            this.rLastTechnicalServiceYear.Appearance.Options.UseTextOptions = true;
            this.rLastTechnicalServiceYear.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rLastTechnicalServiceYear.Name = "rLastTechnicalServiceYear";
            this.rLastTechnicalServiceYear.OptionsRow.AllowMove = false;
            this.rLastTechnicalServiceYear.OptionsRow.AllowSize = false;
            this.rLastTechnicalServiceYear.Properties.Caption = "Год последнего ТО";
            this.rLastTechnicalServiceYear.Properties.FieldName = "LastTechnicalServiceYear";
            this.rLastTechnicalServiceYear.Properties.RowEdit = this.rep4Digits;
            // 
            // rTechnicalServiceDocument
            // 
            this.rTechnicalServiceDocument.Name = "rTechnicalServiceDocument";
            this.rTechnicalServiceDocument.OptionsRow.AllowMove = false;
            this.rTechnicalServiceDocument.OptionsRow.AllowSize = false;
            this.rTechnicalServiceDocument.Properties.Caption = "Документ и организация по итогам ТО";
            this.rTechnicalServiceDocument.Properties.FieldName = "TechnicalServiceDocument";
            this.rTechnicalServiceDocument.Properties.RowEdit = this.repositoryItemMemoEdit1;
            // 
            // rTechnicalServiceConclusion
            // 
            this.rTechnicalServiceConclusion.Name = "rTechnicalServiceConclusion";
            this.rTechnicalServiceConclusion.OptionsRow.AllowMove = false;
            this.rTechnicalServiceConclusion.OptionsRow.AllowSize = false;
            this.rTechnicalServiceConclusion.Properties.Caption = "Заключение по итогам ТО";
            this.rTechnicalServiceConclusion.Properties.FieldName = "TechnicalServiceConclusion";
            this.rTechnicalServiceConclusion.Properties.RowEdit = this.repositoryItemMemoEdit1;
            // 
            // rNextTechnicalServiceYear
            // 
            this.rNextTechnicalServiceYear.Appearance.Options.UseTextOptions = true;
            this.rNextTechnicalServiceYear.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rNextTechnicalServiceYear.Name = "rNextTechnicalServiceYear";
            this.rNextTechnicalServiceYear.OptionsRow.AllowMove = false;
            this.rNextTechnicalServiceYear.OptionsRow.AllowSize = false;
            this.rNextTechnicalServiceYear.Properties.Caption = "Год следующего ТО";
            this.rNextTechnicalServiceYear.Properties.FieldName = "NextTechnicalServiceYear";
            this.rNextTechnicalServiceYear.Properties.RowEdit = this.rep4Digits;
            // 
            // rTechnicalServiceCount
            // 
            this.rTechnicalServiceCount.Appearance.Options.UseTextOptions = true;
            this.rTechnicalServiceCount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rTechnicalServiceCount.Name = "rTechnicalServiceCount";
            this.rTechnicalServiceCount.OptionsRow.AllowMove = false;
            this.rTechnicalServiceCount.OptionsRow.AllowSize = false;
            this.rTechnicalServiceCount.Properties.Caption = "Количество ТО с начала эксплуатации";
            this.rTechnicalServiceCount.Properties.FieldName = "TechnicalServiceCount";
            this.rTechnicalServiceCount.Properties.RowEdit = this.rep4Digits;
            // 
            // PassportDataForm
            // 
            this.AcceptButton = this.bSave;
            this.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(829, 770);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Caramel";
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(782, 500);
            this.Name = "PassportDataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EquipmentRecordForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PassportDataForm_FormClosing);
            this.Load += new System.EventHandler(this.EquipmentRecordForm_Load);
            this.SizeChanged += new System.EventHandler(this.PassportDataForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbEquipmentClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qEquipmentRecordBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetQuery2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qEquipmentClassesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peImage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teEquipmentNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teEquipmentName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipmentKindsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridVertical)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qManufacturersByKindBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qEquipmentTypesByKindBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qSubjectsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qBranchesBySubjectBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qSubstationsByBranchBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qRPNTypesByKindBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qInputVoltageTypesByKindBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qManufacturersInputsByKindBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qSwitchDriveTypesByKindBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repSubstation_)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repManufacturer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep4Digits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep2Digits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repEquipmentType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repConstructionType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCoolingSystemType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repNominalVoltageLow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repNominalVoltageMid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repNominalVoltageHigh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repProtectionOilType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repInputKind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repRPNCnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repRPNVoltage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repSubject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repBranch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repSubstation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repRPNKind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repInputNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repRPNType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repInputType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repManufacturerInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep5Digits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repSwitchDriveType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repInputName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repPower)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource qEquipmentRecordBindingSource;
        private DataSetQuery dataSetQuery;
        private DataSetQuery2 dataSetQuery2;
        private DataSetQuery2TableAdapters.QEquipmentRecordTableAdapter qEquipmentRecordTableAdapter;
        private DataSetMain dataSetMain;
        private System.Windows.Forms.BindingSource equipmentKindsBindingSource;
        private DataSetMainTableAdapters.EquipmentKindsTableAdapter equipmentKindsTableAdapter;
        private DevExpress.Utils.ToolTipController toolTip;
        private System.Windows.Forms.BindingSource qEquipmentTypesByKindBindingSource;
        private DataSetQueryTableAdapters.QEquipmentTypesByKindTableAdapter qEquipmentTypesByKindTableAdapter;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit teEquipmentNumber;
        private DevExpress.XtraEditors.TextEdit teEquipmentName;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.SimpleButton bCancel;
        private DevExpress.XtraEditors.SimpleButton bSave;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraVerticalGrid.VGridControl GridVertical;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repSubstation_;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colSubjectName;
        private DevExpress.XtraGrid.Columns.GridColumn colBranchName;
        private DevExpress.XtraGrid.Columns.GridColumn colSubstationName;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repManufacturer;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rep4Digits;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rep2Digits;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repEquipmentType;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repConstructionType;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repCoolingSystemType;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repNominalVoltageLow;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repNominalVoltageMid;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repNominalVoltageHigh;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repProtectionOilType;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow categoryRow1;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rSubject;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rBranch;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rSubstation;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow categoryRow2;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rManufacturer;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rCreateYear;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rUseBeginYear;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow categoryRow3;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rEquipmentType;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rConstructionType;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rProtectionOilType;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rCoolingSystemType;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow catVoltage;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rNominalVoltageMiddle;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rNominalVoltageHigh;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rNominalPower;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow catTmp;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rProjectLifeTime;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rActualLifeTime;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rLastWorkoverYear;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rLastTechnicalServiceYear;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rTechnicalServiceDocument;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rTechnicalServiceConclusion;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rNextTechnicalServiceYear;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rTechnicalServiceCount;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rNominalVoltageLow;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow catInputHighA;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputKindHighA;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputKindHighB;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputKindHighC;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputKindMiddleA;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputKindMiddleB;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputKindMiddleC;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputKindNeutral;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repInputKind;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repRPNCnt;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repRPNVoltage;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rRPNCnt;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rRPNVoltage;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repSubject;
        private System.Windows.Forms.BindingSource qSubjectsBindingSource;
        private DataSetQueryTableAdapters.QSubjectsTableAdapter qSubjectsTableAdapter;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repBranch;
        private System.Windows.Forms.BindingSource qBranchesBySubjectBindingSource;
        private DataSetQueryTableAdapters.QBranchesBySubjectTableAdapter qBranchesBySubjectTableAdapter;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repSubstation;
        private System.Windows.Forms.BindingSource qSubstationsByBranchBindingSource;
        private DataSetQueryTableAdapters.QSubstationsByBranchTableAdapter qSubstationsByBranchTableAdapter;
        private DevExpress.XtraEditors.PictureEdit peImage;
        private DevExpress.XtraEditors.SimpleButton btnClearPicture;
        private DevExpress.XtraEditors.SimpleButton btnChangePicture;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rNominalVoltageNeitral;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repRPNKind;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rRPNType;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rRPNKind;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow catInputHighB;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow catInputHighC;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow catInputMiddleA;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow catInputMiddleB;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow catInputMiddleC;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow catInputNeutral;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputManufacturerIDHighA;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputManufacturerIDHighB;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputManufacturerIDHighC;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputManufacturerIDMiddleA;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputManufacturerIDMiddleB;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputManufacturerIDMiddleC;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputManufacturerIDNeutral;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repInputNumber;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputNumberHighA;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputNumberHighB;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputNumberHighC;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputNumberMiddleA;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputNumberMiddleB;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputNumberMiddleC;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputNumberNeutral;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputCreateYearHighA;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputUseBeginYearHighA;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputCreateYearHighB;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputUseBeginYearHighB;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputCreateYearHighC;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputUseBeginYearHighC;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputCreateYearMiddleA;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputUseBeginYearMiddleA;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputCreateYearMiddleB;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputUseBeginYearMiddleB;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputCreateYearMiddleC;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputUseBeginYearMiddleC;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputCreateYearNeutral;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputUseBeginYearNeutral;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow catRPN;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rRPNNumber2;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rRPNNumber3;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rRPNNumber;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputTypeIDHighA;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputTypeIDHighB;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputTypeIDHighC;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputTypeIDMiddleA;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputTypeIDMiddleB;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputTypeIDMiddleC;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputTypeIDNeutral;
        private DevExpress.XtraEditors.SimpleButton bNext;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repRPNType;
        private System.Windows.Forms.BindingSource qRPNTypesByKindBindingSource;
        private DataSetQueryTableAdapters.QRPNTypesByKindTableAdapter qRPNTypesByKindTableAdapter;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repInputType;
        private System.Windows.Forms.BindingSource qInputVoltageTypesByKindBindingSource;
        private DataSetQueryTableAdapters.QInputVoltageTypesByKindTableAdapter qInputVoltageTypesByKindTableAdapter;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repManufacturerInput;
        private DevExpress.XtraEditors.LookUpEdit cbEquipmentClass;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.BindingSource qEquipmentClassesBindingSource;
        private DataSetQueryTableAdapters.QEquipmentClassesTableAdapter qEquipmentClassesTableAdapter;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rNominalCurrent;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rep5Digits;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repSwitchDriveType;
        private System.Windows.Forms.BindingSource qSwitchDriveTypesByKindBindingSource;
        private DataSetQueryTableAdapters.QSwitchDriveTypesByKindTableAdapter qSwitchDriveTypesByKindTableAdapter;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rSwitchDriveType;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputNameHighA;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputNameHighB;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputNameHighC;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputNameMiddleA;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputNameMiddleB;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputNameMiddleC;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rInputNameNeutral;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repInputName;
        private System.Windows.Forms.BindingSource qManufacturersByKindBindingSource;
        private System.Windows.Forms.BindingSource qManufacturersInputsByKindBindingSource;
        private DataSetQueryTableAdapters.QManufacturersByKindTableAdapter qManufacturersByKindTableAdapter;
        private DataSetQueryTableAdapters.QManufacturersInputsByKindTableAdapter qManufacturersInputsByKindTableAdapter;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repPower;
    }
}