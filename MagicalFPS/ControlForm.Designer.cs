namespace MagicalFPS
{
    partial class ControlForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ed_MakeFar = new System.Windows.Forms.Button();
            this.ed_MakeNear = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lo_MakeFar = new System.Windows.Forms.Button();
            this.lo_MakeNear = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.bo_MakeFar = new System.Windows.Forms.Button();
            this.bo_MakeNear = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ed_MakeFar);
            this.groupBox1.Controls.Add(this.ed_MakeNear);
            this.groupBox1.Location = new System.Drawing.Point(1, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "EyeDistance";
            // 
            // ed_MakeFar
            // 
            this.ed_MakeFar.Location = new System.Drawing.Point(119, 71);
            this.ed_MakeFar.Name = "ed_MakeFar";
            this.ed_MakeFar.Size = new System.Drawing.Size(75, 23);
            this.ed_MakeFar.TabIndex = 1;
            this.ed_MakeFar.Text = "遠ざける";
            this.ed_MakeFar.UseVisualStyleBackColor = true;
            this.ed_MakeFar.Click += new System.EventHandler(this.ed_MakeFar_Click);
            // 
            // ed_MakeNear
            // 
            this.ed_MakeNear.Location = new System.Drawing.Point(6, 18);
            this.ed_MakeNear.Name = "ed_MakeNear";
            this.ed_MakeNear.Size = new System.Drawing.Size(75, 23);
            this.ed_MakeNear.TabIndex = 0;
            this.ed_MakeNear.Text = "近づける";
            this.ed_MakeNear.UseVisualStyleBackColor = true;
            this.ed_MakeNear.Click += new System.EventHandler(this.ed_MakeNear_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lo_MakeFar);
            this.groupBox2.Controls.Add(this.lo_MakeNear);
            this.groupBox2.Location = new System.Drawing.Point(207, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "LensOffset";
            // 
            // lo_MakeFar
            // 
            this.lo_MakeFar.Location = new System.Drawing.Point(119, 71);
            this.lo_MakeFar.Name = "lo_MakeFar";
            this.lo_MakeFar.Size = new System.Drawing.Size(75, 23);
            this.lo_MakeFar.TabIndex = 1;
            this.lo_MakeFar.Text = "遠ざける";
            this.lo_MakeFar.UseVisualStyleBackColor = true;
            this.lo_MakeFar.Click += new System.EventHandler(this.lo_MakeFar_Click);
            // 
            // lo_MakeNear
            // 
            this.lo_MakeNear.Location = new System.Drawing.Point(6, 18);
            this.lo_MakeNear.Name = "lo_MakeNear";
            this.lo_MakeNear.Size = new System.Drawing.Size(75, 23);
            this.lo_MakeNear.TabIndex = 0;
            this.lo_MakeNear.Text = "近づける";
            this.lo_MakeNear.UseVisualStyleBackColor = true;
            this.lo_MakeNear.Click += new System.EventHandler(this.lo_MakeNear_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.bo_MakeFar);
            this.groupBox3.Controls.Add(this.bo_MakeNear);
            this.groupBox3.Location = new System.Drawing.Point(7, 106);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 100);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "BatchOffset";
            // 
            // bo_MakeFar
            // 
            this.bo_MakeFar.Location = new System.Drawing.Point(119, 71);
            this.bo_MakeFar.Name = "bo_MakeFar";
            this.bo_MakeFar.Size = new System.Drawing.Size(75, 23);
            this.bo_MakeFar.TabIndex = 1;
            this.bo_MakeFar.Text = "遠ざける";
            this.bo_MakeFar.UseVisualStyleBackColor = true;
            this.bo_MakeFar.Click += new System.EventHandler(this.bo_MakeFar_Click);
            // 
            // bo_MakeNear
            // 
            this.bo_MakeNear.Location = new System.Drawing.Point(6, 18);
            this.bo_MakeNear.Name = "bo_MakeNear";
            this.bo_MakeNear.Size = new System.Drawing.Size(75, 23);
            this.bo_MakeNear.TabIndex = 0;
            this.bo_MakeNear.Text = "近づける";
            this.bo_MakeNear.UseVisualStyleBackColor = true;
            this.bo_MakeNear.Click += new System.EventHandler(this.bo_MakeNear_Click);
            // 
            // ControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 381);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ControlForm";
            this.Text = "ControlForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button ed_MakeFar;
        private System.Windows.Forms.Button ed_MakeNear;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button lo_MakeFar;
        private System.Windows.Forms.Button lo_MakeNear;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button bo_MakeFar;
        private System.Windows.Forms.Button bo_MakeNear;
    }
}