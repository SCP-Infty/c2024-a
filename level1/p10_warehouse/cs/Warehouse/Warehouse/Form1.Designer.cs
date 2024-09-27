namespace Warehouse
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.warehouseItems = new System.Windows.Forms.ListView();
            this.addItem = new System.Windows.Forms.Button();
            this.removeItem = new System.Windows.Forms.Button();
            this.merge = new System.Windows.Forms.Button();
            this.saveAndQuit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // warehouseItems
            // 
            this.warehouseItems.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.warehouseItems.HideSelection = false;
            this.warehouseItems.Location = new System.Drawing.Point(50, 50);
            this.warehouseItems.Name = "warehouseItems";
            this.warehouseItems.Size = new System.Drawing.Size(121, 97);
            this.warehouseItems.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.warehouseItems.TabIndex = 0;
            this.warehouseItems.UseCompatibleStateImageBehavior = false;
            // 
            // addItem
            // 
            this.addItem.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addItem.Location = new System.Drawing.Point(678, 200);
            this.addItem.Name = "addItem";
            this.addItem.Size = new System.Drawing.Size(200, 64);
            this.addItem.TabIndex = 1;
            this.addItem.Text = "入库";
            this.addItem.UseVisualStyleBackColor = true;
            // 
            // removeItem
            // 
            this.removeItem.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.removeItem.Location = new System.Drawing.Point(678, 350);
            this.removeItem.Name = "removeItem";
            this.removeItem.Size = new System.Drawing.Size(200, 64);
            this.removeItem.TabIndex = 2;
            this.removeItem.Text = "出库";
            this.removeItem.UseVisualStyleBackColor = true;
            // 
            // merge
            // 
            this.merge.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.merge.Location = new System.Drawing.Point(678, 497);
            this.merge.Name = "merge";
            this.merge.Size = new System.Drawing.Size(200, 64);
            this.merge.TabIndex = 3;
            this.merge.Text = "修改";
            this.merge.UseVisualStyleBackColor = true;
            // 
            // saveAndQuit
            // 
            this.saveAndQuit.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.saveAndQuit.Location = new System.Drawing.Point(678, 638);
            this.saveAndQuit.Name = "saveAndQuit";
            this.saveAndQuit.Size = new System.Drawing.Size(200, 64);
            this.saveAndQuit.TabIndex = 4;
            this.saveAndQuit.Text = "保存&退出";
            this.saveAndQuit.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1427, 781);
            this.Controls.Add(this.saveAndQuit);
            this.Controls.Add(this.merge);
            this.Controls.Add(this.removeItem);
            this.Controls.Add(this.addItem);
            this.Controls.Add(this.warehouseItems);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ListView warehouseItems;
        private System.Windows.Forms.Button addItem;
        private System.Windows.Forms.Button removeItem;
        private System.Windows.Forms.Button merge;
        private System.Windows.Forms.Button saveAndQuit;
    }
}

