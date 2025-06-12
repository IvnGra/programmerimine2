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
            UsersGrid = new DataGridView();
            IdLabel = new Label();
            IdField = new TextBox();
            UserIdLabel = new Label();
            UserIdField = new TextBox();
            UsernameLabel = new Label();
            UsernameField = new TextBox();
            UserEmailLabel = new Label();
            UserEmailField = new TextBox();
            IsAdminLabel = new Label();
            IsAdminCheckbox = new CheckBox();
            NewButton = new Button();
            SaveButton = new Button();
            DeleteButton = new Button();
            ((System.ComponentModel.ISupportInitialize)UsersGrid).BeginInit();
            SuspendLayout();
            // 
            // UsersGrid
            // 
            UsersGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            UsersGrid.Location = new Point(5, 6);
            UsersGrid.MultiSelect = false;
            UsersGrid.Name = "UsersGrid";
            UsersGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            UsersGrid.Size = new Size(450, 400);
            UsersGrid.TabIndex = 0;
            // 
            // IdLabel
            // 
            IdLabel.AutoSize = true;
            IdLabel.Location = new Point(470, 15);
            IdLabel.Name = "IdLabel";
            IdLabel.Size = new Size(21, 15);
            IdLabel.TabIndex = 1;
            IdLabel.Text = "ID:";
            // 
            // IdField
            // 
            IdField.Location = new Point(540, 12);
            IdField.Name = "IdField";
            IdField.ReadOnly = true;
            IdField.Size = new Size(220, 23);
            IdField.TabIndex = 2;
            // 
            // UserIdLabel
            // 
            UserIdLabel.AutoSize = true;
            UserIdLabel.Location = new Point(470, 50);
            UserIdLabel.Name = "UserIdLabel";
            UserIdLabel.Size = new Size(47, 15);
            UserIdLabel.TabIndex = 3;
            UserIdLabel.Text = "User ID:";
            // 
            // UserIdField
            // 
            UserIdField.Location = new Point(540, 47);
            UserIdField.Name = "UserIdField";
            UserIdField.Size = new Size(220, 23);
            UserIdField.TabIndex = 4;
            // 
            // UsernameLabel
            // 
            UsernameLabel.AutoSize = true;
            UsernameLabel.Location = new Point(470, 85);
            UsernameLabel.Name = "UsernameLabel";
            UsernameLabel.Size = new Size(63, 15);
            UsernameLabel.TabIndex = 5;
            UsernameLabel.Text = "Username:";
            // 
            // UsernameField
            // 
            UsernameField.Location = new Point(540, 82);
            UsernameField.Name = "UsernameField";
            UsernameField.Size = new Size(220, 23);
            UsernameField.TabIndex = 6;
            // 
            // UserEmailLabel
            // 
            UserEmailLabel.AutoSize = true;
            UserEmailLabel.Location = new Point(470, 120);
            UserEmailLabel.Name = "UserEmailLabel";
            UserEmailLabel.Size = new Size(65, 15);
            UserEmailLabel.TabIndex = 7;
            UserEmailLabel.Text = "User Email:";
            // 
            // UserEmailField
            // 
            UserEmailField.Location = new Point(540, 117);
            UserEmailField.Name = "UserEmailField";
            UserEmailField.Size = new Size(220, 23);
            UserEmailField.TabIndex = 8;
            // 
            // IsAdminLabel
            // 
            IsAdminLabel.AutoSize = true;
            IsAdminLabel.Location = new Point(470, 155);
            IsAdminLabel.Name = "IsAdminLabel";
            IsAdminLabel.Size = new Size(57, 15);
            IsAdminLabel.TabIndex = 9;
            IsAdminLabel.Text = "Is Admin:";
            // 
            // IsAdminCheckbox
            // 
            IsAdminCheckbox.Location = new Point(540, 153);
            IsAdminCheckbox.Name = "IsAdminCheckbox";
            IsAdminCheckbox.Size = new Size(20, 20);
            IsAdminCheckbox.TabIndex = 10;
            // 
            // NewButton
            // 
            NewButton.Location = new Point(470, 190);
            NewButton.Name = "NewButton";
            NewButton.Size = new Size(75, 25);
            NewButton.TabIndex = 11;
            NewButton.Text = "New";
            NewButton.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(555, 190);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(75, 25);
            SaveButton.TabIndex = 12;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            // 
            // DeleteButton
            // 
            DeleteButton.Location = new Point(640, 190);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(75, 25);
            DeleteButton.TabIndex = 13;
            DeleteButton.Text = "Delete";
            DeleteButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
            ((System.ComponentModel.ISupportInitialize)UsersGrid).EndInit();
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
