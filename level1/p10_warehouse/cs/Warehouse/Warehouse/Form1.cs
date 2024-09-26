using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Warehouse
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            warehouseItems.View = View.Details;
            warehouseItems.Columns.Add("名称", 120, HorizontalAlignment.Left);
            warehouseItems.Columns.Add("型号", 120, HorizontalAlignment.Left);
            warehouseItems.Columns.Add("数量", 120, HorizontalAlignment.Left);

            warehouseItems.Size = new System.Drawing.Size(360, 480);
            Size = new System.Drawing.Size(700, 580);

            Text = "Warehouse System";

            addItem.Location = new Point(480, 78);
            removeItem.Location = new Point(480, 198);
            merge.Location = new Point(480, 318);
            saveAndQuit.Location = new Point(480, 438);
        }

        private void addItem_Click(object sender, EventArgs e)
        {
            ItemAddingForm iaForm = new ItemAddingForm(this);
        }

        List<ListViewItem> items = new List<ListViewItem> { };
    }

    public partial class ItemAddingForm : Form
    {

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        public ItemAddingForm(Form1 parentForm) 
        {
            confirm = new Button();
            cancel = new Button();
            nameLabel = new Label();
            typeLabel = new Label();
            countLabel = new Label();
            nameInput = new TextBox();
            typeInput = new TextBox();
            countInput = new TextBox();


            SuspendLayout();
            Text = "添加物品到仓库中";

            Size = new Size(480, 270);

            Location = new Point(parentForm.Location.X+100, parentForm.Location.Y+100);

            confirm.Text = "添加";
            confirm.TextAlign = ContentAlignment.MiddleCenter;
            confirm.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            confirm.Size = new Size(100,40);
            confirm.Location = new Point(220, 180);
            confirm.Name = "confirm";
            confirm.UseVisualStyleBackColor = true;
            confirm.TabIndex = 0;
            Controls.Add(confirm);

            cancel.Text = "取消";
            cancel.TextAlign = ContentAlignment.MiddleCenter;
            cancel.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cancel.Size = new Size(100, 40);
            cancel.Location = new Point(350, 180);
            cancel.Name = "cancel";
            cancel.UseVisualStyleBackColor = true;
            cancel.TabIndex = 1;
            Controls.Add(cancel);

            nameLabel.Text = "名称：";
            nameLabel.TextAlign = ContentAlignment.MiddleLeft;
            nameLabel.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(20, 20);
            nameLabel.Name = "nameLabel";
            nameLabel.TabIndex = 0;
            Controls.Add(nameLabel);

            typeLabel.Text = "型号：";
            typeLabel.TextAlign = ContentAlignment.MiddleLeft;
            typeLabel.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            typeLabel.AutoSize = true;
            typeLabel.Location = new Point(20, 70);
            typeLabel.Name = "typeLabel";
            typeLabel.TabIndex = 0;
            Controls.Add(typeLabel);

            countLabel.Text = "数量：";
            countLabel.TextAlign = ContentAlignment.MiddleLeft;
            countLabel.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            countLabel.AutoSize = true;
            countLabel.Location = new Point(20, 120);
            countLabel.Name = "countLabel";
            countLabel.TabIndex = 0;
            Controls.Add(countLabel);

            nameInput.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            nameInput.Size = new Size(300, 40);
            nameInput.Location = new Point(120, 20);
            nameInput.Name = "nameInput";
            nameInput.TabIndex = 0;
            Controls.Add(nameInput);

            typeInput.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            typeInput.Size = new Size(300, 40);
            typeInput.Location = new Point(120, 70);
            typeInput.Name = "typeInput";
            typeInput.TabIndex = 0;
            Controls.Add(typeInput);

            countInput.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            countInput.Size = new Size(300, 40);
            countInput.Location = new Point(120, 120);
            countInput.Name = "countInput";
            countInput.TabIndex = 0;
            Controls.Add(countInput);

            AutoScaleDimensions = new SizeF(9F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ResumeLayout(false);

            cancel.Click += new EventHandler(Cancel);

            ShowDialog();

        }

        void Cancel(object sender, EventArgs a) 
        { 
            Close();
            Dispose(true);
        }

        void Confirm(object sender, EventArgs a)
        {
            Close();
            Dispose(true);
        }

        Button confirm;
        Button cancel;
        Label nameLabel;
        Label typeLabel;
        Label countLabel;
        TextBox nameInput;
        TextBox typeInput;
        TextBox countInput;
    }
}
