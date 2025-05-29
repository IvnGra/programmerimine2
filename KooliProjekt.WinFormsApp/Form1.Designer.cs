using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;

namespace KooliProjekt.WinFormsApp
{
    partial class Form1
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            UsersGrid = new System.Windows.Forms.DataGridView();
            IdLabel = new System.Windows.Forms.Label();
            IdField = new System.Windows.Forms.TextBox();
            UserIdLabel = new System.Windows.Forms.Label();
            UserIdField = new System.Windows.Forms.TextBox();
            UsernameLabel = new System.Windows.Forms.Label();
            UsernameField = new System.Windows.Forms.TextBox();
            UserEmailLabel = new System.Windows.Forms.Label();
            UserEmailField = new System.Windows.Forms.TextBox();
            IsAdminLabel = new System.Windows.Forms.Label();
            IsAdminCheckbox = new System.Windows.Forms.CheckBox();
            NewButton = new System.Windows.Forms.Button();
            SaveButton = new System.Windows.Forms.Button();
            DeleteButton = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(UsersGrid)).BeginInit();
            SuspendLayout();

            // 
            // UsersGrid
            // 
            UsersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            UsersGrid.Location = new System.Drawing.Point(5, 6);
            UsersGrid.MultiSelect = false;
            UsersGrid.Name = "UsersGrid";
            UsersGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            UsersGrid.Size = new System.Drawing.Size(450, 400);
            UsersGrid.TabIndex = 0;

            // 
            // IdLabel
            // 
            IdLabel.AutoSize = true;
            IdLabel.Location = new System.Drawing.Point(470, 15);
            IdLabel.Name = "IdLabel";
            IdLabel.Size = new System.Drawing.Size(21, 15);
            IdLabel.TabIndex = 1;
            IdLabel.Text = "ID:";

            // 
            // IdField
            // 
            IdField.Location = new System.Drawing.Point(540, 12);
            IdField.Name = "IdField";
            IdField.ReadOnly = true;
            IdField.Size = new System.Drawing.Size(220, 23);
            IdField.TabIndex = 2;

            // 
            // UserIdLabel
            // 
            UserIdLabel.AutoSize = true;
            UserIdLabel.Location = new System.Drawing.Point(470, 50);
            UserIdLabel.Name = "UserIdLabel";
            UserIdLabel.Size = new System.Drawing.Size(46, 15);
            UserIdLabel.TabIndex = 3;
            UserIdLabel.Text = "User ID:";

            // 
            // UserIdField
            // 
            UserIdField.Location = new System.Drawing.Point(540, 47);
            UserIdField.Name = "UserIdField";
            UserIdField.Size = new System.Drawing.Size(220, 23);
            UserIdField.TabIndex = 4;

            // 
            // UsernameLabel
            // 
            UsernameLabel.AutoSize = true;
            UsernameLabel.Location = new System.Drawing.Point(470, 85);
            UsernameLabel.Name = "UsernameLabel";
            UsernameLabel.Size = new System.Drawing.Size(68, 15);
            UsernameLabel.TabIndex = 5;
            UsernameLabel.Text = "Username:";

            // 
            // UsernameField
            // 
            UsernameField.Location = new System.Drawing.Point(540, 82);
            UsernameField.Name = "UsernameField";
            UsernameField.Size = new System.Drawing.Size(220, 23);
            UsernameField.TabIndex = 6;

            // 
            // UserEmailLabel
            // 
            UserEmailLabel.AutoSize = true;
            UserEmailLabel.Location = new System.Drawing.Point(470, 120);
            UserEmailLabel.Name = "UserEmailLabel";
            UserEmailLabel.Size = new System.Drawing.Size(67, 15);
            UserEmailLabel.TabIndex = 7;
            UserEmailLabel.Text = "User Email:";

            // 
            // UserEmailField
            // 
            UserEmailField.Location = new System.Drawing.Point(540, 117);
            UserEmailField.Name = "UserEmailField";
            UserEmailField.Size = new System.Drawing.Size(220, 23);
            UserEmailField.TabIndex = 8;

            // 
            // IsAdminLabel
            // 
            IsAdminLabel.AutoSize = true;
            IsAdminLabel.Location = new System.Drawing.Point(470, 155);
            IsAdminLabel.Name = "IsAdminLabel";
            IsAdminLabel.Size = new System.Drawing.Size(57, 15);
            IsAdminLabel.TabIndex = 9;
            IsAdminLabel.Text = "Is Admin:";

            // 
            // IsAdminCheckbox
            // 
            IsAdminCheckbox.Location = new System.Drawing.Point(540, 151);
            IsAdminCheckbox.Name = "IsAdminCheckbox";
            IsAdminCheckbox.Size = new System.Drawing.Size(20, 20);
            IsAdminCheckbox.TabIndex = 10;

            // 
            // NewButton
            // 
            NewButton.Location = new System.Drawing.Point(470, 190);
            NewButton.Name = "NewButton";
            NewButton.Size = new System.Drawing.Size(75, 25);
            NewButton.TabIndex = 11;
            NewButton.Text = "New";
            NewButton.UseVisualStyleBackColor = true;

            // 
            // SaveButton
            // 
            SaveButton.Location = new System.Drawing.Point(555, 190);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new System.Drawing.Size(75, 25);
            SaveButton.TabIndex = 12;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;

            // 
            // DeleteButton
            // 
            DeleteButton.Location = new System.Drawing.Point(640, 190);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new System.Drawing.Size(75, 25);
            DeleteButton.TabIndex = 13;
            DeleteButton.Text = "Delete";
            DeleteButton.UseVisualStyleBackColor = true;

            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(DeleteButton);
            Controls.Add(SaveButton);
            Controls.Add(NewButton);
            Controls.Add(IsAdminCheckbox);
            Controls.Add(IsAdminLabel);
            Controls.Add(UserEmailField);
            Controls.Add(UserEmailLabel);
            Controls.Add(UsernameField);
            Controls.Add(UsernameLabel);
            Controls.Add(UserIdField);
            Controls.Add(UserIdLabel);
            Controls.Add(IdField);
            Controls.Add(IdLabel);
            Controls.Add(UsersGrid);
            Name = "Form1";
            Text = "User Management";

            ((System.ComponentModel.ISupportInitialize)(UsersGrid)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView UsersGrid;
        private System.Windows.Forms.Label IdLabel;
        private System.Windows.Forms.TextBox IdField;
        private System.Windows.Forms.Label UserIdLabel;
        private System.Windows.Forms.TextBox UserIdField;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.TextBox UsernameField;
        private System.Windows.Forms.Label UserEmailLabel;
        private System.Windows.Forms.TextBox UserEmailField;
        private System.Windows.Forms.Label IsAdminLabel;
        private System.Windows.Forms.CheckBox IsAdminCheckbox;
        private System.Windows.Forms.Button NewButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button DeleteButton;
    }
}
