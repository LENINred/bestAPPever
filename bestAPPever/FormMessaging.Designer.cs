namespace bestAPPever
{
    partial class FormMessaging
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
            this.groupBoxMessages = new System.Windows.Forms.GroupBox();
            this.groupBoxDialogues = new System.Windows.Forms.GroupBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.textBoxMessage = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // groupBoxMessages
            // 
            this.groupBoxMessages.Location = new System.Drawing.Point(218, 12);
            this.groupBoxMessages.Name = "groupBoxMessages";
            this.groupBoxMessages.Size = new System.Drawing.Size(334, 422);
            this.groupBoxMessages.TabIndex = 0;
            this.groupBoxMessages.TabStop = false;
            this.groupBoxMessages.Text = "Переписка";
            // 
            // groupBoxDialogues
            // 
            this.groupBoxDialogues.Location = new System.Drawing.Point(12, 12);
            this.groupBoxDialogues.Name = "groupBoxDialogues";
            this.groupBoxDialogues.Size = new System.Drawing.Size(200, 501);
            this.groupBoxDialogues.TabIndex = 2;
            this.groupBoxDialogues.TabStop = false;
            this.groupBoxDialogues.Text = "Диалоги";
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(478, 441);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(74, 75);
            this.buttonSend.TabIndex = 3;
            this.buttonSend.Text = "Отправить";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxMessage.Location = new System.Drawing.Point(219, 441);
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.Size = new System.Drawing.Size(253, 75);
            this.textBoxMessage.TabIndex = 4;
            this.textBoxMessage.Text = "";
            // 
            // FormMessaging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 525);
            this.Controls.Add(this.textBoxMessage);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.groupBoxDialogues);
            this.Controls.Add(this.groupBoxMessages);
            this.Name = "FormMessaging";
            this.Text = "Messaging";
            this.Load += new System.EventHandler(this.FormMessaging_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxMessages;
        private System.Windows.Forms.GroupBox groupBoxDialogues;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.RichTextBox textBoxMessage;
    }
}