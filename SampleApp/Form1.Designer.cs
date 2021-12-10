
namespace SampleApp {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.buttonAdd = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.moveItemBehaviorAdd = new ReflectionIT.Windows.Forms.MoveItemBehavior();
            this.moveItemBehaviorDelete = new ReflectionIT.Windows.Forms.MoveItemBehavior();
            this.SuspendLayout();
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(287, 21);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(133, 50);
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.Text = "> Add >";
            this.buttonAdd.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Items.AddRange(new object[] {
            "Item 1",
            "Item 2",
            "Item 3",
            "Item 4",
            "Item 5",
            "Item 6",
            "Item 7",
            "Item 8",
            "Item 9",
            "Item 10"});
            this.listBox1.Location = new System.Drawing.Point(26, 21);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(239, 404);
            this.listBox1.TabIndex = 1;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 20;
            this.listBox2.Location = new System.Drawing.Point(439, 21);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(239, 404);
            this.listBox2.TabIndex = 2;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(287, 87);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(133, 50);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.Text = "< Delete <";
            this.buttonDelete.UseVisualStyleBackColor = true;
            // 
            // moveItemBehaviorAdd
            // 
            this.moveItemBehaviorAdd.ActionButton = this.buttonAdd;
            this.moveItemBehaviorAdd.Destination = this.listBox2;
            this.moveItemBehaviorAdd.Source = this.listBox1;
            // 
            // moveItemBehaviorDelete
            // 
            this.moveItemBehaviorDelete.ActionButton = this.buttonDelete;
            this.moveItemBehaviorDelete.Destination = this.listBox1;
            this.moveItemBehaviorDelete.Source = this.listBox2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 450);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.buttonAdd);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button buttonDelete;
        private ReflectionIT.Windows.Forms.MoveItemBehavior moveItemBehaviorAdd;
        private ReflectionIT.Windows.Forms.MoveItemBehavior moveItemBehaviorDelete;
    }
}

