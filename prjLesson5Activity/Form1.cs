using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjLesson5Activity
{
    public partial class frmPayslip : Form
    {
        public frmPayslip()
        {
            InitializeComponent();
        }

        private void btnGrossIncome_Click(object sender, EventArgs e)
        {
            // BASIC
            double basicRate = Convert.ToDouble(txtBasicRate.Text);
            double basicHours = Convert.ToDouble(txtBasicHours.Text);
            double basicIncome = basicRate * basicHours;
            txtBasicIncome.Text = basicIncome.ToString();

            // HONORARIUM
            double honorRate = Convert.ToDouble(txtHonorRate.Text);
            double honorHours = Convert.ToDouble(txtHonorHours.Text);
            double honorIncome = honorRate * honorHours;
            txtHonorIncome.Text = honorIncome.ToString();

            // OTHER
            double otherRate = Convert.ToDouble(txtOtherRate.Text);
            double otherHours = Convert.ToDouble(txtOtherHours.Text);
            double otherIncome = otherRate * otherHours;
            txtOtherIncome.Text = otherIncome.ToString();

            // GROSS
            double grossIncome = basicIncome + honorIncome + otherIncome;
            txtGrossIncome.Text = grossIncome.ToString();

            // SAMPLE DEDUCTION COMPUTATION
            txtPagibig.Text = "200";

            double sss = grossIncome * 0.05;
            double philHealth = grossIncome * 0.025;
            double pagIbig = 200;

            txtSSS.Text = sss.ToString("N2");
            txtPhilHealth.Text = philHealth.ToString("N2");
            txtPagibig.Text = pagIbig.ToString("N2");
            double taxableIncome = grossIncome - (sss + philHealth + pagIbig);

            double incomeTax = 0;


            if (taxableIncome > 20833.33 && taxableIncome <= 33333.33)
            {
                // Bracket: Over 250k - 400k Annual (15% of excess over 20,833.33 monthly)
                incomeTax = (taxableIncome - 20833.33) * 0.15;
            }
            else if (taxableIncome > 33333.33 && taxableIncome <= 66666.67)
            {
                // Bracket: Over 400k - 800k Annual (1,875 fixed + 20% of excess over 33,333.33)
                incomeTax = 1875 + (taxableIncome - 33333.33) * 0.20;
            }
            else if (taxableIncome > 66666.67 && taxableIncome <= 166666.67)
            {
                // Bracket: Over 800k - 2M Annual (8,541.67 fixed + 25% of excess over 66,666.67)
                incomeTax = 8541.67 + (taxableIncome - 66666.67) * 0.25;
            }
            else if (taxableIncome <= 20833.33)
            {
                // 250k and below annually is 0% tax
                incomeTax = 0;
            }

            txtTax.Text = incomeTax.ToString("C");
        }

        private void btnNetIncome_Click(object sender, EventArgs e)
        {
            // Function to safely get a value or return 0 if empty
            double GetVal(TextBox txt)
            {
                double.TryParse(txt.Text, out double result);
                return result;
            }

            // 1. GET GROSS INCOME
            double grossIncome = GetVal(txtGrossIncome);

            // 2. REGULAR DEDUCTIONS
            double sss = GetVal(txtSSS);
            double philhealth = GetVal(txtPhilHealth);
            double pagibig = GetVal(txtPagibig);
            double tax = GetVal(txtTax);

            // 3. OTHER DEDUCTIONS (Safely handles empty boxes)
            double sssLoan = GetVal(txtSSSLoan);
            double pagibigLoan = GetVal(txtPagibigLoan);
            double savingsDeposit = GetVal(txtFacultySavingsDeposit);
            double savingsLoan = GetVal(txtFacultySavingsLoan);
            double salaryLoan = GetVal(txtSalaryLoan);
            double otherLoan = GetVal(txtOtherLoan);

            // 4. CALCULATIONS
            double totalDeductions = sss + philhealth + pagibig + tax +
                                     sssLoan + pagibigLoan + savingsDeposit +
                                     savingsLoan + salaryLoan + otherLoan;

            double netIncome = grossIncome - totalDeductions;

            // 5. DISPLAY (Using "N2" for standard number formatting)
            txtTotalDeductions.Text = totalDeductions.ToString("N2");
            txtNetIncome.Text = netIncome.ToString("N2");

        }

        private void txtEmployeeNumber_TextChanged(object sender, EventArgs e)
        {
            string employeeNumber = txtEmployeeNumber.Text;
            if (employeeNumber == "202511340")
            {
                txtDepartment.Text = "College of CCIT";
                txtFirstName.Text = "John Edward";
                txtMiddleName.Text = "Tamayo";
                txtSurname.Text = "Pascual";
                txtCivilStatus.Text = "Single";
                txtEmployeeStatus.Text = "Full-Time";
                txtDesignation.Text = "Manager";
                picEmployeePicture.Load(@"C:\edward.jpg");
                picEmployeePicture.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else if (employeeNumber == "2025050606")
            {
                txtDepartment.Text = "College of Business";
                txtFirstName.Text = "Nathan ";
                txtMiddleName.Text = "Santillan";
                txtSurname.Text = "Rapadas";
                txtCivilStatus.Text = "Single";
                txtEmployeeStatus.Text = "Full-Time";
                txtDesignation.Text = "CEO";
                picEmployeePicture.Load(@"C:\nathan.jpg");
                picEmployeePicture.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else if (employeeNumber == "2025030505")
            {
                txtDepartment.Text = "College of Architecture";
                txtFirstName.Text = "Jerald";
                txtMiddleName.Text = "Rapadas";
                txtSurname.Text = "Santillan";
                txtCivilStatus.Text = "Married";
                txtEmployeeStatus.Text = "Full-Time";
                txtDesignation.Text = "Specialist";
                picEmployeePicture.Load(@"C:\jerald.jpg");
                picEmployeePicture.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                // This is the "default" case
                txtDepartment.Clear();
                txtFirstName.Clear();
                txtMiddleName.Clear();
                txtSurname.Clear();
                txtCivilStatus.Clear();
                txtEmployeeStatus.Clear();
                txtDesignation.Clear();
                picEmployeePicture.Image = null;
            }


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Helper to remove ₱ and commas so the code doesn't crash
            double ParseNumeric(string input)
            {
                string clean = input.Replace("₱", "").Replace(",", "").Trim();
                double.TryParse(clean, out double result);
                return result;
            }

            // 1. BASIC INFO REDIRECT
            txtPayslipCompany.Text = "Adamson University";
            txtPayslipEmployeeCode.Text = txtEmployeeNumber.Text;
            txtPayslipEmployeeName.Text = $"{txtSurname.Text}, {txtFirstName.Text} {txtMiddleName.Text}";
            txtPayslipDepartment.Text = txtDepartment.Text;
            txtPayslipPayPeriod.Text = txtPayDate.Text;
            txtPayslipCutOff.Text = txtPayDate.Text;


            // 2. EARNINGS REDIRECT 
            // Make sure these names match your Payslip TextBoxes exactly
            txtPayslipBasicPay.Text = txtBasicIncome.Text;
            txtPayslipOvertime.Text = txtOtherIncome.Text;
            txtPayslipHonorarium.Text = txtHonorIncome.Text;
            txtPayslipHonorAdjustment.Text = "0";
            txtPayslipSubstitution.Text = "0";
            txtPayslipTardy.Text = "0";

            // 3. DEDUCTIONS REDIRECT
            txtPayslipTax.Text = txtTax.Text;
            txtPayslipSSS.Text = txtSSS.Text;
            txtPayslipPhilhealth.Text = txtPhilHealth.Text;
            txtPayslipHDMF.Text = txtPagibig.Text;
            txtPayslipSSS_WISP.Text = "750.00";

            // 4. CALCULATE TOTALS
            double earnings = ParseNumeric(txtBasicIncome.Text) +
                              ParseNumeric(txtOtherIncome.Text) +
                              ParseNumeric(txtHonorIncome.Text);

            double deductions = ParseNumeric(txtTax.Text) +
                                ParseNumeric(txtSSS.Text) +
                                ParseNumeric(txtPhilHealth.Text) +
                                ParseNumeric(txtPagibig.Text) + 750.00; // Including WISP

            // 5. UPDATE SUMMARY LABELS
            txtPayslipGrossEarnings.Text = earnings.ToString("N2");
            txtPayslipTotalDeductions.Text = deductions.ToString("N2");
            txtPayslipNetPay.Text = (earnings - deductions).ToString("N2");

            txtEarningsDisplay.Text = earnings.ToString("N2");
            txtDeductionsDisplay.Text = deductions.ToString("N2");
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            // 1. Clear textboxes on the main form (Left side)
            foreach (Control c in this.Controls)
            {
                if (c is TextBox) c.Text = "";
            }

            // 2. Clear textboxes inside your GroupBox (Right side/Payslip)
            foreach (Control c in groupBox1.Controls)
            {
                if (c is TextBox) c.Text = "";
            }

            foreach (Control c in panel1.Controls)
            {
                if (c is TextBox) c.Text = "";
            }

            foreach (Control c in panel2.Controls)
            {
                if (c is TextBox) c.Text = "";
            }

            foreach (Control c in panel3.Controls)
            {
                if (c is TextBox) c.Text = "";
            }
            // 3. Clear the photo and labels
            if (picEmployeePicture.Image != null)
            {
                picEmployeePicture.Image.Dispose();
                picEmployeePicture.Image = null;
            }

            // Reset summary labels to zero
            txtEarningsDisplay.Text = "₱0.00";
            txtDeductionsDisplay.Text = "₱0.00";
            txtOvertimeDisplay.Text = "₱0.00";

            txtEmployeeNumber.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("UPDATED!");
        }
    }
}

