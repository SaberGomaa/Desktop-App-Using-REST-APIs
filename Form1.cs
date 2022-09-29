using Desktop_App_Using_REST_APIs;

namespace Desktop_App_API
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            viewAllEmployees();
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
                DepartmentId = int.Parse(textBox4.Text)

            };

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://127.0.0.1:82/api/");

            var result = client.PostAsJsonAsync("employee", emp).Result;

            if (result.IsSuccessStatusCode)
            {
                viewAllEmployees();
            }
            else
            {
                MessageBox.Show(result.StatusCode.ToString());
            }
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

        }
    }
}
