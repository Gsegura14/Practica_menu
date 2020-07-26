namespace Practica_menu
{
    partial class FIvasModificar
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
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblId = new System.Windows.Forms.Label();
            this.lblIva = new System.Windows.Forms.Label();
            this.lblRe = new System.Windows.Forms.Label();
            this.txtIvaid = new System.Windows.Forms.TextBox();
            this.txtIva = new System.Windows.Forms.TextBox();
            this.txtRe = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(351, 23);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 0;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(351, 52);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(19, 105);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(34, 13);
            this.lblId.TabIndex = 2;
            this.lblId.Text = "Iva Id";
            // 
            // lblIva
            // 
            this.lblIva.AutoSize = true;
            this.lblIva.Location = new System.Drawing.Point(19, 143);
            this.lblIva.Name = "lblIva";
            this.lblIva.Size = new System.Drawing.Size(22, 13);
            this.lblIva.TabIndex = 3;
            this.lblIva.Text = "Iva";
            // 
            // lblRe
            // 
            this.lblRe.AutoSize = true;
            this.lblRe.Location = new System.Drawing.Point(19, 177);
            this.lblRe.Name = "lblRe";
            this.lblRe.Size = new System.Drawing.Size(127, 13);
            this.lblRe.TabIndex = 4;
            this.lblRe.Text = "Recargo de Equivalencia";
            // 
            // txtIvaid
            // 
            this.txtIvaid.Location = new System.Drawing.Point(71, 97);
            this.txtIvaid.Name = "txtIvaid";
            this.txtIvaid.ReadOnly = true;
            this.txtIvaid.Size = new System.Drawing.Size(181, 20);
            this.txtIvaid.TabIndex = 5;
            // 
            // txtIva
            // 
            this.txtIva.Location = new System.Drawing.Point(71, 136);
            this.txtIva.Name = "txtIva";
            this.txtIva.Size = new System.Drawing.Size(181, 20);
            this.txtIva.TabIndex = 6;
            // 
            // txtRe
            // 
            this.txtRe.Location = new System.Drawing.Point(152, 170);
            this.txtRe.Name = "txtRe";
            this.txtRe.Size = new System.Drawing.Size(100, 20);
            this.txtRe.TabIndex = 7;
            // 
            // FIvasModificar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 243);
            this.Controls.Add(this.txtRe);
            this.Controls.Add(this.txtIva);
            this.Controls.Add(this.txtIvaid);
            this.Controls.Add(this.lblRe);
            this.Controls.Add(this.lblIva);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FIvasModificar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ivas :: Nuevo";
            this.Load += new System.EventHandler(this.FIvasModificar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblIva;
        private System.Windows.Forms.Label lblRe;
        private System.Windows.Forms.TextBox txtIvaid;
        private System.Windows.Forms.TextBox txtIva;
        private System.Windows.Forms.TextBox txtRe;
    }
}