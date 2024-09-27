using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
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
            warehouseItems.FullRowSelect = true;

            warehouseItems.Size = new Size(380, 480);
            Size = new Size(700, 580);

            Text = "A Warehouse System";

            ReadWarehouse();

            addItem.Location = new Point(480, 78);
            removeItem.Location = new Point(480, 198);
            merge.Location = new Point(480, 318);
            saveAndQuit.Location = new Point(480, 438);


            addItem.Click += new EventHandler(AddItem_Click);
            removeItem.Click += new EventHandler(RemoveItem_Click);
            merge.Click += new EventHandler(MergeItem_Click);
            saveAndQuit.Click += new EventHandler(SaveAndQuit);
        }

        private void AddItem_Click(object sender, EventArgs e)
        {
            ItemAddingForm iaForm = new ItemAddingForm(this);
        }

        private void RemoveItem_Click(object sender, EventArgs e)
        {
            ItemRemovingForm irForm = new ItemRemovingForm(this);
        }

        private void MergeItem_Click(object sender, EventArgs e)
        {
            ItemMergingForm imForm = new ItemMergingForm(this);
        }

        private void ReadWarehouse()
        {
            if (!File.Exists(@".\Warehouse.wh"))
            {
                FileStream f = File.Create(@".\Warehouse.wh");  // 其实就是txt
                f.Close();
            }

            StreamReader sr = new StreamReader(@".\Warehouse.wh", Encoding.UTF8);

            ListViewItem item;
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                item = new ListViewItem();
                item.Text = line;
                item.SubItems.Add(sr.ReadLine());
                item.SubItems.Add(sr.ReadLine());
                warehouseItems.Items.Add(item);
                
            }

            sr.Close();
        }
        internal void SaveWarehouse()
        {
            if (!File.Exists(@".\Warehouse.wh"))
            {
                FileStream f = File.Create(@".\Warehouse.wh");  // 其实就是txt
                f.Close();
            }

            StreamWriter sw = new StreamWriter(@".\Warehouse.wh", false, Encoding.UTF8);

            for (int i = 0; i < warehouseItems.Items.Count; i++)
            {
                sw.WriteLine(warehouseItems.Items[i].SubItems[0].Text);
                sw.WriteLine(warehouseItems.Items[i].SubItems[1].Text);
                sw.WriteLine(warehouseItems.Items[i].SubItems[2].Text);
            }

            sw.Close();
        }

        private void SaveAndQuit(object sender, EventArgs e)
        {
            SaveWarehouse();
            Close();
            Dispose(true);
            Application.Exit();
        }
    }

    public partial class ItemAddingForm : Form
    {

        private IContainer components = null;

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
            tipLabel = new Label();
            this.parentForm = parentForm;


            SuspendLayout();
            Text = "添加物品到仓库中";

            Size = new Size(480, 270);

            Location = new Point(parentForm.Location.X+100, parentForm.Location.Y+100);

            MaximizeBox = false;
            MinimizeBox = false;

            confirm.Text = "添加";
            confirm.TextAlign = ContentAlignment.MiddleCenter;
            confirm.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            confirm.Size = new Size(100,40);
            confirm.Location = new Point(220, 180);
            confirm.Name = "confirm";
            confirm.UseVisualStyleBackColor = true;
            confirm.TabIndex = 3;
            Controls.Add(confirm);

            cancel.Text = "取消";
            cancel.TextAlign = ContentAlignment.MiddleCenter;
            cancel.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cancel.Size = new Size(100, 40);
            cancel.Location = new Point(350, 180);
            cancel.Name = "cancel";
            cancel.UseVisualStyleBackColor = true;
            cancel.TabIndex = 4;
            Controls.Add(cancel);

            nameLabel.Text = "名称：";
            nameLabel.TextAlign = ContentAlignment.MiddleLeft;
            nameLabel.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(20, 20);
            nameLabel.Name = "nameLabel";
            nameLabel.TabIndex = 5;
            Controls.Add(nameLabel);

            typeLabel.Text = "型号：";
            typeLabel.TextAlign = ContentAlignment.MiddleLeft;
            typeLabel.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            typeLabel.AutoSize = true;
            typeLabel.Location = new Point(20, 70);
            typeLabel.Name = "typeLabel";
            typeLabel.TabIndex = 6;
            Controls.Add(typeLabel);

            countLabel.Text = "数量：";
            countLabel.TextAlign = ContentAlignment.MiddleLeft;
            countLabel.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            countLabel.AutoSize = true;
            countLabel.Location = new Point(20, 120);
            countLabel.Name = "countLabel";
            countLabel.TabIndex = 7;
            Controls.Add(countLabel);

            tipLabel.Text = "请输入正整数！！";
            tipLabel.TextAlign = ContentAlignment.MiddleLeft;
            tipLabel.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            tipLabel.AutoSize = true;
            tipLabel.Location = new Point(20, 180);
            tipLabel.Name = "tipLabel";
            tipLabel.ForeColor = Color.Red;
            tipLabel.TabIndex = 4;
            Controls.Add(tipLabel);
            tipLabel.Hide();

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
            typeInput.TabIndex = 1;
            Controls.Add(typeInput);

            countInput.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            countInput.Size = new Size(300, 40);
            countInput.Location = new Point(120, 120);
            countInput.Name = "countInput";
            countInput.TabIndex = 2;
            Controls.Add(countInput);

            AutoScaleDimensions = new SizeF(9F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ResumeLayout(false);

            cancel.Click += new EventHandler(Cancel);
            confirm.Click += new EventHandler(Confirm);

            ShowDialog();

        }

        void Cancel(object sender, EventArgs a) 
        { 
            Close();
            Dispose(true);
        }

        void Confirm(object sender, EventArgs a)
        {
            bool filled = true;

            if (string.IsNullOrWhiteSpace(nameInput.Text))
            {
                nameLabel.ForeColor = Color.Red;
                filled = false;
            }
            else 
            { 
                nameLabel.ForeColor = Color.Black;
            }


            if (string.IsNullOrWhiteSpace(typeInput.Text))
            {
                typeLabel.ForeColor = Color.Red;
                filled = false;
            }
            else
            {
                typeLabel.ForeColor = Color.Black;
            }

            if (string.IsNullOrWhiteSpace(countInput.Text))
            {
                countLabel.ForeColor = Color.Red;
                filled = false;
            }
            else
            {
                countLabel.ForeColor = Color.Black;
            }

            try
            {
                if (int.Parse(countInput.Text) <= 0)
                {
                    countLabel.ForeColor = Color.Red;
                    tipLabel.Show();
                }
                else
                {
                    countLabel.ForeColor = Color.Black;
                    tipLabel.Hide();
                    if (filled)
                    {
                        for (int i = 0; i < parentForm.warehouseItems.Items.Count; i++)
                        {
                            if (parentForm.warehouseItems.Items[i].SubItems[0].Text == nameInput.Text && parentForm.warehouseItems.Items[i].SubItems[1].Text == typeInput.Text)
                            {
                                int count = int.Parse(parentForm.warehouseItems.Items[i].SubItems[2].Text);
                                count += int.Parse(countInput.Text);
                                parentForm.warehouseItems.Items[i].SubItems[2].Text = count.ToString();

                                parentForm.SaveWarehouse();

                                Close();
                                Dispose(true);
                                return;
                            }
                        }

                        ListViewItem newItem = new ListViewItem();
                        newItem.Text = nameInput.Text;
                        newItem.SubItems.Add(typeInput.Text);
                        newItem.SubItems.Add(countInput.Text);
                        parentForm.warehouseItems.Items.Add(newItem);

                        parentForm.SaveWarehouse();

                        Close();
                        Dispose(true);
                    }
                }
            }
            catch 
            {
                countLabel.ForeColor = Color.Red;
            }
            
        }

        private Button confirm;
        private Button cancel;
        private Label nameLabel;
        private Label typeLabel;
        private Label countLabel;
        private Label tipLabel;
        private TextBox nameInput;
        private TextBox typeInput;
        private TextBox countInput;
        private Form1 parentForm;
    }

    public partial class ItemRemovingForm : Form
    {

        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        public ItemRemovingForm(Form1 parentForm)
        {
            confirm = new Button();
            cancel = new Button();
            countLabel = new Label();
            countInput = new TextBox();
            tipLabel = new Label();
            this.parentForm = parentForm;


            SuspendLayout();
            Text = "从仓库移除物品";

            Size = new Size(480, 270);

            Location = new Point(parentForm.Location.X + 100, parentForm.Location.Y + 100);

            MaximizeBox = false;
            MinimizeBox = false;

            confirm.Text = "移除";
            confirm.TextAlign = ContentAlignment.MiddleCenter;
            confirm.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            confirm.Size = new Size(100, 40);
            confirm.Location = new Point(220, 180);
            confirm.Name = "confirm";
            confirm.UseVisualStyleBackColor = true;
            confirm.TabIndex = 1;
            Controls.Add(confirm);

            cancel.Text = "取消";
            cancel.TextAlign = ContentAlignment.MiddleCenter;
            cancel.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cancel.Size = new Size(100, 40);
            cancel.Location = new Point(350, 180);
            cancel.Name = "cancel";
            cancel.UseVisualStyleBackColor = true;
            cancel.TabIndex = 2;
            Controls.Add(cancel);

            countLabel.Text = "数量：";
            countLabel.TextAlign = ContentAlignment.MiddleLeft;
            countLabel.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            countLabel.AutoSize = true;
            countLabel.Location = new Point(20, 70);
            countLabel.Name = "countLabel";
            countLabel.TabIndex = 3;
            Controls.Add(countLabel);

            tipLabel.Text = "您还未选择物品！！";
            tipLabel.TextAlign = ContentAlignment.MiddleLeft;
            tipLabel.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            tipLabel.AutoSize = true;
            tipLabel.Location = new Point(20, 20);
            tipLabel.Name = "tipLabel";
            tipLabel.ForeColor = Color.Red;
            tipLabel.TabIndex = 4;
            Controls.Add(tipLabel);

            countInput.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            countInput.Size = new Size(300, 40);
            countInput.Location = new Point(120, 70);
            countInput.Name = "countInput";
            countInput.TabIndex = 0;
            Controls.Add(countInput);

            AutoScaleDimensions = new SizeF(9F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ResumeLayout(false);

            cancel.Click += new EventHandler(Cancel);
            confirm.Click += new EventHandler(Confirm);

            if (parentForm.warehouseItems.SelectedItems.Count == 0)
            { 
                countInput.Enabled = false;
                confirm.Enabled = false;
            }
            else
            {
                tipLabel.Hide();
            }

            ShowDialog();

        }

        void Cancel(object sender, EventArgs a)
        {
            Close();
            Dispose(true);
        }

        void Confirm(object sender, EventArgs a)
        {
            bool filled = true;

            if (string.IsNullOrWhiteSpace(countInput.Text))
            {
                countLabel.ForeColor = Color.Red;
                filled = false;
            }
            else
            {
                countLabel.ForeColor = Color.Black;
            }

            if (filled)
            {
                int count = int.Parse(parentForm.warehouseItems.FocusedItem.SubItems[2].Text);
                try
                {
                    int delta;
                    if((delta = int.Parse(countInput.Text)) > 0)
                    {
                        count -= delta;

                        bool valid = true;

                        if (count == 0)
                        {
                            parentForm.warehouseItems.Items.Remove(parentForm.warehouseItems.FocusedItem);

                            parentForm.SaveWarehouse();
                        }
                        else if (count < 0)
                        {
                            tipLabel.Text = "移除数量超出存储数量";
                            valid = false;
                            tipLabel.Show();
                        }
                        else
                        {
                            parentForm.warehouseItems.FocusedItem.SubItems[2].Text = count.ToString();

                            parentForm.SaveWarehouse();
                        }
                        if (valid)
                        {
                            Close();
                            Dispose(true);
                        }
                    }
                    else
                    {
                        tipLabel.Text = "请输入正整数";
                        tipLabel.Show();
                    }
                }
                catch 
                {
                    tipLabel.Text = "请输入正整数";
                    tipLabel.Show();
                }

                
            }
        }

        private Button confirm;
        private Button cancel;
        private Label countLabel;
        private Label tipLabel;
        private TextBox countInput;
        private Form1 parentForm;
    }

    public partial class ItemMergingForm : Form
    {

        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        public ItemMergingForm(Form1 parentForm)
        {
            confirm = new Button();
            cancel = new Button();
            nameLabel = new Label();
            typeLabel = new Label();
            countLabel = new Label();
            tipLabel = new Label();
            nameInput = new TextBox();
            typeInput = new TextBox();
            countInput = new TextBox();
            this.parentForm = parentForm;


            SuspendLayout();
            Text = "修改仓库中的物品信息";

            Size = new Size(480, 320);

            Location = new Point(parentForm.Location.X + 100, parentForm.Location.Y + 100);

            MaximizeBox = false;
            MinimizeBox = false;

            confirm.Text = "修改";
            confirm.TextAlign = ContentAlignment.MiddleCenter;
            confirm.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            confirm.Size = new Size(100, 40);
            confirm.Location = new Point(220, 180);
            confirm.Name = "confirm";
            confirm.UseVisualStyleBackColor = true;
            confirm.TabIndex = 3;
            Controls.Add(confirm);

            cancel.Text = "取消";
            cancel.TextAlign = ContentAlignment.MiddleCenter;
            cancel.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cancel.Size = new Size(100, 40);
            cancel.Location = new Point(350, 180);
            cancel.Name = "cancel";
            cancel.UseVisualStyleBackColor = true;
            cancel.TabIndex = 4;
            Controls.Add(cancel);

            nameLabel.Text = "名称：";
            nameLabel.TextAlign = ContentAlignment.MiddleLeft;
            nameLabel.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(20, 20);
            nameLabel.Name = "nameLabel";
            nameLabel.TabIndex = 5;
            Controls.Add(nameLabel);

            typeLabel.Text = "型号：";
            typeLabel.TextAlign = ContentAlignment.MiddleLeft;
            typeLabel.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            typeLabel.AutoSize = true;
            typeLabel.Location = new Point(20, 70);
            typeLabel.Name = "typeLabel";
            typeLabel.TabIndex = 6;
            Controls.Add(typeLabel);

            countLabel.Text = "数量：";
            countLabel.TextAlign = ContentAlignment.MiddleLeft;
            countLabel.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            countLabel.AutoSize = true;
            countLabel.Location = new Point(20, 120);
            countLabel.Name = "countLabel";
            countLabel.TabIndex = 7;
            Controls.Add(countLabel);

            tipLabel.Text = "您还未选择物品！！";
            tipLabel.TextAlign = ContentAlignment.MiddleLeft;
            tipLabel.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            tipLabel.AutoSize = true;
            tipLabel.Location = new Point(20, 180);
            tipLabel.Name = "tipLabel";
            tipLabel.ForeColor = Color.Red;
            tipLabel.TabIndex = 4;
            Controls.Add(tipLabel);

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
            typeInput.TabIndex = 1;
            Controls.Add(typeInput);

            countInput.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            countInput.Size = new Size(300, 40);
            countInput.Location = new Point(120, 120);
            countInput.Name = "countInput";
            countInput.TabIndex = 2;
            Controls.Add(countInput);

            AutoScaleDimensions = new SizeF(9F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ResumeLayout(false);

            cancel.Click += new EventHandler(Cancel);
            confirm.Click += new EventHandler(Confirm);

            if (parentForm.warehouseItems.SelectedItems.Count == 0)
            {
                countInput.Enabled = false;
                typeInput.Enabled = false;
                nameInput.Enabled = false;
                confirm.Enabled = false;
            }
            else
            {
                tipLabel.Hide();
                nameInput.Text = parentForm.warehouseItems.FocusedItem.SubItems[0].Text;
                typeInput.Text = parentForm.warehouseItems.FocusedItem.SubItems[1].Text;
                countInput.Text = parentForm.warehouseItems.FocusedItem.SubItems[2].Text;
            }

            ShowDialog();

        }

        void Cancel(object sender, EventArgs a)
        {
            Close();
            Dispose(true);
        }

        void Confirm(object sender, EventArgs a)
        {
            bool filled = true;

            if (string.IsNullOrWhiteSpace(nameInput.Text))
            {
                nameLabel.ForeColor = Color.Red;
                filled = false;
            }
            else
            {
                nameLabel.ForeColor = Color.Black;
            }


            if (string.IsNullOrWhiteSpace(typeInput.Text))
            {
                typeLabel.ForeColor = Color.Red;
                filled = false;
            }
            else
            {
                typeLabel.ForeColor = Color.Black;
            }

            if (string.IsNullOrWhiteSpace(countInput.Text))
            {
                countLabel.ForeColor = Color.Red;
                filled = false;
            }
            else
            {
                countLabel.ForeColor = Color.Black;
            }

            try
            {
                if (int.Parse(countInput.Text) <= 0)
                {
                    countLabel.ForeColor = Color.Red;
                    tipLabel.Show();
                    tipLabel.Text = "请输入正整数！！";
                    try
                    {
                        tipLabel.Click -= new EventHandler(Combine);
                    }
                    catch { }
                }
                else
                {
                    countLabel.ForeColor = Color.Black;
                    tipLabel.Hide();
                    try
                    {
                        tipLabel.Click -= new EventHandler(Combine);
                    }
                    catch { }
                    if (filled)
                    {
                        for (int i = 0; i < parentForm.warehouseItems.Items.Count; i++)
                        {
                            if (parentForm.warehouseItems.Items[i] == parentForm.warehouseItems.FocusedItem)
                            {
                                continue;
                            }
                            if (parentForm.warehouseItems.Items[i].SubItems[0].Text == nameInput.Text && parentForm.warehouseItems.Items[i].SubItems[1].Text == typeInput.Text)
                            {
                                tipLabel.Show();
                                tipLabel.Text = "该物品已存在，\n点击此处合并物品。";
                                tipLabel.Click += new EventHandler(Combine);
                                parentForm.SaveWarehouse();
                                return;
                            }
                        }

                        tipLabel.Hide();
                        try
                        {
                            tipLabel.Click -= new EventHandler(Combine);
                        } catch { }

                        parentForm.warehouseItems.FocusedItem.SubItems[0].Text = nameInput.Text;
                        parentForm.warehouseItems.FocusedItem.SubItems[1].Text = typeInput.Text;
                        parentForm.warehouseItems.FocusedItem.SubItems[2].Text = countInput.Text;
                        parentForm.SaveWarehouse();
                        Close();
                        Dispose(true);
                    }
                }
            }
            catch
            {
                countLabel.ForeColor = Color.Red;
            }

        }

        void Combine(object sender, EventArgs e) 
        {
            for (int i = 0; i < parentForm.warehouseItems.Items.Count; i++)
            {
                if (parentForm.warehouseItems.Items[i].SubItems[0].Text == nameInput.Text && parentForm.warehouseItems.Items[i].SubItems[1].Text == typeInput.Text)
                {
                    int count = int.Parse(parentForm.warehouseItems.Items[i].SubItems[2].Text);
                    count += int.Parse(countInput.Text);
                    parentForm.warehouseItems.Items[i].SubItems[2].Text = count.ToString();
                    parentForm.warehouseItems.Items.Remove(parentForm.warehouseItems.FocusedItem);
                    Close();
                    Dispose(true);
                    return;
                }
            }
        }

        private Button confirm;
        private Button cancel;
        private Label nameLabel;
        private Label typeLabel;
        private Label countLabel;
        private Label tipLabel;
        private TextBox nameInput;
        private TextBox typeInput;
        private TextBox countInput;
        private Form1 parentForm;
    }
}
