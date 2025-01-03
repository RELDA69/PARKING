using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PARKING
{
    public partial class Form1 : Form
    {
        // Use a list of dictionaries to store data with Slot, License Plate, and Status
        List<Dictionary<string, string>> parkingData = new List<Dictionary<string, string>>();
        public Form1()
        {
            InitializeComponent();

            // Set up DataGridView columns
            dgvParkingQueue.ColumnCount = 3;
            dgvParkingQueue.Columns[0].Name = "Slot";
            dgvParkingQueue.Columns[1].Name = "License Plate";
            dgvParkingQueue.Columns[2].Name = "Status";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }       
        private void UpdateDataGridView()
        {
            dgvParkingQueue.Rows.Clear();
            foreach (var data in parkingData)
            {
                dgvParkingQueue.Rows.Add(data["Slot"], data["License Plate"], data["Status"]);
            }
        }

        private void RenumberSlots()
        {
            for (int i = 0; i < parkingData.Count; i++)
            {
                parkingData[i]["Slot"] = (i + 1).ToString();
            }
        }
        //Helper function to find the next available slot
        private int FindNextAvailableSlot()
        {
            if (parkingData.Count == 0) return 1;
            return parkingData.Max(x => int.Parse(x["Slot"])) + 1;
        }

        private void btnAddVehicle_Click_1(object sender, EventArgs e)
        {
            string licensePlate = txtLicensePlate.Text;
            if (string.IsNullOrEmpty(licensePlate))
            {
                MessageBox.Show("Please enter a License Plate.");
                return;
            }

            // Find the next available slot
            int nextSlot = FindNextAvailableSlot();

            // Add data to parkingData
            parkingData.Add(new Dictionary<string, string>()
            {
                { "Slot", nextSlot.ToString() },
                { "License Plate", licensePlate },
                { "Status", "Waiting" }
            });

            UpdateDataGridView();
            txtLicensePlate.Text = "";
        }

        private void btnRemoveVehicle_Click(object sender, EventArgs e)
        {
            string licensePlateToRemove = txtLicensePlate.Text;
            if (string.IsNullOrEmpty(licensePlateToRemove))
            {
                MessageBox.Show("Please enter the license plate to remove.");
                return;
            }

            int indexToRemove = parkingData.FindIndex(x => x["License Plate"] == licensePlateToRemove);

            if (indexToRemove != -1)
            {
                parkingData.RemoveAt(indexToRemove);
                RenumberSlots(); // Call the renumbering function
                UpdateDataGridView();
                txtLicensePlate.Text = "";
            }
            else
            {
                MessageBox.Show("License plate not found in the queue.");
            }
        }

        private void btnClearQueue_Click_1(object sender, EventArgs e)
        {
            parkingData.Clear();
            UpdateDataGridView();
        }
    }
}


  
    
