namespace Practica_menu
{
    partial class MaestroClientes
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
            this.btnSeleccionar = new System.Windows.Forms.Button();
            this.txtBuscaClientes = new System.Windows.Forms.TextBox();
            this.dataGridViewMClientes = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMClientes)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.Location = new System.Drawing.Point(557, 32);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(75, 23);
            this.btnSeleccionar.TabIndex = 0;
            this.btnSeleccionar.Text = "Aceptar";
            this.btnSeleccionar.UseVisualStyleBackColor = true;
            this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click);
            // 
            // txtBuscaClientes
            // 
            this.txtBuscaClientes.Location = new System.Drawing.Point(12, 35);
            this.txtBuscaClientes.Name = "txtBuscaClientes";
            this.txtBuscaClientes.Size = new System.Drawing.Size(525, 20);
            this.txtBuscaClientes.TabIndex = 1;
            this.txtBuscaClientes.TextChanged += new System.EventHandler(this.txtBuscaClientes_TextChanged);
            this.txtBuscaClientes.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBuscaClientes_KeyUp);
            // 
            // dataGridViewMClientes
            // 
            this.dataGridViewMClientes.AllowUserToAddRows = false;
            this.dataGridViewMClientes.AllowUserToDeleteRows = false;
            this.dataGridViewMClientes.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dataGridViewMClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMClientes.Location = new System.Drawing.Point(12, 62);
            this.dataGridViewMClientes.Name = "dataGridViewMClientes";
            this.dataGridViewMClientes.ReadOnly = true;
            this.dataGridViewMClientes.Size = new System.Drawing.Size(620, 360);
            this.dataGridViewMClientes.TabIndex = 2;
            // 
            // MaestroClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 459);
            this.Controls.Add(this.dataGridViewMClientes);
            this.Controls.Add(this.txtBuscaClientes);
            this.Controls.Add(this.btnSeleccionar);
            this.Name = "MaestroClientes";
            this.Text = "maestroClientes";
            this.Load += new System.EventHandler(this.MaestroClientes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMClientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSeleccionar;
        private System.Windows.Forms.TextBox txtBuscaClientes;
        private System.Windows.Forms.DataGridView dataGridViewMClientes;
    }
}