
namespace SampleAppFramework {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.moveItemBehaviorAdd = new ReflectionIT.Windows.Forms.DualList();
            this.moveItemBehaviorDelete = new ReflectionIT.Windows.Forms.DualList();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(318, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 45);
            this.button1.TabIndex = 0;
            this.button1.Text = "> Add >";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Items.AddRange(new object[] {
            "Item 1",
            "Item 2",
            "Item 3",
            "Item 4",
            "Item 5"});
            this.listBox1.Location = new System.Drawing.Point(27, 30);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(269, 388);
            this.listBox1.TabIndex = 3;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 16;
            this.listBox2.Location = new System.Drawing.Point(447, 30);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(269, 388);
            this.listBox2.TabIndex = 4;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(318, 81);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(108, 45);
            this.button2.TabIndex = 5;
            this.button2.Text = "< Delete <";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // moveItemBehaviorAdd
            // 
            this.moveItemBehaviorAdd.Button = this.button1;
            this.moveItemBehaviorAdd.ListBoxTo = this.listBox2;
            this.moveItemBehaviorAdd.ListBoxFrom = this.listBox1;
            // 
            // moveItemBehaviorDelete
            // 
            this.moveItemBehaviorDelete.Button = this.button2;
            this.moveItemBehaviorDelete.ListBoxTo = this.listBox1;
            this.moveItemBehaviorDelete.ListBoxFrom = this.listBox2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 435);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button button2;

        private ReflectionIT.Windows.Forms.DualList moveItemBehaviorAdd;
        private ReflectionIT.Windows.Forms.DualList moveItemBehaviorDelete;
    }
}

