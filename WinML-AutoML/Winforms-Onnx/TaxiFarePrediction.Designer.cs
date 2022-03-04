namespace WinForms_WinML_ONNX
{
    partial class TaxiFarePrediction
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
            this.label1 = new System.Windows.Forms.Label();
            this.passengerCountTB = new System.Windows.Forms.TextBox();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.textResponse = new System.Windows.Forms.RichTextBox();
            this.labelPrediction = new System.Windows.Forms.Label();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonRecognize = new System.Windows.Forms.Button();
            this.textUrl = new System.Windows.Forms.TextBox();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.tripTimeTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tripDistanceTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Passenger Count";
            // 
            // passengerCountTB
            // 
            this.passengerCountTB.Location = new System.Drawing.Point(169, 38);
            this.passengerCountTB.Name = "passengerCountTB";
            this.passengerCountTB.Size = new System.Drawing.Size(100, 22);
            this.passengerCountTB.TabIndex = 1;
            this.passengerCountTB.Text = "1";
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(241, 166);
            this.buttonLoad.Margin = new System.Windows.Forms.Padding(2);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(87, 43);
            this.buttonLoad.TabIndex = 14;
            this.buttonLoad.Text = "Load Model";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.ButtonLoad_Click);
            // 
            // textResponse
            // 
            this.textResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textResponse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textResponse.Font = new System.Drawing.Font("Segoe UI Light", 15.75F);
            this.textResponse.Location = new System.Drawing.Point(401, 11);
            this.textResponse.Margin = new System.Windows.Forms.Padding(2);
            this.textResponse.Name = "textResponse";
            this.textResponse.Size = new System.Drawing.Size(388, 312);
            this.textResponse.TabIndex = 13;
            this.textResponse.Text = "";
            this.textResponse.TextChanged += new System.EventHandler(this.TextResponse_TextChanged);
            // 
            // labelPrediction
            // 
            this.labelPrediction.Font = new System.Drawing.Font("Segoe UI Light", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPrediction.ForeColor = System.Drawing.Color.Maroon;
            this.labelPrediction.Location = new System.Drawing.Point(-12, 212);
            this.labelPrediction.Name = "labelPrediction";
            this.labelPrediction.Size = new System.Drawing.Size(419, 101);
            this.labelPrediction.TabIndex = 12;
            this.labelPrediction.Text = "0";
            this.labelPrediction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(149, 166);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(2);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(87, 43);
            this.buttonClear.TabIndex = 11;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.ButtonClear_Click);
            // 
            // buttonRecognize
            // 
            this.buttonRecognize.Location = new System.Drawing.Point(56, 166);
            this.buttonRecognize.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRecognize.Name = "buttonRecognize";
            this.buttonRecognize.Size = new System.Drawing.Size(87, 43);
            this.buttonRecognize.TabIndex = 10;
            this.buttonRecognize.Text = "Predict";
            this.buttonRecognize.UseVisualStyleBackColor = true;
            this.buttonRecognize.Click += new System.EventHandler(this.ButtonRecognize_Click);
            // 
            // textUrl
            // 
            this.textUrl.Font = new System.Drawing.Font("Segoe UI Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textUrl.Location = new System.Drawing.Point(-27, -82);
            this.textUrl.Margin = new System.Windows.Forms.Padding(2);
            this.textUrl.Name = "textUrl";
            this.textUrl.Size = new System.Drawing.Size(423, 42);
            this.textUrl.TabIndex = 9;
            // 
            // openFile
            // 
            this.openFile.FileName = "model";
            this.openFile.Filter = "ONNX Files|*.onnx|All Files|*.*";
            // 
            // tripTimeTB
            // 
            this.tripTimeTB.Location = new System.Drawing.Point(169, 73);
            this.tripTimeTB.Name = "tripTimeTB";
            this.tripTimeTB.Size = new System.Drawing.Size(100, 22);
            this.tripTimeTB.TabIndex = 16;
            this.tripTimeTB.Text = "1140";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Trip Time";
            // 
            // tripDistanceTB
            // 
            this.tripDistanceTB.Location = new System.Drawing.Point(169, 110);
            this.tripDistanceTB.Name = "tripDistanceTB";
            this.tripDistanceTB.Size = new System.Drawing.Size(100, 22);
            this.tripDistanceTB.TabIndex = 18;
            this.tripDistanceTB.Text = "3.75";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 17);
            this.label3.TabIndex = 17;
            this.label3.Text = "Trip Distance";
            // 
            // TaxiFarePrediction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 334);
            this.Controls.Add(this.tripDistanceTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tripTimeTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.textResponse);
            this.Controls.Add(this.labelPrediction);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonRecognize);
            this.Controls.Add(this.textUrl);
            this.Controls.Add(this.passengerCountTB);
            this.Controls.Add(this.label1);
            this.Name = "TaxiFarePrediction";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox passengerCountTB;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.RichTextBox textResponse;
        private System.Windows.Forms.Label labelPrediction;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonRecognize;
        private System.Windows.Forms.TextBox textUrl;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.TextBox tripTimeTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tripDistanceTB;
        private System.Windows.Forms.Label label3;
    }
}

