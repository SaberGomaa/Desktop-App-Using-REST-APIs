using Desktop_App_Using_REST_APIs;

namespace Desktop_App_API
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:82/api/");

            InitializeComponent();
            viewAllEmployees();
            viewAllEmployeesComboBox();
            viewAllDepartmentsComboBox();

        }

        private void viewAllDepartmentsComboBox()
        {
            HttpClient Client = new HttpClient();

            var result = Client.GetAsync("http://127.0.0.1:82/api/department").Result;

            var depts = result.Content.ReadAsAsync<List<Department>>().Result;

            
            comboBox2.DataSource= depts;
            comboBox2.SelectedIndex = 0;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "DepartmentId";

        }

        private void viewAllEmployeesComboBox()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:82/api/");

            var result = client.GetAsync("employee").Result;

            if (result.IsSuccessStatusCode)
            {
                var emps = result.Content.ReadAsAsync<List<Employee>>().Result;
               
                comboBox2.ValueMember = "Id";
                comboBox1.DisplayMember = "Name";
                comboBox1.DataSource=emps;
                comboBox1.SelectedIndex = 0;
            }
        }

        private void viewAllEmployees()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:82/api/");

            var result = client.GetAsync("employee").Result;
            if (result.IsSuccessStatusCode)
            {
                var emps = result.Content.ReadAsAsync<List<Employee>>().Result;

                dataGridView1.DataSource = emps;
            }
            else
            {
                MessageBox.Show(result.StatusCode.ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee
            {

                //Id = int.Parse(textBox1.Text), // useless case
                Name = textBox1.Text,
                Salary = decimal.Parse(textBox3.Text),
                Age = int.Parse(textBox2.Text),
                DepartmentId = int.Parse(comboBox2.SelectedValue.ToString())

            };

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://127.0.0.1:82/api/");

            var result = client.PostAsJsonAsync("employee", emp).Result;

            if (result.IsSuccessStatusCode)
            {
                viewAllEmployees();
                //viewAllEmployeesComboBox();
            }
            else
            {
                MessageBox.Show(result.StatusCode.ToString());
            }
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();

            int Id = int.Parse(textBox5.Text);

            var result = client.DeleteAsync($"http://localhost:82/api/employee/{textBox5.Text}");


            viewAllEmployees();

            textBox5.Clear();

        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
        }

    }
}
