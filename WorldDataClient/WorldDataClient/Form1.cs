using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldDataClient.ServiceReference1;
using System.Data;
using System.Windows.Forms;

namespace WorldDataClient
{
    public partial class Form1 : Form
    {
        private WorldServiceSoapClient client; // Khai báo client toàn cục
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new WorldServiceSoapClient();
            cmbFunctions.Items.AddRange(new string[]
            {
                "GetAllCountries",
                "GetCountryByCode",
                "GetCityByName",
                "GetCitiesByCountryCode",
                "GetCountriesByRegion",
                "GetPopulationByCountry"
            });
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            string selectedFunction = cmbFunctions.SelectedItem?.ToString();
            string parameter = txtParameter.Text;

            try
            {
                switch (selectedFunction)
                {
                    case "GetAllCountries":
                        dataGridView.DataSource = client.GetAllCountries();
                        break;

                    case "GetCountryByCode":
                        if (!string.IsNullOrEmpty(parameter))
                            dataGridView.DataSource = client.GetCountryByCode(parameter);
                        else
                            MessageBox.Show("Please enter a country code.");
                        break;

                    case "GetCityByName":
                        if (!string.IsNullOrEmpty(parameter))
                            dataGridView.DataSource = client.GetCityByName(parameter);
                        else
                            MessageBox.Show("Please enter a city name.");
                        break;

                    case "GetCitiesByCountryCode":
                        if (!string.IsNullOrEmpty(parameter))
                            dataGridView.DataSource = client.GetCitiesByCountryCode(parameter);
                        else
                            MessageBox.Show("Please enter a country code.");
                        break;

                    case "GetCountriesByRegion":
                        if (!string.IsNullOrEmpty(parameter))
                            dataGridView.DataSource = client.GetCountriesByRegion(parameter);
                        else
                            MessageBox.Show("Please enter a region.");
                        break;

                    case "GetPopulationByCountry":
                        if (!string.IsNullOrEmpty(parameter))
                        {
                            int population = client.GetPopulationByCountry(parameter);
                            lblPopulation.Text = $"Population: {population}";
                        }
                        else
                            MessageBox.Show("Please enter a country code.");
                        break;

                    default:
                        MessageBox.Show("Please select a function.");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
